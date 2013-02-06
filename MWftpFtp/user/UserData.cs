using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace mwftp.ftp.user
{
    public class UserData
    {
        #region Member Variables

        private static UserData theUserData;
        private static readonly object getLock = new object();
        private readonly Dictionary<string, LogedInConnectionCounter> mapUserConnection;
        private readonly object mapUserConnectionLock = new object();
        private readonly object mapUserToDataLock = new object();
        private Dictionary<String, UserDataItem> mapUserToData;

        #endregion

        #region Construction

        protected UserData()
        {
            mapUserToData = new Dictionary<string, UserDataItem>();
            mapUserConnection = new Dictionary<string, LogedInConnectionCounter>();
        }

        #endregion

        #region Properties

        public static UserData Instance
        {
            get
            {
                lock (getLock)
                {
                    if (theUserData == null)
                    {
                        theUserData = new UserData();
                    }
                }
                return theUserData;
            }
        }

        public IEnumerable<String> Users
        {
            get { return mapUserToData.Keys; }
        }

        public IEnumerable<UserSettings> UsersSettings
        {
            get
            {
                foreach (var item in mapUserToData)
                {
                    yield return item.Value;
                }
            }
        }

        public int Count
        {
            get { return mapUserToData.Count; }
        }

        #endregion

        #region Methods

        private UserDataItem GetUserItem(string user)
        {
            try
            {
                return mapUserToData[user];
            }
            catch (KeyNotFoundException)
            {
                throw new UserNotFoundException(user);
            }
        }

        public UserSettings Add(string user)
        {
            lock (mapUserToDataLock)
            {
                mapUserToData.Add(user, new UserDataItem());
            }

            lock (mapUserConnectionLock)
            {
                mapUserConnection.Add(user, new LogedInConnectionCounter());
            }

            return mapUserToData[user];
        }

        public void Add(UserSettings user)
        {
            lock (mapUserToDataLock)
            {
                mapUserToData.Add(user.Name, new UserDataItem(user));
            }

            lock (mapUserConnectionLock)
            {
                mapUserConnection.Add(user.Name, new LogedInConnectionCounter());
            }
        }

        public UserSettings GetSettings(string user)
        {
            return GetUserItem(user);
        }

        public void Remove(string user)
        {
            lock (mapUserToDataLock)
            {
                mapUserToData.Remove(user);
            }
            lock (mapUserConnectionLock)
            {
                mapUserConnection.Remove(user);
            }
        }

        public LogedInConnectionCounter GetConnectionCounter(string user)
        {
            lock (mapUserConnectionLock)
            {
                return mapUserConnection[user];
            }
        }

        public bool HasUser(string user)
        {
            lock (mapUserToDataLock)
            {
                return mapUserToData.ContainsKey(user);
            }
        }

        public void Save(string sFileName)
        {
            var formatter = new BinaryFormatter();
            using (var fileStream = new FileStream(sFileName, FileMode.Create))
            {
                formatter.Serialize(fileStream, mapUserToData);
            }
        }

        public void Load(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return;
            }
            var formatter = new BinaryFormatter();
            using (var fileStream = new FileStream(fileName, FileMode.Open))
            {
                mapUserToData = formatter.Deserialize(fileStream) as Dictionary<string, UserDataItem>;
            }

            foreach (String userName in mapUserToData.Keys)
            {
                mapUserToData[userName].Name = userName;
                mapUserConnection[userName] = new LogedInConnectionCounter();
            }
        }

        private string GetDefaultPath()
        {
            return Path.Combine(Application.StartupPath, "Users.dat");
        }

        public void Save()
        {
            Save(GetDefaultPath());
        }

        public void Load()
        {
            Load(GetDefaultPath());
        }

        #endregion
    }
}