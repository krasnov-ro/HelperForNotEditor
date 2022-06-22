using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace HelperForNotEditor
{
    class richTextBoxColor
    {
        public void textColorEdit(string text, RichTextBox richTextBox, Color color)
        {
            string regExpr = text;
            foreach (Match m in Regex.Matches(richTextBox.Text, text))
            {
                richTextBox.SelectionStart = m.Index;
                richTextBox.SelectionLength = m.Length;
                richTextBox.SelectionColor = color;
            }
        }
    }
}
