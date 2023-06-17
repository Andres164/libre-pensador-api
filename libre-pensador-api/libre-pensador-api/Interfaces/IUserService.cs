using libre_pensador_api.Models.RequestModels;

namespace libre_pensador_api.Interfaces
{
    public interface IUserService
    {
        Models.User? Read(string userName);
        Models.User? ReadUserWhitCredentials(UserCredentials credentials);
        Models.User? Create(Models.User newUser);
        Models.User? Delete(string userName);
        Models.User? Update(string userName, Models.RequestModels.UpdateUserRequest updatedUser);
    }
}
