using libre_pensador_api.Interfaces;
using libre_pensador_api.Models;

namespace libre_pensador_api.CRUD
{
    public class LocalCustomers : ILocalCustomerService
    {
        Models.CafeLibrePensadorDbContext _dbContext;
        private readonly ILoggingService _logger;

        public LocalCustomers(CafeLibrePensadorDbContext dbContext, ILoggingService loggingService)
        {
            this._dbContext = dbContext;
            this._logger = loggingService;
        }

        public Models.LocalCustomer? ReadWithDecryptedEmail(string decryptedEmail) 
        {
            try
            {
                Models.LocalCustomer? cusotmer = this._dbContext.Customers
                    .AsEnumerable()
                    .FirstOrDefault(c => c.DecryptedEmail == decryptedEmail);
                return cusotmer;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex);
                throw;
            }
        }
    }
}
