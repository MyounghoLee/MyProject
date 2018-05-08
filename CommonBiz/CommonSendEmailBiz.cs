using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using biz.CommonBiz;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace biz.CommonBiz
{
    public class CommonSendEmailBiz
    {
        private static string _server;
        private static int _port;
        private static string _user;
        private static string _password;
        private static bool _useSsl;
        private static bool _requiresAuthentication;
        private static string _preferredEncoding;

        public CommonSendEmailBiz()
        {
            Init();
        }

        private void Init()
        {
            _server = CommonConfigurationBuilderBiz.Configuration.GetSection("email:server").Value;
            _port = Convert.ToInt32(CommonConfigurationBuilderBiz.Configuration.GetSection("email:port").Value);
            _user = CommonConfigurationBuilderBiz.Configuration.GetSection("email:user").Value;
            _password = CommonConfigurationBuilderBiz.Configuration.GetSection("email:password").Value;
            _useSsl = Convert.ToBoolean(CommonConfigurationBuilderBiz.Configuration.GetSection("email:useSsl").Value);
            _requiresAuthentication = Convert.ToBoolean(CommonConfigurationBuilderBiz.Configuration.GetSection("email:requiresAuthentication").Value);
            _preferredEncoding = CommonConfigurationBuilderBiz.Configuration.GetSection("email:preferredEncoding").Value;
        }

        public async Task SendEmailAsync( string to, string from, string subject, string message)
        {
            from = string.IsNullOrEmpty(from) ? _user : from;

            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(from, from));
            emailMessage.To.Add(new MailboxAddress(to, to));
            emailMessage.Subject = subject;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = message;
            emailMessage.Body = bodyBuilder.ToMessageBody();

            using (SmtpClient client = new SmtpClient())
            {
                await client.ConnectAsync(_server, _port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                await client.AuthenticateAsync(_user, _password); //error occurs here

                await client.SendAsync(emailMessage).ConfigureAwait(false);
                await client.DisconnectAsync(true);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            }
        }
    }
}
