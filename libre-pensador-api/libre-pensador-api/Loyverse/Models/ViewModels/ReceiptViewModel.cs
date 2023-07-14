namespace libre_pensador_api.Loyverse.Models.ViewModels
{
    public class ReceiptViewModel
    {
        public enum ReceiptTypes { SALE, REFUND }
        public decimal total_money { get; set; }
        public decimal total_tax { get; set; }
        public string receipt_type { get; set; } = string.Empty; // In Set, ensure that the string is valid type
        public DateTime receipt_date { get; set; }
        public ReceiptTypes ReceiptType
        {
            get
            {
                return this.receipt_type switch
                {
                    "SALE" => ReceiptTypes.SALE,
                    "REFUND" => ReceiptTypes.REFUND,
                    _ => throw new Exception("receipt_type has an invalid string value")
                };
            }
        }
    }
}
