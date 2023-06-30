using libre_pensador_api.CRUD;
using Newtonsoft.Json;

namespace libre_pensador_api.Loyverse.Models.ResponseModels
{
    public class LoyverseCustomersResponse
    {
        [JsonProperty("customers")]
        public List<LoyverseCustomer> Customers { get; set; } = null!;
    }
}
