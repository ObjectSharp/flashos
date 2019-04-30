using System;

namespace Hoopp.Flash.Domain.Services
{
    public interface ILocalTimeService
    {
        DateTimeOffset Now();
    }
}
