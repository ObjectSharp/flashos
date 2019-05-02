using System;

namespace Flash.Core.Services
{
    public class LocalTimeService : ILocalTimeService
    {
        public DateTimeOffset Now()
        {
            return DateTimeOffset.Now;
        }
    }
}
