using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace mwftp.ftp
{
    public class ServerSettings
    {
        private const string URI_FORMAT = "file:\\";
        private static readonly string settingsFile;
        private SerializableServerSettings settings;

        static ServerSettings()
        {
            string settingsPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            if (settingsPath.StartsWith(URI_FORMAT))
                settingsPath = settingsPath.Remove(0, URI_FORMAT.Length);
            settingsFile = Path.Combine(settingsPath, "settings.bin");
        }

        internal ServerSettings()
        {
            if (!File.Exists(settingsFile))
            {
                settings = new SerializableServerSettings();
                settings.Save();
            }
            readSettings();
        }

        public int Port
        {
            get { return settings.Port; }
            set
            {
                settings.Port = value;
                settings.Save();
            }
        }

        public int MaxNrOfConnection
        {
            get { return settings.MaxNrOfConnection; }
            set
            {
                settings.MaxNrOfConnection = value;
                settings.Save();
            }
        }

        public int MaxNrOfConnectionPerUser
        {
            get { return settings.MaxNrOfConnectionPerUser; }
            set
            {
                settings.MaxNrOfConnectionPerUser = value;
                settings.Save();
            }
        }

        private void readSettings()
        {
            using (Stream stream = File.OpenRead(settingsFile))
            {
                var bf = new BinaryFormatter();
                var settings = bf.Deserialize(stream) as SerializableServerSettings;
                this.settings = settings;
            }
        }

        #region Nested type: SerializableServerSettings

        [Serializable]
        private class SerializableServerSettings
        {
            public int MaxNrOfConnection;
            public int MaxNrOfConnectionPerUser;
            public int Port;

            internal SerializableServerSettings()
            {
                Port = 21;
                MaxNrOfConnection = 100;
                MaxNrOfConnectionPerUser = 10;
            }

            public void Save()
            {
                using (Stream stream = File.OpenWrite(settingsFile))
                {
                    var bf = new BinaryFormatter();
                    bf.Serialize(stream, this);
                }
            }
        }

        #endregion
    }
}