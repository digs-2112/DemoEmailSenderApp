using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using DemoEmailSender.Models;

namespace DemoEmailSender.Services
{
    public class EmailService
    {
        public static bool SendEmail(SendEmailViewModel model)
        {
            bool isSend = false;
            try
            {
                System.Net.Mail.MailMessage mailMessage = new MailMessage();
                
                string[] _ToEmails = model.To.Split(new char[] { ',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                
                foreach (var email in _ToEmails)
                {
                    mailMessage.To.Add(new MailAddress(email));
                }

                if (!string.IsNullOrEmpty(model.CC))
                {
                    string[] _emails = model.CC.Split(new char[] { ',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var email in _emails)
                    {
                        mailMessage.CC.Add(new MailAddress(email));     
                    }
                }
                if (!string.IsNullOrEmpty(model.BCC))
                {
                    string[] _emails = model.BCC.Split(new char[] { ',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var email in _emails)
                    {
                        mailMessage.Bcc.Add(new MailAddress(email));
                    }
                }
                
                mailMessage.Body = model.EmailContent;
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding  = Encoding.UTF8;
                mailMessage.Subject = model.Subject;
                
                var folderPath = HttpContext.Current.Server.MapPath("~/MediaFiles");
                var files = System.IO.Directory.GetFiles(folderPath);
                
                foreach (var file in files)
                {
                    if (model.IsCSV && Path.GetExtension(file).ToLower() == ".csv")
                    {
                        Attachment attachment = new Attachment(file);
                        mailMessage.Attachments.Add(attachment);    
                    }

                    if (model.IsXLS && (Path.GetExtension(file).ToLower() == ".xls" || Path.GetExtension(file).ToLower() == ".xlsx"))
                    {
                        Attachment attachment = new Attachment(file);
                        mailMessage.Attachments.Add(attachment);    
                    }

                    if (model.IsPDF && Path.GetExtension(file).ToLower() == ".pdf")
                    {
                        Attachment attachment = new Attachment(file);
                        mailMessage.Attachments.Add(attachment);    
                    }
                    
                }

                SmtpClient smtpClient = new SmtpClient();
                
                smtpClient.Send(mailMessage);
                isSend = true;
            }
            catch (Exception e)
            {
                isSend = false;
            }

            return isSend;
        }
    }
}