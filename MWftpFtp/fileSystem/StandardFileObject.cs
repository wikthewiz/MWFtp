using System.IO;
using mwftp.util.General;

namespace mwftp.ftp.fileSystem
{
    internal class StandardFileObject : LoadedClass, FtpFile
    {
        private FileStream theFile;

        public StandardFileObject(string sPath, bool fWrite)
        {
            try
            {
                theFile = new FileStream(sPath,
                                         (fWrite) ? FileMode.OpenOrCreate : FileMode.Open,
                                         (fWrite) ? FileAccess.Write : FileAccess.Read);

                if (fWrite)
                {
                    theFile.Seek(0, SeekOrigin.End);
                }

                fLoaded = true;
            }
            catch (IOException)
            {
                theFile = null;
            }
        }

        #region FtpFile Members

        public int Read(byte[] abData, int nDataSize)
        {
            if (theFile == null)
            {
                return 0;
            }

            try
            {
                return theFile.Read(abData, 0, nDataSize);
            }
            catch (IOException)
            {
                return 0;
            }
        }

        public int Write(byte[] abData, int nDataSize)
        {
            if (theFile == null)
            {
                return 0;
            }

            try
            {
                theFile.Write(abData, 0, nDataSize);
            }
            catch (IOException)
            {
                return 0;
            }

            return nDataSize;
        }

        public void Close()
        {
            if (theFile != null)
            {
                try
                {
                    theFile.Close();
                }
                catch (IOException)
                {
                }

                theFile = null;
            }
        }

        #endregion
    }
}