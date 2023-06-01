using System.Collections.Generic;

namespace Foni.Code.ServicesSystem
{
    public class ServiceLocator
    {
        protected readonly Dictionary<EService, IService> Services;

        public ServiceLocator()
        {
            Services = new Dictionary<EService, IService>();
        }

        public void Register(EService serviceType, IService service)
        {
            Services[serviceType] = service;
        }

        public IService Get(EService serviceType)
        {
            return Services[serviceType];
        }
    }
}