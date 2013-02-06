using mwftp.app.view.formater;
using mwftp.ftp;

namespace mwftp.app.view
{
    public partial class AllLoggerView : LoggerView
    {
        protected override void InitiateServerEventHandlers()
        {
            ServerEvents.Info += FtpServerMessageHandler_Info;
            ServerEvents.Received += FtpServerMessageHandler_Received;
            ServerEvents.Send += FtpServerMessageHandler_Send;
            ServerEvents.Error += FtpServerMessageHandler_Error;
        }

        private void FtpServerMessageHandler_Error(object sender, ServerEvent e)
        {
            MessageHandler(new ErrorMessageFormater(e.Id, e.Message));
        }

        private void FtpServerMessageHandler_Send(object sender, ServerEvent e)
        {
            MessageHandler(new SendMessageFormater(e.Id, e.Message));
        }

        private void FtpServerMessageHandler_Received(object sender, ServerEvent e)
        {
            MessageHandler(new ReceivedMessageFormater(e.Id, e.Message));
        }

        private void FtpServerMessageHandler_Info(object sender, ServerEvent e)
        {
            MessageHandler(new InfoMessageFormater(e.Id, e.Message));
        }
    }
}