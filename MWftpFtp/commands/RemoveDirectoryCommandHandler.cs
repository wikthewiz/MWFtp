using mwftp.ftp.connectionHandling;

namespace mwftp.ftp.commands
{
    internal class RemoveDirectoryCommandHandler : RemoveDirectoryCommandHandlerBase
    {
        public RemoveDirectoryCommandHandler(FtpConnectionObject connectionObject)
            : base("RMD", connectionObject)
        {
        }
    }
}