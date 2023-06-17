using libre_pensador_api.Models;

namespace libre_pensador_api.Interfaces
{
    public interface IExpensesService
    {
        Expense Read(int expenseId);
        Expense Update(int expenseId, Expense updatedExpense);
    }
}
