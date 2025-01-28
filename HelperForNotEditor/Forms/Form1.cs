using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HelperForNotEditor
{
    public partial class Form1 : Form
    {
        public string fileContent;
        public string filePath;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
            }
            checkBox3.Checked = true;
            checkBox4.Checked = true;
            checkBox5.Checked = true;
            button3.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<string> preNames = new List<string>();
            foreach (Control c in this.Controls)
            {
                if (c is CheckBox)
                {
                    if (((CheckBox)c).Checked == true)
                        preNames.Add(c.Text + "_");
                }
            }

            if (textBox1.Text.Length > 0)
            {
                var preNamesTB = textBox1.Text.Split(";");
                foreach (var preName in preNamesTB)
                {
                    if (preName != "")
                    {
                        if (preNamesTB.Length != 1)
                            preNames.Add(preName.Replace(";", "").Replace(" ", ""));
                        else
                            preNames.Add(preName.Replace(" ", ""));
                    }
                }
            }

            if (fileContent != null)
            {
                richTextBox1.Text = ReformatText(fileContent, preNames);
            }
            else
            {
                richTextBox1.Text = ReformatText(richTextBox1.Text, preNames);
            }
        }

        public string ReformatText(string inputText, List<string> preNames)
        {
            string result = "{\n";
            var contentArr = inputText.Split('\n');
            for (int i = 0; i < contentArr.Length; i++)
            {
                if (preNames.Any(p => contentArr[i].Contains(p)) || contentArr[i].Contains("LEVEL"))
                {
                    if(contentArr[i].Count(p => p == '\"') > 2 && radioButton1.Checked == true)
                    {
                        var splitContent = contentArr[i].Split('\"');
                        for (int j = 0; j < splitContent.Length; j++)
                        {
                            if (preNames.Any(p => splitContent[1].Contains(p)))
                            {
                                result = result + "    , \"" + splitContent[1] + "\"\n";
                                break;
                            }
                        }
                    }
                    else if (contentArr[i].Count(p => p == ',') > 1 && radioButton1.Checked == true)
                    {
                        result = result + "    , " + contentArr[i].Replace("{", "").Replace("}", "").Replace(",", "").Replace("    ", "") + "\n";
                    }
                    else if (radioButton1.Checked == true)
                        result = result + contentArr[i].Replace("{", "").Replace("}", "") + "\n";
                    else
                        result = result + contentArr[i] + "\n";
                }
            }
            result = result + "}";
            return result;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            button3.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form.ActiveForm.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            button3.Enabled = true;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            button3.Enabled = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
