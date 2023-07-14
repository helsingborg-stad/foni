using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foni.Code.AsyncSystem;
using Foni.Code.DataSourceSystem;
using Foni.Code.DataSourceSystem.Implementation.StreamingAssetDataSource;
using Foni.Code.PhoneticsSystem;
using Foni.Code.ProfileSystem;
using Foni.Code.UI;
using Foni.Code.Util;
using UnityEngine;
using Random = System.Random;

namespace Foni.Code.Core
{
    public struct LoadedAssets
    {
        public List<Letter> Letters;
        public List<Word> Words;
        public RoundConfig RoundConfig;
    }

    public struct RoundHistoryEntry
    {
        public List<Letter> Letters;
        public List<Word> Words;
    }

    public struct GameState
    {
        public bool IsGameActive;
        public int CurrentRound;
        public int CurrentLetter;
        public List<Letter> ActiveLetters;
        public List<RevealObjectComponent> RevealObjects;
        public List<RoundHistoryEntry> History;
    }

    public class CoreGameMode : MonoBehaviour
    {
        [Header("References")] //
        [SerializeField]
        private PhoneticsTree phoneticsTree;

        [SerializeField] private List<RevealObjectComponent> revealObjects;

        [SerializeField] private HandGesture handGesture;

        [SerializeField] private GameResultsUI gameResultsUI;

        [Header("Config")] //
#if UNITY_EDITOR
        [Tooltip("Not used in builds. Negative values are ignored and will use default/no seed.")]
        [SerializeField]
        private int editorRandomSeed = -1;
#endif

        private LoadedAssets _loadedAssets;
        private GameState _gameState;
        private IDataSource _assetDataSource;
        private Random _randomness;
        private SessionDataBuilder _sessionDataBuilder;

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
            var letters = await LetterSerialization.LoadFromDataSource(_assetDataSource, words);
            var roundConfig = await RoundConfigSerialization.LoadFromDataSource(_assetDataSource, letters);
            _loadedAssets = new LoadedAssets
            {
                Letters = letters,
                Words = words,
                RoundConfig = roundConfig
            };

            Debug.LogFormat("Loaded words: {0}", _loadedAssets.Words.Count);
            Debug.LogFormat("Loaded letters: {0}", _loadedAssets.Letters.Count);
            Debug.LogFormat("Loaded rounds: {0}", _loadedAssets.RoundConfig.Rounds.Count);

            Globals.ServiceLocator.AsyncService.QueueOnMainThread(StartGame_Entry);
        }

        private void StartGame_Entry()
        {
            StartCoroutine(StartGame());
        }

        private IEnumerator StartGame()
        {
            Debug.Log("Starting game");

#if UNITY_EDITOR
            _randomness = editorRandomSeed >= 0 ? new Random(editorRandomSeed) : new Random();
#else
            _randomness = new Random();
#endif

            _gameState = new GameState
            {
                IsGameActive = false,
                CurrentRound = -1,
                CurrentLetter = -1,
                ActiveLetters = new List<Letter>(),
                RevealObjects = new List<RevealObjectComponent>(),
                History = Enumerable.Repeat(new RoundHistoryEntry(), _loadedAssets.RoundConfig.Rounds.Count).ToList()
            };

            phoneticsTree.OnLeafClicked += OnPhoneticsTreeLeafClicked;

            _sessionDataBuilder = new SessionDataBuilder()
                .Initialize();

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

            if (guessedCorrectly)
            {
                letterComponent.SetState(ELetterState.Correct);
                handGesture.Hide();

                StartCoroutine(DoGuessedCorrectly());
            }
            else
            {
                letterComponent.SetState(ELetterState.Incorrect);
                handGesture.Show();
                _sessionDataBuilder.IncrementWrongGuesses();
            }
        }

