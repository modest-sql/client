﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
        private Color DefaultColor = Color.Black;
        private Color WaterMarkColor = Color.Gray;

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
            setTheme(Theme.DarkWoodland);
            theme_comboBox.SelectedIndex = 1;
            console_log.AppendText("Console log:");
            tcp_listener.RunWorkerAsync();
            tcp_ping.RunWorkerAsync();
            setInitialConnected();
            setupButtons();
            setupSqlTextbox();
            BuildAndSendServerRequest(1, " ");
        }

        private void SetAllButtonColors(string BackGround, string ForeGround)
        {
            SetIndividualButtonColor(refresh_button, BackGround, ForeGround);
            SetIndividualButtonColor(load_button, BackGround, ForeGround);
            SetIndividualButtonColor(run_button, BackGround, ForeGround);
            SetIndividualButtonColor(save_button, BackGround, ForeGround);
        }

        private void SetIndividualButtonColor(Button button, string BackGround, string ForeGround)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.BackColor = ColorTranslator.FromHtml(BackGround);
            button.FlatAppearance.BorderColor = ColorTranslator.FromHtml(BackGround);
            button.ForeColor = ColorTranslator.FromHtml(ForeGround);
        }

        private void StrikingAndSimple()
        {
            this.BackColor = ColorTranslator.FromHtml("#0B0C10");
            console_log.BackColor = ColorTranslator.FromHtml("#1F2833");
            table_dataGridView.BackgroundColor = ColorTranslator.FromHtml("#1F2833");
            sql_text.BackColor = ColorTranslator.FromHtml("#1F2833");
            metadata_listBox.BackColor = ColorTranslator.FromHtml("#1F2833");
            SetAllButtonColors("#66FCF1", "#0B0C10");
            SetAllUIElementColors("#C5C6C7");
            DefaultColor = ColorTranslator.FromHtml("#C5C6C7");
            WaterMarkColor = ColorTranslator.FromHtml("#45A29E");
        }

        private void SetAllUIElementColors(string ForeGround)
        {
            metadata_label.ForeColor = ColorTranslator.FromHtml(ForeGround);
            connected_label.ForeColor = ColorTranslator.FromHtml(ForeGround);
            sql_text.ForeColor = ColorTranslator.FromHtml(ForeGround);
            metadata_listBox.ForeColor = ColorTranslator.FromHtml(ForeGround);
            console_log.ForeColor = ColorTranslator.FromHtml(ForeGround);
            theme_label.ForeColor = ColorTranslator.FromHtml(ForeGround);
        }

        private void ContemporaryAndBold()
        {
            this.BackColor = ColorTranslator.FromHtml("#1A1A1D");
            console_log.BackColor = ColorTranslator.FromHtml("#6F2232");
            table_dataGridView.BackgroundColor = ColorTranslator.FromHtml("#6F2232");
            sql_text.BackColor = ColorTranslator.FromHtml("#6F2232");
            metadata_listBox.BackColor = ColorTranslator.FromHtml("#6F2232");
            SetAllButtonColors("#950740", "#1A1A1D");
            SetAllUIElementColors("#7c7c82");
            DefaultColor = ColorTranslator.FromHtml("#7c7c82");
            WaterMarkColor = ColorTranslator.FromHtml("#6D7993");
        }

        private void MutedAndMinimal()
        {
            this.BackColor = ColorTranslator.FromHtml("#D5D5D5");
            console_log.BackColor = ColorTranslator.FromHtml("#9099A2");
            table_dataGridView.BackgroundColor = ColorTranslator.FromHtml("#9099A2");
            sql_text.BackColor = ColorTranslator.FromHtml("#9099A2");
            metadata_listBox.BackColor = ColorTranslator.FromHtml("#9099A2");
            SetAllButtonColors("#6D7993", "#000000");
            SetAllUIElementColors("#544f52");
            DefaultColor = ColorTranslator.FromHtml("#544f52");
            WaterMarkColor = ColorTranslator.FromHtml("#6D7993");
        }

        private void DarkWoodland()
        {
            this.BackColor = ColorTranslator.FromHtml("#B1A296");
            console_log.BackColor = ColorTranslator.FromHtml("#54493f");
            table_dataGridView.BackgroundColor = ColorTranslator.FromHtml("#54493f");
            sql_text.BackColor = ColorTranslator.FromHtml("#5D5C61");
            metadata_listBox.BackColor = ColorTranslator.FromHtml("#5D5C61");

            refresh_button.ForeColor = SystemColors.ControlText;
            refresh_button.FlatAppearance.BorderColor = SystemColors.Control;
            refresh_button.BackColor = SystemColors.Control;
            load_button.ForeColor = SystemColors.ControlText;
            load_button.FlatAppearance.BorderColor = SystemColors.Control;
            load_button.BackColor = SystemColors.Control;
            save_button.ForeColor = SystemColors.ControlText;
            save_button.FlatAppearance.BorderColor = SystemColors.Control;
            save_button.BackColor = SystemColors.Control;
            run_button.ForeColor = SystemColors.ControlText;
            run_button.FlatAppearance.BorderColor = SystemColors.Control;
            run_button.BackColor = SystemColors.Control;

            metadata_label.ForeColor = SystemColors.ControlText;
            connected_label.ForeColor = SystemColors.ControlText;
            theme_label.ForeColor = SystemColors.ControlText;
            sql_text.ForeColor = SystemColors.ControlText;
            metadata_listBox.ForeColor = SystemColors.ControlText;
            console_log.ForeColor = SystemColors.ControlText;
            DefaultColor = Color.Black;
            WaterMarkColor = Color.Gray;
        }

        private void setTheme(Theme colorChoice)
        {
            switch (colorChoice)
            {
                case Theme.MutedAndMinimal:
                    MutedAndMinimal();
                    break;
                case Theme.DarkWoodland:
                    DarkWoodland();
                    break;
                case Theme.ContemporaryAndBold:
                    ContemporaryAndBold();
                    break;
                case Theme.StrikingAndSimple:
                    StrikingAndSimple();
                    break;
                case Theme.Creative:
                    Creative();
                    break;
                case Theme.Sleek:
                    Sleek();
                    break;
            }
            RewriteWatermark();
        }

        private void RewriteWatermark()
        {
            if (sql_text.Text == "" || sql_text.Text == "Enter query here...")
            {
                sql_text.ForeColor = WaterMarkColor;
                sql_text.Text = "Enter query here...";
                sql_text.Select(sql_text.TextLength, 0);

            }
            else
            {
                string buffer = sql_text.Text;
                sql_text.Clear();
                sql_text.ForeColor = DefaultColor;
                sql_text.Text = buffer;
            }
        }

        private void Sleek()
        {
            this.BackColor = ColorTranslator.FromHtml("#2C3531");
            console_log.BackColor = ColorTranslator.FromHtml("#116466");
            table_dataGridView.BackgroundColor = ColorTranslator.FromHtml("#116466");
            sql_text.BackColor = ColorTranslator.FromHtml("#116466");
            metadata_listBox.BackColor = ColorTranslator.FromHtml("#116466");
            SetAllButtonColors("#D9B08C", "#2C3531");
            SetAllUIElementColors("#FFCB9A");
            DefaultColor = ColorTranslator.FromHtml("#FFCB9A");
            WaterMarkColor = ColorTranslator.FromHtml("#D1E8E2");
        }

        private void Creative()
        {
            this.BackColor = ColorTranslator.FromHtml("#D79922");
            console_log.BackColor = ColorTranslator.FromHtml("#EFE2BA");
            table_dataGridView.BackgroundColor = ColorTranslator.FromHtml("#EFE2BA");
            sql_text.BackColor = ColorTranslator.FromHtml("#EFE2BA");
            metadata_listBox.BackColor = ColorTranslator.FromHtml("#EFE2BA");
            SetAllButtonColors("#F13C20", "#4056A1");
            SetAllUIElementColors("#F13C20");
            DefaultColor = ColorTranslator.FromHtml("#F13C20");
            WaterMarkColor = ColorTranslator.FromHtml("#C5CBE3");
        }

        private void setupSqlTextbox()
        {
            this.sql_text.ForeColor = WaterMarkColor;
            this.sql_text.Text = "Enter query here...";
        }

        private void setupButtons()
        {
            /*load_button.Image = Image.FromFile(GetAssetFilePath("load.png"));
            load_button.ImageAlign = ContentAlignment.MiddleRight;
            load_button.TextAlign = ContentAlignment.MiddleLeft;
            load_button.TextImageRelation = TextImageRelation.ImageBeforeText;*/
            /*load_button.BackgroundImage = Image.FromFile(GetAssetFilePath("load.png"));
            load_button.BackgroundImageLayout = ImageLayout.Stretch;
            load_button.Text = "";*/

            refresh_button.BackgroundImage = Image.FromFile(GetAssetFilePath("refresh.png"));
            refresh_button.BackgroundImageLayout = ImageLayout.Stretch;
            refresh_button.Text = "";
            
        }

        private void setInitialConnected()
        {
            connected_pictureBox.BackColor = Color.Green;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse(0, 0, connected_pictureBox.Width, connected_pictureBox.Height);
            connected_pictureBox.Region = new Region(path);
        }

        private void BuildAndSendServerRequest(int id_type, string data)
        {
            string metadata = build_server_request(id_type, data);
            send_to_server(metadata);
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
                //throw;
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
            if (dialogresult == DialogResult.Cancel)
                Environment.Exit(1);
        }

        private DataTable DerializeDataTable(string json)
        {
            var table = JsonConvert.DeserializeObject<DataTable>(json);
            return table;
        }

        private void printTable(string json)
        {
            var table = JsonConvert.DeserializeObject<DataTable>(json);
            table_dataGridView.DataSource = table;
        }

        private void printMetadata(string json)
        {
            DB_Metadata db_meta = new DB_Metadata();
            var trimmed = json;
            trimmed = trimmed.Trim('[');
            trimmed = trimmed.Trim(']');
            //db_meta = JsonConvert.DeserializeObject<DB_Metadata>(json);
            db_meta = ConvertToOldDB(JsonConvert.DeserializeObject<ResponseDB>(json));
            update_new_metadata(db_meta);
            draw_db_meta();
        }

        private DB_Metadata ConvertToOldDB(ResponseDB NewDB)
        {
            DB_Metadata db_meta = new DB_Metadata();
            db_meta.DB_Name = NewDB.DB_Name;
            List<DB_Table> table_list = new List<DB_Table>();

            foreach (var table in NewDB.Tables)
            {
                DB_Table old_table = new DB_Table();
                old_table.Table_Name = table.TableName;

                List<string> columnNames = new List<string>();
                List<string> columnTypes = new List<string>();

                foreach (var field in table.TableColumns)
                {
                    columnNames.Add(field.ColumnName);
                    if (field.ColumnType == DataType.CHAR)
                        columnTypes.Add(field.ColumnType.ToString() + "(" + field.ColumnSize + ")");
                    else
                        columnTypes.Add(field.ColumnType.ToString());
                }

                old_table.ColumnNames = columnNames.ToArray();
                old_table.ColumnTypes = columnTypes.ToArray();

                table_list.Add(old_table);
            }
            db_meta.Tables = table_list.ToArray();
            return db_meta;
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
                    json =
                        @"[{""Name"":""AAA"",""Age"":""22"",""Job"":""PPP""},{""Name"":""BBB"",""Age"":""25"",""Job"":""QQQ""},{""Name"":""CCC"",""Age"":""38"",""Job"":""RRR""}]";
                    db_data =
                        @"{""DB_Name"": ""Mocca DB"",""Tables"": [{""Table_Name"": ""Employee"", ""ColumnNames"": [""First Name"", ""Second Name"", ""First Last Name"", ""Second Last Name"", ""Age"", ""Married""],""ColumnTypes"": [""char[100]"", ""char[100]"", ""char[100]"", ""char[100]"", ""int"", ""boolean""]},{""Table_Name"": ""Department"", ""ColumnNames"": [""Department Name"", ""Description"", ""Address""],""ColumnTypes"": [""char[100]"", ""char[100]"", ""char[100]""]}]}";
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
            console_log.SelectionColor = DefaultColor;
        }

        private void flashy_console_receive(string str)
        {
            var trimmed = str.Trim('\0');
            trimmed = trimmed.Trim('[');
            trimmed = trimmed.Trim(']');
            var des = JsonConvert.DeserializeObject<server_error>(trimmed);
            //sql_text.Text = des.Error;
            if (des.Error != "no error")
            {
                console_log.Select(console_log.TextLength, 0);
                console_log.SelectionColor = Color.Red;
                console_log.AppendText(Environment.NewLine + "received: " + des.Error);
                console_log.SelectionColor = DefaultColor;
            }
            else
            {
                console_log.Select(console_log.TextLength, 0);
                console_log.SelectionColor = Color.Blue;
                console_log.AppendText(Environment.NewLine + "received: success, no error");
                console_log.SelectionColor = DefaultColor;
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
            console_log.SelectionColor = DefaultColor;
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
                    foreach (var table in ((DataBase) meta).tables)
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
                        foreach (var table in ((DataBase) meta).tables)
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
            DataBase db = new DataBase()
            {
                metadata_type = metadata_type.DB_NAME,
                expanded = true,
                value_name = dbMeta.DB_Name
            };

            foreach (var table in dbMeta.Tables)
            {
                Table tbl = new Table()
                {
                    metadata_type = metadata_type.DB_TABLE,
                    value_name = table.Table_Name,
                    expanded = false
                };

                for (int i = 0; i < table.ColumnNames.Length; i++)
                {
                    Column clm = new Column()
                    {
                        metadata_type = metadata_type.COLUMN_NAME,
                        expanded = false,
                        value_name = table.ColumnNames[i]
                    };
                    Column_Type clm_type = new Column_Type()
                    {
                        metadata_type = metadata_type.COLUMN_TYPE,
                        expanded = false,
                        value_name = table.ColumnTypes[i]
                    };
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
            /*string sendThis = build_server_request(2, sqlString);
            send_to_server(sendThis);*/
            BuildAndSendServerRequest(2, sqlString);
        }

        private void send_to_server(string send_this)
        {
            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(send_this);

            //---send the text---
            try
            {
                nwStream.Write(bytesToSend, 0, bytesToSend.Length);
            }
            catch (Exception)
            {
                ConnectedUpdate(false);
                //throw;
            }
        }

        private string build_server_request(int id_type, string data)
        {
            string returnString = "{\"Type\":" + id_type + ", \"Data\":\"" + data + "\"}";
            return returnString;
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
                server_response sr = JsonConvert.DeserializeObject<server_response>(read_server_response());
                ConnectedUpdate(true);
                switch (sr.Type)
                {
                    case 0:
                        //Ping
                        break;
                    case 1:
                        //Metadata
                        MetadataUpdate(sr.Data);
                        break;
                    case 2:
                        //Tabla
                        TableUpdate(sr.Data);
                        break;
                    case 4:
                        //Error
                        ConsoleUpdate(sr.Data);
                        break;
                    default:
                        //Do nothing
                        break;
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
            }
        }

        void isConnected(bool conn)
        {
            if (conn)
            {
                connected_pictureBox.BackColor = Color.Green;
                connected_label.Text = "Connected";
            }
            else
            {
                connected_pictureBox.BackColor = Color.Red;
                connected_label.Text = "Disconnected, attempting to reconnect";
            }
        }

        delegate void ConsoleUpdateDelegate(string val);

        delegate void TableUpdatedelegate(string val);

        delegate void MetadataUpdatedelegate(string val);

        delegate void ChangeConnectedColor(bool val);

        delegate void CycleDisconnectedColor();

        private void CycleUpdate()
        {
            if (connected_label.InvokeRequired)
                connected_label.Invoke(new CycleDisconnectedColor(CycleUpdate));
            else
                disconnected_cycle();
        }

        private void ConnectedUpdate(Boolean updateVal)
        {
            if (connected_pictureBox.InvokeRequired)
                connected_pictureBox.Invoke(new ChangeConnectedColor(ConnectedUpdate), updateVal);
            else
                isConnected(updateVal);
        }

        private void MetadataUpdate(string updateVal)
        {
            if (metadata_listBox.InvokeRequired)
                metadata_listBox.Invoke(new MetadataUpdatedelegate(MetadataUpdate), updateVal);
            else
                printMetadata(updateVal);
        }

        private void TableUpdate(string updateVal)
        {
            if (table_dataGridView.InvokeRequired)
                table_dataGridView.Invoke(new TableUpdatedelegate(TableUpdate), updateVal);
            else
                printTable(updateVal);
        }

        private void ConsoleUpdate(string updateVal)
        {
            if (console_log.InvokeRequired)
                console_log.Invoke(new ConsoleUpdateDelegate(ConsoleUpdate), updateVal);
            else
                flashy_console_receive(updateVal);

        }

        private void tcp_ping_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                Thread.Sleep(5000);
                PingServer();
                if (connected_pictureBox.BackColor == Color.Red)
                {
                    while (!connection_success())
                    {
                        CycleUpdate();
                        Thread.Sleep(300);
                    }
                    ConnectedUpdate(true);
                }
            }
        }

        private void disconnected_cycle()
        {
            if (connected_label.Text == "Disconnected, attempting to reconnect")
                connected_label.Text = "Disconnected, attempting to reconnect.";
            else if (connected_label.Text == "Disconnected, attempting to reconnect.")
                connected_label.Text = "Disconnected, attempting to reconnect..";
            else if (connected_label.Text == "Disconnected, attempting to reconnect..")
                connected_label.Text = "Disconnected, attempting to reconnect...";
            else if (connected_label.Text == "Disconnected, attempting to reconnect...")
                connected_label.Text = "Disconnected, attempting to reconnect";
        }

        private void PingServer()
        {
            try
            {
                /*string ping = build_server_request(0, " ");
                send_to_server(ping);*/
                BuildAndSendServerRequest(0, " ");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private static string GetAssetFilePath(string fileName)
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

        private void refresh_button_Click(object sender, EventArgs e)
        {
            BuildAndSendServerRequest(1, " ");
        }

        private void sql_text_Enter(object sender, EventArgs e)
        {
            if (sql_text.Text == "Enter query here...")
            {
                sql_text.Clear();
                sql_text.ForeColor = DefaultColor;
            }
        }

        private void sql_text_Leave(object sender, EventArgs e)
        {
            if (sql_text.Text == "")
            {
                sql_text.ForeColor = WaterMarkColor;
                sql_text.Text = "Enter query here...";
                sql_text.Select(sql_text.TextLength, 0);

            }
        }

        private void theme_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = theme_comboBox.SelectedIndex;
            switch (selectedIndex)
            {
                case 0:
                    MutedAndMinimal();
                    break;
                case 1:
                    DarkWoodland();
                    break;
                case 2:
                    ContemporaryAndBold();
                    break;
                case 3:
                    StrikingAndSimple();
                    break;
                case 4:
                    Creative();
                    break;
                case 5:
                    Sleek();
                    break;
            }
            RewriteWatermark();
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

    enum Theme
    {
        MutedAndMinimal,
        DarkWoodland,
        ContemporaryAndBold,
        StrikingAndSimple,
        Creative,
        Sleek
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
        public int Type { get; set; }
        public string Data { get; set; }
    }

    class server_error
    {
        public string Error { get; set; }
    }

    class ResponseDB
    {
        public string DB_Name { get; set; }
        public ResponseTable[] Tables { get; set; }
    }

    class ResponseTable
    {
        public string TableName { get; set; }
        public ResponseColumn[] TableColumns { get; set; }
    }

    class ResponseColumn
    {
        public string ColumnName { get; set; }
        public DataType ColumnType { get; set; }
        public UInt16 ColumnSize { get; set; }
    }

    enum DataType
    {
        INTEGER,
	    FLOAT,
        BOOLEAN,
	    CHAR,
        DATETIME
    }
}
