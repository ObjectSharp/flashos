using System;
using System.Collections.Generic;
using System.Linq;

namespace Flash.Trades.Domain.Models
{
    public class Trade
    {
        public string Id { get; set; }
        public string Ticker { get; set; }
        public string Notes { get; set; }
        public decimal Amount => Transactions.Sum(x => x.Amount);
        public IList<Transaction> Transactions { get; set; } = new List<Transaction>();
        public DateTimeOffset CreatedAt { get; set; }
    }
}
