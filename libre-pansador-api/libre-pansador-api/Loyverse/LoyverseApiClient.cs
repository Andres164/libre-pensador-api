using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using libre_pansador_api.Loyverse.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;

namespace libre_pansador_api.Loyverse
{
    public class LoyverseApiClient
    {
        private readonly HttpClient _httpClient;
        private const string _LoyverseApiUri = "https://api.loyverse.com/v1.0";
        private readonly string _accessToken;

        public LoyverseApiClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _accessToken = configuration["Loyverse:AccessToken"] ?? string.Empty;
        }

        public async Task<float?> UpdateCustomerPoints(string loyverseCustomerId, string customerName, float updatedTotalPoints)
        {
            var requestBody = new
            {
                id = loyverseCustomerId,
                name = customerName,
                total_points = updatedTotalPoints
            };
            var jsonRequestBody = JsonConvert.SerializeObject(requestBody);

            var request = new HttpRequestMessage(HttpMethod.Post, $"{_LoyverseApiUri}/customers");
            request.Headers.Add("Authorization", $"Bearer {_accessToken}");
            request.Content = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                throw new Exception("Faied to update customer info from Loyverse API");

            var content = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(content))
                return null;
            try
            {
                var customerInfo = JsonConvert.DeserializeObject<LoyverseCustomer>(content);
                if (customerInfo == null)
                    return null;
                return customerInfo.TotalPoints;
            }
            catch(Exception ex) 
            {
                throw new Exception("Failed to deserialize customer info", ex);
            }
        }

        public async Task<LoyverseCustomer?> GetCustomerInfoAsync(string customerId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_LoyverseApiUri}/customers/{customerId}");
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
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_LoyverseApiUri}/customers");
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
