namespace Assingment_2
{
    partial class ChatForm
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
            this.menustrip = new System.Windows.Forms.MenuStrip();
            this.connectionsubmenu = new System.Windows.Forms.ToolStripMenuItem();
            this.connectbutton = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectbutton = new System.Windows.Forms.ToolStripMenuItem();
            this.sendbutton = new System.Windows.Forms.Button();
            this.messagetextbox = new System.Windows.Forms.TextBox();
            this.conversationtextbox = new System.Windows.Forms.TextBox();
            this.menustrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menustrip
            // 
            this.menustrip.AutoSize = false;
            this.menustrip.BackColor = System.Drawing.Color.Transparent;
            this.menustrip.GripMargin = new System.Windows.Forms.Padding(0);
            this.menustrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectionsubmenu});
            this.menustrip.Location = new System.Drawing.Point(0, 0);
            this.menustrip.Name = "menustrip";
            this.menustrip.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.menustrip.Size = new System.Drawing.Size(392, 18);
            this.menustrip.TabIndex = 0;
            this.menustrip.Text = "menustrip";
            // 
            // connectionsubmenu
            // 
            this.connectionsubmenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectbutton,
            this.disconnectbutton});
            this.connectionsubmenu.Name = "connectionsubmenu";
            this.connectionsubmenu.Size = new System.Drawing.Size(79, 16);
            this.connectionsubmenu.Text = "Connection";
            // 
            // connectbutton
            // 
            this.connectbutton.BackColor = System.Drawing.Color.Transparent;
            this.connectbutton.Name = "connectbutton";
            this.connectbutton.Size = new System.Drawing.Size(134, 22);
            this.connectbutton.Text = "Connect";
            this.connectbutton.Click += new System.EventHandler(this.connectbutton_Click);
            // 
            // disconnectbutton
            // 
            this.disconnectbutton.BackColor = System.Drawing.Color.Transparent;
            this.disconnectbutton.Name = "disconnectbutton";
            this.disconnectbutton.Size = new System.Drawing.Size(134, 22);
            this.disconnectbutton.Text = "Disconnect";
            this.disconnectbutton.Click += new System.EventHandler(this.disconnectbutton_Click);
            // 
            // sendbutton
            // 
            this.sendbutton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sendbutton.Location = new System.Drawing.Point(316, 441);
            this.sendbutton.Name = "sendbutton";
            this.sendbutton.Size = new System.Drawing.Size(68, 23);
            this.sendbutton.TabIndex = 1;
            this.sendbutton.Text = "Send";
            this.sendbutton.UseVisualStyleBackColor = true;
            this.sendbutton.Click += new System.EventHandler(this.sendbutton_Click);
            // 
            // messagetextbox
            // 
            this.messagetextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.messagetextbox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.messagetextbox.Location = new System.Drawing.Point(9, 384);
            this.messagetextbox.Margin = new System.Windows.Forms.Padding(0);
            this.messagetextbox.Multiline = true;
            this.messagetextbox.Name = "messagetextbox";
            this.messagetextbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.messagetextbox.Size = new System.Drawing.Size(300, 80);
            this.messagetextbox.TabIndex = 2;
            // 
            // conversationtextbox
            // 
            this.conversationtextbox.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.conversationtextbox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.conversationtextbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.conversationtextbox.Location = new System.Drawing.Point(9, 21);
            this.conversationtextbox.Multiline = true;
            this.conversationtextbox.Name = "conversationtextbox";
            this.conversationtextbox.ReadOnly = true;
            this.conversationtextbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.conversationtextbox.Size = new System.Drawing.Size(375, 360);
            this.conversationtextbox.TabIndex = 3;
            // 
            // ChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 473);
            this.Controls.Add(this.conversationtextbox);
            this.Controls.Add(this.messagetextbox);
            this.Controls.Add(this.sendbutton);
            this.Controls.Add(this.menustrip);
            this.MainMenuStrip = this.menustrip;
            this.MaximumSize = new System.Drawing.Size(400, 500);
            this.MinimumSize = new System.Drawing.Size(400, 500);
            this.Name = "ChatForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ray\'s Chat Application";
            this.Load += new System.EventHandler(this.ChatForm_Load);
            this.menustrip.ResumeLayout(false);
            this.menustrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menustrip;
        private System.Windows.Forms.ToolStripMenuItem connectionsubmenu;
        private System.Windows.Forms.ToolStripMenuItem connectbutton;
        private System.Windows.Forms.ToolStripMenuItem disconnectbutton;
        private System.Windows.Forms.Button sendbutton;
        private System.Windows.Forms.TextBox messagetextbox;
        private System.Windows.Forms.TextBox conversationtextbox;
    }
}

