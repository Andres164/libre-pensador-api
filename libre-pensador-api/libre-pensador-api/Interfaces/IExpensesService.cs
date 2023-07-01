using SharedModels.Models;
using SharedModels.Models.RequestModels;
using SharedModels.Models.ViewModels;

namespace libre_pensador_api.Interfaces
{
    public interface IExpensesService
    {
        List<ExpenseViewModel> ReadAll();
        List<Expense> ReadPeriod(DateTime periodStart, DateTime periodEnd);
        ExpenseViewModel? Read(int expenseId);
        Expense? Create(ExpenseRequest newExpense);
        Expense? Delete(int expenseId);
        Expense? Update(int expenseId, ExpenseRequest updatedExpense);
    }
}
