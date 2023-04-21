using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace libre_pansador_api.Loyverse
{
    public class LoyverseApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _accessToken;

        public LoyverseApiClient(HttpClient httpClient, string accessToken)
        {
            _httpClient = httpClient;
            _accessToken = accessToken;
        }

        public async Task<dynamic> GetCustomerInfoAsync(string customerId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.loyverse.com/v1.0/customers/{customerId}");
            request.Headers.Add("Authorization", $"Bearer {_accessToken}");

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to fetch customer info from Loyverse API");
            }

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<dynamic>(content);
        }
    }

}
