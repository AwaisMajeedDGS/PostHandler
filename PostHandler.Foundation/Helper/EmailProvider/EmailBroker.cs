using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using System.Net.Mail;
using PostHandler.Foundation.Helper;
using PostHandler.Foundation.Configurations;

namespace PostHandler.Foundation.Helper
{
    public enum EmailType
    {
        PasswordReset,
        SignUp,
        BugReport,
        VoiceMail,
        ExceptionHandling
    }

    public class EmailBroker
    {
        private readonly IMessageService MessagingService;
        protected APISettings _resourceApiSettings;

        public EmailBroker()
        {
            _resourceApiSettings = APIConfigurationManager.Current.APISettings;
            MessagingService = EmailService.Create(_resourceApiSettings.NCUserName,
                _resourceApiSettings.NCPassword,
                _resourceApiSettings.SMTPHost,
                _resourceApiSettings.SMTPPort);
        }

        public static EmailBroker Create()
        {
            return new EmailBroker();
        }

        public async Task SendEmail(EmailBrokerDTO broker, EmailType emailType)
        {
            switch (emailType)
            {
                case EmailType.SignUp:
                    await SendSignUpEmail(broker);
                    return;
                case EmailType.PasswordReset:
                    await ForgotPasswordEmail(broker);
                    return;
                default:
                    return;
            }
        }

        private async Task SendSignUpEmail(EmailBrokerDTO broker)
        {

            var body = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + broker.TemplatePath);
            body = body.Replace("[userName]", broker.Name);
            body = body.Replace("[userEmail]", broker.Email);
            body = body.Replace("[password]", broker.Password);
            body = body.Replace("[role]", broker.UserRole);
            body = body.Replace("[SMSPortalUrl]", _resourceApiSettings.SMSportal);
            await MessagingService.SendAsync(new Message()
            {
                Body = body,
                Subject = broker.EmailSubject,
                To = broker.Email
            });

            return;
        }


        private async Task ForgotPasswordEmail(EmailBrokerDTO broker)
        {
            var body = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + broker.TemplatePath);
            body = body.Replace("[userName]", broker.Name);
            body = body.Replace("[userEmail]", broker.Email);
            body = body.Replace("[code]", broker.EmailVerificationCode);
            await MessagingService.SendAsync(new Message()
            {
                Body = body,
                Subject = broker.EmailSubject,
                To = broker.Email
            });

            return;
        }
    }

    public class EmailBrokerDTO
    {

        public EmailBrokerDTO()
        {

        }

        public EmailBrokerDTO(string userName, string userEmail, string password, string userrole, string subject, string templatePath)
        {
            Name = userName;
            Email = userEmail;
            Password = password;
            EmailSubject = subject;
            TemplatePath = templatePath;
            // UserName = username;
            UserRole = userrole;
        }

        public EmailBrokerDTO(string userName, string userEmail, string code, string subject, string templatePath)
        {
            Name = userName;
            Email = userEmail;
            EmailSubject = subject;
            TemplatePath = templatePath;
            EmailVerificationCode = code;
        }
        public EmailBrokerDTO(string emailTo, string subject, string description, string username, string templatepath, string imageAttachmentPath, string filename)
        {
            Email = emailTo;
            Subject = subject;
            ImageAttachmentPath = imageAttachmentPath;
            TemplatePath = templatepath;
            Description = description;
            UserName = username;
            Filename = filename;
        }

        #region factory Implementation

        public static EmailBrokerDTO Create()
        {
            return new EmailBrokerDTO();
        }

        public static EmailBrokerDTO Create(string userName, string userEmail, string password, string userrole, string subject, string templatePath)
        {
            return new EmailBrokerDTO(userName, userEmail, password, userrole, subject, templatePath);
        }

        public static EmailBrokerDTO Create(string userName, string userEmail, string code, string subject, string templatePath)
        {
            return new EmailBrokerDTO(userName, userEmail, code, subject, templatePath);
        }

        public static EmailBrokerDTO Create(string emailTo, string subject, string description, string username, string templatepath, string imageAttachmentPath, string filename)
        {
            return new EmailBrokerDTO(emailTo, subject, description, username, templatepath, imageAttachmentPath, filename);
        }

        #endregion

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string EmailVerificationCode { get; set; }
        public string TemplatePath { get; set; }
        public string UserRole { get; set; }
        public string EmailSubject { get; set; }
        public string Subject { get; set; }
        public string ImageAttachmentPath { get; set; }
        public string Filename { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
    }
}
