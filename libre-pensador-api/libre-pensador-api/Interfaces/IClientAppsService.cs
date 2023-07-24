using libre_pensador_api.Models;
using libre_pensador_api.Models.ViewModels;

namespace libre_pensador_api.Interfaces
{
    public interface IClientAppsService
    {
        Task<ClientApp?> ReadAppAsync(int appId);
        Task<ClientApp?> CreateAsync(ClientAppViewModel newApp);
        Task<ClientApp?> DeleteAsync(int appId);
    }
}
