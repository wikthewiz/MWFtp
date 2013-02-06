using System.Drawing;

namespace mwftp.app.view.formater
{
    internal class ErrorMessageFormater : MessageFormater
    {
        public ErrorMessageFormater(int id, string msg) : base(id, msg)
        {
        }

        public override Color MessageColor
        {
            get { return Color.Black; }
        }

        public override string Format()
        {
            return "<>" + GetFormatMessage();
        }
    }
}