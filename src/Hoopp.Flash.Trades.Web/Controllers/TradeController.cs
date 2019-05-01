using System.Diagnostics;
using System.Threading.Tasks;
using Hoopp.Flash.Trades.Domain.Models;
using Hoopp.Flash.Trades.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Hoopp.Flash.Trades.Web.Controllers
{
    [Route("api/trades")]
    public class TradeController : ControllerBase
    {
        private readonly ITradeService _service;

        public TradeController(ILogger<HealthController> logger, ITradeService service)
        {
            _service = service;
        }
        
        [HttpOptions]
        public IActionResult Config()
        {
            if (Debugger.IsAttached || _service.CurrentConfig.ConfigEndpointEnabled)
                return Ok(_service.CurrentConfig);

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result != null)
                return Ok(result);

            // I prefer returning 400 (BadRequest) instead of 404 (NotFound) or 204 (NoContent)
            // but this is definitely a preference thing
            return BadRequest(); 
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody]Trade trade)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.InsertAsync(trade);
            return Created($"/api/trades/{result.Id}", result);
        }
    }
}