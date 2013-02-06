using System.Drawing;

namespace mwftp.app.view.formater
{
    internal class SendMessageFormater : MessageFormater
    {
        public SendMessageFormater(int id, string msg)
            : base(id, msg)
        {
        }

        public override Color MessageColor
        {
            get { return Color.Green; }
        }

        public override string Format()
        {
            return "<-- " + GetFormatMessage();
        }
    }
}