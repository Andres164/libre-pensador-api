using libre_pensador_api.Models;
using libre_pensador_api.Models.ViewModels;

namespace libre_pensador_api.Mappers
{
    public static class ExpenseMapper
    {
        public static ExpenseViewModel ToViewModel(Expense expenseModel, string ExpenseCategoryName)
        {
            return new ExpenseViewModel
            {
                ExpenseId = expenseModel.ExpenseId,
                Type = expenseModel.Type,
                Importance = expenseModel.Importance,
                CategoryId = expenseModel.CategoryId,
                AmountSpent = expenseModel.AmountSpent,
                Date = expenseModel.Date,
                Description = expenseModel.Description,
                CategoryName = ExpenseCategoryName
            };
        }
    }
}
