using SharedModels.Models.RequestModels;
using SharedModels.Models;
using SharedModels.Models.ViewModels;

namespace libre_pensador_api.Interfaces
{
    public interface IExpenseCategoriesService
    {
        List<ExpenseCategory> ReadAll();
        ExpenseCategoryViewModel? Read(int categoryId);
        ExpenseCategory? Create(ExpenseCategoryViewModel newCategory);
        ExpenseCategory? Delete(int categoryId);
        ExpenseCategory? Update(int categoryId, ExpenseCategoryViewModel updatedCategory);
        bool CanBeDeleted(int categoryId);
    }
}
