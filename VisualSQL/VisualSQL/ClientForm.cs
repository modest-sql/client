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
                string sql_string = sql_text.Text;
                if (testing)
                {
                    json = @"[{""Name"":""AAA"",""Age"":""22"",""Job"":""PPP""},{""Name"":""BBB"",""Age"":""25"",""Job"":""QQQ""},{""Name"":""CCC"",""Age"":""38"",""Job"":""RRR""}]";
                    sql_text.Text = ipAddress + ":" + portNumber;
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
}
