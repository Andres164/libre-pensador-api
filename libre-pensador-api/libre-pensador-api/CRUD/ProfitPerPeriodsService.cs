using libre_pensador_api.Interfaces;
using libre_pensador_api.Loyverse;
using libre_pensador_api.Loyverse.Models.RequestModels;
using libre_pensador_api.Loyverse.Models.ViewModels;
using libre_pensador_api.Mappers;
using SharedModels.Models;
using SharedModels.RequestModels;

namespace libre_pensador_api.CRUD
{
    public class ProfitPerPeriodsService : IProfitPerPeriodsService
    {
        private readonly LoyverseReceiptsApiClient _loyverseReceipts;
        private readonly IExpensesService _expenses;
        private readonly ILoggingService _loggingService;

        public ProfitPerPeriodsService(LoyverseReceiptsApiClient loyverseReceiptsApiClient, IExpensesService expensesService, ILoggingService loggingService)
        {
            this._loyverseReceipts = loyverseReceiptsApiClient;
            this._expenses = expensesService;
            this._loggingService = loggingService;
        }

        public async Task<List<ProfitOfPeriod>> ReadAsync(ProfitOfPeriodRequest request)
        {
            try
            {
                ReceiptRequest loyverseReceiptRequest = ProfitOfPeriodMapper.RequestToLoyverseReceiptsRequest(request);
                List<ReceiptViewModel> receipts = await this._loyverseReceipts.GetReceiptsAsync(loyverseReceiptRequest);

                List<ProfitOfPeriod> periodsIncome = new List<ProfitOfPeriod>();
                
                foreach (var receipt in receipts)
                {
                    if (receipt.ReceiptType == ReceiptViewModel.ReceiptTypes.SALE)
                    {
                        periodsIncome.IncomeBeforeTaxes += receipt.total_money + receipt.total_tax;
                        periodsIncome.NetIncome += receipt.total_money;
                    }
                    else
                    {
                        periodsIncome.IncomeBeforeTaxes -= receipt.total_money + receipt.total_tax;
                        periodsIncome.NetIncome -= receipt.total_money;
                    }
                }

                DateTime periodStart = loyverseReceiptRequest.created_at_min,
                         periodEnd = loyverseReceiptRequest.created_at_max;
                List<Expense> periodExpenses = this._expenses.ReadPeriod(periodStart, periodEnd);

                double totalPeriodExpenses = 0;
                foreach (Expense expense in periodExpenses)
                    totalPeriodExpenses += Convert.ToDouble(expense.AmountSpent);

                periodsIncome.IncomeBeforeTaxes -= totalPeriodExpenses;
                periodsIncome.NetIncome -= totalPeriodExpenses;

                return periodsIncome;
            }
            catch(HttpRequestException)
            {
                throw;
            }
            catch (Exception ex)
            {
                this._loggingService.LogError(ex);
                throw;
            }
        }

        private int GetDateSubPeriod(DateTime date, int period)
        {
            string[] splitedDate = date.ToString().Split('-');
            return splitedDate[period];
        }
    }
}
