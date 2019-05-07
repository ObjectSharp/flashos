using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FakeItEasy;
using Flash.Core.Test;
using Flash.Trades.Domain.Configuration;
using Flash.Trades.Domain.Models;
using Flash.Trades.Web;
using Flash.Trades.Web.ViewModels;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Options;
using Xunit;

namespace Flash.Trades.InteractionTests.Controllers
{
    public class TradeControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public TradeControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        private HttpClient CreateClientWithDisabledConfig()
        {
            return _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.ReplaceAsSingleton<IOptions<TradeServiceOptions>>(
                        x => Options.Create(new TradeServiceOptions
                        {
                            ConfigEndpointEnabled = false
                        }));
                });
            }).CreateClient();
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
        public async Task Options_trades_with_custom_config_returns_config_200()
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
            using (var client = CreateClientWithDisabledConfig())
            {
                var response = await client.OptionsAsync("api/trades");
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }

        [Fact]
        public async Task Get_trades_with_invalid_id_returns_400()
        {
            // NOTE: Shows how to replace a service with a mock service
            using (var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.ReplaceAsSingleton<ITradeRepository>(
                        x =>
                        {
                            var mock = A.Fake<ITradeRepository>();
                            A.CallTo(() => mock.GetByIdAsync("foo")).Returns(Task.FromResult<Trade>(null));
                            return mock;
                        });
                });
            }).CreateClient())
            {
                var response = await client.GetAsync($"api/trades/foo");
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

        [Fact]
        public async Task Get_trades_with_valid_id_returns_trade_200()
        {
            // NOTE: Shows how to replace a service with a mock service
            using (var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.ReplaceAsSingleton<ITradeRepository>(
                        x =>
                        {
                            var trade = new Trade
                            {
                                Id = "1",
                                CreatedAt = DateTimeOffset.Now,
                                Notes = "notes",
                                Ticker = "ticker"
                            };
                            var mock = A.Fake<ITradeRepository>();
                            A.CallTo(() => mock.GetByIdAsync("1")).Returns(Task.FromResult(trade));
                            return mock;
                        });
                });
            }).CreateClient())
            {
                var response = await client.GetAsync($"api/trades/1");
                Assert.True(response.IsSuccessStatusCode);

                var result = await response.ReadAsAsync<Trade>();
                Assert.NotNull(result);
                Assert.Equal("1", result.Id);
                Assert.Equal("notes", result.Notes);
                Assert.Equal("ticker", result.Ticker);
            }
        }

        [Fact]
        public async Task Get_trades_with_valid_ticker_returns_trades_200()
        {
            // NOTE: Shows how to replace a service with a mock service
            using (var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.ReplaceAsSingleton<ITradeRepository>(
                        x =>
                        {
                            var trades = new[]
                            {
                                new Trade
                                {
                                    Id = "1",
                                    CreatedAt = DateTimeOffset.Now,
                                    Notes = "notes 1",
                                    Ticker = "TCKR"
                                },
                                new Trade
                                {
                                    Id = "2",
                                    CreatedAt = DateTimeOffset.Now,
                                    Notes = "notes 2",
                                    Ticker = "TCKR"
                                }
                            };
                            var mock = A.Fake<ITradeRepository>();
                            A.CallTo(() => mock.GetByTickerAsync("TCKR")).Returns(
                                Task.FromResult<IEnumerable<Trade>>(trades)
                            );
                            return mock;
                        });
                });
            }).CreateClient())
            {
                var response = await client.GetAsync($"api/trades?ticker=TCKR");
                Assert.True(response.IsSuccessStatusCode);

                var result = await response.ReadAsAsync<TradeBatch>();
                Assert.NotNull(result);
                Assert.NotNull(result.Data);
                Assert.Equal(2, result.Data.Count());
            }
        }

        [Fact]
        public async Task Get_trades_with_invvalid_ticker_returns_empty_200()
        {
            // NOTE: Shows how to replace a service with a mock service
            using (var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.ReplaceAsSingleton<ITradeRepository>(
                        x =>
                        {
                            var mock = A.Fake<ITradeRepository>();
                            A.CallTo(() => mock.GetByTickerAsync("INVD")).Returns(
                                Task.FromResult<IEnumerable<Trade>>(new Trade[0])
                            );
                            return mock;
                        });
                });
            }).CreateClient())
            {
                var response = await client.GetAsync($"api/trades?ticker=INVD");
                Assert.True(response.IsSuccessStatusCode);

                var result = await response.ReadAsAsync<TradeBatch>();
                Assert.NotNull(result);
                Assert.NotNull(result.Data);
                Assert.Empty(result.Data);
            }
        }

        //[Fact]
        //public async Task Post_trades_with_valid_trade_returns_trade_201()
        //{
        //    using (var client = _factory.WithWebHostBuilder(builder =>
        //    {
        //        builder.ConfigureTestServices(services =>
        //        {
        //            services.ReplaceAsSingleton<ITradeRepository>(
        //                x =>
        //                {
        //                    var mock = A.Fake<ITradeRepository>();
        //                    A.CallTo(() => mock.InsertAsync(A<Trade>._)).Returns(
        //                        Task.FromResult(true)
        //                    );
        //                    return mock;
        //                });
        //        });
        //    }).CreateClient())
        //    {
        //        var response = await client.PostAsync($"api/trades?ticker=TCKR");
        //        Assert.True(response.IsSuccessStatusCode);

        //        var result = await response.ReadAsAsync<TradeBatch>();
        //        Assert.NotNull(result);
        //        Assert.NotNull(result.Data);
        //        Assert.Equal(2, result.Data.Count());
        //    }
        //}
    }
}
