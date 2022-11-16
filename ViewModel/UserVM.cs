using Botex.database;
using Botex.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstagramApiSharp.Classes;
using System.Security.Cryptography;

namespace Botex.ViewModel
{
    internal class UserVM
    {
        public int UserId; // tworzenie "sesji", gdy -1 to uzytkownik nie istnieje/nie zalogowany
        public int failedAttempsCounter;
        
        private static SHA512 shaM = new SHA512Managed();

        public UserVM()
        {
            failedAttempsCounter = 0;
            UserId = -1;
        }

        private string ShaMyString(string myStringToSha)
        {
            return shaM.ComputeHash(Encoding.UTF8.GetBytes(myStringToSha)).ToString();
        }
        public bool userLoginCheck(string login, string password)
        {
            // UserId = ToDbControl.FromDbLogin(login, ShaMyString(password);
            UserId = ToDbControl.FromDbLogin(login, password);
            if (UserId != -1)
            {
                failedAttempsCounter = 0;
                return true;
            }
            else
            {
                failedAttempsCounter++;
               
                return false;
            }
        }

        public void createUserAcc(string login, string password, int UserCreatingAccId)
        {
            //Admin może tworzyć konta
            ToDbControl.ToDbUser(login, ShaMyString(password), UserCreatingAccId);

        }
    }
}
