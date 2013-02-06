using System;
using System.Diagnostics;
using System.IO;
using mwftp.ftp.connectionHandling;
using mwftp.ftp.user;
using mwftp.util.General;

namespace mwftp.ftp.commands
{
    /// <summary>
    /// Base class for all ftp command handlers.
    /// </summary>
    internal abstract class CommandHandler : IDisposable
    {
        #region Member Variables

        private readonly string command;
        private readonly FtpConnectionObject theConnectionObject;

        #endregion

        #region Construction

        protected CommandHandler(string command, FtpConnectionObject connectionObject)
        {
            this.command = command;
            theConnectionObject = connectionObject;
        }

        ~CommandHandler()
        {
            Dispose(false);
        }

        #endregion

        #region Properties

        public string Command
        {
            get { return command; }
        }

        public FtpConnectionObject ConnectionObject
        {
            get { return theConnectionObject; }
        }

        #endregion

        #region Methods

        protected abstract bool IsReadCommand { get; }
        protected abstract bool IsWriteCommand { get; }

        public void Process(string message)
        {
            if (hasCommandRights())
            {
                SendMessage(OnProcess(message));
            }
        }

        private bool hasCommandRights()
        {
            return isLoginCommand() || (isReadOk() && isWriteOk());
        }

        private bool isLoginCommand()
        {
            return theConnectionObject.User == null && this is UserCommandHandler;
        }

        private bool isReadOk()
        {
            UserSettings userSettings = UserData.Instance.GetSettings(theConnectionObject.User);
            FileAccess userAccess = userSettings.AccessRights;
            if (IsReadCommand)
            {
                switch (userAccess)
                {
                    case FileAccess.Read:
                        return true;
                    case FileAccess.ReadWrite:
                        return true;
                    default:
                        sendReadDenied();
                        return false;
                }
            }
            return true;
        }

        private bool isWriteOk()
        {
            UserSettings userSettings = UserData.Instance.GetSettings(theConnectionObject.User);
            FileAccess userAccess = userSettings.AccessRights;
            if (IsWriteCommand)
            {
                switch (userAccess)
                {
                    case FileAccess.Write:
                        return true;
                    case FileAccess.ReadWrite:
                        return true;
                    default:
                        sendWriteDenied();
                        return false;
                }
            }
            return true;
        }

        private void sendReadDenied()
        {
            sendDenyMessage("read");
        }

        private void sendWriteDenied()
        {
            sendDenyMessage("write");
        }

        private void sendDenyMessage(string typeOfDenial)
        {
            Send("451 Requested action aborted. User don't have correct file prevelige");
        }

        protected virtual string OnProcess(string message)
        {
            Debug.Assert(false, "FtpCommandHandler::OnProcess base called");
            return "";
        }

        protected string GetMessage(int returnCode, string message)
        {
            return string.Format("{0} {1}\r\n", returnCode, message);
        }

        protected string GetPath(string path)
        {
            if (path.Length == 0)
            {
                return theConnectionObject.CurrentDirectory;
            }

            path = path.Replace('/', '\\');

            return Path.Combine(theConnectionObject.CurrentDirectory, path);
        }

        protected void Send(string message)
        {
            ServerEvents.SendMessage(this, new ServerEvent(ConnectionObject.Id, message));
            SocketHelpers.Send(ConnectionObject.Socket, message);
        }

        private void SendMessage(string message)
        {
            if (message.Length == 0)
            {
                return;
            }

            int nEndIndex = message.IndexOf('\r');

            if (nEndIndex < 0)
            {
                ServerEvents.SendMessage(this, new ServerEvent(theConnectionObject.Id, message));
            }
            else
            {
                ServerEvents.SendMessage(this, new ServerEvent(theConnectionObject.Id, message.Substring(0, nEndIndex)));
            }
            SocketHelpers.Send(ConnectionObject.Socket, message);
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        #endregion

        protected virtual void Dispose(bool disposing)
        {
        }
    }
}