using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace HelperForNotEditor
{
    public partial class ReplacerForm : Form
    {
        private string folderName;
        public ReplacerForm()
        {
            InitializeComponent();
        }
        public void sendFolder(string folderStr)
        {
            if (folderStr != string.Empty)
            {
                folderName = folderStr;
            }
        }

        private void replaceButton_Click(object sender, EventArgs e)
        {
            if (folderName == null || folderName == string.Empty)
            {
                richTextBox2.Text = richTextBox2.Text + "[error] не указана папка assets\n";

                string regExpr = @"error";
                foreach (Match m in Regex.Matches(richTextBox2.Text, regExpr))
                {
                    richTextBox2.SelectionStart = m.Index;
                    richTextBox2.SelectionLength = m.Length;
                    richTextBox2.SelectionColor = Color.Red;
                }

                return;
            }
            if (richTextBox1.Text == "" || richTextBox1.Text == null)
            {
                richTextBox2.Text = richTextBox2.Text + "[error] не указана строка которую нужно найти и заменить\n";

                string regExpr = @"error";
                foreach (Match m in Regex.Matches(richTextBox2.Text, regExpr))
                {
                    richTextBox2.SelectionStart = m.Index;
                    richTextBox2.SelectionLength = m.Length;
                    richTextBox2.SelectionColor = Color.Red;
                }

                return;
            }
            if (richTextBox3.Text == "" || richTextBox3.Text == null)
            {
                richTextBox2.Text = richTextBox2.Text + "[error] не указана строка на которую нужно заменить\n";

                string regExpr = @"error";
                foreach (Match m in Regex.Matches(richTextBox2.Text, regExpr))
                {
                    richTextBox2.SelectionStart = m.Index;
                    richTextBox2.SelectionLength = m.Length;
                    richTextBox2.SelectionColor = Color.Red;
                }

                return;
            }


            string[] allFoundFiles = Directory.GetFiles(folderName, "*.lua", SearchOption.AllDirectories);
            foreach (string file in allFoundFiles)
            {
                string tmp = File.ReadAllText(file);
                if (tmp.IndexOf(richTextBox1.Text.Replace("\n","\r\n").Trim(), StringComparison.CurrentCulture) != -1)
                {
                    File.Delete(file);
                    File.WriteAllText(file, ReplaceLogEvents(tmp, file));
                }
            }
            richTextBox2.Text = richTextBox2.Text + "Замена успешно завершена!\n";
            string regExpr2 = @"--------------------------------------------------------\n";
            foreach (Match m in Regex.Matches(richTextBox2.Text, regExpr2))
            {
                richTextBox2.SelectionStart = m.Index;
                richTextBox2.SelectionLength = m.Length;
                richTextBox2.SelectionColor = Color.Green;
            }
        }

        public string ReplaceLogEvents(string inputText, string file)
        {
            string inputFirst = inputText;
            string inputTextOld = inputText;

            if (inputText != null)
                inputText = inputText.Replace(richTextBox1.Text, richTextBox3.Text);
            richTextBox2.Text = richTextBox2.Text + "Произведена замена " + richTextBox1.Text + " в файле " + file + "\n " +
                " на следующее\n " + richTextBox3.Text;

            if (!inputFirst.Equals(inputText))
            {
                richTextBox2.Text = richTextBox2.Text + "--------------------------------------------------------\n";
                string regExpr = @"--------------------------------------------------------\n";
                foreach (Match m in Regex.Matches(richTextBox2.Text, regExpr))
                {
                    richTextBox2.SelectionStart = m.Index;
                    richTextBox2.SelectionLength = m.Length;
                    richTextBox2.SelectionColor = Color.Green;
                }
            }
            return inputText;
        }
    }
}
