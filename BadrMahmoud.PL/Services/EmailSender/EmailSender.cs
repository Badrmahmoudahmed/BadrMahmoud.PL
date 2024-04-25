using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BadrMahmoud.PL.Services.EmailSender
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendAsync(string from, string reciptients, string body)
        {
            var SenderEmail = _configuration["EmailSetting : EmailSender"];
            var SenderPassword = _configuration["EmailSetting : EmailPassword"];
            var emailmsg = new MailMessage();
            emailmsg.From = new MailAddress(from);
            emailmsg.To.Add(reciptients);
            emailmsg.Body = body;
            emailmsg.IsBodyHtml = false;

            var smtpclient = new SmtpClient(_configuration["Emailhost : EmailPassword"],  int.Parse(_configuration["Emailhost : Emailport"]));
            await smtpclient.SendMailAsync(emailmsg);

        }
    }
}
