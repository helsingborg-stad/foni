using Foni.Code.AudioSystem;
using Foni.Code.ProfileSystem;
using UnityEngine;

namespace Foni.Code.Core
{
    public static class Globals
    {
        private static GlobalsManager _globalsManager;

        public static GlobalsManager GlobalsManager
        {
            get
            {
                EnsureGlobalsObject();
                return _globalsManager;
            }
            set => _globalsManager = value;
        }

        private static CoreServiceLocator _serviceLocator;

        public static CoreServiceLocator ServiceLocator
        {
            get
            {
                EnsureGlobalsObject();
                return _serviceLocator;
            }
            set => _serviceLocator = value;
        }

        private static AvatarIconMapper _avatarIconMapper;

        public static AvatarIconMapper AvatarIconMapper
        {
            get
            {
                EnsureGlobalsObject();
                return _avatarIconMapper;
            }
            set => _avatarIconMapper = value;
        }

        private static AudioManager _audioManager;

        public static AudioManager AudioManager
        {
            get
            {
                EnsureGlobalsObject();
                return _audioManager;
            }
            set => _audioManager = value;
        }

        public static void EnsureGlobalsObject()
        {
            if (_globalsManager != null) return;

            _globalsManager = Object.FindObjectOfType<GlobalsManager>();
            if (_globalsManager != null) return;
            Debug.Log("No GlobalsManager found; creating...");
            var globalsPrefab = Resources.Load<GlobalsManager>("P_Globals");
            _globalsManager = Object.Instantiate(globalsPrefab);
            _globalsManager.SetupGlobals();
        }
    }
}