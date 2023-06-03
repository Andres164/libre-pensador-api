namespace libre_pensador_api.Interfaces
{
    public interface IUserService
    {
        Models.User? Read(string userName);
        Models.User? Create(Models.User newUser);
        Models.User? Delete(string userName);
    }
}
