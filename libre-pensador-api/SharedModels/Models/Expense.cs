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

        public static string GetImportanceTranslation(Expense.ImportanceCategories importance)
        {
            return importance switch
            {
                Expense.ImportanceCategories.Essential => "Esencial",
                Expense.ImportanceCategories.NonEssential => "No esencial",
                Expense.ImportanceCategories.Luxury => "Lujo",
                _ => throw new ArgumentOutOfRangeException(nameof(importance), importance, $"Value must be between 0 and 2. Argument's {nameof(importance)} value {importance} is out of range.")
            };
        }

        public static string GetTypeTranslation(Expense.Types type)
        {
            return type switch
            {
                Expense.Types.Personal => "Personal",
                Expense.Types.Buissines => "Negocios",
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, $"Value must be either 0 or 1. Argument's {nameof(type)} value {type} is out of range.")
            };
        }
    }
}
