using mwftp.ftp.connectionHandling;

namespace mwftp.ftp.commands
{
    internal class RenameCompleteCommandHandler : CommandHandler
    {
        public RenameCompleteCommandHandler(FtpConnectionObject connectionObject)
            : base("RNTO", connectionObject)
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
            if (ConnectionObject.FileToRename.Length == 0)
            {
                return GetMessage(503, "RNTO must be preceded by a RNFR.");
            }

            string sNewFileName = GetPath(sMessage);
            string sOldFileName = ConnectionObject.FileToRename;

            ConnectionObject.FileToRename = "";

            if (ConnectionObject.FileSystemObject.FileExists(sNewFileName) ||
                ConnectionObject.FileSystemObject.DirectoryExists(sNewFileName))
            {
                return GetMessage(553, string.Format("File already exists ({0}).", sNewFileName));
            }

            if (!ConnectionObject.FileSystemObject.Move(sOldFileName, sNewFileName))
            {
                return GetMessage(553, "Move failed");
            }

            return GetMessage(250, "Renamed file successfully.");
        }
    }
}