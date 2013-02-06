using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using mwftp.ftp.commands;
using mwftp.ftp.fileSystem;
using mwftp.ftp.user;
using mwftp.util.General;

namespace mwftp.ftp.connectionHandling
{
    /// <summary>
    /// Processes incoming messages and passes the data on to the relevant handler class.
    /// </summary>
    internal class FtpConnectionObject : FtpConnectionData
    {
        #region Member Variables

        private readonly Dictionary<String, CommandHandler> commandHashTable;
        private readonly IFileSystemClassFactory fileSystemClassFactory;

        #endregion

        #region LoginReturns enum

        public enum LoginReturns
        {
            Access,
            AccessDenied,
            MaxNrOfConnectionReached,
        }

        #endregion

        #region Methods

        public LoginReturns CheckLogin(string password)
        {
            if (!hasCorrectPassword(password))
            {
                return LoginReturns.AccessDenied;
            }

            if (!hasFreeConnections())
            {
                return LoginReturns.MaxNrOfConnectionReached;
            }

            return LoginReturns.Access;
        }

        public void Login(string password)
        {
            FileSystem fileSystem = fileSystemClassFactory.Create(User, password);
            SetFileSystemObject(fileSystem);
            addConnection();
            ServerEvents.InfoMessage(this, createLogedInMessage());
        }

        public void LogOut()
        {
            removeConnection();
            closePasvSocket();
            if (!string.IsNullOrEmpty(User))
            {
                ServerEvents.InfoMessage(this, createLogOutMessage());
            }
        }

        private ServerEvent createLogedInMessage()
        {
            return new ServerEvent(
                Id,
                "User: " + User + " Loged In",
                User,
                FtpAction.UserLogedIn);
        }

        private ServerEvent createLogOutMessage()
        {
            return new ServerEvent(
                Id,
                User + " loged out",
                User,
                FtpAction.UserLogedOut);
        }

        private void closePasvSocket()
        {
            if (PasvSocket != null)
            {
                PasvSocket.Close();
            }
        }

        private void removeConnection()
        {
            if (User != null)
            {
                LogedInConnectionCounter counter = UserData.Instance.GetConnectionCounter(User);
                counter.Remove(Id);
            }
        }

        private void addConnection()
        {
            LogedInConnectionCounter counter = UserData.Instance.GetConnectionCounter(User);
            counter.Add(Id);
        }

        private bool hasFreeConnections()
        {
            return fileSystemClassFactory.HasFreeConnections(User);
        }

        private bool hasCorrectPassword(string password)
        {
            return fileSystemClassFactory.IsCorrectUserPassword(User, password);
        }

        private void LoadCommands()
        {
            AddCommand(new UserCommandHandler(this));
            AddCommand(new PasswordCommandHandler(this));
            AddCommand(new QuitCommandHandler(this));
            AddCommand(new CwdCommandHandler(this));
            AddCommand(new PortCommandHandler(this));
            AddCommand(new PasvCommandHandler(this));
            AddCommand(new ListCommandHandler(this));
            AddCommand(new NlstCommandHandler(this));
            AddCommand(new PwdCommandHandler(this));
            AddCommand(new XPwdCommandHandler(this));
            AddCommand(new TypeCommandHandler(this));
            AddCommand(new RetrCommandHandler(this));
            AddCommand(new NoopCommandHandler(this));
            AddCommand(new SizeCommandHandler(this));
            AddCommand(new DeleCommandHandler(this));
            AddCommand(new AlloCommandHandler(this));
            AddCommand(new StoreCommandHandler(this));
            AddCommand(new MakeDirectoryCommandHandler(this));
            AddCommand(new RemoveDirectoryCommandHandler(this));
            AddCommand(new AppendCommandHandler(this));
            AddCommand(new RenameStartCommandHandler(this));
            AddCommand(new RenameCompleteCommandHandler(this));
            AddCommand(new XMkdCommandHandler(this));
            AddCommand(new XRmdCommandHandler(this));
            AddCommand(new MDTMCommandHandler(this));
        }

        private void AddCommand(CommandHandler handler)
        {
            commandHashTable.Add(handler.Command, handler);
        }

        public void Process(Byte[] abData)
        {
            string sMessage = Encoding.ASCII.GetString(abData);
            sMessage = sMessage.Substring(0, sMessage.IndexOf('\r'));

            ServerEvents.ReceivedMessage(this, new ServerEvent(Id, sMessage));

            MessageTuple messageTuple = getCommand(sMessage);
            handleCommand(messageTuple);
        }

        private MessageTuple getCommand(string sMessage)
        {
            int spaceIndex = sMessage.IndexOf(' ');
            if (spaceIndex < 0)
            {
                return new MessageTuple(sMessage.ToUpper(), String.Empty);
            }
            else
            {
                string command = sMessage.Substring(0, spaceIndex).ToUpper();
                string sValue = sMessage.Substring(command.Length + 1);
                return new MessageTuple(command, sValue);
            }
        }


        private void handleCommand(MessageTuple messageTuple)
        {
            string command = messageTuple.Command;
            string sValue = messageTuple.SValue;
            try
            {
                ServerEvents.InfoMessage(this, new ServerEvent(Id, "Processing command: " + command));
                CommandHandler handler = commandHashTable[command];
                handler.Process(sValue);
            }
            catch (KeyNotFoundException)
            {
                Send("550 Unknown command\r\n");
            }
        }


        private void Send(string message)
        {
            ServerEvents.SendMessage(this, new ServerEvent(Id, message));
            SocketHelpers.Send(Socket, message);
        }

        private class MessageTuple
        {
            private readonly string command;
            private readonly string sValue;

            public MessageTuple(String command, string sValue)
            {
                this.command = command;
                this.sValue = sValue;
            }

            public string Command
            {
                get { return command; }
            }

            public string SValue
            {
                get { return sValue; }
            }
        }

        #endregion

        public FtpConnectionObject(IFileSystemClassFactory fileSystemClassFactory, int nId, TcpClient socket)
            : base(nId, socket)
        {
            commandHashTable = new Dictionary<String, CommandHandler>(17);
            this.fileSystemClassFactory = fileSystemClassFactory;
            LoadCommands();
        }

        ~FtpConnectionObject()
        {
            unloadCommands();
        }

        private void unloadCommands()
        {
            foreach (var keyVal in commandHashTable)
            {
                keyVal.Value.Dispose();
            }
        }

        internal void Dispose()
        {
            GC.SuppressFinalize(this);
            unloadCommands();
        }
    }
}