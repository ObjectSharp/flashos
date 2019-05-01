using System;

namespace Hoopp.Flash.Core.Services
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
