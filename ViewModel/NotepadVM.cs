﻿using Botex.database;
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

        private static readonly string welcomeMsg = "Uruchomiono notatnik\nWpisz 'wczytaj' aby wczytac notatke\nwpisz 'stworz' aby stworzyc notatke";

        public NotepadVM(RichTextBox targetRichTextBox)
        {
            RichTextBoxDataChanging.changeTextRichAnswerBox(welcomeMsg, targetRichTextBox);

        }

        
        /*
        static void PrintDefaultMsg(RichTextBox targetRichTextBox)
        {
            RichTextBoxDataChanging.changeTextRichAnswerBox(welcomeMsg, targetRichTextBox);

        }
        */

        public void InsertMsgToDb(string msg, string title, int userId)
        {
            ToDbControl.ToDbNotepad(msg, title, userId);
        }

    }
}