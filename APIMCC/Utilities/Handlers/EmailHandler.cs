using APIMCC.Contracts;
using System.Net.Mail;

namespace APIMCC.Utilities.Handlers
{
    public class EmailHandler : IEmailHandler
    {
        private readonly string _smptpServer;
        private readonly int _smptpPort;
        private readonly string _fromEmailAddress;

        public EmailHandler(string smptpServer, int smptpPort, string fromEmailAddress)
        {
            _smptpServer = smptpServer;
            _smptpPort = smptpPort;
            _fromEmailAddress = fromEmailAddress;
        }

        public void SendEmail(string toEmail, string subject, string htmlMessage)
        {
            var message = new MailMessage
            {
                From = new MailAddress(_fromEmailAddress),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };
            message.To.Add(new MailAddress(toEmail));

            using var client = new SmtpClient(_smptpServer,_smptpPort);
            client.Send(message);
        }
    }
}
