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
            ReceiptRequest loyverseReceiptRequest = ProfitOfPeriodMapper.RequestToLoyverseReceiptsRequest(request);
            List<ReceiptViewModel> receipts = await this._loyverseReceipts.GetReceiptsAsync(loyverseReceiptRequest);
            List<ProfitOfPeriod> profitPerSubPeriod = new List<ProfitOfPeriod>();

            DateTime nextSubPeriodStartDate = request.PeriodEnd;
            while(nextSubPeriodStartDate >= request.PeriodStart)
            {
                ProfitOfPeriod period = new ProfitOfPeriod()
                {
                    PeriodDuration = request.PeriodDivision,
                    PeriodDate = nextSubPeriodStartDate
                    // Fields NetIncome and IncomeBeforeTaxes are initialized as 0, no need to do it manually
                };
                profitPerSubPeriod.Add(period);
                nextSubPeriodStartDate = ProfitOfPeriodRequest.AddTimeLapseToDate(nextSubPeriodStartDate, request.PeriodDivision, -1);
            }
            
            nextSubPeriodStartDate = ProfitOfPeriodRequest.AddTimeLapseToDate(request.PeriodEnd, request.PeriodDivision, -1);
            int currentSubPeriodIndex = 0;

            var receiptsEnumerator = receipts.GetEnumerator();
            receiptsEnumerator.MoveNext();
            while (receiptsEnumerator.Current != null) 
            {
                if(receiptsEnumerator.Current.receipt_date <= nextSubPeriodStartDate)
                {
                    nextSubPeriodStartDate = ProfitOfPeriodRequest.AddTimeLapseToDate(nextSubPeriodStartDate, request.PeriodDivision, -1);
                    currentSubPeriodIndex++;
                }
                else
                {
                    profitPerSubPeriod[currentSubPeriodIndex] = AddReceiptToProfitOfPeriod(profitPerSubPeriod[currentSubPeriodIndex], receiptsEnumerator.Current);
                    receiptsEnumerator.MoveNext();
                }
            }
            receiptsEnumerator.Dispose();
            
            DateTime periodStart = loyverseReceiptRequest.created_at_min,
                     periodEnd = loyverseReceiptRequest.created_at_max;
            List<Expense> periodExpenses = this._expenses.ReadPeriod(periodStart, periodEnd);
                        
            nextSubPeriodStartDate = ProfitOfPeriodRequest.AddTimeLapseToDate(request.PeriodEnd, request.PeriodDivision, -1);
            currentSubPeriodIndex = 0;

            var expensesEnumerator = periodExpenses.GetEnumerator();
            expensesEnumerator.MoveNext();
            while (expensesEnumerator.Current != null)
            {
                if (expensesEnumerator.Current.Date <= nextSubPeriodStartDate)
                {
                    nextSubPeriodStartDate = ProfitOfPeriodRequest.AddTimeLapseToDate(nextSubPeriodStartDate, request.PeriodDivision, -1);
                    currentSubPeriodIndex++;
                }
                else
                {
                    profitPerSubPeriod[currentSubPeriodIndex].IncomeBeforeTaxes -= (double)expensesEnumerator.Current.AmountSpent;
                    profitPerSubPeriod[currentSubPeriodIndex].NetIncome -= (double)expensesEnumerator.Current.AmountSpent;
                    expensesEnumerator.MoveNext();
                }
            }
            expensesEnumerator.Dispose();

            return profitPerSubPeriod;
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
