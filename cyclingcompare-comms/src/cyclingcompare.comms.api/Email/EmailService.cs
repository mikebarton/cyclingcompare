using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cyclingcompare.comms.api.Email
{
    public class EmailService
    {
        private readonly IOptions<SendGridOptions> _options;

        public EmailService(IOptions<SendGridOptions> options)
        {
            _options = options;
        }

        public async Task SendMessage(string toEmail, string fromEmail, string fromName, string subject, string body)
        {            
            var client = new SendGridClient(_options.Value.ApiKey);
            var from = new EmailAddress(fromEmail, fromName);
            
            var to = new EmailAddress(toEmail);            
            var msg = MailHelper.CreateSingleEmail(from, to, subject, body, body);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
