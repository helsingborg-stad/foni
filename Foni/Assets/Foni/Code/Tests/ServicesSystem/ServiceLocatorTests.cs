using System;
using Foni.Code.ServicesSystem;
using NUnit.Framework;

namespace Foni.Code.Tests.ServicesSystem
{
    public class ServiceLocatorTests
    {
        private class MockService : IService
        {
        }

        [Test]
        public void RegisterAndGet()
        {
            var serviceLocator = new ServiceLocator();
            var mockService = new MockService();

            serviceLocator.Register(EService.AssetService, mockService);
            var result = serviceLocator.Get(EService.AssetService);

            Assert.AreSame(mockService, result);
        }

        [Test]
        public void RegisterSecondTimeOverrides()
        {
            var serviceLocator = new ServiceLocator();
            var mockService1 = new MockService();
            var mockService2 = new MockService();

            serviceLocator.Register(EService.AssetService, mockService1);
            serviceLocator.Register(EService.AssetService, mockService2);
            var result = serviceLocator.Get(EService.AssetService);

            Assert.AreNotSame(mockService1, result);
            Assert.AreSame(mockService2, result);
        }

        [Test]
        public void ThrowsWhenNotRegistered()
        {
            var serviceLocator = new ServiceLocator();

            Assert.Throws(Is.InstanceOf<Exception>(), () => serviceLocator.Get(EService.AssetService));
        }
    }
}