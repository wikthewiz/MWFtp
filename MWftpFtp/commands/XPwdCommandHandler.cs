using mwftp.ftp.connectionHandling;

namespace mwftp.ftp.commands
{
    /// <summary>
    /// Present working directory command handler
    /// </summary>
    internal class XPwdCommandHandler : PwdCommandHandlerBase
    {
        public XPwdCommandHandler(FtpConnectionObject connectionObject)
            : base("XPWD", connectionObject)
        {
        }
    }
}