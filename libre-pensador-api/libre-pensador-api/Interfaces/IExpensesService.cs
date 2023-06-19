using libre_pensador_api.Models;
using libre_pensador_api.Models.RequestModels;
using libre_pensador_api.Models.ViewModels;

namespace libre_pensador_api.Interfaces
{
    public interface IExpensesService
    {
        List<ExpenseViewModel>? ReadAll();
        ExpenseViewModel? Read(int expenseId);
        Expense? Create(ExpenseRequest newExpense);
        Expense? Delete(int expenseId);
        Expense? Update(int expenseId, ExpenseRequest updatedExpense);
    }
}
