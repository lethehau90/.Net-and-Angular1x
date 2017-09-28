using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace HauShop.Common
{
   public class MailHelper
    {
        public static bool SendMail(string toEmail, string subject, string content)
        {
            try
            {
                var host = ConfigHelper.GetByKey("SMTPHost");
                var port = int.Parse(ConfigHelper.GetByKey("SMTPPort"));
                var fromEmail = ConfigHelper.GetByKey("FromEmailAddress");
                var password = ConfigHelper.GetByKey("FromEmailPassword");
                var fromName = ConfigHelper.GetByKey("FromName");

                var smtpClient = new SmtpClient(host, port)
                {
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential(fromEmail, password),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    EnableSsl = true,
                    Timeout = 100000
                };

                var mail = new MailMessage
                {
                    Body = content,
                    Subject = subject,
                    From = new MailAddress(fromEmail, fromName)
                };

                mail.To.Add(new MailAddress(toEmail));
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                smtpClient.Send(mail);

                return true;
            }
            catch (SmtpException smex)
            {
               
                return false;
            }
        }
      
      public static void sendmailFile(string toEmail, string cc, string bcc, string subject, string content, string strAttachment)
        {
            var host = "smtp.gmail.com";
            var port = 587;
            var fromEmail = "lethehau90@gmail.com";
            var password = "xxxx";
            var fromName = "LVCRM";

            var smtpClient = new SmtpClient(host, port)
            {
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(fromEmail, password),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true,
                Timeout = 100000
            };

            var mail = new MailMessage
            {
                Body = content,
                Subject = subject,
                From = new MailAddress(fromEmail, fromName)
            };

            mail.BodyEncoding = mail.SubjectEncoding = System.Text.Encoding.UTF8;

            if (toEmail == "") return;

            string[] ToMuliId = toEmail.Split(',');
            foreach (string ToEMailId in ToMuliId)
            {
                mail.To.Add(new MailAddress(ToEMailId)); //adding multiple TO Email Id
            }

            if (cc != "")
            {
                string[] CCId = cc.Split(',');

                foreach (string CCEmail in CCId)
                {
                    mail.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id
                }
            }

            if (bcc != "")
            {
                string[] bccid = bcc.Split(',');

                foreach (string bccEmailId in bccid)
                {
                    mail.Bcc.Add(new MailAddress(bccEmailId)); //Adding Multiple BCC email Id
                }
            }

            if (strAttachment != "")
            {
                //Adding multiple BCC Addresses
                foreach (string sAttachment in strAttachment.Split(",".ToCharArray()))
                {
                    Attachment attachment = new Attachment(sAttachment);
                    mail.Attachments.Add(attachment);
                }
            }

            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;

            try
            {
                smtpClient.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in CreateMessageWithAttachment(): {0}",
                            ex.ToString());
            }

        }
      
      private bool CheckForInternetConnection()
		{
			try
			{
				System.Net.IPHostEntry hostEntry = System.Net.Dns.GetHostEntry("www.google.com");
				return true;
			}
			catch
			{
				return false;
			}
		}
    }
}
