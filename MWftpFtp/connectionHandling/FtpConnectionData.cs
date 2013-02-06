using System.Net.Sockets;
using mwftp.ftp.fileSystem;

namespace mwftp.ftp.connectionHandling
{
    /// <summary>
    /// Contains all the data relating to a particular FTP connection
    /// </summary>
    internal class FtpConnectionData
    {
        #region Member Variables

        private readonly int id;
        private readonly TcpClient theSocket;
        private string currentDirectory = "\\";
        private FileSystem fileSystem;
        private string portCommandSocketAddress = "";
        private int portCommandSocketPort = 20;

        #endregion

        #region Construction

        public FtpConnectionData(int id, TcpClient socket)
        {
            this.id = id;
            theSocket = socket;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Main connection socket
        /// </summary>
        public TcpClient Socket
        {
            get { return theSocket; }
        }

        public string User { get; set; }

        /// <summary>
        /// This connection's current directory
        /// </summary>
        public string CurrentDirectory
        {
            get { return currentDirectory; }

            set { currentDirectory = value; }
        }

        /// <summary>
        /// This connection's Id
        /// </summary>
        public int Id
        {
            get { return id; }
        }

        /// <summary>
        /// Socket address from PORT command.
        /// See FtpReplySocket class.
        /// </summary>
        public string PortCommandSocketAddress
        {
            get { return portCommandSocketAddress; }

            set { portCommandSocketAddress = value; }
        }

        /// <summary>
        /// Port from PORT command.
        /// See FtpReplySocket class.
        /// </summary>
        public int PortCommandSocketPort
        {
            get { return portCommandSocketPort; }

            set { portCommandSocketPort = value; }
        }

        /// <summary>
        /// Whether the connection is in binary or ASCII transfer mode.
        /// </summary>
        public bool BinaryMode { get; set; }

        /// <summary>
        /// If this is non-null the last command was a PASV and should therefore use this socket.
        /// If this is null the last command was a PORT command and should therefore use that mechanism instead.
        /// </summary>
        public TcpClient PasvSocket { get; set; }

        /// <summary>
        /// Rename takes place with 2 commands - we store the old name here
        /// </summary>
        public string FileToRename { get; set; }

        /// <summary>
        /// This is true if the file to rename is a directory
        /// </summary>
        public bool RenameDirectory { get; set; }

        public FileSystem FileSystemObject
        {
            get { return fileSystem; }
        }

        protected void SetFileSystemObject(FileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        #endregion
    }
}