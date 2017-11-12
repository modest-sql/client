namespace VisualSQL
{
    partial class PopupForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopupForm));
            this.ip_textBox = new System.Windows.Forms.TextBox();
            this.ip_label = new System.Windows.Forms.Label();
            this.connect_button = new System.Windows.Forms.Button();
            this.port_label = new System.Windows.Forms.Label();
            this.port_textBox = new System.Windows.Forms.TextBox();
            this.localhost_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ip_textBox
            // 
            this.ip_textBox.Location = new System.Drawing.Point(87, 12);
            this.ip_textBox.Name = "ip_textBox";
            this.ip_textBox.Size = new System.Drawing.Size(185, 20);
            this.ip_textBox.TabIndex = 0;
            // 
            // ip_label
            // 
            this.ip_label.AutoSize = true;
            this.ip_label.Location = new System.Drawing.Point(12, 15);
            this.ip_label.Name = "ip_label";
            this.ip_label.Size = new System.Drawing.Size(58, 13);
            this.ip_label.TabIndex = 1;
            this.ip_label.Text = "IP Address";
            // 
            // connect_button
            // 
            this.connect_button.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.connect_button.Location = new System.Drawing.Point(197, 94);
            this.connect_button.Name = "connect_button";
            this.connect_button.Size = new System.Drawing.Size(75, 23);
            this.connect_button.TabIndex = 3;
            this.connect_button.Text = "Connect";
            this.connect_button.UseVisualStyleBackColor = true;
            // 
            // port_label
            // 
            this.port_label.AutoSize = true;
            this.port_label.Location = new System.Drawing.Point(12, 58);
            this.port_label.Name = "port_label";
            this.port_label.Size = new System.Drawing.Size(66, 13);
            this.port_label.TabIndex = 4;
            this.port_label.Text = "Port Number";
            // 
            // port_textBox
            // 
            this.port_textBox.Location = new System.Drawing.Point(87, 55);
            this.port_textBox.Name = "port_textBox";
            this.port_textBox.Size = new System.Drawing.Size(185, 20);
            this.port_textBox.TabIndex = 2;
            // 
            // localhost_button
            // 
            this.localhost_button.Location = new System.Drawing.Point(15, 94);
            this.localhost_button.Name = "localhost_button";
            this.localhost_button.Size = new System.Drawing.Size(75, 23);
            this.localhost_button.TabIndex = 5;
            this.localhost_button.Text = "Localhost";
            this.localhost_button.UseVisualStyleBackColor = true;
            this.localhost_button.Click += new System.EventHandler(this.LocalhostClick);
            // 
            // PopupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 144);
            this.Controls.Add(this.localhost_button);
            this.Controls.Add(this.port_label);
            this.Controls.Add(this.port_textBox);
            this.Controls.Add(this.connect_button);
            this.Controls.Add(this.ip_label);
            this.Controls.Add(this.ip_textBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PopupForm";
            this.Text = "TCP Connection";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ip_textBox;
        private System.Windows.Forms.Label ip_label;
        private System.Windows.Forms.Button connect_button;
        private System.Windows.Forms.Label port_label;
        private System.Windows.Forms.TextBox port_textBox;
        private System.Windows.Forms.Button localhost_button;
    }
}