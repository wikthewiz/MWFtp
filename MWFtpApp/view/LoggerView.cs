using System;
using System.Windows.Forms;
using mwftp.app.view.formater;

namespace mwftp.app.view
{
    public partial class LoggerView : UserControl
    {
        #region Delegates

        public delegate void MessageHandlerDel(MessageFormater messageFormater);

        #endregion

        public LoggerView()
        {
            InitializeComponent();
            InitiateServerEventHandlers();
        }

        protected virtual void InitiateServerEventHandlers()
        {
        }

        protected void MessageHandler(MessageFormater messageFormater)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MessageHandlerDel(MessageHandler), new object[] {messageFormater});
            }
            else
            {
                listBoxMessages.SuspendLayout();

                addMessageLine(messageFormater);

                if (listBoxMessages.Lines.Length > 5000)
                {
                    removeFirstFromListBox();
                }

                scrollToEndOfListBox();

                listBoxMessages.ResumeLayout(true);
            }
        }

        private void addMessageLine(MessageFormater messageFormater)
        {
            listBoxMessages.SelectionColor = messageFormater.MessageColor;
            listBoxMessages.AppendText(messageFormater.Format());
        }

        private void removeFirstFromListBox()
        {
            var newLines = new string[listBoxMessages.Lines.Length - 1];
            Array.Copy(listBoxMessages.Lines, 1, newLines, 0, newLines.Length);
            listBoxMessages.Lines = newLines;
        }

        private void scrollToEndOfListBox()
        {
            listBoxMessages.ScrollToCaret();
        }
    }
}