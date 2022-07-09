namespace CloudFolderBrowser
{
    partial class ErrorLogForm
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
            this.log_richTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // log_richTextBox
            // 
            this.log_richTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.log_richTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.log_richTextBox.Location = new System.Drawing.Point(0, 0);
            this.log_richTextBox.Name = "log_richTextBox";
            this.log_richTextBox.Size = new System.Drawing.Size(562, 510);
            this.log_richTextBox.TabIndex = 0;
            this.log_richTextBox.Text = "";
            // 
            // ErrorLogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 510);
            this.Controls.Add(this.log_richTextBox);
            this.Name = "ErrorLogForm";
            this.Text = "Error Logs";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox log_richTextBox;
    }
}