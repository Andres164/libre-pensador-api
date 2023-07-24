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


        public async Task<ClientApp?> ReadAppAsync(int appId)
        {
            return await this._dbContext.ClientApps.FindAsync(appId);
        }

        public async Task<ClientApp?> CreateAsync(string newAppName)
        {
            
        }

        public async Task<ClientApp?> DeleteAsync(int appId)
        {
            var appToDelete = await this.ReadAppAsync(appId);
            if (appToDelete == null)
                return null;
            var deletedApp = this._dbContext.Remove(appToDelete);
            return deletedApp.Entity;
        }
    }
}
