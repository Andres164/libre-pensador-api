using libre_pensador_api.CRUD;
using libre_pensador_api.Interfaces;
using libre_pensador_api.Loyverse;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace libre_pensador_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersService _customers;

        public CustomersController(ICustomersService customers)
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
            Models.LocalCustomer? createdCustomer = this._customers.Create(newCustomer);
            return Ok(createdCustomer);
        }

        // DELETE api/Clients/email@hotmail.com
        [HttpDelete("{email}")]
        [ProducesResponseType(200, Type = typeof(Models.LocalCustomer))]
        [ProducesResponseType(400)]
        public IActionResult Delete(string email)
        {
            Models.LocalCustomer? deletedCustomer = this._customers.Delete(email);
            if (deletedCustomer == null)
                return NotFound();
            return Ok(deletedCustomer);
        }
    }
}
