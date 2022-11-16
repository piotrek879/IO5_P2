using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using static System.Net.Mime.MediaTypeNames;
using MimeKit;
using InstagramApiSharp.Classes.Models;

namespace Botex.ViewModel
{
    internal class mailVM
    {

        public mailVM()
        {
            
        }

        public void saveMailToDb(string userId, string subject, string body, string group)
        {

        }

        public void getMailFromDb(string userId, string subject)
        {

        }

        public void sendMail(string user,string password, string fromMail, string toMail, string subject, string body)
        {
            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress(user, fromMail));
            mailMessage.To.Add(new MailboxAddress("", toMail));
            mailMessage.Subject = subject;
            mailMessage.Body = new TextPart("plain")
            {
                Text = body
            };

            using (var smtpClient = new MailKit.Net.Smtp.SmtpClient())
            {

                smtpClient.Connect("smtp.gmail.com", 587, true);
                smtpClient.Authenticate(user, password);
                smtpClient.Send(mailMessage);
                smtpClient.Disconnect(true);
            }
        }
         
    }
}
