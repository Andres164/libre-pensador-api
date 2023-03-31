using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace libre_pansador_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        // GET api/Clients/5
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Models.Customer))]
        [ProducesResponseType(400)]
        public IActionResult Get(int id)
        {

        }

        // POST api/Clients
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(Models.Customer))]
        public IActionResult Post([FromBody] )
        {

        }

        // PUT api/Clients/5
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(Models.Customer))]
        [ProducesResponseType(400)]
        public void Put(int id, [FromBody] )
        {

        }

        // DELETE api/Clients/5
        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(Models.Customer))]
        [ProducesResponseType(400)]
        public void Delete(int id)
        {

        }
    }
}
