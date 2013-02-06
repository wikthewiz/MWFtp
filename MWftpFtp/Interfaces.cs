using System;
using System.Runtime.Serialization;

namespace mwftp.ftp.fileSystem
{
    public interface FtpFile
    {
        int Read(byte[] abData, int nDataSize);
        int Write(byte[] abData, int nDataSize);
        void Close();
    }

    public interface FtpFileInfo
    {
        DateTime GetModifiedTime();
        long GetSize();
        string GetAttributeString();
        bool IsDirectory();
    }

    public interface FileSystem
    {
        FtpFile OpenFile(string sPath, bool write);
        FtpFileInfo GetFileInfo(string path);

        string[] GetFiles(string path);
        string[] GetFiles(string path, string wildcard);
        string[] GetDirectories(string path);
        string[] GetDirectories(string path, string wildcard);

        bool DirectoryExists(string sPath);
        bool FileExists(string sPath);

        bool CreateDirectory(string path);
        bool Move(string sOldPath, string newPath);
        bool Delete(string path);
    }

    public interface IFileSystemClassFactory
    {
        bool HasFreeConnections(string userName);
        bool IsCorrectUserPassword(string userName, string password);
        FileSystem Create(string sUser, string sPassword);
    }

    [Serializable]
    public class NoFreeConnectionsException : Exception
    {
        public NoFreeConnectionsException()
        {
        }

        public NoFreeConnectionsException(string message) : base(message)
        {
        }

        public NoFreeConnectionsException(string message, Exception inner) : base(message, inner)
        {
        }

        protected NoFreeConnectionsException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}