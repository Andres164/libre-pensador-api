using libre_pensador_api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SharedModels.Models;
using SharedModels.RequestModels;

namespace libre_pensador_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfitsController : ControllerBase
    {
        private readonly IProfitPerPeriodsService _incomeService;

        public ProfitsController(IProfitPerPeriodsService incomeService)
        {
            this._incomeService = incomeService;
        }

        // GET: api/<IncomeController>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ProfitOfPeriod))]
        [ProducesResponseType(502)]
        public async Task<IActionResult> Get([FromQuery] DateOnly periodStart, [FromQuery] DateOnly periodEnd)
        {
            ProfitOfPeriodRequest period = new ProfitOfPeriodRequest()
            {
                PeriodStart = periodStart,
                PeriodEnd = periodEnd
            };
            try
            {
                var peroidIncome = await this._incomeService.ReadAsync(period);
                return Ok(peroidIncome);
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == null || (int)ex.StatusCode < 500)
                    throw;
                return StatusCode(502, ex.Message);
            }
        }
    }
}
