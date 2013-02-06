using mwftp.ftp.connectionHandling;

namespace mwftp.ftp.commands
{
    internal class AlloCommandHandler : CommandHandler
    {
        public AlloCommandHandler(FtpConnectionObject connectionObject)
            : base("ALLO", connectionObject)
        {
        }


        protected override bool IsReadCommand
        {
            get { return false; }
        }

        protected override bool IsWriteCommand
        {
            get { return true; }
        }

        protected override string OnProcess(string sMessage)
        {
            return GetMessage(202, "Allo processed successfully (depreciated).");
        }
    }
}