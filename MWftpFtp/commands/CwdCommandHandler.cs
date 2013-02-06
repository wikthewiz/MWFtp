using System.IO;
using mwftp.ftp.connectionHandling;
using mwftp.ftp.General;

namespace mwftp.ftp.commands
{
    internal class CwdCommandHandler : CommandHandler
    {
        public CwdCommandHandler(FtpConnectionObject connectionObject)
            : base("CWD", connectionObject)
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

        protected override string OnProcess(string sMessage)
        {
            sMessage = sMessage.Replace('/', '\\');

            if (!FileNameHelpers.IsValid(sMessage))
            {
                return GetMessage(550, "Not a valid directory string.");
            }

            string sDirectory = GetPath(sMessage);

            if (!ConnectionObject.FileSystemObject.DirectoryExists(sDirectory))
            {
                return GetMessage(550, "Not a valid directory.");
            }

            ConnectionObject.CurrentDirectory = Path.Combine(ConnectionObject.CurrentDirectory, sMessage);
            return GetMessage(250,
                              string.Format("CWD Successful ({0})", ConnectionObject.CurrentDirectory.Replace("\\", "/")));
        }
    }
}