using mwftp.ftp.connectionHandling;
using mwftp.ftp.fileSystem;

namespace mwftp.ftp.commands
{
    /// <summary>
    /// Starts a rename file operation
    /// </summary>
    internal class RenameStartCommandHandler : CommandHandler
    {
        public RenameStartCommandHandler(FtpConnectionObject connectionObject)
            : base("RNFR", connectionObject)
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

            ConnectionObject.FileToRename = sFile;

            FtpFileInfo info = ConnectionObject.FileSystemObject.GetFileInfo(sFile);

            if (info == null)
            {
                return GetMessage(550, string.Format("File does not exist ({0}).", sFile));
            }

            ConnectionObject.RenameDirectory = info.IsDirectory();
            return GetMessage(350, string.Format("Rename file started ({0}).", sFile));
        }
    }
}