using System;
using System.Drawing;

namespace mwftp.app.view.formater
{
    internal class InfoMessageFormater : MessageFormater
    {
        public InfoMessageFormater(int id, string msg)
            : base(id, msg)
        {
        }

        public override Color MessageColor
        {
            get { return Color.Blue; }
        }

        public override string Format()
        {
            string msg = GetFormatMessage();
            string message = msg.Remove(msg.Length - Environment.NewLine.Length);
            return "[ " + message + " ]" + Environment.NewLine;
        }
    }
}