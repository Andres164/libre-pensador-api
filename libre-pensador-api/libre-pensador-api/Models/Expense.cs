using System.Diagnostics;

namespace libre_pensador_api.Models
{
    public class Expense
    {
        public enum ExpenseType { Personal, Buissines };
        public enum ExpenseImportance { Essential, NonEssential, Luxury};

        public int ExpenseId { get; set; }
        public ExpenseType Type { get; set; }
        public ExpenseImportance Importance { get; set; }
        public int CategoryId { get; set; }
        public decimal AmountSpent { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; } = null!;
    }
}
