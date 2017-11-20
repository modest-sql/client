using System;
using System.Windows.Forms;

namespace VisualSQLPro
{
    public partial class ClientForm
    {
        private void AdjustTaskManagerPosition()
        {
            if (task_manager_groupBox.Width >= (Size.Width * 0.025) && task_manager_groupBox.Visible == false)
                task_manager_groupBox.Visible = true;
            else if (task_manager_groupBox.Width < (Size.Width * 0.025) && task_manager_groupBox.Visible)
                task_manager_groupBox.Visible = false;
            else
                task_manager_groupBox.Width = Convert.ToInt32(Size.Width - PointToClient(Cursor.Position).X - 15); // For some reason, resize is always 15 pixels off
        }

        private void AdjustMetadataPosition()
        {
            if (metadata_group.Width >= (Size.Width * 0.025) && metadata_group.Visible == false)
                metadata_group.Visible = true;
            else if (metadata_group.Width < (Size.Width * 0.025) && metadata_group.Visible)
                metadata_group.Visible = false;
            else
                metadata_group.Width = Convert.ToInt32(PointToClient(Cursor.Position).X + 2); // For some reason, resize is always 2 pixels off
        }

        private void AdjustConsoleLogPosition()
        {
            if (console_groupBox.Height >= (Size.Height * 0.025) && console_groupBox.Visible == false)
                console_groupBox.Visible = true;
            else if (console_groupBox.Height < (Size.Height * 0.025) && console_groupBox.Visible)
                console_groupBox.Visible = false;
            else
                console_groupBox.Height = Convert.ToInt32(Size.Height - PointToClient(Cursor.Position).Y - 30); // For some reason, resize is always 30 pixels off
        }
    }
}
