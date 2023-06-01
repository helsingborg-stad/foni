using Foni.Code.AssetSystem.Implementation;
using Foni.Code.AsyncSystem;
using UnityEngine;

namespace Foni.Code.Core
{
    public class GlobalsManager : MonoBehaviour
    {
        private void Start()
        {
            DontDestroyOnLoad(this);
            Globals.GlobalsManager = this;
        }

        public void SetupGlobals()
        {
            Debug.Log("Setting up globals");
            var serviceLocator = new CoreServiceLocator
            {
                AssetService = new DeviceLocalAssetService(),
                AsyncService = gameObject.AddComponent<AsyncService>()
            };
            Globals.ServiceLocator = serviceLocator;
        }
    }
}