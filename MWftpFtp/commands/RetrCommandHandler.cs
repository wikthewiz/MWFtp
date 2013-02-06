using mwftp.ftp.connectionHandling;
using mwftp.ftp.fileSystem;

namespace mwftp.ftp.commands
{
    /// <summary>
    /// Implements the RETR command
    /// </summary>
    internal class RetrCommandHandler : CommandHandler
    {
        public RetrCommandHandler(FtpConnectionObject connectionObject)
            : base("RETR", connectionObject)
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
            string sFilePath = GetPath(sMessage);

            if (!ConnectionObject.FileSystemObject.FileExists(sFilePath))
            {
                return GetMessage(550, "File doesn't exist");
            }

            var replySocket = new FtpReplySocket(ConnectionObject);

            if (!replySocket.Loaded)
            {
                return GetMessage(550, "Unable to establish data connection");
            }

            Send("150 Starting data transfer, please wait...\r\n");

            const int bufferSize = 65536;

            FtpFile file = ConnectionObject.FileSystemObject.OpenFile(sFilePath, false);

            if (file == null)
            {
                return GetMessage(550, "Couldn't open file");
            }

            var byteBuffer = new byte[bufferSize];

            int read = file.Read(byteBuffer, bufferSize);

            try
            {
                while (read > 0)
                {
                    replySocket.Send(byteBuffer, read);
                    read = file.Read(byteBuffer, bufferSize);
                }

                return GetMessage(226, "File download succeeded.");
            }
            finally
            {
                file.Close();
                replySocket.Close();
            }
        }
    }
}