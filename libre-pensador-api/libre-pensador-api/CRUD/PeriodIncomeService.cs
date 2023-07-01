using libre_pensador_api.Interfaces;
using libre_pensador_api.Loyverse;
using libre_pensador_api.Loyverse.Models.RequestModels;
using libre_pensador_api.Loyverse.Models.ViewModels;
using libre_pensador_api.Mappers;
using SharedModels.Models;
using SharedModels.RequestModels;

namespace libre_pensador_api.CRUD
{
    public class PeriodIncomeService : IPeriodIncomeService
    {
        private readonly LoyverseReceiptsApiClient _loyverseReceipts;
        private readonly IExpensesService _expenses;

        public PeriodIncomeService(LoyverseReceiptsApiClient loyverseReceiptsApiClient, IExpensesService expensesService)
        {
            this._loyverseReceipts = loyverseReceiptsApiClient;
            this._expenses = expensesService;
        }

        public async Task<PeriodIncome> ReadAsync(PeriodIncomeRequest request)
        {
            ReceiptRequest receiptRequest = PeriodIncomeRequestMapper.ToLoyverseReceiptsRequest(request);

            List<ReceiptViewModel> receipts = await this._loyverseReceipts.GetReceiptsAsync(receiptRequest);
             
            PeriodIncome periodIncome = new PeriodIncome();
            foreach (var receipt in receipts)
            {
                if (receipt.ReceiptType == ReceiptViewModel.ReceiptTypes.SALE)
                {
                    periodIncome.IncomeBeforeTaxes += receipt.total_money + receipt.total_tax;
                    periodIncome.NetIncome += receipt.total_money;
                }
                else
                {
                    periodIncome.IncomeBeforeTaxes -= receipt.total_money + receipt.total_tax;
                    periodIncome.NetIncome -= receipt.total_money;
                }
            }

            DateTime periodStart = receiptRequest.created_at_min, 
                     periodEnd = receiptRequest.created_at_max;
            List<Expense> periodExpenses = this._expenses.ReadPeriod(periodStart, periodEnd);

            double totalPeriodExpenses = 0;
            foreach(Expense expense in periodExpenses)
                totalPeriodExpenses += Convert.ToDouble(expense.AmountSpent);

            periodIncome.IncomeBeforeTaxes -= totalPeriodExpenses;
            periodIncome.NetIncome -= totalPeriodExpenses;

            return periodIncome;
        }
    }
}
