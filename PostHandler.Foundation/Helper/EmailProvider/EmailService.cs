using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PostHandler.Foundation.Helper
{
    public class EmailService : IMessageService
    {
        private readonly string CredentialUserName = string.Empty;
        private readonly string SentFrom = string.Empty;
        private readonly string Pwd = string.Empty;
        private readonly string EmailSMTP = string.Empty;
        private readonly int EmailSMTPPORT = 0;

        public EmailService(string credentialUsername, string password, string emailSmtpHost, int smtpPort)
        {
            CredentialUserName = credentialUsername;
            Pwd = password;
            EmailSMTP = emailSmtpHost;
            EmailSMTPPORT = smtpPort;
        }

        public EmailService()
        {
        }

        #region Factory Implemenation
        public static EmailService Create(string credentialUsername, string password, string emailSmtpHost, int smtpPort)
        {
            return new EmailService(credentialUsername, password, emailSmtpHost, smtpPort);
        }

        public static EmailService Create()
        {
            return new EmailService();
        }
        #endregion

        public async Task SendAsync(Message message)
        {
            await configSMTPasync(message);
        }

        public async Task SendAsync(Message message, string attachmentpath, string logAttachmentPath)
        {
            await configSMTPasync(message, attachmentpath, logAttachmentPath);
        }

        private async Task configSMTPasync(Message message)
        {
            using (var client = new SmtpClient(EmailSMTP, EmailSMTPPORT))
            {
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(CredentialUserName, Pwd);
                var mailmessage = new MailMessage(
                    CredentialUserName,
                    message.To,
                    message.Subject,
                    message.Body
                );
                try
                {
                    mailmessage.IsBodyHtml = true;
                    await client.SendMailAsync(mailmessage);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private async Task configSMTPasync(Message message, string attachmentpath, string logAttachmentPath)
        {
            using (var client = new SmtpClient(EmailSMTP, EmailSMTPPORT))
            {
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(CredentialUserName, Pwd);
                var mailmessage = new MailMessage(
                    CredentialUserName,
                    message.To,
                    message.Subject,
                    message.Body
                );
                Attachment att = new Attachment(attachmentpath);
                if (logAttachmentPath != "")
                {
                    Attachment attach = new Attachment(logAttachmentPath);
                    mailmessage.Attachments.Add(attach);
                }
                att.ContentDisposition.Inline = true;
                try
                {
                    mailmessage.IsBodyHtml = true;
                    mailmessage.Attachments.Add(att);
                    //Common.Common.Log("Inside Email");
                    await client.SendMailAsync(mailmessage);
                }
                catch (Exception ex)
                {
                    //Common.Common.Log(e.Message);
                    throw;
                }

            }
        }
    }
}