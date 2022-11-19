using Botex.Model;
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

        //Notatnik
        private static bool IsTitleIncluded = false;
        private static bool IsAskedForTitle = false;
        private static int LoadOrCreateOption; // 1 - stworz  2 - wczytaj
        private static string title;


        private NotepadVM notepadvm;

        //Mail
        private static bool IsAskedForAction = false;
        private static bool IsAskedForPassword = false;
        private static bool IsAskedForToMail = false;
        private static bool IsAskedForFromMail = false;

        private static bool IsAskedForSubject = false;
        private static bool IsAskedForGroup = false;
        private static bool IsAskedForMailContent = false;

        private static string user = "Botex ";
        private static string passwd = string.Empty;
        private static string toUserMail = string.Empty;
        private static string fromUserMail = string.Empty;

        private static int IsMailCompleted = 0; // 0 nie 1 zapis 2 wczytanie 3 recznie
        private static int mailAskedToCreateLoadSave; //1-do db 2 - wczytaj 3 - recznie
        private static MailModel mailModel;
        private mailVM mailvm;

        //Twitter
        private TwitterVM twitterVM;
        private static TweetModel tweetmodel;
        //Dane inicjalizacyjne twit
        private static string ck = string.Empty;
        private static string cks = string.Empty;
        private static string at = string.Empty;
        private static string ats = string.Empty;

        private static bool isAskedForCk = false;
        private static bool isAskedForCks = false;
        private static bool isAskedForAt = false;
        private static bool isAskedForAts = false;

        private static bool isAskedForTweeterContent = false;
        private static bool isAskedForTweeterGroup = false;

        private static bool isTweetCompleted = false;
        private static int isAskedForTweeterActionType = 0; // 0 - nie 1 - do db 2- wczytaj 3 -recznie

        //private TwitterView twitterview;

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
                    createdObjects.Add("MAIL");
                    mailvm = new mailVM();
                    mailModel = new MailModel();
                    MainBotexView.analized = true;
                    return true;
                }
                if (input.ToUpper().Split(' ').Contains("OTWORZ") && input.ToUpper().Split(' ').Contains("TWEETER") || input.ToUpper().Split(' ').Contains("TWETER"))
                {
                    //wywołaj tweeter
                    createdObjects.Add("TWEETER");
                    twitterVM = new TwitterVM();
                    tweetmodel = new TweetModel();
                    MainBotexView.analized = true;
                    return true;
                }
                /*
                if (input.ToUpper().Split(' ').Contains("OTWORZ") && input.ToUpper().Split(' ').Contains("KONTA") || input.ToUpper().Split(' ').Contains("KONTA"))
                {
                    //wywołaj logowaniie
                    return true;
                }
                */
               
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
                    #region Notatnik
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
                                    MainBotexView.analized = false;
                                    return true;
                                }
                                else
                                {
                                    notepadvm.InsertMsgToDb(input, title, userId);
                                    createdObjects.Clear();
                                    MainBotexView.analized = false;
                                    return true;
                                }
                                
                            }
                        }
                        break;
                    #endregion
                    #region Mail
                    case var _ when createdObjects.Contains("MAIL"):
                        if (IsMailCompleted == 0)
                        {
                            if (IsAskedForAction == false)
                            {

                                if (input.ToUpper().Split(' ').Contains("STWORZ") || input.ToUpper().Split(' ').Contains("'STWORZ'"))
                                {
                                    mailAskedToCreateLoadSave = 0;
                                }
                                if (input.ToUpper().Split(' ').Contains("WCZYTAJ") || input.ToUpper().Split(' ').Contains("'WCZYTAJ'"))
                                {
                                    mailAskedToCreateLoadSave = 1;
                                }
                                if (input.ToUpper().Split(' ').Contains("RECZNIE") || input.ToUpper().Split(' ').Contains("'RECZNIE'"))
                                {
                                    mailAskedToCreateLoadSave = 2;
                                }
                                RichTextBoxDataChanging.changeTextRichAnswerBox("Wprowadz tytuł: ", botexAnswerBox);
                                TextBoxDataChanging.textBoxClear(MainBotexView.myInputTextBox);
                                IsAskedForAction = true;
                            }
                            else
                            {
                                if(IsAskedForTitle == false)
                                {
                                    mailModel.Title = input;

                                    if (mailAskedToCreateLoadSave != 1)
                                    {
                                        RichTextBoxDataChanging.changeTextRichAnswerBox("Wprowadz treść wiadomości: ", botexAnswerBox);
                                        TextBoxDataChanging.textBoxClear(MainBotexView.myInputTextBox);
                                        //IsAskedForMailContent = true;
                                    }
                                    IsAskedForTitle = true;
                                }
                                else
                                {
                                    if (IsAskedForMailContent == false)
                                    {
                                        mailModel.Content = input;
                                        if (mailAskedToCreateLoadSave != 2)
                                        {
                                            RichTextBoxDataChanging.changeTextRichAnswerBox("Wprowadz grupę wiadomości: ", botexAnswerBox);
                                            TextBoxDataChanging.textBoxClear(MainBotexView.myInputTextBox);
                                        }
                                        else
                                        {
                                            IsAskedForGroup = true;
                                        }
                                        IsAskedForMailContent = true;
                                    }
                                    else
                                    {
                                        if (IsAskedForGroup == false)
                                        {
                                            mailModel.Group = input;
                                            TextBoxDataChanging.textBoxClear(MainBotexView.myInputTextBox);

                                            if (mailAskedToCreateLoadSave == 0)
                                            {
                                                IsMailCompleted = 1;
                                            }
                                            else
                                            {
                                               RichTextBoxDataChanging.changeTextRichAnswerBox("Wprowadz docelowy adres mail wiadomości: ", botexAnswerBox);
                                            }
                                            IsAskedForGroup = true;
                                        }
                                        else
                                        {
                                            if(IsAskedForToMail == false)
                                            {
                                                toUserMail = input;
                                                TextBoxDataChanging.textBoxClear(MainBotexView.myInputTextBox);
                                                RichTextBoxDataChanging.changeTextRichAnswerBox("Wprowadz adres mail wysyłającego wiadomości: ", botexAnswerBox);
                                                IsAskedForToMail = true;
                                            }
                                            else
                                            {
                                                if(IsAskedForFromMail == false)
                                                {
                                                    fromUserMail = input;
                                                    TextBoxDataChanging.textBoxClear(MainBotexView.myInputTextBox);
                                                    RichTextBoxDataChanging.changeTextRichAnswerBox("Wprowadz hasło do konta mail: ", botexAnswerBox);
                                                    IsAskedForFromMail = true;
                                                }
                                                else
                                                {
                                                    if(IsAskedForPassword == false)
                                                    {
                                                        passwd = input;
                                                       if (mailAskedToCreateLoadSave == 2)
                                                        {
                                                            IsMailCompleted=1;
                                                        }
                                                       if(mailAskedToCreateLoadSave == 3)
                                                        {
                                                            IsMailCompleted = 2;
                                                        }
                                                        IsAskedForPassword = true;
                                                    }

                                                }
                                            }
                                        }                               
                                    }
                                }
                            }
                            
                        }
                        else
                        {
                            if (IsMailCompleted ==1)
                            {
                                mailvm.saveMailToDb(userId, mailModel.Title, mailModel.Content, mailModel.Group);
                                RichTextBoxDataChanging.changeTextRichAnswerBox("Pomyślnie zapisano maila do db ", botexAnswerBox);
                                TextBoxDataChanging.textBoxClear(MainBotexView.myInputTextBox);
                                createdObjects.Clear();
                                MainBotexView.analized = false;
                                return true;
                            }
                            if(IsMailCompleted ==2)
                            {
                                mailvm.sendMailFromDb(user + userId.ToString(), passwd, fromUserMail, toUserMail, mailModel.Title, mailModel.Group);
                                RichTextBoxDataChanging.changeTextRichAnswerBox("Pomyślnie wysłano maila z bazy danych ", botexAnswerBox);
                                TextBoxDataChanging.textBoxClear(MainBotexView.myInputTextBox);
                                createdObjects.Clear();
                                MainBotexView.analized = false;
                                return true;
                            }
                            if(IsMailCompleted ==3)
                            {
                                mailvm.sendMail(user + userId.ToString(), passwd, fromUserMail, toUserMail, mailModel.Title, mailModel.Content);
                                RichTextBoxDataChanging.changeTextRichAnswerBox("Pomyślnie wyslano maila ", botexAnswerBox);
                                TextBoxDataChanging.textBoxClear(MainBotexView.myInputTextBox);
                                createdObjects.Clear();
                                MainBotexView.analized = false;
                                return true;
                            }
                        }
                        break;
                    #endregion
                    #region Twitter
                    case var _ when createdObjects.Contains("TWEETER"):
                        if(isTweetCompleted == false)
                        {
                            MessageBox.Show(input.ToString());
                            if (isAskedForTweeterActionType == 0)
                            {
                                
                                if (input.ToUpper().Split(' ').Contains("STWORZ") || input.ToUpper().Split(' ').Contains("'STWORZ'"))
                                {
                                    isAskedForTweeterActionType = 1;
                                }
                                if (input.ToUpper().Split(' ').Contains("WCZYTAJ") || input.ToUpper().Split(' ').Contains("'WCZYTAJ'"))
                                {
                                    isAskedForTweeterActionType = 2;
                                }
                                if (input.ToUpper().Split(' ').Contains("WYSLIJ") || input.ToUpper().Split(' ').Contains("'WYSLIJ'"))
                                {
                                    isAskedForTweeterActionType = 3;
                                }
                                RichTextBoxDataChanging.changeTextRichAnswerBox("Wprowadz customer key: ", botexAnswerBox);
                                TextBoxDataChanging.textBoxClear(MainBotexView.myInputTextBox);
                            }
                            else
                            {
                                if (isAskedForCk == false && isAskedForTweeterActionType != 1)
                                {
                                    ck = input;
                                    RichTextBoxDataChanging.changeTextRichAnswerBox("Wprowadz customer key secret: ", botexAnswerBox);
                                    TextBoxDataChanging.textBoxClear(MainBotexView.myInputTextBox);
                                    isAskedForCk = true;
                                }
                                else
                                {
                                    if (isAskedForCks == false && isAskedForTweeterActionType != 1)
                                    {
                                        cks = input;
                                        RichTextBoxDataChanging.changeTextRichAnswerBox("Wprowadz access token: ", botexAnswerBox);
                                        TextBoxDataChanging.textBoxClear(MainBotexView.myInputTextBox);
                                        isAskedForCks = true;
                                    }
                                    else
                                    {
                                        if(isAskedForAt == false && isAskedForTweeterActionType != 1)
                                        {
                                            at = input;
                                            RichTextBoxDataChanging.changeTextRichAnswerBox("Wprowadz access token secret: ", botexAnswerBox);
                                            TextBoxDataChanging.textBoxClear(MainBotexView.myInputTextBox);
                                            isAskedForAt = true;
                                        }
                                        else
                                        {
                                            if(isAskedForAts == false && isAskedForTweeterActionType != 1)
                                            {
                                                ats = input;
                                                if (isAskedForTweeterActionType == 3)
                                                {
                                                    RichTextBoxDataChanging.changeTextRichAnswerBox("Wprowadz treść tweeta: ", botexAnswerBox);
                                                    TextBoxDataChanging.textBoxClear(MainBotexView.myInputTextBox);
                                                }
                                                if (isAskedForTweeterActionType != 3)
                                                {
                                                    RichTextBoxDataChanging.changeTextRichAnswerBox("Wprowadz grupę tweeta: ", botexAnswerBox);
                                                    TextBoxDataChanging.textBoxClear(MainBotexView.myInputTextBox);
                                                }
                                                isAskedForAts = true;
                                            }
                                            else
                                            {

                                                if(isAskedForTweeterActionType ==3)
                                                {
                                                    tweetmodel.Content = input;
                                                    isTweetCompleted = true;
                                                }
                                                if(isAskedForTweeterGroup == false)
                                                {
                                                    tweetmodel.Group = input;
                                                    if(isAskedForTweeterActionType == 2)
                                                    {
                                                        isTweetCompleted = true;
                                                    }

                                                    isAskedForTweeterGroup = true;
                                                }
                                                else
                                                {
                                                    if(isAskedForTweeterContent == false)
                                                    {
                                                        tweetmodel.Content = input;
                                                        isAskedForTweeterContent = true;
                                                        isTweetCompleted = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            

                            if (isAskedForTweeterActionType == 3)
                            {
                                twitterVM.setTweeterLogData(ck, cks, at, ats);
                                twitterVM.SendTweet(tweetmodel.Content);
                                RichTextBoxDataChanging.changeTextRichAnswerBox("Pomyslnie wyslano tweeta ", botexAnswerBox);
                                createdObjects.Clear();
                                MainBotexView.analized = false;
                                return true;
                            }
                            if (isAskedForTweeterActionType == 2)
                            {
                                twitterVM.setTweeterLogData(ck, cks, at, ats);
                                twitterVM.sendTweetFromDb(tweetmodel.Group);
                                RichTextBoxDataChanging.changeTextRichAnswerBox("Pomyslnie wyslano tweeta z db ", botexAnswerBox);
                                createdObjects.Clear();
                                MainBotexView.analized = false;
                                return true;
                            }
                            if(isAskedForTweeterActionType ==1)
                            {
                                twitterVM.saveTweetToDb(userId, tweetmodel.Content, tweetmodel.Group);
                                RichTextBoxDataChanging.changeTextRichAnswerBox("Pomyslnie zapisano tweeta do db ", botexAnswerBox);
                                createdObjects.Clear();
                                MainBotexView.analized = false;
                                return true;
                            }
                            
                            TextBoxDataChanging.textBoxClear(MainBotexView.myInputTextBox);
                        }
                        break;

                    #endregion

                }
            }
            return true;
        }
    }
}
