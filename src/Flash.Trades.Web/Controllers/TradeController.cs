using System.Threading.Tasks;
using Flash.Core.Models;
using Flash.Trades.Domain.Models;
using Flash.Trades.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Flash.Trades.Web.Controllers
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
            if (_service.CurrentConfig.ConfigEndpointEnabled)
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

        [HttpGet]
        public async Task<IActionResult> GetByTicker([FromQuery]string ticker)
        {
            var result = await _service.GetByTickerAsync(ticker);

            // This is again a preference, but my frontend devs prefer to always
            // return an object with the items inside, instead of a direct array
            return Ok(new Batch<Trade> { Data = result });
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody]Trade trade)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.InsertAsync(trade);
            return Created($"/api/trades/{result.Id}", result);
        }

        [HttpPost("batch")]
        public async Task<IActionResult> InsertBatch([FromBody]Batch<Trade> trades)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.InsertBatchAsync(trades.Data);
            return Ok(result);
        }
    }
}