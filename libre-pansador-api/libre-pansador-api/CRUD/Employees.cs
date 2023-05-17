using libre_pansador_api.Models;

namespace libre_pansador_api.CRUD
{
    public static class Employees
    {
        public static Models.Employee? Read(string userName) 
        {
            using(var dbContext = new Models.CafeLibrePensadorDbContext())
            {
                return dbContext.Employees.Find(userName);
            }
        }
    }
}
