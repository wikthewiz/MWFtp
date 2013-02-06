using mwftp.ftp.connectionHandling;
using mwftp.ftp.fileSystem;

namespace mwftp.ftp.commands
{
    internal class SizeCommandHandler : CommandHandler
    {
        public SizeCommandHandler(FtpConnectionObject connectionObject)
            : base("SIZE", connectionObject)
        {
        }

        protected override bool IsReadCommand
        {
            get { return true; }
        }

        protected override bool IsWriteCommand
        {
            get { return false; }
        }

        protected override string OnProcess(string sMessage)
        {
            string sPath = GetPath(sMessage);

            if (!ConnectionObject.FileSystemObject.FileExists(sPath))
            {
                return GetMessage(550, string.Format("File doesn't exist ({0})", sPath));
            }

            FtpFileInfo info = ConnectionObject.FileSystemObject.GetFileInfo(sPath);

            if (info == null)
            {
                return GetMessage(550, "Error in getting file information");
            }

            return GetMessage(220, info.GetSize().ToString());
        }
    }
}