using libre_pensador_api.Interfaces;
using libre_pensador_api.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
            try
            {
                return this._dbContext.Users.Find(userName);
            }
            catch (Exception ex)
            {
                // Log exception correctly
                Console.WriteLine($"Unexpected exception when trying to read user {userName}: {ex}");
                throw;
            }
        }

        public Models.User? Create(Models.User newUser)
        {
            try
            {
                var createdUser = this._dbContext.Users.Add(newUser);
                this._dbContext.SaveChanges();
                return createdUser.Entity;
            }
            catch (Exception ex)
            {
                // Log exception correctly
                Console.WriteLine($"Unexpected exception when trying to Create user {newUser.UserName}: {ex}");
                throw;
            }
        }

        public Models.User? Delete(string userName)
        {
            try
            {
                var usertoDelete = this._dbContext.Users.Find(userName);
                if (usertoDelete == null)
                    return null;
                var deletedUser = this._dbContext.Users.Remove(usertoDelete);
                this._dbContext.SaveChanges();
                return deletedUser.Entity;
            }
            catch (Exception ex)
            {
                // Log exception correctly
                Console.WriteLine($"Unexpected exception when trying to Delete user {userName}: {ex}");
                throw;
            }
        }

        public Models.User? Update(string userName, Models.RequestModels.UpdateUserRequest updatedUser)
        {
            try
            {
                Models.User? userToUpdate = this._dbContext.Users.Find(userName);
                if (userToUpdate == null)
                    return null;
                userToUpdate.Password = updatedUser.Password;
                this._dbContext.SaveChanges();
                return userToUpdate;
            }
            catch(Exception ex) 
            { 
                // Log exception correctly
                Console.WriteLine($"Unexpected exception when trying to Update user {userName}: {ex}");
                throw;
            }
        }
    }
}
