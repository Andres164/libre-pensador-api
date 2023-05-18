using libre_pansador_api.Interfaces;
using libre_pansador_api.Models;

namespace libre_pansador_api.CRUD
{
    public class Employees : IEmployeesService
    {
        private readonly CafeLibrePensadorDbContext _dbContext;

        public Employees(CafeLibrePensadorDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public Models.Employee? Read(string userName) 
        {
            return this._dbContext.Employees.Find(userName);
        }
    }
}
