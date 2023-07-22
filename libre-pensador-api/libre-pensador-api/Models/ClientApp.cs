namespace libre_pensador_api.Models
{
    public class ClientApp
    {
        public int AppId { get; set; }
        public string Name { get; set; } = null!;
        public string EncryptedJwtSecretKey { get; set; } = null!;
    }
}
