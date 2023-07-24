using System.Threading.Tasks;
using Foni.Code.SystemPropertiesSystem.Implementation;

namespace Foni.Code.SystemPropertiesSystem
{
    public static class SystemPropertiesServiceFactory
    {
        public static async Task<ISystemPropertiesService> Create()
        {
#if !UNITY_EDITOR && UNITY_IOS
            var service = new IOSSystemPropertiesService();
            await service.Init();
            return service;
#else
            var service = new DesktopSystemPropertiesService();
            await service.Init();
            return service;
#endif
        }
    }
}