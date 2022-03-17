using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HelperForNotEditor
{
    public partial class menuForm : Form
    {
        // !!!       !!!       !!!             !!!
        // при изменении позиций в массиве, придется скорректировать номер в функции goWork_Click()
        string[] comboItems = {
            "Исключение ненужных строк",                                        ///0
            "Произвести замену числового кода на словарный код в LogEvents",    ///1
            "Закомментирование всех строк вызова interface.ObjDoNotDrop",       ///2
            "Проставление в assets вызов функции CheckEnergy()",                ///3
            "Перенос f2p файлов в assets (копируем)"                            ///4
        };

        public menuForm()
        {
            InitializeComponent();
            for(int i = 0; i < comboItems.Length; i++)
            {
                comboBox1.Items.Add(comboItems[i]);
            }
        }

        private void goWork_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == comboItems[0])
            {
                Form1 a = new Form1();
                this.Hide();
                a.ShowDialog();
                this.Show();
            }
            else if(comboBox1.SelectedItem == comboItems[1])
            {
                LogEvents_changer a = new LogEvents_changer();
                this.Hide();
                a.ChangeForm("LogEvents");
                a.ShowDialog();
                this.Show();
            }
            else if(comboBox1.SelectedItem == comboItems[2])
            {
                LogEvents_changer a = new LogEvents_changer();
                this.Hide();
                a.ChangeForm("ObjDoNotDrop");
                a.ShowDialog();
                this.Show();
            }
            else if(comboBox1.SelectedItem == comboItems[3])
            {
                LogEvents_changer a = new LogEvents_changer();
                this.Hide();
                a.ChangeForm("CheckEnergy");
                a.ShowDialog();
                this.Show();
            }
            else if(comboBox1.SelectedItem == comboItems[4])
            {
                f2pFilesForm a = new f2pFilesForm();
                this.Hide();
                a.ShowDialog();
                this.Show();
            }
        }
    }
}
