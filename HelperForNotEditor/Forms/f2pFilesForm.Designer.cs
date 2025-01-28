
namespace HelperForNotEditor
{
    partial class f2pFilesForm
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
            this.sourceButton = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.targetButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog2 = new System.Windows.Forms.FolderBrowserDialog();
            this.goButton = new System.Windows.Forms.Button();
            this.filesListButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // sourceButton
            // 
            this.sourceButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sourceButton.Location = new System.Drawing.Point(13, 14);
            this.sourceButton.Name = "sourceButton";
            this.sourceButton.Size = new System.Drawing.Size(116, 38);
            this.sourceButton.TabIndex = 0;
            this.sourceButton.Text = "Откуда";
            this.sourceButton.UseVisualStyleBackColor = true;
            this.sourceButton.Click += new System.EventHandler(this.sourceButton_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(135, 14);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(449, 424);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // targetButton
            // 
            this.targetButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.targetButton.Location = new System.Drawing.Point(13, 59);
            this.targetButton.Name = "targetButton";
            this.targetButton.Size = new System.Drawing.Size(116, 38);
            this.targetButton.TabIndex = 2;
            this.targetButton.Text = "Куда";
            this.targetButton.UseVisualStyleBackColor = true;
            this.targetButton.Click += new System.EventHandler(this.targetButton_Click);
            // 
            // goButton
            // 
            this.goButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.goButton.Location = new System.Drawing.Point(12, 398);
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(117, 38);
            this.goButton.TabIndex = 3;
            this.goButton.Text = "Начать";
            this.goButton.UseVisualStyleBackColor = true;
            this.goButton.Click += new System.EventHandler(this.goButton_Click);
            // 
            // filesListButton
            // 
            this.filesListButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.filesListButton.Location = new System.Drawing.Point(13, 104);
            this.filesListButton.Name = "filesListButton";
            this.filesListButton.Size = new System.Drawing.Size(116, 38);
            this.filesListButton.TabIndex = 4;
            this.filesListButton.Text = "Список файлов";
            this.filesListButton.UseVisualStyleBackColor = true;
            this.filesListButton.Click += new System.EventHandler(this.filesListButton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // f2pFilesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 450);
            this.Controls.Add(this.filesListButton);
            this.Controls.Add(this.goButton);
            this.Controls.Add(this.targetButton);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.sourceButton);
            this.Name = "f2pFilesForm";
            this.Text = "f2pFilesForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button sourceButton;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button targetButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog2;
        private System.Windows.Forms.Button goButton;
        private System.Windows.Forms.Button filesListButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}