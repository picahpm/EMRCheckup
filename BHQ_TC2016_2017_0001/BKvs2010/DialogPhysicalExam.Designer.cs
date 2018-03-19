using System;

namespace BKvs2010
{
    partial class DialogPhysicalExam
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogPhysicalExam));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSendToDocscan = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lbAlertMsg = new UserControlLibrary.LabelAutoResizeFont();
            this.btnSave = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.RetrieveXrayBtn = new System.Windows.Forms.Button();
            this.btnRetreiveLab = new System.Windows.Forms.Button();
            this.btnRetrieveVS = new System.Windows.Forms.Button();
            this.btnHealth = new System.Windows.Forms.Button();
            this.btnAssessment = new System.Windows.Forms.Button();
            this.tabPhyExamUC1 = new BKvs2010.UserControlEMR.TabPhyExamUC();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1195, 645);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnSendToDocscan);
            this.panel2.Controls.Add(this.btnPrint);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Controls.Add(this.lbAlertMsg);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 603);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1195, 42);
            this.panel2.TabIndex = 1;
            // 
            // btnSendToDocscan
            // 
            this.btnSendToDocscan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendToDocscan.Enabled = false;
            this.btnSendToDocscan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSendToDocscan.Image = ((System.Drawing.Image)(resources.GetObject("btnSendToDocscan.Image")));
            this.btnSendToDocscan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSendToDocscan.Location = new System.Drawing.Point(939, 5);
            this.btnSendToDocscan.Name = "btnSendToDocscan";
            this.btnSendToDocscan.Size = new System.Drawing.Size(125, 32);
            this.btnSendToDocscan.TabIndex = 89;
            this.btnSendToDocscan.Text = "Send To Doc Scan";
            this.btnSendToDocscan.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSendToDocscan.UseVisualStyleBackColor = true;
            this.btnSendToDocscan.Click += new System.EventHandler(this.btnSendToDocscan_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.Location = new System.Drawing.Point(1070, 5);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnPrint.Size = new System.Drawing.Size(118, 32);
            this.btnPrint.TabIndex = 88;
            this.btnPrint.Text = "Print Preview";
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(7, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(68, 32);
            this.btnClose.TabIndex = 87;
            this.btnClose.Text = "Cancel";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lbAlertMsg
            // 
            this.lbAlertMsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbAlertMsg.AutoResizeFont = true;
            this.lbAlertMsg.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbAlertMsg.ForeColor = System.Drawing.Color.Red;
            this.lbAlertMsg.Location = new System.Drawing.Point(83, 5);
            this.lbAlertMsg.MaxFontSize = 11.5F;
            this.lbAlertMsg.Name = "lbAlertMsg";
            this.lbAlertMsg.Size = new System.Drawing.Size(570, 30);
            this.lbAlertMsg.TabIndex = 86;
            this.lbAlertMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(839, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(94, 32);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save Data";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabPhyExamUC1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 42);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1195, 561);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.RetrieveXrayBtn);
            this.panel3.Controls.Add(this.btnRetreiveLab);
            this.panel3.Controls.Add(this.btnRetrieveVS);
            this.panel3.Controls.Add(this.btnHealth);
            this.panel3.Controls.Add(this.btnAssessment);
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1185, 36);
            this.panel3.TabIndex = 2;
            // 
            // RetrieveXrayBtn
            // 
            this.RetrieveXrayBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RetrieveXrayBtn.Location = new System.Drawing.Point(314, 2);
            this.RetrieveXrayBtn.Name = "RetrieveXrayBtn";
            this.RetrieveXrayBtn.Size = new System.Drawing.Size(91, 32);
            this.RetrieveXrayBtn.TabIndex = 232;
            this.RetrieveXrayBtn.Text = "Retreive X-Ray";
            this.RetrieveXrayBtn.UseVisualStyleBackColor = true;
            this.RetrieveXrayBtn.Click += new System.EventHandler(this.RetrieveXrayBtn_Click);
            // 
            // btnRetreiveLab
            // 
            this.btnRetreiveLab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRetreiveLab.Location = new System.Drawing.Point(197, 2);
            this.btnRetreiveLab.Name = "btnRetreiveLab";
            this.btnRetreiveLab.Size = new System.Drawing.Size(111, 32);
            this.btnRetreiveLab.TabIndex = 231;
            this.btnRetreiveLab.Text = "Retreive Lab Result";
            this.btnRetreiveLab.UseVisualStyleBackColor = true;
            this.btnRetreiveLab.Click += new System.EventHandler(this.btnRetreiveLab_Click);
            // 
            // btnRetrieveVS
            // 
            this.btnRetrieveVS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRetrieveVS.Location = new System.Drawing.Point(100, 2);
            this.btnRetrieveVS.Name = "btnRetrieveVS";
            this.btnRetrieveVS.Size = new System.Drawing.Size(91, 32);
            this.btnRetrieveVS.TabIndex = 230;
            this.btnRetrieveVS.Text = "Retreive V/S";
            this.btnRetrieveVS.UseVisualStyleBackColor = true;
            this.btnRetrieveVS.Click += new System.EventHandler(this.btnRetrieveVS_Click);
            // 
            // btnHealth
            // 
            this.btnHealth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHealth.Location = new System.Drawing.Point(3, 2);
            this.btnHealth.Name = "btnHealth";
            this.btnHealth.Size = new System.Drawing.Size(91, 32);
            this.btnHealth.TabIndex = 229;
            this.btnHealth.Text = "Health History";
            this.btnHealth.UseVisualStyleBackColor = true;
            this.btnHealth.Click += new System.EventHandler(this.btnHealth_Click);
            // 
            // btnAssessment
            // 
            this.btnAssessment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAssessment.Location = new System.Drawing.Point(411, 2);
            this.btnAssessment.Name = "btnAssessment";
            this.btnAssessment.Size = new System.Drawing.Size(122, 32);
            this.btnAssessment.TabIndex = 228;
            this.btnAssessment.Text = "Assessment and Plan";
            this.btnAssessment.UseVisualStyleBackColor = true;
            this.btnAssessment.Click += new System.EventHandler(this.btnAssessment_Click);
            // 
            // tabPhyExamUC1
            // 
            this.tabPhyExamUC1.AutoScroll = true;
            this.tabPhyExamUC1.BackColor = System.Drawing.Color.Transparent;
            this.tabPhyExamUC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPhyExamUC1.IsENGPrt = false;
            this.tabPhyExamUC1.Location = new System.Drawing.Point(0, 0);
            this.tabPhyExamUC1.Name = "tabPhyExamUC1";
            this.tabPhyExamUC1.PatientRegis = null;
            this.tabPhyExamUC1.Size = new System.Drawing.Size(1195, 561);
            this.tabPhyExamUC1.TabIndex = 0;
            this.tabPhyExamUC1.TabSelected = null;
            // 
            // DialogPhysicalExam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1195, 645);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(1203, 672);
            this.Name = "DialogPhysicalExam";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DialogPhysicalExam";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private UserControlEMR.TabPhyExamUC tabPhyExamUC1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSendToDocscan;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnClose;
        private UserControlLibrary.LabelAutoResizeFont lbAlertMsg;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button RetrieveXrayBtn;
        private System.Windows.Forms.Button btnRetreiveLab;
        private System.Windows.Forms.Button btnRetrieveVS;
        private System.Windows.Forms.Button btnHealth;
        private System.Windows.Forms.Button btnAssessment;
    }
}