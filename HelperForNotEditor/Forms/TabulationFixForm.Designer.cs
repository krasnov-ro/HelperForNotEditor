namespace HelperForNotEditor
{
    partial class TabulationFixForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            fileSelectButton = new System.Windows.Forms.Button();
            textBox1 = new System.Windows.Forms.TextBox();
            textBox2 = new System.Windows.Forms.TextBox();
            openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            SuspendLayout();
            // 
            // fileSelectButton
            // 
            fileSelectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            fileSelectButton.Location = new System.Drawing.Point(12, 12);
            fileSelectButton.Name = "fileSelectButton";
            fileSelectButton.Size = new System.Drawing.Size(119, 35);
            fileSelectButton.TabIndex = 0;
            fileSelectButton.Text = "Выберите файл";
            fileSelectButton.UseVisualStyleBackColor = true;
            fileSelectButton.Click += fileSelectButton_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new System.Drawing.Point(144, 0);
            textBox1.Margin = new System.Windows.Forms.Padding(300, 3, 300, 3);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            textBox1.Size = new System.Drawing.Size(980, 280);
            textBox1.TabIndex = 1;
            // 
            // textBox2
            // 
            textBox2.Location = new System.Drawing.Point(144, 291);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            textBox2.Size = new System.Drawing.Size(980, 280);
            textBox2.TabIndex = 2;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // TabulationFixForm
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(1124, 571);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(fileSelectButton);
            Name = "TabulationFixForm";
            Text = "TabulationFixForm";
            SizeChanged += TabulationFixForm_SizeChanged;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button fileSelectButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}