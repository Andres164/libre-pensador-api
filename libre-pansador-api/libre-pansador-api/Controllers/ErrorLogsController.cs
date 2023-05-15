using libre_pansador_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace libre_pansador_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorLogsController : ControllerBase
    {
        private readonly EmailService _emailService;
        private readonly MailboxAddress _recipientMailBoxAdress;

        public ErrorLogsController(EmailService emailService, MailboxAddress recipientMailBoxAddress)
        {
            this._emailService = emailService;
            this._recipientMailBoxAdress = recipientMailBoxAddress;
        }

        [HttpPost("send-error-log")]
        public async Task<IActionResult> SendErrorLog([FromBody] string errorLog)
        {
            try
            {
                await _emailService.SendErrorLogEmailAsync(errorLog, this._recipientMailBoxAdress.Name, this._recipientMailBoxAdress.Address);
                return Ok("Error log sent successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to send error log: {ex.Message}");
            }
        }
    }

}
