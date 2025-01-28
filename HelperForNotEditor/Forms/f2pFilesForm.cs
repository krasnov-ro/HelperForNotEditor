using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HelperForNotEditor
{
    public partial class f2pFilesForm : Form
    {
        string sourceFolderName;
        string targetFolderName;
        string[] filesArray;
        int i = 0;
        int j = 0;
        public f2pFilesForm()
        {
            InitializeComponent();
            goButton.Visible = false;
        }

        private void sourceButton_Click(object sender, EventArgs e)
        {
            // показать диалог выбора папки
            DialogResult result = folderBrowserDialog1.ShowDialog();

            // если папка выбрана и нажата клавиша `OK` - значит можно получить путь к папке
            if (result == DialogResult.OK)
            {
                // запишем в нашу переменную путь к папке
                sourceFolderName = folderBrowserDialog1.SelectedPath;
                richTextBox1.Text = richTextBox1.Text + "Выбрана папка откуда копируем f2p файлы: " + sourceFolderName + "\n";
            }
            checkReady();
        }

        private void targetButton_Click(object sender, EventArgs e)
        {
            // показать диалог выбора папки
            DialogResult result = folderBrowserDialog2.ShowDialog();

            // если папка выбрана и нажата клавиша `OK` - значит можно получить путь к папке
            if (result == DialogResult.OK)
            {
                // запишем в нашу переменную путь к папке
                targetFolderName = folderBrowserDialog2.SelectedPath;
                richTextBox1.Text = richTextBox1.Text + "Выбрана папка куда копируем f2p файлы: " + targetFolderName + "\n";
            }
            checkReady();
        }

        private void filesListButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var filePath = openFileDialog.FileName;

                    filesArray = File.ReadAllLines(filePath);
                }
            }
            richTextBox1.Text = richTextBox1.Text + "\nЗагружен список файлов для переноса [" + filesArray.Count() + " шт]";
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
            checkReady();
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            CopyDir(sourceFolderName, targetFolderName);
            richTextBox1.Text = richTextBox1.Text + "\nКопирование файлов завершено! [Возникло " +j+ " ошибок]";
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();

            //goButton.Location = targetButton.Location;
            //sourceButton.Visible = false;
            //targetButton.Visible = false;
            //filesListButton.Visible = false;
            //goButton.Text = ""
        }

        void CopyDir(string sourceDir, string targetDir)
        {
            Directory.CreateDirectory(targetDir);
            foreach (string s1 in Directory.GetFiles(sourceDir))
            {
                foreach (string filePath in filesArray)
                {
                    if (s1.Contains(filePath))
                    {
                        string s2 = targetDir + "\\" + Path.GetFileName(s1);
                        try
                        {
                            File.Copy(s1, s2, true);
                            i++;
                            richTextBox1.Text = richTextBox1.Text + "\n[" + i+ "]\nСкопирован файл: " + s1 + "\n Сюда: " + s2;
                            richTextBox1.SelectionStart = richTextBox1.Text.Length;
                            richTextBox1.ScrollToCaret();
                        }
                        catch (Exception ex)
                        {
                            j++;
                            richTextBox1.Text = richTextBox1.Text + "\n!!!! Произошла ошибка: " + ex;
                            richTextBox1.SelectionStart = richTextBox1.Text.Length;
                            richTextBox1.ScrollToCaret();
                        }
                    }
                }
            }
            foreach (string s in Directory.GetDirectories(sourceDir))
            {
                CopyDir(s, targetDir + "\\" + Path.GetFileName(s));
            }
        }

        private void checkReady()
        {
            if(sourceFolderName != null && targetFolderName != null && filesArray != null)
            {
                goButton.Visible = true;
            }
        }
    }
}
