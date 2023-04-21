using libre_pansador_api.Loyverse;

namespace libre_pansador_api.CRUD
{
    public class Customers
    {
        private readonly LoyverseApiClient _loyverseApiClient;

        public Customers()
        {
            string loyverseAccessToken = "";
            this._loyverseApiClient = new LoyverseApiClient(new HttpClient(), loyverseAccessToken);
        }

        public async Task<Models.Customer?> ReadAsync(string customerId)
        {
            using (var dbContext = new Models.CafeLibrePensadorDbContext())
            {
                var localCustomer = dbContext.Customers.Find(customerId);
                if (localCustomer == null)
                {
                    return null;
                }

                try
                {
                    var loyverseCustomerInfo = await _loyverseApiClient.GetCustomerInfoAsync(customerId);

                    // Merge the data from both APIs here
                    // For example, you can add the address and total points from the Loyverse API to your local customer data

                    localCustomer.Address = loyverseCustomerInfo.address;
                    localCustomer.TotalPoints = loyverseCustomerInfo.total_points;
                }
                catch (Exception ex)
                {
                    // Handle errors while fetching data from the Loyverse API
                    Console.WriteLine("Error fetching data from Loyverse API: " + ex.Message);
                }

                return localCustomer;
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
