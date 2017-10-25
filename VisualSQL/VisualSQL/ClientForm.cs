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
        private bool testing = false;
        private TcpClient client;
        private NetworkStream nwStream;

        public ClientForm()
        {
            InitializeComponent();
            this.Text = "Modest SQL Client";
            PopUp_Connection();
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
                    db_data = @"{""Name"": ""Mocca DB"",""ColumnNames"": [""First Name"", ""Second Name"", ""First Last Name"", ""Second Last Name"", ""Age"", ""Married""],""ColumnTypes"": [""char[100]"", ""char[100]"", ""char[100]"", ""char[100]"", ""int"", ""boolean""]}";
                    sql_text.Text = ipAddress + ":" + portNumber;

                    DB_Metadata db_meta = new DB_Metadata();
                    db_meta = JsonConvert.DeserializeObject<DB_Metadata>(db_data);
                    fill_db_meta(db_meta);
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

        private void fill_db_meta(DB_Metadata dbMeta)
        {
            check_column_type_integrity(dbMeta);

            /*ListViewItem listitem = new ListViewItem(dbMeta.Name);
            for (int i = 0; i < dbMeta.ColumnNames.Length; i++)
            {
                string NewSubItem = dbMeta.ColumnNames[i] + "(" + dbMeta.ColumnTypes[i] + ")";
                listitem.SubItems.Add(NewSubItem);
            }
            listBox1.Items.Add(listitem);*/

            /*ListViewItem listitemName = new ListViewItem(dbMeta.Name);
            listBox1.Items.Add(listitemName);
            for (int i = 0; i < dbMeta.ColumnNames.Length; i++)
            {
                ListViewItem listitem = new ListViewItem(dbMeta.ColumnNames[i] + "(" + dbMeta.ColumnTypes[i] + ")");
                listBox1.Items.Add(listitem);
            }*/

            listBox1.Items.Add(dbMeta.Name);
            for (int i = 0; i < dbMeta.ColumnNames.Length; i++)
            {
                listBox1.Items.Add(dbMeta.ColumnNames[i] + "(" + dbMeta.ColumnTypes[i] + ")");
            }
        }

        private void check_column_type_integrity(DB_Metadata dbMeta)
        {
            if (dbMeta.ColumnNames.Length != dbMeta.ColumnTypes.Length)
                throw new Exception("Hey! Column count and type count don't match!");
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

    class DB_Metadata
    {
        public string Name { get; set; }
        public string[] ColumnNames { get; set; }
        public string[] ColumnTypes { get; set; }
    }
}
