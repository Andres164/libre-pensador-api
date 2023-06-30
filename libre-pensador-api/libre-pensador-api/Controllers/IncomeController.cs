using libre_pensador_api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SharedModels.Models;
using SharedModels.RequestModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace libre_pensador_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeController : ControllerBase
    {
        public IPeriodIncomeService Sales { get; set; }

        public IncomeController(IPeriodIncomeService salesService)
        {
            this.Sales = salesService;
        }

        // GET: api/<SalesController>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(PeriodIncome))]
        public IActionResult Get([FromBody] PeriodIncomeRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
