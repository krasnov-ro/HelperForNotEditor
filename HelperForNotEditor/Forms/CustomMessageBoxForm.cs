using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HelperForNotEditor
{
    public partial class CustomMessageBoxForm : Form
    {
        public bool YesNo = false;
        public bool HaveClick = false;
        public CustomMessageBoxForm()
        {
            InitializeComponent();
            HaveClick = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            YesNo = true;
            HaveClick = true;
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            YesNo = false;
            HaveClick = true;
            this.Hide();
        }
    }
}
