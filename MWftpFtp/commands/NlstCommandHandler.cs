using mwftp.ftp.connectionHandling;

namespace mwftp.ftp.commands
{
    internal class NlstCommandHandler : ListCommandHandlerBase
    {
        public NlstCommandHandler(FtpConnectionObject connectionObject)
            : base("NLST", connectionObject)
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
            if (sMessage == "-L" || sMessage == "-l")
            {
                return BuildLongReply(asFiles);
            }
            else
            {
                return BuildShortReply(asFiles);
            }
        }
    }
}