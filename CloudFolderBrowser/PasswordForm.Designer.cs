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
            this.SuspendLayout();
            // 
            // password_textBox
            // 
            this.password_textBox.Location = new System.Drawing.Point(12, 12);
            this.password_textBox.Name = "password_textBox";
            this.password_textBox.Size = new System.Drawing.Size(310, 20);
            this.password_textBox.TabIndex = 0;
            // 
            // confirmPassword_button
            // 
            this.confirmPassword_button.Location = new System.Drawing.Point(12, 38);
            this.confirmPassword_button.Name = "confirmPassword_button";
            this.confirmPassword_button.Size = new System.Drawing.Size(310, 58);
            this.confirmPassword_button.TabIndex = 1;
            this.confirmPassword_button.Text = "OK";
            this.confirmPassword_button.UseVisualStyleBackColor = true;
            this.confirmPassword_button.Click += new System.EventHandler(this.confirmPassword_button_Click);
            // 
            // PasswordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 108);
            this.Controls.Add(this.confirmPassword_button);
            this.Controls.Add(this.password_textBox);
            this.MaximumSize = new System.Drawing.Size(353, 142);
            this.MinimumSize = new System.Drawing.Size(353, 142);
            this.Name = "PasswordForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Input password";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox password_textBox;
        private System.Windows.Forms.Button confirmPassword_button;
    }
}