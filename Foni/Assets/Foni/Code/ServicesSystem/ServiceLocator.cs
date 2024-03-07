using System.Collections.Generic;

namespace Foni.Code.ServicesSystem
{
    public class ServiceLocator
    {
        private readonly Dictionary<EService, IService> _services = new();

        public void Register(EService serviceType, IService service)
        {
            _services[serviceType] = service;
        }

        public IService Get(EService serviceType)
        {
            return _services[serviceType];
        }
    }
}