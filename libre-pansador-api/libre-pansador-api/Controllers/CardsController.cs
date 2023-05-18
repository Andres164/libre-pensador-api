using Microsoft.AspNetCore.Mvc;
using libre_pansador_api.Models.RequestModels;
using libre_pansador_api.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace libre_pansador_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardsService _cards;

        public CardsController(ICardsService cards)
        {
            this._cards = cards;
        }

        // GET api/Cards/QR00005
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Models.Card))]
        [ProducesResponseType(400)]
        public IActionResult Get(string id)
        {
            Models.Card? card = this._cards.Read(id);
            if(card == null)
                return NotFound();
            return Ok(card);
        }

        // PUT api/Cards/QR00005
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(Models.Card))]
        [ProducesResponseType(400)]
        public IActionResult Put(string id, [FromBody] UpdateCardRequest requestBody)
        {
            Models.Card? updatedCard = this._cards.Update(id, requestBody.CustomerEmail);
            if(updatedCard == null) 
                return NotFound();
            return Ok(updatedCard);
        }
    }
}
