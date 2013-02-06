using System;
using System.IO;
using System.Windows.Forms;
using mwftp.app.controller;
using mwftp.ftp.user;

namespace mwftp.app.view
{
    public partial class UserForm : Form
    {
        private readonly UserFormController controller;

        public UserForm(UserFormController controller)
        {
            InitializeComponent();
            this.controller = controller;
            initiate();
        }

        private void initiate()
        {
            listBoxRights.Items.Clear();
            listBoxRights.Items.AddRange(FileAccessHelper.AccessNames);
            listBoxRights.SelectedItem = FileAccessHelper.GetAccessName(controller.InitStateOfFileAccess);

            textBoxUserName.Text = controller.User.Name;
            textBoxPassword.Text = controller.User.Password;
            textBoxFolder.Text = controller.User.StartingDirectory;
            if (controller.User.MaxNrOfConnections > 0)
            {
                maskedTextBoxMaxConnection.Text = controller.User.MaxNrOfConnections.ToString();
            }

            if (controller.User is NewUser)
            {
                textBoxUserName.ReadOnly = false;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            controller.Cancel();
            Dispose();
        }

        private void buttonFolderSelecter_Click(object sender, EventArgs e)
        {
            setStartDirPath();
        }

        private void setAccessRights()
        {
            if (shouldSetFileAccess())
            {
                controller.User.AccessRights = getAccess();
            }
        }

        private void setUserName()
        {
            if (shouldSetUserName())
            {
                ((NewUser) controller.User).Name = getUserName();
            }
        }

        private void setStartDir()
        {
            if (textBoxFolder.Text != "N/A")
            {
                controller.User.StartingDirectory = getStartDir();
            }
        }

        private void setMaxConnections()
        {
            if (!string.IsNullOrEmpty(maskedTextBoxMaxConnection.Text))
            {
                controller.User.MaxNrOfConnections = getMaxConnections();
            }
        }

        private void setPassword()
        {
            if (!string.IsNullOrEmpty(textBoxPassword.Text))
            {
                controller.User.Password = getPassword();
            }
            else if (controller.User is NewUser)
            {
                throw new ArgumentException("Lösenordet är tomt");
            }
        }

        private bool shouldSetUserName()
        {
            return controller.User is NewUser;
        }

        private bool shouldSetFileAccess()
        {
            return
                controller.User is NewUser ||
                FileAccessHelper.GetAccess((string) listBoxRights.SelectedItem) != StartFileAccess.None;
        }

        private string getUserName()
        {
            if (controller.User is NewUser)
            {
                if (string.IsNullOrEmpty(textBoxUserName.Text))
                {
                    throw new ArgumentException("Användarnamnet är tomt");
                }

                if (UserData.Instance.HasUser(textBoxUserName.Text))
                {
                    throw new ArgumentException("Användarnamnet finns redan");
                }

                return textBoxUserName.Text;
            }
            return controller.User.Name;
        }


        private string getPassword()
        {
            return textBoxPassword.Text;
        }

        private int getMaxConnections()
        {
            return Convert.ToInt32(maskedTextBoxMaxConnection.Text);
        }

        private void setStartDirPath()
        {
            initiateFolderBrowser();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxFolder.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void initiateFolderBrowser()
        {
            folderBrowserDialog1.SelectedPath = controller.User.StartingDirectory;
            folderBrowserDialog1.Description = "Välj den mapp som denna användare (" + controller.User.Name +
                                               ") kommer att loggas in till";
            folderBrowserDialog1.ShowNewFolderButton = true;
        }

        private string getStartDir()
        {
            return textBoxFolder.Text;
        }

        private FileAccess getAccess()
        {
            try
            {
                StartFileAccess access = FileAccessHelper.GetAccess((string) listBoxRights.SelectedItem);
                return FileAccessHelper.Pars(access);
            }
            catch (ArgumentException)
            {
                return FileAccess.Read;
            }
        }

        private void buttonFakeOk_Click(object sender, EventArgs e)
        {
            try
            {
                setUserName();
                setPassword();
                setAccessRights();
                setStartDir();
                setMaxConnections();
                buttonOK.GenerateClick(e);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "MWFtp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}