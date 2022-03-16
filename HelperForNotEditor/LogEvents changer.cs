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
    public partial class LogEvents_changer : Form
    {
        List<string> filesArrGlobal = new List<string>();
        public LogEvents_changer()
        {
            InitializeComponent();
        }

        private string folderName;

        private void button1_Click(object sender, EventArgs e)
        {
            // показать диалог выбора папки
            DialogResult result = folderBrowserDialog1.ShowDialog();

            // если папка выбрана и нажата клавиша `OK` - значит можно получить путь к папке
            if (result == DialogResult.OK)
            {
                // запишем в нашу переменную путь к папке
                folderName = folderBrowserDialog1.SelectedPath;
                richTextBox1.Text = richTextBox1.Text + "Выбрана папка: " + folderName + "\n";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (folderName == null)
            {
                richTextBox1.Text = richTextBox1.Text + "error не указана папка assets\n";

                string regExpr = @"error";
                foreach (Match m in Regex.Matches(richTextBox1.Text, regExpr))
                {
                    richTextBox1.SelectionStart = m.Index;
                    richTextBox1.SelectionLength = m.Length;
                    richTextBox1.SelectionColor = Color.Red;
                }

                return;
            }

            string[] allFoundFiles = Directory.GetFiles(folderName, "*.lua", SearchOption.AllDirectories);
            foreach (string file in allFoundFiles)
            {
                string tmp = File.ReadAllText(file);
                if (tmp.IndexOf("LogEvents(", StringComparison.CurrentCulture) != -1)
                {
                    File.Delete(file);
                    File.WriteAllText(file, ReplaceLogEvents(tmp, file));
                }
            }
            richTextBox1.Text = richTextBox1.Text + "Замена успешно завершена!\n";
            string regExpr2 = @"--------------------------------------------------------\n";
            foreach (Match m in Regex.Matches(richTextBox1.Text, regExpr2))
            {
                richTextBox1.SelectionStart = m.Index;
                richTextBox1.SelectionLength = m.Length;
                richTextBox1.SelectionColor = Color.Green;
            }
        }

        public string ReplaceLogEvents(string inputText, string file)
        {
            string inputFirst = inputText;
            string inputTextOld = inputText;

            #region Replace
            inputText = inputText.Replace("LogEvents(2)", "LogEvents(ng_stat.game.StartIntro)");
            if(!inputText.Equals(inputTextOld))
            {
                richTextBox1.Text = richTextBox1.Text + "Произведена замена LogEvents(2) в файле " + file + "\n";
            }

            inputTextOld = inputText;
            inputText = inputText.Replace("LogEvents(3)", "LogEvents(ng_stat.game.EndIntro)");
            if (!inputText.Equals(inputTextOld))
            {
                richTextBox1.Text = richTextBox1.Text + "Произведена замена LogEvents(3) в файле " + file + "\n";
            }


            inputTextOld = inputText;
            inputText = inputText.Replace("LogEvents(3,", "LogEvents(ng_stat.game.EndIntro,");
            if (!inputText.Equals(inputTextOld))
            {
                richTextBox1.Text = richTextBox1.Text + "Произведена замена LogEvents(3) в файле " + file + "\n";
            }

            inputTextOld = inputText;
            inputText = inputText.Replace("LogEvents(35, {step_id", "LogEvents(ng_stat.game.Tutorial, {step_id");
            if (!inputText.Equals(inputTextOld))
            {
                richTextBox1.Text = richTextBox1.Text + "Произведена замена LogEvents(35) в файле " + file + "\n";
            }

            inputTextOld = inputText;
            inputText = inputText.Replace("LogEvents(33, {step_id", "LogEvents(ng_stat.game.Tutorial, {step_id");
            if (!inputText.Equals(inputTextOld))
            {
                richTextBox1.Text = richTextBox1.Text + "Произведена замена LogEvents(33) в файле " + file + "\n";
            }

            inputTextOld = inputText;
            inputText = inputText.Replace("LogEvents(4", "LogEvents(ng_stat.game.ShowWindowName");
            if (!inputText.Equals(inputTextOld))
            {
                richTextBox1.Text = richTextBox1.Text + "Произведена замена LogEvents(4) в файле " + file + "\n";
            }

            inputTextOld = inputText;
            inputText = inputText.Replace("LogEvents(5", "LogEvents(ng_stat.game.EnterName");
            if (!inputText.Equals(inputTextOld))
            {
                richTextBox1.Text = richTextBox1.Text + "Произведена замена LogEvents(5) в файле " + file + "\n";
            }

            inputTextOld = inputText;
            inputText = inputText.Replace("LogEvents(6", "LogEvents(ng_stat.game.PlayDown");
            if (!inputText.Equals(inputTextOld))
            {
                richTextBox1.Text = richTextBox1.Text + "Произведена заменаLogEvents(6) в файле " + file + "\n";
            }

            inputTextOld = inputText;
            inputText = inputText.Replace("LogEvents(7", "LogEvents(ng_stat.game.ChangeDifficulty");
            if (!inputText.Equals(inputTextOld))
            {
                richTextBox1.Text = richTextBox1.Text + "Произведена замена LogEvents(7) в файле " + file + "\n";
            }

            inputTextOld = inputText;
            inputText = inputText.Replace("LogEvents(8", "LogEvents(ng_stat.game.EnterRmUnlock");
            if (!inputText.Equals(inputTextOld))
            {
                richTextBox1.Text = richTextBox1.Text + "Произведена замена LogEvents(8) в файле " + file + "\n";
            }

            inputTextOld = inputText;
            inputText = inputText.Replace("LogEvents(9", "LogEvents(ng_stat.game.CloseRmUnlock");
            if (!inputText.Equals(inputTextOld))
            {
                richTextBox1.Text = richTextBox1.Text + "Произведена замена LogEvents(9) в файле " + file + "\n";
            }

            inputTextOld = inputText;
            inputText = inputText.Replace("LogEvents(10", "LogEvents(ng_stat.game.BuyGame");
            if (!inputText.Equals(inputTextOld))
            {
                richTextBox1.Text = richTextBox1.Text + "Произведена замена LogEvents(10) в файле " + file + "\n";
            }

            inputTextOld = inputText;
            inputText = inputText.Replace("LogEvents(11", "LogEvents(ng_stat.game.StartCutScene");
            if (!inputText.Equals(inputTextOld))
            {
                richTextBox1.Text = richTextBox1.Text + "Произведена замена LogEvents(11) в файле " + file + "\n";
            }

            inputTextOld = inputText;
            inputText = inputText.Replace("LogEvents(12", "LogEvents(ng_stat.game.EndCutScene");
            if (!inputText.Equals(inputTextOld))
            {
                richTextBox1.Text = richTextBox1.Text + "Произведена замена LogEvents(12) в файле " + file + "\n";
            }

            inputTextOld = inputText;
            inputText = inputText.Replace("LogEvents(13", "LogEvents(ng_stat.game.UnlockLocation");
            if (!inputText.Equals(inputTextOld))
            {
                richTextBox1.Text = richTextBox1.Text + "Произведена замена LogEvents(13) в файле " + file + "\n";
            }

            inputTextOld = inputText;
            inputText = inputText.Replace("LogEvents(14", "LogEvents(ng_stat.game.CompleteHO");
            if (!inputText.Equals(inputTextOld))
            {
                richTextBox1.Text = richTextBox1.Text + "Произведена замена LogEvents(14) в файле " + file + "\n";
            }

            inputTextOld = inputText;
            inputText = inputText.Replace("LogEvents(15", "LogEvents(ng_stat.game.CompleteMiniGame");
            if (!inputText.Equals(inputTextOld))
            {
                richTextBox1.Text = richTextBox1.Text + "Произведена замена LogEvents(15) в файле " + file + "\n";
            }

            inputTextOld = inputText;
            inputText = inputText.Replace("LogEvents(16", "LogEvents(ng_stat.game.GetItem");
            if (!inputText.Equals(inputTextOld))
            {
                richTextBox1.Text = richTextBox1.Text + "Произведена замена LogEvents(16) в файле " + file + "\n";
            }

            inputTextOld = inputText;
            inputText = inputText.Replace("LogEvents(17", "LogEvents(ng_stat.game.OpenStrategyGuide");
            if (!inputText.Equals(inputTextOld))
            {
                richTextBox1.Text = richTextBox1.Text + "Произведена замена LogEvents(17) в файле " + file + "\n";
            }

            inputTextOld = inputText;
            inputText = inputText.Replace("LogEvents(18", "LogEvents(ng_stat.game.ClickTask");
            if (!inputText.Equals(inputTextOld))
            {
                richTextBox1.Text = richTextBox1.Text + "Произведена замена LogEvents(18) в файле " + file + "\n";
            }

            inputTextOld = inputText;
            inputText = inputText.Replace("LogEvents(19", "LogEvents(ng_stat.game.ClickHint");
            if (!inputText.Equals(inputTextOld))
            {
                richTextBox1.Text = richTextBox1.Text + "Произведена замена LogEvents(19) в файле " + file + "\n";
            }

            inputTextOld = inputText;
            inputText = inputText.Replace("LogEvents(20", "LogEvents(ng_stat.game.ChangeLocation");
            if (!inputText.Equals(inputTextOld))
            {
                richTextBox1.Text = richTextBox1.Text + "Произведена замена LogEvents(20) в файле " + file + "\n";
            }

            inputTextOld = inputText;
            inputText = inputText.Replace("LogEvents(22", "LogEvents(ng_stat.game.EndGame");
            if (!inputText.Equals(inputTextOld))
            {
                richTextBox1.Text = richTextBox1.Text + "Произведена замена LogEvents(22) в файле " + file + "\n";
            }

            inputTextOld = inputText;
            inputText = inputText.Replace("LogEvents(24", "LogEvents(ng_stat.game.StartBG");
            if (!inputText.Equals(inputTextOld))
            {
                richTextBox1.Text = richTextBox1.Text + "Произведена замена LogEvents(24) в файле " + file + "\n";
            }

            inputTextOld = inputText;
            inputText = inputText.Replace("LogEvents(25", "LogEvents(ng_stat.game.FinishBG");
            if (!inputText.Equals(inputTextOld))
            {
                richTextBox1.Text = richTextBox1.Text + "Произведена замена LogEvents(25) в файле " + file + "\n";
            }

            inputTextOld = inputText;
            inputText = inputText.Replace("LogEvents(26", "LogEvents(ng_stat.game.GotAchievs");
            if (!inputText.Equals(inputTextOld))
            {
                richTextBox1.Text = richTextBox1.Text + "Произведена замена LogEvents(26) в файле " + file + "\n";
            }

            inputTextOld = inputText;
            inputText = inputText.Replace("LogEvents(27", "LogEvents(ng_stat.game.PlayPG");
            if (!inputText.Equals(inputTextOld))
            {
                richTextBox1.Text = richTextBox1.Text + "Произведена замена LogEvents(27) в файле " + file + "\n";
            }

            inputTextOld = inputText;
            inputText = inputText.Replace("LogEvents(28", "LogEvents(ng_stat.game.ExpRoom");
            if (!inputText.Equals(inputTextOld))
            {
                richTextBox1.Text = richTextBox1.Text + "Произведена замена LogEvents(28) в файле " + file + "\n";
            }

            inputTextOld = inputText;
            inputText = inputText.Replace("LogEvents(29", "LogEvents(ng_stat.game.PuzzleInfo");
            if (!inputText.Equals(inputTextOld))
            {
                richTextBox1.Text = richTextBox1.Text + "Произведена замена LogEvents(29) в файле " + file + "\n";
            }

            inputTextOld = inputText;
            inputText = inputText.Replace("LogEvents(30", "LogEvents(ng_stat.game.PuzzleCollected");
            if (!inputText.Equals(inputTextOld))
            {
                richTextBox1.Text = richTextBox1.Text + "Произведена замена LogEvents(30) в файле " + file + "\n";
            }

            inputTextOld = inputText;
            inputText = inputText.Replace("LogEvents(31", "LogEvents(ng_stat.game.SecretRoom");
            if (!inputText.Equals(inputTextOld))
            {
                richTextBox1.Text = richTextBox1.Text + "Произведена замена LogEvents(31) в файле " + file + "\n";
            }

            inputTextOld = inputText;
            inputText = inputText.Replace("LogEvents(32", "LogEvents(ng_stat.game.EndContent");
            if (!inputText.Equals(inputTextOld))
            {
                richTextBox1.Text = richTextBox1.Text + "Произведена замена LogEvents(32) в файле " + file + "\n";
            }

            inputTextOld = inputText;
            inputText = inputText.Replace("LogEvents(33, {game_progress_main", "LogEvents(ng_stat.game.GameProgressMain, {game_progress_main");
            if (!inputText.Equals(inputTextOld))
            {
                richTextBox1.Text = richTextBox1.Text + "Произведена замена LogEvents(33) в файле " + file + "\n";
            }

            inputTextOld = inputText;
            inputText = inputText.Replace("LogEvents(34, {game_progress_bonus", "LogEvents(ng_stat.game.GameProgressBonus, {game_progress_bonus");
            if (!inputText.Equals(inputTextOld))
            {
                richTextBox1.Text = richTextBox1.Text + "Произведена замена LogEvents(34) в файле " + file + "\n";
            }
            inputTextOld = inputText;
            inputText = inputText.Replace("LogEvents(35", "LogEvents(ng_stat.game.Tutorial");
            if (!inputText.Equals(inputTextOld))
            {
                richTextBox1.Text = richTextBox1.Text + "Произведена замена LogEvents(35) в файле " + file + "\n";
            }
            #endregion
            if (!inputFirst.Equals(inputText))
            {
                richTextBox1.Text = richTextBox1.Text + "--------------------------------------------------------\n";
                string regExpr = @"--------------------------------------------------------\n";
                foreach (Match m in Regex.Matches(richTextBox1.Text, regExpr))
                {
                    richTextBox1.SelectionStart = m.Index;
                    richTextBox1.SelectionLength = m.Length;
                    richTextBox1.SelectionColor = Color.Green;
                }
            }
            return inputText;
        }

        public string ReplaceObjDoNotDrop(string inputText, string file)
        {
            string inputFirst = inputText;
            string inputTextOld = inputText;

            #region Replace
            inputText = inputText.Replace("interface.ObjDoNotDrop", "--interface.ObjDoNotDrop");
            if (!inputText.Equals(inputTextOld))
            {
                richTextBox1.Text = richTextBox1.Text + "Произведена замена interface.ObjDoNotDrop в файле " + file + "\n";
            }

            inputTextOld = inputText;
            inputText = inputText.Replace(" ObjDoNotDrop();", "--ObjDoNotDrop();");
            if (!inputText.Equals(inputTextOld))
            {
                richTextBox1.Text = richTextBox1.Text + "Произведена замена ObjDoNotDrop(); в файле " + file + "\n";
            }
            #endregion

            if (!inputFirst.Equals(inputText))
            {
                richTextBox1.Text = richTextBox1.Text + "--------------------------------------------------------\n";
                string regExpr = @"--------------------------------------------------------\n";
                foreach (Match m in Regex.Matches(richTextBox1.Text, regExpr))
                {
                    richTextBox1.SelectionStart = m.Index;
                    richTextBox1.SelectionLength = m.Length;
                    richTextBox1.SelectionColor = Color.Green;
                }
            }
            return inputText;
        }
        public string ReplaceCheckEnergy(string inputText, string file)
        {
            string inputFirst = inputText;
            string inputTextOld = inputText;

            var inputTextLines = inputText.Split("\n");
            foreach (var line in inputTextLines)
            {
                if(line.Contains("function private.get_"))
                {
                    richTextBox1.Text = "Мы хотим заменить это:\n";

                    var startIndexLine = inputText.IndexOf(line); // Начальный индекс строки объявления функции
                    var functionEndIndex = inputText.IndexOf("function p", startIndexLine + line.Length); // Начальный индекс обьявления следующей функции
                    // Обрезаем текущую функцию до следующей
                    var inputTextFunction1 = inputText.Substring(startIndexLine, functionEndIndex - startIndexLine);
                    richTextBox1.Text = richTextBox1.Text + "\n" + inputTextFunction1;

                    //Вырезаем название события, например: "get_compas1"
                    var value = line.Substring(line.IndexOf("get_"), line.IndexOf("(") - line.IndexOf("get_"));
                    //Вставляем проверку энергии 
                    inputText = inputText.Replace(line, line + "\n  if port_common.CheckEnergy(\"" + value + "\") then\n");

                    richTextBox1.Text = richTextBox1.Text + "\n На это: \n";

                    startIndexLine = inputText.IndexOf(line); // Начальный индекс строки объявления функции
                    functionEndIndex = inputText.IndexOf("function p", startIndexLine + line.Length); // Начальный индекс обьявления следующей функции
                    // Обрезаем текущую функцию до следующей
                    var inputTextFunction2 = inputText.Substring(startIndexLine, functionEndIndex - startIndexLine);
                    richTextBox1.Text = richTextBox1.Text + "\n" + inputTextFunction2;
                    richTextBox1.Update();
                    var result = MessageBox.Show("Изменения правильные?", "?", MessageBoxButtons.YesNo);
                    richTextBox1.SelectionStart = richTextBox1.Text.Length;
                    richTextBox1.ScrollToCaret();

                    if (result == DialogResult.Yes)
                    {
                        inputText = inputText.Replace(inputTextFunction1, inputTextFunction2);
                    }
                    else if(result == DialogResult.No)
                    {
                      //  inputText = inputText;
                      ////  ReplaceChange a = new ReplaceChange(inputTextFunction1);
                      //  a.ShowDialog();
                      //  this.Show();
                      //  if (a.DialogResult == DialogResult.OK)
                      //      richTextBox1.Text = richTextBox1.Text + "\nВручную заменили на это: \n" + a.ReturnData();
                    }

                }
                else if(line.Contains("function public.use_"))
                {
                    ////Вырезаем название события, например: "use_compas1"
                    //var value = line.Substring(line.IndexOf("use_"), line.IndexOf("(") - line.IndexOf("use_"));
                    //inputText = inputText.Replace("cmn.CallEventHandler( \""+value+"_inv\" );", "\n  if port_common.CheckEnergy(\"" + value + "\") then\n" +
                    //                                                    "    cmn.CallEventHandler(\"" + value + "_inv\");\n" +
                    //                                                    "    cmn.CallEventHandler(\"" + value + "_beg\");\n"
                    //                                                    );
                    //var index = inputText.IndexOf("\n  if port_common.CheckEnergy(\"" + value + "\") then\n");
                    //var returnTrueIndex = inputText.IndexOf("return true;", index);
                    
                    //inputText = inputText.Insert(returnTrueIndex, "end;\n");
                    //var secondReplaceText = inputText.Substring(index, returnTrueIndex - (index - 100));
                }
                else if(line.Contains("function private.clk_"))
                {
                    //var value = line.Substring(line.IndexOf("clk_"), line.IndexOf("(") - line.IndexOf("clk_"));
                    //inputText = inputText.Replace("cmn.CallEventHandler( \"" + value + "_inv\" );", "\n  if port_common.CheckEnergy(\"" + value + "\") then\n" +
                    //                                                    "    cmn.CallEventHandler(\"" + value + "_inv\");\n" +
                    //                                                    "    cmn.CallEventHandler(\"" + value + "_beg\");\n"
                    //                                                    );
                    //var index = inputText.IndexOf("\n  if port_common.CheckEnergy(\"" + value + "\") then\n");
                    //var returnTrueIndex = inputText.IndexOf("return true;", index);

                    //inputText = inputText.Insert(returnTrueIndex, "end;\n");
                    //var secondReplaceText = inputText.Substring(index, returnTrueIndex - (index - 100));
                }
            }

            if (!inputFirst.Equals(inputText))
            {
                richTextBox1.Text = richTextBox1.Text + "--------------------------------------------------------\n";
                string regExpr = @"--------------------------------------------------------\n";
                foreach (Match m in Regex.Matches(richTextBox1.Text, regExpr))
                {
                    richTextBox1.SelectionStart = m.Index;
                    richTextBox1.SelectionLength = m.Length;
                    richTextBox1.SelectionColor = Color.Green;
                }
            }
            return inputText;
        }
        private void change_ObjDoNotDrop_button_Click(object sender, EventArgs e)
        {
            if (folderName == null)
            {
                richTextBox1.Text = richTextBox1.Text + "error не указана папка assets\n";

                string regExpr = @"error";
                foreach (Match m in Regex.Matches(richTextBox1.Text, regExpr))
                {
                    richTextBox1.SelectionStart = m.Index;
                    richTextBox1.SelectionLength = m.Length;
                    richTextBox1.SelectionColor = Color.Red;
                }

                return;
            }

            string[] allFoundFiles = Directory.GetFiles(folderName, "*.lua", SearchOption.AllDirectories);
            foreach (string file in allFoundFiles)
            {
                string tmp = File.ReadAllText(file);
                if (tmp.IndexOf("ObjDoNotDrop", StringComparison.CurrentCulture) != -1)
                {
                    if (!file.Contains("mod_botcontroller"))
                    {
                        File.Delete(file);
                        File.WriteAllText(file, ReplaceObjDoNotDrop(tmp, file));
                    }
                }
            }
            richTextBox1.Text = richTextBox1.Text + "Замена успешно завершена!\n";
            string regExpr2 = @"--------------------------------------------------------\n";
            foreach (Match m in Regex.Matches(richTextBox1.Text, regExpr2))
            {
                richTextBox1.SelectionStart = m.Index;
                richTextBox1.SelectionLength = m.Length;
                richTextBox1.SelectionColor = Color.Green;
            }
        }

        private void CheckEnergyButton_Click(object sender, EventArgs e)
        {
            List<string> filesArr = new List<string>();

            if (folderName == null)
            {
                richTextBox1.Text = richTextBox1.Text + "error не указана папка assets\n";

                string regExpr = @"error";
                foreach (Match m in Regex.Matches(richTextBox1.Text, regExpr))
                {
                    richTextBox1.SelectionStart = m.Index;
                    richTextBox1.SelectionLength = m.Length;
                    richTextBox1.SelectionColor = Color.Red;
                }

                return;
            }

            string[] allFoundFiles = Directory.GetFiles(folderName, "*.lua", SearchOption.AllDirectories);
            foreach (string file in allFoundFiles)
            {
                string tmp = File.ReadAllText(file);
                if ((tmp.IndexOf("function private.get_", StringComparison.CurrentCulture) != -1) ||
                    (tmp.IndexOf("function public.use_", StringComparison.CurrentCulture) != -1) ||
                    (tmp.IndexOf("function private.clk_", StringComparison.CurrentCulture) != -1)
                    )
                {
                    if (!file.Contains("mg_") && !file.Contains("ho_"))
                    {
                        filesArr.Add(file);
                        richTextBox1.Text = richTextBox1.Text + "\n Добавлен файл: " + file + " \n";
                        //File.Delete(file);
                        //File.WriteAllText(file, ReplaceCheckEnergy(tmp, file));
                    }
                }
            }
            richTextBox1.Text = richTextBox1.Text + "Замена успешно завершена!\n";
            goFilesButton.Visible = true;
            filesArrGlobal = filesArr;
        }

        private void goFilesButton_Click(object sender, EventArgs e)
        {
            ReplaceChange a = new ReplaceChange(filesArrGlobal);
            a.Show();
            this.Show();
        }
    }
}
