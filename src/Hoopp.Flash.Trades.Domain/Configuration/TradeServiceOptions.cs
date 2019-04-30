namespace Hoopp.Flash.Trades.Domain.Configuration
{
    // Set default configuration values in class
    // Override order: defaults -> appsettings -> environment variables

    public class TradeServiceOptions
    {
        public int MaxTransactionLimit { get; set; } = 10;
        public string DefaultTicker { get; set; } = "AAPL";
        public bool ConfigEndpointEnabled { get; set; } = false;
        public TradeServiceRulesOptions Rules { get; set; } = new TradeServiceRulesOptions();
    }

    public class TradeServiceRulesOptions
    {
        public bool ValidationEnabled { get; set; } = true;
        public bool CanOverride { get; set; } = false;
    }
}