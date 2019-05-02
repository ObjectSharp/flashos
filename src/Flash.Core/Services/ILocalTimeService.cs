using System;

namespace Flash.Core.Services
{
    public interface ILocalTimeService
    {
        DateTimeOffset Now();
    }
}
