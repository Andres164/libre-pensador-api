namespace SharedModels.Models
{
    public class ExpenseCategory
    {
        public int ExpenseCategoryId { get; set; }
        public string ExpenseCategoryName { get; set; } = null!;

        public ExpenseCategory() { }

        public ExpenseCategory(ExpenseCategory original)
        {
            this.ExpenseCategoryId = original.ExpenseCategoryId;
            this.ExpenseCategoryName = original.ExpenseCategoryName;
        }
    }
}
