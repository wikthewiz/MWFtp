using System;
using System.IO;
using mwftp.app.exception;
using mwftp.ftp.user;

namespace mwftp.app.controller
{
    public enum StartFileAccess
    {
        None = 0,
        Read = 1,
        Write = 2,
        ReadWrite = 3,
    }

    public class UserFormController
    {
        private readonly UserSettings userSettings;
        private bool cancel;

        public UserFormController(UserSettings user)
        {
            userSettings = user;
        }

        public bool WasCancled
        {
            get { return cancel; }
        }

        public UserSettings User
        {
            get
            {
                if (cancel)
                    throw new CancelExcpetion();
                return userSettings;
            }
        }

        public StartFileAccess InitStateOfFileAccess
        {
            get
            {
                try
                {
                    return FileAccessHelper.Pars(userSettings.AccessRights);
                }
                catch (Exception)
                {
                    return StartFileAccess.None;
                }
            }
        }

        public void Cancel()
        {
            cancel = true;
        }
    }

    internal static class FileAccessHelper
    {
        public static readonly string[] AccessNames = new[] {"N/A", "Läs", "Skriv", "Skriv&Läs"};

        public static string GetAccessName(StartFileAccess access)
        {
            return AccessNames[(int) access];
        }

        public static StartFileAccess GetAccess(string name)
        {
            if (name == GetAccessName(StartFileAccess.Read))
            {
                return StartFileAccess.Read;
            }
            if (name == GetAccessName(StartFileAccess.ReadWrite))
            {
                return StartFileAccess.ReadWrite;
            }
            if (name == GetAccessName(StartFileAccess.Write))
            {
                return StartFileAccess.Write;
            }
            return StartFileAccess.None;
        }

        public static StartFileAccess Pars(FileAccess access)
        {
            switch (access)
            {
                case FileAccess.Read:
                    return StartFileAccess.Read;
                case FileAccess.Write:
                    return StartFileAccess.Write;
                case FileAccess.ReadWrite:
                    return StartFileAccess.ReadWrite;
                default:
                    throw new NotImplementedException("doesn't exists");
            }
        }


        public static FileAccess Pars(StartFileAccess access)
        {
            switch (access)
            {
                case StartFileAccess.Read:
                    return FileAccess.Read;
                case StartFileAccess.Write:
                    return FileAccess.Write;
                case StartFileAccess.ReadWrite:
                    return FileAccess.ReadWrite;
                default:
                    throw new ArgumentException("doesn't exists");
            }
        }
    }

    public class NewUser : UserSettings
    {
        private FileAccess access = FileAccess.Read;
        private string name = string.Empty;
        private string password = string.Empty;
        private string startDir = string.Empty;

        #region UserSettings Members

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string StartingDirectory
        {
            get { return startDir; }
            set { startDir = value; }
        }

        public FileAccess AccessRights
        {
            get { return access; }
            set { access = value; }
        }

        public string Name
        {
            get { return name; }
            internal set { name = value; }
        }

        public int MaxNrOfConnections { get; set; }

        #endregion
    }
}