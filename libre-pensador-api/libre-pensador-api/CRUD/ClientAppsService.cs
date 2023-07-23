using libre_pensador_api.Interfaces;
using libre_pensador_api.Models;

namespace libre_pensador_api.CRUD
{
    public class ClientAppsService : IClientAppsService
    {
        private readonly CafeLibrePensadorDbContext _dbContext;

        public ClientAppsService(CafeLibrePensadorDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public Task CreateAsync(string newAppName)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int appId)
        {
            throw new NotImplementedException();
        }

        public Task<ClientApp> ReadAppAsync(int appId)
        {
            throw new NotImplementedException();
        }
    }
}
