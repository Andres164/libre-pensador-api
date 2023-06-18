using libre_pensador_api.Models.RequestModels;
using libre_pensador_api.Models;
using libre_pensador_api.Models.ViewModels;

namespace libre_pensador_api.Interfaces
{
    public interface IExpenseCategoriesService
    {
        List<ExpenseCategory>? ReadAll();
        ExpenseCategory? Read(int categoryId);
        ExpenseCategory? Create(ExpenseCategoryViewModel newCategory);
        ExpenseCategory? Delete(int categoryId);
        ExpenseCategory? Update(int categoryId, ExpenseCategoryViewModel updatedCategory);
    }
}
