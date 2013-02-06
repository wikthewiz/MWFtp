using System.IO;
using System.Windows.Forms;
using mwftp.ftp;
using mwftp.ftp.user;

namespace mwftp.app.view
{
    public partial class ConnectionControl : UserControl
    {
        public ConnectionControl()
        {
            InitializeComponent();
            ServerEvents.Info += FtpServerMessageHandler_Info;
        }

        private void FtpServerMessageHandler_Info(object sender, ServerEvent e)
        {
            if (e.Action == FtpAction.UserLogedIn || e.Action == FtpAction.UserLogedOut)
            {
                UserSettings userSetting = UserData.Instance.GetSettings(e.User);
                BeginInvoke(new ConnectionUpdateDelegate(update), new object[] {new ConnectionUser(userSetting)});
            }
        }

        private void update(ConnectionUser connectionUser)
        {
            if (connectionUser.CurrentConnectionCount <= 0)
            {
                removeUser(connectionUser);
            }
            else if (!userIsLogedIn(connectionUser))
            {
                addUser(connectionUser);
            }
            else
            {
                updateCount(connectionUser);
            }
        }

        private void updateCount(ConnectionUser connectionUser)
        {
            listView1.SuspendLayout();
            listView1.Items.RemoveByKey(connectionUser.Name);
            ListViewItem viewItem = createListViewItem(connectionUser);
            listView1.Items.Add(viewItem);
            listView1.ResumeLayout(true);
        }

        private void removeUser(ConnectionUser connectionUser)
        {
            listView1.SuspendLayout();
            listView1.Items.RemoveByKey(connectionUser.Name);
            listView1.ResumeLayout(true);
        }

        private bool userIsLogedIn(ConnectionUser user)
        {
            ListViewItem[] items = listView1.Items.Find(user.Name, false);
            if (items != null)
            {
                return
                    listView1.Items.Find(user.Name, false).Length >= 1 &&
                    user.CurrentConnectionCount > 0;
            }
            return false;
        }

        private void addUser(ConnectionUser user)
        {
            listView1.Items.Add(createListViewItem(user));
        }

        private ListViewItem createListViewItem(ConnectionUser user)
        {
            var subItems = new[]
                               {
                                   user.Name,
                                   user.CurrentConnectionCount.ToString(),
                                   user.LatestCommand
                               };
            var item = new ListViewItem(subItems);
            item.Name = user.Name;
            return item;
        }

        #region Nested type: ConnectionUpdateDelegate

        private delegate void ConnectionUpdateDelegate(ConnectionUser connectionUser);

        #endregion
    }


    public class ConnectionUser
    {
        private readonly string command;
        private readonly UserSettings user;

        public ConnectionUser(UserSettings user)
        {
            this.user = user;
            command = string.Empty;
        }

        public string Password
        {
            get { return user.Password; }
        }

        public string StartingDirectory
        {
            get { return user.StartingDirectory; }
        }

        public FileAccess AccessRights
        {
            get { return user.AccessRights; }
        }

        public string Name
        {
            get { return user.Name; }
        }

        public int MaxNrOfConnections
        {
            get { return user.MaxNrOfConnections; }
        }

        public int CurrentConnectionCount
        {
            get { return UserData.Instance.GetConnectionCounter(Name).Count; }
        }

        public string LatestCommand
        {
            get { return command; }
        }
    }
}