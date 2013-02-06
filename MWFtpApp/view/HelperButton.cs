using System;
using System.Windows.Forms;

namespace mwftp.app.view
{
    internal class HelperButton : Button
    {
        public void GenerateClick(EventArgs e)
        {
            OnClick(e);
        }
    }
}