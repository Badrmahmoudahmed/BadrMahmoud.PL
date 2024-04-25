using Microsoft.Extensions.Configuration;
using System.Net;
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
        public async Task SendAsync(string from, string reciptients,string subject , string body)
        {
            var SenderEmail = "badrmahmoud201312@gmail.com";
            var SenderPassword = "icfa xyjx ftwm vjvx";
            var emailmsg = new MailMessage();
            emailmsg.From = new MailAddress(from);
            emailmsg.To.Add(reciptients);
            emailmsg.Subject = subject;
            emailmsg.Body = body;
            emailmsg.IsBodyHtml = false;

            var smtpclient = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(SenderEmail, SenderPassword),
                EnableSsl = true,
            };
            await smtpclient.SendMailAsync(emailmsg);

        }
    }
}
