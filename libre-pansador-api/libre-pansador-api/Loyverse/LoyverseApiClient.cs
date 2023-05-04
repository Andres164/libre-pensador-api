using System;
using System.Net.Http;
using System.Threading.Tasks;
using libre_pansador_api.Loyverse.Models;
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

        public async Task<Customer?> GetCustomerInfoAsync(string customerId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.loyverse.com/v1.0/customers/{customerId}");
            request.Headers.Add("Authorization", $"Bearer {_accessToken}");

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Failed to fetch customer info from Loyverse API");

            var content = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(content))
                return null;
            try
            {
                return JsonConvert.DeserializeObject<Customer>(content);
            }
            catch (JsonException ex)
            {
                throw new Exception("Failed to deserialize customer info", ex);
            }
        }

        public async Task<List<Customer>?> GetAllCustomersAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.loyverse.com/v1.0/customers");
            request.Headers.Add("Authorization", $"Bearer {_accessToken}");

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                throw new Exception("Failed to fetch customers from Loyverse API");

            var content = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(content))
                return null;

            try
            {
                return JsonConvert.DeserializeObject<List<Customer>>(content);
            }
            catch (JsonException ex)
            {
                throw new Exception("Failed to deserialize customers list", ex);
            }
        }

    }

}
