using libre_pensador_api.Loyverse.Models.RequestModels;
using SharedModels.RequestModels;

namespace libre_pensador_api.Mappers
{
    public static class PeriodIncomeRequestMapper
    {
        public static ReceiptRequest ToLoyverseReceiptsRequest(PeriodIncomeRequest periodIncomeRequest)
        {
            return new ReceiptRequest
            {
                created_at_min = periodIncomeRequest.PeriodStart.ToDateTime(TimeOnly.MinValue),
                created_at_max = periodIncomeRequest.PeriodEnd.ToDateTime(TimeOnly.MinValue)
            };
        }
    }
}
