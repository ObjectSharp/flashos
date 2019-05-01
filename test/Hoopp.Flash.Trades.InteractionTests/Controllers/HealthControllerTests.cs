using System.Threading.Tasks;
using Hoopp.Flash.Trades.Web;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Hoopp.Flash.Trades.InteractionTests.Controllers
{
    public class HealthControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public HealthControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_health_returns_200()
        {
            using (var client = _factory.CreateClient())
            {
                var response = await client.GetAsync("api/health");
                Assert.True(response.IsSuccessStatusCode);
            }
        }
    }
}
