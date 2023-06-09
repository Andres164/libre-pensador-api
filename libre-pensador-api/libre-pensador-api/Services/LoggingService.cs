using libre_pensador_api.Models;

namespace libre_pensador_api.Services
{
    public  class LoggingService
    {
        private readonly CafeLibrePensadorDbContext _dbContext;

        LoggingService(CafeLibrePensadorDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public void LogError(Exception logEx, DateTime? ocurredOn = null) 
        {
            try
            {
                LogEntry logEntry = new LogEntry(logEx, ocurredOn);
                this._dbContext.LogEntries.Add(logEntry);
                this._dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                string path = "Logs/error-log.txt";
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                System.IO.File.AppendAllText(path, $"Unexpected exception when saving log entry: {ex}. \n\nException being logged: {logEx} With Message {logEx.Message}. At: { ocurredOn ?? DateTime.Now } ");
            }
        }
    }
}
