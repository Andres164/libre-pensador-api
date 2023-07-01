using libre_pensador_api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SharedModels.Models;
using SharedModels.RequestModels;

namespace libre_pensador_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeController : ControllerBase
    {
        private readonly IPeriodIncomeService _incomeService;

        public IncomeController(IPeriodIncomeService incomeService)
        {
            this._incomeService = incomeService;
        }

        // GET: api/<IncomeController>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(PeriodIncome))]
        [ProducesResponseType(502)]
        public async Task<IActionResult> Get([FromBody] PeriodIncomeRequest request)
        {
            try
            {
                var peroidIncome = await this._incomeService.ReadAsync(request);
                return Ok(peroidIncome);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(502, ex.Message);
            }
        }
    }
}
