using Botex.database;
using Botex.scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Botex.ViewModel
{
    internal class NotepadVM
    {
        private  readonly DbControl dbControl = new DbControl();
        private static readonly string welcomeMsg = "Uruchomiono notatnik\nWpisz 'wczytaj' aby wczytac notatke\nwpisz 'stworz' aby stworzyc notatke";

        static void PrintDefaultMsg(RichTextBox targetRichTextBox)
        {
            RichTextBoxDataChanging.changeTextRichAnswerBox(welcomeMsg, targetRichTextBox);

        }

        static void 
    }
}
