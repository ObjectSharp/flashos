using System;

namespace Hoopp.Flash.Core.Services
{
    public interface IGuidGeneratorService
    {
        Guid GenerateGuid();
        string GenerateString();
    }
}
