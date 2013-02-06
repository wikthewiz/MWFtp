using mwftp.ftp.connectionHandling;

namespace mwftp.ftp.commands
{
    internal class MakeDirectoryCommandHandler : MakeDirectoryCommandHandlerBase
    {
        public MakeDirectoryCommandHandler(FtpConnectionObject connectionObject)
            : base("MKD", connectionObject)
        {
        }
    }
}