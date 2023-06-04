using libre_pensador_api.Interfaces;
using libre_pensador_api.Models;
using Microsoft.EntityFrameworkCore;

namespace libre_pensador_api.CRUD
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

        public Models.User? Update(string userName, Models.RequestModels.UpdateUserRequest updatedUser)
        {
            string sql = "UPDATE TABLE users SET password = @p0 WHERE user_name = @p1";
            int rowsAffected = this._dbContext.Database.ExecuteSqlRaw(sql, updatedUser.Password, userName);

            this._dbContext.Users
                .AsEnumerable()
                .FirstOrDefault(u => u.)
            return rowsAffected > 0 ? updatedUser : null;

        }
    }
}
