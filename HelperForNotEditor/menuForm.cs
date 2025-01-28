﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HelperForNotEditor
{
    public partial class MenuForm : Form
    {
        private enum Operation
        {
            RemoveUnnecessaryLines,
            ReplaceNumericCodeWithDictionaryCode,
            CommentInterfaceObjDoNotDropCalls,
            AddCheckEnergyCallInAssets,
            CopyF2pFilesToAssets,
            ReplaceSpecifiedLinesInFiles,
            FixTabulation
        }

        private readonly Dictionary<Operation, (string Name, Func<Form> FormCreater)> _operationFormMap
                   = new Dictionary<Operation, (string, Func<Form>)>()
        {
            {
                Operation.RemoveUnnecessaryLines,
                ("Удаление ненужных строк", () => new Form1())
            },
            {
                Operation.ReplaceNumericCodeWithDictionaryCode,
                ("Произвести замену числового кода на словарный код в LogEvents", () => new LogEvents_changer("LogEvents"))
            },
            {
                Operation.CommentInterfaceObjDoNotDropCalls,
                ("Закомментирование всех строк вызова interface.ObjDoNotDrop", () => new LogEvents_changer("ObjDoNotDrop"))
            },
            {
                Operation.AddCheckEnergyCallInAssets,
                ("Проставление в assets вызов функции CheckEnergy()", () => new LogEvents_changer("CheckEnergy"))
            },
            {
                Operation.CopyF2pFilesToAssets,
                ("Перенос f2p файлов в assets (копируем)", () => new f2pFilesForm())
            },
            {
                Operation.ReplaceSpecifiedLinesInFiles,
                ("Замена указанных строк в файлах", () => new ReplacerForm())
            },
            {
                Operation.FixTabulation,
                ("Исправление табуляции строк", () => new TabulationFixForm())
            }
        };

        public MenuForm()
        {
            InitializeComponent();
            InitializeComboBox();
        }

        private void InitializeComboBox()
        {
            foreach (var operation in _operationFormMap)
            {
                comboBox1.Items.Add(operation.Value.Name);
            }
        }

        private void GoWork_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Выберите функцию!");
                return;
            }

            var selectedOperationName = comboBox1.SelectedItem.ToString();
            var selectedOperation = _operationFormMap.FirstOrDefault(p => p.Value.Name == selectedOperationName).Key;
            if (_operationFormMap.TryGetValue(selectedOperation, out var formFactory))
            {
                ShowForm(formFactory.FormCreater());
            }
            else
            {
                MessageBox.Show("Функция не реализована!");
            }
        }

        private void ShowForm(Form form)
        {
            this.Hide();
            form.ShowDialog();
            this.Show();
        }
    }
}
