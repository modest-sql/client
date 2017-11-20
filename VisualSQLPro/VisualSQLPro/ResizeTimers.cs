using System.Timers;

namespace VisualSQLPro
{
    public partial class ClientForm
    {
        private readonly Timer _metadataTimer = new Timer();
        private readonly Timer _consoleTimer = new Timer();
        private readonly Timer _taskManagerTimer = new Timer();

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

            _taskManagerTimer.Interval = 50;
            _taskManagerTimer.Enabled = false;
            _taskManagerTimer.Elapsed += TaskManagerTimerEvent;
            _taskManagerTimer.AutoReset = true;
        }

        private void TaskManagerTimerEvent(object sender, ElapsedEventArgs e)
        {
            UpdateTaskPosition();
        }

        private void ConsoleTimerEvent(object sender, ElapsedEventArgs e)
        {
            UpdateConPosition();
        }

        private void MetadataTimerEvent(object sender, ElapsedEventArgs e)
        {
            UpdateMetaPosition();
        }
    }
}
