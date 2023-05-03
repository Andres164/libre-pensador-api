using libre_pansador_api.Loyverse;
using libre_pansador_api.Models;
using Microsoft.EntityFrameworkCore;

namespace libre_pansador_api.CRUD
{
    public class Customers
    {
        private readonly LoyverseApiClient _loyverseApiClient;

        public Customers()
        {
            string loyverseAccessToken = "3d58c3cab85a41e7a396dbffac531707";
            this._loyverseApiClient = new LoyverseApiClient(new HttpClient(), loyverseAccessToken);
        }

        public async Task<Models.MergedCustomer?> ReadAsync(string customerEmail)
        {
            using (var dbContext = new Models.CafeLibrePensadorDbContext())
            {
                Models.Customer? customer = dbContext.Customers.Find(customerEmail);
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

        public static Models.Customer? create(Models.Customer newCustomer)
        {
            using(var dbContext = new Models.CafeLibrePensadorDbContext())
            {
                dbContext.Customers.Add(newCustomer);
                dbContext.SaveChanges();
                return newCustomer;
            }
        }

        public static Models.Customer? delete(string customer_id)
        {
            using(var dbContext = new Models.CafeLibrePensadorDbContext())
            {
                Models.Customer? customerToDelete = dbContext.Customers.Find(customer_id);
                if (customerToDelete == null)
                    return null;
                dbContext.Customers.Remove(customerToDelete);
                dbContext.SaveChanges();
                return customerToDelete;
            }
        }
    }
}
