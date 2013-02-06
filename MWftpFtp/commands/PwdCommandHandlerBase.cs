using mwftp.ftp.connectionHandling;

namespace mwftp.ftp.commands
{
    /// <summary>
    /// Base class for present current directory commands
    /// </summary>
    internal class PwdCommandHandlerBase : CommandHandler
    {
        public PwdCommandHandlerBase(string sCommand, FtpConnectionObject connectionObject)
            : base(sCommand, connectionObject)
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
            string sDirectory = ConnectionObject.CurrentDirectory;
            sDirectory = sDirectory.Replace('\\', '/');
            return GetMessage(257, string.Format("\"{0}\" PWD Successful.", sDirectory));
        }
    }
}