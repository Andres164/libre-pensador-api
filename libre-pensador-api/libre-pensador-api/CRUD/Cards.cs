using libre_pensador_api.Exceptions;
using libre_pensador_api.Interfaces;
using libre_pensador_api.Models;
using Microsoft.CodeAnalysis;

namespace libre_pensador_api.CRUD
{
    public class Cards : ICardsService
    {
        private readonly CafeLibrePensadorDbContext _dbContext;
        private readonly ILoggingService _logger;

        public Cards(CafeLibrePensadorDbContext dbContext, ILoggingService loggingService)
        { 
            this._dbContext = dbContext;
            this._logger = loggingService;
        }

        public List<Models.Card> ReadAll() 
        {
            try
            {
                var orderedCards = this._dbContext.Cards.OrderBy(c => c.CardId).ToList();
                return orderedCards;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex);
                throw;
            }
        }

        public Models.Card? Read(string card_id)
        {
            try
            {
                return this._dbContext.Cards.Find(card_id);
            }
            catch(Exception ex) 
            {
                this._logger.LogError(ex);
                throw;
            }
        }

        public Models.Card? Update(string card_id, string? updatedEmail)
        {
            try
            {
                if (updatedEmail != null)
                {
                    ILocalCustomerService localCustomerService = new CRUD.LocalCustomers(this._dbContext, this._logger);
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
            catch(BadRequestException)
            { 
                throw;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex);
                throw;
            }
        }
    }
}
