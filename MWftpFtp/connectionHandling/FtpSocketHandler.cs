using System;
using System.Net.Sockets;
using System.Threading;
using mwftp.ftp.fileSystem;
using mwftp.ftp.user;
using mwftp.util.General;

namespace mwftp.ftp.connectionHandling
{
    /// <summary>
    /// Contains the socket read functionality. Works on its own thread since all socket operation is blocking.
    /// </summary>
    internal class FtpSocketHandler : IDisposable
    {
        #region Member Variables

        private const int nBufferSize = 65536;
        private readonly IFileSystemClassFactory fileSystemClassFactory;
        private readonly int id;
        private FtpConnectionObject connectionObject;
        private bool isRunnning;
        private TcpClient theSocket;
        private Thread theThread;

        #endregion

        #region Events

        #region Delegates

        public delegate void CloseHandler(FtpSocketHandler handler);

        #endregion

        public event CloseHandler Closed;

        #endregion

        public FtpSocketHandler(IFileSystemClassFactory fileSystemClassFactory, int id)
        {
            this.id = id;
            this.fileSystemClassFactory = fileSystemClassFactory;
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        #endregion

        ~FtpSocketHandler()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Stop();
                connectionObject.Dispose();
            }
        }

        #region Methods

        public void Start(TcpClient socket)
        {
            theSocket = socket;

            connectionObject = new FtpConnectionObject(fileSystemClassFactory, id, socket);
            theThread = new Thread(ThreadRun);
            theThread.Start();
        }

        public void Stop()
        {
            SocketHelpers.Close(theSocket);
            if (isRunnning)
            {
                if (!theThread.Join(3000))
                {
                    theThread.Abort();
                }
            }
        }

        private void ThreadRun()
        {
            try
            {
                isRunnning = true;
                var abData = new Byte[nBufferSize];
                int nReceived = theSocket.GetStream().Read(abData, 0, nBufferSize);

                while (nReceived > 0)
                {
                    connectionObject.Process(abData);

                    nReceived = theSocket.GetStream().Read(abData, 0, nBufferSize);
                }
            }
            catch (UserNotFoundException e)
            {
                ServerEvents.InfoMessage(this, new ServerEvent(id, "user (" + e.User + ") not found"));
            }
            catch (Exception e)
            {
                ServerEvents.ErrroMessage(this, new ServerEvent(id, e.Message));
            }
            finally
            {
                closeConnection();
                isRunnning = false;
                ServerEvents.InfoMessage(this, new ServerEvent(id, "Connection closed"));
            }
        }

        private void closeConnection()
        {
            connectionObject.LogOut();
            if (Closed != null)
            {
                Closed(this);
            }
        }

        #endregion

        #region Properties

        public int Id
        {
            get { return id; }
        }

        #endregion
    }
}