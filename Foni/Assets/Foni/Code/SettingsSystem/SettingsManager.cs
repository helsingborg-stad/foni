using Foni.Code.Util;
using UnityEngine;

namespace Foni.Code.SettingsSystem
{
    public class SettingsManager : MonoBehaviour
    {
        public static void ApplySettings()
        {
            var settings = new SettingsFactory().GetSettings();

            Debug.LogFormat("Applying settings {0}", StructUtils.PrintStruct(settings));

            Application.targetFrameRate = settings.TargetFrameRate;
        }
    }
}