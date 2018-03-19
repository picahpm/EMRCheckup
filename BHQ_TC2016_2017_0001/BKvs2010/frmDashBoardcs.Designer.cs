namespace BKvs2010
{
    partial class frmDashBoardcs
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
            this.tbPanal = new System.Windows.Forms.TableLayoutPanel();
            this.uiDashBoard = new BKvs2010.Usercontrols.UIDashBoard();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnPointMaster = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.timerRefreshDashBoard = new System.Windows.Forms.Timer(this.components);
            this.tbPanal.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbPanal
            // 
            this.tbPanal.ColumnCount = 1;
            this.tbPanal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbPanal.Controls.Add(this.uiDashBoard, 0, 1);
            this.tbPanal.Controls.Add(this.panel1, 0, 0);
            this.tbPanal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbPanal.Location = new System.Drawing.Point(0, 0);
            this.tbPanal.Name = "tbPanal";
            this.tbPanal.RowCount = 2;
            this.tbPanal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tbPanal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbPanal.Size = new System.Drawing.Size(884, 322);
            this.tbPanal.TabIndex = 1;
            // 
            // uiDashBoard
            // 
            this.uiDashBoard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiDashBoard.Location = new System.Drawing.Point(3, 38);
            this.uiDashBoard.Name = "uiDashBoard";
            this.uiDashBoard.Padding = new System.Windows.Forms.Padding(14, 14, 0, 14);
            this.uiDashBoard.Size = new System.Drawing.Size(878, 281);
            this.uiDashBoard.TabIndex = 0;
            this.uiDashBoard.changeComplete += new BKvs2010.Usercontrols.UIDashBoard.ChangeDoctorComplete(this.uiDashBoard_ChangeDoctorComplete);
            this.uiDashBoard.ChangingDoctor += new BKvs2010.Usercontrols.UIDashBoard.changingDoctor(this.uiDashBoard_ChangingDoctor);
            this.uiDashBoard.Click += new System.EventHandler(this.uiDashBoard_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnPointMaster);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(878, 29);
            this.panel1.TabIndex = 1;
            // 
            // btnPointMaster
            // 
            this.btnPointMaster.Location = new System.Drawing.Point(729, 0);
            this.btnPointMaster.Name = "btnPointMaster";
            this.btnPointMaster.Size = new System.Drawing.Size(140, 29);
            this.btnPointMaster.TabIndex = 2;
            this.btnPointMaster.Text = "Set up master package";
            this.btnPointMaster.UseVisualStyleBackColor = true;
            this.btnPointMaster.Click += new System.EventHandler(this.btnPointMaster_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(123, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(600, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "(REQ) =Request ,(Req-S) = Request Same DR. , (Req-M) Request แพทย์ชาย,(Req-F) Req" +
    "uest แพทย์หญิง";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(3, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(108, 29);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // timerRefreshDashBoard
            // 
            this.timerRefreshDashBoard.Enabled = true;
            this.timerRefreshDashBoard.Interval = 10000;
            this.timerRefreshDashBoard.Tick += new System.EventHandler(this.timerRefreshDashBoard_Tick);
            // 
            // frmDashBoardcs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 322);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.tbPanal);
            this.Name = "frmDashBoardcs";
            this.Text = "frmDashBoardcs";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmDashBoardcs_FormClosed);
            this.Load += new System.EventHandler(this.frmDashBoardcs_Load);
            this.tbPanal.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Usercontrols.UIDashBoard uiDashBoard;
        private System.Windows.Forms.TableLayoutPanel tbPanal;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Timer timerRefreshDashBoard;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPointMaster;
    }
}