using System;
using System.Windows.Forms;

namespace VisualSQLPro
{
    public partial class ClientForm : Form
    {
        public ClientForm()
        {
            InitializeComponent();
            SetUpResizers();
            SetUpTimers();
            SetUpScintilla();
            task_manager_groupBox.Visible = false;
            PopUp_Cycle();
            tcp_listener.RunWorkerAsync();
            tcp_ping.RunWorkerAsync();
            BuildAndSendServerRequest(1, " ");
        }

        private void PopUp_Cycle()
        {
            PopUp_Connection();
            while (!connection_success())
            {
                MessageBox.Show(@"Could not establish connection with IP address and port number, please try again.");
                PopUp_Connection();
            }
        }

        private void PopUp_Connection()
        {
            PopupForm popup = new PopupForm {StartPosition = FormStartPosition.CenterParent};
            DialogResult dialogresult = popup.ShowDialog();
            _ipAddress = popup.ipAddress;
            _portNumber = popup.portNumber;
            popup.Dispose();
            if (dialogresult == DialogResult.Cancel)
                Environment.Exit(1);
        }
        
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _client.Close();
            base.OnFormClosing(e);
        }
    }
}