using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using mwftp.app.controller;
using mwftp.ftp.user;

namespace mwftp.app.view
{
    public class UsersForm : Form
    {
        #region Member Variables

        private readonly Container components;
        private Button buttonAddUser;
        private Button buttonEdit;
        private Button buttonOK;
        private Button buttonRemoveUser;
        private ColumnHeader columnHeaderAccess;
        private ColumnHeader columnHeaderConnections;
        private ColumnHeader columnHeaderDirectory;
        private ColumnHeader columnHeaderUser;
        private GroupBox groupBoxUsers;
        private ListView listViewUsers;

        #endregion

        public UsersForm()
        {
            InitializeComponent();

            loadUsers();
        }

        private void loadUsers()
        {
            listViewUsers.Items.Clear();
            foreach (UserSettings  user in UserData.Instance.UsersSettings)
            {
                addUserToList(user);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private void buttonAddUser_Click(object sender, EventArgs e)
        {
            try
            {
                UserFormController controller = createNewUserController();
                var userForm = new UserForm(controller);
                if (userForm.ShowDialog() == DialogResult.OK)
                {
                    addUser(controller.User);
                    listViewUsers.SelectedItems.Clear();
                    ListViewItem listViewUser = addUserToList(controller.User);
                    listViewUser.Selected = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private UserFormController createNewUserController()
        {
            return new UserFormController(new NewUser());
        }

        private ListViewItem addUserToList(UserSettings user)
        {
            var subItems = new[]
                               {
                                   user.Name,
                                   user.StartingDirectory,
                                   user.AccessRights.ToString(),
                                   user.MaxNrOfConnections.ToString()
                               };
            return listViewUsers.Items.Add(new ListViewItem(subItems));
        }

        private void buttonRemoveUser_Click(object sender, EventArgs e)
        {
            try
            {
                listViewUsers.SuspendLayout();
                for (int nIndex = 0; nIndex < listViewUsers.SelectedItems.Count; nIndex++)
                {
                    ListViewItem item = listViewUsers.SelectedItems[nIndex];

                    string user = item.SubItems[0].Text;
                    deleteUser(user);
                    listViewUsers.Items.Remove(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            listViewUsers.ResumeLayout(true);
        }

        private void deleteUser(string user)
        {
            UserData.Instance.Remove(user);
            UserData.Instance.Save();
        }

        private void addUser(UserSettings user)
        {
            UserData.Instance.Add(user);
            UserData.Instance.Save();
        }

        private void listViewUsers_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            SuspendLayout();
            switch (listViewUsers.SelectedItems.Count)
            {
                case 0:
                    setButtonStateNoSelected();
                    break;
                case 1:
                    setButtonStateOneSelected();
                    break;
                default:
                    setButtonStateManySelected();
                    break;
            }
            ResumeLayout(true);
        }

        private void setButtonStateOneSelected()
        {
            buttonAddUser.Enabled = true;
            buttonRemoveUser.Enabled = true;
            buttonEdit.Enabled = true;
        }

        private void setButtonStateNoSelected()
        {
            buttonAddUser.Enabled = true;
            buttonRemoveUser.Enabled = false;
            buttonEdit.Enabled = false;
        }

        private void setButtonStateManySelected()
        {
            buttonAddUser.Enabled = true;
            buttonRemoveUser.Enabled = true;
            buttonEdit.Enabled = true;
        }

        private void setAccessForEachSelected(FileAccess access)
        {
            foreach (ListViewItem item in listViewUsers.SelectedItems)
            {
                string user = item.Text;
                UserSettings userSetting = UserData.Instance.GetSettings(user);
                userSetting.AccessRights = access;
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            try
            {
                UserFormController controller = createEditController();
                var userForm = new UserForm(controller);

                if (userForm.ShowDialog() == DialogResult.OK)
                {
                    UserData.Instance.Save();
                    loadUsers();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private UserFormController createEditController()
        {
            if (listViewUsers.SelectedItems.Count > 1)
            {
                return createMultiEditController();
            }
            else if (listViewUsers.SelectedItems.Count == 1)
            {
                return createSingleEditController();
            }
            else
            {
                throw new ArgumentException("Ingen användare vald");
            }
        }

        private UserFormController createSingleEditController()
        {
            UserSettings user = UserData.Instance.GetSettings(listViewUsers.SelectedItems[0].Text);
            return new UserFormController(user);
        }

        private UserFormController createMultiEditController()
        {
            var users = new List<UserSettings>(listViewUsers.SelectedItems.Count);

            foreach (ListViewItem userItem in listViewUsers.SelectedItems)
            {
                UserSettings user = UserData.Instance.GetSettings(userItem.Text);
                users.Add(user);
            }

            return new UserFormController(new MultiUser(users));
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listViewUsers = new System.Windows.Forms.ListView();
            this.columnHeaderUser = ((System.Windows.Forms.ColumnHeader) (new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderDirectory = ((System.Windows.Forms.ColumnHeader) (new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderAccess = ((System.Windows.Forms.ColumnHeader) (new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderConnections =
                ((System.Windows.Forms.ColumnHeader) (new System.Windows.Forms.ColumnHeader()));
            this.buttonAddUser = new System.Windows.Forms.Button();
            this.groupBoxUsers = new System.Windows.Forms.GroupBox();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonRemoveUser = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.groupBoxUsers.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewUsers
            // 
            this.listViewUsers.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                   | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewUsers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listViewUsers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[]
                                                    {
                                                        this.columnHeaderUser,
                                                        this.columnHeaderDirectory,
                                                        this.columnHeaderAccess,
                                                        this.columnHeaderConnections
                                                    });
            this.listViewUsers.FullRowSelect = true;
            this.listViewUsers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewUsers.HideSelection = false;
            this.listViewUsers.Location = new System.Drawing.Point(8, 16);
            this.listViewUsers.Name = "listViewUsers";
            this.listViewUsers.Size = new System.Drawing.Size(741, 299);
            this.listViewUsers.TabIndex = 0;
            this.listViewUsers.UseCompatibleStateImageBehavior = false;
            this.listViewUsers.View = System.Windows.Forms.View.Details;
            this.listViewUsers.ItemSelectionChanged +=
                new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(
                    this.listViewUsers_ItemSelectionChanged);
            // 
            // columnHeaderUser
            // 
            this.columnHeaderUser.Text = "User";
            this.columnHeaderUser.Width = 94;
            // 
            // columnHeaderDirectory
            // 
            this.columnHeaderDirectory.Text = "Start Directory";
            this.columnHeaderDirectory.Width = 470;
            // 
            // columnHeaderAccess
            // 
            this.columnHeaderAccess.Text = "AccessRights";
            this.columnHeaderAccess.Width = 78;
            // 
            // columnHeaderConnections
            // 
            this.columnHeaderConnections.Text = "Max Connections";
            this.columnHeaderConnections.Width = 96;
            // 
            // buttonAddUser
            // 
            this.buttonAddUser.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddUser.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonAddUser.Location = new System.Drawing.Point(8, 321);
            this.buttonAddUser.Name = "buttonAddUser";
            this.buttonAddUser.Size = new System.Drawing.Size(75, 23);
            this.buttonAddUser.TabIndex = 1;
            this.buttonAddUser.Text = "&Add...";
            this.buttonAddUser.Click += new System.EventHandler(this.buttonAddUser_Click);
            // 
            // groupBoxUsers
            // 
            this.groupBoxUsers.Controls.Add(this.buttonEdit);
            this.groupBoxUsers.Controls.Add(this.buttonRemoveUser);
            this.groupBoxUsers.Controls.Add(this.listViewUsers);
            this.groupBoxUsers.Controls.Add(this.buttonAddUser);
            this.groupBoxUsers.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBoxUsers.Location = new System.Drawing.Point(8, 8);
            this.groupBoxUsers.Name = "groupBoxUsers";
            this.groupBoxUsers.Size = new System.Drawing.Size(755, 355);
            this.groupBoxUsers.TabIndex = 0;
            this.groupBoxUsers.TabStop = false;
            this.groupBoxUsers.Text = "&Users";
            // 
            // buttonEdit
            // 
            this.buttonEdit.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEdit.Enabled = false;
            this.buttonEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonEdit.Location = new System.Drawing.Point(89, 321);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(75, 24);
            this.buttonEdit.TabIndex = 2;
            this.buttonEdit.Text = "&Edit...";
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonRemoveUser
            // 
            this.buttonRemoveUser.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemoveUser.Enabled = false;
            this.buttonRemoveUser.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonRemoveUser.Location = new System.Drawing.Point(170, 321);
            this.buttonRemoveUser.Name = "buttonRemoveUser";
            this.buttonRemoveUser.Size = new System.Drawing.Size(75, 24);
            this.buttonRemoveUser.TabIndex = 2;
            this.buttonRemoveUser.Text = "Remove";
            this.buttonRemoveUser.Click += new System.EventHandler(this.buttonRemoveUser_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonOK.Location = new System.Drawing.Point(688, 369);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "Close";
            // 
            // UsersForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.buttonOK;
            this.ClientSize = new System.Drawing.Size(775, 404);
            this.ControlBox = false;
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupBoxUsers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "UsersForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UsersForm";
            this.groupBoxUsers.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion
    }
}