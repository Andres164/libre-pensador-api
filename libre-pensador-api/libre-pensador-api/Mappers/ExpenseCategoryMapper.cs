using SharedModels.Models;
using SharedModels.Models.ViewModels;

namespace libre_pensador_api.Mappers
{
    public static class ExpenseCategoryMapper
    {
        public static ExpenseCategoryViewModel ToViewModel(ExpenseCategory category)
        {
            return new ExpenseCategoryViewModel 
            { 
                ExpenseCategoryName = category.ExpenseCategoryName 
            };
        }

        public static ExpenseCategory ToModel(ExpenseCategoryViewModel category) 
        {
            return new ExpenseCategory
            {
                ExpenseCategoryId = 0,
                ExpenseCategoryName = category.ExpenseCategoryName
            };
        }
    }
}
