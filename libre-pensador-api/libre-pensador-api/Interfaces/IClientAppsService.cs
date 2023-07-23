using libre_pensador_api.Models;

namespace libre_pensador_api.Interfaces
{
    public interface IClientAppsService
    {
        Task<ClientApp> ReadAppAsync(int appId);
        Task CreateAsync(string newAppName);
        Task DeleteAsync(int appId);
    }
}
