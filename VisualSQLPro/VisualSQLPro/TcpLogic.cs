using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Newtonsoft.Json;

namespace VisualSQLPro
{
    public partial class ClientForm
    {
        private string _ipAddress = "";
        private string _portNumber = "";
        private TcpClient _client;
        private NetworkStream _nwStream;

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

        private void open_connection()
        {
            _client = new TcpClient(_ipAddress, Convert.ToInt32(_portNumber));
            _nwStream = _client.GetStream();
        }

        private string read_server_response()
        {
            byte[] bytesToRead = new byte[_client.ReceiveBufferSize];
            _nwStream.Read(bytesToRead, 0, _client.ReceiveBufferSize);

            string result = Encoding.UTF8.GetString(bytesToRead);
            return result;
        }

        private void send_sql_text(string sqlString)
        {
            BuildAndSendServerRequest((int) ServerRequests.Query, sqlString);
        }

        private void send_to_server(string sendThis)
        {
            byte[] bytesToSend = Encoding.ASCII.GetBytes(sendThis);

            try
            {
                _nwStream.Write(bytesToSend, 0, bytesToSend.Length);
            }
            catch (Exception)
            {
                ConnectedUpdate(false);
                //throw;
            }
        }

        private string build_server_request(int idType, string data)
        {
            string returnString = "{\"Type\":" + idType + ", \"Data\":\"" + data + "\"}";
            return returnString;
        }

        private void BuildAndSendServerRequest(int idType, string data)
        {
            string metadata = build_server_request(idType, data);
            send_to_server(metadata);
        }

        private void tcp_listener_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (true)
                StartListening();
        }

        private void StartListening()
        {
            try
            {
                //Console.WriteLine(@"Escuchamos wuu");
                ServerResponse sr = JsonConvert.DeserializeObject<ServerResponse>(read_server_response());
                ConnectedUpdate(true);
                switch (sr.Type)
                {
                    case (int) ServerRequests.KeepAlive:
                        //Ping
                        break;
                    case (int)ServerRequests.GetMetadata:
                        //Metadata
                        MetadataUpdate(sr.Data);
                        break;
                    case (int)ServerRequests.Query:
                        //Tabla
                        TableUpdate(sr.Data);
                        break;
                    case (int)ServerRequests.Error:
                        //Error
                        ConsoleUpdate(sr.Data);
                        break;
                    case (int)ServerRequests.ShowTransaction:
                        //Error
                        TaskManagerUpdate(sr.Data);
                        break;
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
            }
        }

        void IsConnected(bool conn)
        {
            /*if (conn)
            {
                connected_pictureBox.BackColor = Color.Green;
                connected_label.Text = "Connected";
            }
            else
            {
                connected_pictureBox.BackColor = Color.Red;
                connected_label.Text = "Disconnected, attempting to reconnect";
            }*/

            Text = conn ? @"Modest SQL Client Pro (Connected)" : @"Modest SQL Client Pro (Disconnected)";
        }

        private void tcp_ping_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (true)
            {
                Thread.Sleep(5000);
                PingServer();
                if (/*connected_pictureBox.BackColor == Color.Red*/Text == @"Modest SQL Client Pro (Disconnected)")
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

        private void PingServer()
        {
            try
            {
                BuildAndSendServerRequest((int) ServerRequests.KeepAlive, " ");
                //Console.WriteLine(@"Pingeamos wuu");
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
            }
        }

        private void disconnected_cycle()
        {
            /*if (connected_label.Text == "Disconnected, attempting to reconnect")
                connected_label.Text = "Disconnected, attempting to reconnect.";
            else if (connected_label.Text == "Disconnected, attempting to reconnect.")
                connected_label.Text = "Disconnected, attempting to reconnect..";
            else if (connected_label.Text == "Disconnected, attempting to reconnect..")
                connected_label.Text = "Disconnected, attempting to reconnect...";
            else if (connected_label.Text == "Disconnected, attempting to reconnect...")
                connected_label.Text = "Disconnected, attempting to reconnect";*/
        }
    }

    class ServerResponse
    {
        public int Type { get; set; }
        public string Data { get; set; }
    }
}
