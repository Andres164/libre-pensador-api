namespace libre_pensador_api.Interfaces
{
    public interface ILocalCustomerService
    {
        Models.LocalCustomer? ReadWithDecryptedEmail(string decryptedEmail);
    }
}
