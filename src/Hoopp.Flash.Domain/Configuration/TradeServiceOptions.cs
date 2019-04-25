namespace Hoopp.Flash.Domain.Configuration
{
    public class TradeServiceOptions
    {
        public int MaxTransactionLimit { get; set; }
        public bool CanOverride { get; set; }
        public string DefaultTicker { get; set; }
    }
}