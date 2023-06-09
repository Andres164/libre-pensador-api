using libre_pensador_api.Interfaces;
using libre_pensador_api.Loyverse;
using libre_pensador_api.Loyverse.Models;
using libre_pensador_api.Models;
using Microsoft.EntityFrameworkCore;

namespace libre_pensador_api.CRUD
{
    public class Customers : ICustomersService
    {
        private readonly LoyverseApiClient _loyverseApiClient;
        private readonly CafeLibrePensadorDbContext _dbContext;
        private readonly ILoggingService _logger;

        public Customers(LoyverseApiClient loyverseApiClient, CafeLibrePensadorDbContext dbContext, ILoggingService loggingService)
        {
            this._loyverseApiClient = loyverseApiClient;
            this._dbContext = dbContext;
            this._logger = loggingService;
        }
        
        public async Task<Models.MergedCustomer?> ReadAsync(string customerEmail)
        {

            Models.LocalCustomer? customer = this._dbContext.Customers
                .AsEnumerable()
                .FirstOrDefault( c => c.DecryptedEmail == customerEmail );
            if (customer == null)
                return null;
            try
            {
                var loyverseCustomerInfo = await _loyverseApiClient.GetCustomerInfoAsync(customer.LoyverseCustomerId);
                if(loyverseCustomerInfo == null)
                {
                    Console.WriteLine("Couldn't find the customer in the loyverse API");
                    return null;
                }
                return new MergedCustomer(customer, loyverseCustomerInfo);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching data from Loyverse API: " + ex.Message);
                return null;
            }
        }

        public Models.LocalCustomer Create(Models.LocalCustomer newCustomer)
        {
            try
            {
                var createdCustomer = this._dbContext.Customers.Add(newCustomer);
                this._dbContext.SaveChanges();
                return createdCustomer.Entity;
            }
            catch (Exception ex)
            {
                // Log exception correctly
                Console.WriteLine($"Unexpected exception when trying to Create the customer with email {newCustomer}: {ex}");
                throw;
            }
        }

        public Models.LocalCustomer? Delete(string customerEmail)
        {
            try
            {
                Models.LocalCustomer? customerToDelete = this._dbContext.Customers
                    .AsEnumerable()
                    .FirstOrDefault(c => c.DecryptedEmail == customerEmail);
                if (customerToDelete == null)
                    return null;
                var deletedCustomer = this._dbContext.Customers.Remove(customerToDelete);
                this._dbContext.SaveChanges();
                return deletedCustomer.Entity;
            }
            catch (Exception ex)
            {
                // Log exception correctly
                Console.WriteLine($"Unexpected exception when trying to Delete the customer with email {customerEmail}: {ex}");
                throw;
            }
        }
    }
}
