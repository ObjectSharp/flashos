using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flash.Trades.Domain.Models
{
    public interface ITradeRepository
    {
        Task<bool> InsertAsync(Trade trade);

        Task<int> InsertBatchAsync(IEnumerable<Trade> trades);

        Task<Trade> GetByIdAsync(string tradeId);

        Task<IEnumerable<Trade>> GetByTickerAsync(string ticker);
    }
}
