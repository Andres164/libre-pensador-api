namespace libre_pansador_api.Interfaces
{
    public interface ICustomersService
    {
        Task<Models.MergedCustomer?> ReadAsync(string customerEmail);
        Models.LocalCustomer? Create(Models.LocalCustomer newCustomer);
        Models.LocalCustomer? Delete(string customerEmail);

    }
}
