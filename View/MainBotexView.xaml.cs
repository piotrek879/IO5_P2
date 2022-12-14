using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Botex.scripts;
using System.Speech;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Threading;
using System.Windows.Navigation;
using Botex.ViewModel;
using System.Runtime.CompilerServices;

namespace Botex.View
{
    /// <summary>
    /// Logika interakcji dla klasy MainBotexView.xaml
    /// </summary>
    public partial class MainBotexView : Window
    {

        public static bool analized = false;

        SpeechSynthesizer ss = new SpeechSynthesizer();
        PromptBuilder pb = new PromptBuilder();
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();
        Choices clist = new Choices();



        private InputAnalize inputAnalize;

        public static RichTextBox myRespodRichTextBox;
        public static TextBox myInputTextBox;
        private  UserVM currentLoggedUser;
        public int LoggedUserId;


        public MainBotexView()
        {
            InitializeComponent();
            currentLoggedUser = new UserVM();
            myRespodRichTextBox = (RichTextBox)this.FindName("botexAnswerBox");
            myInputTextBox = (TextBox)this.FindName("botexInputBox");
            inputAnalize = new InputAnalize(botexAnswerBox);

            RichTextBoxDataChanging.changeTextRichAnswerBox("Witam w Botex", botexAnswerBox);
            RichTextBoxDataChanging.changeTextRichAnswerBoxWithoutClear("Wprowadz dane autoryzacyjne",botexAnswerBox);
        }

       

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
          inputAnalize.analizeInput(botexInputBox.Text.ToString(), botexAnswerBox, LoggedUserId);
        }
        private void speechToTextBtnClick(object sender, RoutedEventArgs e)
        {
            clist.Add(new string[] { "Hello", "Good Morning", "Welcome", "Thank You" });
            Grammar gr = new Grammar(new GrammarBuilder(clist));
          
            try
            {
                sre.RequestRecognizerUpdate();
                sre.LoadGrammar(gr);
                sre.SpeechRecognized += sre_SpeechRecognized;
                sre.SetInputToDefaultAudioDevice();
                sre.RecognizeAsync(RecognizeMode.Multiple);

            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Eroor"); }
        }
        private void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            botexInputBox.Text = botexInputBox.Text + e.Result.Text.ToString() + Environment.NewLine;
        }

        private void lgnBttn_Click(object sender, RoutedEventArgs e)
        {
            TextBox loginBox = (TextBox)this.FindName("LoginBox");
            PasswordBox passwordBox = (PasswordBox)this.FindName("PasswordBox");
            if (currentLoggedUser.failedAttempsCounter != 3)
            {
                if (currentLoggedUser.userLoginCheck(loginBox.Text.ToString(), passwordBox.Password.ToString()) == true)
                {
                        //Gdy istnieje to ustaw user id;
                    LoggedUserId = currentLoggedUser.UserId;
                    Button LoginBtn = (Button)this.FindName("lgnBttn");
                    Button AckInptBtn = (Button)this.FindName("submitButton");
                    TextBox Inputbox = (TextBox)this.FindName("botexInputBox");

                    //TextBox loginBox = (TextBox)this.FindName("LoginBox");
                    //TextBox passwordBox = (TextBox)this.FindName("PasswordBox");
                    LoginBtn.Visibility = Visibility.Collapsed;
                    loginBox.Visibility = Visibility.Collapsed;
                    passwordBox.Visibility = Visibility.Collapsed;

                    AckInptBtn.IsEnabled = true;
                    Inputbox.IsEnabled = true;
                    inputAnalize.analizeInput("POMOC", botexAnswerBox, LoggedUserId);
                  
                }

            }
            else
            {
                //Gdy 3 błędne dane wyjdz z aplikacji
                this.Close();
            }
        }
    }
}
