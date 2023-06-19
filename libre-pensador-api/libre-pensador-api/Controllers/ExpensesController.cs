using libre_pensador_api.Interfaces;
using libre_pensador_api.Models.RequestModels;
using libre_pensador_api.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace libre_pensador_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpensesService _expenses;

        public ExpensesController(IExpensesService expensesService)
        {
            this._expenses = expensesService;
        }

        // GET: api/<ExpensesController>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<ExpenseViewModel>))]
        public IActionResult Get()
        {
            
        }

        // GET api/<ExpensesController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ExpenseViewModel))]
        public IActionResult Get(int id)
        {
            
        }

        // POST api/<ExpensesController>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ExpenseViewModel))]
        public IActionResult Post([FromBody] ExpenseRequest expense)
        {

        }

        // PUT api/<ExpensesController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(ExpenseViewModel))]
        public IActionResult Put(int id, [FromBody] ExpenseRequest expense)
        {

        }

        // DELETE api/<ExpensesController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(ExpenseViewModel))]
        public IActionResult Delete(int id)
        {

        }
    }
}
