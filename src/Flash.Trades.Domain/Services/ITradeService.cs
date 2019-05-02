using System.Collections.Generic;
using System.Threading.Tasks;
using Flash.Trades.Domain.Configuration;
using Flash.Trades.Domain.Models;

namespace Flash.Trades.Domain.Services
{
    public interface ITradeService
    {
        // Configuration
        TradeServiceOptions CurrentConfig { get; }

        // Reads
        Task<Trade> GetByIdAsync(string id);
        Task<IEnumerable<Trade>> GetByTickerAsync(string ticker);

        // Writes
        Task<Trade> InsertAsync(Trade trade);
    }
}
