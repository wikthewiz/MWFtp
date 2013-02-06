using System.Collections.Generic;
using System.IO;
using System.Text;
using mwftp.app.controller;
using mwftp.ftp.user;

namespace mwftp.app.view
{
    internal class MultiUser : UserSettings
    {
        private readonly List<UserSettings> users;
        private StartFileAccess access;
        private int maxNrOfConnections;
        private StringBuilder name;
        private string password = string.Empty;
        private string startDir = string.Empty;

        public MultiUser(List<UserSettings> users)
        {
            this.users = users;
            initiateFields();
            setFiledValues();
        }

        #region UserSettings Members

        public string Name
        {
            get { return name.ToString(); }
        }

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                setPasswordForEachUser();
            }
        }

        public string StartingDirectory
        {
            get { return startDir; }
            set
            {
                startDir = value;
                setStartFolderForEachUser();
            }
        }

        public FileAccess AccessRights
        {
            get { return FileAccessHelper.Pars(access); }
            set
            {
                access = FileAccessHelper.Pars(value);
                setAccessForEachUser(value);
            }
        }

        public int MaxNrOfConnections
        {
            get { return maxNrOfConnections; }
            set
            {
                maxNrOfConnections = value;
                setMaxConnectionsForEachUser();
            }
        }

        #endregion

        private void initiateFields()
        {
            access = FileAccessHelper.Pars(users[0].AccessRights);
            maxNrOfConnections = users[0].MaxNrOfConnections;
            startDir = users[0].StartingDirectory;
            name = new StringBuilder(users[0].Name);
        }

        private void setFiledValues()
        {
            for (int i = 1; i < users.Count; i++)
            {
                UserSettings user = users[i];

                if (user.AccessRights != FileAccessHelper.Pars(access))
                {
                    access = StartFileAccess.None;
                }
                if (user.MaxNrOfConnections != maxNrOfConnections)
                {
                    maxNrOfConnections = -1;
                }
                if (user.StartingDirectory != startDir)
                {
                    startDir = "N/A";
                }

                name.Append(", " + user.Name);
            }
        }

        private void setMaxConnectionsForEachUser()
        {
            users.ForEach(delegate(UserSettings user) { user.MaxNrOfConnections = maxNrOfConnections; });
        }

        private void setStartFolderForEachUser()
        {
            users.ForEach(delegate(UserSettings user) { user.StartingDirectory = startDir; });
        }

        private void setPasswordForEachUser()
        {
            users.ForEach(delegate(UserSettings user) { user.Password = password; });
        }

        private void setAccessForEachUser(FileAccess access)
        {
            users.ForEach(delegate(UserSettings user) { user.AccessRights = access; });
        }
    }
}