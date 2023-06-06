using libre_pensador_api.Models.ViewModels;
using libre_pensador_api.Models;

namespace libre_pensador_api.Mappers
{
    public static class CardMapper
    {
        public static CardViewModel ToViewModel(Card card)
        {
            return new CardViewModel
            {
                CardId = card.CardId,
                CustomerEmail = EncryptionUtility.Decrypt(card.EncryptedCustomerEmail)!
            };
        }
    }

}
