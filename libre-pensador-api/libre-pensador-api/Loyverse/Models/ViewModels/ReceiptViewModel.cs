namespace libre_pensador_api.Loyverse.Models.ViewModels
{
    public class ReceiptViewModel
    {
        public enum ReceiptTypes { SALE, REFUND }
        public double total_money { get; set; }
        public double total_tax { get; set; }
        public string receipt_type { get; set; } = string.Empty; // In Set, ensure that the string is valid type
        public ReceiptTypes ReceiptType
        {
            get
            {
                if(this.receipt_type == "SALE")
                    return ReceiptTypes.SALE;
                else if(this.receipt_type == "REFUND")
                    return ReceiptTypes.REFUND;

                throw new Exception("receipt_type has an invalid string value");
            }
        }
    }
}
