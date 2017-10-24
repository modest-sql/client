using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualSQL
{
    public partial class PopupForm: Form
    {
        public string ipAddress { get { return textBox1.Text; } }
        public string portNumber { get { return textBox2.Text; } }
        public PopupForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "127.0.0.1";
            textBox2.Text = "3333";
        }
    }
}
