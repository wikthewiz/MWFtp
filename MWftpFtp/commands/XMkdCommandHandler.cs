using mwftp.ftp.connectionHandling;

namespace mwftp.ftp.commands
{
    internal class XMkdCommandHandler : MakeDirectoryCommandHandlerBase
    {
        public XMkdCommandHandler(FtpConnectionObject connectionObject)
            : base("XMKD", connectionObject)
        {
        }
    }
}