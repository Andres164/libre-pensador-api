using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using libre_pensador_api.Interfaces;
using libre_pensador_api.Loyverse.Models;
using libre_pensador_api.Loyverse.Models.ResponseModels;
using libre_pensador_api.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static System.Net.WebRequestMethods;

namespace libre_pensador_api.Loyverse
{
    public class LoyverseCustomersApiClient : LoyverseApiClient
    {
        public LoyverseCustomersApiClient(HttpClient httpClient, IConfiguration configuration, ILoggingService loggingService)
        : base(httpClient, configuration, loggingService)
        {

        }

        public async Task<float?> AddPointsToCustomer(string customerId, float pointsToAdd)
        {
            var customer = await GetRawCustomerInfoAsync(customerId);
            if (customer == null)
                return null;
            var jsonCustomer = JObject.Parse(customer);
            float updatedPoints = (float)jsonCustomer["total_points"]! + pointsToAdd;
            jsonCustomer["total_points"] = updatedPoints;
            var updatedCustomer = JsonConvert.SerializeObject(jsonCustomer);

            var request = new HttpRequestMessage(HttpMethod.Post, $"{_LoyverseApiUri}/customers");
            request.Headers.Add("Authorization", $"Bearer {_accessToken}");
            request.Content = new StringContent(updatedCustomer, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            if ((int)response.StatusCode >= 500)
                throw new HttpRequestException("Received a server error (500 level status code) from the Loyverse API while updating the customer.");
            if ((int)response.StatusCode >= 400)
                return null;

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
            request.Headers.Add("Authorization", $"Bearer {this._accessToken}");

            var response = await this._httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Failed to fetch customer info from Loyverse API" + response.StatusCode);

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
            request.Headers.Add("Authorization", $"Bearer {this._accessToken}");

            var response = await this._httpClient.SendAsync(request);
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

        private async Task<string?> GetRawCustomerInfoAsync(string customerId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_LoyverseApiUri}/customers/{customerId}");
            request.Headers.Add("Authorization", $"Bearer {this._accessToken}");

            var response = await this._httpClient.SendAsync(request);
            if((int)response.StatusCode >= 500)
                throw new HttpRequestException("Received a server error (500 level status code) from the Loyverse API While trying to Get the customer.");
            if ((int)response.StatusCode >= 400)
                return null;
            var rawCustomerInfo = await response.Content.ReadAsStringAsync();
            return rawCustomerInfo;
        }


    }

}
