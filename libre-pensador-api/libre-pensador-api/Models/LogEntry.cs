using System.ComponentModel.DataAnnotations.Schema;

namespace libre_pensador_api.Models
{
    public class LogEntry
    {
        public int Id { get; set; }
        public string LogLevel { get; set; }
        public string Message { get; set; }
        public string? Exception { get; set; }
        public string? StackTrace { get; set; }
        public DateTime OccurredOn { get; set; }

        public LogEntry() { }

        public LogEntry(Exception ex, DateTime? occurredOn = null, string logLevel = "Error")
        {
            Message = ex.Message;
            StackTrace = ex.StackTrace;
            Exception = ex.ToString();
            LogLevel = logLevel;
            OccurredOn = occurredOn ?? DateTime.Now;
        }
    }
}
