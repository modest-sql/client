using System;
using System.Drawing;
using Newtonsoft.Json;

namespace VisualSQLPro
{
    public partial class ClientForm
    {
        private void flashy_console_receive(string str, ServerRequests request)
        {
            /*var trimmed = str.Trim('\0');
            trimmed = trimmed.Trim('[');
            trimmed = trimmed.Trim(']');
            var des = JsonConvert.DeserializeObject<ServerError>(trimmed);*/
            if (request == ServerRequests.Error)
            {
                console_log.Select(console_log.TextLength, 0);
                console_log.SelectionColor = Color.Red;
                console_log.AppendText(Environment.NewLine + "received: " + str);
                console_log.SelectionColor = SystemColors.WindowText;
            }
            else
            {
                console_log.Select(console_log.TextLength, 0);
                console_log.SelectionColor = Color.Blue;
                console_log.AppendText(Environment.NewLine + "received: " + str);
                console_log.SelectionColor = SystemColors.WindowText;
            }
            console_log.ScrollToCaret();
        }

        private void console_send(string str)
        {
            console_log.Select(console_log.TextLength, 0);
            console_log.SelectionColor = SystemColors.WindowText;
            console_log.AppendText(Environment.NewLine + "sending: " + str);
            console_log.ScrollToCaret();
        }

        private void FancyConsolePrint(string printThis, Color color)
        {
            console_log.Select(console_log.TextLength, 0);
            console_log.SelectionColor = color;
            console_log.AppendText(Environment.NewLine + printThis);
            console_log.SelectionColor = SystemColors.WindowText;
            console_log.ScrollToCaret();
        }
    }

    class ServerError
    {
        public string Error { get; set; }
    }
}
