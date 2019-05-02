using System;

namespace Flash.Core.Services
{
    public interface IGuidGeneratorService
    {
        Guid GenerateGuid();
        string GenerateString();
    }
}
