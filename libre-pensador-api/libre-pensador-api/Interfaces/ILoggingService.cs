namespace libre_pensador_api.Interfaces
{
    public interface ILoggingService
    {
        void LogError(Exception logEx, DateTime? ocurredOn = null);
    }
}
