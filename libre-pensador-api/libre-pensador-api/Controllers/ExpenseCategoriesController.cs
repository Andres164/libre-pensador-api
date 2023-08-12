using libre_pensador_api.Interfaces;
using SharedModels.Models;
using SharedModels.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace libre_pensador_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "AdminOnly")]
    public class ExpenseCategoriesController : ControllerBase
    {
        private readonly IExpenseCategoriesService _expenseCategories;

        public ExpenseCategoriesController(IExpenseCategoriesService expenseCategoriesService)
        {
            this._expenseCategories = expenseCategoriesService;
        }

        [Authorize]
        // GET: api/<ExpenseCategoriesController>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ExpenseCategory))]
        public IActionResult Get()
        {
            return Ok(this._expenseCategories.ReadAll());
        }

        [Authorize]
        // GET api/<ExpenseCategoriesController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ExpenseCategoryViewModel))]
        public IActionResult Get(int id)
        {
            var category = this._expenseCategories.Read(id);
            if (category == null)
                return NotFound();
            return Ok(category);
        }

        // POST api/<ExpenseCategoriesController>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ExpenseCategory))]
        public IActionResult Post([FromBody] ExpenseCategoryViewModel newCategory)
        {
            var createdCategory = this._expenseCategories.Create(newCategory);
            if(createdCategory == null)
                return NotFound();
            return Ok(createdCategory);
        }

        // PUT api/<ExpenseCategoriesController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(ExpenseCategory))]
        public IActionResult Put(int id, [FromBody] ExpenseCategoryViewModel updateCategoryView)
        {
            var updatedCategory = this._expenseCategories.Update(id, updateCategoryView);
            if(updatedCategory == null)
                return NotFound();
            return Ok(updatedCategory);
        }

        // DELETE api/<ExpenseCategoriesController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(ExpenseCategory))]
        public IActionResult Delete(int id)
        {
            bool isCategoryReferencedInExpenses = this._expenseCategories.CanBeDeleted(id);
            if(isCategoryReferencedInExpenses)
                return Conflict();
            var deletedCategory = this._expenseCategories.Delete(id);
            if(deletedCategory == null)
                return NotFound();
            return Ok(deletedCategory);
        }

        // GET api/<ExpenseCategoriesController>/canBeDeleted/5
        [HttpGet("canBeDeleted/{id}")]
        [ProducesResponseType(200, Type = typeof(bool))]
        public IActionResult CanBeDeleted(int id)
        {
            return Ok(this._expenseCategories.CanBeDeleted(id));
        }
    }
}
