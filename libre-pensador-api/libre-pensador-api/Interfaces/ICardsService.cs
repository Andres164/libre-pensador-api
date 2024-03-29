﻿namespace libre_pensador_api.Interfaces
{
    public interface ICardsService
    {
        List<Models.Card> ReadAll();
        Models.Card? Read(string card_id);
        Models.Card? Update(string card_id, string? updatedEmail);
    }
}
