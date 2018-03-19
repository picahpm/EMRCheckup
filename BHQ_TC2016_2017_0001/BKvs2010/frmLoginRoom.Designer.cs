namespace BKvs2010
{
    partial class frmLoginRoom
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLoginRoom));
            this.BtnLogin = new System.Windows.Forms.Button();
            this.CBRoom = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.RDOnQueue = new System.Windows.Forms.RadioButton();
            this.RdOutQueue = new System.Windows.Forms.RadioButton();
            this.PQueue = new System.Windows.Forms.Panel();
            this.PQueue.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnLogin
            // 
            this.BtnLogin.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BtnLogin.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.BtnLogin.Location = new System.Drawing.Point(0, 79);
            this.BtnLogin.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnLogin.Name = "BtnLogin";
            this.BtnLogin.Size = new System.Drawing.Size(301, 36);
            this.BtnLogin.TabIndex = 7;
            this.BtnLogin.Text = "Select";
            this.BtnLogin.UseVisualStyleBackColor = true;
            this.BtnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // CBRoom
            // 
            this.CBRoom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.CBRoom.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.CBRoom.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CBRoom.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.CBRoom.FormattingEnabled = true;
            this.CBRoom.Location = new System.Drawing.Point(65, 24);
            this.CBRoom.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CBRoom.Name = "CBRoom";
            this.CBRoom.Size = new System.Drawing.Size(228, 24);
            this.CBRoom.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.Location = new System.Drawing.Point(8, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "Room :";
            // 
            // RDOnQueue
            // 
            this.RDOnQueue.AutoSize = true;
            this.RDOnQueue.Checked = true;
            this.RDOnQueue.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.RDOnQueue.Location = new System.Drawing.Point(3, 2);
            this.RDOnQueue.Name = "RDOnQueue";
            this.RDOnQueue.Size = new System.Drawing.Size(83, 20);
            this.RDOnQueue.TabIndex = 8;
            this.RDOnQueue.TabStop = true;
            this.RDOnQueue.Text = "On Queue";
            this.RDOnQueue.UseVisualStyleBackColor = true;
            // 
            // RdOutQueue
            // 
            this.RdOutQueue.AutoSize = true;
            this.RdOutQueue.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.RdOutQueue.Location = new System.Drawing.Point(118, 2);
            this.RdOutQueue.Name = "RdOutQueue";
            this.RdOutQueue.Size = new System.Drawing.Size(87, 20);
            this.RdOutQueue.TabIndex = 9;
            this.RdOutQueue.Text = "Out Queue";
            this.RdOutQueue.UseVisualStyleBackColor = true;
            // 
            // PQueue
            // 
            this.PQueue.Controls.Add(this.RDOnQueue);
            this.PQueue.Controls.Add(this.RdOutQueue);
            this.PQueue.Location = new System.Drawing.Point(65, 10);
            this.PQueue.Name = "PQueue";
            this.PQueue.Size = new System.Drawing.Size(209, 23);
            this.PQueue.TabIndex = 10;
            this.PQueue.Visible = false;
            // 
            // frmLoginRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(301, 115);
            this.Controls.Add(this.PQueue);
            this.Controls.Add(this.BtnLogin);
            this.Controls.Add(this.CBRoom);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(317, 149);
            this.MinimumSize = new System.Drawing.Size(317, 149);
            this.Name = "frmLoginRoom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Room";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmLoginRoom_FormClosed);
            this.Load += new System.EventHandler(this.frmLoginRoom_Load);
            this.PQueue.ResumeLayout(false);
            this.PQueue.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnLogin;
        private System.Windows.Forms.ComboBox CBRoom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton RDOnQueue;
        private System.Windows.Forms.RadioButton RdOutQueue;
        private System.Windows.Forms.Panel PQueue;
    }
}