namespace Foni.Code.SettingsSystem.Implementation
{
    public class EditorSettingsProvider : ISettingsProvider
    {
        public Settings GetSettings()
        {
            return new Settings
            {
                TargetFrameRate = -1
            };
        }
    }
}