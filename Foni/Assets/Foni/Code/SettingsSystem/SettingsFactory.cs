using Foni.Code.SettingsSystem.Implementation;

namespace Foni.Code.SettingsSystem
{
    public class SettingsFactory
    {
        public Settings GetSettings()
        {
#if UNITY_EDITOR
            return new EditorSettingsProvider().GetSettings();
#elif UNITY_IOS
            return new IOSSettingsProvider().GetSettings();
#else
            throw new Exception("No SettingsProvider for current platform");
#endif
        }
    }
}