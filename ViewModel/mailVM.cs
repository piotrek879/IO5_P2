using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using static System.Net.Mime.MediaTypeNames;
using MimeKit;
using InstagramApiSharp.Classes.Models;
using Botex.database;
using Botex.Model;
using Botex.scripts;
using Botex.View;
using System.Windows.Controls;

namespace Botex.ViewModel
{
    internal class mailVM
    {
        private static readonly string welcomeMsg = "Uruchomiono Main\nWpisz 'wczytaj' aby wczytac main\nwpisz 'stworz' aby stworzyc mail\nwpisz 'wyslij' aby wyslac recznie";

        public mailVM()
        {

            PrintDefaultMsg(MainBotexView.myRespodRichTextBox);
            TextBoxDataChanging.textBoxClear(MainBotexView.myInputTextBox);

        }

        private void PrintDefaultMsg(RichTextBox targetRichTextBox)
        {
            RichTextBoxDataChanging.changeTextRichAnswerBox(welcomeMsg, targetRichTextBox);
        }

        public void saveMailToDb(string userId, string subject, string body, string group)
        {
            ToDbControl.ToDbMail(userId, subject, body, group);
        }

        public void sendMailFromDb(string user, string password, string fromMail, string toMail, string subject, string group)
        {
            MailModel mailModel = new MailModel();
            mailModel = this.getMailFromDb( subject, group);
            sendMail(user, password, fromMail, toMail, mailModel.Title, mailModel.Content);
        }
        private MailModel getMailFromDb(string subject, string group)
        {
           return ToDbControl.FromDbMail( subject, group);
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
