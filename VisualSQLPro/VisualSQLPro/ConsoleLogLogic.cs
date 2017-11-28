using System;
using System.Drawing;
using Newtonsoft.Json;

namespace VisualSQLPro
{
    public partial class ClientForm
    {
        private void flashy_console_receive(string str, ServerRequests request)
        {
            console_log.Select(console_log.TextLength, 0);
            console_log.SelectionColor = request == ServerRequests.Error ? _activeTheme.FailColor : _activeTheme.SuccessColor;
            console_log.AppendText(Environment.NewLine + "received: " + str);
            console_log.SelectionColor = _activeTheme.DefaultTextColor;
            console_log.ScrollToCaret();
        }

        private void console_send(string str)
        {
            console_log.Select(console_log.TextLength, 0);
            console_log.SelectionColor = _activeTheme.DefaultTextColor;
            console_log.AppendText(Environment.NewLine + "sending: " + str);
            console_log.ScrollToCaret();
        }

        private void FancyConsolePrint(string printThis, Color color)
        {
            console_log.Select(console_log.TextLength, 0);
            console_log.SelectionColor = color;
            console_log.AppendText(Environment.NewLine + printThis);
            console_log.SelectionColor = _activeTheme.DefaultTextColor;
            console_log.ScrollToCaret();
        }
    }

    class ServerError
    {
        public string Error { get; set; }
    }
}
