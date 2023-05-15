using libre_pansador_api.CRUD;
using libre_pansador_api.Loyverse;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace libre_pansador_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly Customers _customers;

        public CustomersController(Customers customers)
        {
            _customers = customers;
        }

        // GET api/Clients/email@hotmail.com
        [HttpGet("{email}")]
        [ProducesResponseType(200, Type = typeof(Models.MergedCustomer))]
        [ProducesResponseType(400)]
        public IActionResult Get(string email)
        {
            Task<Models.MergedCustomer?> customer = this._customers.ReadAsync(email);
            if(customer.Result == null)
                return NotFound();
            return Ok(customer.Result);
        }

        // POST api/Clients
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(Models.LocalCustomer))]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] Models.LocalCustomer newCustomer)
        {
            Models.LocalCustomer? createdCustomer = CRUD.Customers.create(newCustomer);
            return Ok(createdCustomer);
        }

        // DELETE api/Clients/email@hotmail.com
        [HttpDelete("{email}")]
        [ProducesResponseType(200, Type = typeof(Models.LocalCustomer))]
        [ProducesResponseType(400)]
        public IActionResult Delete(string email)
        {
            Models.LocalCustomer? deletedCustomer = CRUD.Customers.delete(email);
            if (deletedCustomer == null)
                return NotFound();
            return Ok(deletedCustomer);
        }
    }
}
