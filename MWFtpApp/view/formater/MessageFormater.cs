using System;
using System.Drawing;

namespace mwftp.app.view.formater
{
    public abstract class MessageFormater
    {
        private readonly int id;
        private readonly string message;

        public MessageFormater(int id, string message)
        {
            this.id = id;
            this.message = message;
        }

        public abstract Color MessageColor { get; }
        public abstract string Format();

        protected string GetFormatMessage()
        {
            string formatedMessage = message.EndsWith("\r\n") ? message : message + Environment.NewLine;
            return string.Format("({0}) <{1}> {2}", id, DateTime.Now, formatedMessage);
        }
    }
}