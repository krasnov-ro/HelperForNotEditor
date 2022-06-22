using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace HelperForNotEditor
{
    public partial class GameMenuPortForm : Form
    {
        private string GameAssetsFolder = string.Empty;
        public GameMenuPortForm()
        {
            InitializeComponent();
        }

        public void sendFolder(string folder)
        {
            if (folder == null || folder == string.Empty)
            {
                richTextBox1.Text = richTextBox1.Text + "error не указана папка assets\n";
                richTextBoxColor richTextBoxColor = new richTextBoxColor();
                richTextBoxColor.textColorEdit(@"error", richTextBox1, Color.Red);

                button1.Enabled = false;
            }
            else
            {
                GameAssetsFolder = folder;
                button1.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var menuXmlFile = Directory.GetFiles(GameAssetsFolder, "mod_menu.xml", SearchOption.AllDirectories);

            foreach (string file in menuXmlFile)
            {
                if (file.Contains("rm_menu"))
                {
                    richTextBox1.Text = richTextBox1.Text + "Найден файл структуры меню [" + file + "]\n";
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(file);
                    richTextBox1.Text = richTextBox1.Text + "Открываем файл\n";
                    XmlElement? XmlRoot = xmlDoc.DocumentElement;
                    richTextBox1.Text = richTextBox1.Text + "Получаем корневой элемент XML файла [" + XmlRoot?.Name +"]\n";
                    if (XmlRoot != null)
                    {
                        // обход всех узлов в корневом элементе
                        foreach (XmlElement XmlNode in XmlRoot)
                        {
                            foreach (XmlNode childNode in XmlNode.ChildNodes)
                            {
                                // получаем атрибут name
                                XmlNode? attr = childNode.Attributes.GetNamedItem("name");
                                if (attr == null)
                                    attr = childNode.Attributes.GetNamedItem("_name");
                                richTextBox1.Text = richTextBox1.Text + "Обход элементов [" + attr?.Value + "]\n";
                                if(attr?.Value == "__rm_menu_rootobj")
                                {
                                    foreach (XmlNode gNode in childNode.ChildNodes)
                                    {
                                        // получаем атрибут name
                                        XmlNode? attrG = gNode.Attributes.GetNamedItem("name");
                                        if (attrG == null)
                                            attrG = gNode.Attributes.GetNamedItem("_name");
                                        richTextBox1.Text = richTextBox1.Text + "Обход элементов [" + attrG?.Value + "]\n";

                                        if(attrG?.Value == "rm_menu")
                                        {
                                            XmlDocument xmlTimersDoc = new XmlDocument();
                                            xmlTimersDoc.Load("");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
