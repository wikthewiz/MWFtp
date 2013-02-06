using mwftp.ftp.connectionHandling;

namespace mwftp.ftp.commands
{
    /// <summary>
    /// Delete command handler
    /// </summary>
    internal class DeleCommandHandler : CommandHandler
    {
        public DeleCommandHandler(FtpConnectionObject connectionObject)
            : base("DELE", connectionObject)
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

            if (!ConnectionObject.FileSystemObject.FileExists(sFile))
            {
                return GetMessage(550, "File does not exist.");
            }

            if (!ConnectionObject.FileSystemObject.Delete(sFile))
            {
                return GetMessage(550, "Couldn't delete file.");
            }

            return GetMessage(250, "File deleted successfully");
        }
    }
}