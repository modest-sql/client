using System.Windows.Forms;

namespace VisualSQLPro
{
    public partial class ClientForm
    {
        private void SetUpResizers()
        {
            metadata_group.MouseDown += Metadata_group_MouseDown;
            metadata_group.MouseUp += Metadata_group_MouseUp;
            metadata_group.MouseMove += Metadata_group_MouseMove;

            console_groupBox.MouseDown += ConsoleLog_group_MouseDown;
            console_groupBox.MouseUp += ConsoleLog_group_MouseUp;
            console_groupBox.MouseMove += ConsoleLog_group_MouseMove;

            task_manager_groupBox.MouseDown += Task_manager_groupBox_MouseDown;
            task_manager_groupBox.MouseUp += Task_manager_groupBox_MouseUp;
            task_manager_groupBox.MouseMove += Task_manager_groupBox_MouseMove;
        }

        private void Task_manager_groupBox_MouseDown(object sender, MouseEventArgs e)
        {
            _taskManagerTimer.Enabled = true;
        }

        private void Task_manager_groupBox_MouseUp(object sender, MouseEventArgs e)
        {
            _taskManagerTimer.Enabled = false;
        }

        private void Task_manager_groupBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (Cursor.Current != Cursors.VSplit)
                Cursor.Current = Cursors.VSplit;
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
            if (Cursor.Current != Cursors.HSplit)
                Cursor.Current = Cursors.HSplit;
        }
    }
}
