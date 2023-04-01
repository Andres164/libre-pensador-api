using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace libre_pansador_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        // GET api/Cards/QR00005
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Models.Card))]
        [ProducesResponseType(400)]
        public IActionResult Get(string id)
        {
            Models.Card? card = CRUD.Cards.read(id);
            if(card == null)
                return NotFound();
            return Ok(card);
        }

        // PUT api/Cards/QR00005
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(Models.Card))]
        [ProducesResponseType(400)]
        public IActionResult Put(string id, string customerEmail)
        {
            Models.Card? updatedCard = CRUD.Cards.update(id, customerEmail);
            if(updatedCard == null) 
                return NotFound();
            return Ok(updatedCard);
        }
    }
}
