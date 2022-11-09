﻿using Botex.View;
using Botex.ViewModel;
using OpenQA.Selenium.DevTools.V104.Network;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Controls;

namespace Botex.scripts
{
     class InputAnalize
    {
        //Wszystko związane z analizą inputu
        private static string helpString = "Aby wywołać funkcję Botex, prosze wpisać polecenia.";
        private static string avOptionsString = "Otworz notatnik \nOtworz Instagram\nOtworz mail\nOtworz tweeter\nOtworz zaloguj";
        private static List<string> createdObjects = new List<string>(); // zapisywanie która opcja została wybrana wczesniej

        public  NotepadVM notepadvm;

        public  InputAnalize(RichTextBox botexAnswerBox)
        {
            notepadvm = new NotepadVM(botexAnswerBox);
        }
        //private static NotepadVM notepadvm = new NotepadVM(botexAnswerBox);

        public  void analizeInput(string input, RichTextBox botexAnswerBox)
        {
            //Wywoływanie fukncji na podstawie wpisanego tekstu 
            
            if (MainBotexView.analized == false)
            {
                if (input.ToUpper().Split(' ').Contains("OTWORZ") && input.ToUpper().Split(' ').Contains("NOTATNIK"))
                {
                    createdObjects.Add("NOTATNIK");
                    //tutaj obsługa inputu do notepada

                }
                if (input.ToUpper().Split(' ').Contains("OTWORZ") && input.ToUpper().Split(' ').Contains("INSTAGRAM"))
                {
                    //wywołaj instagram
                }
                if (input.ToUpper().Split(' ').Contains("OTWORZ") && input.ToUpper().Split(' ').Contains("MAIL") || input.ToUpper().Split(' ').Contains("E-MAIL"))
                {
                    //wywołaj mail
                }
                if (input.ToUpper().Split(' ').Contains("OTWORZ") && input.ToUpper().Split(' ').Contains("TWEETER") || input.ToUpper().Split(' ').Contains("TWETER"))
                {
                    //wywołaj tweeter
                }
                if (input.ToUpper().Split(' ').Contains("OTWORZ") && input.ToUpper().Split(' ').Contains("ZALOGUJ") || input.ToUpper().Split(' ').Contains("LOGOWANIE"))
                {
                    //wywołaj logowaniie
                }
                if (input.ToUpper().Split(' ').Contains("NAPISZ"))
                {
                    RichTextBoxDataChanging.changeTextRichAnswerBox(input, botexAnswerBox);

                }
                if (input.ToUpper().Split(' ').Contains("POMOC") && input.ToUpper().Split(' ').Contains("'POMOC'"))
                {
                    RichTextBoxDataChanging.changeTextRichAnswerBox(helpString, botexAnswerBox);
                    RichTextBoxDataChanging.changeTextRichAnswerBoxWithoutClear(avOptionsString, botexAnswerBox);

                }
            }
            else
            {
                //przekierowanie do odpowiedniej metody w utworzonym obiekcie
                switch(createdObjects)
                {
                    case var _ when createdObjects.Contains("NOTATNIK"):
                        if(input.ToUpper().Split(' ').Contains("stworz"))
                        {
                            notepadvm.InsertMsgToDb(input, input, 1);
                        }
                        break;
                }
                
                
            }
        }

/*
        private static void deleteUsedObjects()
        {
            switch(createdObjects)
            {
                case var _ when createdObjects.Contains("NOTATNIK"):
                    NotepadVM 
                    break;

            }
        }
        
    }
*/

}
