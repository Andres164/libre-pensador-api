using libre_pansador_api.Interfaces;
using libre_pansador_api.Models;
using Microsoft.EntityFrameworkCore;

namespace libre_pansador_api.CRUD
{
    public class Users : IUserService
    {
        private readonly CafeLibrePensadorDbContext _dbContext;

        public Users(CafeLibrePensadorDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public Models.User? Read(string userName)
        {
            var user = this._dbContext.Users
                .AsEnumerable()
                .FirstOrDefault(u => u.UserName == userName);

            return user;
        }

        public Models.User? Create(Models.User newUser)
        {
            this._dbContext.Users.Add(newUser);
            this._dbContext.SaveChanges();
            return this.Read(newUser.UserName);
        }

        public Models.User? Delete(string userName)
        {
            Models.User? userToDelete = this._dbContext.Users
                .AsEnumerable()
                .FirstOrDefault(u => u.UserName == userName);
            if (userToDelete == null)
                return null;
            string sql = "DELETE FROM users WHERE user_number = @p0";
            int rowsAffected = _dbContext.Database.ExecuteSqlRaw(sql, userToDelete.UserNumber!);

            return rowsAffected > 0 ? userToDelete : null;
        }
    }
}
