using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace  GlampingProyect.Web.Services
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public SmtpEmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var host = _configuration["Smtp:Host"];
            var port = int.Parse(_configuration["Smtp:Port"]!);
            var user = _configuration["Smtp:User"];
            var pass = _configuration["Smtp:Pass"];

            using var client = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(user, pass),
                EnableSsl = true
            };

            var mail = new MailMessage(from: user, to: email, subject: subject, body: htmlMessage)
            {
                IsBodyHtml = true
            };

            await client.SendMailAsync(mail);
        }
    }
}
