using System.Windows.Forms;

namespace VisualSQLPro
{
    public partial class GeneralDataInputPopup : Form
    {
        public string Data => data_textBox.Text;

        public GeneralDataInputPopup()
        {
            InitializeComponent();
            MaximizeBox = false;
        }

        public void ChangeLabelText(string newLabel)
        {
            data_label.Text = newLabel;
        }
    }
}
