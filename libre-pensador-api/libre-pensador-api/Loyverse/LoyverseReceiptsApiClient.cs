using libre_pensador_api.Interfaces;
using libre_pensador_api.Loyverse.Models.RequestModels;
using libre_pensador_api.Loyverse.Models.ResponseModels;
using libre_pensador_api.Loyverse.Models.ViewModels;
using Newtonsoft.Json;
using System.Text;

namespace libre_pensador_api.Loyverse
{
    public class LoyverseReceiptsApiClient : LoyverseApiClient
    {
        public LoyverseReceiptsApiClient(HttpClient httpClient, IConfiguration configuration, ILoggingService loggingService)
        : base(httpClient, configuration, loggingService)
        {

        }

        public async Task<List<ReceiptViewModel>> GetReceiptsAsync(ReceiptRequest requestBody)
        {
            try
            {
                string createdAtMin = requestBody.created_at_min.ToString("yyyy-MM-ddTHH:mm:ss.fff"),
                       createdAtMax = requestBody.created_at_max.ToString("yyyy-MM-ddTHH:mm:ss.fff");
                string uri = $"{_LoyverseApiUri}/receipts?created_at_min={createdAtMin}Z&created_at_max={createdAtMax}Z";
                var httpRequest = new HttpRequestMessage(HttpMethod.Get, uri);
                httpRequest.Headers.Add("Authorization", $"Bearer {this._accessToken}");

                var getResponse = await this._httpClient.SendAsync(httpRequest);
                int responseStatusCode = (int)getResponse.StatusCode;
                if (responseStatusCode >= 500)
                    throw new HttpRequestException($"Received a server error ({responseStatusCode}) from the Loyverse API while getting receipts.", null, getResponse.StatusCode);
                if (responseStatusCode >= 400)
                    throw new HttpRequestException($"Received a 400 level status code on get receipts request. Status code: {responseStatusCode}", null, getResponse.StatusCode);

                string responseBody = await getResponse.Content.ReadAsStringAsync();
                ReceiptsResponse? deserializedBody = JsonConvert.DeserializeObject<ReceiptsResponse>(responseBody);
                if (deserializedBody == null)
                    throw new NullReferenceException("Couldn't deserialize the response body correctly, as it returned null");

                return deserializedBody.Receipts;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex);
                throw;
            }
        }
    }
}
