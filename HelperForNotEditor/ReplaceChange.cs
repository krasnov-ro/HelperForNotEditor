using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace HelperForNotEditor
{
    public enum EditStuses
    {
        ReEditing,
        NeedToEdit,
        Edited,
        Done
    }

    public class EditedFuncton
    {
        /// <summary>
        /// Имя функции
        /// </summary>
        public string FunctionName { get; set; }
        /// <summary>
        /// Код функции
        /// </summary>
        public string FunctonCode { get; set; }
        /// <summary>
        /// Статус функции
        /// </summary>
        public EditStuses FunctionStatus { get; set; }
    }

    public partial class ReplaceChange : Form
    {
        List<string> filesArr;
        int counter = 0;
        List<EditedFuncton> funcsGet_Global = new List<EditedFuncton>();
        List<EditedFuncton> funcsGet_GlobalOrg = new List<EditedFuncton>();
        List<EditedFuncton> funcsUse_Global = new List<EditedFuncton>();
        List<EditedFuncton> funcsUse_GlobalOrg = new List<EditedFuncton>();
        List<EditedFuncton> funcsClk_Global = new List<EditedFuncton>();
        List<EditedFuncton> funcsClk_GlobalOrg = new List<EditedFuncton>();
        string funcTextGlobal = string.Empty;
        string resultTextFuncGlobal = string.Empty;
        bool changesSaved = true;
        bool richBoxClear = true;
        List<EditedFuncton> getFuncsCallInUseFunc = new List<EditedFuncton>();
        List<EditedFuncton> getFuncsCallInGetFunc = new List<EditedFuncton>();
        List<EditedFuncton> getFuncsCallInClkFunc = new List<EditedFuncton>();

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
                    fileOrgText = fileOrgText.Replace(funcGet_Org.FunctonCode, funcGet_New.FunctonCode);
                }
            }

            for (int num = 0; num < funcsUse_GlobalOrg.Count; num++)
            {
                var funcUse_Org = funcsUse_GlobalOrg[num];
                var funcUse_New = funcsUse_Global[num];
                if (!funcUse_Org.Equals(funcUse_New))
                {
                    fileOrgText = fileOrgText.Replace(funcUse_Org.FunctonCode, funcUse_New.FunctonCode);
                }
            }

            for (int num = 0; num < funcsClk_GlobalOrg.Count; num++)
            {
                var funcClk_Org = funcsClk_GlobalOrg[num];
                var funcClk_New = funcsClk_Global[num];
                if (!funcClk_Org.Equals(funcClk_New))
                {
                    fileOrgText = fileOrgText.Replace(funcClk_Org.FunctonCode, funcClk_New.FunctonCode);
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

                richTextBox1.Text = richTextBox1.Text + "\n считываем use_ функции: \n";
                funcsUse_GlobalOrg = GetFunctions(tmp, "use_", false);
                funcsUse_Global = GetFunctions(tmp, "use_", true);

                richTextBox1.Text = richTextBox1.Text + "\n считываем clk_ функции: \n";
                funcsClk_GlobalOrg = GetFunctions(tmp, "clk_", false);
                funcsClk_Global = GetFunctions(tmp, "clk_", true);

                Go.Visible = false;
                changesSaved = false;

                ButtonsVisible();
            }
        }

        public void ButtonsVisible()
        {
            if (funcsGet_Global.Any(p => p.FunctionStatus == EditStuses.NeedToEdit) ||
                funcsGet_Global.Any(p => p.FunctionStatus == EditStuses.ReEditing))
            {
                goGetButton.Visible = true;
                goUseButton.Visible = false;
                goClkButton.Visible = false;
            }
            else
            {
                if (funcsUse_Global.Any(p => p.FunctionStatus == EditStuses.NeedToEdit) ||
                funcsUse_Global.Any(p => p.FunctionStatus == EditStuses.ReEditing) &&
              !(funcsGet_Global.Any(p => p.FunctionStatus == EditStuses.NeedToEdit) ||
                funcsGet_Global.Any(p => p.FunctionStatus == EditStuses.ReEditing)))
                    goUseButton.Visible = true;
                if (funcsClk_Global.Any(p => p.FunctionStatus == EditStuses.NeedToEdit) ||
                    funcsClk_Global.Any(p => p.FunctionStatus == EditStuses.ReEditing) &&
                  !(funcsGet_Global.Any(p => p.FunctionStatus == EditStuses.NeedToEdit) ||
                    funcsGet_Global.Any(p => p.FunctionStatus == EditStuses.ReEditing)))
                    goClkButton.Visible = true;
            }
            if (!(funcsGet_Global.Any(p => p.FunctionStatus == EditStuses.NeedToEdit) || funcsGet_Global.Any(p => p.FunctionStatus == EditStuses.ReEditing)) &&
               !(funcsUse_Global.Any(p => p.FunctionStatus == EditStuses.NeedToEdit) || funcsGet_Global.Any(p => p.FunctionStatus == EditStuses.ReEditing)) &&
               !(funcsClk_Global.Any(p => p.FunctionStatus == EditStuses.NeedToEdit) || funcsGet_Global.Any(p => p.FunctionStatus == EditStuses.ReEditing)))
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

        /// <summary>
        /// Из кода файла вырываем нужную нам функцию.
        /// </summary>
        /// <param name="inputText"></param>
        /// <param name="code"></param>
        /// <param name="print"></param>
        /// <returns></returns>
        private List<EditedFuncton> GetFunctions(string inputText, string code, bool print)
        {
            List<EditedFuncton> getFunctions = new List<EditedFuncton>();
            var inputTextLines = inputText.Split("\n");
            int endCount = 0; //считаем сколько end надо для закрытия блока кода 
            string value = string.Empty; //название события, например: "get_compas1"
            int startIndexLine = -1; // Начальный индекс строки объявления функции
            int cuurentLineIndex = -1; // Индекс текущей строки
            int functionEndIndex = -1; // Индекс закрытия функции end; или end
            int checkAnimPlay = -1; //проверяем является ли текущий блок кода AnimPlay
            int functionLvl = 0; //проверяем текущее кол-во вложенных функций в блоке кода


            foreach (var line in inputTextLines)
            {
                if (line.Contains("function private." + code))
                {
                    if (!(line.Substring(0, 2) == "--"))
                    {
                        endCount = 1;
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
                        endCount = 1;
                        startIndexLine = inputText.IndexOf(line); // Начальный индекс строки объявления функции

                        //Вырезаем название события, например: "get_compas1"
                        value = line.Substring(line.IndexOf(code), line.IndexOf("(") - line.IndexOf(code));
                        cuurentLineIndex = startIndexLine;
                    }
                }



                if (startIndexLine > -1)
                {
                    cuurentLineIndex = inputText.IndexOf(line, cuurentLineIndex);
                    //////////////////////////////////////////////////////////////////////////////////////////////////////////
                    endCount = CheckTabulationLvlChange(line, endCount);
                    if (endCount == 0) // Функция закрылась
                    {
                        functionEndIndex = inputText.IndexOf(line, cuurentLineIndex) + line.Length;
                        cuurentLineIndex = cuurentLineIndex + line.Length;
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
                }

                if (startIndexLine > -1 && functionEndIndex > startIndexLine)
                {
                    // Обрезаем текущую функцию до следующей
                    var inputTextFunction1 = inputText.Substring(startIndexLine, functionEndIndex - startIndexLine);
                    if (!inputTextFunction1.Contains("if port_common.CheckEnergy("))
                    {
                        if (!value.Contains(code + "collect"))
                        {
                            if (inputTextFunction1.Contains("cmn.SetEventDone("))
                            {
                                getFunctions.Add(new EditedFuncton { FunctionName = value, FunctonCode = inputTextFunction1, FunctionStatus = EditStuses.NeedToEdit }); // Добавляем функцию в скисок функций этого файла
                                if (print == true)
                                    richTextBox1.Text = richTextBox1.Text + value + ";  ";
                            }
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
            getFuncsCallInGetFunc.Clear();
            if (funcsGet_Global.Any(p => p.FunctionStatus == EditStuses.NeedToEdit) &&
                funcsGet_Global.Any(p => p.FunctionStatus == EditStuses.Edited) &&
                funcsGet_Global.Any(p => p.FunctionStatus == EditStuses.ReEditing))
            {
                button1.Text = "Принять";
                button2.Text = "Отклонить";
                goGetButton.Visible = false;
                goUseButton.Visible = true;
                if (funcsUse_Global.Count == 0 && funcsClk_Global.Count == 0)
                    Go.Visible = true;
                return;
            }
            else
            {
                var currentFunction = funcsGet_Global.First(p => p.FunctionStatus == EditStuses.ReEditing || p.FunctionStatus == EditStuses.NeedToEdit);
                if (currentFunction.FunctionStatus != EditStuses.ReEditing)
                    currentFunction.FunctionStatus = EditStuses.Edited;

                richTextBox1.Text = richTextBox1.Text + "----------------------------------------\nРедактируем функцию: \n" + currentFunction.FunctonCode;

                richTextBox1.Text = richTextBox1.Text + "\nПрограмма предлагает автоматически такой вариант: \n";

                var funcText = currentFunction.FunctonCode;
                var lines = funcText.Split("\n");
                var resultTextFunc = string.Empty;
                List<string> editedLines = new List<string>();

                var firstPatternOfFuncCall = "public.get_";
                var secondPatternOfFuncCall = "private.get_";
                var cutFuncText = funcText.Replace(lines[0], "");
                var callingFunc = new EditedFuncton();
                List<string> callingFuncNames = new List<string>();

                if (cutFuncText.Contains(firstPatternOfFuncCall) || cutFuncText.Contains(secondPatternOfFuncCall))
                {
                    var cutFuncLines = cutFuncText.Split("\n");
                    foreach (var cutFuncLine in cutFuncLines)
                    {
                        if (cutFuncLine.Length > 2 && cutFuncLine.Trim().Substring(0, 2) != "--")
                        {
                            if (cutFuncLine.Contains(firstPatternOfFuncCall))
                            {
                                var funcCallStr = cutFuncLine.Substring(cutFuncLine.IndexOf(firstPatternOfFuncCall), (cutFuncLine.IndexOf("()", cutFuncLine.IndexOf(firstPatternOfFuncCall)) - cutFuncLine.IndexOf(firstPatternOfFuncCall)) + 2);
                                callingFunc = funcsGet_Global.Find(p => p.FunctionName == funcCallStr.Replace(firstPatternOfFuncCall, "get_").Replace("()", ""));
                                funcText = funcText.Replace(funcCallStr, funcCallStr.Replace("()", "(true)"));
                                editedLines.Add(funcCallStr.Replace("()", "(true)"));
                            }
                            else if (cutFuncLine.Contains(secondPatternOfFuncCall))
                            {
                                var funcCallStr = cutFuncLine.Substring(cutFuncLine.IndexOf(secondPatternOfFuncCall), (cutFuncLine.IndexOf("()", cutFuncLine.IndexOf(secondPatternOfFuncCall)) - cutFuncLine.IndexOf(secondPatternOfFuncCall)) + 2);
                                callingFunc = funcsGet_Global.Find(p => p.FunctionName == funcCallStr.Replace(secondPatternOfFuncCall, "get_").Replace("()", ""));
                                funcText = funcText.Replace(funcCallStr, funcCallStr.Replace("()", "(true)"));
                                editedLines.Add(funcCallStr.Replace("()", "(true)"));
                            }
                            if (callingFunc.FunctonCode != null)
                            {
                                callingFunc.FunctionStatus = EditStuses.ReEditing;
                                richTextBox1.Text = richTextBox1.Text + "Функция " + callingFunc.FunctionName + " отправлена на перередактирование!\n";
                                callingFuncNames.Add(callingFunc.FunctionName);
                                callingFunc = new EditedFuncton();
                            }
                        }
                    }
                }

                if (currentFunction.FunctionStatus == EditStuses.ReEditing)
                {
                    if (!funcText.Contains("if port_common.CheckEnergy(\"" + currentFunction.FunctionName + "\") then"))
                    {
                        funcText = funcText.Replace(lines[0], lines[0] + "\n  if port_common.CheckEnergy(\"" + currentFunction.FunctionName + "\") then");
                        var lastEndIndex = funcText.LastIndexOf("end");
                        funcText = funcText.Insert(lastEndIndex, "end;\n");
                        funcText = TabulationFunc(funcText);

                        var checkEnergyIndex1 = richTextBox1.Text.IndexOf("if port_common.CheckEnergy(\"" + currentFunction.FunctionName + "\") then");
                        RichTextColor("if port_common.CheckEnergy(\"" + currentFunction.FunctionName + "\") then\n", Color.Green, checkEnergyIndex1);
                    }
                    var textForLine0 = lines[0].Replace("()", "(flag)");
                    resultTextFunc = funcText.Replace(lines[0].Replace("\r", ""), textForLine0).Replace("if port_common.CheckEnergy(\"" + currentFunction.FunctionName + "\") then", "if port_common.CheckEnergy(\"" + currentFunction.FunctionName + "\") or flag then");
                    richTextBox1.Text = richTextBox1.Text + resultTextFunc;
                    currentFunction.FunctionStatus = EditStuses.Edited;

                    var checkEnergyIndex = richTextBox1.Text.IndexOf("if port_common.CheckEnergy(\"" + currentFunction.FunctionName + "\") or flag then\n");
                    RichTextColor("if port_common.CheckEnergy(\"" + currentFunction.FunctionName + "\") or flag then\n", Color.Green, checkEnergyIndex);
                    RichTextColor("(flag)", Color.Green);
                }
                else
                {
                    resultTextFunc = funcText.Replace(lines[0], lines[0] + "\n  if port_common.CheckEnergy(\"" + currentFunction.FunctionName + "\") then");
                    var lastEndIndex = resultTextFunc.LastIndexOf("end");
                    resultTextFunc = resultTextFunc.Insert(lastEndIndex, "end;\n");
                    resultTextFunc = TabulationFunc(resultTextFunc);
                    richTextBox1.Text = richTextBox1.Text + resultTextFunc;
                    var checkEnergyIndex = richTextBox1.Text.IndexOf("if port_common.CheckEnergy(\"" + currentFunction.FunctionName + "\") then");
                    RichTextColor("if port_common.CheckEnergy(\"" + currentFunction.FunctionName + "\") then\n", Color.Green, checkEnergyIndex);
                }

                var returnTrueIndex = richTextBox1.Text.LastIndexOf("end;");
                RichTextColor("end;\n", Color.Green, returnTrueIndex);
                foreach (var callingFuncName in callingFuncNames)
                {
                    RichTextColor("Функция " + callingFuncName + " отправлена на перередактирование!\n", Color.Blue);
                }
                foreach (var editedLine in editedLines)
                {
                    RichTextColor(editedLine, Color.Green);
                }

                richTextBox1.SelectionStart = richTextBox1.Text.Length;
                richTextBox1.ScrollToCaret();

                funcTextGlobal = funcText;
                currentFunction.FunctonCode = resultTextFunc;
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
            if (funcsUse_Global.Any(p => p.FunctionStatus == EditStuses.NeedToEdit) &&
                funcsUse_Global.Any(p => p.FunctionStatus == EditStuses.Edited) &&
                funcsUse_Global.Any(p => p.FunctionStatus == EditStuses.ReEditing))
            {
                button1.Text = "Принять";
                button2.Text = "Отклонить";
                goUseButton.Visible = false;
                if (funcsGet_Global.Count == 0 && funcsClk_Global.Count == 0)
                    Go.Visible = true;
                return;
            }
            else
            {
                var currentFunction = funcsUse_Global.First(p => p.FunctionStatus == EditStuses.ReEditing || p.FunctionStatus == EditStuses.NeedToEdit);
                if (currentFunction.FunctionStatus != EditStuses.ReEditing)
                    currentFunction.FunctionStatus = EditStuses.Edited;

                richTextBox1.Text = richTextBox1.Text + "----------------------------------------\nРедактируем функцию: \n" + currentFunction.FunctonCode;
                richTextBox1.SelectionStart = richTextBox1.Text.Length;
                richTextBox1.ScrollToCaret();

                richTextBox1.Text = richTextBox1.Text + "\nПрограмма предлагает автоматически такой вариант: \n";

                var funcText = currentFunction.FunctonCode;
                var lines = funcText.Split("\n");
                List<string> editedLines = new List<string>();
                var resultTextFunc = string.Empty;
                EditedFuncton callingFunc = new EditedFuncton();
                List<string> callingFuncNames = new List<string>();

                var firstPatternOfFuncCall = "public.get_";
                var secondPatternOfFuncCall = "private.get_";
                var cutFuncText = funcText.Replace(lines[0], "");
                if (cutFuncText.Contains(firstPatternOfFuncCall) || cutFuncText.Contains(secondPatternOfFuncCall))
                {
                    var cutFuncLines = cutFuncText.Split("\n");
                    foreach (var cutFuncLine in cutFuncLines)
                    {
                        if (cutFuncLine.Length > 2 && cutFuncLine.Trim().Substring(0, 2) != "--")
                        {
                            if (cutFuncLine.Contains(firstPatternOfFuncCall))
                            {
                                var funcCallStr = cutFuncLine.Substring(cutFuncLine.IndexOf(firstPatternOfFuncCall), (cutFuncLine.IndexOf("()", cutFuncLine.IndexOf(firstPatternOfFuncCall)) - cutFuncLine.IndexOf(firstPatternOfFuncCall)) + 2);
                                callingFunc = funcsGet_Global.Find(p => p.FunctionName == funcCallStr.Replace(firstPatternOfFuncCall, "get_").Replace("()", ""));
                                funcText = funcText.Replace(funcCallStr, funcCallStr.Replace("()", "(true)"));
                                editedLines.Add(funcCallStr.Replace("()", "(true)"));
                            }
                            else if (cutFuncLine.Contains(secondPatternOfFuncCall))
                            {
                                var funcCallStr = cutFuncLine.Substring(cutFuncLine.IndexOf(secondPatternOfFuncCall), (cutFuncLine.IndexOf("()", cutFuncLine.IndexOf(secondPatternOfFuncCall)) - cutFuncLine.IndexOf(secondPatternOfFuncCall)) + 2);
                                callingFunc = funcsGet_Global.Find(p => p.FunctionName == funcCallStr.Replace(firstPatternOfFuncCall, "get_").Replace("()", ""));
                                funcText = funcText.Replace(funcCallStr, funcCallStr.Replace("()", "(true)"));
                                editedLines.Add(funcCallStr.Replace("()", "(true)"));
                            }
                            if (callingFunc != null && callingFunc.FunctonCode != null)
                            {
                                callingFunc.FunctionStatus = EditStuses.ReEditing;
                                richTextBox1.Text = richTextBox1.Text + "Функция " + callingFunc.FunctionName + " отправлена на перередактирование!\n";
                                callingFuncNames.Add(callingFunc.FunctionName);
                                callingFunc = new EditedFuncton();
                            }
                        }
                    }
                }

                var setEventDoneIndex = funcText.IndexOf("cmn.SetEventDone( \"" + currentFunction.FunctionName + "\" );");
                if (setEventDoneIndex == -1)
                    setEventDoneIndex = funcText.IndexOf("cmn.SetEventDone(\"" + currentFunction.FunctionName + "\");");
                if (setEventDoneIndex == -1)
                    setEventDoneIndex = funcText.IndexOf("cmn.SetEventDone( \"" + currentFunction.FunctionName + "\" )");
                if (setEventDoneIndex == -1)
                    setEventDoneIndex = funcText.IndexOf("cmn.SetEventDone(\"" + currentFunction.FunctionName + "\")");
                if (setEventDoneIndex != -1)
                {
                    var func_endIndex = funcText.IndexOf("func_end = function()");
                    var blockStartText = "func_end = function()";
                    if (func_endIndex == -1)
                    {
                        func_endIndex = funcText.IndexOf("func_end=function()");
                        blockStartText = "func_end=function()";
                    }
                    if (func_endIndex == -1)
                    {
                        func_endIndex = funcText.IndexOf("end_func = function()");
                        blockStartText = "end_func = function()";
                    }
                    if (func_endIndex == -1)
                    {
                        var callEventHandlerIndex = funcText.IndexOf("cmn.CallEventHandler( \"" + currentFunction.FunctionName + "_inv" + "\"");
                        if (callEventHandlerIndex == -1)
                            callEventHandlerIndex = funcText.IndexOf("cmn.CallEventHandler(\"" + currentFunction.FunctionName + "_inv" + "\"");
                        if (callEventHandlerIndex == -1)
                            callEventHandlerIndex = funcText.IndexOf("cmn.CallEventHandler(  \"" + currentFunction.FunctionName + "_inv" + "\"");
                        if (callEventHandlerIndex == -1)
                        {
                            var localitem = funcText.IndexOf("local item = \"inv_");
                            var forvalue = funcText.IndexOf("\r", localitem);
                            var value = funcText.Substring(localitem + 18, forvalue - (localitem + 18)).Replace("\"", "").Replace(";", "");
                            callEventHandlerIndex = funcText.IndexOf("cmn.CallEventHandler( \"use_" + value + "_inv" + "\"");
                            if (callEventHandlerIndex == -1)
                                callEventHandlerIndex = funcText.IndexOf("cmn.CallEventHandler(\"use_" + value + "_inv" + "\"");
                            if (callEventHandlerIndex == -1)
                                callEventHandlerIndex = funcText.IndexOf("cmn.CallEventHandler(  \"use_" + value + "_inv" + "\"");
                            if (callEventHandlerIndex == -1)
                                callEventHandlerIndex = funcText.IndexOf("cmn.CallEventHandler(");
                        }

                        resultTextFunc = funcText.Insert(callEventHandlerIndex, "if port_common.CheckEnergy(\"" + currentFunction.FunctionName + "\") then\n");
                        var checkEnergyIndex = resultTextFunc.IndexOf("if port_common.CheckEnergy(\"" + currentFunction.FunctionName + "\") then\n");
                        var returnTrueIndex = resultTextFunc.IndexOf("return true;", checkEnergyIndex);
                        if (returnTrueIndex == -1)
                        {
                            returnTrueIndex = resultTextFunc.LastIndexOf("AnimPlay(");
                            if (returnTrueIndex == -1)
                                returnTrueIndex = resultTextFunc.LastIndexOf("end;");
                            returnTrueIndex = resultTextFunc.IndexOf("\r", returnTrueIndex) + 1;
                        }
                        var subsringFunc = resultTextFunc.Substring(checkEnergyIndex, returnTrueIndex - checkEnergyIndex);
                        var linesForRichTextBoxColor = new List<string>();

                        checkEnergyIndex = resultTextFunc.IndexOf("if port_common.CheckEnergy(\"" + currentFunction.FunctionName + "\") then\n");
                        returnTrueIndex = resultTextFunc.IndexOf("return true;", checkEnergyIndex);
                        if (returnTrueIndex == -1)
                        {
                            returnTrueIndex = resultTextFunc.LastIndexOf("AnimPlay("); if (returnTrueIndex == -1)
                                returnTrueIndex = resultTextFunc.LastIndexOf("end;");
                            returnTrueIndex = resultTextFunc.IndexOf("\r", returnTrueIndex) + 1;
                        }
                        resultTextFunc = resultTextFunc.Insert(returnTrueIndex, "end;\n");
                        resultTextFunc = TabulationFunc(resultTextFunc);
                        richTextBox1.Text = richTextBox1.Text + resultTextFunc;

                        checkEnergyIndex = richTextBox1.Text.IndexOf("if port_common.CheckEnergy(\"" + currentFunction.FunctionName + "\") then\n");
                        RichTextColor("if port_common.CheckEnergy(\"" + currentFunction.FunctionName + "\") then\n", Color.Green, checkEnergyIndex);

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

                    }
                    else
                    {
                        string funcEndText = TabulationCheckFunc(funcText, blockStartText);
                        richTextBox1.Text = richTextBox1.Text + "Проверку энергии вставим после этой функции:\n" + funcEndText + "\n";

                        resultTextFunc = funcText.Replace(funcEndText, funcEndText + "\nif port_common.CheckEnergy(\"" + currentFunction.FunctionName + "\") then\n");
                        var lastEndIndex = resultTextFunc.LastIndexOf("end;");
                        if (lastEndIndex == -1)
                        {
                            lastEndIndex = resultTextFunc.LastIndexOf("end");
                        }
                        resultTextFunc = resultTextFunc.Insert(lastEndIndex, "end;\n");
                        resultTextFunc = TabulationFunc(resultTextFunc);
                        richTextBox1.Text = richTextBox1.Text + resultTextFunc;

                        var checkEnergyIndex = richTextBox1.Text.IndexOf("if port_common.CheckEnergy(\"" + currentFunction.FunctionName + "\") then");
                        RichTextColor("if port_common.CheckEnergy(\"" + currentFunction.FunctionName + "\") then\n", Color.Green, checkEnergyIndex);
                        var returnTrueIndex = richTextBox1.Text.LastIndexOf("end;");
                        RichTextColor("end;\n", Color.Green, returnTrueIndex);
                        RichTextColor("Проверку энергии вставим после этой функции:\n" + funcEndText, Color.Red);
                    }

                }
                else
                {
                    resultTextFunc = funcText;
                    richTextBox1.Text = richTextBox1.Text + resultTextFunc;
                }

                foreach (var editedLine in editedLines)
                {
                    RichTextColor(editedLine, Color.Green);
                }
                foreach (var callingFuncName in callingFuncNames)
                {
                    RichTextColor("Функция " + callingFuncName + " отправлена на перередактирование!\n", Color.Blue);
                }

                richTextBox1.SelectionStart = richTextBox1.Text.Length;
                richTextBox1.ScrollToCaret();

                funcTextGlobal = funcText;
                currentFunction.FunctonCode = resultTextFunc;
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
            getFuncsCallInClkFunc.Clear();
            if (funcsClk_Global.Any(p => p.FunctionStatus == EditStuses.NeedToEdit) &&
                funcsClk_Global.Any(p => p.FunctionStatus == EditStuses.Edited) &&
                funcsClk_Global.Any(p => p.FunctionStatus == EditStuses.ReEditing))
            {
                button1.Text = "Принять";
                button2.Text = "Отклонить";
                goClkButton.Visible = false;
                if (funcsUse_Global.Count == 0 && funcsGet_Global.Count == 0)
                    Go.Visible = true;
                return;
            }
            else
            {
                var currentFunction = funcsClk_Global.First(p => p.FunctionStatus == EditStuses.ReEditing || p.FunctionStatus == EditStuses.NeedToEdit);
                if (currentFunction.FunctionStatus != EditStuses.ReEditing)
                    currentFunction.FunctionStatus = EditStuses.Edited;

                richTextBox1.Text = richTextBox1.Text + "----------------------------------------\nРедактируем функцию: \n" + currentFunction.FunctonCode;
                richTextBox1.Text = richTextBox1.Text + "\nПрограмма предлагает автоматически такой вариант: \n";

                var funcText = currentFunction.FunctonCode;
                var lines = funcText.Split("\n");
                List<string> editedLines = new List<string>();
                var resultTextFunc = String.Empty;
                EditedFuncton callingFunc = new EditedFuncton();
                List<string> callingFuncNames = new List<string>();

                var firstPatternOfFuncCall = "public.get_";
                var secondPatternOfFuncCall = "private.get_";
                var cutFuncText = funcText.Replace(lines[0], "");
                if (cutFuncText.Contains(firstPatternOfFuncCall) || cutFuncText.Contains(secondPatternOfFuncCall))
                {
                    var cutFuncLines = cutFuncText.Split("\n");
                    foreach (var cutFuncLine in cutFuncLines)
                    {
                        if (cutFuncLine.Length > 2 && cutFuncLine.Trim().Substring(0, 2) != "--")
                        {
                            if (cutFuncLine.Contains(firstPatternOfFuncCall))
                            {
                                var funcCallStr = cutFuncLine.Substring(cutFuncLine.IndexOf(firstPatternOfFuncCall), (cutFuncLine.IndexOf("()", cutFuncLine.IndexOf(firstPatternOfFuncCall)) - cutFuncLine.IndexOf(firstPatternOfFuncCall)) + 2);
                                callingFunc = funcsGet_Global.Find(p => p.FunctionName == funcCallStr.Replace(firstPatternOfFuncCall, "get_").Replace("()", ""));
                                funcText = funcText.Replace(funcCallStr, funcCallStr.Replace("()", "(true)"));
                                editedLines.Add(funcCallStr.Replace("()", "(true)"));
                            }
                            else if (cutFuncLine.Contains(secondPatternOfFuncCall))
                            {
                                var funcCallStr = cutFuncLine.Substring(cutFuncLine.IndexOf(secondPatternOfFuncCall), (cutFuncLine.IndexOf("()", cutFuncLine.IndexOf(secondPatternOfFuncCall)) - cutFuncLine.IndexOf(secondPatternOfFuncCall)) + 2);
                                callingFunc = funcsGet_Global.Find(p => p.FunctionName == funcCallStr.Replace(firstPatternOfFuncCall, "get_").Replace("()", ""));
                                funcText = funcText.Replace(funcCallStr, funcCallStr.Replace("()", "(true)"));
                                editedLines.Add(funcCallStr.Replace("()", "(true)"));
                            }
                            if (callingFunc.FunctonCode != null)
                            {
                                callingFunc.FunctionStatus = EditStuses.ReEditing;
                                richTextBox1.Text = richTextBox1.Text + "Функция " + callingFunc.FunctionName + " отправлена на перередактирование!\n";
                                callingFuncNames.Add(callingFunc.FunctionName);
                                callingFunc = new EditedFuncton();
                            }
                        }
                    }
                }

                var setEventDoneIndex = funcText.IndexOf("cmn.SetEventDone( \"" + currentFunction.FunctionName + "\" );");
                if (setEventDoneIndex == -1)
                    setEventDoneIndex = funcText.IndexOf("cmn.SetEventDone(\"" + currentFunction.FunctionName + "\");");
                if (setEventDoneIndex == -1)
                    setEventDoneIndex = funcText.IndexOf("cmn.SetEventDone( \"" + currentFunction.FunctionName + "\" )");
                if (setEventDoneIndex == -1)
                    setEventDoneIndex = funcText.IndexOf("cmn.SetEventDone(\"" + currentFunction.FunctionName + "\")");
                if (setEventDoneIndex != -1)
                {
                    var callEventHandlerIndex = funcText.IndexOf("cmn.CallEventHandler( \"clk_" + currentFunction.FunctionName + "_beg" + "\" ");
                    if (callEventHandlerIndex == -1)
                        callEventHandlerIndex = funcText.IndexOf("cmn.CallEventHandler(\"clk_" + currentFunction.FunctionName + "_beg" + "\"");
                    if (callEventHandlerIndex == -1)
                    {
                        var func_endIndex = funcText.IndexOf("func_end = function()");
                        var blockStartText = "func_end = function()";
                        if (func_endIndex == -1)
                        {
                            func_endIndex = funcText.IndexOf("func_end=function()");
                            blockStartText = "func_end=function()";
                        }
                        if (func_endIndex == -1)
                        {
                            func_endIndex = funcText.IndexOf("end_func = function()");
                            blockStartText = "end_func = function()";
                        }
                        if (func_endIndex == -1)
                        {
                            resultTextFunc = funcText.Replace(lines[0], lines[0] + "\n  if port_common.CheckEnergy(\"" + currentFunction.FunctionName + "\") then");
                            var lastEndIndex = resultTextFunc.LastIndexOf("end;");
                            if (lastEndIndex == -1)
                            {
                                lastEndIndex = resultTextFunc.LastIndexOf("end");
                            }
                            resultTextFunc = resultTextFunc.Insert(lastEndIndex, "end;\n");
                            resultTextFunc = TabulationFunc(resultTextFunc);
                            richTextBox1.Text = richTextBox1.Text + resultTextFunc;

                            var checkEnergyIndex = richTextBox1.Text.IndexOf("if port_common.CheckEnergy(\"" + currentFunction.FunctionName + "\") then");
                            RichTextColor("if port_common.CheckEnergy(\"" + currentFunction.FunctionName + "\") then\n", Color.Green, checkEnergyIndex);
                            var returnTrueIndex = richTextBox1.Text.LastIndexOf("end;");
                            RichTextColor("end;\n", Color.Green, returnTrueIndex);
                        }
                        else
                        {
                            string funcEndText = TabulationCheckFunc(funcText, blockStartText);
                            richTextBox1.Text = richTextBox1.Text + "Проверку энергии вставим после этой функции:\n" + funcEndText + "\n";

                            resultTextFunc = funcText.Replace(funcEndText, funcEndText + "\nif port_common.CheckEnergy(\"" + currentFunction.FunctionName + "\") then\n");
                            var lastEndIndex = resultTextFunc.LastIndexOf("end;");
                            if (lastEndIndex == -1)
                            {
                                lastEndIndex = resultTextFunc.LastIndexOf("end");
                            }
                            resultTextFunc = resultTextFunc.Insert(lastEndIndex, "end;\n");
                            resultTextFunc = TabulationFunc(resultTextFunc);
                            richTextBox1.Text = richTextBox1.Text + resultTextFunc;

                            var checkEnergyIndex = richTextBox1.Text.IndexOf("if port_common.CheckEnergy(\"" + currentFunction.FunctionName + "\") then");
                            RichTextColor("if port_common.CheckEnergy(\"" + currentFunction.FunctionName + "\") then\n", Color.Green, checkEnergyIndex);
                            var returnTrueIndex = richTextBox1.Text.LastIndexOf("end;");
                            RichTextColor("end;\n", Color.Green, returnTrueIndex);
                            RichTextColor("Проверку энергии вставим после этой функции:\n" + funcEndText, Color.Red);
                        }
                    }
                    else
                    {
                        resultTextFunc = funcText.Insert(callEventHandlerIndex, "\nif port_common.CheckEnergy(\"" + currentFunction.FunctionName + "\") then");
                        var lastEndIndex = resultTextFunc.LastIndexOf("end;");
                        if (lastEndIndex == -1)
                        {
                            lastEndIndex = resultTextFunc.LastIndexOf("end");
                        }
                        resultTextFunc = resultTextFunc.Insert(lastEndIndex, "end;\n");
                        resultTextFunc = TabulationFunc(resultTextFunc);
                        richTextBox1.Text = richTextBox1.Text + resultTextFunc;

                        var checkEnergyIndex = richTextBox1.Text.IndexOf("if port_common.CheckEnergy(\"" + currentFunction.FunctionName + "\") then");
                        RichTextColor("if port_common.CheckEnergy(\"" + currentFunction.FunctionName + "\") then\n", Color.Green, checkEnergyIndex);
                        var returnTrueIndex = richTextBox1.Text.LastIndexOf("end;");
                        RichTextColor("end;\n", Color.Green, returnTrueIndex);
                    }
                }
                else
                {
                    resultTextFunc = funcText;
                    richTextBox1.Text = richTextBox1.Text + resultTextFunc;
                }

                foreach (var editedLine in editedLines)
                {
                    RichTextColor(editedLine, Color.Green);
                }
                foreach (var callingFuncName in callingFuncNames)
                {
                    RichTextColor("Функция " + callingFuncName + " отправлена на перередактирование!\n", Color.Blue);
                }
                richTextBox1.SelectionStart = richTextBox1.Text.Length;
                richTextBox1.ScrollToCaret();

                funcTextGlobal = funcText;
                currentFunction.FunctonCode = resultTextFunc;
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
                var currFunc = funcsGet_Global.First(p => p.FunctionStatus == EditStuses.Edited);
                currFunc.FunctionStatus = EditStuses.Done;
                funcsGet_GlobalOrg.First(p => p.FunctionName == currFunc.FunctionName).FunctionStatus = EditStuses.Done;

                richTextBox1.Text = richTextBox1.Text + "\nВариант одобрен!";
                richTextBox1.SelectionStart = richTextBox1.Text.Length;
                richTextBox1.ScrollToCaret();
            }
            else if (button1.Text.Contains(" use_"))
            {
                var currFunc = funcsUse_Global.First(p => p.FunctionStatus == EditStuses.Edited);
                currFunc.FunctionStatus = EditStuses.Done;
                funcsUse_GlobalOrg.First(p => p.FunctionName == currFunc.FunctionName).FunctionStatus = EditStuses.Done;
                richTextBox1.Text = richTextBox1.Text + "\nВариант одобрен!";
                richTextBox1.SelectionStart = richTextBox1.Text.Length;
                richTextBox1.ScrollToCaret();
            }
            else if (button1.Text.Contains(" clk_"))
            {
                var currFunc = funcsClk_Global.First(p => p.FunctionStatus == EditStuses.Edited);
                currFunc.FunctionStatus = EditStuses.Done;
                funcsClk_GlobalOrg.First(p => p.FunctionName == currFunc.FunctionName).FunctionStatus = EditStuses.Done;
                richTextBox1.Text = richTextBox1.Text + "\nВариант одобрен!";
                richTextBox1.SelectionStart = richTextBox1.Text.Length;
                richTextBox1.ScrollToCaret();
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
                    funcsGet_Global.First(p => p.FunctionStatus == EditStuses.Edited || p.FunctionStatus == EditStuses.ReEditing).FunctonCode = a.ReturnData();
                    richTextBox1.SelectionStart = richTextBox1.Text.Length;
                    richTextBox1.ScrollToCaret();
                }
            }
            else if (button2.Text.Contains(" use_"))
            {
                NoAutoChangesForm a = new NoAutoChangesForm(funcTextGlobal);
                a.ShowDialog();
                this.Show();
                if (a.DialogResult == DialogResult.OK)
                {
                    richTextBox1.Text = richTextBox1.Text + "\nВручную заменили на это: \n" + a.ReturnData();
                    funcsUse_Global.First(p => p.FunctionStatus == EditStuses.Edited || p.FunctionStatus == EditStuses.ReEditing).FunctonCode = a.ReturnData();
                    richTextBox1.SelectionStart = richTextBox1.Text.Length;
                    richTextBox1.ScrollToCaret();
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
                    funcsClk_Global.First(p => p.FunctionStatus == EditStuses.Edited || p.FunctionStatus == EditStuses.ReEditing).FunctonCode = a.ReturnData();
                    richTextBox1.SelectionStart = richTextBox1.Text.Length;
                    richTextBox1.ScrollToCaret();
                }
            }

            button1.Text = "Принять";
            button2.Text = "Отклонить";
            button1.Visible = false;
            button2.Visible = false;

            ButtonsVisible();
        }

        public void RichTextColor(string text, Color color, int startIndex)
        {
            if (startIndex != -1)
            {
                richTextBox1.SelectionStart = startIndex;
                richTextBox1.SelectionLength = text.Length;
                richTextBox1.SelectionColor = color;
            }
        }

        public void RichTextColor(string text, Color color)
        {
            int needTextIndex = richTextBox1.Text.LastIndexOf(text.Replace("\r\n", "\n"));

            if (needTextIndex != -1)
            {
                richTextBox1.SelectionStart = needTextIndex;
                richTextBox1.SelectionLength = text.Replace("\r\n", "\n").Length;
                richTextBox1.SelectionColor = color;
            }
        }

        public string TabulationFunc(string text)
        {
            string resultText = string.Empty;
            int tabulationLvl = 0;
            int tabulationLvlLast = -1;
            var linesSplitText = text.Split("\n");
            foreach (var line in linesSplitText)
            {
                if (line.Contains("function private."))
                {
                    if (!(line.Substring(0, 2) == "--"))
                    {
                        resultText = resultText + IfTabLvl(tabulationLvl) + line.Trim() + "\n";
                        tabulationLvl++;
                    }
                }
                else if (line.Contains("function public."))
                {
                    if (!(line.Substring(0, 2) == "--"))
                    {
                        resultText = resultText + IfTabLvl(tabulationLvl) + line.Trim() + "\n";
                        tabulationLvl++;
                    }
                }
                else
                {
                    tabulationLvlLast = tabulationLvl;
                    tabulationLvl = CheckTabulationLvlChange(line, tabulationLvl);
                    if (tabulationLvlLast - tabulationLvl >= 0)
                    {
                        resultText = resultText + IfTabLvl(tabulationLvl) + line.Trim() + "\n";
                    }
                    else
                    {
                        resultText = resultText + IfTabLvl(tabulationLvl - 1) + line.Trim() + "\n";
                    }
                }
            }
            return resultText;
        }
        public string TabulationCheckFunc(string text, string needCodeBlock)
        {
            string resultText = string.Empty;

            if (needCodeBlock != null && text != null)
            {
                int tabulationLvl = 0;
                int checkAnimPlay = -1;
                text = text.Substring(text.IndexOf(needCodeBlock), text.Length - text.IndexOf(needCodeBlock));
                var linesSplitText = text.Split("\n");
                foreach (var line in linesSplitText)
                {
                    if (!(line.Length > 2 && line.Substring(0, 2) == "--"))
                    {
                        tabulationLvl = CheckTabulationLvlChange(line, tabulationLvl);

                        if (tabulationLvl > 0)
                            resultText = resultText + line + "\n";
                        else if (tabulationLvl == 0)
                        {
                            return resultText + line + "\n";
                        }
                    }
                }
            }

            return resultText;
        }
        public string IfTabLvl(int tabulationLvl)
        {
            return new string(' ', tabulationLvl * 2);
        }
        public int CheckTabulationLvlChange(string line, int tabLvl)
        {
            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (Regex.Match(line, @"\bif\b").Success && (Regex.Match(line, @"\bthen\b").Success && !(Regex.Match(line, @"\belseif\b").Success))
                 || (Regex.Match(line, @"\bfor\b").Success && (Regex.Match(line, @"\bdo\b").Success))
                 || (Regex.Match(line, @"\bwhile\b").Success && (Regex.Match(line, @"\bdo\b").Success))
                 || (Regex.Match(line, @"\bthen\b").Success && !(Regex.Match(line, @"\belseif\b").Success)))
            {
                tabLvl++;
            }
            else if (Regex.Match(line, @"\bfunction\b\s*\(\)").Success)
            {
                tabLvl++;
            }

            if (Regex.Match(line, @"\bend;\b").Success || Regex.Match(line, @"\bend,\b").Success || Regex.Match(line, @"\bend\b").Success)
            {
                tabLvl--;
            }
            return tabLvl;
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
        }

        public static async void SendEmail(string recipientEmail, string senderEmail, string senderPassword, string subject, string message)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(senderEmail);
                    mail.To.Add(recipientEmail);
                    mail.Subject = subject;
                    mail.Body = message;

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 465)) // Замените на адрес и порт вашего SMTP-сервера
                    {
                        smtp.Credentials = new NetworkCredential(senderEmail, "oxif bave fxqu zyuo");
                        smtp.EnableSsl = true;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.UseDefaultCredentials = false;
                        await smtp.SendMailAsync(mail);
                    }
                }

                Console.WriteLine("Письмо успешно отправлено.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при отправке письма: " + ex.Message);
            }
        }
    }
}
