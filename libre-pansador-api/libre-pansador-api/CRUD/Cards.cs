namespace libre_pansador_api.CRUD
{
    public static class Cards
    {
        public static Models.Card? read(string card_id)
        { 
            using (var dbContext = new Models.CafeLibrePensadorDbContext())
            {
                return dbContext.Cards.Find(card_id);
            }
        }

        public static Models.Card? update(string card_id, string? updatedEmail)
        {
            using (var dbContext = new Models.CafeLibrePensadorDbContext())
            {
                Models.Card? cardToUpdate = dbContext.Cards.Find(card_id);
                if (cardToUpdate == null)
                    return null;
                cardToUpdate.CustomerEmail = updatedEmail;
                dbContext.SaveChanges();
                return cardToUpdate;
            }
        }

    }
}
