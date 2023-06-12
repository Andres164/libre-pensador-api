using libre_pensador_api.Interfaces;
using libre_pensador_api.Models;
using libre_pensador_api.Models.RequestModels;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace libre_pensador_api.CRUD
{
    public class Users : IUserService
    {
        private readonly CafeLibrePensadorDbContext _dbContext;
        private readonly ILoggingService _logger;

        public Users(CafeLibrePensadorDbContext dbContext, ILoggingService loggingService)
        {
            this._dbContext = dbContext;
            this._logger = loggingService;
        }

        public Models.User? Read(string userName)
        {
            try
            {
                return this._dbContext.Users.Find(userName);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex);
                throw;
            }
        }

        public Models.User? ReadUserWhitCredentials(UserCredentials credentials)
        {
            var employee = this.Read(credentials.Username);
            if (employee == null)
                return null;

            bool areCredentialsValid = employee.UserName == credentials.Username && Utilities.HashingUtility.ValidatePassword(credentials.Password, employee.Password);
            return areCredentialsValid ? employee : null;
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
                this._logger.LogError(ex);
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
                this._logger.LogError(ex);
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
                this._logger.LogError(ex);
                throw;
            }
        }
    }
}
