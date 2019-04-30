using System;

namespace Hoopp.Flash.Domain.Services
{
    public class GuidGeneratorService : IGuidGeneratorService
    {
        public Guid GenerateGuid()
        {
            return Guid.NewGuid();
        }

        public string GenerateString()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
