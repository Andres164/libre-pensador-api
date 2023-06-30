using libre_pensador_api.Loyverse.Models.ViewModels;
using Newtonsoft.Json;

namespace libre_pensador_api.Loyverse.Models.ResponseModels
{
    public class ReceiptsResponse
    {
        [JsonProperty("receipts")]
        public List<ReceiptViewModel> Receipts { get; set; } = null!;
    }
}
