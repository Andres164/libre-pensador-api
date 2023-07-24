using libre_pensador_api.Models;
using libre_pensador_api.Models.ViewModels;

namespace libre_pensador_api.Interfaces
{
    public interface IClientAppsService
    {
        Task<List<ClientApp>> ReadAllAsync();
        Task<ClientApp?> ReadAsync(int appId);
        Task<ClientApp?> CreateAsync(ClientAppViewModel newApp);
        Task<ClientApp?> DeleteAsync(int appId);
    }
}
