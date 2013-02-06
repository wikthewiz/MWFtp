using mwftp.ftp.connectionHandling;
using mwftp.ftp.fileSystem;

namespace mwftp.ftp.commands
{
    internal class AppendCommandHandler : CommandHandler
    {
        private const int nBufferSize = 65536;

        public AppendCommandHandler(FtpConnectionObject connectionObject)
            : base("APPE", connectionObject)
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

            FtpFile file = ConnectionObject.FileSystemObject.OpenFile(sFile, true);

            if (file == null)
            {
                return GetMessage(425, "Couldn't open file");
            }

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
                nReceived = socketReply.Receive(abData);
                file.Write(abData, nReceived);
            }

            file.Close();
            socketReply.Close();

            return GetMessage(226, string.Format("Appended file successfully. ({0})", sFile));
        }
    }
}