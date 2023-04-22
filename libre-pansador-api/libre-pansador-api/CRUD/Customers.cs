using libre_pansador_api.Loyverse;
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

        public async Task<Models.Customer?> ReadAsync(string customerEmail)
        {
            using (var dbContext = new Models.CafeLibrePensadorDbContext())
            {
                var fetchCustomer = dbContext.Customers.FirstOrDefaultAsync(c => c.Email == customerEmail);
                Models.Customer? customer = fetchCustomer.Result;
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
                    loyverseCustomerInfo.DateOfBirth = customer.DateOfBirth;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error fetching data from Loyverse API: " + ex.Message);
                    return null;
                }
                return customer;
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
