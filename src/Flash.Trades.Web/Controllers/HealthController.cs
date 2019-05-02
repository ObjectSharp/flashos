using Flash.Trades.Domain.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Flash.Trades.Web.Controllers
{
    [Route("api/health")]
    public class HealthController : ControllerBase
    {
        private readonly ILogger _logger;

        public HealthController(ILogger<HealthController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Ping()
        {
            _logger.LogInformation("Ping");
            return Ok();
        }
    }
}