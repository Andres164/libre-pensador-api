using System.Collections.Generic;

namespace libre_pensador_api.Loyverse.Models.RequestModels
{
    public class ReceiptRequest
    {
        public DateTime created_at_min { get; set; }
        public DateTime created_at_max { get; set; }
        public int limit { get; set; } = 50;
        public string? cursor { get; set; }
    }
}
