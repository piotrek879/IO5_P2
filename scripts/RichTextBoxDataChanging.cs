using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Botex.scripts
{
    internal class RichTextBoxDataChanging
    {
        //Tutaj będą statyczne metody dla manipulowania zawartością RichTextBox
        public static void clearTextRichAnswerBox(RichTextBox answerBox)
        {
            answerBox.Document.Blocks.Clear();
        }

        public static void changeTextRichAnswerBox(string textString,  RichTextBox answerBox)
        {
            clearTextRichAnswerBox(answerBox);
            answerBox.Document.Blocks.Add(new Paragraph(new Run(textString)));
        }

        public static void changeTextRichAnswerBoxWithoutClear(string textString, RichTextBox answerBox)
        {
            answerBox.Document.Blocks.Add(new Paragraph(new Run(textString)));
        }
    }
}
