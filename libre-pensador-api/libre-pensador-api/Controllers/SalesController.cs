using libre_pensador_api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SharedModels.Models;
using SharedModels.RequestModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace libre_pensador_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        public ISalesService Sales { get; set; }

        public SalesController(ISalesService salesService)
        {
            this.Sales = salesService;
        }

        // GET: api/<SalesController>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Sale))]
        public IActionResult Get([FromBody] SalesRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
