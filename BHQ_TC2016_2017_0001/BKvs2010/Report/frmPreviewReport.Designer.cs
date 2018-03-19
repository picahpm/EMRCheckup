namespace BKvs2010.Report
{
    partial class frmPreviewReport
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnPrintAll = new System.Windows.Forms.Button();
            this.btnMoveLast = new System.Windows.Forms.Button();
            this.btnMoveNext = new System.Windows.Forms.Button();
            this.btnMoveBack = new System.Windows.Forms.Button();
            this.btnMoveFirst = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblReportName = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.crystalReportViewer1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1090, 504);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.DisplayStatusBar = false;
            this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewer1.EnableDrillDown = false;
            this.crystalReportViewer1.Location = new System.Drawing.Point(3, 34);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.ShowCloseButton = false;
            this.crystalReportViewer1.ShowExportButton = false;
            this.crystalReportViewer1.ShowGroupTreeButton = false;
            this.crystalReportViewer1.ShowParameterPanelButton = false;
            this.crystalReportViewer1.Size = new System.Drawing.Size(1084, 427);
            this.crystalReportViewer1.TabIndex = 0;
            this.crystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 561F));
            this.tableLayoutPanel2.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 467);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1084, 34);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnPrintAll);
            this.panel1.Controls.Add(this.btnMoveLast);
            this.panel1.Controls.Add(this.btnMoveNext);
            this.panel1.Controls.Add(this.btnMoveBack);
            this.panel1.Controls.Add(this.btnMoveFirst);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(526, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(555, 28);
            this.panel1.TabIndex = 0;
            // 
            // btnPrintAll
            // 
            this.btnPrintAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnPrintAll.Location = new System.Drawing.Point(439, 2);
            this.btnPrintAll.Name = "btnPrintAll";
            this.btnPrintAll.Size = new System.Drawing.Size(114, 23);
            this.btnPrintAll.TabIndex = 15;
            this.btnPrintAll.Text = "Print All Reports";
            this.btnPrintAll.UseVisualStyleBackColor = true;
            this.btnPrintAll.Click += new System.EventHandler(this.btnPrintAll_Click);
            // 
            // btnMoveLast
            // 
            this.btnMoveLast.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoveLast.Location = new System.Drawing.Point(375, 2);
            this.btnMoveLast.Name = "btnMoveLast";
            this.btnMoveLast.Size = new System.Drawing.Size(58, 23);
            this.btnMoveLast.TabIndex = 14;
            this.btnMoveLast.Text = ">>";
            this.btnMoveLast.UseVisualStyleBackColor = true;
            this.btnMoveLast.Click += new System.EventHandler(this.btnMoveLast_Click);
            // 
            // btnMoveNext
            // 
            this.btnMoveNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoveNext.Location = new System.Drawing.Point(311, 2);
            this.btnMoveNext.Name = "btnMoveNext";
            this.btnMoveNext.Size = new System.Drawing.Size(58, 23);
            this.btnMoveNext.TabIndex = 13;
            this.btnMoveNext.Text = ">";
            this.btnMoveNext.UseVisualStyleBackColor = true;
            this.btnMoveNext.Click += new System.EventHandler(this.btnMoveNext_Click);
            // 
            // btnMoveBack
            // 
            this.btnMoveBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoveBack.Location = new System.Drawing.Point(247, 2);
            this.btnMoveBack.Name = "btnMoveBack";
            this.btnMoveBack.Size = new System.Drawing.Size(58, 23);
            this.btnMoveBack.TabIndex = 12;
            this.btnMoveBack.Text = "<";
            this.btnMoveBack.UseVisualStyleBackColor = true;
            this.btnMoveBack.Click += new System.EventHandler(this.btnMoveBack_Click);
            // 
            // btnMoveFirst
            // 
            this.btnMoveFirst.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoveFirst.Location = new System.Drawing.Point(183, 2);
            this.btnMoveFirst.Name = "btnMoveFirst";
            this.btnMoveFirst.Size = new System.Drawing.Size(58, 23);
            this.btnMoveFirst.TabIndex = 11;
            this.btnMoveFirst.Text = "<<";
            this.btnMoveFirst.UseVisualStyleBackColor = true;
            this.btnMoveFirst.Click += new System.EventHandler(this.btnMoveFirst_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblReportName);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1084, 25);
            this.panel2.TabIndex = 2;
            // 
            // lblReportName
            // 
            this.lblReportName.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblReportName.Location = new System.Drawing.Point(2, 5);
            this.lblReportName.Name = "lblReportName";
            this.lblReportName.Size = new System.Drawing.Size(1079, 16);
            this.lblReportName.TabIndex = 0;
            // 
            // frmPreviewReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1090, 504);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "frmPreviewReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmPreviewReport";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmPreviewReport_FormClosed);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnPrintAll;
        private System.Windows.Forms.Button btnMoveLast;
        private System.Windows.Forms.Button btnMoveNext;
        private System.Windows.Forms.Button btnMoveBack;
        private System.Windows.Forms.Button btnMoveFirst;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblReportName;
    }
}