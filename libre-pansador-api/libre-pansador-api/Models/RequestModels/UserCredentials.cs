namespace libre_pansador_api.Models.RequestModels
{
    public class UserCredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public UserCredentials(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }
    }
}
