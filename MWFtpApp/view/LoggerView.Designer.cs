namespace mwftp.app.view
{
    partial class LoggerView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.RichTextBox listBoxMessages;

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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBoxMessages = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // listBoxMessages
            // 
            this.listBoxMessages.BackColor = System.Drawing.SystemColors.Info;
            this.listBoxMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxMessages.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxMessages.Location = new System.Drawing.Point(0, 0);
            this.listBoxMessages.Name = "listBoxMessages";
            this.listBoxMessages.ReadOnly = true;
            this.listBoxMessages.Size = new System.Drawing.Size(723, 377);
            this.listBoxMessages.TabIndex = 0;
            this.listBoxMessages.Tag = "";
            this.listBoxMessages.Text = "";
            // 
            // LoggerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listBoxMessages);
            this.BackColor = System.Drawing.SystemColors.Info;
            this.Name = "LoggerView";
            this.Size = new System.Drawing.Size(723, 377);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
