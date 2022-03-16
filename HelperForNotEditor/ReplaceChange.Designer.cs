
namespace HelperForNotEditor
{
    partial class ReplaceChange
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
            this.label1 = new System.Windows.Forms.Label();
            this.applyChangesButton = new System.Windows.Forms.Button();
            this.Go = new System.Windows.Forms.Button();
            this.goGetButton = new System.Windows.Forms.Button();
            this.goUseButton = new System.Windows.Forms.Button();
            this.goClkButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 28);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(776, 598);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Change replace";
            // 
            // applyChangesButton
            // 
            this.applyChangesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.applyChangesButton.Location = new System.Drawing.Point(685, 632);
            this.applyChangesButton.Name = "applyChangesButton";
            this.applyChangesButton.Size = new System.Drawing.Size(103, 36);
            this.applyChangesButton.TabIndex = 2;
            this.applyChangesButton.Text = "Apply changes";
            this.applyChangesButton.UseVisualStyleBackColor = true;
            this.applyChangesButton.Click += new System.EventHandler(this.applyChangesButton_Click);
            // 
            // Go
            // 
            this.Go.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Go.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Go.Location = new System.Drawing.Point(13, 633);
            this.Go.Name = "Go";
            this.Go.Size = new System.Drawing.Size(80, 35);
            this.Go.TabIndex = 3;
            this.Go.Text = "Next file";
            this.Go.UseVisualStyleBackColor = true;
            this.Go.Click += new System.EventHandler(this.Go_Click);
            // 
            // goGetButton
            // 
            this.goGetButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.goGetButton.Location = new System.Drawing.Point(99, 633);
            this.goGetButton.Name = "goGetButton";
            this.goGetButton.Size = new System.Drawing.Size(80, 35);
            this.goGetButton.TabIndex = 4;
            this.goGetButton.Text = "go get_";
            this.goGetButton.UseVisualStyleBackColor = true;
            this.goGetButton.Visible = false;
            this.goGetButton.Click += new System.EventHandler(this.goGetButton_Click);
            // 
            // goUseButton
            // 
            this.goUseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.goUseButton.Location = new System.Drawing.Point(185, 633);
            this.goUseButton.Name = "goUseButton";
            this.goUseButton.Size = new System.Drawing.Size(80, 35);
            this.goUseButton.TabIndex = 5;
            this.goUseButton.Text = "go use_";
            this.goUseButton.UseVisualStyleBackColor = true;
            this.goUseButton.Visible = false;
            this.goUseButton.Click += new System.EventHandler(this.goUseButton_Click);
            // 
            // goClkButton
            // 
            this.goClkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.goClkButton.Location = new System.Drawing.Point(271, 633);
            this.goClkButton.Name = "goClkButton";
            this.goClkButton.Size = new System.Drawing.Size(80, 35);
            this.goClkButton.TabIndex = 6;
            this.goClkButton.Text = "go clk_";
            this.goClkButton.UseVisualStyleBackColor = true;
            this.goClkButton.Visible = false;
            this.goClkButton.Click += new System.EventHandler(this.goClkButton_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Lime;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(358, 633);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 35);
            this.button1.TabIndex = 7;
            this.button1.Text = "Принять";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Red;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(447, 633);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(83, 35);
            this.button2.TabIndex = 8;
            this.button2.Text = "Отклонить";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ReplaceChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 670);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.goClkButton);
            this.Controls.Add(this.goUseButton);
            this.Controls.Add(this.goGetButton);
            this.Controls.Add(this.Go);
            this.Controls.Add(this.applyChangesButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox1);
            this.Name = "ReplaceChange";
            this.Text = "ReplaceChange";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button applyChangesButton;
        private System.Windows.Forms.Button Go;
        private System.Windows.Forms.Button goGetButton;
        private System.Windows.Forms.Button goUseButton;
        private System.Windows.Forms.Button goClkButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}