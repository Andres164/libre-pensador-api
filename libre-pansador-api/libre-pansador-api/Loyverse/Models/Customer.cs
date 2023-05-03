using Newtonsoft.Json;

namespace libre_pansador_api.Loyverse.Models
{
    public class Customer
    {
        [JsonProperty("id")]
        public string LoyverseCustomerId { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        [JsonProperty("phone_number")]
        public string Phone { get; set; } = null!;
        public string Address { get; set; } = null!;
        [JsonProperty("total_points")]
        public float TotalPoints { get; set; } = 0;
    }
}
