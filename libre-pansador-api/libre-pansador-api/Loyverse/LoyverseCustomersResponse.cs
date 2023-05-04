using libre_pansador_api.CRUD;
using Newtonsoft.Json;

namespace libre_pansador_api.Loyverse
{
    public class LoyverseCustomersResponse
    {
        [JsonProperty("customers")]
        public List<Models.LoyverseCustomer> Customers { get; set; }
    }
}
