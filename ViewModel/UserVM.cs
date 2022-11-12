using Botex.database;
using Botex.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botex.ViewModel
{
    internal class UserVM
    {
        public int UserId; // tworzenie "sesji", gdy -1 to uzytkownik nie istnieje/nie zalogowany
        public int failedAttempsCounter;
        public UserVM()
        {
            failedAttempsCounter = 0;
            UserId = -1;
        }

        public bool userLoginCheck(string login, string password)
        {
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
    }
}
