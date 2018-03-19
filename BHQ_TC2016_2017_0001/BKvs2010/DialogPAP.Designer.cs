namespace BKvs2010
{
    partial class DialogPAP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogPAP));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabObstetricsCKUC1 = new BKvs2010.UserControlEMR.TabObstetricsCKUC();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbLang = new UserControlLibrary.ComboAutoDropDownWidth();
            this.btnSendToDocscan = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.lbAlertMsg = new UserControlLibrary.LabelAutoResizeFont();
            this.btnSave = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1004, 673);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabObstetricsCKUC1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(998, 627);
            this.panel1.TabIndex = 0;
            // 
            // tabObstetricsCKUC1
            // 
            this.tabObstetricsCKUC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabObstetricsCKUC1.isDoctor = false;
            this.tabObstetricsCKUC1.Location = new System.Drawing.Point(0, 0);
            this.tabObstetricsCKUC1.Name = "tabObstetricsCKUC1";
            this.tabObstetricsCKUC1.PatientRegis = null;
            this.tabObstetricsCKUC1.Size = new System.Drawing.Size(998, 627);
            this.tabObstetricsCKUC1.TabIndex = 0;
            this.tabObstetricsCKUC1.TabSelected = null;
            this.tabObstetricsCKUC1.user = null;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.cmbLang);
            this.panel2.Controls.Add(this.btnSendToDocscan);
            this.panel2.Controls.Add(this.btnPrint);
            this.panel2.Controls.Add(this.lbAlertMsg);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 636);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(998, 34);
            this.panel2.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(516, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 99;
            this.label2.Text = "Language :";
            // 
            // cmbLang
            // 
            this.cmbLang.DisplayMember = "Thai";
            this.cmbLang.FormattingEnabled = true;
            this.cmbLang.Items.AddRange(new object[] {
            "(TH)",
            "(ARB)",
            "(JAP)"});
            this.cmbLang.Location = new System.Drawing.Point(583, 8);
            this.cmbLang.Name = "cmbLang";
            this.cmbLang.RightClickDelete = true;
            this.cmbLang.Size = new System.Drawing.Size(66, 21);
            this.cmbLang.TabIndex = 98;
            // 
            // btnSendToDocscan
            // 
            this.btnSendToDocscan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendToDocscan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSendToDocscan.Image = ((System.Drawing.Image)(resources.GetObject("btnSendToDocscan.Image")));
            this.btnSendToDocscan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSendToDocscan.Location = new System.Drawing.Point(754, 3);
            this.btnSendToDocscan.Name = "btnSendToDocscan";
            this.btnSendToDocscan.Size = new System.Drawing.Size(121, 28);
            this.btnSendToDocscan.TabIndex = 97;
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
            this.btnPrint.Location = new System.Drawing.Point(878, 3);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnPrint.Size = new System.Drawing.Size(115, 28);
            this.btnPrint.TabIndex = 96;
            this.btnPrint.Text = "Print Preview";
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // lbAlertMsg
            // 
            this.lbAlertMsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbAlertMsg.AutoResizeFont = true;
            this.lbAlertMsg.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbAlertMsg.ForeColor = System.Drawing.Color.Red;
            this.lbAlertMsg.Location = new System.Drawing.Point(0, 2);
            this.lbAlertMsg.MaxFontSize = 11.5F;
            this.lbAlertMsg.Name = "lbAlertMsg";
            this.lbAlertMsg.Size = new System.Drawing.Size(486, 30);
            this.lbAlertMsg.TabIndex = 95;
            this.lbAlertMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(671, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 28);
            this.btnSave.TabIndex = 94;
            this.btnSave.Text = "Save Data";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // DialogPAP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 673);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(1012, 700);
            this.Name = "DialogPAP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dialog PAP";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSendToDocscan;
        private System.Windows.Forms.Button btnPrint;
        private UserControlLibrary.LabelAutoResizeFont lbAlertMsg;
        private System.Windows.Forms.Button btnSave;
        private UserControlEMR.TabObstetricsCKUC tabObstetricsCKUC1;
        private System.Windows.Forms.Label label2;
        private UserControlLibrary.ComboAutoDropDownWidth cmbLang;
    }
}