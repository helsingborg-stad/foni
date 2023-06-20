using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foni.Code.AsyncSystem;
using Foni.Code.DataSourceSystem;
using Foni.Code.DataSourceSystem.Implementation.StreamingAssetDataSource;
using Foni.Code.PhoneticsSystem;
using Foni.Code.Util;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

namespace Foni.Code.Core
{
    public struct LoadedAssets
    {
        public List<Letter> Letters;
        public List<Word> Words;
        public string[][] RoundConfig;
    }

    public struct GameState
    {
        public bool IsGameActive;
        public int CurrentRound;
        public int CurrentLetter;
        public List<Letter> ActiveLetters;
        public List<RevealObjectComponent> RevealObjects;
    }

    public class CoreGameMode : MonoBehaviour
    {
        [FormerlySerializedAs("tree")]
        [Header("References")] //
        [SerializeField]
        private PhoneticsTree phoneticsTree;

        [SerializeField] private List<RevealObjectComponent> revealObjects;

        private LoadedAssets _loadedAssets;
        private GameState _gameState;
        private IDataSource _assetDataSource;
        private Random _randomness;

        private IEnumerator Start()
        {
            _assetDataSource = new StreamingAssetsDataSource();
            yield return new WaitForSeconds(1f);
            yield return DoInitialLoad();
        }

        private IEnumerator DoInitialLoad()
        {
            Globals.GlobalsManager.SetupGlobals();
            yield return new WaitForTask(LoadAssets);
        }

        private async Task LoadAssets()
        {
            var words = await WordSerialization.LoadFromDataSource(_assetDataSource);
            _loadedAssets = new LoadedAssets
            {
                Letters = await LetterSerialization.LoadFromDataSource(_assetDataSource, words),
                Words = words,
                RoundConfig = new[]
                {
                    new[] { "s", "i", "l" },
                    new[] { "o", "m" },
                    new[] { "e", "n" },
                    new[] { "a", "t" },
                    new[] { "b", "u" },
                    new[] { "r", "f" },
                    new[] { "d", "å" },
                    new[] { "j", "k" },
                    new[] { "g", "p" },
                    new[] { "h", "y" },
                    new[] { "v", "ä" },
                    new[] { "ö" }
                }
            };

            Debug.LogFormat("Loaded words: {0}", _loadedAssets.Words.Count);
            Debug.LogFormat("Loaded letters: {0}", _loadedAssets.Letters.Count);

            Globals.ServiceLocator.AsyncService.QueueOnMainThread(StartGame_Entry);
        }

        private void StartGame_Entry()
        {
            StartCoroutine(StartGame());
        }

        private IEnumerator StartGame()
        {
            Debug.Log("Starting game");
            _randomness = new Random(1337);

            _gameState = new GameState
            {
                IsGameActive = false,
                CurrentRound = -1,
                CurrentLetter = -1,
                ActiveLetters = new List<Letter>(),
                RevealObjects = new List<RevealObjectComponent>()
            };

            phoneticsTree.OnLeafClicked += OnPhoneticsTreeLeafClicked;

            yield return SetupNextRound();
            yield return DoOpeningAnimation();
            _gameState.IsGameActive = true;
            yield return StartNextGuess();
        }

        private void OnPhoneticsTreeLeafClicked(object sender, LetterComponent letterComponent)
        {
            if (!_gameState.IsGameActive)
            {
                return;
            }

            var guessedLetter = letterComponent.Letter;
            var correctLetter = _gameState.ActiveLetters[_gameState.CurrentLetter];
            var guessedCorrectly = guessedLetter.ID == correctLetter.ID;

            Debug.LogFormat("Guessed correctly? {0}", guessedCorrectly);

            if (guessedCorrectly)
            {
                letterComponent.SetState(ELetterState.Correct);
                StartCoroutine(DoGuessedCorrectly());
            }
            else
            {
                letterComponent.SetState(ELetterState.Incorrect);
            }
        }

        private IEnumerator SetupNextRound()
        {
            ++_gameState.CurrentRound;

            var lettersForRound = _loadedAssets.RoundConfig[_gameState.CurrentRound]
                .ToList()
                .ConvertAll(character => _loadedAssets.Letters
                    .Find(letter => letter.ID == character));

            var lettersNotPartOfRound =
                _loadedAssets.Letters
                    .Except(lettersForRound)
                    .InRandomOrder(_randomness);

            var allLettersToShow =
                lettersForRound
                    .Pad(lettersNotPartOfRound, phoneticsTree.RequiredLetterCount)
                    .InRandomOrder(_randomness);

            var wordsForRound = lettersForRound
                .ConvertAll(letter => letter.Words.PickRandom(_randomness));

            revealObjects
                .ConvertAll(GameObjectUtils.GetGameObject)
                .ForEach(GameObjectUtils.DisableIfEnabled);

            var activeRevealObjects = revealObjects
                .InRandomOrder(_randomness)
                .GetRange(0, wordsForRound.Count);

            activeRevealObjects
                .ConvertAll(GameObjectUtils.GetGameObject)
                .ForEach(GameObjectUtils.EnableIfDisabled);

            // TODO: Load in parallel + make sure assets aren't loading 2+ times (see SoftRef.cs)
            foreach (var word in wordsForRound)
            {
                yield return new WaitForTask(() => word.Sprite.Load(_assetDataSource));
            }

            phoneticsTree.SetFromLetters(allLettersToShow);

            activeRevealObjects.ForEachWithIndex(
                (revealObject, index) => revealObject.SetSprite(wordsForRound[index].Sprite.Asset)
            );

            _gameState.ActiveLetters = lettersForRound;
            _gameState.RevealObjects = activeRevealObjects;
            _gameState.CurrentLetter = -1;
        }

        private IEnumerator DoOpeningAnimation()
        {
            var activeCoroutines = new List<Coroutine>();
            var randomizedRevealObjects = _gameState.RevealObjects.InRandomOrder(_randomness);
            foreach (var revealObject in randomizedRevealObjects)
            {
                activeCoroutines.Add(StartCoroutine(revealObject.AnimateSpawn()));
                yield return new WaitForSeconds(0.1f);
            }

            foreach (var activeCoroutine in activeCoroutines)
            {
                yield return activeCoroutine;
            }
        }

        private IEnumerator DoGuessedCorrectly()
        {
            // TODO: Play feedback effects
            _gameState.IsGameActive = false;

            yield return _gameState.RevealObjects[_gameState.CurrentLetter].AnimateDespawn();

            var isLastLetterOfRound = _gameState.CurrentLetter == _gameState.ActiveLetters.Count - 1;
            if (isLastLetterOfRound)
            {
                var isLastRound = _gameState.CurrentRound == _loadedAssets.RoundConfig.Length - 1;
                if (isLastRound)
                {
                    yield return DoGameOver();
                    yield break;
                }

                _gameState.RevealObjects.ForEach(revealObject => revealObject.HideImmediately());
                yield return SetupNextRound();
                yield return DoOpeningAnimation();
            }

            _gameState.IsGameActive = true;
            yield return StartNextGuess();
        }

        private IEnumerator DoGameOver()
        {
            Debug.Log("Game is over");
            yield break;
        }

        private IEnumerator StartNextGuess()
        {
            _gameState.CurrentLetter++;
            var activeRevealObject = _gameState.RevealObjects[_gameState.CurrentLetter];
            phoneticsTree.ResetAllLeaves();
            yield return activeRevealObject.AnimateReveal();
        }
    }
}