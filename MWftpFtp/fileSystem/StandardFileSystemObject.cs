using System.IO;

namespace mwftp.ftp.fileSystem
{
    internal class StandardFileSystemObject : FileSystem
    {
        #region Member Variables

        private readonly string startDirectory = "";

        #endregion

        #region Construction

        public StandardFileSystemObject(string startDirectory)
        {
            this.startDirectory = startDirectory;
        }

        #endregion

        #region Methods

        private string GetPath(string sPath)
        {
            if (sPath.Length == 0)
            {
                return startDirectory;
            }

            if (sPath[0] == '\\')
            {
                sPath = sPath.Substring(1);
            }

            return Path.Combine(startDirectory, sPath);
        }

        #endregion

        #region FileSystem Members

        public FtpFile OpenFile(string sPath, bool fWrite)
        {
            var file = new StandardFileObject(GetPath(sPath), fWrite);

            if (file.Loaded)
            {
                return file;
            }
            else
            {
                return null;
            }
        }

        public FtpFileInfo GetFileInfo(string sPath)
        {
            var info = new StandardFileInfoObject(GetPath(sPath));
            if (info.Loaded)
            {
                return info;
            }
            else
            {
                return null;
            }
        }

        public string[] GetFiles(string sPath)
        {
            string sCurrentPath = GetPath(sPath);
            string[] asFiles = Directory.GetFiles(sCurrentPath);
            RemovePath(asFiles, sCurrentPath);
            return asFiles;
        }

        public string[] GetFiles(string sPath, string sWildcard)
        {
            string sCurrentPath = GetPath(sPath);
            string[] asFiles = Directory.GetFiles(sCurrentPath, sWildcard);
            RemovePath(asFiles, sCurrentPath);
            return asFiles;
        }

        public string[] GetDirectories(string sPath)
        {
            string sCurrentPath = GetPath(sPath);
            string[] asFiles = Directory.GetDirectories(sCurrentPath);
            RemovePath(asFiles, sCurrentPath);
            return asFiles;
        }

        public string[] GetDirectories(string sPath, string sWildcard)
        {
            string sCurrentPath = GetPath(sPath);
            string[] asFiles = Directory.GetDirectories(sCurrentPath, sWildcard);
            RemovePath(asFiles, sCurrentPath);
            return asFiles;
        }

        public bool DirectoryExists(string sPath)
        {
            return Directory.Exists(GetPath(sPath));
        }

        public bool FileExists(string sPath)
        {
            return File.Exists(GetPath(sPath));
        }

        public bool Move(string sOldPath, string sNewPath)
        {
            string sFullPathOld = GetPath(sOldPath);
            string sFullPathNew = GetPath(sNewPath);

            try
            {
                var info = new FileInfo(sFullPathOld);

                if (info == null)
                {
                    return false;
                }

                if ((info.Attributes & FileAttributes.Directory) != 0)
                {
                    Directory.Move(sFullPathOld, sFullPathNew);
                }
                else
                {
                    File.Move(sFullPathOld, sFullPathNew);
                }
            }
            catch (IOException)
            {
                return false;
            }

            return true;
        }

        public bool Delete(string sPath)
        {
            try
            {
                string sFullPath = GetPath(sPath);

                var info = new FileInfo(sFullPath);

                if (info == null)
                {
                    return false;
                }

                if ((info.Attributes & FileAttributes.Directory) != 0)
                {
                    Directory.Delete(sFullPath);
                }
                else
                {
                    File.Delete(sFullPath);
                }
            }
            catch (IOException)
            {
                return false;
            }

            return true;
        }

        public bool CreateDirectory(string sPath)
        {
            string sFullPath = GetPath(sPath);

            try
            {
                Directory.CreateDirectory(sFullPath);
            }
            catch (IOException)
            {
                return false;
            }

            return true;
        }

        #endregion

        private void RemovePath(string[] asFiles, string sPath)
        {
            int nIndex = 0;

            string sPathLowerCase = sPath.ToLower();

            foreach (string sString in asFiles)
            {
                if (sString.Substring(0, sPath.Length).ToLower() == sPathLowerCase)
                {
                    string sFileName = sString.Substring(sPath.Length);

                    if (sFileName.Length > 0 && sFileName[0] == '\\')
                    {
                        sFileName = sFileName.Substring(1);
                    }

                    asFiles[nIndex] = sFileName;
                }

                nIndex += 1;
            }
        }
    }
}