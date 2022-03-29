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
    public partial class ReplaceChange : Form
    {
        List<string> filesArr;
        int counter = 0;
        List<string> funcsGet_Global = new List<string>();
        List<string> funcsGet_GlobalOrg = new List<string>();
        int counterGet = 0;
        List<string> funcsUse_Global = new List<string>();
        List<string> funcsUse_GlobalOrg = new List<string>();
        int counterUse = 0;
        List<string> funcsClk_Global = new List<string>();
        List<string> funcsClk_GlobalOrg = new List<string>();
        int counterClk = 0;
        string funcTextGlobal = string.Empty;
        string resultTextFuncGlobal = string.Empty;
        bool changesSaved = true;
        bool richBoxClear = true;
        List<string> getFuncsCallInUseFunc = new List<string>();

        public ReplaceChange(List<string> replaceText)
        {
            InitializeComponent();
            filesArr = new List<string>();
            filesArr = replaceText;
            counter = 0;
        }

        private void applyChangesButton_Click(object sender, EventArgs e)
        {
            string fileOrgText = File.ReadAllText(filesArr[counter]);

            for (int num = 0; num < funcsGet_GlobalOrg.Count; num++)
            {
                var funcGet_Org = funcsGet_GlobalOrg[num];
                var funcGet_New = funcsGet_Global[num];
                if (!funcGet_Org.Equals(funcGet_New))
                {
                    fileOrgText = fileOrgText.Replace(funcGet_Org, funcGet_New);
                }
            }

            for (int num = 0; num < funcsUse_GlobalOrg.Count; num++)
            {
                var funcUse_Org = funcsUse_GlobalOrg[num];
                var funcUse_New = funcsUse_Global[num];
                if (!funcUse_Org.Equals(funcUse_New))
                {
                    fileOrgText = fileOrgText.Replace(funcUse_Org, funcUse_New);
                }
            }

            for (int num = 0; num < funcsClk_GlobalOrg.Count; num++)
            {
                var funcClk_Org = funcsClk_GlobalOrg[num];
                var funcClk_New = funcsClk_Global[num];
                if (!funcClk_Org.Equals(funcClk_New))
                {
                    fileOrgText = fileOrgText.Replace(funcClk_Org, funcClk_New);
                }
            }

            File.Delete(filesArr[counter]);
            File.WriteAllText(filesArr[counter], fileOrgText);
            richTextBox1.Text = richTextBox1.Text + "\n Файл находящийся по пути: " + filesArr[counter] + " изменен и сохранен!\n";

            counter++;
            applyChangesButton.Visible = false;
            changesSaved = true;
            richBoxClear = true;
        }

        private void Go_Click(object sender, EventArgs e)
        {
            DoGo();
        }

        private void DoGo()
        {
            if (changesSaved == false)
            {
                richTextBox1.Text = richTextBox1.Text + "\n Перед тем как начать работу над следующим файлом сохраните предыдущий!!!\n";
                string regExpr = @"Перед тем как начать работу над следующим файлом сохраните предыдущий!!!";
                foreach (Match m in Regex.Matches(richTextBox1.Text, regExpr))
                {
                    richTextBox1.SelectionStart = m.Index;
                    richTextBox1.SelectionLength = m.Length;
                    richTextBox1.SelectionColor = Color.Red;
                }
                richTextBox1.SelectionStart = richTextBox1.Text.Length;
                richTextBox1.ScrollToCaret();
                return;
            }
            else
            {
                List<string> funcsGet_ = new List<string>();
                List<string> funcsUse_ = new List<string>();
                List<string> funcsClk_ = new List<string>();

                if (filesArr.Count == counter)
                {
                    richTextBox1.Text = richTextBox1.Text + "\n файлы все обработаны!";
                    return;
                }

                string tmp = File.ReadAllText(filesArr[counter]);
                if (richBoxClear == false)
                    richTextBox1.Text = richTextBox1.Text + "Считываем данные из файла: " + filesArr[counter];
                else if (richBoxClear == true)
                    richTextBox1.Text = "Считываем данные из файла: " + filesArr[counter];

                richTextBox1.Text = richTextBox1.Text + "\n считываем get_ функции: \n";
                funcsGet_GlobalOrg = GetFunctions(tmp, "get_", false);
                funcsGet_Global = GetFunctions(tmp, "get_", true);
                counterGet = 0;

                richTextBox1.Text = richTextBox1.Text + "\n считываем use_ функции: \n";
                funcsUse_GlobalOrg = GetFunctions(tmp, "use_", false);
                funcsUse_Global = GetFunctions(tmp, "use_", true);
                counterUse = 0;

                richTextBox1.Text = richTextBox1.Text + "\n считываем clk_ функции: \n";
                funcsClk_GlobalOrg = GetFunctions(tmp, "clk_", false);
                funcsClk_Global = GetFunctions(tmp, "clk_", true);
                counterClk = 0;

                Go.Visible = false;
                changesSaved = false;

                ButtonsVisible();
            }
        }

        public void ButtonsVisible()
        {
            if (funcsGet_Global.Count != 0 && funcsGet_Global.Count != counterGet)
            {
                goGetButton.Visible = true;
                goUseButton.Visible = false;
            }
            if (funcsUse_Global.Count != 0 && funcsUse_Global.Count != counterUse && !(funcsGet_Global.Count != 0 && funcsGet_Global.Count != counterGet))
                goUseButton.Visible = true;
            if (funcsClk_Global.Count != 0 && funcsClk_Global.Count != counterClk)
                goClkButton.Visible = true;
            if ((funcsGet_Global.Count == 0 || funcsGet_Global.Count == counterGet) &&
                (funcsUse_Global.Count == 0 || funcsUse_Global.Count == counterUse) &&
                (funcsClk_Global.Count == 0 || funcsClk_Global.Count == counterClk))
            {
                Go.Visible = true;
                applyChangesButton.Visible = true;
                {
                    if (funcsGet_Global.Count == 0 && funcsUse_Global.Count == 0 && funcsClk_Global.Count == 0)
                    {
                        changesSaved = true;
                        richTextBox1.Update();
                        richTextBox1.SelectionStart = richTextBox1.Text.Length;
                        richTextBox1.ScrollToCaret();
                        counter++;
                        richBoxClear = false;
                        DoGo();

                    }
                }
            }
        }

        private List<string> GetFunctions(string inputText, string code, bool print)
        {
            if (code.Contains("use_")) 
            {
                int i = 9;
            }
            List<string> getFunctions = new List<string>();
            var inputTextLines = inputText.Split("\n");
            int endCount = 0; //считаем сколько end надо для закрытия блока кода 
            string value = string.Empty; //название события, например: "get_compas1"
            int startIndexLine = -1; // Начальный индекс строки объявления функции
            int cuurentLineIndex = -1; // Индекс текущей строки
            int functionEndIndex = -1; // Индекс закрытия функции end; или end

            foreach (var line in inputTextLines)
            {
                if (line.Contains("function private." + code))
                {
                    if (!(line.Substring(0, 2) == "--"))
                    {
                        endCount++;
                        startIndexLine = inputText.IndexOf(line); // Начальный индекс строки объявления функции

                        //Вырезаем название события, например: "get_compas1"
                        value = line.Substring(line.IndexOf(code), line.IndexOf("(") - line.IndexOf(code));
                        cuurentLineIndex = startIndexLine;
                    }
                }
                else if (line.Contains("function public." + code))
                {
                    if (!(line.Substring(0, 2) == "--"))
                    {
                        endCount++;
                        startIndexLine = inputText.IndexOf(line); // Начальный индекс строки объявления функции

                        //Вырезаем название события, например: "get_compas1"
                        value = line.Substring(line.IndexOf(code), line.IndexOf("(") - line.IndexOf(code));
                        cuurentLineIndex = startIndexLine;
                    }
                }
                
                if (startIndexLine > -1)
                {
                    cuurentLineIndex = inputText.IndexOf(line, cuurentLineIndex);
                    if (
                        (line.Contains("if ") && line.Contains(" then"))
                     || (line.Contains("for ") && line.Contains(" do"))
                     || (line.Contains("while ") && line.Contains(" do"))
                     || (line.Contains("= function(")))
                    {
                        endCount++;
                        if (line.Contains(" end\n") || line.Contains(" end;") || line.Contains(" end\r") || line.Contains(" end;\r"))
                        {
                            endCount--;
                        }
                    }
                    else if (line.Trim() == "end;" || line.Trim() == "end" || line.Trim() == "end\r" || line.Trim() == "end;\r")
                    {
                        if (endCount > 0)
                        {
                            endCount--;
                            if (endCount == 0) // Функция закрылась
                            {
                                functionEndIndex = inputText.IndexOf(line, cuurentLineIndex) + line.Length;
                                cuurentLineIndex = cuurentLineIndex + line.Length;
                            }
                        }
                    }
                }

                if(startIndexLine > -1 && functionEndIndex > startIndexLine)
                {
                    // Обрезаем текущую функцию до следующей
                    var inputTextFunction1 = inputText.Substring(startIndexLine, functionEndIndex - startIndexLine);
                    if (!inputTextFunction1.Contains("if port_common.CheckEnergy("))
                    {
                        if (!value.Contains(code + "collect"))
                        {
                            getFunctions.Add(inputTextFunction1); // Добавляем функцию в скисок функций этого файла
                            if (print == true)
                                richTextBox1.Text = richTextBox1.Text + value + ";  ";
                        }
                    }
                    startIndexLine = -1;
                    functionEndIndex = -1;
                    cuurentLineIndex = startIndexLine;
                }
            }

            return getFunctions;
        }

        private void goGetButton_Click(object sender, EventArgs e)
        {
            if (funcsGet_Global.Count == counterGet || funcsGet_Global.Count == 0)
            {
                button1.Text = "Принять";
                button2.Text = "Отклонить";
                goGetButton.Visible = false;
                goUseButton.Visible = true;
                if (funcsUse_Global.Count == 0 && funcsClk_Global.Count == 0)
                    Go.Visible = true;
                counterGet = 0;
                return;
            }
            else
            {
                richTextBox1.Text = richTextBox1.Text + "----------------------------------------\nРедактируем функцию: \n" + funcsGet_Global[counterGet];

                richTextBox1.Text = richTextBox1.Text + "\nПрограмма предлагает автоматически такой вариант: \n";

                var funcText = funcsGet_Global[counterGet];
                var lines = funcText.Split("\n");
                var resultTextFunc = string.Empty;

                var value = lines[0].Substring(lines[0].IndexOf("get_"), lines[0].IndexOf("(") - lines[0].IndexOf("get_")).Trim();
                if (funcText.Contains("-+-+-"))
                {
                    var textForLine0 = lines[0].Replace("()", "(flag)").Replace("-+-+-", "");
                    resultTextFunc = funcText.Replace(lines[0], textForLine0).Replace("if port_common.CheckEnergy(\"" + value + "\") then", "if port_common.CheckEnergy(\"" + value + "\") or flag then");
                    richTextBox1.Text = richTextBox1.Text + resultTextFunc;
                    var checkEnergyIndex = richTextBox1.Text.IndexOf("if port_common.CheckEnergy(\"" + value + "\") or flag then\n");
                    RichTextColor("if port_common.CheckEnergy(\"" + value + "\") or flag then\n", Color.Green, checkEnergyIndex);
                    RichTextColor("(flag)", Color.Green, richTextBox1.Text.IndexOf("(flag)"));
                }
                else
                {
                    resultTextFunc = funcText.Replace(lines[0], lines[0] + "\n  if port_common.CheckEnergy(\"" + value + "\") then\n");
                    var lastEndIndex = resultTextFunc.LastIndexOf("end");
                    resultTextFunc = resultTextFunc.Insert(lastEndIndex, "end;\n");
                    richTextBox1.Text = richTextBox1.Text + resultTextFunc;
                    var checkEnergyIndex = richTextBox1.Text.IndexOf("if port_common.CheckEnergy(\"" + value + "\") then\n");
                    RichTextColor("if port_common.CheckEnergy(\"" + value + "\") then\n", Color.Green, checkEnergyIndex);
                }

                var returnTrueIndex = richTextBox1.Text.LastIndexOf("end;");
                RichTextColor("end;\n", Color.Green, returnTrueIndex);

                richTextBox1.SelectionStart = richTextBox1.Text.Length;
                richTextBox1.ScrollToCaret();

                funcTextGlobal = funcText;
                resultTextFuncGlobal = resultTextFunc;
                button1.Visible = true;
                button1.Text = button1.Text + " get_";
                button2.Visible = true;
                button2.Text = button2.Text + " get_";
                goGetButton.Visible = false;
                goUseButton.Visible = false;
                Go.Visible = false;
                goClkButton.Visible = false;
                applyChangesButton.Visible = false;
            }
        }

        private void goUseButton_Click(object sender, EventArgs e)
        {
            getFuncsCallInUseFunc.Clear();
            if (funcsUse_Global.Count == counterUse || funcsUse_Global.Count == 0)
            {
                button1.Text = "Принять";
                button2.Text = "Отклонить";
                goUseButton.Visible = false;
                if (funcsGet_Global.Count == 0 && funcsClk_Global.Count == 0)
                    Go.Visible = true;
                counterUse = 0;
                return;
            }
            else
            {
                richTextBox1.Text = richTextBox1.Text + "----------------------------------------\nРедактируем функцию: \n" + funcsUse_Global[counterUse];
                richTextBox1.SelectionStart = richTextBox1.Text.Length;
                richTextBox1.ScrollToCaret();

                richTextBox1.Text = richTextBox1.Text + "\nПрограмма предлагает автоматически такой вариант: \n";

                var funcText = funcsUse_Global[counterUse];
                var lines = funcText.Split("\n");
                //Вырезаем название события, например: "use_compas1"
                var value = lines[0].Substring(lines[0].IndexOf("use_"), lines[0].IndexOf("(") - lines[0].IndexOf("use_")).Trim();
                var callEventHandlerIndex = funcText.IndexOf("cmn.CallEventHandler( \"" + value + "_inv" +"\"");
                if (callEventHandlerIndex == -1)
                    callEventHandlerIndex = funcText.IndexOf("cmn.CallEventHandler(\"" + value + "_inv" + "\"");
                if (callEventHandlerIndex == -1)
                    callEventHandlerIndex = funcText.IndexOf("cmn.CallEventHandler(  \"" + value + "_inv" + "\"");

                var resultTextFunc = funcText.Insert(callEventHandlerIndex, "if port_common.CheckEnergy(\"" + value + "\") then\n");
                var checkEnergyIndex = resultTextFunc.IndexOf("if port_common.CheckEnergy(\"" + value + "\") then\n");
                var returnTrueIndex = resultTextFunc.IndexOf("return true;", checkEnergyIndex);
                if (returnTrueIndex == -1)
                {
                    returnTrueIndex = resultTextFunc.LastIndexOf("AnimPlay(");
                    returnTrueIndex = resultTextFunc.IndexOf("\n", returnTrueIndex) + 1;
                }
                var subsringFunc = resultTextFunc.Substring(checkEnergyIndex, returnTrueIndex - checkEnergyIndex);
                var linesForRichTextBoxColor = new List<string>();

                if (subsringFunc.IndexOf("private.get_") < 0)
                {
                    foreach (var findPrivateGetLine in subsringFunc.Split("\n"))
                    {
                        if (findPrivateGetLine.Contains("private.get_"))
                        {
                            if (!findPrivateGetLine.Contains("--p"))
                            {
                                var getFuncValue = findPrivateGetLine.Substring(findPrivateGetLine.IndexOf("get_"), findPrivateGetLine.IndexOf("(") - findPrivateGetLine.IndexOf("get_")).Trim();
                                var pasteStr = "private." + getFuncValue + " (true)";
                                resultTextFunc = resultTextFunc.Replace(findPrivateGetLine, pasteStr);
                                linesForRichTextBoxColor.Add(pasteStr);
                                getFuncsCallInUseFunc.Add(funcsGet_Global[funcsGet_Global.IndexOf(funcsGet_Global.Find(p => p.Contains("private." + getFuncValue)))]);
                            }
                        }
                    }
                }

                checkEnergyIndex = resultTextFunc.IndexOf("if port_common.CheckEnergy(\"" + value + "\") then\n");
                returnTrueIndex = resultTextFunc.IndexOf("return true;", checkEnergyIndex);
                if (returnTrueIndex == -1)
                {
                    returnTrueIndex = resultTextFunc.LastIndexOf("AnimPlay(");
                    returnTrueIndex = resultTextFunc.IndexOf("\n", returnTrueIndex) + 1;
                }
                resultTextFunc = resultTextFunc.Insert(returnTrueIndex, "end;\n");
                richTextBox1.Text = richTextBox1.Text + resultTextFunc;


                checkEnergyIndex = richTextBox1.Text.IndexOf("if port_common.CheckEnergy(\"" + value + "\") then\n");
                RichTextColor("if port_common.CheckEnergy(\"" + value + "\") then\n", Color.Green, checkEnergyIndex);
                int n = 5;
                returnTrueIndex = richTextBox1.Text.IndexOf("return true;", checkEnergyIndex);
                if (returnTrueIndex == -1)
                {
                    returnTrueIndex = richTextBox1.Text.LastIndexOf("AnimPlay(");
                    returnTrueIndex = richTextBox1.Text.IndexOf("\n", returnTrueIndex) + 1;
                }
                foreach (var line in linesForRichTextBoxColor)
                {
                    RichTextColor(line, Color.Green, richTextBox1.Text.IndexOf(line, checkEnergyIndex));
                }
                RichTextColor("end;\n", Color.Green, returnTrueIndex);
                richTextBox1.SelectionStart = richTextBox1.Text.Length;
                richTextBox1.ScrollToCaret();

                funcTextGlobal = funcText;
                resultTextFuncGlobal = resultTextFunc;
                button1.Visible = true;
                button1.Text = button1.Text + " use_";
                button2.Visible = true;
                button2.Text = button2.Text + " use_";
                goGetButton.Visible = false;
                goUseButton.Visible = false;
                Go.Visible = false;
                goClkButton.Visible = false;
                applyChangesButton.Visible = false;
            }
        }

        private void goClkButton_Click(object sender, EventArgs e)
        {
            if (funcsClk_Global.Count == counterClk || funcsClk_Global.Count == 0)
            {
                button1.Text = "Принять";
                button2.Text = "Отклонить";
                goClkButton.Visible = false;
                if (funcsUse_Global.Count == 0 && funcsGet_Global.Count == 0)
                    Go.Visible = true;
                counterClk = 0;
                return;
            }
            else
            {
                richTextBox1.Text = richTextBox1.Text + "----------------------------------------\nРедактируем функцию: \n" + funcsClk_Global[counterClk];
                richTextBox1.Text = richTextBox1.Text + "\nПрограмма предлагает автоматически такой вариант: \n";

                var funcText = funcsClk_Global[counterClk];
                var lines = funcText.Split("\n");
                var value = lines[0].Substring(lines[0].IndexOf("clk_"), lines[0].IndexOf("(") - lines[0].IndexOf("clk_")).Trim();
                var resultTextFunc = funcText.Replace(lines[0], lines[0] + "\n  if port_common.CheckEnergy(\"" + value + "\") then\n");
                var lastEndIndex = resultTextFunc.LastIndexOf("end;");
                if (lastEndIndex == -1)
                {
                    lastEndIndex = resultTextFunc.LastIndexOf("end");
                }
                resultTextFunc = resultTextFunc.Insert(lastEndIndex, "end;\n");
                richTextBox1.Text = richTextBox1.Text + resultTextFunc;

                var checkEnergyIndex = richTextBox1.Text.IndexOf("if port_common.CheckEnergy(\"" + value + "\") then\n");
                RichTextColor("if port_common.CheckEnergy(\"" + value + "\") then\n", Color.Green, checkEnergyIndex);
                var returnTrueIndex = richTextBox1.Text.LastIndexOf("end;");
                RichTextColor("end;\n", Color.Green, returnTrueIndex);
                richTextBox1.SelectionStart = richTextBox1.Text.Length;
                richTextBox1.ScrollToCaret();

                funcTextGlobal = funcText;
                resultTextFuncGlobal = resultTextFunc;
                button1.Visible = true;
                button1.Text = button1.Text + " clk_";
                button2.Visible = true;
                button2.Text = button2.Text + " clk_";
                goGetButton.Visible = false;
                goUseButton.Visible = false;
                Go.Visible = false;
                goClkButton.Visible = false;
                applyChangesButton.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text.Contains(" get_"))
            {
                funcsGet_Global[counterGet] = resultTextFuncGlobal;
                richTextBox1.Text = richTextBox1.Text + "\nВариант одобрен!";
                richTextBox1.SelectionStart = richTextBox1.Text.Length;
                richTextBox1.ScrollToCaret();
                counterGet++;
            }
            else if (button1.Text.Contains(" use_"))
            {
                funcsUse_Global[counterUse] = resultTextFuncGlobal;
                richTextBox1.Text = richTextBox1.Text + "\nВариант одобрен!";
                richTextBox1.SelectionStart = richTextBox1.Text.Length;
                richTextBox1.ScrollToCaret();
                counterUse++;

                if (getFuncsCallInUseFunc.Count != 0)
                {
                    foreach (var func in getFuncsCallInUseFunc)
                    {
                        funcsGet_GlobalOrg.Add(func);
                        funcsGet_Global.Add("-+-+-" + func);
                    }
                }
            }
            else if (button1.Text.Contains(" clk_"))
            {
                funcsClk_Global[counterClk] = resultTextFuncGlobal;
                richTextBox1.Text = richTextBox1.Text + "\nВариант одобрен!";
                richTextBox1.SelectionStart = richTextBox1.Text.Length;
                richTextBox1.ScrollToCaret();
                counterClk++;
            }
            button1.Text = "Принять";
            button2.Text = "Отклонить";
            button1.Visible = false;
            button2.Visible = false;

            ButtonsVisible();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text.Contains(" get_"))
            {
                NoAutoChangesForm a = new NoAutoChangesForm(funcTextGlobal);
                a.ShowDialog();
                this.Show();
                if (a.DialogResult == DialogResult.OK)
                {
                    richTextBox1.Text = richTextBox1.Text + "\nВручную заменили на это: \n" + a.ReturnData();
                    funcsGet_Global[counterGet] = a.ReturnData();
                    richTextBox1.SelectionStart = richTextBox1.Text.Length;
                    richTextBox1.ScrollToCaret();
                }
                counterGet++;
            }
            else if (button2.Text.Contains(" use_"))
            {
                NoAutoChangesForm a = new NoAutoChangesForm(funcTextGlobal);
                a.ShowDialog();
                this.Show();
                if (a.DialogResult == DialogResult.OK)
                {
                    richTextBox1.Text = richTextBox1.Text + "\nВручную заменили на это: \n" + a.ReturnData();
                    funcsUse_Global[counterUse] = a.ReturnData();
                    richTextBox1.SelectionStart = richTextBox1.Text.Length;
                    richTextBox1.ScrollToCaret();
                }
                counterUse++;

                if (getFuncsCallInUseFunc.Count != 0)
                {
                    foreach (var func in getFuncsCallInUseFunc)
                    {
                        funcsGet_GlobalOrg.Add(func);
                        funcsGet_Global.Add("-+-+-" + func);
                    }
                }
            }
            else if (button2.Text.Contains(" clk_"))
            {
                NoAutoChangesForm a = new NoAutoChangesForm(funcTextGlobal);
                a.ShowDialog();
                this.Show();
                if (a.DialogResult == DialogResult.OK)
                {
                    richTextBox1.Text = richTextBox1.Text + "\nВручную заменили на это: \n" + a.ReturnData();
                    funcsClk_Global[counterClk] = a.ReturnData();
                    richTextBox1.SelectionStart = richTextBox1.Text.Length;
                    richTextBox1.ScrollToCaret();
                }
                counterClk++;
            }

            button1.Text = "Принять";
            button2.Text = "Отклонить";
            button1.Visible = false;
            button2.Visible = false;

            ButtonsVisible();
        }

        public void RichTextColor(string text, Color color, int? index)
        {
            if (index == null && index > 0)
            {
                foreach (Match m in Regex.Matches(richTextBox1.Text, @"" + text + ""))
                {
                    richTextBox1.SelectionStart = m.Index;
                    richTextBox1.SelectionLength = m.Length;
                    richTextBox1.SelectionColor = color;
                }
            }
            else if (index != null && index > 0)
            {
                richTextBox1.SelectionStart = (int)index;
                richTextBox1.SelectionLength = text.Length;
                richTextBox1.SelectionColor = color;
            }
        }
    }
}
