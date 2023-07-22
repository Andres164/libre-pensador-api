namespace libre_pensador_api.Models.RequestModels
{
    public class UserCredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ClientAppId { get; set; }
        public bool RememberMe { get; set; } = false;

        public UserCredentials(string username, string password, string clientAppId)
        {
            this.Username = username;
            this.Password = password;
            this.ClientAppId = clientAppId;
        }
    }
}
