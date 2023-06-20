using libre_pensador_api.Interfaces;
using SharedModels.Models;
using SharedModels.Models.RequestModels;
using SharedModels.Models.ViewModels;
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
            List<ExpenseViewModel> expenses = this._expenses.ReadAll();
            return Ok(expenses);
        }

        // GET api/<ExpensesController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ExpenseViewModel))]
        public IActionResult Get(int id)
        {
            ExpenseViewModel? expense = this._expenses.Read(id);
            if (expense == null)
                return NotFound();
            return Ok(expense);
        }

        // POST api/<ExpensesController>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(Expense))]
        public IActionResult Post([FromBody] ExpenseRequest newExpense)
        {
            Expense? createdExpense = this._expenses.Create(newExpense);
            if(createdExpense == null)
                return NotFound();
            return Ok(createdExpense);
        }

        // PUT api/<ExpensesController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(Expense))]
        public IActionResult Put(int id, [FromBody] ExpenseRequest updateExpense)
        {
            Expense? updatedExpense = this._expenses.Update(id, updateExpense);
            if (updatedExpense == null)
                return NotFound();
            return Ok(updatedExpense);
        }

        // DELETE api/<ExpensesController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(Expense))]
        public IActionResult Delete(int id)
        {
            Expense? deletedExpense = this._expenses.Delete(id);
            if(deletedExpense == null)
                return NotFound();
            return Ok(deletedExpense);
        }
    }
}
