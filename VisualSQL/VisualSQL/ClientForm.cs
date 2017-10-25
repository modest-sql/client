using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        private bool testing = true;
        private TcpClient client;
        private NetworkStream nwStream;
        private DB_Meta_Manager current_metadata = new DB_Meta_Manager();

        public ClientForm()
        {
            InitializeComponent();
            this.Text = "Modest SQL Client";
            PopUp_Connection();
            this.listBox1.MouseDoubleClick += new MouseEventHandler(listBox1_MouseDoubleClick);
        }

        private void PopUp_Connection()
        {
            PopupForm popup = new PopupForm();
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
                if (testing)
                {
                    json = @"[{""Name"":""AAA"",""Age"":""22"",""Job"":""PPP""},{""Name"":""BBB"",""Age"":""25"",""Job"":""QQQ""},{""Name"":""CCC"",""Age"":""38"",""Job"":""RRR""}]";
                    db_data = @"{""DB_Name"": ""Mocca DB"",""Tables"": [{""Table_Name"": ""Employee"", ""ColumnNames"": [""First Name"", ""Second Name"", ""First Last Name"", ""Second Last Name"", ""Age"", ""Married""],""ColumnTypes"": [""char[100]"", ""char[100]"", ""char[100]"", ""char[100]"", ""int"", ""boolean""]},{""Table_Name"": ""Department"", ""ColumnNames"": [""Department Name"", ""Description"", ""Address""],""ColumnTypes"": [""char[100]"", ""char[100]"", ""char[100]""]}]}";
                    sql_text.Text = ipAddress + ":" + portNumber;

                    DB_Metadata db_meta = new DB_Metadata();
                    db_meta = JsonConvert.DeserializeObject<DB_Metadata>(db_data);
                    update_new_metadata(db_meta);
                    draw_db_meta();
                    //fill_db_meta(db_meta);
                }
                else
                {
                    open_connection();
                    send_sql_text(sql_string);
                    json = read_server_response();
                    close_connection();
                }
                var table = JsonConvert.DeserializeObject<DataTable>(json);
                dataGridView1.DataSource = table;
            }
        }

        void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = listBox1.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                MessageBox.Show(index.ToString());
            }
        }

        private void draw_db_meta()
        {
            if (current_metadata.meta_dictionary.Count != 0)
            {
                foreach(var elem in current_metadata.meta_dictionary)
                {
                    if (elem.Value.showing)
                    {
                        switch (elem.Value.type)
                        {
                            case metadata_type.DB_NAME:
                                listBox1.Items.Add(elem.Value.value);
                                break;
                            case metadata_type.DB_TABLE:
                                listBox1.Items.Add("\t" + elem.Value.value);
                                break;
                            case metadata_type.COLUMN_NAME:
                                listBox1.Items.Add("\t\t" + elem.Value.value);
                                break;
                            case metadata_type.COLUMN_TYPE:
                                listBox1.Items.Add("\t\t\t" + elem.Value.value);
                                break;
                        }
                    }
                }
            }
        }

        private void update_new_metadata(DB_Metadata db_meta)
        {
            int counter = 0;
            check_column_type_integrity(db_meta);
            current_metadata.metadata = db_meta;
            current_metadata.meta_dictionary.Clear();
            current_metadata.meta_dictionary.Add(counter, new DB_Value_Struct(db_meta.DB_Name, metadata_type.DB_NAME, true));
            counter++;
            foreach (var table in db_meta.Tables)
            {
                current_metadata.meta_dictionary.Add(counter, new DB_Value_Struct(table.Table_Name, metadata_type.DB_TABLE, true));
                counter++;

                for (int i = 0; i < table.ColumnNames.Length; i++)
                {
                    current_metadata.meta_dictionary.Add(counter, new DB_Value_Struct(table.ColumnNames[i] + " (" + table.ColumnTypes[i] + ")", metadata_type.COLUMN_NAME, false));
                    counter++;
                }
            }
        }

        private void fill_db_meta(DB_Metadata dbMeta)
        {
            check_column_type_integrity(dbMeta);

            listBox1.Items.Add(dbMeta.DB_Name);
            foreach (var table in dbMeta.Tables)
            {
                listBox1.Items.Add("\t" + table.Table_Name);
                for (int i = 0; i < table.ColumnNames.Length; i++)
                {
                    listBox1.Items.Add("\t\t" + table.ColumnNames[i] + " (" + table.ColumnTypes[i] + ")");
                }
            }
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
    }

    enum metadata_type
    {
        DB_NAME,
        DB_TABLE,
        COLUMN_NAME,
        COLUMN_TYPE
    }

    class DB_Meta_Manager
    {
        public DB_Metadata metadata { get; set; }
        public Dictionary<int, DB_Value_Struct> meta_dictionary { get; set; }

        public DB_Meta_Manager()
        {
            metadata = new DB_Metadata();
            meta_dictionary = new Dictionary<int, DB_Value_Struct>();
        }
    }

    struct DB_Value_Struct
    {
        public string value;
        public metadata_type type;
        public bool showing;

        public DB_Value_Struct(string value, metadata_type m_type, bool show) : this()
        {
            this.value = value;
            this.type = m_type;
            this.showing = show;
        }
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
}
