using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HitachiSolutions
{
    public class SMTPClient
    {
        private SmtpClient mailServer;
        public SMTPClient() 
        {
            mailServer = new SmtpClient("smtp.office365.com", 587); 
            mailServer.EnableSsl = true;
        } 

        public bool SendMail(string senderMail, string senderPassWord, string receiverMail, string filePath)
        {
            try
            {
                mailServer.Credentials = new System.Net.NetworkCredential(senderMail, senderPassWord);

                MailMessage msg = new MailMessage(senderMail, receiverMail);
                msg.Subject = "Space report";
                msg.Body = "The csv is attached below :)";
                msg.Attachments.Add(new Attachment(filePath));
                mailServer.Send(msg);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }  
    }
}
