﻿
namespace HelperForNotEditor
{
    partial class NoAutoChangesForm
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.goChangesButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(776, 376);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // goChangesButton
            // 
            this.goChangesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.goChangesButton.Location = new System.Drawing.Point(12, 395);
            this.goChangesButton.Name = "goChangesButton";
            this.goChangesButton.Size = new System.Drawing.Size(776, 43);
            this.goChangesButton.TabIndex = 1;
            this.goChangesButton.Text = "Применить";
            this.goChangesButton.UseVisualStyleBackColor = true;
            this.goChangesButton.Click += new System.EventHandler(this.goChangesButton_Click);
            // 
            // NoAutoChangesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.goChangesButton);
            this.Controls.Add(this.richTextBox1);
            this.Name = "NoAutoChangesForm";
            this.Text = "NoAutoChangesForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button goChangesButton;
    }
}