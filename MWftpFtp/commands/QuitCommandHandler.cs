using mwftp.ftp.connectionHandling;

namespace mwftp.ftp.commands
{
    internal class QuitCommandHandler : CommandHandler
    {
        public QuitCommandHandler(FtpConnectionObject connectionObject)
            : base("QUIT", connectionObject)
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
            return GetMessage(220, "Goodbye");
        }
    }
}