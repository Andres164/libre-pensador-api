using libre_pensador_api.Interfaces;
using libre_pensador_api.Mappers;
using libre_pensador_api.Models;
using libre_pensador_api.Models.ViewModels;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace libre_pensador_api.CRUD
{
    public class ExpenseCategories : IExpenseCategoriesService
    {
        private readonly CafeLibrePensadorDbContext _dbContext;
        private readonly ILoggingService _logger;

        public ExpenseCategories(CafeLibrePensadorDbContext dbContext, ILoggingService loggingService)
        {
            this._dbContext = dbContext;
            this._logger = loggingService;
        }

        public List<ExpenseCategory> ReadAll()
        {
            try
            {
                return this._dbContext.ExpenseCategories.ToList();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex);
                throw;
            }
        }
        public ExpenseCategoryViewModel? Read(int categoryId)
        {
            try
            {
                var category = this._dbContext.ExpenseCategories.Find(categoryId);
                if (category == null)
                    return null;
                var categoryView = ExpenseCategoryMapper.ToViewModel(category);
                return categoryView;
            }
            catch (Exception ex) 
            {
                this._logger.LogError(ex);
                throw;
            }
        }
        public ExpenseCategory? Create(ExpenseCategoryViewModel newCategory)
        {
            try
            {
                var categoryModel = ExpenseCategoryMapper.ToModel(newCategory);
                var createdCategory = this._dbContext.ExpenseCategories.Add(categoryModel);

                this._dbContext.SaveChanges();
                return createdCategory.Entity;
            }
            catch (Exception ex) 
            {
                this._logger.LogError(ex);
                throw;
            }
        }
        public ExpenseCategory? Delete(int categoryId)
        {
            try
            {
                var categoryToDelete = this._dbContext.ExpenseCategories.Find(categoryId);
                if (categoryToDelete == null)
                    return null;

                this._dbContext.ExpenseCategories.Remove(categoryToDelete);
                this._dbContext.SaveChanges();
                return categoryToDelete;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex);
                throw;
            }
        }
        public ExpenseCategory? Update(int categoryId, ExpenseCategoryViewModel updatedCategory)
        {
            try
            {
                var categoryToUpdate = this._dbContext.ExpenseCategories.Find(categoryId);
                if (categoryToUpdate == null)
                    return null;

                categoryToUpdate.ExpenseCategoryName = updatedCategory.ExpenseCategoryName;
                this._dbContext.SaveChanges();
                return categoryToUpdate;
            }
            catch(Exception ex) 
            {
                this._logger.LogError(ex);
                throw;
            }
        }
    }
}
