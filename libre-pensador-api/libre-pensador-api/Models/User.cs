namespace libre_pensador_api.Models
{
    public class User
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsAdmin { get; set; } = false;
        public int? UserNumber { get; set; }
    }
}
