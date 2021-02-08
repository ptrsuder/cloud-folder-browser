
namespace CloudFolderBrowser
{
    partial class ErrorForm
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
            this.message_richTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // message_richTextBox
            // 
            this.message_richTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.message_richTextBox.Location = new System.Drawing.Point(0, 0);
            this.message_richTextBox.Name = "message_richTextBox";
            this.message_richTextBox.ReadOnly = true;
            this.message_richTextBox.Size = new System.Drawing.Size(431, 133);
            this.message_richTextBox.TabIndex = 0;
            this.message_richTextBox.Text = "";
            // 
            // ErrorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 133);
            this.Controls.Add(this.message_richTextBox);
            this.Name = "ErrorForm";
            this.Text = "ErrorForm";
            this.Load += new System.EventHandler(this.ErrorForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox message_richTextBox;
    }
}