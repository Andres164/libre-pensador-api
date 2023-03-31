using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace libre_pansador_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        // GET api/Cards/5
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Models.Card))]
        [ProducesResponseType(400)]
        public IActionResult Get(int id)
        {
            
        }

        // PUT api/Cards/5
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(Models.Card))]
        [ProducesResponseType(400)]
        public IActionResult Put(int id, [FromBody])
        {
            
        }
    }
}
