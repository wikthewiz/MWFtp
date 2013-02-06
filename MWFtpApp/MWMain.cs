using System;
using System.Windows.Forms;
using mwftp.app.view;
using mwftp.ftp.user;

namespace mwftp.app
{
    internal class MWMain
    {
        [STAThread]
        public static void Main()
        {
            UserData.Instance.Load();
            Application.Run(new MWFtpForm());
            UserData.Instance.Save();
        }
    }
}