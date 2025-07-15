using Microsoft.Extensions.Options;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Infrastructure.Service.GmailSettings;
using System.Net;
using System.Net.Mail;

namespace orchid_backend_net.Infrastructure.Repository
{
    public class EmailSender(IOptions<GmailOptions> options) : IEmailSender
    {
        public async Task SendEmailAsync(string recipient, string subject, string body)
        {
            MailMessage message = new()
            {
                From = new MailAddress(options.Value.Email),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            message.To.Add(recipient);

            using var smptClient = new SmtpClient();
            smptClient.Host = options.Value.Host;
            smptClient.Port = options.Value.Port;
            smptClient.Credentials = new NetworkCredential(options.Value.Email, options.Value.Password);
            smptClient.EnableSsl = true;

            await smptClient.SendMailAsync(message);
        }
    }
}
