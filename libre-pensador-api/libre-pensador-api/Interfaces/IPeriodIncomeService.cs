using SharedModels.Models;
using SharedModels.RequestModels;

namespace libre_pensador_api.Interfaces
{
    public interface IPeriodIncomeService
    {
        Task<PeriodIncome> ReadAsync(PeriodIncomeRequest request);
    }
}
