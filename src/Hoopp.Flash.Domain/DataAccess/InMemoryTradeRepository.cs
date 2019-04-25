using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hoopp.Flash.Domain.Configuration;
using Hoopp.Flash.Domain.Models;
using Microsoft.Extensions.Options;

namespace Hoopp.Flash.Domain.DataAccess
{
    public class InMemoryTradeRepository : ITradeRepository
    {
        // Inject fields should be readonly
        private readonly IOptions<ConnectionStringsOptions> _options;

        // Properly keyed dictionaries are much more effecient than generic lists
        private readonly IDictionary<string, Trade> _data;
        
        public InMemoryTradeRepository(IOptions<ConnectionStringsOptions> options, IEnumerable<Trade> seed = null)
        {
            _options = options;

            // Provide simple data seeder for testing
            if (seed != null)
                _data = seed.ToDictionary(x => x.Ticker);
        }

        Task<bool> ITradeRepository.InsertAsync(Trade trade)
        {
            throw new System.NotImplementedException();
        }

        Task<IList<Trade>> ITradeRepository.GetByTicker(string ticker)
        {
            throw new System.NotImplementedException();
        }
    }
}
