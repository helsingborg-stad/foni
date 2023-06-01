using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Foni.Code.AsyncSystem;
using Foni.Code.PhoneticsSystem;
using UnityEngine;

namespace Foni.Code.Core
{
    public struct GameplayData
    {
        public List<Letter> Letters;
    }

    public class CoreGameMode : MonoBehaviour
    {
        private GameplayData _gameplayData;

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
            var assetLoader = Globals.ServiceLocator.AssetService;

            _gameplayData = new GameplayData
            {
                Letters = await assetLoader.LoadLetters()
            };

            Debug.Log("All assets loaded!");
        }
    }
}