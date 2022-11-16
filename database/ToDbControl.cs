using Botex.Model;
using Botex.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Botex.database
{
    internal class ToDbControl
    {

        private static readonly DbControl dbControl = new DbControl();

        public static void ToDbNotepad(string msg, string title, int idUser )
        {
            string myDbQuery = $"INSERT INTO notepad(userId,content,title) VALUES('{idUser}','{msg}','{title}')"; 
            dbControl.insertDataToDB(myDbQuery); 
        }

        public static void ToDbUser(string login, string password, int UserCreatingAccId)
        {
            // dodać potem uprawnienia user-admin
            /*
             *  string myDbQuery = $"SELECT permission FROM user WHERE userId = '{UserCreatingAccId}' ";
            if( getPermissionsFromDb(myDbQuery) == 1) //Admin
            {
            to co juz tu jest nizej
            return true;
            }
            else
            {
                return false;
            }
            
            */

            string myDbQuery = $"INSERT INTO user(login,haslo) VALUES('{login}','{password}')"; 
            dbControl.insertDataToDB(myDbQuery);
        }


        public static void FromDbNotepad(string title, int idUser, RichTextBox botexAnswerBox)
        {
            string myDbQuery = $"SELECT content FROM notepad WHERE title LIKE '{title}' AND userId = '{idUser}' LIMIT 1";
            dbControl.ReadDataFromDB(myDbQuery, botexAnswerBox);
        }

        public static void ToDbMail(string userId, string subject, string body, string group)
        {
            //Potem dodać tu uprawnienia - tylko admin moze zapisać
            if(group == "")
            {
                group = "Bez Grupy"; 
            }
            string myDbQuery = $"INSERT INTO mail(content,title, grup) VALUES('{body}','{subject}','{group}')";
            dbControl.insertDataToDB(myDbQuery);
        }
        public static MailModel FromDbMail(string subject, string group)
        {
            string myDbQuery = $"SELECT idMail,content,title,group FROM mail WHERE title LIKE '{subject}' AND grup LIKE '{group}' LIMIT 1";
            return dbControl.GetMailModelFromDb(myDbQuery);
        }

        public static void ToDbTweet(int userId, string content, string group)
        {
            if (group == "")
            {
                group = "Bez Grupy";
            }
            string myDbQuery = $"INSERT INTO tweeter(content, grup) VALUES('{content}','{group}')";
            dbControl.insertDataToDB(myDbQuery);
        }

        public static TweetModel FromDbTweet(string content, string group)
        {
            string myDbQuery = $"SELECT idTweet,content,group FROM mail WHERE group LIKE '{group}' LIMIT 1";
            return dbControl.GetTweetModelFromDb(myDbQuery);
        }


        public static int FromDbLogin(string login, string passwd )
        {
            string myDbQuery = $"SELECT idUzytkownika FROM users WHERE login LIKE '{login}' AND haslo LIKE '{passwd}' LIMIT 1";
            return dbControl.getIdFromDb(myDbQuery); 
        }
    }
}
// tweeter(idTweet INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, content TEXT, group TEXT )