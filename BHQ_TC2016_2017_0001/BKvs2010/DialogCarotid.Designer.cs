namespace BKvs2010
{
    partial class DialogCarotid
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogCarotid));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSendToDocscan = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.lbAlertMsg = new UserControlLibrary.LabelAutoResizeFont();
            this.btnSave = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabCarotidUC1 = new BKvs2010.UserControlEMR.TabCarotidUC();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1124, 633);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.btnSendToDocscan);
            this.panel2.Controls.Add(this.btnPrint);
            this.panel2.Controls.Add(this.lbAlertMsg);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Location = new System.Drawing.Point(3, 594);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1118, 36);
            this.panel2.TabIndex = 1;
            // 
            // btnSendToDocscan
            // 
            this.btnSendToDocscan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendToDocscan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSendToDocscan.Image = ((System.Drawing.Image)(resources.GetObject("btnSendToDocscan.Image")));
            this.btnSendToDocscan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSendToDocscan.Location = new System.Drawing.Point(872, 5);
            this.btnSendToDocscan.Name = "btnSendToDocscan";
            this.btnSendToDocscan.Size = new System.Drawing.Size(125, 32);
            this.btnSendToDocscan.TabIndex = 95;
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
            this.btnPrint.Location = new System.Drawing.Point(1000, 5);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnPrint.Size = new System.Drawing.Size(118, 32);
            this.btnPrint.TabIndex = 94;
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
            this.lbAlertMsg.Location = new System.Drawing.Point(3, 6);
            this.lbAlertMsg.MaxFontSize = 11.5F;
            this.lbAlertMsg.Name = "lbAlertMsg";
            this.lbAlertMsg.Size = new System.Drawing.Size(768, 30);
            this.lbAlertMsg.TabIndex = 93;
            this.lbAlertMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(775, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(94, 32);
            this.btnSave.TabIndex = 92;
            this.btnSave.Text = "Save Data";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.tabCarotidUC1);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1118, 585);
            this.panel1.TabIndex = 0;
            // 
            // tabCarotidUC1
            // 
            this.tabCarotidUC1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabCarotidUC1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.tabCarotidUC1.Location = new System.Drawing.Point(0, 0);
            this.tabCarotidUC1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabCarotidUC1.Name = "tabCarotidUC1";
            this.tabCarotidUC1.PatientRegis = null;
            this.tabCarotidUC1.Size = new System.Drawing.Size(1118, 585);
            this.tabCarotidUC1.TabIndex = 0;
            this.tabCarotidUC1.TabSelected = null;
            // 
            // DialogCarotid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1124, 633);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "DialogCarotid";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DialogCarotid";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSendToDocscan;
        private System.Windows.Forms.Button btnPrint;
        private UserControlLibrary.LabelAutoResizeFont lbAlertMsg;
        private System.Windows.Forms.Button btnSave;
        private UserControlEMR.TabCarotidUC tabCarotidUC1;
    }
}