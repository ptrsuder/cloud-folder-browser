namespace CloudFolderBrowser
{
    partial class LoginMegaForm
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
            this.login_textBox = new System.Windows.Forms.TextBox();
            this.warning_label = new System.Windows.Forms.Label();
            this.signin_button = new System.Windows.Forms.Button();
            this.savePassword_checkBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // password_textBox
            // 
            this.password_textBox.Location = new System.Drawing.Point(31, 90);
            this.password_textBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.password_textBox.Name = "password_textBox";
            this.password_textBox.Size = new System.Drawing.Size(362, 23);
            this.password_textBox.TabIndex = 0;
            // 
            // login_textBox
            // 
            this.login_textBox.Location = new System.Drawing.Point(31, 60);
            this.login_textBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.login_textBox.Name = "login_textBox";
            this.login_textBox.Size = new System.Drawing.Size(362, 23);
            this.login_textBox.TabIndex = 0;
            // 
            // warning_label
            // 
            this.warning_label.AutoSize = true;
            this.warning_label.Location = new System.Drawing.Point(28, 10);
            this.warning_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.warning_label.Name = "warning_label";
            this.warning_label.Size = new System.Drawing.Size(266, 15);
            this.warning_label.TabIndex = 1;
            this.warning_label.Text = "Always use a burner account for security reasons.";
            // 
            // signin_button
            // 
            this.signin_button.Location = new System.Drawing.Point(307, 120);
            this.signin_button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.signin_button.Name = "signin_button";
            this.signin_button.Size = new System.Drawing.Size(88, 27);
            this.signin_button.TabIndex = 2;
            this.signin_button.Text = "Sign in";
            this.signin_button.UseVisualStyleBackColor = true;
            this.signin_button.Click += new System.EventHandler(this.signIn_button_Click);
            // 
            // savePassword_checkBox
            // 
            this.savePassword_checkBox.AutoSize = true;
            this.savePassword_checkBox.Location = new System.Drawing.Point(31, 125);
            this.savePassword_checkBox.Name = "savePassword_checkBox";
            this.savePassword_checkBox.Size = new System.Drawing.Size(137, 19);
            this.savePassword_checkBox.TabIndex = 3;
            this.savePassword_checkBox.Text = "Remember password";
            this.savePassword_checkBox.UseVisualStyleBackColor = true;
            // 
            // LoginMegaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 165);
            this.Controls.Add(this.savePassword_checkBox);
            this.Controls.Add(this.signin_button);
            this.Controls.Add(this.warning_label);
            this.Controls.Add(this.login_textBox);
            this.Controls.Add(this.password_textBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximumSize = new System.Drawing.Size(446, 204);
            this.MinimumSize = new System.Drawing.Size(446, 204);
            this.Name = "LoginMegaForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MEGA.NZ";
            this.Load += new System.EventHandler(this.LoginMegaForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox password_textBox;
        private System.Windows.Forms.TextBox login_textBox;
        private System.Windows.Forms.Label warning_label;
        private System.Windows.Forms.Button signin_button;
        private CheckBox savePassword_checkBox;
    }
}