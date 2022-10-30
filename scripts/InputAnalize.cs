using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Botex.scripts
{
    internal class InputAnalize
    {
        //Wszystko związane z analizą inputu
        private static string helpString = "Aby wywołać funkcję Botex, prosze wpisać polecenia.";
        private static string avOptionsString = "Otworz notatnik \nOtworz Instagram\nOtworz mail\nOtworz tweeter\nOtworz zaloguj";

        public static void analizeInput(string input, RichTextBox botexAnswerBox)
        {
            //Wywoływanie fukncji na podstawie wpisanego tekstu 
            if (input.ToUpper().Split(' ').Contains("OTWORZ") && input.ToUpper().Split(' ').Contains("NOTATNIK"))
            {
                //wywołaj notatnik
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
            if (input.ToUpper().Split(' ').Contains("POMOC"))
            {
                RichTextBoxDataChanging.changeTextRichAnswerBox(helpString, botexAnswerBox);
                RichTextBoxDataChanging.changeTextRichAnswerBoxWithoutClear(avOptionsString,botexAnswerBox);

            }
        }
    }

}
