using System.Collections.Generic;

namespace mwftp.ftp.user
{
    public class LogedInConnectionCounter
    {
        private readonly List<int> idList = new List<int>(10);

        public int Count
        {
            get { return idList.Count; }
        }

        internal void Add(int connectionId)
        {
            idList.Add(connectionId);
        }

        internal void Remove(int connectionId)
        {
            idList.Remove(connectionId);
        }

        internal bool Contains(int connectionId)
        {
            return idList.Contains(connectionId);
        }
    }
}