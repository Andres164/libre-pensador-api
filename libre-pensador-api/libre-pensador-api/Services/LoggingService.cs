using libre_pensador_api.Interfaces;
using libre_pensador_api.Models;
using Microsoft.EntityFrameworkCore;

namespace libre_pensador_api.Services
{
    public  class LoggingService : ILoggingService
    {
        private readonly IDbContextFactory<CafeLibrePensadorDbContext> _dbContextFactory;

        public LoggingService(IDbContextFactory<CafeLibrePensadorDbContext> dbContextFactory)
        {
            this._dbContextFactory = dbContextFactory;
        }

        public void LogError(Exception logEx, DateTime? ocurredOn = null) 
        {
            try
            {
                using var dbContext = this._dbContextFactory.CreateDbContext();
                LogEntry logEntry = new LogEntry(logEx, ocurredOn);
                dbContext.LogEntries.Add(logEntry);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                string path = "Logs/error-log.txt";
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                System.IO.File.AppendAllText(path, $"\n\n[{ocurredOn ?? DateTime.Now}] Unexpected exception when saving log entry: {ex}. \n# Exception being logged #: {logEx} With Message {logEx.Message}.");
            }
        }
    }
}
