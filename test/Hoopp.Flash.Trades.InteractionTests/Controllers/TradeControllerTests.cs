using System.Net;
using System.Threading.Tasks;
using Hoopp.Flash.Core.Test;
using Hoopp.Flash.Trades.Domain.Configuration;
using Hoopp.Flash.Trades.Web;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Options;
using Xunit;

namespace Hoopp.Flash.Trades.InteractionTests.Controllers
{
    public class TradeControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public TradeControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Options_trades_returns_config_200()
        {
            using (var client = _factory.CreateClient())
            {
                var response = await client.OptionsAsync("api/trades");
                Assert.True(response.IsSuccessStatusCode);

                var result = await response.ReadAsAsync<TradeServiceOptions>();
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async Task Options_trades_with_custom_config_returns_200()
        {
            // NOTE: Shows how to replace configuration options, but any registered
            // service can be replaced using this method
            using (var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.ReplaceAsSingleton<IOptions<TradeServiceOptions>>(
                        x => Options.Create(new TradeServiceOptions
                        {
                            ConfigEndpointEnabled = true,
                            DefaultTicker = "NEW-TICKER",
                            MaxTransactionLimit = 100,
                            Rules = new TradeServiceRulesOptions
                            {
                                CanOverride = false,
                                ValidationEnabled = false
                            }
                        }));
                });
            }).CreateClient())
            {
                var response = await client.OptionsAsync("api/trades");
                Assert.True(response.IsSuccessStatusCode);

                var result = await response.ReadAsAsync<TradeServiceOptions>();
                Assert.NotNull(result);
                Assert.Equal("NEW-TICKER", result.DefaultTicker);
                Assert.Equal(100, result.MaxTransactionLimit);
            }
        }

        [Fact]
        public async Task Options_trades_with_disabled_config_returns_404()
        {
            // NOTE: Shows how to replace configuration options, but any registered
            // service can be replaced using this method
            using (var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.ReplaceAsSingleton<IOptions<TradeServiceOptions>>(
                        x => Options.Create(new TradeServiceOptions
                        {
                            ConfigEndpointEnabled = false
                        }));
                });
            }).CreateClient())
            {
                var response = await client.OptionsAsync("api/trades");
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }
    }
}
