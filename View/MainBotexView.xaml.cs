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



        public MainBotexView()
        {
           
            InitializeComponent();
            myRespodRichTextBox = (RichTextBox)this.FindName("botexAnswerBox");
            myInputTextBox = (TextBox)this.FindName("botexInputBox");
            inputAnalize = new InputAnalize(botexAnswerBox);
            
            RichTextBoxDataChanging.changeTextRichAnswerBox("Witam w Botex", botexAnswerBox);
            RichTextBoxDataChanging.changeTextRichAnswerBoxWithoutClear("Wprowadz dane autoryzacyjne",botexAnswerBox);
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            inputAnalize.analizeInput(botexInputBox.Text.ToString(), botexAnswerBox);
    
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

    }
}
