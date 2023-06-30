using libre_pensador_api.Loyverse;
using libre_pensador_api.Loyverse.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class LoyverseCustomersController : ControllerBase
{
    private readonly LoyverseCustomersApiClient _loyverseApiClient;

    public LoyverseCustomersController(LoyverseCustomersApiClient loyverseApiClient)
    {
        _loyverseApiClient = loyverseApiClient;
    }

    // GET api/loyverse/customers/email@mail.com
    [HttpGet("customers/{email}")]
    [ProducesResponseType(200, Type = typeof(LoyverseCustomer))]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetCustomerByEmailAsync(string email)
    {
        try
        {
            var customers = await _loyverseApiClient.GetAllCustomersAsync();
            if (customers == null || !customers.Any())
                return NotFound();

            var customer = customers.FirstOrDefault(c => c != null && c.Email != null && c.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            if (customer == null)
                return NotFound();
            return Ok(customer);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while fetching the customer from the Loyverse API: " + ex);
        }
    }

    // PUT api/loyverse/customers/{LoyverseCustomerId}
    [HttpPut("customers/{loyverseCustomerId}")]
    [ProducesResponseType(200, Type = typeof(float))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> AddPointsToCustomer(string loyverseCustomerId, [FromBody] float pointsToAdd)
    {
        try
        {
            float? newPointBalance = await this._loyverseApiClient.AddPointsToCustomer(loyverseCustomerId, pointsToAdd);
            if (newPointBalance == null)
                return NotFound();
            return Ok(newPointBalance);
        }
        catch(HttpRequestException ex)
        {
            return StatusCode(502, ex.Message);
        }
        catch(Exception ex) 
        {
            return StatusCode(500, "An error occurred while updating the customer of the Loyverse API: " + ex);
        }
    }
}

