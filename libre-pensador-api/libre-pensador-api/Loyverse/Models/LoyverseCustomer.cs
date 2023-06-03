using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace libre_pensador_api.Loyverse.Models
{
    public class LoyverseCustomer
    {
        [JsonProperty("id")]
        public string LoyverseCustomerId { get; set; } = null!;

        [JsonProperty("email")]
        public string Email { get; set; } = null!;

        [JsonProperty("name")]
        public string Name { get; set; } = null!;

        [JsonProperty("phone_number")]
        public string Phone { get; set; } = null!;

        [JsonProperty("address")]
        public string Address { get; set; } = null!;

        [JsonProperty("total_points")]
        public float TotalPoints { get; set; } = 0;
    }
}
