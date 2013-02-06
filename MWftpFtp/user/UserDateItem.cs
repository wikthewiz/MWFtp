using System;
using System.IO;

namespace mwftp.ftp.user
{
    [Serializable]
    internal class UserDataItem : UserSettings
    {
        private FileAccess fileAccessRights = FileAccess.Read;
        private int maxNrOfConnection = FtpServer.Settings.MaxNrOfConnectionPerUser;
        [NonSerialized] private string name;
        private string password = String.Empty;
        private string startingDirectory = @"C:\";

        public UserDataItem(UserSettings user)
        {
            Password = user.Password;
            StartingDirectory = user.StartingDirectory;
            AccessRights = user.AccessRights;
            MaxNrOfConnections = user.MaxNrOfConnections;
            name = user.Name;
        }

        public UserDataItem()
        {
            name = string.Empty;
        }

        #region UserSettings Members

        public string Password
        {
            get { return password; }

            set { password = value; }
        }

        public string StartingDirectory
        {
            get { return startingDirectory; }

            set { startingDirectory = value; }
        }

        public FileAccess AccessRights
        {
            get { return fileAccessRights; }
            set { fileAccessRights = value; }
        }

        public int MaxNrOfConnections
        {
            get { return maxNrOfConnection; }
            set { maxNrOfConnection = value; }
        }

        public string Name
        {
            get { return name; }
            internal set { name = value; }
        }

        #endregion

        public override bool Equals(object obj)
        {
            return Equals(obj as UserDataItem);
        }

        public bool Equals(UserDataItem item)
        {
            if (item == null) return false;
            return name.Equals(item.name);
        }

        public override int GetHashCode()
        {
            return name.GetHashCode();
        }
    }
}