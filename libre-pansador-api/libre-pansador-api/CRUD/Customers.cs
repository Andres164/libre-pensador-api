﻿using libre_pansador_api.Interfaces;
using libre_pansador_api.Loyverse;
using libre_pansador_api.Models;
using Microsoft.EntityFrameworkCore;

namespace libre_pansador_api.CRUD
{
    public class Customers : ICustomersService
    {
        private readonly LoyverseApiClient _loyverseApiClient;
        private readonly CafeLibrePensadorDbContext _dbContext;

        public Customers(LoyverseApiClient loyverseApiClient, CafeLibrePensadorDbContext dbContext)
        {
            this._loyverseApiClient = loyverseApiClient;
            this._dbContext = dbContext;
        }

        public async Task<Models.MergedCustomer?> ReadAsync(string customerEmail)
        {

            Models.LocalCustomer? customer = this._dbContext.Customers.Find(customerEmail);
            if (customer == null)
                return null;
            try
            {
                // DEBUGGING
                Console.WriteLine($"(ReadAsync)Customer Email: {customer.Email}");
                /////
                var loyverseCustomerInfo = await _loyverseApiClient.GetCustomerInfoAsync(customer.LoyverseCustomerId);
                if(loyverseCustomerInfo == null)
                {
                    Console.WriteLine("Couldn't find the customer in the loyverse API");
                    return null;
                }
                return new MergedCustomer(customer, loyverseCustomerInfo);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching data from Loyverse API: " + ex.Message);
                return null;
            }
        }

        public Models.LocalCustomer? Create(Models.LocalCustomer newCustomer)
        {
            this._dbContext.Customers.Add(newCustomer);
            this._dbContext.SaveChanges();
            return this._dbContext.Customers.Find(newCustomer.LoyverseCustomerId);
        }

        public Models.LocalCustomer? Delete(string customerEmail)
        {
            Models.LocalCustomer? customerToDelete = this._dbContext.Customers.Find(customerEmail);
            if (customerToDelete == null)
                return null;
            this._dbContext.Customers.Remove(customerToDelete);
            this._dbContext.SaveChanges();
            return customerToDelete;
        }
    }
}
