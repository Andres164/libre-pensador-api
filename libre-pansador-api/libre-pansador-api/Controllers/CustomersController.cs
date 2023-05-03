using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace libre_pansador_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        // GET api/Clients/email@hotmail.com
        [HttpGet("{email}")]
        [ProducesResponseType(200, Type = typeof(Models.MergedCustomer))]
        [ProducesResponseType(400)]
        public IActionResult Get(string email)
        {
            var customers = new CRUD.Customers();
            Task<Models.MergedCustomer?> customer = customers.ReadAsync(email);
            if(customer.Result == null)
                return NotFound();
            return Ok(customer.Result);
        }

        // POST api/Clients
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(Models.Customer))]
        public IActionResult Post([FromBody] Models.Customer newCustomer)
        {
            Models.Customer? createdCustomer = CRUD.Customers.create(newCustomer);
            return Ok(createdCustomer);
        }

        // DELETE api/Clients/email@hotmail.com
        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(Models.Customer))]
        [ProducesResponseType(400)]
        public IActionResult Delete(string id)
        {
            Models.Customer? deletedCustomer = CRUD.Customers.delete(id);
            if (deletedCustomer == null)
                return NotFound();
            return Ok(deletedCustomer);
        }
    }
}
