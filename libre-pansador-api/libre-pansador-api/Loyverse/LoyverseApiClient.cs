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

        public LoyverseApiClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _accessToken = configuration["Loyverse:AccessToken"] ?? string.Empty;
        }

        public async Task<LoyverseCustomer?> GetCustomerInfoAsync(string customerId)
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
                return JsonConvert.DeserializeObject<LoyverseCustomer>(content);
            }
            catch (JsonException ex)
            {
                throw new Exception("Failed to deserialize customer info", ex);
            }
        }

        public async Task<List<LoyverseCustomer>?> GetAllCustomersAsync()
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
                var responseObject = JsonConvert.DeserializeObject<LoyverseCustomersResponse>(content);
                if (responseObject != null)
                    return responseObject.Customers;

                return null;
            }
            catch (JsonException ex)
            {
                throw new Exception("Failed to deserialize customers list", ex);
            }
        }

    }

}
