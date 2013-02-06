using System;
using System.IO;
using System.Text;
using mwftp.ftp.connectionHandling;
using mwftp.ftp.fileSystem;
using mwftp.ftp.General;
using mwftp.util.General;

namespace mwftp.ftp.commands
{
    /// <summary>
    /// Base class for list commands
    /// </summary>
    internal abstract class ListCommandHandlerBase : CommandHandler
    {
        public ListCommandHandlerBase(string sCommand, FtpConnectionObject connectionObject)
            : base(sCommand, connectionObject)
        {
        }

        protected override string OnProcess(string sMessage)
        {
            Send("150 Opening data connection for LIST\r\n");

            string[] asFiles = null;
            string[] asDirectories = null;

            sMessage = sMessage.Trim();

            string sPath = GetPath("");

            if (sMessage.Length == 0 || sMessage[0] == '-')
            {
                asFiles = ConnectionObject.FileSystemObject.GetFiles(sPath);
                asDirectories = ConnectionObject.FileSystemObject.GetDirectories(sPath);
            }
            else
            {
                asFiles = ConnectionObject.FileSystemObject.GetFiles(sPath, sMessage);
                asDirectories = ConnectionObject.FileSystemObject.GetDirectories(sPath, sMessage);
            }

            var asAll = ArrayHelpers.Add(asDirectories, asFiles) as string[];
            string sFileList = BuildReply(sMessage, asAll);

            var socketReply = new FtpReplySocket(ConnectionObject);

            if (!socketReply.Loaded)
            {
                return GetMessage(550, "LIST unable to establish return connection.");
            }

            try
            {
                socketReply.Send(sFileList, ConnectionObject.Id);
                return GetMessage(226, "LIST successful.");
            }
            finally
            {
                socketReply.Close();
            }
        }

        protected abstract string BuildReply(string sMessage, string[] asFiles);

        protected string BuildShortReply(string[] asFiles)
        {
            string sFileList = string.Join("\r\n", asFiles);
            sFileList += "\r\n";
            return sFileList;
        }

        protected string BuildLongReply(string[] asFiles)
        {
            string directory = GetPath("");

            var stringBuilder = new StringBuilder();

            for (int index = 0; index < asFiles.Length; index++)
            {
                string file = asFiles[index];
                file = Path.Combine(directory, file);

                FtpFileInfo info = ConnectionObject.FileSystemObject.GetFileInfo(file);

                if (info != null)
                {
                    string sAttributes = info.GetAttributeString();
                    stringBuilder.Append(sAttributes);
                    stringBuilder.Append(" 1 owner group");

                    if (info.IsDirectory())
                    {
                        stringBuilder.Append("            1 ");
                    }
                    else
                    {
                        string sFileSize = info.GetSize().ToString();
                        stringBuilder.Append(TextHelpers.RightAlignString(sFileSize, 13, ' '));
                        stringBuilder.Append(" ");
                    }

                    DateTime fileDate = info.GetModifiedTime();

                    string sDay = fileDate.Day.ToString();

                    stringBuilder.Append(TextHelpers.Month(fileDate.Month));
                    stringBuilder.Append(" ");

                    if (sDay.Length == 1)
                    {
                        stringBuilder.Append(" ");
                    }

                    stringBuilder.Append(sDay);
                    stringBuilder.Append(" ");
                    stringBuilder.Append(string.Format("{0:hh}", fileDate));
                    stringBuilder.Append(":");
                    stringBuilder.Append(string.Format("{0:mm}", fileDate));
                    stringBuilder.Append(" ");

                    stringBuilder.Append(asFiles[index]);
                    stringBuilder.Append("\r\n");
                }
            }

            return stringBuilder.ToString();
        }
    }
}