using mwftp.ftp.connectionHandling;

namespace mwftp.ftp.commands
{
    /// <summary>
    /// Present working directory command handler
    /// </summary>
    internal class PwdCommandHandler : PwdCommandHandlerBase
    {
        public PwdCommandHandler(FtpConnectionObject connectionObject)
            : base("PWD", connectionObject)
        {
        }
    }
}