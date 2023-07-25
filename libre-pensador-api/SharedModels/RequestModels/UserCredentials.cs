namespace libre_pensador_api.Models.RequestModels
{
    public class UserCredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; } = true;
    }
}
