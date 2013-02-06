using System.Net.Sockets;
using System.Text;
using mwftp.util.General;

namespace mwftp.ftp.connectionHandling
{
    /// <summary>
    /// Encapsulates the functionality necessary to send data along the reply connection
    /// </summary>
    internal class FtpReplySocket
    {
        private TcpClient theSocket;

        public FtpReplySocket(FtpConnectionObject connectionObject)
        {
            theSocket = OpenSocket(connectionObject);
        }

        public bool Loaded
        {
            get { return theSocket != null; }
        }

        public void Close()
        {
            SocketHelpers.Close(theSocket);
            theSocket = null;
        }

        public void Send(byte[] abData, int nSize)
        {
            SocketHelpers.Send(theSocket, abData, 0, nSize);
        }

        public void Send(string message, int id)
        {
            ServerEvents.SendMessage(this, new ServerEvent(id, message));
            byte[] abData = Encoding.ASCII.GetBytes(message);
            Send(abData, abData.Length);
        }

        public int Receive(byte[] abData)
        {
            return theSocket.GetStream().Read(abData, 0, abData.Length);
        }

        private static TcpClient OpenSocket(FtpConnectionObject connectionObject)
        {
            TcpClient socketPasv = connectionObject.PasvSocket;

            if (socketPasv != null)
            {
                connectionObject.PasvSocket = null;
                return socketPasv;
            }

            return SocketHelpers.CreateTcpClient(connectionObject.PortCommandSocketAddress,
                                                 connectionObject.PortCommandSocketPort);
        }
    }
}