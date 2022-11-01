using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Helper
{
    public static class SendEmail
    {
       public  static async Task Send()
        {
            var apiKey = Environment.GetEnvironmentVariable("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");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("test@example.com", "Example User");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress("test@example.com", "Example User");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);

            Console.WriteLine("sendgrid is done");
        }
    }
}
