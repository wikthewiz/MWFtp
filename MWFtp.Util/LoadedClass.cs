namespace mwftp.util.General
{
    public interface Loaded
    {
        bool Loaded { get; }
    }

    public class LoadedClass : Loaded
    {
        protected bool fLoaded;

        #region Loaded Members

        public bool Loaded
        {
            get { return fLoaded; }
        }

        #endregion
    }
}