using System;
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
        }

        private void LocalhostClick(object sender, EventArgs e)
        {
            ip_textBox.Text = "127.0.0.1";
            port_textBox.Text = "3333";
        }
    }
}
