using System;
using System.Drawing;
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
            //UpdateTaskManager("{\r\n\t\"Transactions\": [\r\n\t\t{\r\n\t\t\t\"Transaction_ID\":\"b86ilpuloh16jmkg9vog\",\r\n\t\t\t\"TransactionQueries\":[\"CREATE TABLE TABLA_1.\", \"UNKNOWN QUERY.\"],\r\n\t\t\t\"Transaction_State\":0\r\n\t\t}\r\n\t]\r\n}");
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
    }

    enum ServerRequests
    {
        KeepAlive = 200,
        NewDatabase = 201,
        LoadDatabase = 202,
        NewTable = 203,
        FindTable = 204,
        GetMetadata = 205,
        Query = 206,
        ShowTransaction = 207,
        Error = 208
    }
}