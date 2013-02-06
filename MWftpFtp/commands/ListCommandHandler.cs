using mwftp.ftp.connectionHandling;

namespace mwftp.ftp.commands
{
    internal class ListCommandHandler : ListCommandHandlerBase
    {
        public ListCommandHandler(FtpConnectionObject connectionObject)
            : base("LIST", connectionObject)
        {
        }

        protected override bool IsReadCommand
        {
            get { return true; }
        }

        protected override bool IsWriteCommand
        {
            get { return false; }
        }

        protected override string BuildReply(string sMessage, string[] asFiles)
        {
            return BuildLongReply(asFiles);
        }
    }
}