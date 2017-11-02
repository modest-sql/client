using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace VisualSQL
{
    public partial class ClientForm : Form
    {
        private string ipAddress;
        private string portNumber;
        private bool testing = false;
        private TcpClient client;
        private NetworkStream nwStream;
        private List<Metadata> current_metadata = new List<Metadata>();

        public ClientForm()
        {
            InitializeComponent();
            this.Text = "Modest SQL Client";
            if (!testing)
            {
                PopUp_Connection();
                while (!connection_success())
                {
                    MessageBox.Show("Could not establish connection with IP address and port number, please try again.");
                    PopUp_Connection();
                }
            }
            console_log.AppendText("Console log:");
            tcp_listener.RunWorkerAsync();
        }

        private bool connection_success()
        {
            try
            {
                open_connection();
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            return true;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            close_connection();
            base.OnFormClosing(e);
        }

        private void PopUp_Connection()
        {
            PopupForm popup = new PopupForm();
            popup.StartPosition = FormStartPosition.CenterParent;
            popup.Text = "TCP Connection";
            DialogResult dialogresult = popup.ShowDialog();
            ipAddress = popup.ipAddress;
            portNumber = popup.portNumber;
            popup.Dispose();
        }

        private DataTable DerializeDataTable(string json)
        {
            var table = JsonConvert.DeserializeObject<DataTable>(json);
            return table;
        }

        private void run_button_Click(object sender, EventArgs e)
        {
            if (sql_text.Text != "")
            {
                string json;
                string db_data;
                string sql_string = sql_text.Text;
                //sql_text.Text = "";
                console_send(sql_string);
                if (testing)
                {
                    json = @"[{""Name"":""AAA"",""Age"":""22"",""Job"":""PPP""},{""Name"":""BBB"",""Age"":""25"",""Job"":""QQQ""},{""Name"":""CCC"",""Age"":""38"",""Job"":""RRR""}]";
                    db_data = @"{""DB_Name"": ""Mocca DB"",""Tables"": [{""Table_Name"": ""Employee"", ""ColumnNames"": [""First Name"", ""Second Name"", ""First Last Name"", ""Second Last Name"", ""Age"", ""Married""],""ColumnTypes"": [""char[100]"", ""char[100]"", ""char[100]"", ""char[100]"", ""int"", ""boolean""]},{""Table_Name"": ""Department"", ""ColumnNames"": [""Department Name"", ""Description"", ""Address""],""ColumnTypes"": [""char[100]"", ""char[100]"", ""char[100]""]}]}";
                    //sql_text.Text = ipAddress + ":" + portNumber;
                    //console_receive("response from fake server, don't believe what I tell you, but you succeeded!");
                    flashy_testing_console_receive();

                    DB_Metadata db_meta = new DB_Metadata();
                    db_meta = JsonConvert.DeserializeObject<DB_Metadata>(db_data);
                    update_new_metadata(db_meta);
                    draw_db_meta();
                    var table = JsonConvert.DeserializeObject<DataTable>(json);
                    table_dataGridView.DataSource = table;
                }
                else
                {
                    send_sql_text(sql_string);
                }
                //var table = JsonConvert.DeserializeObject<DataTable>(json);
                //dataGridView1.DataSource = table;
            }
        }

        private void flashy_testing_console_receive()
        {
            console_log.AppendText(Environment.NewLine + "received: response from fake server, ");
            console_log.Select(console_log.TextLength, 0);
            console_log.SelectionColor = Color.Red;
            console_log.AppendText("don't believe what I tell you, ");
            console_log.Select(console_log.TextLength, 0);
            console_log.SelectionColor = Color.Blue;
            console_log.AppendText("but you succeeded!");
            console_log.SelectionColor = Color.Black;
        }

        private void flashy_console_receive(string str)
        {
            var trimmed = str.Trim('\0');
            trimmed = trimmed.Trim('[');
            trimmed = trimmed.Trim(']');
            var des = JsonConvert.DeserializeObject<server_response>(trimmed);
            //sql_text.Text = des.Error;
            if (des.Error != "no error")
            {
                console_log.Select(console_log.TextLength, 0);
                console_log.SelectionColor = Color.Red;
                console_log.AppendText(Environment.NewLine + "received: " + des.Error);
                console_log.SelectionColor = Color.Black;
            }
            else
            {
                console_log.Select(console_log.TextLength, 0);
                console_log.SelectionColor = Color.Blue;
                console_log.AppendText(Environment.NewLine + "received: success, no error");
                console_log.SelectionColor = Color.Black;
            }
            console_log.ScrollToCaret();
        }

        private void console_receive(string str)
        {
            console_log.AppendText(Environment.NewLine + "received: " + str);
            console_log.ScrollToCaret();
        }

        private void console_send(string str)
        {
            console_log.Select(console_log.TextLength, 0);
            console_log.SelectionColor = Color.Black;
            console_log.AppendText(Environment.NewLine + "sending: " + str);
            console_log.ScrollToCaret();
        }

        void metadata_listBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = metadata_listBox.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                switch (current_metadata[index].metadata_type)
                {
                    case metadata_type.DB_NAME:
                        switch_database(current_metadata[index].value_name);
                        break;
                    case metadata_type.DB_TABLE:
                        switch_table(current_metadata[index].value_name, index);
                        break;
                    case metadata_type.COLUMN_NAME:
                        switch_column(current_metadata[index].value_name, index);
                        break;
                }
            }
            draw_db_meta();
        }

        private void switch_column(string column_name, int index)
        {
            int counter = index;
            string parent_database = "";
            string parent_table = "";
            while (counter >= 0)
            {
                if (current_metadata[counter].metadata_type == metadata_type.DB_TABLE)
                {
                    parent_table = current_metadata[counter].value_name;
                    break;
                }
                counter--;
            }
            while (counter >= 0)
            {
                if (current_metadata[counter].metadata_type == metadata_type.DB_NAME)
                {
                    parent_database = current_metadata[counter].value_name;
                    break;
                }
                counter--;
            }
            foreach (var meta in current_metadata)
            {
                if (meta.value_name == parent_database && meta.metadata_type == metadata_type.DB_NAME)
                {
                    foreach (var table in ((DataBase)meta).tables)
                    {
                        if (table.value_name == parent_table)
                        {
                            foreach (var column in table.columns)
                            {
                                if (column.value_name == column_name)
                                {
                                    column.expanded = !column.expanded;
                                    break;
                                }    
                            }
                        }
                    }
                    break;
                }
            }
        }

        private void switch_table(string table_name, int index)
        {
            int counter = index;
            string parent_database = "";
            while (counter >= 0)
            {
                if (current_metadata[counter].metadata_type == metadata_type.DB_NAME)
                {
                    parent_database = current_metadata[counter].value_name;
                    break;
                }
                counter--;
            }
            foreach (var meta in current_metadata)
            {
                if (meta.value_name == parent_database && meta.metadata_type == metadata_type.DB_NAME)
                {
                    foreach (var table in ((DataBase) meta).tables)
                    {
                        if (table.value_name == table_name)
                        {
                            table.expanded = !table.expanded;
                            foreach (var column in table.columns)
                            {
                                column.expanded = false;
                                column.column_type.expanded = false;
                            }
                        }
                    }
                    break;
                }
            }
        }

        private void switch_database(string db_name)
        {
            foreach (var meta in current_metadata)
            {
                if (meta.value_name == db_name && meta.metadata_type == metadata_type.DB_NAME)
                {
                    meta.expanded = !meta.expanded;
                    foreach (var table in ((DataBase) meta).tables)
                    {
                        table.expanded = false;
                        foreach (var column in table.columns)
                        {
                            column.expanded = false;
                            column.column_type.expanded = false;
                        }
                    }
                    break;
                }
            }
        }

        private void update_current_meta()
        {
            List<Metadata> updated_metadata = new List<Metadata>();

            foreach (var meta in current_metadata)
            {
                if (meta.metadata_type == metadata_type.DB_NAME)
                {
                    updated_metadata.Add(meta);
                    if (meta.expanded)
                    {
                        foreach (var table in ((DataBase)meta).tables)
                        {
                            updated_metadata.Add(table);
                            if (table.expanded)
                            {
                                foreach (var column in table.columns)
                                {
                                    updated_metadata.Add(column);
                                    if (column.expanded)
                                        updated_metadata.Add(column.column_type);
                                }
                            }
                        }
                    }
                }    
            }
            current_metadata = updated_metadata;
        }

        private void draw_db_meta()
        {
            metadata_listBox.Items.Clear();
            update_current_meta();
            if (current_metadata.Count != 0)
            {
                foreach (var meta in current_metadata)
                {
                    switch (meta.metadata_type)
                    {
                        case metadata_type.DB_NAME:
                            metadata_listBox.Items.Add(meta.value_name);
                            break;
                        case metadata_type.DB_TABLE:
                            metadata_listBox.Items.Add("\t" + meta.value_name);
                            break;
                        case metadata_type.COLUMN_NAME:
                            metadata_listBox.Items.Add("\t\t" + meta.value_name);
                            break;
                        case metadata_type.COLUMN_TYPE:
                            metadata_listBox.Items.Add("\t\t\t" + meta.value_name);
                            break;
                    }
                }
            }
        }

        private void update_new_metadata(DB_Metadata db_meta)
        {
            check_column_type_integrity(db_meta);
            current_metadata.Clear();
            DataBase db = BuildDatabase(db_meta);
            fill_new_meta_list(db);
        }

        private void fill_new_meta_list(DataBase db)
        {
            current_metadata.Add(db);

            foreach (var table in db.tables)
            {
                current_metadata.Add(table);
            }
        }

        private DataBase BuildDatabase(DB_Metadata dbMeta)
        {
            DataBase db = new DataBase() { metadata_type = metadata_type.DB_NAME, expanded = true, value_name = dbMeta.DB_Name };

            foreach (var table in dbMeta.Tables)
            {
                Table tbl = new Table() { metadata_type = metadata_type.DB_TABLE, value_name = table.Table_Name, expanded = false };

                for (int i = 0; i < table.ColumnNames.Length; i++)
                {
                    Column clm = new Column() {metadata_type = metadata_type.COLUMN_NAME, expanded = false, value_name = table.ColumnNames[i] };
                    Column_Type clm_type = new Column_Type() {metadata_type = metadata_type.COLUMN_TYPE, expanded = false, value_name = table.ColumnTypes[i] };
                    clm.column_type = clm_type;
                    tbl.columns.Add(clm);
                }
                db.tables.Add(tbl);
            }
            return db;
        }

        private void check_column_type_integrity(DB_Metadata dbMeta)
        {
            foreach (var table in dbMeta.Tables)
            {
                if (table.ColumnNames.Length != table.ColumnTypes.Length)
                    throw new Exception("Hey! Column count and type count don't match in table " + table.Table_Name);
            }
        }

        private void close_connection()
        {
            if (!testing)
                client.Close();
        }

        private void open_connection()
        {
            //---create a TCPClient object at the IP and port no.---
            client = new TcpClient(ipAddress, Convert.ToInt32(portNumber));
            nwStream = client.GetStream();
        }

        private string read_server_response()
        {
            //---read back the text---
            byte[] bytesToRead = new byte[client.ReceiveBufferSize];
            int bytesRead = nwStream.Read(bytesToRead, 0, client.ReceiveBufferSize);

            string result = System.Text.Encoding.UTF8.GetString(bytesToRead);
            return result;
        }

        private void send_sql_text(string sqlString)
        {
            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(sqlString);

            //---send the text---
            nwStream.Write(bytesToSend, 0, bytesToSend.Length);
        }

        private void load_button_Click(object sender, EventArgs e)
        {
            FileDialog load_fileDialog = new OpenFileDialog();
            load_fileDialog.DefaultExt = "sql";
            load_fileDialog.Filter = "SQL files (*.sql)|*.sql|Text files (*.txt)|*.txt";
            DialogResult result = load_fileDialog.ShowDialog();
            if (load_fileDialog.FileName != "" && result == DialogResult.OK)
                sql_text.Text = File.ReadAllText(load_fileDialog.FileName);
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            FileDialog save_fileDialog = new SaveFileDialog();
            save_fileDialog.FileName = "MySQL";
            save_fileDialog.DefaultExt = "sql";
            save_fileDialog.Filter = "SQL file (*.sql)|*.sql|Text file (*.txt)|*.txt";
            DialogResult result = save_fileDialog.ShowDialog();
            if (save_fileDialog.FileName != "" && result == DialogResult.OK && sql_text.Text != "")
                File.WriteAllText(save_fileDialog.FileName, sql_text.Text);
        }

        private void tcp_listener_DoWork(object sender, DoWorkEventArgs e)
        {
            while(true)
                StartListening();
        }

        private void StartListening()
        {
            try
            {
                ThreadUpdate(read_server_response());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        delegate void ThreadUpdateDelegate(string val);

        private void ThreadUpdate(string updateVal)
        {
            if (console_log.InvokeRequired)
                console_log.Invoke(new ThreadUpdateDelegate(ThreadUpdate), updateVal);
            else
                flashy_console_receive(updateVal);

        }
    }

    abstract class Metadata
    {
        public string value_name { get; set; }
        public metadata_type metadata_type { get; set; }
        public bool expanded { get; set; }
    }

    class DataBase : Metadata
    {
        public List<Table> tables = new List<Table>();
    }

    class Table : Metadata
    {
        public List<Column> columns = new List<Column>();
    }

    class Column : Metadata
    {
        public Column_Type column_type;
    }

    class Column_Type : Metadata
    {
        
    }

    enum metadata_type
    {
        DB_NAME,
        DB_TABLE,
        COLUMN_NAME,
        COLUMN_TYPE
    }

    class DB_Metadata
    {
        public string DB_Name { get; set; }
        public DB_Table[] Tables { get; set; }
    }

    class DB_Table
    {
        public string Table_Name { get; set; }
        public string[] ColumnNames { get; set; }
        public string[] ColumnTypes { get; set; }
    }

    class server_response
    {
        public string Error { get; set; }
    }
}
