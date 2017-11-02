namespace VisualSQL
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
            this.table_dataGridView = new System.Windows.Forms.DataGridView();
            this.sql_text = new System.Windows.Forms.TextBox();
            this.run_button = new System.Windows.Forms.Button();
            this.metadata_listBox = new System.Windows.Forms.ListBox();
            this.console_log = new System.Windows.Forms.RichTextBox();
            this.metadata_label = new System.Windows.Forms.Label();
            this.load_button = new System.Windows.Forms.Button();
            this.save_button = new System.Windows.Forms.Button();
            this.tcp_listener = new System.ComponentModel.BackgroundWorker();
            this.connected_pictureBox = new System.Windows.Forms.PictureBox();
            this.connected_label = new System.Windows.Forms.Label();
            this.tcp_ping = new System.ComponentModel.BackgroundWorker();
            this.general_tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.refresh_button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.table_dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.connected_pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // table_dataGridView
            // 
            this.table_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.table_dataGridView.Location = new System.Drawing.Point(354, 215);
            this.table_dataGridView.Name = "table_dataGridView";
            this.table_dataGridView.Size = new System.Drawing.Size(540, 450);
            this.table_dataGridView.TabIndex = 0;
            this.general_tooltip.SetToolTip(this.table_dataGridView, "Queried table contents");
            // 
            // sql_text
            // 
            this.sql_text.Location = new System.Drawing.Point(354, 50);
            this.sql_text.Multiline = true;
            this.sql_text.Name = "sql_text";
            this.sql_text.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.sql_text.Size = new System.Drawing.Size(540, 159);
            this.sql_text.TabIndex = 1;
            this.sql_text.Enter += new System.EventHandler(this.sql_text_Enter);
            this.sql_text.Leave += new System.EventHandler(this.sql_text_Leave);
            // 
            // run_button
            // 
            this.run_button.Location = new System.Drawing.Point(354, 21);
            this.run_button.Name = "run_button";
            this.run_button.Size = new System.Drawing.Size(86, 23);
            this.run_button.TabIndex = 2;
            this.run_button.Text = "Execute Query";
            this.general_tooltip.SetToolTip(this.run_button, "Execute the current query");
            this.run_button.UseVisualStyleBackColor = true;
            this.run_button.Click += new System.EventHandler(this.run_button_Click);
            // 
            // metadata_listBox
            // 
            this.metadata_listBox.FormattingEnabled = true;
            this.metadata_listBox.Location = new System.Drawing.Point(12, 50);
            this.metadata_listBox.Name = "metadata_listBox";
            this.metadata_listBox.Size = new System.Drawing.Size(336, 615);
            this.metadata_listBox.TabIndex = 3;
            this.metadata_listBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.metadata_listBox_MouseDoubleClick);
            // 
            // console_log
            // 
            this.console_log.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.console_log.Location = new System.Drawing.Point(12, 671);
            this.console_log.Name = "console_log";
            this.console_log.ReadOnly = true;
            this.console_log.Size = new System.Drawing.Size(882, 218);
            this.console_log.TabIndex = 4;
            this.console_log.Text = "";
            // 
            // metadata_label
            // 
            this.metadata_label.AutoSize = true;
            this.metadata_label.Location = new System.Drawing.Point(12, 34);
            this.metadata_label.Name = "metadata_label";
            this.metadata_label.Size = new System.Drawing.Size(90, 13);
            this.metadata_label.TabIndex = 5;
            this.metadata_label.Text = "Current Database";
            // 
            // load_button
            // 
            this.load_button.Location = new System.Drawing.Point(446, 21);
            this.load_button.Name = "load_button";
            this.load_button.Size = new System.Drawing.Size(75, 23);
            this.load_button.TabIndex = 6;
            this.load_button.Text = "Load";
            this.general_tooltip.SetToolTip(this.load_button, "Load a query from a file");
            this.load_button.UseVisualStyleBackColor = true;
            this.load_button.Click += new System.EventHandler(this.load_button_Click);
            // 
            // save_button
            // 
            this.save_button.Location = new System.Drawing.Point(527, 21);
            this.save_button.Name = "save_button";
            this.save_button.Size = new System.Drawing.Size(75, 23);
            this.save_button.TabIndex = 7;
            this.save_button.Text = "Save";
            this.general_tooltip.SetToolTip(this.save_button, "Save the current query into a file");
            this.save_button.UseVisualStyleBackColor = true;
            this.save_button.Click += new System.EventHandler(this.save_button_Click);
            // 
            // tcp_listener
            // 
            this.tcp_listener.DoWork += new System.ComponentModel.DoWorkEventHandler(this.tcp_listener_DoWork);
            // 
            // connected_pictureBox
            // 
            this.connected_pictureBox.Location = new System.Drawing.Point(15, 12);
            this.connected_pictureBox.Name = "connected_pictureBox";
            this.connected_pictureBox.Size = new System.Drawing.Size(17, 17);
            this.connected_pictureBox.TabIndex = 9;
            this.connected_pictureBox.TabStop = false;
            this.general_tooltip.SetToolTip(this.connected_pictureBox, "Connection status");
            // 
            // connected_label
            // 
            this.connected_label.AutoSize = true;
            this.connected_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.connected_label.Location = new System.Drawing.Point(38, 12);
            this.connected_label.Name = "connected_label";
            this.connected_label.Size = new System.Drawing.Size(68, 13);
            this.connected_label.TabIndex = 10;
            this.connected_label.Text = "Connected";
            // 
            // tcp_ping
            // 
            this.tcp_ping.DoWork += new System.ComponentModel.DoWorkEventHandler(this.tcp_ping_DoWork);
            // 
            // refresh_button
            // 
            this.refresh_button.Location = new System.Drawing.Point(108, 24);
            this.refresh_button.Name = "refresh_button";
            this.refresh_button.Size = new System.Drawing.Size(28, 23);
            this.refresh_button.TabIndex = 11;
            this.refresh_button.Text = "button1";
            this.general_tooltip.SetToolTip(this.refresh_button, "Refresh database metadata");
            this.refresh_button.UseVisualStyleBackColor = true;
            this.refresh_button.Click += new System.EventHandler(this.refresh_button_Click);
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 901);
            this.Controls.Add(this.refresh_button);
            this.Controls.Add(this.connected_label);
            this.Controls.Add(this.connected_pictureBox);
            this.Controls.Add(this.save_button);
            this.Controls.Add(this.load_button);
            this.Controls.Add(this.metadata_label);
            this.Controls.Add(this.console_log);
            this.Controls.Add(this.metadata_listBox);
            this.Controls.Add(this.run_button);
            this.Controls.Add(this.sql_text);
            this.Controls.Add(this.table_dataGridView);
            this.Name = "ClientForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.table_dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.connected_pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView table_dataGridView;
        private System.Windows.Forms.TextBox sql_text;
        private System.Windows.Forms.Button run_button;
        private System.Windows.Forms.ListBox metadata_listBox;
        private System.Windows.Forms.RichTextBox console_log;
        private System.Windows.Forms.Label metadata_label;
        private System.Windows.Forms.Button load_button;
        private System.Windows.Forms.Button save_button;
        private System.ComponentModel.BackgroundWorker tcp_listener;
        private System.Windows.Forms.PictureBox connected_pictureBox;
        private System.Windows.Forms.Label connected_label;
        private System.ComponentModel.BackgroundWorker tcp_ping;
        private System.Windows.Forms.ToolTip general_tooltip;
        private System.Windows.Forms.Button refresh_button;
    }
}

