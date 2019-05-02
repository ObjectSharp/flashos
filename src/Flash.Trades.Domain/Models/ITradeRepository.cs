﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flash.Trades.Domain.Models
{
    public interface ITradeRepository
    {
        Task<bool> InsertAsync(Trade trade);

        Task<bool> InsertBatchAsync(IEnumerable<Trade> trade);

        Task<Trade> GetByIdAsync(string tradeId);

        Task<IEnumerable<Trade>> GetByTickerAsync(string ticker);
    }
}