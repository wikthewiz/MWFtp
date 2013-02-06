namespace mwftp.app.view
{
    partial class UserForm
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
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.buttonOK = new mwftp.app.view.HelperButton();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.listBoxRights = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxFolder = new System.Windows.Forms.TextBox();
            this.buttonFolderSelecter = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.maskedTextBoxMaxConnection = new System.Windows.Forms.MaskedTextBox();
            this.buttonFakeOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.Location = new System.Drawing.Point(8, 10);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(48, 16);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "&Name:";
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxUserName.Location = new System.Drawing.Point(126, 6);
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.ReadOnly = true;
            this.textBoxUserName.Size = new System.Drawing.Size(192, 20);
            this.textBoxUserName.TabIndex = 1;
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonOK.Location = new System.Drawing.Point(319, 172);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.Visible = false;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonCancel.Location = new System.Drawing.Point(400, 172);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(72, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // listBoxRights
            // 
            this.listBoxRights.FormattingEnabled = true;
            this.listBoxRights.Items.AddRange(new object[] {
            "Läs",
            "Läs&Skriv",
            "Skriv",
            "N/A"});
            this.listBoxRights.Location = new System.Drawing.Point(126, 37);
            this.listBoxRights.Name = "listBoxRights";
            this.listBoxRights.ScrollAlwaysVisible = true;
            this.listBoxRights.Size = new System.Drawing.Size(93, 17);
            this.listBoxRights.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "&Rättighet:";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPassword.Location = new System.Drawing.Point(126, 130);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(192, 20);
            this.textBoxPassword.TabIndex = 5;
            this.textBoxPassword.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "&Lösen ord:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "&Startmapp:";
            // 
            // textBoxFolder
            // 
            this.textBoxFolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxFolder.Location = new System.Drawing.Point(126, 66);
            this.textBoxFolder.MaxLength = 10;
            this.textBoxFolder.Name = "textBoxFolder";
            this.textBoxFolder.Size = new System.Drawing.Size(338, 20);
            this.textBoxFolder.TabIndex = 5;
            // 
            // buttonFolderSelecter
            // 
            this.buttonFolderSelecter.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonFolderSelecter.Location = new System.Drawing.Point(439, 66);
            this.buttonFolderSelecter.Name = "buttonFolderSelecter";
            this.buttonFolderSelecter.Size = new System.Drawing.Size(25, 20);
            this.buttonFolderSelecter.TabIndex = 6;
            this.buttonFolderSelecter.Text = "...";
            this.buttonFolderSelecter.UseVisualStyleBackColor = true;
            this.buttonFolderSelecter.Click += new System.EventHandler(this.buttonFolderSelecter_Click);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 32);
            this.label4.TabIndex = 4;
            this.label4.Text = "&Max samtidiga anslutningar:";
            // 
            // maskedTextBoxMaxConnection
            // 
            this.maskedTextBoxMaxConnection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.maskedTextBoxMaxConnection.Location = new System.Drawing.Point(126, 100);
            this.maskedTextBoxMaxConnection.Mask = "00000";
            this.maskedTextBoxMaxConnection.Name = "maskedTextBoxMaxConnection";
            this.maskedTextBoxMaxConnection.Size = new System.Drawing.Size(38, 20);
            this.maskedTextBoxMaxConnection.TabIndex = 7;
            this.maskedTextBoxMaxConnection.ValidatingType = typeof(int);
            // 
            // buttonFakeOk
            // 
            this.buttonFakeOk.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonFakeOk.Location = new System.Drawing.Point(319, 172);
            this.buttonFakeOk.Name = "buttonFakeOk";
            this.buttonFakeOk.Size = new System.Drawing.Size(75, 23);
            this.buttonFakeOk.TabIndex = 8;
            this.buttonFakeOk.Text = "OK";
            this.buttonFakeOk.UseVisualStyleBackColor = true;
            this.buttonFakeOk.Click += new System.EventHandler(this.buttonFakeOk_Click);
            // 
            // UserForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(480, 205);
            this.ControlBox = false;
            this.Controls.Add(this.buttonFakeOk);
            this.Controls.Add(this.maskedTextBoxMaxConnection);
            this.Controls.Add(this.buttonFolderSelecter);
            this.Controls.Add(this.textBoxFolder);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.textBoxUserName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.listBoxRights);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "UserForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox listBoxRights;
        private System.Windows.Forms.TextBox textBoxUserName;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxFolder;
        private System.Windows.Forms.Button buttonFolderSelecter;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxMaxConnection;
        private System.Windows.Forms.Button buttonFakeOk;
        private HelperButton buttonOK;
    }
}