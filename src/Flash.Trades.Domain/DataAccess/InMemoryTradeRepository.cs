using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Flash.Trades.Domain.Configuration;
using Flash.Trades.Domain.Models;
using Microsoft.Extensions.Options;

namespace Flash.Trades.Domain.DataAccess
{
    // NOTE: Typically we would exclude repositories from code coverage since
    // they tend to require access to external dependencies (sql). There are 
    // ways to use LocalDB, but these start getting slow.
    [ExcludeFromCodeCoverage]
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

        public async Task<int> InsertBatchAsync(IEnumerable<Trade> trades)
        {
            return await Task.Run(() =>
            {
                var i = 0;
                foreach (var trade in trades)
                {
                    _data.Add(trade.Id, trade);
                    i++;
                }
                return i;
            });
        }

        public async Task<Trade> GetByIdAsync(string tradeId)
        {
            return await Task.Run(() =>
            {
                if (_data.TryGetValue(tradeId, out var trade))
                    return trade;

                return null;
            });
        }
    }
}
