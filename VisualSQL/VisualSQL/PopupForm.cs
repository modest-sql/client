using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace VisualSQL
{
    public partial class PopupForm: Form
    {
        public string ipAddress { get { return ip_textBox.Text; } }
        public string portNumber { get { return port_textBox.Text; } }
        public PopupForm()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            DarkWoodland();
        }

        private void DarkWoodland()
        {
            this.BackColor = ColorTranslator.FromHtml("#B1A296");
            ip_textBox.BackColor = ColorTranslator.FromHtml("#5D5C61");
            port_textBox.BackColor = ColorTranslator.FromHtml("#5D5C61");
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
