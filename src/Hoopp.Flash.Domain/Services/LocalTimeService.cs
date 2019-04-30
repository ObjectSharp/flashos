using System;

namespace Hoopp.Flash.Domain.Services
{
    public class LocalTimeService : ILocalTimeService
    {
        public DateTimeOffset Now()
        {
            return DateTimeOffset.Now;
        }
    }
}
