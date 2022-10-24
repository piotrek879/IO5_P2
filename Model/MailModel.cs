using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botex.Model
{
    internal class MailModel
    {
        private int idMail;
        private string content;
        private string title;
        private string group;

        public int IdMail { get { return idMail; } set { idMail = value; } }
        public string Content { get { return content; } set { content = value; } }
        public string Title { get { return title; } set { title = value; } }
        public string Group { get { return group; } set { group = value; } }
    }
}
