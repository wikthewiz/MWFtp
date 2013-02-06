using mwftp.ftp.connectionHandling;
using mwftp.ftp.fileSystem;

namespace mwftp.ftp.commands
{
    internal class StoreCommandHandler : CommandHandler
    {
        private const int nBufferSize = 65536;

        public StoreCommandHandler(FtpConnectionObject connectionObject)
            : base("STOR", connectionObject)
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

            if (ConnectionObject.FileSystemObject.FileExists(sFile))
            {
                return GetMessage(553, "File already exists.");
            }

            FtpFile file = ConnectionObject.FileSystemObject.OpenFile(sFile, true);

            var socketReply = new FtpReplySocket(ConnectionObject);

            if (!socketReply.Loaded)
            {
                return GetMessage(425, "Error in establishing data connection.");
            }

            var abData = new byte[nBufferSize];

            Send(GetMessage(150, "Opening connection for data transfer."));

            int nReceived = socketReply.Receive(abData);

            while (nReceived > 0)
            {
                file.Write(abData, nReceived);
                nReceived = socketReply.Receive(abData);
            }

            file.Close();
            socketReply.Close();

            return GetMessage(226, "Uploaded file successfully.");
        }
    }
}