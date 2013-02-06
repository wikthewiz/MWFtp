using System;
using mwftp.ftp.user;

namespace mwftp.ftp.fileSystem
{
    public class StandardFileSystemClassFactory : IFileSystemClassFactory
    {
        #region IFileSystemClassFactory Members

        public FileSystem Create(string userName, string password)
        {
            if (!IsCorrectUserPassword(userName, password))
            {
                throw new MissingFieldException("user: " + userName + " password: " + password + " wasn't found");
            }
            if (!HasFreeConnections(userName))
            {
                throw new NoFreeConnectionsException();
            }

            UserSettings theUser = UserData.Instance.GetSettings(userName);
            return new StandardFileSystemObject(theUser.StartingDirectory);
        }

        public bool IsCorrectUserPassword(string userName, string password)
        {
            return UserData.Instance.HasUser(userName) && UserData.Instance.GetSettings(userName).Password == password;
        }

        public bool HasFreeConnections(string userName)
        {
            LogedInConnectionCounter connectionCounter = UserData.Instance.GetConnectionCounter(userName);
            UserSettings userSettings = UserData.Instance.GetSettings(userName);
            return connectionCounter.Count < userSettings.MaxNrOfConnections;
        }

        #endregion
    }
}