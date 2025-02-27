using AlphaManagement.DAL;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.Domain.EmailService.interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace AlphaManagement.Domain.EmailService
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly IConfiguration _configuration;
        private readonly AlphaDbContext _context;

        public EmailSenderService(IConfiguration configuration, AlphaDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task SendEmail(string mailTo, string subject, string message)
        {
            try
            {
                string userName = _configuration["Email:Email"];
                string displayName = _configuration["Email:UserName"];
                string password = _configuration["Email:Password"];
                string host = _configuration["Email:Host"];
                int port = int.Parse(_configuration["Email:Port"]);
                string mailFrom = _configuration["Email:Email"];
                using (var client = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = userName,
                        Password = password
                    };
                    client.Credentials = credential;
                    client.Host = host;
                    client.Port = port;
                    //client.UseDefaultCredentials = false;
                    client.EnableSsl = true;
                    using (var emailMessage = new MailMessage())
                    {
                        emailMessage.To.Add(new MailAddress(mailTo));
                        emailMessage.From = new MailAddress(mailFrom, displayName);
                        emailMessage.Subject = subject;
                        emailMessage.Body = message;
                        emailMessage.IsBodyHtml = true;
                        client.Send(emailMessage);
                    }
                }
                await Task.CompletedTask;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }


        public async Task SendEmailWithFrom(string mailTo, string name, string subject, string message)
        {
            string userName = _configuration["Email:Email"];
            string password = _configuration["Email:Password"];
            string host = _configuration["Email:Host"];
            int port = int.Parse(_configuration["Email:Port"]);
            string mailFrom = _configuration["Email:Email"];
            using (var client = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = userName,
                    Password = password
                };

                client.Credentials = credential;
                client.Host = host;
                client.Port = port;
                client.EnableSsl = true;

                using (var emailMessage = new MailMessage())
                {
                    emailMessage.To.Add(new MailAddress(mailTo));
                    emailMessage.From = new MailAddress(mailFrom,name);
                    emailMessage.Subject = subject;
                    emailMessage.Body = message;
                    emailMessage.IsBodyHtml = true;
                    client.Send(emailMessage);
                }
            }
            await Task.CompletedTask;
        }

        public async Task<bool> SaveMailLog(MailLog mailLog)
        {
            if (mailLog.Id != 0)
                _context.MailLogs.Update(mailLog);
            else
                _context.MailLogs.Add(mailLog);

            return 1 == await _context.SaveChangesAsync();
        }
    }
}
