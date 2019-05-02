using System.Collections.Generic;
using System.Threading.Tasks;
using Flash.Core.Services;
using Flash.Trades.Domain.Configuration;
using Flash.Trades.Domain.Models;
using Microsoft.Extensions.Options;

namespace Flash.Trades.Domain.Services
{
    public class TradeService : ITradeService
    {
        // Injected fields should be readonly
        private readonly IOptions<TradeServiceOptions> _options;
        private readonly ITradeRepository _repo;
        private readonly IGuidGeneratorService _uids;
        private readonly ILocalTimeService _timestamps;
        
        public TradeService(
            IOptions<TradeServiceOptions> options, 
            ITradeRepository repo, 
            IGuidGeneratorService uids, 
            ILocalTimeService timestamps)
        {
            _options = options;
            _repo = repo;
            _uids = uids;
            _timestamps = timestamps;
        }

        public TradeServiceOptions CurrentConfig => _options?.Value;

        public async Task<Trade> GetByIdAsync(string id)
        {
            var result = await _repo.GetByIdAsync(id);
            return result;
        }

        public async Task<IEnumerable<Trade>> GetByTickerAsync(string ticker)
        {
            var result = await _repo.GetByTickerAsync(ticker);
            return result;
        }

        public async Task<Trade> InsertAsync(Trade trade)
        {
            // Enrich payload
            trade.Id = _uids.GenerateString();
            trade.CreatedAt = _timestamps.Now();

            // Store
            var result = await _repo.InsertAsync(trade);
            if (result)
                return trade;
            
            return null;
        }
    }
}
