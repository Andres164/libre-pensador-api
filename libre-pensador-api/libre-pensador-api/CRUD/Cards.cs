using libre_pensador_api.Interfaces;
using libre_pensador_api.Models;
using Microsoft.CodeAnalysis;

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
            if(updatedEmail != null) 
            {
                ILocalCustomerService localCustomerService = new CRUD.LocalCustomers(this._dbContext);
                Models.LocalCustomer? customer = localCustomerService.ReadWithDecryptedEmail(updatedEmail);
                if (customer == null)
                    throw new Exceptions.BadRequestException($"Customer with email {updatedEmail} not found.");
                updatedEmail = customer.EncryptedEmail;
            }

            Models.Card? cardToUpdate = this._dbContext.Cards.Find(card_id);
            if (cardToUpdate == null)
                return null;
            cardToUpdate.EncryptedCustomerEmail = updatedEmail;
            this._dbContext.SaveChanges();
            return cardToUpdate;
        }
    }
}
