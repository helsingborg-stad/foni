namespace Foni.Code.SettingsSystem.Implementation
{
    public class IOSSettingsProvider : ISettingsProvider
    {
        public Settings GetSettings()
        {
            return new Settings
            {
                TargetFrameRate = 60
            };
        }
    }
}