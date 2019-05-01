using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hoopp.Flash.Trades.Domain.Configuration;
using Hoopp.Flash.Trades.Domain.Models;
using Microsoft.Extensions.Options;

namespace Hoopp.Flash.Trades.Domain.DataAccess
{
    public class InMemoryTradeRepository : ITradeRepository
    {
        // Injected fields should be readonly
        private readonly IOptions<ConnectionStringsOptions> _options;

        // Properly keyed dictionaries are much more effecient than generic lists
        private readonly IDictionary<string, Trade> _data;
        
        public InMemoryTradeRepository(IOptions<ConnectionStringsOptions> options, IEnumerable<Trade> seed = null)
        {
            _options = options;

            // Provide simple data seeder for testing
            if (seed != null)
                _data = seed.ToDictionary(x => x.Id);
        }

        public async Task<bool> InsertAsync(Trade trade)
        {
            return await Task.Run(() =>
            {
                _data.Add(trade.Id, trade);
                return true;
            });
        }

        public async Task<IEnumerable<Trade>> GetByTickerAsync(string ticker)
        {
            return await Task.Run(() =>
                _data.Values.Where(x => x.Ticker.Equals(ticker, StringComparison.OrdinalIgnoreCase)));
        }

        public Task<bool> InsertBatchAsync(IEnumerable<Trade> trade)
        {
            throw new System.NotImplementedException();
        }

        public Task<Trade> GetByIdAsync(string tradeId)
        {
            throw new System.NotImplementedException();
        }
    }
}
