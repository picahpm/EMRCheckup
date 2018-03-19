namespace CheckupBO
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.LoginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LogoffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.policyEditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.policyEditToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(1140, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "MenuStrip";
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LoginToolStripMenuItem,
            this.LogoffToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileMenu.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileMenu.ImageTransparentColor = System.Drawing.SystemColors.ActiveBorder;
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(78, 20);
            this.fileMenu.Tag = "1";
            this.fileMenu.Text = "&MainMenu";
            // 
            // LoginToolStripMenuItem
            // 
            this.LoginToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("LoginToolStripMenuItem.Image")));
            this.LoginToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.LoginToolStripMenuItem.Name = "LoginToolStripMenuItem";
            this.LoginToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.LoginToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.LoginToolStripMenuItem.Tag = "1";
            this.LoginToolStripMenuItem.Text = "&Log In";
            this.LoginToolStripMenuItem.Click += new System.EventHandler(this.LoginToolStripMenuItem_Click);
            // 
            // LogoffToolStripMenuItem
            // 
            this.LogoffToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("LogoffToolStripMenuItem.Image")));
            this.LogoffToolStripMenuItem.Name = "LogoffToolStripMenuItem";
            this.LogoffToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.LogoffToolStripMenuItem.Tag = "1";
            this.LogoffToolStripMenuItem.Text = "&Log Off";
            this.LogoffToolStripMenuItem.Visible = false;
            this.LogoffToolStripMenuItem.Click += new System.EventHandler(this.LogoffToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.exitToolStripMenuItem.Tag = "1";
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // policyEditToolStripMenuItem
            // 
            this.policyEditToolStripMenuItem.Name = "policyEditToolStripMenuItem";
            this.policyEditToolStripMenuItem.Size = new System.Drawing.Size(74, 20);
            this.policyEditToolStripMenuItem.Tag = "1";
            this.policyEditToolStripMenuItem.Text = "Policy Edit";
            this.policyEditToolStripMenuItem.Visible = false;
            this.policyEditToolStripMenuItem.Click += new System.EventHandler(this.policyEditToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1140, 658);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Back Office EMR Check-up";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem LoginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LogoffToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem policyEditToolStripMenuItem;
    }
}