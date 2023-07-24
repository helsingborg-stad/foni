using System.Collections;
using System.Threading.Tasks;
using Foni.Code.Core;
using UnityEngine;

namespace Foni.Code.SystemPropertiesSystem.Implementation
{
    public class DesktopSystemPropertiesService : ISystemPropertiesService
    {
        public string Version { get; private set; }
        public string Build { get; private set; }
        public string Revision { get; private set; }

        private IEnumerator SetVersionFromApplication()
        {
            Version = Application.version;
            yield break;
        }
        
        public async Task Init()
        {
            Debug.Log("DesktopSystemPropertiesService.Init");

            await Globals.ServiceLocator.AsyncService.RunOnMainThread(SetVersionFromApplication);
            
            Build = "0";
            Revision = await GitProperties.GetCommitHash();
        }
    }
}