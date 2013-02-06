using System;
using System.Runtime.Serialization;

namespace mwftp.ftp.user
{
    public class UserNotFoundException : Exception
    {
        private readonly string user;

        public UserNotFoundException(string user)
        {
            this.user = user;
        }

        public UserNotFoundException(string user, string message) : base(message)
        {
            this.user = user;
        }

        public UserNotFoundException(string user, string message, Exception inner) : base(message, inner)
        {
            this.user = user;
        }

        protected UserNotFoundException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }

        public string User
        {
            get { return user; }
        }
    }
}