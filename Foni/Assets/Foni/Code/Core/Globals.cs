using Foni.Code.SettingsSystem;

namespace Foni.Code.Core
{
    public static class Globals
    {
        public static GlobalsManager GlobalsManager { get; set; }
        public static CoreServiceLocator ServiceLocator { get; set; }
        public static SettingsManager SettingsManager { get; set; }
    }
}