using Microsoft.AspNet.Identity;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceMVC.Service.Identity
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {

            return ConfigSendGridasync(message);
        }

        private async Task ConfigSendGridasync(IdentityMessage message)
        {
            try
            {
                // Create a Web transport for sending email.
                var apiKey = ConfigurationManager.AppSettings["SENDGRID_KEY"];
                var client = new SendGridClient(apiKey);
                var to = new EmailAddress(message.Destination);
                var subject = message.Subject;

                var from = new EmailAddress("t.anatolii96@gmail.com", "PlayerUp");
                var plainTextContent = message.Body;
                var htmlContent = message.Body;
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);
            }
            catch (Exception)
            {


            }

        }
    }
}
