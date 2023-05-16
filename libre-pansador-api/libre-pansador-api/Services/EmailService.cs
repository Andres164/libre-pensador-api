using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace libre_pansador_api.Services
{
    public class EmailService
    {
        private readonly MailboxAddress _senderMailBoxAddress;
        private readonly string _senderEmailPassword;

        public EmailService(MailboxAddress senderMailBoxAddress, string senderEmailPassword)
        {
            if (senderEmailPassword == string.Empty)
                throw new ArgumentException("EmailServices constructor doesn't accept empty string as argument for senderEmailPassword");

            this._senderMailBoxAddress = senderMailBoxAddress;
            this._senderEmailPassword = senderEmailPassword;
        }

        public async Task SendErrorLogEmailAsync(string errorLog, string toName, string toEmail)
        {
            var email = new MimeMessage();
            email.From.Add(this._senderMailBoxAddress);
            email.To.Add(new MailboxAddress(toName, toEmail));
            email.Subject = "Error Log";
            email.Body = new TextPart("plain")
            {
                Text = errorLog
            };
            email.Headers.Add("X-Priority", "1");
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(this._senderMailBoxAddress.Address, this._senderEmailPassword);
                await client.SendAsync(email);
                await client.DisconnectAsync(true);
            }
        }
    }

}
