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
        /// <summary>
        /// Метод для получения пути до ассетсов проекта
        /// </summary>
        /// <param name="folder">Путь до ассетсов проекта</param>
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

        public void ChangeForm(string type)
        {
            switch (type)
            {
                case "GatesPort":
                    gatesGoPortButton.Enabled = true;
                    gatesGoPortButton.Visible = true;

                    button1.Enabled = false;
                    button1.Visible = false;
                break;

                default:
                    gatesGoPortButton.Enabled = false;
                    gatesGoPortButton.Visible = false;

                    button1.Enabled = true;
                    button1.Visible = true;
                    break;
            }

        }

        /// <summary>
        /// Портирование rm_menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Портирование gates
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gatesGoPortButton_Click(object sender, EventArgs e)
        {
            // Находим все файлы расширения .lua
            string[] allFoundFiles = Directory.GetFiles(GameAssetsFolder, "*.lua", SearchOption.AllDirectories);
            // Находим mod_common_impl.lua
            string[] mod_common_impl = Directory.GetFiles(GameAssetsFolder, "mod_common_impl.lua", SearchOption.AllDirectories);

            // Вытаскиваем gates-ы из mod_common_impl.lua
            string tmp = File.ReadAllText(mod_common_impl[0]);
            // Проверяем на наличие упоминания port.list_gates_
            if (tmp.IndexOf("port.list_gates_", StringComparison.CurrentCulture) != -1)
            {
                Regex regex = new Regex("port.list_gates_", RegexOptions.IgnoreCase);
                MatchCollection regCollect = regex.Matches(tmp);
                // Вызываем метод для получения списка gates
                var gatesList = GetGatesList(tmp, regCollect.Count);
                var getGates = GetGrmGatesList(gatesList);

                // Проходимся по найденным gates
                foreach (string gate in getGates)
                {
                    // Проходимся по найденным файлам .lua
                    foreach (string file in allFoundFiles)
                    {
                        string fileText = File.ReadAllText(file);
                        // Проверяем на наличие упоминания функции gate
                        if (fileText.IndexOf(gate.Replace("_focus", ""), StringComparison.CurrentCulture) != -1)
                        {
                            if (!file.Contains("mod_common_impl.lua"))
                            {
                                //File.Delete(file);
                                // Вызываем метод для комментирования вызова ObjDoNotDrop
                                File.WriteAllText(file, SetLightStatusForGate(fileText, file, gate));
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Вырываем port.list_gates_# из *lua
        /// </summary>
        /// <param name="inputText">Исходный текст lua модуля с port.list_gates_#</param>
        /// <param name="countLua">Кол-во встреающихся port.list_gates_# в текущем модуле</param>
        /// <returns></returns>
        private List<string> GetGatesList(string inputText, int countLua)
        {
            string inputFirst = inputText;
            string inputTextOld = inputText;
            int currentLineIndex = 0; //индекс текущей строки
            int findLineIndex = 0;    //индекс найденной строки
            List<string> gatesList = new List<string>();
            
            for (int i = 1; i <= countLua; i++)
            {
                findLineIndex = inputText.IndexOf("port.list_gates_" + i);
                if (findLineIndex != -1)
                {
                    currentLineIndex = findLineIndex;
                    findLineIndex = inputText.IndexOf("};",currentLineIndex);
                    var gateListLua = inputText.Substring(currentLineIndex, findLineIndex - currentLineIndex);
                    richTextBox1.Text = richTextBox1.Text + "\nНайдены следующие gates:\n" + gateListLua;
                    richTextBox1.SelectionStart = richTextBox1.TextLength;
                    richTextBox1.ScrollToCaret();
                    gatesList.Add(gateListLua);
                }
            }

            return gatesList;
        }

        /// <summary>
        /// Метод для вытаскивание grm_**_focus из port.list_gates_#
        /// </summary>
        /// <param name="gatesList">Список port.list_gates_#</param>
        /// <returns></returns>
        private List<string> GetGrmGatesList(List<string> gatesList)
        {
            List<string> grmGatesList = new List<string>();
            for(int i = 0; i < gatesList.Count; i++)
            {
                var splitedLine = gatesList[i].Split("\n");
                for (int j = 0; j < splitedLine.Length; j++)
                {
                    if (splitedLine[j].Contains("\"ho_") || splitedLine[j].Contains("\"mg_"))
                        gatesList[i] = gatesList[i].Replace(splitedLine[j], "");

                }
                var splitedGrmGates = gatesList[i].Split('"');
                for (int j = 0; j < splitedGrmGates.Length; j++)
                {
                    if(splitedGrmGates[j].Contains("grm_") && splitedGrmGates[j].Contains("_focus"))
                        grmGatesList.Add(splitedGrmGates[j]);
                }
            }

            return grmGatesList;
        }

        private string SetLightStatusForGate(string inputText, string file, string gate)
        {
            string outputText = inputText;
            var patternCount = 0;
            var pattern = "ObjSet(";
            var pattern2 = "\"" + gate.Replace("_focus", "") + "\"";
            

            foreach (string line in inputText.Split("\n"))
            {
                if (line.Contains(pattern) && line.Contains(pattern2))
                {
                    patternCount++;
                }
            }

            if (patternCount == 1)
            {
                var indexGateEditLine = inputText.IndexOf("ObjSet(\"" + gate.Replace("_focus", ""));

                if(indexGateEditLine == -1)
                    indexGateEditLine = inputText.IndexOf("ObjSet( \"" + gate.Replace("_focus", ""));
                if (indexGateEditLine == -1)
                    indexGateEditLine = inputText.IndexOf("ObjSet( \" " + gate.Replace("_focus", ""));
                if (indexGateEditLine == -1)
                    indexGateEditLine = inputText.IndexOf("ObjSet(\" " + gate.Replace("_focus", ""));

                var indexGateEditLineEnd = inputText.IndexOf("\n", indexGateEditLine);
                var cutThisLine = inputText.Substring(indexGateEditLine, indexGateEditLineEnd - indexGateEditLine);
                richTextBox1.Text = richTextBox1.Text + "\nНайдена такая реализация gate в файле:\n" + file + "\n" + cutThisLine;
                richTextBox1.SelectionStart = richTextBox1.TextLength;
                richTextBox1.ScrollToCaret();

                if (cutThisLine.Contains("=true") || cutThisLine.Contains("= true") || cutThisLine.Contains("= 1") || cutThisLine.Contains("=1"))
                {
                    outputText = inputText.Insert(indexGateEditLineEnd + 1, "\n  if port.isPortable() == 1 then\n    port.GrmEventLightOn(\"" + gate + "\");\n  end;\n");
                }
                else if (cutThisLine.Contains("=false") || cutThisLine.Contains("= false") || cutThisLine.Contains("= 0") || cutThisLine.Contains("=0"))
                {
                    outputText = inputText.Insert(indexGateEditLineEnd + 1, "\n  if port.isPortable() == 1 then\n    port.GrmEventLightOff(\"" + gate + "\");\n  end;\n");
                }
            }
            else
            {
                int indexLastFindLine = 0;
                for (int i = 1; i <= patternCount; i++)
                {
                    var indexGateEditLine = inputText.IndexOf("ObjSet(\"" + gate.Replace("_focus", ""), indexLastFindLine);

                    if (indexGateEditLine == -1)
                        indexGateEditLine = inputText.IndexOf("ObjSet( \"" + gate.Replace("_focus", ""), indexLastFindLine);
                    if (indexGateEditLine == -1)
                        indexGateEditLine = inputText.IndexOf("ObjSet( \" " + gate.Replace("_focus", ""), indexLastFindLine);
                    if (indexGateEditLine == -1)
                        indexGateEditLine = inputText.IndexOf("ObjSet(\" " + gate.Replace("_focus", ""), indexLastFindLine);

                    var indexGateEditLineEnd = inputText.IndexOf("\n", indexGateEditLine);
                    var cutThisLine = inputText.Substring(indexGateEditLine, indexGateEditLineEnd - indexGateEditLine);
                    richTextBox1.Text = richTextBox1.Text + "\nНайдена такая реализация gate в файле:\n" + file + "\n" + cutThisLine;

                    if (cutThisLine.Contains("=true") || cutThisLine.Contains("= true"))
                    {
                        inputText = inputText.Insert(indexGateEditLineEnd + 1, "\n  if port.isPortable() == 1 then\n    port.GrmEventLightOn(\"" + gate + "\");\n  end;\n");
                    }
                    else if (cutThisLine.Contains("=false") || cutThisLine.Contains("= false"))
                    {
                        inputText = inputText.Insert(indexGateEditLineEnd + 1, "\n  if port.isPortable() == 1 then\n    port.GrmEventLightOff(\"" + gate + "\");\n  end;\n");
                    }
                    indexLastFindLine = indexGateEditLineEnd;
                }
                outputText = inputText;
            }
            return outputText;
        }
    }
}
