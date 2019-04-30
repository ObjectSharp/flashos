using System.Collections.Generic;
using System.Threading.Tasks;
using Hoopp.Flash.Domain.Configuration;
using Hoopp.Flash.Domain.Models;

namespace Hoopp.Flash.Domain.Services
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
