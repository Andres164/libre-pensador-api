using libre_pansador_api.Loyverse;
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
            var customer = await this._loyverseApiClient.GetCustomerInfoAsync(loyverseCustomerId);
            if (customer == null) 
                return NotFound();
            float updatedTotalPoints = customer.TotalPoints + pointsToAdd;
            float? newPointBalance = await this._loyverseApiClient.UpdateCustomerPoints(loyverseCustomerId, customer.Name, updatedTotalPoints);
            if (newPointBalance == null)
                return NotFound();
            return Ok(newPointBalance);
        }
        catch(Exception ex) 
        {
            return StatusCode(500, "An error occurred while updating the customer of the Loyverse API: " + ex);
        }
    }
}

