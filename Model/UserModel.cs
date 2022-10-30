using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botex.Model
{
    internal class UserModel
    {
        private int idUzytkownika;
        private string login;
        private string haslo;

        public int IdUzytkownika { get { return idUzytkownika; } set { idUzytkownika = value; } }
        public string Login { get { return login; } set { login = value; } }
        public string Haslo { get { return haslo; } set { haslo = value; } }
    }
}
