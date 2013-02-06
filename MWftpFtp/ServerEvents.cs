using System;

namespace mwftp.ftp
{
    public enum FtpAction
    {
        UserLogedIn,
        UserLogedOut,
        NoAction,
    }

    public static class ServerEvents
    {
        public static event EventHandler<ServerEvent> Info;
        public static event EventHandler<ServerEvent> Received;
        public static event EventHandler<ServerEvent> Send;
        public static event EventHandler<ServerEvent> Error;

        internal static void ErrroMessage(object sender, ServerEvent message)
        {
            send(Error, sender, message);
        }

        internal static void ReceivedMessage(object sender, ServerEvent message)
        {
            send(Received, sender, message);
        }

        internal static void SendMessage(object sender, ServerEvent message)
        {
            send(Send, sender, message);
        }

        internal static void InfoMessage(object sender, ServerEvent message)
        {
            send(Info, sender, message);
        }

        private static void send(EventHandler<ServerEvent> theEvent, object sender, ServerEvent message)
        {
            if (theEvent != null)
            {
                theEvent(sender, message);
            }
        }
    }

    public class ServerEvent : EventArgs
    {
        private readonly FtpAction action;
        private readonly int id;
        private readonly string message;
        private readonly string user;

        internal ServerEvent(int id, string message) : this(id, message, string.Empty, FtpAction.NoAction)
        {
        }

        internal ServerEvent(int id, string message, string user) : this(id, message, user, FtpAction.NoAction)
        {
        }

        internal ServerEvent(int id, string message, string user, FtpAction action)
        {
            this.id = id;
            this.message = message;
            this.user = user;
            this.action = action;
        }

        public int Id
        {
            get { return id; }
        }

        public string Message
        {
            get { return message; }
        }

        public FtpAction Action
        {
            get { return action; }
        }

        public string User
        {
            get { return user; }
        }
    }
}