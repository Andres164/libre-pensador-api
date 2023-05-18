namespace libre_pansador_api.Interfaces
{
    public interface ICardsService
    {
        Models.Card? Read(string card_id);
        Models.Card? Update(string card_id, string? updatedEmail);
    }
}
