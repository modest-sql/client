﻿using System;
using System.Net.Sockets;
using System.Text;
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
            BuildAndSendServerRequest(2, sqlString);
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
                //ConnectedUpdate(false);
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
                ServerResponse sr = JsonConvert.DeserializeObject<ServerResponse>(read_server_response());
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
                        //TableUpdate(sr.Data);
                        break;
                    case 4:
                        //Error
                        //ConsoleUpdate(sr.Data);
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
    }

    class ServerResponse
    {
        public int Type { get; set; }
        public string Data { get; set; }
    }
}