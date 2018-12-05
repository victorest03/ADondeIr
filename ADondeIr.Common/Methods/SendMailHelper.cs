using System.Linq;

namespace ADondeIr.Common.Methods
{
    using System.Configuration;
    using System.IO;
    using System.Net.Mail;

    public static class SendMailHelper
    {
        private static readonly string From = ConfigurationManager.AppSettings["FromNameMail"];
        private static readonly string Address = ConfigurationManager.AppSettings["FromAddressMail"];
        public static void SendMail(string[] to, string subject, string body, Stream attached = null, string nameAttached = null)
        {
            var mail = new MailMessage();
            var smtp = new SmtpClient();


            to.ToList().ForEach(f => mail.Bcc.Add(f));
            mail.From = new MailAddress(Address, From);

            mail.Subject = subject;

            mail.Body = body;

            mail.IsBodyHtml = true;
            if (attached != null)
            {
                mail.Attachments.Add(new Attachment(attached, nameAttached ?? "file"));
            }

            smtp.Send(mail);
        }

    }
}
