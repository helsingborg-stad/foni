using Foni.Code.ServicesSystem;

namespace Foni.Code.SystemPropertiesSystem
{
    public interface ISystemPropertiesService : IService
    {
        public string Version { get; }
        public string Build { get; }
        public string Revision { get; }
    }
}