using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using mwftp.app.exception;
using mwftp.app.controller;

namespace mwftp.app.view
{
    public partial class SetAccesRightsForm : Form
    {
        private UserFormController controller;
        public SetAccesRightsForm(UserFormController controller)
        {
            InitializeComponent();
            this.controller = controller;
        }

        private void initiate() 
        {
            switch (controller.InitStateOfFileAccess)
            {
                case StartFileAccess.Read:
                    listBoxRights.SelectedItem = "Läs";
                    break;
                case StartFileAccess.Write:
                    listBoxRights.SelectedItem = "Skriv";
                    break;
                case StartFileAccess.ReadWrite:
                    listBoxRights.SelectedItem = "Läs&Skriv";
                    break;
                default:
                    listBoxRights.SelectedItem = "N/A";
                    break;
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                controller.User.AccessRights = getAccess();
            }
            catch (CancelExcpetion) 
            { 
                controller.Cancel();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Dispose();
            }
        }

        private FileAccess getAccess()
        {
            switch (listBoxRights.SelectedItem.ToString())
            {
                case "Läs":
                    return FileAccess.Read;
                case "Skriv":
                    return FileAccess.Read;
                case "Skriv&Läs":
                    return FileAccess.Read;
                default:
                    throw new CancelExcpetion();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            controller.Cancel();
            Dispose();
        }
    }
}
