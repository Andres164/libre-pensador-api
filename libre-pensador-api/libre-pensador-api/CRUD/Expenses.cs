using libre_pensador_api.Interfaces;
using libre_pensador_api.Mappers;
using libre_pensador_api.Models;
using libre_pensador_api.Models.RequestModels;
using libre_pensador_api.Models.ViewModels;

namespace libre_pensador_api.CRUD
{
    public class Expenses : IExpensesService
    {
        private readonly CafeLibrePensadorDbContext _dbContext;
        private readonly ILoggingService _logger;

        public Expenses(CafeLibrePensadorDbContext dbContext, ILoggingService logger)
        {
            this._dbContext = dbContext;
            this._logger = logger;

        }

        public List<ExpenseViewModel> ReadAll()
        {
            try
            {
                List<Expense> expenses = this._dbContext.Expenses.OrderByDescending(e => e.Date).ToList();  // the amount of returned rows can be set with .Take(amount)
                List<ExpenseViewModel> expenseViews = new List<ExpenseViewModel>(expenses.Count);

                foreach (var expense in expenses)
                {
                    var expenseCategory = this._dbContext.ExpenseCategories.Find(expense.CategoryId);
                    var categoryName = expenseCategory!.ExpenseCategoryName; // CategoryId is a foreign key, meaning expenseCategory will not be null
                    expenseViews.Add(ExpenseMapper.ToViewModel(expense, categoryName));
                }
                return expenseViews;
            }
            catch (Exception ex) 
            { 
                this._logger.LogError(ex);
                throw;
            }
        }
        public ExpenseViewModel? Read(int expenseId)
        {
            try
            {
                var expense = this._dbContext.Expenses.Find(expenseId);
                if (expense == null)
                    return null;
                var expenseCategory = this._dbContext.ExpenseCategories.Find(expense.CategoryId);
                var categoryName = expenseCategory!.ExpenseCategoryName;
                return ExpenseMapper.ToViewModel(expense, categoryName);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex);
                throw;
            }
        }
        public Expense? Create(ExpenseRequest newExpense)
        {
            try
            {
                Expense newExpenseModel = ExpenseMapper.ToModel(newExpense);
                var createdExpense = this._dbContext.Expenses.Add(newExpenseModel); // Need to handle foreign key violation excpetion
                this._dbContext.SaveChanges();
                return newExpenseModel;
            }
            catch(Exception ex)
            { 
                this._logger.LogError(ex);
                throw;
            }
        }
        public Expense? Delete(int expenseId)
        {
            try
            {
                var expenseToDelete = this._dbContext.Expenses.Find(expenseId);
                if(expenseToDelete == null)
                    return null;
                var deletedExpense = this._dbContext.Expenses.Remove(expenseToDelete);
                this._dbContext.SaveChanges();
                return deletedExpense.Entity;
            }
            catch(Exception ex)
            {
                this._logger.LogError(ex);
                throw;
            }
        }
        public Expense? Update(int expenseId, ExpenseRequest updatedExpense)
        {
            try
            {
                var expenseToUpdate = this._dbContext.Expenses.Find(expenseId);
                if (expenseToUpdate == null)
                    return null;

                expenseToUpdate.Type = updatedExpense.Type;
                expenseToUpdate.Importance = updatedExpense.Importance;
                expenseToUpdate.CategoryId = updatedExpense.CategoryId;
                expenseToUpdate.AmountSpent = updatedExpense.AmountSpent;
                expenseToUpdate.Date = updatedExpense.Date;
                expenseToUpdate.Description = updatedExpense.Description;

                this._dbContext.SaveChanges();
                return expenseToUpdate;
            }
            catch (Exception ex)
            { 
                this._logger.LogError(ex);
                throw;
            }
        }
    }
}
