using System;
using System.Collections.Generic;
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
        private int _chunkSize = 256;
        private bool _lockedTcpConnection;

        private bool connection_success()
        {
            try
            {
                open_connection();
            }
            catch (Exception)
            {
                _lockedTcpConnection = true;
                return false;
                //throw;
            }
            _lockedTcpConnection = false;
            return true;
        }

        private void open_connection()
        {
            _client = new TcpClient(_ipAddress, Convert.ToInt32(_portNumber));
            _nwStream = _client.GetStream();
        }

        private void send_sql_text(string sqlString)
        {
            /*var trimmed = sqlString.Trim('\n');
            trimmed = trimmed.Trim('\r');
            trimmed = trimmed.Replace("\r\n", string.Empty);*/
            var trimmed = sqlString.Replace("\n", "\\n");
            trimmed = trimmed.Replace("\r", "\\r");
            BuildAndSendServerRequest((int) ServerRequests.Query, trimmed);
        }

        private string build_server_request(int idType, string data)
        {
            string returnString = "{\"Type\":" + idType + ", \"Data\":\"" + data + "\"}";
            return returnString;
        }

        private void BuildAndSendServerRequest(int idType, string data)
        {
            string dataToSend = build_server_request(idType, data);
            //send_to_server(dataToSend);
            send_to_server_new(dataToSend);
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
                //ServerResponse sr = JsonConvert.DeserializeObject<ServerResponse>(read_server_response());
                string serverResponseString = read_server_response_new();
                ServerResponse sr = JsonConvert.DeserializeObject<ServerResponse>(serverResponseString);
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
                        ConsoleUpdate(sr.Data, ServerRequests.Error);
                        break;
                    case (int)ServerRequests.ShowTransaction:
                        //Error
                        TaskManagerUpdate(sr.Data);
                        break;
                    case (int)ServerRequests.Notification:
                        //Notification
                        ConsoleUpdate(sr.Data, ServerRequests.Notification);
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
                Thread.Sleep(2000);
                PingServer();
                if (/*connected_pictureBox.BackColor == Color.Red*/Text == @"Modest SQL Client Pro (Disconnected)")
                {
                    while (!connection_success())
                    {
                        CycleUpdate();
                        Thread.Sleep(300);
                    }
                    if (_activeDb != "")
                        SetActiveDb(_activeDb);
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

        private void send_to_server_new(string sendThis)
        {
            while (_lockedTcpConnection)
            {
                //Lock until tcp connection can be used
            }
            _lockedTcpConnection = true;
            byte[] bytesToSend = Encoding.ASCII.GetBytes(sendThis);

            byte[] sendLength = BitConverter.GetBytes((uint)bytesToSend.Length);
            Array.Reverse(sendLength);
            byte[] reversedLength = sendLength;

            try
            {
                //Console.WriteLine(@"Prefix to send: " + bytesToSend.Length);
                _nwStream.Write(reversedLength, 0, reversedLength.Length);

                byte[][] chunks = SplitIntoChunks(bytesToSend, _chunkSize);
                int counter = 0;
                foreach (var chunk in chunks)
                {
                    //Console.WriteLine(@"Current index counter: " + counter);
                    _nwStream.Write(chunk, 0, chunk.Length);
                    counter++;
                }
            }
            catch (Exception)
            {
                ConnectedUpdate(false);
                //throw;
            }
            _lockedTcpConnection = false;
        }

        private byte[][] SplitIntoChunks(byte[] bytesToSend, int chunkSize)
        {
            List<byte[]> returnChunks = new List<byte[]>();
            int bytesLength = bytesToSend.Length;
            int i = 0;
            while (bytesLength >= chunkSize)
            {
                byte[] currentChunk = new byte[chunkSize];
                Array.Copy(bytesToSend, (i * chunkSize), currentChunk, 0, chunkSize);
                returnChunks.Add(currentChunk);

                i++;
                bytesLength -= chunkSize;
            }
            if (bytesLength > 0)
            {
                byte[] currentChunk = new byte[bytesLength];
                Array.Copy(bytesToSend, (i * chunkSize), currentChunk, 0, bytesLength);
                returnChunks.Add(currentChunk);
            }

            return returnChunks.ToArray();
        }

        private string read_server_response_new()
        {
            byte[] bytesToReadLength = new byte[4];
            _nwStream.Read(bytesToReadLength, 0, 4);
            Array.Reverse(bytesToReadLength);
            byte[] reversedLength = bytesToReadLength;
            int readLength = BitConverter.ToInt32(reversedLength, 0);

            //Console.WriteLine(@"Read length: " + readLength);

            byte[] completeMessage = new byte[readLength];

            int chunkAmount;
            if (readLength % _chunkSize == 0)
                chunkAmount = readLength / _chunkSize;
            else
                chunkAmount = readLength / _chunkSize + 1;
            int counter = 0;
            for (int i = 0; i < (chunkAmount - 1); i++)
            {
                byte[] chunkToRead = new byte[_chunkSize];
                _nwStream.Read(chunkToRead, 0, _chunkSize);
                Array.Copy(chunkToRead, 0, completeMessage, (i * _chunkSize), _chunkSize);
                //Console.WriteLine(@"Current index i: " + i + @", current chunk bytes read: " + Encoding.UTF8.GetString(chunkToRead));
                counter++;
                Thread.Sleep(5);
            }
            byte[] lastChunk = new byte[GetLastChunkSize(readLength, _chunkSize)];
            _nwStream.Read(lastChunk, 0, GetLastChunkSize(readLength, _chunkSize));
            Array.Copy(lastChunk, 0, completeMessage, ((chunkAmount - 1) * _chunkSize), GetLastChunkSize(readLength, _chunkSize));
            //Console.WriteLine(@"Current index i: " + counter + @", current chunk bytes read: " + Encoding.UTF8.GetString(lastChunk) + @", last chunk size: " + GetLastChunkSize(readLength, _chunkSize));

            //trim here maybe
            Array.Resize(ref completeMessage, readLength);

            string result = Encoding.UTF8.GetString(completeMessage);
            //Console.WriteLine(result);
            return result;
        }

        private int GetLastChunkSize(int totalLength, int chunkSize)
        {
            int returnValue = totalLength;
            while (returnValue > chunkSize)
                returnValue -= chunkSize;
            return returnValue;
        }
    }

    class ServerResponse
    {
        public int Type { get; set; }
        public string Data { get; set; }
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
        Error = 208,
        Notification = 209,
        SessionExited = 210,
        DropDb = 211
    }
}
