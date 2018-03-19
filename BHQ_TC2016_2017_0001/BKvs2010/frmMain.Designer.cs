namespace BKvs2010
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.LoginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LogoffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.piorityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testCodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testButtonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vitalSignsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comulativeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolstatusLocation = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStatusUserLogin = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolstatusIPaddress = new System.Windows.Forms.ToolStripStatusLabel();
            this.stripVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.piorityToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(1095, 24);
            this.menuStrip.TabIndex = 0;
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
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolsStripMenuItem_Click);
            // 
            // piorityToolStripMenuItem
            // 
            this.piorityToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testCodeToolStripMenuItem,
            this.vitalSignsToolStripMenuItem,
            this.comulativeToolStripMenuItem});
            this.piorityToolStripMenuItem.Name = "piorityToolStripMenuItem";
            this.piorityToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.piorityToolStripMenuItem.Tag = "1";
            this.piorityToolStripMenuItem.Text = "Test Fuction";
            this.piorityToolStripMenuItem.Visible = false;
            // 
            // testCodeToolStripMenuItem
            // 
            this.testCodeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testButtonToolStripMenuItem});
            this.testCodeToolStripMenuItem.Name = "testCodeToolStripMenuItem";
            this.testCodeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.testCodeToolStripMenuItem.Tag = "1";
            this.testCodeToolStripMenuItem.Text = "Test Code";
            // 
            // testButtonToolStripMenuItem
            // 
            this.testButtonToolStripMenuItem.Name = "testButtonToolStripMenuItem";
            this.testButtonToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.testButtonToolStripMenuItem.Text = "Test Button";
            // 
            // vitalSignsToolStripMenuItem
            // 
            this.vitalSignsToolStripMenuItem.Name = "vitalSignsToolStripMenuItem";
            this.vitalSignsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.vitalSignsToolStripMenuItem.Text = "VitalSigns";
            this.vitalSignsToolStripMenuItem.Click += new System.EventHandler(this.vitalSignsToolStripMenuItem_Click);
            // 
            // comulativeToolStripMenuItem
            // 
            this.comulativeToolStripMenuItem.Name = "comulativeToolStripMenuItem";
            this.comulativeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.comulativeToolStripMenuItem.Text = "Comulative";
            this.comulativeToolStripMenuItem.Click += new System.EventHandler(this.comulativeToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolstatusLocation,
            this.toolStatusUserLogin,
            this.toolstatusIPaddress,
            this.stripVersion});
            this.statusStrip.Location = new System.Drawing.Point(0, 601);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip.Size = new System.Drawing.Size(1095, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // toolstatusLocation
            // 
            this.toolstatusLocation.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolstatusLocation.Name = "toolstatusLocation";
            this.toolstatusLocation.Size = new System.Drawing.Size(342, 17);
            this.toolstatusLocation.Spring = true;
            this.toolstatusLocation.Text = "Location :";
            this.toolstatusLocation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStatusUserLogin
            // 
            this.toolStatusUserLogin.Name = "toolStatusUserLogin";
            this.toolStatusUserLogin.Size = new System.Drawing.Size(342, 17);
            this.toolStatusUserLogin.Spring = true;
            this.toolStatusUserLogin.Text = "User Log in :";
            this.toolStatusUserLogin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolstatusIPaddress
            // 
            this.toolstatusIPaddress.Name = "toolstatusIPaddress";
            this.toolstatusIPaddress.Size = new System.Drawing.Size(342, 17);
            this.toolstatusIPaddress.Spring = true;
            this.toolstatusIPaddress.Text = "Computer IP Address : ";
            this.toolstatusIPaddress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // stripVersion
            // 
            this.stripVersion.Name = "stripVersion";
            this.stripVersion.Size = new System.Drawing.Size(52, 17);
            this.stripVersion.Text = "Version :";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1095, 623);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "หน้าเมนูหลัก";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolstatusLocation;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem LoginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LogoffToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripStatusLabel toolStatusUserLogin;
        private System.Windows.Forms.ToolStripStatusLabel toolstatusIPaddress;
        private System.Windows.Forms.ToolStripMenuItem piorityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testCodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vitalSignsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem comulativeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testButtonToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel stripVersion;
    }
}



