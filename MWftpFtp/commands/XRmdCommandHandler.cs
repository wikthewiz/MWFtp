using mwftp.ftp.connectionHandling;

namespace mwftp.ftp.commands
{
    internal class XRmdCommandHandler : RemoveDirectoryCommandHandlerBase
    {
        public XRmdCommandHandler(FtpConnectionObject connectionObject)
            : base("XRMD", connectionObject)
        {
        }
    }
}