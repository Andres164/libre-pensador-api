using libre_pensador_api.Interfaces;
using libre_pensador_api.Mappers;
using libre_pensador_api.Models;
using libre_pensador_api.Models.RequestModels;

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

        List<Expense>? ReadAll()
        {
            try
            {
                var expensesOrderedByNewest = this._dbContext.Expenses.OrderByDescending(e => e.Date).ToList();  // the amount of returned rows can be set with .Take(amount)
                int resultCount = expensesOrderedByNewest.Count;
                return resultCount >= 1 ? expensesOrderedByNewest : null;
            }
            catch (Exception ex) 
            { 
                this._logger.LogError(ex);
                throw;
            }
        }
        Expense? Read(int expenseId)
        {
            try
            {
                return this._dbContext.Expenses.Find(expenseId);
            }
            catch (Exception ex) 
            {
                this._logger.LogError(ex);
                throw;
            }
        }
        Expense? Create(ExpenseRequest newExpense)
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
        Expense? Delete(int expenseId)
        {

        }
        Expense? Update(int expenseId, ExpenseRequest updatedExpense)
        {
                            
        }
    }
}
