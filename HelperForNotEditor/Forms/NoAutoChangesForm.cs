using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HelperForNotEditor
{
    public partial class NoAutoChangesForm : Form
    {
        public NoAutoChangesForm(string function)
        {
            InitializeComponent();
            richTextBox1.Text = function;
        }

        private void goChangesButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        public string ReturnData()
        {
            return richTextBox1.Text;
        }
    }
}
