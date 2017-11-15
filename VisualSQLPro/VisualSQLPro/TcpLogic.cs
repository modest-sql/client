using System;
using System.Net.Sockets;
using System.Text;

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
    }
}
