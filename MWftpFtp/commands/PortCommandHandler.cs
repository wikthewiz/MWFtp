using mwftp.ftp.connectionHandling;

namespace mwftp.ftp.commands
{
    internal class PortCommandHandler : CommandHandler
    {
        public PortCommandHandler(FtpConnectionObject connectionObject)
            : base("PORT", connectionObject)
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
            string[] asData = sMessage.Split(new[] {','});

            if (asData.Length != 6)
            {
                return GetMessage(550, "Error in setting up data connection");
            }

            int nSocketPort = int.Parse(asData[4])*256 + int.Parse(asData[5]);

            ConnectionObject.PortCommandSocketPort = nSocketPort;
            ConnectionObject.PortCommandSocketAddress = string.Join(".", asData, 0, 4);

            return GetMessage(200, string.Format("{0} command succeeded", Command));
        }
    }
}