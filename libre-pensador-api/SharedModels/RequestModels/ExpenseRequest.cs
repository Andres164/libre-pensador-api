namespace SharedModels.Models.RequestModels
{
    public class ExpenseRequest
    {
        public Expense.Types Type { get; set; }
        public Expense.ImportanceCategories Importance { get; set; }
        public int CategoryId { get; set; }
        public decimal AmountSpent { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; } = null;

        public DateTime DateInLocalTime
        {
            get
            {
                return this.Date.ToLocalTime();
            }
            set
            {
                this.Date = value.ToUniversalTime();
            }
        }
    }
}
