using mwftp.ftp.connectionHandling;

namespace mwftp.ftp.commands
{
    internal class RemoveDirectoryCommandHandlerBase : CommandHandler
    {
        protected RemoveDirectoryCommandHandlerBase(string sCommand, FtpConnectionObject connectionObject)
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

            if (!ConnectionObject.FileSystemObject.DirectoryExists(sFile))
            {
                return GetMessage(550, string.Format("Directory does not exist"));
            }

            if (ConnectionObject.FileSystemObject.Delete(sFile))
            {
                return GetMessage(250, "Directory removed.");
            }
            else
            {
                return GetMessage(550, string.Format("Couldn't remove directory ({0}).", sFile));
            }
        }
    }
}