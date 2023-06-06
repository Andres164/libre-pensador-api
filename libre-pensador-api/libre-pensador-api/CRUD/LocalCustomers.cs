using libre_pensador_api.Interfaces;
using libre_pensador_api.Models;

namespace libre_pensador_api.CRUD
{
    public class LocalCustomers : ILocalCustomerService
    {
        Models.CafeLibrePensadorDbContext _dbContext;

        public LocalCustomers(CafeLibrePensadorDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public Models.LocalCustomer? ReadWithDecryptedEmail(string decryptedEmail) 
        {
            Models.LocalCustomer? cusotmer = this._dbContext.Customers
                .AsEnumerable()
                .FirstOrDefault(c => c.DecryptedEmail == decryptedEmail);
            return cusotmer;
        }
    }
}
