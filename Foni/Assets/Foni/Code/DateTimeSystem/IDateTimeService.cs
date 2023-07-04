using System;
using Foni.Code.ServicesSystem;

namespace Foni.Code.DateTimeSystem
{
    public interface IDateTimeService : IService
    {
        DateTime Now { get; }
    }
}