using libre_pensador_api.Loyverse.Models.RequestModels;
using SharedModels.RequestModels;

namespace libre_pensador_api.Mappers
{
    public static class ProfitOfPeriodMapper
    {
        public static ReceiptRequest RequestToLoyverseReceiptsRequest(ProfitOfPeriodRequest periodIncomeRequest)
        {
            return new ReceiptRequest
            {
                created_at_min = periodIncomeRequest.PeriodStart,
                created_at_max = periodIncomeRequest.PeriodEnd
            };
        }
    }
}
