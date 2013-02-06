using System.IO;

namespace mwftp.ftp.user
{
    public interface UserSettings
    {
        string Password { get; set; }
        string StartingDirectory { get; set; }
        FileAccess AccessRights { get; set; }
        string Name { get; }
        int MaxNrOfConnections { get; set; }
    }
}