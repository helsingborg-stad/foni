using Foni.Code.AssetSystem;
using Foni.Code.AsyncSystem;
using Foni.Code.DateTimeSystem;
using Foni.Code.ProfileSystem;
using Foni.Code.SaveSystem.Implementation;
using Foni.Code.SettingsSystem;
using UnityEngine;

namespace Foni.Code.Core
{
    public class GlobalsManager : MonoBehaviour
    {
        private bool _hasSetupGlobals;

        private void Start()
        {
            DontDestroyOnLoad(this);
            Globals.GlobalsManager = this;
        }

        public void SetupGlobals()
        {
            if (_hasSetupGlobals) return;

            Debug.Log("Setting up globals");
            var saveService = new LocalSaveService();
            var serviceLocator = new CoreServiceLocator
            {
                AsyncService = gameObject.AddComponent<AsyncService>(),
                AssetCache = new AssetCache(),
                DateTimeService = new DefaultDateTimeService(),
                SaveService = saveService,
                ProfileService = new ProfileService(saveService)
            };
            Globals.ServiceLocator = serviceLocator;
            Globals.SettingsManager = gameObject.AddComponent<SettingsManager>();
            Globals.AvatarIconMapper = gameObject.GetComponent<AvatarIconMapper>();

            SettingsManager.ApplySettings();

            _hasSetupGlobals = true;
        }
    }
}