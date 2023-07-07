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

                List<ProfitOfPeriod> profitPerSubPeriod = new List<ProfitOfPeriod>() { new ProfitOfPeriod() };

                DateTime nextSubPeriodStartDate = ProfitOfPeriodRequest.GetSubstractTimeLapseToDate(request.PeriodEnd, request.PeriodDivision);
                int currentSubPeriodIndex = 0;

                var receiptsEnumerator = receipts.GetEnumerator();
                while (receiptsEnumerator.Current != null) 
                {
                    if(receiptsEnumerator.Current.receipt_date <= nextSubPeriodStartDate)
                    {
                        nextSubPeriodStartDate = ProfitOfPeriodRequest.GetSubstractTimeLapseToDate(nextSubPeriodStartDate, request.PeriodDivision);
                        profitPerSubPeriod.Add(new ProfitOfPeriod()); // Fields of ProfitOfPeriod are initialized as 0, no need to do it manually here
                        currentSubPeriodIndex++;
                    }
                    else
                    {
                        profitPerSubPeriod[currentSubPeriodIndex] = AddReceiptToProfitOfPeriod(profitPerSubPeriod[currentSubPeriodIndex], receiptsEnumerator.Current);
                        receiptsEnumerator.MoveNext();
                    }
                }

                DateTime periodStart = loyverseReceiptRequest.created_at_min,
                         periodEnd = loyverseReceiptRequest.created_at_max;
                List<Expense> periodExpenses = this._expenses.ReadPeriod(periodStart, periodEnd);

                double totalPeriodExpenses = 0;
                foreach (Expense expense in periodExpenses)
                    totalPeriodExpenses += Convert.ToDouble(expense.AmountSpent);

                profitPerSubPeriod.IncomeBeforeTaxes -= totalPeriodExpenses;
                profitPerSubPeriod.NetIncome -= totalPeriodExpenses;

                return profitPerSubPeriod;
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

        private ProfitOfPeriod AddReceiptToProfitOfPeriod(ProfitOfPeriod profitOfPeriod, ReceiptViewModel receipt)
        {
            if (receipt.ReceiptType == ReceiptViewModel.ReceiptTypes.SALE)
            {
                profitOfPeriod.IncomeBeforeTaxes += receipt.total_money + receipt.total_tax;
                profitOfPeriod.NetIncome += receipt.total_money;
            }
            else
            {
                profitOfPeriod.IncomeBeforeTaxes -= receipt.total_money + receipt.total_tax;
                profitOfPeriod.NetIncome -= receipt.total_money;
            }
            return profitOfPeriod;
        }

    }
}
