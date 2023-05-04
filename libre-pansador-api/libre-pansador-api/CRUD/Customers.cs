using libre_pansador_api.Loyverse;
using libre_pansador_api.Models;
using Microsoft.EntityFrameworkCore;

namespace libre_pansador_api.CRUD
{
    public class Customers
    {
        private readonly LoyverseApiClient _loyverseApiClient;

        public Customers(LoyverseApiClient loyverseApiClient)
        {
            this._loyverseApiClient = loyverseApiClient;
        }

        public async Task<Models.MergedCustomer?> ReadAsync(string customerEmail)
        {
            using (var dbContext = new Models.CafeLibrePensadorDbContext())
            {
                Models.LocalCustomer? customer = dbContext.Customers.Find(customerEmail);
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
        }

        public static Models.LocalCustomer? create(Models.LocalCustomer newCustomer)
        {
            using(var dbContext = new Models.CafeLibrePensadorDbContext())
            {
                dbContext.Customers.Add(newCustomer);
                dbContext.SaveChanges();
                return newCustomer;
            }
        }

        public static Models.LocalCustomer? delete(string customerEmail)
        {
            using(var dbContext = new Models.CafeLibrePensadorDbContext())
            {
                Models.LocalCustomer? customerToDelete = dbContext.Customers.Find(customerEmail);
                if (customerToDelete == null)
                    return null;
                dbContext.Customers.Remove(customerToDelete);
                dbContext.SaveChanges();
                return customerToDelete;
            }
        }
    }
}
