using System;
using System.Net;
using System.Windows.Forms;
using mwftp.ftp;
using mwftp.util.General;

namespace mwftp.app.view
{
    public partial class ServerSettingsForm : Form
    {
        public ServerSettingsForm()
        {
            InitializeComponent();
            initValues();
            addIpsTpList();
            setSelectedIp();
        }

        private void addIpsTpList()
        {
            listBoxIpAddresses.Items.Clear();
            IPAddress[] addresses = SocketHelpers.GetLocalAddresses();
            foreach (IPAddress ip in addresses)
            {
                addIpToList(ip);
            }
        }

        private void setSelectedIp()
        {
            listBoxIpAddresses.SelectedItem = SocketHelpers.GetLocalAddress();
        }

        private void addIpToList(IPAddress ipAddress)
        {
            listBoxIpAddresses.Items.Add(ipAddress);
        }

        private void initValues()
        {
            textBoxPort.Text = FtpServer.Settings.Port.ToString();
            textBoxMaxConnectionUser.Text = FtpServer.Settings.MaxNrOfConnectionPerUser.ToString();
            textBoxMaxConnections.Text = FtpServer.Settings.MaxNrOfConnection.ToString();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                FtpServer.Settings.Port = getPort();
                FtpServer.Settings.MaxNrOfConnectionPerUser = getMaxConnectionPerUser();
                FtpServer.Settings.MaxNrOfConnection = getMaxConnections();
                SocketHelpers.SetLocalIpAddress((IPAddress) listBoxIpAddresses.SelectedItem);
                Dispose();
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Dispose();
        }


        private int getPort()
        {
            try
            {
                return toInt(textBoxPort.Text);
            }
            catch (FormatException)
            {
                throw new FormatException("Porten måste vara en siffra");
            }
        }

        private int getMaxConnections()
        {
            try
            {
                return toInt(textBoxMaxConnections.Text);
            }
            catch (FormatException)
            {
                throw new FormatException("Max antalet uppkopplingar måste vara en siffra");
            }
        }

        private int getMaxConnectionPerUser()
        {
            try
            {
                return toInt(textBoxMaxConnectionUser.Text);
            }
            catch (FormatException)
            {
                throw new FormatException("Antale uppkopplingar per användare måste vara en siffra");
            }
        }

        private int toInt(string number)
        {
            return int.Parse(number);
        }
    }
}