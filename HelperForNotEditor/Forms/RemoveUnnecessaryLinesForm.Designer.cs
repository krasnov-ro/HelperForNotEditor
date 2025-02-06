
namespace HelperForNotEditor
{
    partial class RemoveUnnecessaryLinesForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            buttonLoadFile = new System.Windows.Forms.Button();
            buttonGoRemove = new System.Windows.Forms.Button();
            checkBoxGet = new System.Windows.Forms.CheckBox();
            checkBoxClk = new System.Windows.Forms.CheckBox();
            checkBoxUse = new System.Windows.Forms.CheckBox();
            checkBoxWin = new System.Windows.Forms.CheckBox();
            checkBoxOpn = new System.Windows.Forms.CheckBox();
            checkBoxDlg = new System.Windows.Forms.CheckBox();
            richTextBoxConsole = new System.Windows.Forms.RichTextBox();
            labelOtherCriterias = new System.Windows.Forms.Label();
            textBoxNewCriteria = new System.Windows.Forms.TextBox();
            checkBoxForNotEditor = new System.Windows.Forms.CheckBox();
            SuspendLayout();
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // buttonLoadFile
            // 
            buttonLoadFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonLoadFile.Location = new System.Drawing.Point(12, 9);
            buttonLoadFile.Name = "buttonLoadFile";
            buttonLoadFile.Size = new System.Drawing.Size(147, 29);
            buttonLoadFile.TabIndex = 1;
            buttonLoadFile.Text = "Load File";
            buttonLoadFile.UseVisualStyleBackColor = true;
            buttonLoadFile.Click += buttonLoadFile_Click;
            // 
            // buttonGoRemove
            // 
            buttonGoRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonGoRemove.Location = new System.Drawing.Point(12, 247);
            buttonGoRemove.Name = "buttonGoRemove";
            buttonGoRemove.Size = new System.Drawing.Size(147, 29);
            buttonGoRemove.TabIndex = 1;
            buttonGoRemove.Text = "Go";
            buttonGoRemove.UseVisualStyleBackColor = true;
            buttonGoRemove.Click += buttonGoRemove_Click;
            // 
            // checkBoxGet
            // 
            checkBoxGet.AutoSize = true;
            checkBoxGet.Location = new System.Drawing.Point(12, 42);
            checkBoxGet.Name = "checkBoxGet";
            checkBoxGet.Size = new System.Drawing.Size(43, 19);
            checkBoxGet.TabIndex = 3;
            checkBoxGet.Text = "get";
            checkBoxGet.UseVisualStyleBackColor = true;
            // 
            // checkBoxClk
            // 
            checkBoxClk.AutoSize = true;
            checkBoxClk.Location = new System.Drawing.Point(12, 67);
            checkBoxClk.Name = "checkBoxClk";
            checkBoxClk.Size = new System.Drawing.Size(41, 19);
            checkBoxClk.TabIndex = 3;
            checkBoxClk.Text = "clk";
            checkBoxClk.UseVisualStyleBackColor = true;
            // 
            // checkBoxUse
            // 
            checkBoxUse.AutoSize = true;
            checkBoxUse.Location = new System.Drawing.Point(12, 92);
            checkBoxUse.Name = "checkBoxUse";
            checkBoxUse.Size = new System.Drawing.Size(44, 19);
            checkBoxUse.TabIndex = 3;
            checkBoxUse.Text = "use";
            checkBoxUse.UseVisualStyleBackColor = true;
            // 
            // checkBoxWin
            // 
            checkBoxWin.AutoSize = true;
            checkBoxWin.Location = new System.Drawing.Point(12, 117);
            checkBoxWin.Name = "checkBoxWin";
            checkBoxWin.Size = new System.Drawing.Size(45, 19);
            checkBoxWin.TabIndex = 3;
            checkBoxWin.Text = "win";
            checkBoxWin.UseVisualStyleBackColor = true;
            // 
            // checkBoxOpn
            // 
            checkBoxOpn.AutoSize = true;
            checkBoxOpn.Location = new System.Drawing.Point(12, 142);
            checkBoxOpn.Name = "checkBoxOpn";
            checkBoxOpn.Size = new System.Drawing.Size(47, 19);
            checkBoxOpn.TabIndex = 3;
            checkBoxOpn.Text = "opn";
            checkBoxOpn.UseVisualStyleBackColor = true;
            // 
            // checkBoxDlg
            // 
            checkBoxDlg.AutoSize = true;
            checkBoxDlg.Location = new System.Drawing.Point(12, 167);
            checkBoxDlg.Name = "checkBoxDlg";
            checkBoxDlg.Size = new System.Drawing.Size(43, 19);
            checkBoxDlg.TabIndex = 3;
            checkBoxDlg.Text = "dlg";
            checkBoxDlg.UseVisualStyleBackColor = true;
            // 
            // richTextBoxConsole
            // 
            richTextBoxConsole.Location = new System.Drawing.Point(166, 9);
            richTextBoxConsole.Name = "richTextBoxConsole";
            richTextBoxConsole.Size = new System.Drawing.Size(621, 425);
            richTextBoxConsole.TabIndex = 4;
            richTextBoxConsole.Text = "";
            richTextBoxConsole.TextChanged += richTextBoxConsole_TextChanged;
            // 
            // labelOtherCriterias
            // 
            labelOtherCriterias.AutoSize = true;
            labelOtherCriterias.Location = new System.Drawing.Point(12, 193);
            labelOtherCriterias.Name = "labelOtherCriterias";
            labelOtherCriterias.Size = new System.Drawing.Size(113, 15);
            labelOtherCriterias.TabIndex = 5;
            labelOtherCriterias.Text = "other (delimetr = ; ):";
            // 
            // textBoxNewCriteria
            // 
            textBoxNewCriteria.Location = new System.Drawing.Point(12, 218);
            textBoxNewCriteria.Name = "textBoxNewCriteria";
            textBoxNewCriteria.Size = new System.Drawing.Size(147, 23);
            textBoxNewCriteria.TabIndex = 6;
            textBoxNewCriteria.TextChanged += textBoxNewCriteria_TextChanged;
            // 
            // checkBoxForNotEditor
            // 
            checkBoxForNotEditor.AutoSize = true;
            checkBoxForNotEditor.Location = new System.Drawing.Point(12, 291);
            checkBoxForNotEditor.Name = "checkBoxForNotEditor";
            checkBoxForNotEditor.Size = new System.Drawing.Size(99, 19);
            checkBoxForNotEditor.TabIndex = 8;
            checkBoxForNotEditor.Text = "Для notEditor";
            checkBoxForNotEditor.UseVisualStyleBackColor = true;
            // 
            // RemoveUnnecessaryLinesForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(799, 444);
            Controls.Add(checkBoxForNotEditor);
            Controls.Add(textBoxNewCriteria);
            Controls.Add(labelOtherCriterias);
            Controls.Add(richTextBoxConsole);
            Controls.Add(checkBoxDlg);
            Controls.Add(checkBoxOpn);
            Controls.Add(checkBoxWin);
            Controls.Add(checkBoxUse);
            Controls.Add(checkBoxClk);
            Controls.Add(checkBoxGet);
            Controls.Add(buttonGoRemove);
            Controls.Add(buttonLoadFile);
            Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            Name = "RemoveUnnecessaryLinesForm";
            Text = "Lines Deleter";
            Load += RemoveUnnecessaryLinesForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button buttonLoadFile;
        private System.Windows.Forms.Button buttonGoRemove;
        private System.Windows.Forms.CheckBox checkBoxGet;
        private System.Windows.Forms.CheckBox checkBoxClk;
        private System.Windows.Forms.CheckBox checkBoxUse;
        private System.Windows.Forms.CheckBox checkBoxWin;
        private System.Windows.Forms.CheckBox checkBoxOpn;
        private System.Windows.Forms.CheckBox checkBoxDlg;
        private System.Windows.Forms.RichTextBox richTextBoxConsole;
        private System.Windows.Forms.Label labelOtherCriterias;
        private System.Windows.Forms.TextBox textBoxNewCriteria;
        private System.Windows.Forms.CheckBox checkBoxForNotEditor;
    }
}

