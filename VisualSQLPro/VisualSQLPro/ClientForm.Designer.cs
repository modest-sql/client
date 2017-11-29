namespace VisualSQLPro
{
    partial class ClientForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientForm));
            this.metadata_group = new System.Windows.Forms.GroupBox();
            this.metadata_listBox = new System.Windows.Forms.ListView();
            this.refresh_metadata_button = new System.Windows.Forms.Button();
            this.console_groupBox = new System.Windows.Forms.GroupBox();
            this.console_log = new System.Windows.Forms.RichTextBox();
            this.query_groupBox = new System.Windows.Forms.GroupBox();
            this.queries_tabControl = new System.Windows.Forms.TabControl();
            this.query_toolStrip = new System.Windows.Forms.ToolStrip();
            this.execute_Button = new System.Windows.Forms.ToolStripButton();
            this.load_Button = new System.Windows.Forms.ToolStripButton();
            this.save_Button = new System.Windows.Forms.ToolStripButton();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createDBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.executeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.metadataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.consoleLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.taskManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.themesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.task_manager_groupBox = new System.Windows.Forms.GroupBox();
            this.task_manager_listBox = new System.Windows.Forms.ListBox();
            this.tcp_listener = new System.ComponentModel.BackgroundWorker();
            this.tcp_ping = new System.ComponentModel.BackgroundWorker();
            this.general_toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.metadata_group.SuspendLayout();
            this.console_groupBox.SuspendLayout();
            this.query_groupBox.SuspendLayout();
            this.query_toolStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.task_manager_groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // metadata_group
            // 
            this.metadata_group.Controls.Add(this.metadata_listBox);
            this.metadata_group.Controls.Add(this.refresh_metadata_button);
            this.metadata_group.Dock = System.Windows.Forms.DockStyle.Left;
            this.metadata_group.Location = new System.Drawing.Point(0, 24);
            this.metadata_group.Name = "metadata_group";
            this.metadata_group.Size = new System.Drawing.Size(200, 580);
            this.metadata_group.TabIndex = 0;
            this.metadata_group.TabStop = false;
            this.metadata_group.Text = "Metadata";
            // 
            // metadata_listBox
            // 
            this.metadata_listBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metadata_listBox.Location = new System.Drawing.Point(3, 39);
            this.metadata_listBox.Name = "metadata_listBox";
            this.metadata_listBox.Size = new System.Drawing.Size(194, 538);
            this.metadata_listBox.TabIndex = 4;
            this.metadata_listBox.UseCompatibleStateImageBehavior = false;
            // 
            // refresh_metadata_button
            // 
            this.refresh_metadata_button.Dock = System.Windows.Forms.DockStyle.Top;
            this.refresh_metadata_button.Location = new System.Drawing.Point(3, 16);
            this.refresh_metadata_button.Name = "refresh_metadata_button";
            this.refresh_metadata_button.Size = new System.Drawing.Size(194, 23);
            this.refresh_metadata_button.TabIndex = 3;
            this.refresh_metadata_button.Text = "Refresh";
            this.general_toolTip.SetToolTip(this.refresh_metadata_button, "Refresh database metadata");
            this.refresh_metadata_button.UseVisualStyleBackColor = true;
            this.refresh_metadata_button.Click += new System.EventHandler(this.refresh_metadata_button_Click);
            // 
            // console_groupBox
            // 
            this.console_groupBox.Controls.Add(this.console_log);
            this.console_groupBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.console_groupBox.Location = new System.Drawing.Point(200, 470);
            this.console_groupBox.Name = "console_groupBox";
            this.console_groupBox.Size = new System.Drawing.Size(218, 134);
            this.console_groupBox.TabIndex = 1;
            this.console_groupBox.TabStop = false;
            this.console_groupBox.Text = "Console Log";
            // 
            // console_log
            // 
            this.console_log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.console_log.Location = new System.Drawing.Point(3, 16);
            this.console_log.Name = "console_log";
            this.console_log.ReadOnly = true;
            this.console_log.Size = new System.Drawing.Size(212, 115);
            this.console_log.TabIndex = 2;
            this.console_log.Text = "";
            // 
            // query_groupBox
            // 
            this.query_groupBox.Controls.Add(this.queries_tabControl);
            this.query_groupBox.Controls.Add(this.query_toolStrip);
            this.query_groupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.query_groupBox.Location = new System.Drawing.Point(200, 24);
            this.query_groupBox.Name = "query_groupBox";
            this.query_groupBox.Size = new System.Drawing.Size(218, 446);
            this.query_groupBox.TabIndex = 2;
            this.query_groupBox.TabStop = false;
            this.query_groupBox.Text = "Queries";
            // 
            // queries_tabControl
            // 
            this.queries_tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.queries_tabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.queries_tabControl.Location = new System.Drawing.Point(3, 48);
            this.queries_tabControl.Name = "queries_tabControl";
            this.queries_tabControl.SelectedIndex = 0;
            this.queries_tabControl.Size = new System.Drawing.Size(212, 395);
            this.queries_tabControl.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.queries_tabControl.TabIndex = 0;
            this.queries_tabControl.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.queries_tabControl_DrawItem);
            this.queries_tabControl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.queries_tabControl_MouseClick);
            // 
            // query_toolStrip
            // 
            this.query_toolStrip.ImageScalingSize = new System.Drawing.Size(25, 25);
            this.query_toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.execute_Button,
            this.load_Button,
            this.save_Button});
            this.query_toolStrip.Location = new System.Drawing.Point(3, 16);
            this.query_toolStrip.Name = "query_toolStrip";
            this.query_toolStrip.Size = new System.Drawing.Size(212, 32);
            this.query_toolStrip.TabIndex = 1;
            // 
            // execute_Button
            // 
            this.execute_Button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.execute_Button.Image = global::VisualSQLPro.Properties.Resources.execute;
            this.execute_Button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.execute_Button.Name = "execute_Button";
            this.execute_Button.Size = new System.Drawing.Size(29, 29);
            this.execute_Button.ToolTipText = "Execute the current query";
            this.execute_Button.Click += new System.EventHandler(this.execute_Button_Click);
            // 
            // load_Button
            // 
            this.load_Button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.load_Button.Image = global::VisualSQLPro.Properties.Resources.load;
            this.load_Button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.load_Button.Name = "load_Button";
            this.load_Button.Size = new System.Drawing.Size(29, 29);
            this.load_Button.ToolTipText = "Load a query from a file";
            this.load_Button.Click += new System.EventHandler(this.load_Button_Click);
            // 
            // save_Button
            // 
            this.save_Button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.save_Button.Image = global::VisualSQLPro.Properties.Resources.save;
            this.save_Button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.save_Button.Name = "save_Button";
            this.save_Button.Size = new System.Drawing.Size(29, 29);
            this.save_Button.ToolTipText = "Save the current query into a file";
            this.save_Button.Click += new System.EventHandler(this.save_Button_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.themesToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(618, 24);
            this.menuStrip.TabIndex = 3;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createDBToolStripMenuItem,
            this.executeToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // createDBToolStripMenuItem
            // 
            this.createDBToolStripMenuItem.Name = "createDBToolStripMenuItem";
            this.createDBToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.createDBToolStripMenuItem.Text = "Create DB";
            this.createDBToolStripMenuItem.Click += new System.EventHandler(this.createDBToolStripMenuItem_Click);
            // 
            // executeToolStripMenuItem
            // 
            this.executeToolStripMenuItem.Name = "executeToolStripMenuItem";
            this.executeToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.executeToolStripMenuItem.Text = "Execute";
            this.executeToolStripMenuItem.Click += new System.EventHandler(this.executeToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.metadataToolStripMenuItem,
            this.consoleLogToolStripMenuItem,
            this.taskManagerToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // metadataToolStripMenuItem
            // 
            this.metadataToolStripMenuItem.Name = "metadataToolStripMenuItem";
            this.metadataToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.metadataToolStripMenuItem.Text = "Metadata";
            this.metadataToolStripMenuItem.Click += new System.EventHandler(this.metadataToolStripMenuItem_Click);
            // 
            // consoleLogToolStripMenuItem
            // 
            this.consoleLogToolStripMenuItem.Name = "consoleLogToolStripMenuItem";
            this.consoleLogToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.consoleLogToolStripMenuItem.Text = "Console Log";
            this.consoleLogToolStripMenuItem.Click += new System.EventHandler(this.consoleLogToolStripMenuItem_Click);
            // 
            // taskManagerToolStripMenuItem
            // 
            this.taskManagerToolStripMenuItem.Name = "taskManagerToolStripMenuItem";
            this.taskManagerToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.taskManagerToolStripMenuItem.Text = "Task Manager";
            this.taskManagerToolStripMenuItem.Click += new System.EventHandler(this.taskManagerToolStripMenuItem_Click);
            // 
            // themesToolStripMenuItem
            // 
            this.themesToolStripMenuItem.Name = "themesToolStripMenuItem";
            this.themesToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.themesToolStripMenuItem.Text = "Themes";
            this.themesToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.themesToolStripMenuItem_DropDownItemClicked);
            // 
            // task_manager_groupBox
            // 
            this.task_manager_groupBox.Controls.Add(this.task_manager_listBox);
            this.task_manager_groupBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.task_manager_groupBox.Location = new System.Drawing.Point(418, 24);
            this.task_manager_groupBox.Name = "task_manager_groupBox";
            this.task_manager_groupBox.Size = new System.Drawing.Size(200, 580);
            this.task_manager_groupBox.TabIndex = 4;
            this.task_manager_groupBox.TabStop = false;
            this.task_manager_groupBox.Text = "Task Manager";
            // 
            // task_manager_listBox
            // 
            this.task_manager_listBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.task_manager_listBox.FormattingEnabled = true;
            this.task_manager_listBox.HorizontalScrollbar = true;
            this.task_manager_listBox.Location = new System.Drawing.Point(3, 16);
            this.task_manager_listBox.Name = "task_manager_listBox";
            this.task_manager_listBox.Size = new System.Drawing.Size(194, 561);
            this.task_manager_listBox.TabIndex = 0;
            // 
            // tcp_listener
            // 
            this.tcp_listener.DoWork += new System.ComponentModel.DoWorkEventHandler(this.tcp_listener_DoWork);
            // 
            // tcp_ping
            // 
            this.tcp_ping.DoWork += new System.ComponentModel.DoWorkEventHandler(this.tcp_ping_DoWork);
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 604);
            this.Controls.Add(this.query_groupBox);
            this.Controls.Add(this.console_groupBox);
            this.Controls.Add(this.metadata_group);
            this.Controls.Add(this.task_manager_groupBox);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "ClientForm";
            this.Text = "Modest SQL Client Pro";
            this.metadata_group.ResumeLayout(false);
            this.console_groupBox.ResumeLayout(false);
            this.query_groupBox.ResumeLayout(false);
            this.query_groupBox.PerformLayout();
            this.query_toolStrip.ResumeLayout(false);
            this.query_toolStrip.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.task_manager_groupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox metadata_group;
        private System.Windows.Forms.Button refresh_metadata_button;
        private System.Windows.Forms.GroupBox console_groupBox;
        private System.Windows.Forms.RichTextBox console_log;
        private System.Windows.Forms.GroupBox query_groupBox;
        private System.Windows.Forms.TabControl queries_tabControl;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem metadataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem consoleLogToolStripMenuItem;
        private System.Windows.Forms.GroupBox task_manager_groupBox;
        private System.Windows.Forms.ListBox task_manager_listBox;
        private System.Windows.Forms.ToolStripMenuItem taskManagerToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker tcp_listener;
        private System.ComponentModel.BackgroundWorker tcp_ping;
        private System.Windows.Forms.ToolStripMenuItem executeToolStripMenuItem;
        private System.Windows.Forms.ToolStrip query_toolStrip;
        private System.Windows.Forms.ToolStripButton execute_Button;
        private System.Windows.Forms.ToolStripButton load_Button;
        private System.Windows.Forms.ToolStripButton save_Button;
        private System.Windows.Forms.ToolTip general_toolTip;
        private System.Windows.Forms.ToolStripMenuItem themesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createDBToolStripMenuItem;
        private System.Windows.Forms.ListView metadata_listBox;
    }
}

