using libre_pensador_api.CRUD;
using Newtonsoft.Json;

namespace libre_pensador_api.Loyverse
{
    public class LoyverseCustomersResponse
    {
        [JsonProperty("customers")]
        public List<Models.LoyverseCustomer> Customers { get; set; }
    }
}
