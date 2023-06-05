using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Foni.Code.AsyncSystem;
using Foni.Code.DataSourceSystem.Implementation.StreamingAssetDataSource;
using Foni.Code.PhoneticsSystem;
using Foni.Code.Util;
using UnityEngine;
using UnityEngine.Serialization;

namespace Foni.Code.Core
{
    public struct LoadedAssets
    {
        public List<Letter> Letters;
        public List<Word> Words;
    }

    public struct GameState
    {
        public int CurrentLetter;
        public int CurrentRound;
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

        private IEnumerator Start()
        {
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
            var streamingAssetDataSource = new StreamingAssetsDataSource();

            var words = await WordSerialization.LoadFromDataSource(streamingAssetDataSource);
            _loadedAssets = new LoadedAssets
            {
                Letters = await LetterSerialization.LoadFromDataSource(streamingAssetDataSource, words),
                Words = words
            };

            Debug.LogFormat("Loaded words: {0}", _loadedAssets.Words.Count);
            Debug.LogFormat("Loaded letters: {0}", _loadedAssets.Letters.Count);
            Debug.Log("All assets loaded!");

            Globals.ServiceLocator.AsyncService.QueueOnMainThread(StartGame);
        }

        private void StartGame()
        {
            Debug.Log("Starting game");

            _gameState = new GameState
            {
                CurrentLetter = 0,
                CurrentRound = 0
            };

            phoneticsTree.SetFromLetters(new List<Letter>
            {
                _loadedAssets.Letters[0],
                _loadedAssets.Letters[1],
                _loadedAssets.Letters[2],
                _loadedAssets.Letters[3],
                _loadedAssets.Letters[4],
                _loadedAssets.Letters[5]
            });

            // TODO: load random word per letter, and "fully" load those words
            // TODO: and implement cache for fully loaded assets so they can be cleaned up maybe?

            StartCoroutine(DoOpeningAnimation());
        }

        private IEnumerator DoOpeningAnimation()
        {
            var randomizedRevealObjects = ListUtils.GetRandomOrder(revealObjects);
            foreach (var revealObject in randomizedRevealObjects)
            {
                StartCoroutine(revealObject.AnimateSpawn());
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}