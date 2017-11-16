using System;

namespace VisualSQLPro
{
    public partial class ClientForm
    {
        delegate void UpdateMetadataPosition();
        delegate void UpdateConsolePosition();
        delegate void UpdateTaskManagerPosition();
        delegate void ChangeConnectedColor(bool val);
        delegate void MetadataUpdatedelegate(string val);
        delegate void CycleDisconnectedColor();
        delegate void ConsoleUpdateDelegate(string val);
        delegate void TableUpdatedelegate(string val);
        delegate void TaskManagerUpdatedelegate(string val);

        private void UpdateTaskPosition()
        {
            if (task_manager_groupBox.InvokeRequired)
                task_manager_groupBox.Invoke(new UpdateTaskManagerPosition(UpdateTaskPosition));
            else
                AdjustTaskManagerPosition();
        }

        private void UpdateConPosition()
        {
            if (console_groupBox.InvokeRequired)
                console_groupBox.Invoke(new UpdateConsolePosition(UpdateConPosition));
            else
                AdjustConsoleLogPosition();
        }

        private void UpdateMetaPosition()
        {
            if (metadata_group.InvokeRequired)
                metadata_group.Invoke(new UpdateMetadataPosition(UpdateMetaPosition));
            else
                AdjustMetadataPosition();
        }

        private void ConnectedUpdate(Boolean updateVal)
        {
            /*if (connected_pictureBox.InvokeRequired)
                connected_pictureBox.Invoke(new ChangeConnectedColor(ConnectedUpdate), updateVal);
            else
                isConnected(updateVal);*/

            if (InvokeRequired)
                Invoke(new ChangeConnectedColor(ConnectedUpdate), updateVal);
            else
                IsConnected(updateVal);
        }

        private void MetadataUpdate(string updateVal)
        {
            if (metadata_listBox.InvokeRequired)
                metadata_listBox.Invoke(new MetadataUpdatedelegate(MetadataUpdate), updateVal);
            else
                PrintMetadata(updateVal);
        }

        private void CycleUpdate()
        {
            /*if (connected_label.InvokeRequired)
                connected_label.Invoke(new CycleDisconnectedColor(CycleUpdate));
            else
                disconnected_cycle();*/

            if (InvokeRequired)
                Invoke(new CycleDisconnectedColor(CycleUpdate));
            else
                disconnected_cycle();
        }

        private void ConsoleUpdate(string updateVal)
        {
            if (console_log.InvokeRequired)
                console_log.Invoke(new ConsoleUpdateDelegate(ConsoleUpdate), updateVal);
            else
                flashy_console_receive(updateVal);
        }

        private void TableUpdate(string updateVal)
        {
            if (queries_tabControl.InvokeRequired)
                queries_tabControl.Invoke(new TableUpdatedelegate(TableUpdate), updateVal);
            else
                PrintTable(updateVal);
        }

        private void TaskManagerUpdate(string updateVal)
        {
            if (task_manager_listBox.InvokeRequired)
                task_manager_listBox.Invoke(new TaskManagerUpdatedelegate(TaskManagerUpdate), updateVal);
            else
                UpdateTaskManager(updateVal);
        }
    }
}
