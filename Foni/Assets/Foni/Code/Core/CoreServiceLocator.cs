using Foni.Code.AssetSystem;
using Foni.Code.AsyncSystem;
using Foni.Code.DateTimeSystem;
using Foni.Code.ProfileSystem;
using Foni.Code.SaveSystem;
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

        public IAsyncService AsyncService
        {
            get => GetAs<IAsyncService>(EService.AsyncManager);
            set => RegisterAs(EService.AsyncManager, value);
        }

        public IAssetCache AssetCache
        {
            get => GetAs<IAssetCache>(EService.AssetCache);
            set => RegisterAs(EService.AssetCache, value);
        }

        public IDateTimeService DateTimeService
        {
            get => GetAs<IDateTimeService>(EService.DateTimeService);
            set => RegisterAs(EService.DateTimeService, value);
        }

        public ISaveService SaveService
        {
            get => GetAs<ISaveService>(EService.SaveService);
            set => RegisterAs(EService.SaveService, value);
        }

        public IProfileService ProfileService
        {
            get => GetAs<IProfileService>(EService.ProfileService);
            set => RegisterAs(EService.ProfileService, value);
        }
    }
}