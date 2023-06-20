using SharedModels.Models;
using SharedModels.Models.RequestModels;
using SharedModels.Models.ViewModels;

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

        public static Expense ToModel(ExpenseRequest expenseRequest)
        {
            return new Expense
            {
                ExpenseId = 0,
                Type = expenseRequest.Type,
                Importance = expenseRequest.Importance,
                CategoryId = expenseRequest.CategoryId,
                AmountSpent = expenseRequest.AmountSpent,
                Date = expenseRequest.Date,
                Description = expenseRequest.Description
            };
        }
    }
}
