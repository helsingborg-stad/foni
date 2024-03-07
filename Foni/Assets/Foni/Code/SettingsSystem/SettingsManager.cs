using Foni.Code.Util;
using UnityEngine;

namespace Foni.Code.SettingsSystem
{
    public static class SettingsManager
    {
        public static void ApplySettings()
        {
            var settings = SettingsFactory.GetSettings();

            Debug.LogFormat("Applying settings {0}", StructUtils.PrintStruct(settings));

            Application.targetFrameRate = settings.TargetFrameRate;
        }
    }
}