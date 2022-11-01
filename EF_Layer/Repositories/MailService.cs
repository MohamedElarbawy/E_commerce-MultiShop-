using CoreLayer.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MimeKit;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Repositories
{
    public class MailService : IMailService
    {
        private readonly EmailSettings mailSettings;

        public MailService(IOptions<EmailSettings> mailSettings)
        {
            this.mailSettings = mailSettings.Value;
        }

        public void SendEmail(string emailTo, string subject, string body, IList<IFormFile>? attatchments = null)
        {
            var mail = new MimeMessage()
            {
                Sender = MailboxAddress.Parse(mailSettings.Email),
                Subject = subject,
            };
            mail.To.Add(MailboxAddress.Parse(emailTo));
            var builder = new BodyBuilder();

            if (attatchments != null)
            {
                byte[] fileBytes;
                foreach (var file in attatchments)
                {
                    if(file.Length > 0)
                    {
                        using var mStream = new MemoryStream();
                        file.CopyTo(mStream);
                        fileBytes = mStream.ToArray();
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }

           builder.HtmlBody=body;
            mail.Body = builder.ToMessageBody();
            mail.From.Add(new MailboxAddress(mailSettings.DisplayName, mailSettings.Email));

            using var smtp = new SmtpClient();
            smtp.Connect(mailSettings.Host, mailSettings.Port,SecureSocketOptions.StartTls);
            smtp.Authenticate(mailSettings.Email, mailSettings.Password);
            smtp.Send(mail);
            smtp.Disconnect(true);
        }
    }
}
