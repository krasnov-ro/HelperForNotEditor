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
            "Для реализации событий в SetEventDone, выборка событий use, opn, win",  ///0
            "Портирование меню (не готово)",                                         ///1
            "Закомментирование всех строк вызова interface.ObjDoNotDrop",            ///2
            "Реализация проверки энергии игрока",                                    ///3
            "Подключение библиотек портирования к текущему проекту",                 ///4
            "Замена указанных строк в файлах игры",                                  ///5
            "Конвертация .ogg в .mp4",                                               ///6
            "Пройтись по gates и выставить LightOn or LightOff"                      ///7
        };
        string folder = string.Empty;

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
            if(comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Выберите функцию!");
                return;
            }
            if (comboBox1.SelectedItem.ToString() == comboItems[0])
            {
                Form1 a = new Form1();
                this.Hide();
                a.ShowDialog();
                this.Show();
            }
            else if(comboBox1.SelectedItem.ToString() == comboItems[1])
            {
                GameMenuPortForm a = new GameMenuPortForm();
                this.Hide();
                a.sendFolder(folder);
                a.ShowDialog();
                this.Show();
            }
            else if(comboBox1.SelectedItem.ToString() == comboItems[2])
            {
                LogEvents_changer a = new LogEvents_changer();
                this.Hide();
                a.ChangeForm("ObjDoNotDrop");
                a.sendFolder(folder);
                a.Text = comboBox1.SelectedItem.ToString();
                a.ShowDialog();
                this.Show();
            }
            else if(comboBox1.SelectedItem.ToString() == comboItems[3])
            {
                LogEvents_changer a = new LogEvents_changer();
                this.Hide();
                a.ChangeForm("CheckEnergy");
                a.sendFolder(folder);
                a.Text = comboBox1.SelectedItem.ToString();
                a.ShowDialog();
                this.Show();
            }
            else if(comboBox1.SelectedItem.ToString() == comboItems[4])
            {
                f2pFilesForm a = new f2pFilesForm();
                this.Hide();
                a.sendFolder(folder);
                a.Text = comboBox1.SelectedItem.ToString();
                a.ShowDialog();
                this.Show();
            }
            else if(comboBox1.SelectedItem.ToString() == comboItems[5])
            {
                ReplacerForm a = new ReplacerForm();
                this.Hide();
                a.sendFolder(folder);
                a.Text = comboBox1.SelectedItem.ToString();
                a.ShowDialog();
                this.Show();
            }
            else if (comboBox1.SelectedItem.ToString() == comboItems[6])
            {
                Ogg2MP4_converter a = new Ogg2MP4_converter();
                this.Hide();
                a.sendFolder(folder);
                a.ShowDialog();
                this.Show();
            }
            else if (comboBox1.SelectedItem.ToString() == comboItems[7])
            {
                GameMenuPortForm gatesPort = new GameMenuPortForm();
                this.Hide();
                gatesPort.sendFolder(folder);
                gatesPort.ChangeForm("GatesPort");
                gatesPort.Text = comboBox1.SelectedItem.ToString();
                gatesPort.ShowDialog();
                this.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // показать диалог выбора папки
            DialogResult result = folderBrowserDialog1.ShowDialog();

            // если папка выбрана и нажата клавиша `OK` - значит можно получить путь к папке
            if (result == DialogResult.OK)
            {
                // запишем в нашу переменную путь к папке
                folder = folderBrowserDialog1.SelectedPath;
                textBox1.Text = folderBrowserDialog1.SelectedPath.ToString();
            }
        }
    }
}
