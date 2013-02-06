namespace mwftp.app.view
{
    partial class ServerSettingsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxMaxConnections = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxMaxConnectionUser = new System.Windows.Forms.TextBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.listBoxIpAddresses = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Port:";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(180, 14);
            this.textBoxPort.MaxLength = 5;
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(44, 20);
            this.textBoxPort.TabIndex = 1;
            this.textBoxPort.Text = "21";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Max number of connections:";
            // 
            // textBoxMaxConnections
            // 
            this.textBoxMaxConnections.Location = new System.Drawing.Point(180, 40);
            this.textBoxMaxConnections.MaxLength = 10;
            this.textBoxMaxConnections.Name = "textBoxMaxConnections";
            this.textBoxMaxConnections.Size = new System.Drawing.Size(65, 20);
            this.textBoxMaxConnections.TabIndex = 1;
            this.textBoxMaxConnections.Text = "1000";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Max connections/user:";
            // 
            // textBoxMaxConnectionUser
            // 
            this.textBoxMaxConnectionUser.Location = new System.Drawing.Point(180, 66);
            this.textBoxMaxConnectionUser.MaxLength = 10;
            this.textBoxMaxConnectionUser.Name = "textBoxMaxConnectionUser";
            this.textBoxMaxConnectionUser.Size = new System.Drawing.Size(65, 20);
            this.textBoxMaxConnectionUser.TabIndex = 1;
            this.textBoxMaxConnectionUser.Text = "10";
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(27, 152);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 2;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(170, 152);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Local ip address:";
            // 
            // listBoxIpAddresses
            // 
            this.listBoxIpAddresses.FormattingEnabled = true;
            this.listBoxIpAddresses.Location = new System.Drawing.Point(182, 95);
            this.listBoxIpAddresses.Name = "listBoxIpAddresses";
            this.listBoxIpAddresses.Size = new System.Drawing.Size(90, 17);
            this.listBoxIpAddresses.TabIndex = 4;
            // 
            // ServerSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 187);
            this.Controls.Add(this.listBoxIpAddresses);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.textBoxMaxConnectionUser);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxMaxConnections);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.label1);
            this.Name = "ServerSettingsForm";
            this.Text = "ServerSettings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxMaxConnections;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxMaxConnectionUser;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox listBoxIpAddresses;
    }
}