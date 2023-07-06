using SharedModels.Models;
using SharedModels.RequestModels;

namespace libre_pensador_api.Interfaces
{
    public interface IProfitPerPeriodsService
    {
        Task<List<ProfitOfPeriod>> ReadAsync(ProfitOfPeriodRequest request);
    }
}
