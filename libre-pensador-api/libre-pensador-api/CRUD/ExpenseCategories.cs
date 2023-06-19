using libre_pensador_api.Interfaces;
using libre_pensador_api.Models;
using libre_pensador_api.Models.ViewModels;

namespace libre_pensador_api.CRUD
{
    public class ExpenseCategories : IExpenseCategoriesService
    {
        private readonly CafeLibrePensadorDbContext _dbContext;

        public ExpenseCategories(CafeLibrePensadorDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public List<ExpenseCategory> ReadAll()
        {
            throw new Exception();
        }
        public ExpenseCategory? Read(int categoryId)
        {
            throw new Exception();
        }
        public ExpenseCategory? Create(ExpenseCategoryViewModel newCategory)
        {
            throw new Exception();
        }
        public ExpenseCategory? Delete(int categoryId)
        {
            throw new Exception();
        }
        public ExpenseCategory? Update(int categoryId, ExpenseCategoryViewModel updatedCategory)
        {
            throw new Exception();
        }
    }
}
