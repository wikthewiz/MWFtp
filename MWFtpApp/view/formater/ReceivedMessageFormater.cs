using System.Drawing;

namespace mwftp.app.view.formater
{
    internal class ReceivedMessageFormater : MessageFormater
    {
        public ReceivedMessageFormater(int id, string msg)
            : base(id, msg)
        {
        }

        public override Color MessageColor
        {
            get { return Color.Red; }
        }

        public override string Format()
        {
            return "--> " + GetFormatMessage();
        }
    }
}