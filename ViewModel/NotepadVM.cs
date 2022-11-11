using Botex.database;
using Botex.scripts;
using Botex.View;
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

        private static readonly string welcomeMsg = "Uruchomiono notatnik\nWpisz 'wczytaj' aby wczytac notatke\nwpisz 'stworz' aby stworzyc notatke";
        
        public NotepadVM()
        {
            //RichTextBoxDataChanging.changeTextRichAnswerBox(welcomeMsg, MainBotexView.myRespodRichTextBox);
            PrintDefaultMsg(MainBotexView.myRespodRichTextBox);
            TextBoxDataChanging.textBoxClear(MainBotexView.myInputTextBox);

        }

      
        
        
        
        private void PrintDefaultMsg(RichTextBox targetRichTextBox)
        {
            RichTextBoxDataChanging.changeTextRichAnswerBox(welcomeMsg, targetRichTextBox);

        }

        public void PrintDefaultMsgWithoutClear(RichTextBox targetRichTextBox)
        {
            RichTextBoxDataChanging.changeTextRichAnswerBoxWithoutClear(welcomeMsg, targetRichTextBox);
        }
        

        public void InsertMsgToDb(string msg, string title, int userId)
        {
            ToDbControl.ToDbNotepad(msg, title, userId);
        }

        public void ReadMsgFromDb(string title, int idUser, RichTextBox botexAnswerBox)
        {
            ToDbControl.FromDbNotepad(title, idUser, botexAnswerBox);
        }
    }
}
