using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace Foni.Code.SystemPropertiesSystem.Implementation
{
    public class IOSSystemPropertiesService : ISystemPropertiesService
    {
        public string Version { get; private set; }
        public string Build { get; private set; }
        public string Revision { get; private set; }

        public async Task Init()
        {
            Debug.Log("IOSSystemPropertiesService.Init");
            var plistPath = Path.GetFullPath(Path.Join(Application.dataPath, "../", "Info.plist"));

            var dict = (Dictionary<string, object>)PlistCS.PlistCS.readPlist(plistPath);

            Version = dict["CFBundleShortVersionString"] as string;
            Build = dict["CFBundleVersion"] as string;
            Revision = await GitProperties.GetCommitHash();
        }
    }
}