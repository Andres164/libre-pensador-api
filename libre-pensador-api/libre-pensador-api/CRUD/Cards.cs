using libre_pensador_api.Interfaces;
using libre_pensador_api.Models;

namespace libre_pensador_api.CRUD
{
    public class Cards : ICardsService
    {
        private readonly CafeLibrePensadorDbContext _dbContext;

        public Cards(CafeLibrePensadorDbContext dbContext)
        { 
            this._dbContext = dbContext;
        }
        public Models.Card? Read(string card_id)
        {
            return this._dbContext.Cards.Find(card_id);
        }

        public Models.Card? Update(string card_id, string? updatedEmail)
        {
            Models.Card? cardToUpdate = this._dbContext.Cards.Find(card_id);
            if (cardToUpdate == null)
                return null;
            cardToUpdate.CustomerEmail = updatedEmail;
            this._dbContext.SaveChanges();
            return cardToUpdate;
        }
    }
}
