using System.Windows.Forms;
using System.ComponentModel;
namespace mwftp.app.view
{
    partial class MWFtpForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.Windows.Forms.MainMenu mainMenu;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuItemFile;
        private System.Windows.Forms.MenuItem menuItemUsers;
        private System.Windows.Forms.MenuItem menuItemExit;
        private System.Windows.Forms.MenuItem menuItemHelp;
        private System.Windows.Forms.MenuItem menuItemAbout;
        private System.Windows.Forms.TabControl tabControlConnections;
        private System.Windows.Forms.TabPage tabPageAll;
        private mwftp.ftp.FtpServer theFtpServer = null;
        private MenuItem menuItemSettings;
        private NotifyIcon notifyIcon1;
        private ToolStripMenuItem startFTPServerToolStripMenuItem;
        private ToolStripMenuItem stopToolStripMenuItem;
        private ToolStripMenuItem visaToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ContextMenuStrip trayIconMenu;
        private MenuItem menuItemServer;
        private MenuItem menuItemStopServer;
        private MenuItem menuItemStartServer;
        private IContainer components;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (exit)
                {
                    dispose();
                }
                else
                {
                    hideToIconTray();
                    return;
                }
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MWFtpForm));
            this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.menuItemFile = new System.Windows.Forms.MenuItem();
            this.menuItemUsers = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItemSettings = new System.Windows.Forms.MenuItem();
            this.menuItemExit = new System.Windows.Forms.MenuItem();
            this.menuItemServer = new System.Windows.Forms.MenuItem();
            this.menuItemStopServer = new System.Windows.Forms.MenuItem();
            this.menuItemStartServer = new System.Windows.Forms.MenuItem();
            this.menuItemHelp = new System.Windows.Forms.MenuItem();
            this.menuItemAbout = new System.Windows.Forms.MenuItem();
            this.tabControlConnections = new System.Windows.Forms.TabControl();
            this.tabPageAll = new System.Windows.Forms.TabPage();
            this.allLoggerView1 = new mwftp.app.view.AllLoggerView();
            this.tabPageErrors = new System.Windows.Forms.TabPage();
            this.errorLoggerView1 = new mwftp.app.view.ErrorLoggerView();
            this.tabpageCurrentUsers = new System.Windows.Forms.TabPage();
            this.connectionControl2 = new mwftp.app.view.ConnectionControl();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.startFTPServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.visaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trayIconMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tabControlConnections.SuspendLayout();
            this.tabPageAll.SuspendLayout();
            this.tabPageErrors.SuspendLayout();
            this.tabpageCurrentUsers.SuspendLayout();
            this.trayIconMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemFile,
            this.menuItemServer,
            this.menuItemHelp});
            // 
            // menuItemFile
            // 
            this.menuItemFile.Index = 0;
            this.menuItemFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemUsers,
            this.menuItem4,
            this.menuItemSettings,
            this.menuItemExit});
            this.menuItemFile.Text = "&File";
            // 
            // menuItemUsers
            // 
            this.menuItemUsers.Index = 0;
            this.menuItemUsers.Text = "&Users...";
            this.menuItemUsers.Click += new System.EventHandler(this.menuItemUsers_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 1;
            this.menuItem4.Text = "-";
            // 
            // menuItemSettings
            // 
            this.menuItemSettings.Index = 2;
            this.menuItemSettings.Text = "Settings...";
            this.menuItemSettings.Click += new System.EventHandler(this.menuItemSettings_Click);
            // 
            // menuItemExit
            // 
            this.menuItemExit.Index = 3;
            this.menuItemExit.Text = "&Exit";
            this.menuItemExit.Click += new System.EventHandler(this.menuExit_Click);
            // 
            // menuItemServer
            // 
            this.menuItemServer.Index = 1;
            this.menuItemServer.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemStopServer,
            this.menuItemStartServer});
            this.menuItemServer.Text = "&Server";
            // 
            // menuItemStopServer
            // 
            this.menuItemStopServer.Index = 0;
            this.menuItemStopServer.Text = "Stop";
            this.menuItemStopServer.Click += new System.EventHandler(this.menuStopServer_Click);
            // 
            // menuItemStartServer
            // 
            this.menuItemStartServer.Index = 1;
            this.menuItemStartServer.Text = "Start";
            this.menuItemStartServer.Click += new System.EventHandler(this.menuStartServer_Click);
            // 
            // menuItemHelp
            // 
            this.menuItemHelp.Index = 2;
            this.menuItemHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemAbout});
            this.menuItemHelp.Text = "&Help";
            // 
            // menuItemAbout
            // 
            this.menuItemAbout.Index = 0;
            this.menuItemAbout.Text = "&About...";
            this.menuItemAbout.Click += new System.EventHandler(this.menuItemAbout_Click);
            // 
            // tabControlConnections
            // 
            this.tabControlConnections.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControlConnections.Controls.Add(this.tabPageAll);
            this.tabControlConnections.Controls.Add(this.tabPageErrors);
            this.tabControlConnections.Controls.Add(this.tabpageCurrentUsers);
            this.tabControlConnections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlConnections.Location = new System.Drawing.Point(0, 0);
            this.tabControlConnections.Name = "tabControlConnections";
            this.tabControlConnections.SelectedIndex = 0;
            this.tabControlConnections.Size = new System.Drawing.Size(731, 406);
            this.tabControlConnections.TabIndex = 1;
            // 
            // tabPageAll
            // 
            this.tabPageAll.Controls.Add(this.allLoggerView1);
            this.tabPageAll.Location = new System.Drawing.Point(4, 25);
            this.tabPageAll.Name = "tabPageAll";
            this.tabPageAll.Size = new System.Drawing.Size(723, 377);
            this.tabPageAll.TabIndex = 0;
            this.tabPageAll.Text = "All";
            // 
            // allLoggerView1
            // 
            this.allLoggerView1.BackColor = System.Drawing.SystemColors.Info;
            this.allLoggerView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.allLoggerView1.Location = new System.Drawing.Point(0, 0);
            this.allLoggerView1.Name = "allLoggerView1";
            this.allLoggerView1.Size = new System.Drawing.Size(723, 377);
            this.allLoggerView1.TabIndex = 0;
            // 
            // tabPageErrors
            // 
            this.tabPageErrors.BackColor = System.Drawing.SystemColors.Info;
            this.tabPageErrors.Controls.Add(this.errorLoggerView1);
            this.tabPageErrors.Location = new System.Drawing.Point(4, 25);
            this.tabPageErrors.Name = "tabPageErrors";
            this.tabPageErrors.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageErrors.Size = new System.Drawing.Size(723, 377);
            this.tabPageErrors.TabIndex = 2;
            this.tabPageErrors.Text = "Errors";
            // 
            // errorLoggerView1
            // 
            this.errorLoggerView1.BackColor = System.Drawing.SystemColors.Info;
            this.errorLoggerView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.errorLoggerView1.Location = new System.Drawing.Point(3, 3);
            this.errorLoggerView1.Name = "errorLoggerView1";
            this.errorLoggerView1.Size = new System.Drawing.Size(717, 371);
            this.errorLoggerView1.TabIndex = 0;
            // 
            // tabpageCurrentUsers
            // 
            this.tabpageCurrentUsers.Controls.Add(this.connectionControl2);
            this.tabpageCurrentUsers.Location = new System.Drawing.Point(4, 25);
            this.tabpageCurrentUsers.Name = "tabpageCurrentUsers";
            this.tabpageCurrentUsers.Padding = new System.Windows.Forms.Padding(3);
            this.tabpageCurrentUsers.Size = new System.Drawing.Size(723, 377);
            this.tabpageCurrentUsers.TabIndex = 1;
            this.tabpageCurrentUsers.Text = "Connected Users";
            this.tabpageCurrentUsers.UseVisualStyleBackColor = true;
            // 
            // connectionControl2
            // 
            this.connectionControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.connectionControl2.Location = new System.Drawing.Point(3, 3);
            this.connectionControl2.Name = "connectionControl2";
            this.connectionControl2.Size = new System.Drawing.Size(717, 371);
            this.connectionControl2.TabIndex = 0;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipText = "Dubbel klicka";
            this.notifyIcon1.BalloonTipTitle = "FTP Server";
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // startFTPServerToolStripMenuItem
            // 
            this.startFTPServerToolStripMenuItem.Name = "startFTPServerToolStripMenuItem";
            this.startFTPServerToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.startFTPServerToolStripMenuItem.Text = "Starta";
            this.startFTPServerToolStripMenuItem.Click += new System.EventHandler(this.menuStartServer_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.stopToolStripMenuItem.Text = "Stoppa";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.menuStopServer_Click);
            // 
            // visaToolStripMenuItem
            // 
            this.visaToolStripMenuItem.Name = "visaToolStripMenuItem";
            this.visaToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.visaToolStripMenuItem.Text = "Visa";
            this.visaToolStripMenuItem.Click += new System.EventHandler(this.visaToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.menuExit_Click);
            // 
            // trayIconMenu
            // 
            this.trayIconMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startFTPServerToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.visaToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.trayIconMenu.Name = "trayIconMenu";
            this.trayIconMenu.Size = new System.Drawing.Size(112, 92);
            // 
            // MWFtpForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(731, 406);
            this.Controls.Add(this.tabControlConnections);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MWFtpForm";
            this.Text = "FTP Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.tabControlConnections.ResumeLayout(false);
            this.tabPageAll.ResumeLayout(false);
            this.tabPageErrors.ResumeLayout(false);
            this.tabpageCurrentUsers.ResumeLayout(false);
            this.trayIconMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TabPage tabpageCurrentUsers;
        private ConnectionControl connectionControl2;
        private AllLoggerView allLoggerView1;
        private TabPage tabPageErrors;
        private ErrorLoggerView errorLoggerView1;
    }
}