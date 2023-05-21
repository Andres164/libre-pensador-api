using libre_pansador_api.Interfaces;
using libre_pansador_api.Models;
using Microsoft.EntityFrameworkCore;

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
            var employee = this._dbContext.Employees
                .AsEnumerable()
                .FirstOrDefault(e => e.UserName == userName);
            return employee;
        }

        public Models.Employee? Create(Models.Employee newEmployee)
        {
            //newEmployee.IsAdmin = false;
            this._dbContext.Employees.Add(newEmployee);
            this._dbContext.SaveChanges();
            return Read(newEmployee.UserName);
        }

        public Models.Employee? Delete(string userName)
        {
            Models.Employee? employeeToDelete = this._dbContext.Employees
                .AsEnumerable()
                .FirstOrDefault(e => e.UserName == userName);
            if (employeeToDelete == null)
                return null;
            string sql = "DELETE FROM employees WHERE user_name = @p0";
            int rowsAffected = _dbContext.Database.ExecuteSqlRaw(sql, employeeToDelete.UserName);

            return rowsAffected > 0 ? employeeToDelete : null;
        }
    }
}
