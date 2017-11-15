namespace VisualSQLPro
{
    public partial class ClientForm
    {
        delegate void UpdateMetadataPosition();
        delegate void UpdateConsolePosition();
        delegate void UpdateTaskManagerPosition();

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
    }
}
