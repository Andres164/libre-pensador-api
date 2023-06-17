namespace libre_pensador_api.Models.RequestModels
{
    public class UpdatedExpenseRequest
    {
        public Expense.Types Type { get; set; }
        public Expense.ImportanceCategories Importance { get; set; }
        public int CategoryId { get; set; }
        public decimal AmountSpent { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; } = null!;
    }
}
