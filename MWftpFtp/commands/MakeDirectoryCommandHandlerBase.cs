using mwftp.ftp.connectionHandling;

namespace mwftp.ftp.commands
{
    internal class MakeDirectoryCommandHandlerBase : CommandHandler
    {
        protected MakeDirectoryCommandHandlerBase(string sCommand, FtpConnectionObject connectionObject)
            : base(sCommand, connectionObject)
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
            string sFile = GetPath(sMessage);

            if (!ConnectionObject.FileSystemObject.CreateDirectory(sFile))
            {
                return GetMessage(550, string.Format("Couldn't create directory. ({0})", sFile));
            }

            return GetMessage(257, sFile);
        }
    }
}