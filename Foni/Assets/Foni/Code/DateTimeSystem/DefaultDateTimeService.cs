using System;

namespace Foni.Code.DateTimeSystem
{
    public class DefaultDateTimeService : IDateTimeService
    {
        public DateTime Now => DateTime.Now;
    }
}