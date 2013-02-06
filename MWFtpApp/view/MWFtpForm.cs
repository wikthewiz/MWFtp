using System;
using System.Windows.Forms;
using mwftp.ftp;
using mwftp.ftp.fileSystem;

namespace mwftp.app.view
{
    public partial class MWFtpForm : Form
    {
        private const int SECONDS_FOR_BALLOON_TIP = 3;
        private bool exit;

        public MWFtpForm()
        {
            InitializeComponent();
            theFtpServer = new FtpServer(new StandardFileSystemClassFactory());
            notifyIcon1.ContextMenuStrip = trayIconMenu;
            Menu = mainMenu;
            ServerEvents.Info += ServerEvents_Info;
            startServer();
        }

        private bool shouldNotify(ServerEvent e, FtpAction action)
        {
            return e.Action == FtpAction.UserLogedIn && !Visible;
        }

        private bool isLoggedInNotify(ServerEvent e)
        {
            return shouldNotify(e, FtpAction.UserLogedIn);
        }

        private bool isLoggedOutNotify(ServerEvent e)
        {
            return shouldNotify(e, FtpAction.UserLogedOut);
        }

        private void ServerEvents_Info(object sender, ServerEvent e)
        {
            if (isLoggedInNotify(e))
            {
                showLoggedInBalloonNotify(e.User);
            }
            else if (isLoggedOutNotify(e))
            {
                showLoggedOutBalloonNotify(e.User);
            }
        }

        private void showLoggedOutBalloonNotify(string userName)
        {
            string message = userName + " had LOGGED OUT";
            showUserActionBalloonNotify(message);
        }

        private void showLoggedInBalloonNotify(string userName)
        {
            string message = userName + " has LOGGED IN";
            showUserActionBalloonNotify(message);
        }

        private void menuItemUsers_Click(object sender, EventArgs e)
        {
            var form = new UsersForm();
            form.ShowDialog();
        }

        private void menuItemSettings_Click(object sender, EventArgs e)
        {
            var form = new ServerSettingsForm();
            form.ShowDialog();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            showFromIconTray();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            hideToIconTray();
        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            exitApplication();
        }

        private void menuItemAbout_Click(object sender, EventArgs e)
        {
            var form = new AboutForm();
            form.ShowDialog();
        }

        private void visaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showFromIconTray();
        }

        private void menuStopServer_Click(object sender, EventArgs e)
        {
            stopServer();
        }

        private void menuStartServer_Click(object sender, EventArgs e)
        {
            startServer();
        }

        private void dispose()
        {
            if (components != null)
            {
                components.Dispose();
            }
            trayIconMenu.Update();
            theFtpServer.Dispose();
        }

        private void hideToIconTray()
        {
            showWarningBalloonNotify("The application has not closed to close use Exit in menu");
            WindowState = FormWindowState.Minimized;
            Hide();
        }

        private void showUserActionBalloonNotify(string message)
        {
            notifyIcon1.ShowBalloonTip(SECONDS_FOR_BALLOON_TIP*1000, "User Action", message, ToolTipIcon.Info);
            ;
        }

        private void showWarningBalloonNotify(string message)
        {
            notifyIcon1.ShowBalloonTip(SECONDS_FOR_BALLOON_TIP*1000, "Warning", message, ToolTipIcon.Warning);
            ;
        }

        private void showFromIconTray()
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void exitApplication()
        {
            exit = true;
            Dispose();
        }

        private void stopServer()
        {
            theFtpServer.Stop();
            setServerNotRunningState();
        }

        private void startServer()
        {
            theFtpServer.Start();
            setServerRunningState();
        }

        private void setServerRunningState()
        {
            stopToolStripMenuItem.Enabled = true;
            startFTPServerToolStripMenuItem.Enabled = false;
            menuItemStartServer.Enabled = false;
            menuItemStopServer.Enabled = true;
        }

        private void setServerNotRunningState()
        {
            stopToolStripMenuItem.Enabled = false;
            startFTPServerToolStripMenuItem.Enabled = true;
            menuItemStartServer.Enabled = true;
            menuItemStopServer.Enabled = false;
        }
    }
}