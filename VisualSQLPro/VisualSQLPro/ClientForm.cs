using System;
using System.Timers;
using System.Windows.Forms;

namespace VisualSQLPro
{
    public partial class ClientForm : Form
    {
        private readonly System.Timers.Timer _metadataTimer = new System.Timers.Timer();
        private readonly System.Timers.Timer _consoleTimer = new System.Timers.Timer();
        public ClientForm()
        {
            InitializeComponent();
            Text = "Modest SQL Client Pro";
            SetUpResizers();
            SetUpTimers();
        }

        private void SetUpTimers()
        {
            _metadataTimer.Interval = 50;
            _metadataTimer.Enabled = false;
            _metadataTimer.Elapsed += MetadataTimerEvent;
            _metadataTimer.AutoReset = true;

            _consoleTimer.Interval = 50;
            _consoleTimer.Enabled = false;
            _consoleTimer.Elapsed += ConsoleTimerEvent;
            _consoleTimer.AutoReset = true;
        }

        private void ConsoleTimerEvent(object sender, ElapsedEventArgs e)
        {
            UpdateConPosition();
        }

        private void MetadataTimerEvent(object sender, ElapsedEventArgs e)
        {
            UpdateMetaPosition();
        }

        delegate void UpdateMetadataPosition();
        delegate void UpdateConsolePosition();

        private void UpdateConPosition()
        {
            if (console_groupBox.InvokeRequired)
                console_groupBox.Invoke(new UpdateConsolePosition(UpdateConPosition));
            else
                console_groupBox.Height = Convert.ToInt32(Size.Height - PointToClient(Cursor.Position).Y - 30); // For some reason, resize is always 30 pixels off
        }

        private void UpdateMetaPosition()
        {
            if (metadata_group.InvokeRequired)
                metadata_group.Invoke(new UpdateMetadataPosition(UpdateMetaPosition));
            else
                metadata_group.Width = Convert.ToInt32(PointToClient(Cursor.Position).X + 2); // For some reason, resize is always 2 pixels off
        }

        private void SetUpResizers()
        {
            metadata_group.MouseDown += Metadata_group_MouseDown;
            metadata_group.MouseUp += Metadata_group_MouseUp;
            metadata_group.MouseMove += Metadata_group_MouseMove;
            console_groupBox.MouseDown += ConsoleLog_group_MouseDown;
            console_groupBox.MouseUp += ConsoleLog_group_MouseUp;
            console_groupBox.MouseMove += ConsoleLog_group_MouseMove;
        }
        
        private void Metadata_group_MouseDown(object sender, MouseEventArgs e)
        {
            _metadataTimer.Enabled = true;
        }

        private void Metadata_group_MouseUp(object sender, MouseEventArgs e)
        {
            _metadataTimer.Enabled = false;
        }

        private void Metadata_group_MouseMove(object sender, MouseEventArgs e)
        {
            if (Cursor.Current != Cursors.VSplit)
                Cursor.Current = Cursors.VSplit;
        }

        private void ConsoleLog_group_MouseDown(object sender, MouseEventArgs e)
        {
            _consoleTimer.Enabled = true;
        }

        private void ConsoleLog_group_MouseUp(object sender, MouseEventArgs e)
        {
            _consoleTimer.Enabled = false;
        }

        private void ConsoleLog_group_MouseMove(object sender, MouseEventArgs e)
        {
            if (Cursor.Current != Cursors.VSplit)
                Cursor.Current = Cursors.HSplit;
        }
    }
}
