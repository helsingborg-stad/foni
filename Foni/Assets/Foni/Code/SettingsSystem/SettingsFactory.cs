using Foni.Code.SettingsSystem.Implementation;

namespace Foni.Code.SettingsSystem
{
    public static class SettingsFactory
    {
        private static ISettingsProvider GetPlatformProvider()
        {
#if UNITY_EDITOR
            return new EditorSettingsProvider();
#elif UNITY_IOS
            return new IOSSettingsProvider();
#else
            throw new Exception("No SettingsProvider for current platform");
#endif
        }
        
        public static Settings GetSettings()
        {
            return GetPlatformProvider().GetSettings();
        }
    }
}