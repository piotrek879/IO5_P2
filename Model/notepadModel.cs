using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace Botex.Model
{
    internal class notepadModel
    {
        private int idNotepad;
        private UserModel userId;
        private string date;
        private string content;
        private string title;

        public int IdNotepad { get { return idNotepad; } set { idNotepad = value; } }
        public UserModel User { get { return userId; } set { userId = value; } }
        public string Date { get { return date; } set { date = value; } }
        public string Content { get { return content; } set { content = value; } }
        public string Title { get { return title; } set { title = value; } }
    }
}
