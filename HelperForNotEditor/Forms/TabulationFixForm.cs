using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;

namespace HelperForNotEditor
{
    public partial class TabulationFixForm : Form
    {
        private const UInt32 EM_SETTABSTOPS = 0x00CB;
        private const int unitsPerCharacter = 4;

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, ref IntPtr lParam);

        private int TextBox1WidthAndFormWidthDifference = 0;
        private int TextBox2WidthAndFormWidthDifference = 0;
        private int TextBox1HeightAndFormWidthDifference = 0;
        private int TextBox2HeightAndFormWidthDifference = 0;
        private int TextBox2AndTextBox1Difference = 0;
        public TabulationFixForm()
        {
            InitializeComponent();
            TextBox1WidthAndFormWidthDifference = this.Width - textBox1.Width;
            TextBox2WidthAndFormWidthDifference = this.Width - textBox2.Width;
            TextBox1HeightAndFormWidthDifference = (this.Height / 2) - textBox1.Height;
            TextBox2HeightAndFormWidthDifference = (this.Height / 2) - textBox2.Height;
            TextBox2AndTextBox1Difference = textBox2.Location.Y - (textBox1.Location.Y + textBox1.Height);
        }

        private void TabulationFixForm_SizeChanged(object sender, EventArgs e)
        {
            // Пропорционально изменяем размеры TextBox
            textBox1.Width = this.Width - TextBox1WidthAndFormWidthDifference;
            textBox1.Height = (this.Height / 2) - TextBox1HeightAndFormWidthDifference;

            textBox2.Width = this.Width - TextBox2WidthAndFormWidthDifference;
            textBox2.Height = (this.Height / 2) - TextBox2HeightAndFormWidthDifference;
            textBox2.Location = new Point(textBox2.Location.X, (this.Height / 2) - TextBox2AndTextBox1Difference);
        }

        private void fileSelectButton_Click(object sender, EventArgs e)
        {
            List<string> fileLines = new List<string>();
            string filePath = string.Empty;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    fileLines = new List<string>(File.ReadAllLines(filePath));
                }
            }

            ReplaceChange rc = new ReplaceChange();
            var fileText = string.Join("\r\n", fileLines);
            SetTextBoxTabStopLength(textBox1, 2);
            SetTextBoxTabStopLength(textBox2, 2);

            textBox1.Text = fileText;
            var result = rc.TabulationFunc(fileText, '\t');
            textBox2.Text = result;
            File.WriteAllText(filePath, result);
        }

        public static void SetTextBoxTabStopLength(TextBox tb, int tabSizeInCharacters)
        {
            // 1 means all tab stops are the the same length
            // This means lParam must point to a single integer that contains the desired tab length
            const uint regularLength = 1;

            // A dialog unit is 1/4 of the average character width
            int length = tabSizeInCharacters * unitsPerCharacter;

            // Pass the length pointer by reference, essentially passing a pointer to the desired length
            IntPtr lengthPointer = new IntPtr(length);
            SendMessage(tb.Handle, EM_SETTABSTOPS, (IntPtr)regularLength, ref lengthPointer);
        }
    }
}
