using System;
using System.Windows.Forms;

namespace VisualSQLPro
{
    public partial class ClientForm : Form
    {
        public ClientForm()
        {
            InitializeComponent();
            Text = "Modest SQL Client Pro";
            SetUpResizers();
        }

        private void SetUpResizers()
        {
            metadata_group.MouseUp += Metadata_group_MouseUp;
            metadata_group.MouseMove += Metadata_group_MouseMove;
            console_groupBox.MouseUp += ConsoleLog_group_MouseUp;
            console_groupBox.MouseMove += ConsoleLog_group_MouseMove;
        }

        private void Metadata_group_MouseUp(object sender, MouseEventArgs e)
        {
            metadata_group.Width = Convert.ToInt32(e.X + 2); // For some reason, resize is always 2 pixels off
        }

        private void Metadata_group_MouseMove(object sender, MouseEventArgs e)
        {
            if (Cursor.Current != Cursors.VSplit)
                Cursor.Current = Cursors.VSplit;
        }

        private void ConsoleLog_group_MouseUp(object sender, MouseEventArgs e)
        {
            console_groupBox.Height = Convert.ToInt32(Size.Height - PointToClient(Cursor.Position).Y - 30); // For some reason, resize is always 30 pixels off
        }

        private void ConsoleLog_group_MouseMove(object sender, MouseEventArgs e)
        {
            if (Cursor.Current != Cursors.VSplit)
                Cursor.Current = Cursors.HSplit;
        }
    }
}
