using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace libre_pansador_api.Loyverse.Models
{
    [DataContract(Name = "LoyverseCustomer")]
    public class LoyverseCustomer
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
