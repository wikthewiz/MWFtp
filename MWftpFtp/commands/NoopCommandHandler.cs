using mwftp.ftp.connectionHandling;

namespace mwftp.ftp.commands
{
    internal class NoopCommandHandler : CommandHandler
    {
        public NoopCommandHandler(FtpConnectionObject connectionObject)
            : base("NOOP", connectionObject)
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
            return GetMessage(200, "");
        }
    }
}