using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace SharedModels.Models
{
    public class Expense
    {
        public enum Types { Personal, Buissines };
        public enum ImportanceCategories { Essential, NonEssential, Luxury};

        public int ExpenseId { get; set; }
        public Expense.Types Type { get; set; }
        public Expense.ImportanceCategories Importance { get; set; }
        public int CategoryId { get; set; }
        public decimal AmountSpent { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; } = null!;
    }
}
