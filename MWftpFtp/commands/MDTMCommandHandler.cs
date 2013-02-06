using System;
using System.IO;
using mwftp.ftp.connectionHandling;
using mwftp.ftp.fileSystem;

namespace mwftp.ftp.commands
{
    internal class MDTMCommandHandler : CommandHandler
    {
        public MDTMCommandHandler(FtpConnectionObject connectionObject)
            : base("MDTM", connectionObject)
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

        protected override string OnProcess(string file)
        {
            string path = GetPath("");
            string fullFileName = Path.Combine(path, file);
            try
            {
                FtpFileInfo fileInfo = ConnectionObject.FileSystemObject.GetFileInfo(fullFileName);
                long gmt = fileInfo.GetModifiedTime().ToFileTimeUtc();
                return GetMessage(213, gmt.ToString());
            }
            catch (Exception e)
            {
                ServerEvents.ErrroMessage(this, new ServerEvent(ConnectionObject.Id, e.Message));
                return GetMessage(550, "No file named " + file);
            }
        }
    }
}