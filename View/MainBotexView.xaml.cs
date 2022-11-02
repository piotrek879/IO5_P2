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
namespace Botex.View
{
    /// <summary>
    /// Logika interakcji dla klasy MainBotexView.xaml
    /// </summary>
    public partial class MainBotexView : Window
    {
        public MainBotexView()
        {
            InitializeComponent();
            RichTextBoxDataChanging.changeTextRichAnswerBox("Witam w Botex",botexAnswerBox);
            RichTextBoxDataChanging.changeTextRichAnswerBoxWithoutClear("Wpisz 'pomoc' aby uzyskac pomoc", botexAnswerBox);
            RichTextBoxDataChanging.changeTextRichAnswerBoxWithoutClear("Wprowadz dane autoryzacyjne", botexAnswerBox);
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            InputAnalize.analizeInput(botexInputBox.Text.ToString(), botexAnswerBox);
            Trace.WriteLine(botexInputBox.Text);
        }
    }
}
