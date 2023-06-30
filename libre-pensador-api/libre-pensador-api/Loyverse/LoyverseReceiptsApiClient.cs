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
                var jsonRequestBody = JsonConvert.SerializeObject(requestBody);

                var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{_LoyverseApiUri}/receipts");
                httpRequest.Headers.Add("Authorization", $"Bearer {this._accessToken}");
                httpRequest.Content = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");

                var getResponse = await this._httpClient.SendAsync(httpRequest);
                int responseStatusCode = (int)getResponse.StatusCode;
                if (responseStatusCode >= 500)
                    throw new HttpRequestException("Received a server error (500 level status code) from the Loyverse API while getting receipts.");
                if (responseStatusCode >= 400)
                    throw new Exception($"Unexpected non success status code on get receipts request. Status code: {responseStatusCode}");

                string responseBody = await getResponse.Content.ReadAsStringAsync();
                ReceiptsResponse? deserializedBody = JsonConvert.DeserializeObject<ReceiptsResponse>(responseBody);
                if (deserializedBody == null)
                    throw new Exception("Couldn't deserialize the response body correctly, as it returned null");

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
