using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace mwftp.util.General
{
    public sealed class SocketHelpers
    {
        private const string END_OF_LINE = "\r\n";
        private static IPAddress localIp;

        private SocketHelpers()
        {
        }

        public static void Send(TcpClient socket, byte[] abMessage)
        {
            Send(socket, abMessage, 0, abMessage.Length);
        }

        public static void Send(TcpClient socket, byte[] abMessage, int nStart, int nLength)
        {
            var writer = new BinaryWriter(socket.GetStream());
            writer.Write(abMessage, nStart, nLength);
            writer.Flush();
        }

        public static void Send(TcpClient socket, string message)
        {
            string messageToSend = message.EndsWith(END_OF_LINE) ? message : message + END_OF_LINE;
            byte[] byteMessage = Encoding.UTF8.GetBytes(messageToSend);
            Send(socket, byteMessage);
        }

        public static void Close(TcpClient socket)
        {
            if (socket == null)
            {
                return;
            }

            try
            {
                if (socket.Connected)
                {
                    if (socket.GetStream() != null)
                    {
                        socket.GetStream().Flush();
                        socket.GetStream().Close();
                    }
                }
            }
            finally
            {
                socket.Close();
            }
        }

        public static TcpClient CreateTcpClient(string sAddress, int nPort)
        {
            TcpClient client = null;

            try
            {
                client = new TcpClient(sAddress, nPort);
            }
            catch (SocketException)
            {
                client = null;
            }

            return client;
        }

        public static TcpListener CreateTcpListener(int port)
        {
            TcpListener listener = null;

            try
            {
                listener = new TcpListener(IPAddress.Any, port);
            }
            catch (SocketException)
            {
                listener = null;
            }

            return listener;
        }

        public static void SetLocalIpAddress(IPAddress localIpAddress)
        {
            localIp = localIpAddress;
        }

        public static IPAddress[] GetLocalAddresses()
        {
            IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());

            if (hostEntry.AddressList.Length == 0)
            {
                return null;
            }
            return hostEntry.AddressList;
        }

        public static IPAddress GetLocalAddress()
        {
            IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());

            if (hostEntry.AddressList.Length == 0)
            {
                return null;
            }

            int index = hostEntry.AddressList.Length - 1;
            if (localIp != null)
            {
                index = indexOf(hostEntry.AddressList, localIp);
                if (index < 0)
                {
                    index = hostEntry.AddressList.Length - 1;
                }
            }
            return hostEntry.AddressList[index];
        }

        private static int indexOf(IPAddress[] addresses, IPAddress address)
        {
            return Array.IndexOf(addresses, address, 0, addresses.Length);
        }
    }
}