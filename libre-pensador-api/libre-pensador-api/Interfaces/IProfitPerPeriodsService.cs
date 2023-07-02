using SharedModels.Models;
using SharedModels.RequestModels;

namespace libre_pensador_api.Interfaces
{
    public interface IProfitPerPeriodsService
    {
        Task<ProfitOfPeriod> ReadAsync(ProfitOfPeriodRequest request);
    }
}
