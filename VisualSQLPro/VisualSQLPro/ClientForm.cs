using System;
using System.Drawing;
using System.IO;
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
            PopUp_Cycle();
            tcp_listener.RunWorkerAsync();
            tcp_ping.RunWorkerAsync();
            SetUpTaskManager();
            BuildAndSendServerRequest((int) ServerRequests.GetMetadata, " ");
            console_log.AppendText("Welcome!");
            SetUpThemes();
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
            try
            {
                _client.Close();
            }
            catch (Exception)
            {
                //throw;
            }
            base.OnFormClosing(e);
        }

        private void execute_sql()
        {
            if (_myScintilla.Text == @"Omae wa mou shindeiru")
                FancyConsolePrint("Nani?!", Color.DarkRed);
            else if (_myScintilla.Text != "")
            {
                string sqlString = _myScintilla.Text;
                console_send(sqlString);
                send_sql_text(sqlString);
            }
        }

        private void CreateDb()
        {
            GeneralDataInputPopup popup = new GeneralDataInputPopup { StartPosition = FormStartPosition.CenterParent };
            popup.Text = @"Input DB Name";
            popup.ChangeLabelText("Input new database name:");
            DialogResult dialogresult = popup.ShowDialog();
            string data = popup.Data;
            popup.Dispose();
            if (dialogresult == DialogResult.OK && data != "")
            {
                if (Path.GetExtension(data) != ".db")
                    data += ".db";
                BuildAndSendServerRequest((int)ServerRequests.NewDatabase, data);
            }
            else if (dialogresult == DialogResult.OK && data == "")
                MessageBox.Show(@"Database name can't be empty.");
        }
    }
}