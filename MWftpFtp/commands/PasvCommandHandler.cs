using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using mwftp.ftp.connectionHandling;
using mwftp.util.General;

namespace mwftp.ftp.commands
{
    internal class PasvCommandHandler : CommandHandler
    {
        private const int PASV_PORT = 0;
        private static int portOffset = 1050;
        private readonly List<TcpListener> listeners = new List<TcpListener>();

        public PasvCommandHandler(FtpConnectionObject connectionObject)
            : base("PASV", connectionObject)
        {
        }

        protected override bool IsReadCommand
        {
            get { return false; }
        }

        protected override bool IsWriteCommand
        {
            get { return false; }
        }

        ~PasvCommandHandler()
        {
        }

        protected override string OnProcess(string sMessage)
        {
            if (ConnectionObject.PasvSocket == null)
            {
                TcpListener listener = SocketHelpers.CreateTcpListener(getPort());
                listeners.Add(listener);

                if (listener == null)
                {
                    return GetMessage(550, string.Format("Couldn't start listener on port {0}", getPort()));
                }

                Timer timer = null;
                try
                {
                    SendPasvReply();
                    portOffset++;
                    timer = startListenerStopper(listener);
                    listener.Start();
                    ConnectionObject.PasvSocket = listener.AcceptTcpClient();
                    return "";
                }
                finally
                {
                    if (timer != null)
                    {
                        timer.Dispose();
                    }
                    listener.Stop();
                    listeners.Remove(listener);
                }
            }
            else
            {
                SendPasvReply();
                return "";
            }
        }

        private Timer startListenerStopper(TcpListener listener)
        {
            int hours = 0;
            int minuts = 0;
            int seconds = 5;
            var delayTime = new TimeSpan(hours, minuts, seconds);
            var period = new TimeSpan(-1);
            TimerCallback timerDelegate = killListenerCallback;
            var timer = new Timer(timerDelegate, listener, delayTime, period);
            return timer;
        }

        private void killListenerCallback(object arg)
        {
            var listener = (TcpListener) arg;
            listener.Stop();
            listeners.Remove(listener);
        }

        private static int getPort()
        {
            return 2*PASV_PORT + portOffset;
        }

        private void SendPasvReply()
        {
            string ipAddress = SocketHelpers.GetLocalAddress().ToString();
            ipAddress = ipAddress.Replace('.', ',');
            ipAddress += ',';

            ipAddress += PASV_PORT;
            ipAddress += ',';
            ipAddress += portOffset;

            Send(string.Format("227 Entering Passive Mode ({0})\r\n", ipAddress));
        }

        protected override void Dispose(bool disposing)
        {
            foreach (TcpListener listener in listeners)
            {
                listener.Stop();
            }
            base.Dispose(disposing);
        }
    }
}