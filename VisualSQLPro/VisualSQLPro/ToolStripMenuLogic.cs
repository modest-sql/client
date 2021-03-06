﻿using System;

namespace VisualSQLPro
{
    public partial class ClientForm
    {
        private void metadataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (metadata_group.Visible == false)
                metadata_group.Width = (int)(Size.Width * 0.20);
            metadata_group.Visible = !metadata_group.Visible;
        }

        private void consoleLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (console_groupBox.Visible == false)
                console_groupBox.Height = (int)(Size.Height * 0.20);
            console_groupBox.Visible = !console_groupBox.Visible;
        }

        private void taskManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (task_manager_groupBox.Visible == false)
                task_manager_groupBox.Width = (int)(Size.Width * 0.20);
            task_manager_groupBox.Visible = !task_manager_groupBox.Visible;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PromptSave();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PromptLoad();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }

        private void executeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            execute_sql();
        }

        private void createDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateDb();
        }
    }
}
