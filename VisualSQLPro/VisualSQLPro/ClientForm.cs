using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace VisualSQLPro
{
    public partial class ClientForm : Form
    {
        private string _activeDb = "";
        private Theme _activeTheme = new Theme("Default");
        private Control _sqlTextControl = new RichTextBox();
        public ClientForm()
        {
            InitializeComponent();
            SetUpResizers();
            SetUpTimers();
            SetUpSqlTextBox();
            //SetUpScintilla();
            SetUpMetadata();
            SetUpThemes();
            PopUp_Cycle();
            tcp_listener.RunWorkerAsync();
            tcp_ping.RunWorkerAsync();
            SetUpTaskManager();
            BuildAndSendServerRequest((int) ServerRequests.GetMetadata, " ");
            ApplyWholeTheme(_activeTheme);
            console_log.AppendText("Welcome!");
            //PrintTable("[\r\n    {\"Name\":\"AAA\",\"Age\":\"22\",\"Married\":true},\r\n    {\"Name\":\"BBB\",\"Age\":\"25\",\"Married\":false},\r\n    {\"Name\":\"CCC\",\"Age\":\"38\",\"Married\":null}]");
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
                while (_lockedTcpConnection)
                {
                    //Lock until tcp connection can be used
                }
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
            if (_sqlTextControl.Text == @"Omae wa mou shindeiru")
                FancyConsolePrint("Nani?!", Color.DarkRed);
            else if (_sqlTextControl.Text != "")
            {
                string sqlString = _sqlTextControl.Text;
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
                _activeDb = data;
            }
            else if (dialogresult == DialogResult.OK && data == "")
                MessageBox.Show(@"Database name can't be empty.");
        }

        private string GetAssetFilePath(string fileName)
        {
            string solutionPath = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            if (solutionPath != null)
            {
                DirectoryInfo parentDir = Directory.GetParent(solutionPath);
                string testFolderPath = Path.Combine(parentDir.FullName, "assets");
                string filePath = Path.Combine(testFolderPath, fileName);
                return filePath;
            }
            return null;
        }
    }
}