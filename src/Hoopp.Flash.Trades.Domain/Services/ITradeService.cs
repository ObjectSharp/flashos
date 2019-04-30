using System.Collections.Generic;
using System.Threading.Tasks;
using Hoopp.Flash.Trades.Domain.Configuration;
using Hoopp.Flash.Trades.Domain.Models;

namespace Hoopp.Flash.Trades.Domain.Services
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
