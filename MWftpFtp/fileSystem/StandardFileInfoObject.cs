using System;
using System.IO;
using System.Text;
using mwftp.util.General;

namespace mwftp.ftp.fileSystem
{
    internal class StandardFileInfoObject : LoadedClass, FtpFileInfo
    {
        #region Member Variables

        private readonly FileInfo theInfo;

        #endregion

        #region Construction

        public StandardFileInfoObject(string sPath)
        {
            try
            {
                theInfo = new FileInfo(sPath);
                fLoaded = true;
            }
            catch (IOException)
            {
                theInfo = null;
            }
        }

        #endregion

        #region FtpFileInfo Members

        public bool IsDirectory()
        {
            return (theInfo.Attributes & FileAttributes.Directory) != 0;
        }

        public DateTime GetModifiedTime()
        {
            return theInfo.LastWriteTime;
        }

        public long GetSize()
        {
            return theInfo.Length;
        }

        public string GetAttributeString()
        {
            bool fDirectory = (theInfo.Attributes & FileAttributes.Directory) != 0;
            bool fReadOnly = (theInfo.Attributes & FileAttributes.ReadOnly) != 0;

            var builder = new StringBuilder();

            if (fDirectory)
            {
                builder.Append("d");
            }
            else
            {
                builder.Append("-");
            }

            builder.Append("r");

            if (fReadOnly)
            {
                builder.Append("-");
            }
            else
            {
                builder.Append("w");
            }

            if (fDirectory)
            {
                builder.Append("x");
            }
            else
            {
                builder.Append("-");
            }

            if (fDirectory)
            {
                builder.Append("r-xr-x");
            }
            else
            {
                builder.Append("r--r--");
            }

            return builder.ToString();
        }

        #endregion
    }
}