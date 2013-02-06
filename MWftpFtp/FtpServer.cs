using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using mwftp.ftp.connectionHandling;
using mwftp.ftp.fileSystem;
using mwftp.ftp.user;
using mwftp.util.General;

namespace mwftp.ftp
{
    /// <summary>
    /// Listens for incoming connections and accepts them.
    /// Incomming socket connections are then passed to the socket handling class (FtpSocketHandler).
    /// </summary>
    public class FtpServer : IDisposable
    {
        #region Member Variables

        private static readonly ServerSettings settings;
        private readonly List<FtpSocketHandler> apConnections;

        private readonly Action<FtpSocketHandler> disposeAction =
            delegate(FtpSocketHandler handler) { handler.Dispose(); };

        private readonly IFileSystemClassFactory fileSystemClassFactory;
        private readonly Action<FtpSocketHandler> stopAction = delegate(FtpSocketHandler handler) { handler.Stop(); };
        private int id;
        private int port;
        private TcpListener socketListen;
        private Thread theThread;

        #endregion

        #region Events

        public delegate void ConnectionHandler(int connectionId, UserSettings user);

        #endregion

        public static ServerSettings Settings
        {
            get { return settings; }
        }

        public bool IsRunning
        {
            get { return theThread != null && theThread.IsAlive; }
        }

        #region Construction

        static FtpServer()
        {
            settings = new ServerSettings();
        }

        public FtpServer(IFileSystemClassFactory fileSystemClassFactory)
        {
            apConnections = new List<FtpSocketHandler>();
            this.fileSystemClassFactory = fileSystemClassFactory;
        }

        ~FtpServer()
        {
            dispose(false);
        }

        #endregion

        #region Methods

        public void Start()
        {
            start(Settings.Port);
        }

        private void start(int port)
        {
            if (!IsRunning)
            {
                this.port = port;
                theThread = new Thread(ThreadRun);
                theThread.Start();
            }
        }

        public void Stop()
        {
            stop(stopAction);
        }

        private void stop(Action<FtpSocketHandler> action)
        {
            if (IsRunning)
            {
                if (apConnections != null)
                {
                    apConnections.ForEach(action);
                }
                if (socketListen != null)
                {
                    socketListen.Stop();
                }
                if (theThread.IsAlive)
                {
                    if (!theThread.Join(3000))
                    {
                        theThread.Abort();
                    }
                }
            }
        }

        private void ThreadRun()
        {
            socketListen = SocketHelpers.CreateTcpListener(port);

            if (socketListen != null)
            {
                socketListen.Start();

                ServerEvents.InfoMessage(this, new ServerEvent(0, "FTP Server Started"));

                bool keepListenForConnections = true;

                while (keepListenForConnections)
                {
                    TcpClient socket = null;

                    try
                    {
                        socket = socketListen.AcceptTcpClient();
                    }
                    catch (SocketException)
                    {
                        keepListenForConnections = false;
                    }
                    finally
                    {
                        if (socket == null)
                        {
                            keepListenForConnections = false;
                        }
                        else
                        {
                            if (apConnections.Count < settings.MaxNrOfConnection)
                            {
                                acceptConnection(socket);
                            }
                            else
                            {
                                refuseConnection(socket);
                            }
                        }
                    }
                }
            }
            else
            {
                ServerEvents.InfoMessage(this, new ServerEvent(0, "Error in starting FTP server"));
            }
        }

        private void acceptConnection(TcpClient socket)
        {
            socket.NoDelay = false;

            id++;
            ServerEvents.InfoMessage(this, new ServerEvent(id, "New connection"));
            try
            {
                SendAcceptMessage(socket);
                InitialiseSocketHandler(socket);
            }
            catch (Exception e)
            {
                ServerEvents.ErrroMessage(this, new ServerEvent(id, e.Message));
            }
        }

        private void refuseConnection(TcpClient socket)
        {
            try
            {
                sendRefuseConnection(socket);
                socket.Close();
            }
            catch (Exception e)
            {
                ServerEvents.ErrroMessage(this, new ServerEvent(id, e.Message));
            }
        }

        private void sendRefuseConnection(TcpClient socket)
        {
            send(socket, "451 Requested action aborted. MaxNrOfConnection reached\r\n");
        }

        private void SendAcceptMessage(TcpClient socket)
        {
            send(socket, "220 FTP Server Ready\r\n");
        }

        private void send(TcpClient socket, string message)
        {
            ServerEvents.SendMessage(this, new ServerEvent(id, message));
            SocketHelpers.Send(socket, message);
        }

        private void InitialiseSocketHandler(TcpClient socket)
        {
            var handler = new FtpSocketHandler(fileSystemClassFactory, id);
            handler.Start(socket);

            apConnections.Add(handler);

            handler.Closed += handler_Closed;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            dispose(true);
        }

        #endregion

        private void handler_Closed(FtpSocketHandler handler)
        {
            apConnections.Remove(handler);
        }

        private void dispose(bool disposing)
        {
            stop(disposeAction);
            if (socketListen != null)
            {
                socketListen.Stop();
            }
        }
    }
}