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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.sql_text = new System.Windows.Forms.TextBox();
            this.run_button = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.console_log = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(354, 177);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(540, 488);
            this.dataGridView1.TabIndex = 0;
            // 
            // sql_text
            // 
            this.sql_text.Location = new System.Drawing.Point(354, 12);
            this.sql_text.Multiline = true;
            this.sql_text.Name = "sql_text";
            this.sql_text.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.sql_text.Size = new System.Drawing.Size(540, 159);
            this.sql_text.TabIndex = 1;
            // 
            // run_button
            // 
            this.run_button.Location = new System.Drawing.Point(256, 12);
            this.run_button.Name = "run_button";
            this.run_button.Size = new System.Drawing.Size(75, 23);
            this.run_button.TabIndex = 2;
            this.run_button.Text = "Run";
            this.run_button.UseVisualStyleBackColor = true;
            this.run_button.Click += new System.EventHandler(this.run_button_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 50);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(336, 615);
            this.listBox1.TabIndex = 3;
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
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 901);
            this.Controls.Add(this.console_log);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.run_button);
            this.Controls.Add(this.sql_text);
            this.Controls.Add(this.dataGridView1);
            this.Name = "ClientForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox sql_text;
        private System.Windows.Forms.Button run_button;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.RichTextBox console_log;
    }
}

