using Microsoft.Extensions.Configuration;
using School.Data.Helpers;
using School.Service.IService;
using System.Net;
using System.Net.Mail;


namespace School.Service.ServiceImp
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> SendEmailAsync(string ToEmail, string Subject, string Body, bool IsBodyHtml = false)
        {
            try
            {
                EmailSettings email = new EmailSettings();
                _configuration.Bind("EmailSettings", email);

                string MailServer = email.MailServer;
                string FromEmail = email.FromEmail;
                string Password = email.Password;
                int Port = int.Parse(email.MailPort);

                var client = new SmtpClient(MailServer, Port)
                {
                    Credentials = new NetworkCredential(FromEmail, Password),
                    EnableSsl = true,
                };
                MailMessage mailMessage = new MailMessage(FromEmail, ToEmail, Subject, Body)
                {
                    IsBodyHtml = IsBodyHtml
                };
                client.SendMailAsync(mailMessage);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }

}
