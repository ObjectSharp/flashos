using System;

namespace Hoopp.Flash.Core.Services
{
    public class LocalTimeService : ILocalTimeService
    {
        public DateTimeOffset Now()
        {
            return DateTimeOffset.Now;
        }
    }
}
