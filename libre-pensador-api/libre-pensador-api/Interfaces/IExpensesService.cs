using libre_pensador_api.Models;
using libre_pensador_api.Models.RequestModels;

namespace libre_pensador_api.Interfaces
{
    public interface IExpensesService
    {
        List<Expense>? ReadAll();
        Expense? Read(int expenseId);
        Expense? Create(ExpenseRequest newExpense);
        Expense? Delete(int expenseId);
        Expense? Update(int expenseId, ExpenseRequest updatedExpense);
    }
}
