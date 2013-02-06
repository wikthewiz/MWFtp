using mwftp.ftp.connectionHandling;

namespace mwftp.ftp.commands
{
    internal class UserCommandHandler : CommandHandler
    {
        public UserCommandHandler(FtpConnectionObject connectionObject)
            : base("USER", connectionObject)
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

        protected override string OnProcess(string sMessage)
        {
            ConnectionObject.User = sMessage;

            return GetMessage(331, string.Format("User {0} logged in, needs password", sMessage));
        }
    }
}