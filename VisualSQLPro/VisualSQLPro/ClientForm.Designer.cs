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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientForm));
            this.metadata_group = new System.Windows.Forms.GroupBox();
            this.refresh_metadata_button = new System.Windows.Forms.Button();
            this.metadata_listBox = new System.Windows.Forms.ListBox();
            this.console_groupBox = new System.Windows.Forms.GroupBox();
            this.console_log = new System.Windows.Forms.RichTextBox();
            this.metadata_group.SuspendLayout();
            this.console_groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // metadata_group
            // 
            this.metadata_group.Controls.Add(this.refresh_metadata_button);
            this.metadata_group.Controls.Add(this.metadata_listBox);
            this.metadata_group.Dock = System.Windows.Forms.DockStyle.Left;
            this.metadata_group.Location = new System.Drawing.Point(0, 0);
            this.metadata_group.Name = "metadata_group";
            this.metadata_group.Size = new System.Drawing.Size(200, 604);
            this.metadata_group.TabIndex = 0;
            this.metadata_group.TabStop = false;
            this.metadata_group.Text = "Metadata";
            // 
            // refresh_metadata_button
            // 
            this.refresh_metadata_button.Dock = System.Windows.Forms.DockStyle.Top;
            this.refresh_metadata_button.Location = new System.Drawing.Point(3, 16);
            this.refresh_metadata_button.Name = "refresh_metadata_button";
            this.refresh_metadata_button.Size = new System.Drawing.Size(194, 23);
            this.refresh_metadata_button.TabIndex = 3;
            this.refresh_metadata_button.Text = "Refresh";
            this.refresh_metadata_button.UseVisualStyleBackColor = true;
            // 
            // metadata_listBox
            // 
            this.metadata_listBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metadata_listBox.FormattingEnabled = true;
            this.metadata_listBox.Location = new System.Drawing.Point(3, 16);
            this.metadata_listBox.Name = "metadata_listBox";
            this.metadata_listBox.Size = new System.Drawing.Size(194, 585);
            this.metadata_listBox.TabIndex = 2;
            // 
            // console_groupBox
            // 
            this.console_groupBox.Controls.Add(this.console_log);
            this.console_groupBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.console_groupBox.Location = new System.Drawing.Point(200, 470);
            this.console_groupBox.Name = "console_groupBox";
            this.console_groupBox.Size = new System.Drawing.Size(418, 134);
            this.console_groupBox.TabIndex = 1;
            this.console_groupBox.TabStop = false;
            this.console_groupBox.Text = "Console Log";
            // 
            // console_log
            // 
            this.console_log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.console_log.Location = new System.Drawing.Point(3, 16);
            this.console_log.Name = "console_log";
            this.console_log.Size = new System.Drawing.Size(412, 115);
            this.console_log.TabIndex = 2;
            this.console_log.Text = "";
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 604);
            this.Controls.Add(this.console_groupBox);
            this.Controls.Add(this.metadata_group);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ClientForm";
            this.Text = "Form1";
            this.metadata_group.ResumeLayout(false);
            this.console_groupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox metadata_group;
        private System.Windows.Forms.ListBox metadata_listBox;
        private System.Windows.Forms.Button refresh_metadata_button;
        private System.Windows.Forms.GroupBox console_groupBox;
        private System.Windows.Forms.RichTextBox console_log;
    }
}

