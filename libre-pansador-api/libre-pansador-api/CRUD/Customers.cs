namespace libre_pansador_api.CRUD
{
    public static class Customers
    {
        public static Models.Customer? read(string customer_id)
        {
            using(var dbContext = new Models.CafeLibrePensadorDbContext())
            {
                return dbContext.Customers.Find(customer_id);
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
