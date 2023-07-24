using libre_pensador_api.Models;

namespace libre_pensador_api.Interfaces
{
    public interface IClientAppsService
    {
        Task<ClientApp?> ReadAppAsync(int appId);
        Task<ClientApp?> CreateAsync(string newAppName);
        Task<ClientApp?> DeleteAsync(int appId);
    }
}
