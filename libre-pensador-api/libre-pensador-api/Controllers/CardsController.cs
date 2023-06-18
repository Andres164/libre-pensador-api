using Microsoft.AspNetCore.Mvc;
using libre_pensador_api.Models.RequestModels;
using libre_pensador_api.Interfaces;
using libre_pensador_api.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace libre_pensador_api.Controllers
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

        // GET api/Cards/
        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Models.Card))]
        [ProducesResponseType(400)]
        public IActionResult Get() 
        {
            List<Models.Card> cards = this._cards.ReadAll();
            return Ok(cards);
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
            var cardView = Mappers.CardMapper.ToViewModel(card);
            return Ok(cardView);
        }

        // PUT api/Cards/QR00005
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(Models.Card))]
        [ProducesResponseType(400)]
        public IActionResult Put(string id, [FromBody] UpdateCardRequest requestBody)
        {
            Models.Card? updatedCard;
            try
            {
                updatedCard = this._cards.Update(id, requestBody.CustomerEmail);
            }
            catch (Exceptions.BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            if(updatedCard == null) 
                return NotFound();
            var cardView = Mappers.CardMapper.ToViewModel(updatedCard);
            return Ok(cardView);
        }
    }
}
