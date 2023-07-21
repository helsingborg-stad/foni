using System;
using Foni.Code.Core;
using Foni.Code.DateTimeSystem;
using Foni.Code.ServicesSystem;

namespace Foni.Code.Tests.Mocks
{
    internal class MockDateTimeService : IDateTimeService
    {
        public DateTime Now { get; set; }

        public static MockDateTimeService Get()
        {
            var dateTimeService = new MockDateTimeService();
            Globals.ServiceLocator = new CoreServiceLocator();
            Globals.ServiceLocator.Register(EService.DateTimeService, dateTimeService);
            dateTimeService.Now = new DateTime(2023, 06, 30, 15, 30, 45, DateTimeKind.Utc);
            return dateTimeService;
        }
    }
}