using libre_pensador_api.Interfaces;

namespace libre_pensador_api.Loyverse
{
    public abstract class LoyverseApiClient
    {
        protected readonly HttpClient _httpClient;
        protected const string _LoyverseApiUri = "https://api.loyverse.com/v1.0";
        protected readonly string _accessToken;
        protected ILoggingService _logger;

        public LoyverseApiClient(HttpClient httpClient, IConfiguration configuration, ILoggingService loggingService)
        {
            this._httpClient = httpClient;
            this._logger = loggingService;
            this._accessToken = configuration["Loyverse:AccessToken"] ?? throw new ArgumentException("Missing access token in the configurations");
        }
    }
}
