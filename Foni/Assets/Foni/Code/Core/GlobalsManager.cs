using Foni.Code.AssetSystem;
using Foni.Code.AsyncSystem;
using Foni.Code.SettingsSystem;
using UnityEngine;

namespace Foni.Code.Core
{
    public class GlobalsManager : MonoBehaviour
    {
        private void Start()
        {
            /*DontDestroyOnLoad(this);*/
            Globals.GlobalsManager = this;
        }

        public void SetupGlobals()
        {
            Debug.Log("Setting up globals");
            var serviceLocator = new CoreServiceLocator
            {
                AsyncService = gameObject.AddComponent<AsyncService>(),
                AssetCache = new AssetCache()
            };
            Globals.ServiceLocator = serviceLocator;
            Globals.SettingsManager = gameObject.AddComponent<SettingsManager>();

            SettingsManager.ApplySettings();
        }
    }
}