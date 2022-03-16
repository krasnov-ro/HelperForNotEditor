
namespace HelperForNotEditor
{
    partial class LogEvents_changer
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
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.change_ObjDoNotDrop_button = new System.Windows.Forms.Button();
            this.CheckEnergyButton = new System.Windows.Forms.Button();
            this.goFilesButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(13, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(153, 33);
            this.button1.TabIndex = 0;
            this.button1.Text = "Folder";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(185, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(603, 426);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(13, 51);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(153, 33);
            this.button2.TabIndex = 2;
            this.button2.Text = "Change LogEvents";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // change_ObjDoNotDrop_button
            // 
            this.change_ObjDoNotDrop_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.change_ObjDoNotDrop_button.Location = new System.Drawing.Point(13, 91);
            this.change_ObjDoNotDrop_button.Name = "change_ObjDoNotDrop_button";
            this.change_ObjDoNotDrop_button.Size = new System.Drawing.Size(153, 33);
            this.change_ObjDoNotDrop_button.TabIndex = 3;
            this.change_ObjDoNotDrop_button.Text = "Change ObjDoNotDrop";
            this.change_ObjDoNotDrop_button.UseMnemonic = false;
            this.change_ObjDoNotDrop_button.UseVisualStyleBackColor = true;
            this.change_ObjDoNotDrop_button.Click += new System.EventHandler(this.change_ObjDoNotDrop_button_Click);
            // 
            // CheckEnergyButton
            // 
            this.CheckEnergyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CheckEnergyButton.Location = new System.Drawing.Point(13, 130);
            this.CheckEnergyButton.Name = "CheckEnergyButton";
            this.CheckEnergyButton.Size = new System.Drawing.Size(153, 33);
            this.CheckEnergyButton.TabIndex = 4;
            this.CheckEnergyButton.Text = "CheckEnergy";
            this.CheckEnergyButton.UseVisualStyleBackColor = true;
            this.CheckEnergyButton.Click += new System.EventHandler(this.CheckEnergyButton_Click);
            // 
            // goFilesButton
            // 
            this.goFilesButton.BackColor = System.Drawing.Color.LimeGreen;
            this.goFilesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.goFilesButton.Location = new System.Drawing.Point(12, 401);
            this.goFilesButton.Name = "goFilesButton";
            this.goFilesButton.Size = new System.Drawing.Size(153, 37);
            this.goFilesButton.TabIndex = 5;
            this.goFilesButton.Text = "Начать ";
            this.goFilesButton.UseVisualStyleBackColor = false;
            this.goFilesButton.Visible = false;
            this.goFilesButton.Click += new System.EventHandler(this.goFilesButton_Click);
            // 
            // LogEvents_changer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.goFilesButton);
            this.Controls.Add(this.CheckEnergyButton);
            this.Controls.Add(this.change_ObjDoNotDrop_button);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button1);
            this.Name = "LogEvents_changer";
            this.Text = "Replacer";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button change_ObjDoNotDrop_button;
        private System.Windows.Forms.Button CheckEnergyButton;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button goFilesButton;
    }
}