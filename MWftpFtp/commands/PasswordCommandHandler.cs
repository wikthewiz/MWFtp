using mwftp.ftp.connectionHandling;

namespace mwftp.ftp.commands
{
    internal class PasswordCommandHandler : CommandHandler
    {
        public PasswordCommandHandler(FtpConnectionObject connectionObject)
            : base("PASS", connectionObject)
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

        protected override string OnProcess(string password)
        {
            switch (ConnectionObject.CheckLogin(password))
            {
                case FtpConnectionObject.LoginReturns.Access:
                    ConnectionObject.Login(password);
                    return GetMessage(220, "Password ok, FTP server ready");
                case FtpConnectionObject.LoginReturns.AccessDenied:
                    return GetMessage(530, "Username or password incorrect");
                case FtpConnectionObject.LoginReturns.MaxNrOfConnectionReached:
                    return GetMessage(530, "No free connections");
                default:
                    return GetMessage(530, "internal server error");
            }
        }
    }
}