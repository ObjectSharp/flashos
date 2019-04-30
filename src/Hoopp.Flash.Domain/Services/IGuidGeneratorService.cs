using System;

namespace Hoopp.Flash.Domain.Services
{
    public interface IGuidGeneratorService
    {
        Guid GenerateGuid();
        string GenerateString();
    }
}
