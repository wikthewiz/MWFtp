using mwftp.app.view.formater;
using mwftp.ftp;

namespace mwftp.app.view
{
    public partial class ErrorLoggerView : LoggerView
    {
        public ErrorLoggerView()
        {
            InitializeComponent();
        }

        protected override void InitiateServerEventHandlers()
        {
            ServerEvents.Error += FtpServerMessageHandler_Error;
        }

        private void FtpServerMessageHandler_Error(object sender, ServerEvent e)
        {
            MessageHandler(new ErrorMessageFormater(e.Id, e.Message));
        }
    }
}