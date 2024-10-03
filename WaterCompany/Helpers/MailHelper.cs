using Microsoft.Extensions.Configuration;
using MimeKit;
using System;

namespace WaterCompany.Helpers
{
    public class MailHelper : IMailHelper
    {
        private readonly IConfiguration _configuration;

        public MailHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Response SendEmail(string to, string subject, string body)
        {
            var nameFrom = _configuration["Mail:NameFrom"];
            var From = _configuration["Mail:From"];
            var Smtp = _configuration["Mail:Smtp"];
            var Port = _configuration["Mail:Port"];
            var Password = _configuration["Mail:Password"];

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(nameFrom, From));
            message.To.Add(new MailboxAddress(to, to));
            message.Subject = subject;

            var Bodybuilder = new BodyBuilder
            {
                HtmlBody = body,
            };
            message.Body = Bodybuilder.ToMessageBody();

            try
            {
                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Connect(Smtp, int.Parse(Port), false);
                    client.Authenticate(From, Password);
                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.ToString()
                };
            }

            return new Response
            {
                IsSuccess = true
            };

        }
    }
}
