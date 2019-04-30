using Hoopp.Flash.Domain.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Hoopp.Flash.Web.Controllers
{
    [Route("api/health")]
    public class HealthController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IOptions<TradeServiceOptions> _options;

        public HealthController(ILogger<HealthController> logger, IOptions<TradeServiceOptions> options)
        {
            _logger = logger;
            _options = options;
        }

        [HttpGet]
        public IActionResult Ping()
        {
            _logger.LogInformation("Ping");
            return Ok();
        }
    }
}