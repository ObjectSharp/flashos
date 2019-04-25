using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hoopp.Flash.Domain.Models
{
    public interface ITradeRepository
    {
        Task<bool> InsertAsync(Trade trade);
        Task<IList<Trade>> GetByTicker(string ticker);
    }
}
