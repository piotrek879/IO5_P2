using Botex.View;
using Botex.ViewModel;
using OpenQA.Selenium.DevTools.V104.Network;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;

namespace Botex.scripts
{
    class InputAnalize
    {
        //Wszystko związane z analizą inputu
        private static string helpString = "Aby wywołać funkcję Botex, prosze wpisać polecenia.";
        private static string avOptionsString = "Otworz notatnik \nOtworz Instagram\nOtworz mail\nOtworz tweeter";
        private static List<string> createdObjects = new List<string>(); // zapisywanie która opcja została wybrana wczesniej

        private static bool IsTitleIncluded = false;
        private static bool IsAskedForTitle = false;
        private static int LoadOrCreateOption; // 1 - stworz  2 - wczytaj
        private static string title;


        public NotepadVM notepadvm;

        public InputAnalize(RichTextBox botexAnswerBox)
        {
            //notepadvm = new NotepadVM(MainBotexView.myRespodRichTextBox);
        }

        public bool analizeInput(string input, RichTextBox botexAnswerBox, int userId)
        {
            //Wywoływanie fukncji na podstawie wpisanego tekstu 

            if (MainBotexView.analized == false)
            {
                if (input.ToUpper().Split(' ').Contains("OTWORZ") && input.ToUpper().Split(' ').Contains("NOTATNIK"))
                {
                    createdObjects.Add("NOTATNIK");
                    notepadvm = new NotepadVM();
                    MainBotexView.analized = true;
                    return true;
                    //tutaj obsługa inputu do notepada
                    //RichTextBoxDataChanging.changeTextRichAnswerBox("Otwieram notatnik ", botexAnswerBox);

                }
                if (input.ToUpper().Split(' ').Contains("OTWORZ") && input.ToUpper().Split(' ').Contains("INSTAGRAM"))
                {
                    Botex.View.IgView IgViewWindow = new Botex.View.IgView();
                    IgViewWindow.Show();
                    return true;
                }
                if (input.ToUpper().Split(' ').Contains("OTWORZ") && input.ToUpper().Split(' ').Contains("MAIL") || input.ToUpper().Split(' ').Contains("E-MAIL"))
                {
                    //wywołaj mail
                    return true;
                }
                if (input.ToUpper().Split(' ').Contains("OTWORZ") && input.ToUpper().Split(' ').Contains("TWEETER") || input.ToUpper().Split(' ').Contains("TWETER"))
                {
                    //wywołaj tweeter
                    return true;
                }
                /*
                if (input.ToUpper().Split(' ').Contains("OTWORZ") && input.ToUpper().Split(' ').Contains("ZALOGUJ") || input.ToUpper().Split(' ').Contains("LOGOWANIE"))
                {
                    //wywołaj logowaniie
                    return true;
                }
                */
                if (input.ToUpper().Split(' ').Contains("NAPISZ"))
                {
                    RichTextBoxDataChanging.changeTextRichAnswerBox(input, botexAnswerBox);
                    return true;

                }
                if (input.ToUpper().Split(' ').Contains("POMOC") || input.ToUpper().Split(' ').Contains("'POMOC'"))
                {
                    RichTextBoxDataChanging.changeTextRichAnswerBox(helpString, botexAnswerBox);
                    RichTextBoxDataChanging.changeTextRichAnswerBoxWithoutClear(avOptionsString, botexAnswerBox);
                    return true;

                }
            }
            else
            {
                //przekierowanie do odpowiedniej metody w utworzonym obiekcie
                switch (createdObjects)
                {
                    case var _ when createdObjects.Contains("NOTATNIK"):
                        
                        if (IsTitleIncluded == true)
                        {
                            if(LoadOrCreateOption == 1)
                            {
                                notepadvm.InsertMsgToDb(input, title, userId);
                                TextBoxDataChanging.textBoxClear(MainBotexView.myInputTextBox);
                                RichTextBoxDataChanging.changeTextRichAnswerBox("Pomyślnie utworzono notatkę", botexAnswerBox);
                                RichTextBoxDataChanging.changeTextRichAnswerBoxWithoutClear(helpString, botexAnswerBox);
                                RichTextBoxDataChanging.changeTextRichAnswerBoxWithoutClear(avOptionsString, botexAnswerBox);
                            }

                        }
                        else
                        {
                            if (IsAskedForTitle == false)
                            {
                                if(input.ToUpper().Split(' ').Contains("STWORZ") || input.ToUpper().Split(' ').Contains("'STWORZ'"))
                                {
                                    LoadOrCreateOption = 1;
                                }
                                else if(input.ToUpper().Split(' ').Contains("WCZYTAJ") || input.ToUpper().Split(' ').Contains("'WCZYTAJ'"))
                                {
                                    LoadOrCreateOption = 2;
                                }
                                else
                                {
                                    TextBoxDataChanging.textBoxClear(MainBotexView.myInputTextBox);
                                    RichTextBoxDataChanging.changeTextRichAnswerBox("Wprowadzono złą wartość\nProsze wpisac ponownie\n", botexAnswerBox);
                                    notepadvm.PrintDefaultMsgWithoutClear(botexAnswerBox);
                                    
                                    return false;
                                }
                                TextBoxDataChanging.textBoxClear(MainBotexView.myInputTextBox);
                                RichTextBoxDataChanging.changeTextRichAnswerBox("Podaj tytuł wiadomości: ", botexAnswerBox);
                                IsAskedForTitle = true;
                            }
                            else
                            {
                                RichTextBoxDataChanging.changeTextRichAnswerBox("Podaj treść wiadomości: ", botexAnswerBox);
                                title = input;
                                IsTitleIncluded = true;
                                if (LoadOrCreateOption == 2)
                                {
                                    notepadvm.ReadMsgFromDb(title, userId , botexAnswerBox);
                                    createdObjects.Clear();
                                    return true;
                                }
                                
                            }
                        }
                        break;
                }
            }
            return true;
        }
    }
}
