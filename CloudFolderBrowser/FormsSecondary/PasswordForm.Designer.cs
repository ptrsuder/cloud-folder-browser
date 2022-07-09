namespace CloudFolderBrowser
{
    partial class PasswordForm
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
            this.password_textBox = new System.Windows.Forms.TextBox();
            this.confirmPassword_button = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // password_textBox
            // 
            this.password_textBox.Location = new System.Drawing.Point(14, 14);
            this.password_textBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.password_textBox.Name = "password_textBox";
            this.password_textBox.Size = new System.Drawing.Size(361, 23);
            this.password_textBox.TabIndex = 0;
            // 
            // confirmPassword_button
            // 
            this.confirmPassword_button.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.confirmPassword_button.Location = new System.Drawing.Point(14, 44);
            this.confirmPassword_button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.confirmPassword_button.Name = "confirmPassword_button";
            this.confirmPassword_button.Size = new System.Drawing.Size(180, 67);
            this.confirmPassword_button.TabIndex = 1;
            this.confirmPassword_button.Text = "OK";
            this.confirmPassword_button.UseVisualStyleBackColor = true;
            this.confirmPassword_button.Click += new System.EventHandler(this.confirmPassword_button_Click);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(201, 44);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(175, 67);
            this.button1.TabIndex = 1;
            this.button1.Text = "CANCEL";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.cancelPassword_button_Click);
            // 
            // PasswordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 119);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.confirmPassword_button);
            this.Controls.Add(this.password_textBox);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximumSize = new System.Drawing.Size(409, 158);
            this.MinimumSize = new System.Drawing.Size(409, 158);
            this.Name = "PasswordForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Link may be protected by password";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox password_textBox;
        private System.Windows.Forms.Button confirmPassword_button;
        private System.Windows.Forms.Button button1;
    }
}