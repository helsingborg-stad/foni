using Foni.Code.AssetSystem;
using Foni.Code.AsyncSystem;
using Foni.Code.ServicesSystem;
using UnityEngine;

namespace Foni.Code.Core
{
    public class CoreServiceLocator : ServiceLocator
    {
        private TExpectedType GetAs<TExpectedType>(EService serviceType) where TExpectedType : class
        {
            var service = Get(serviceType);
            Debug.AssertFormat(service is TExpectedType, "service {0} is _not_ of type {1}", serviceType,
                typeof(TExpectedType));
            return service as TExpectedType;
        }

        private void RegisterAs<T>(EService serviceType, T service)
        {
            Debug.AssertFormat(service is IService, "service {0} {1} does _not_ implement IService", serviceType,
                service.GetType());
            Register(serviceType, (IService)service);
        }

        public IAssetService AssetService
        {
            get => GetAs<IAssetService>(EService.AssetService);
            set => RegisterAs(EService.AssetService, value);
        }

        public IAsyncService AsyncService
        {
            get => GetAs<IAsyncService>(EService.AsyncManager);
            set => RegisterAs(EService.AsyncManager, value);
        }
    }
}