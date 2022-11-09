using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Botex.scripts
{
    static class TextBoxDataChanging
    {
        //operacje na textboxie (inpucie)
        public static void textBoxClear(TextBox inputBotexBox)
        {
            inputBotexBox.Clear();
        }

        public static void textBoxChangeTxt(string input,TextBox inputBotexBox)
        {
            inputBotexBox.Text = input;
        }
    }
}