        private IEnumerator SetupNextRound()
        {
            ++_gameState.CurrentRound;

            var lettersForRound = _loadedAssets.RoundConfig.Rounds[_gameState.CurrentRound].Letters;

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
            foreach (var letter in lettersForRound)
            {
                yield return new WaitForTask(() => letter.HandGestureSprite.Load(_assetDataSource));
            }

            foreach (var word in wordsForRound)
            {
                yield return new WaitForTask(() => word.Sprite.Load(_assetDataSource));
                yield return new WaitForTask(() => word.SoundClip.Load(_assetDataSource));
            }

            phoneticsTree.SetFromLetters(allLettersToShow);

            activeRevealObjects.ForEachWithIndex(
                (revealObject, index) =>
                {
                    var word = wordsForRound[index];
                    revealObject.SetSprite(word.Sprite.Asset);
                    revealObject.SetSound(word.SoundClip.Asset);
                    revealObject.OnSoundButtonClickedEvent = () => _sessionDataBuilder.IncrementTimesSoundPlayed();
                }
            );

            _gameState.ActiveLetters = lettersForRound;
            _gameState.RevealObjects = activeRevealObjects;
            _gameState.CurrentLetter = -1;
            _gameState.History[_gameState.CurrentRound] = new RoundHistoryEntry
            {
                Letters = lettersForRound,
                Words = wordsForRound
            };
        }

        private IEnumerator DoOpeningAnimation()
        {
            var activeCoroutines = new List<Coroutine>();
            var randomizedRevealObjects = _gameState.RevealObjects.InRandomOrder(_randomness);
            foreach (var revealObject in randomizedRevealObjects)
            {
                activeCoroutines.Add(StartCoroutine(revealObject.AnimateShow()));
                yield return new WaitForSeconds(0.1f);
            }

            foreach (var activeCoroutine in activeCoroutines)
            {
                yield return activeCoroutine;
            }

            yield return phoneticsTree.AnimateShowingLeaves();
        }

        private IEnumerator DoGuessedCorrectly()
        {
            // TODO: Play feedback effects
            _gameState.IsGameActive = false;
            _sessionDataBuilder.EndGuess();

            yield return _gameState.RevealObjects[_gameState.CurrentLetter].AnimateHide();

            var isLastLetterOfRound = _gameState.CurrentLetter == _gameState.ActiveLetters.Count - 1;
            if (isLastLetterOfRound)
            {
                var isLastRound = _gameState.CurrentRound == _loadedAssets.RoundConfig.Rounds.Count - 1;
                if (isLastRound)
                {
                    yield return DoGameOver();
                    yield break;
                }

                _gameState.RevealObjects.ForEach(revealObject => revealObject.ShroudImmediately());
                yield return phoneticsTree.AnimateHidingLeaves();
                phoneticsTree.ResetAllLeaves();
                yield return SetupNextRound();
                yield return DoOpeningAnimation();
            }

            _gameState.IsGameActive = true;
            yield return StartNextGuess();
        }

        private IEnumerator DoGameOver()
        {
            Debug.Log("Game is over");
            var sessionData = _sessionDataBuilder.EndSession();
            var activeProfile = Globals.ServiceLocator.ProfileService.GetActiveProfile();
            if (activeProfile.HasValue)
            {
                activeProfile.Value.statistics.sessions.Add(sessionData);
                Globals.ServiceLocator.ProfileService.UpdateProfile(activeProfile.Value);
            }

            yield return phoneticsTree.AnimateHidingLeaves();
            gameResultsUI.gameObject.EnableIfDisabled();

            var resultCardInfos = _gameState.History
                .ConvertAll(RoundHistoryEntryToResultCardInfo)
                .SelectMany(list => list)
                .ToList();

            gameResultsUI.SetCards(resultCardInfos);

            yield return gameResultsUI.AnimateShow();
        }

        private static List<ResultCardInfo> RoundHistoryEntryToResultCardInfo(RoundHistoryEntry historyEntry)
        {
            return historyEntry.Letters.Select((letter, index) => new ResultCardInfo
            {
                Title = letter.ID,
                Sprite = historyEntry.Words[index].Sprite.Asset
            }).ToList();
        }

        private IEnumerator StartNextGuess()
        {
            _gameState.CurrentLetter++;
            var letter = _gameState.ActiveLetters[_gameState.CurrentLetter];
            var activeRevealObject = _gameState.RevealObjects[_gameState.CurrentLetter];
            phoneticsTree.ResetAllLeaves();
            handGesture.SetSprite(letter.HandGestureSprite.Asset);
            _sessionDataBuilder.StartGuess(letter.ID);
            yield return activeRevealObject.AnimateReveal();
        }
    }
}