using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace VisualSQLPro
{
    public partial class PopupForm: Form
    {
        public string ipAddress { get { return ip_textBox.Text; } }
        public string portNumber { get { return port_textBox.Text; } }
        public PopupForm()
        {
            InitializeComponent();
            this.MaximizeBox = false;
        }

        private void LocalhostClick(object sender, EventArgs e)
        {
            ip_textBox.Text = GetLocalIPAddress();
            port_textBox.Text = "3333";
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return "127.0.0.1";
        }
    }
}
