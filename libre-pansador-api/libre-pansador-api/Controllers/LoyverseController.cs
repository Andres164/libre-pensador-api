﻿using libre_pansador_api.Loyverse;
using libre_pansador_api.Loyverse.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class LoyverseController : ControllerBase
{
    private readonly LoyverseApiClient _loyverseApiClient;

    public LoyverseController(LoyverseApiClient loyverseApiClient)
    {
        _loyverseApiClient = loyverseApiClient;
    }

    // GET api/loyverse/customers/email@mail.com
    [HttpGet("customer/{email}")]
    [ProducesResponseType(200, Type = typeof(Customer))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetCustomerByEmailAsync(string email)
    {
        try
        {
            var customers = await _loyverseApiClient.GetAllCustomersAsync();
            if (customers == null || !customers.Any())
                return NotFound();

            var customer = customers.FirstOrDefault(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            if (customer == null)
                return NotFound();
            return Ok(customer);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while fetching the customer from the Loyverse API");
        }
    }

}

