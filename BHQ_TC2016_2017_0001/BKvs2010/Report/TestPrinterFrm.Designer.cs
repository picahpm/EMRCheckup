namespace BKvs2010.Report
{
    partial class TestPrinterFrm
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
            this.comboAutoDropDownWidth1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboAutoDropDownWidth2 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // comboAutoDropDownWidth1
            // 
            this.comboAutoDropDownWidth1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboAutoDropDownWidth1.FormattingEnabled = true;
            this.comboAutoDropDownWidth1.Location = new System.Drawing.Point(92, 12);
            this.comboAutoDropDownWidth1.Name = "comboAutoDropDownWidth1";
            this.comboAutoDropDownWidth1.Size = new System.Drawing.Size(410, 21);
            this.comboAutoDropDownWidth1.TabIndex = 0;
            this.comboAutoDropDownWidth1.SelectionChangeCommitted += new System.EventHandler(this.comboAutoDropDownWidth1_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Printer Name :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Paper Size :";
            // 
            // comboAutoDropDownWidth2
            // 
            this.comboAutoDropDownWidth2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboAutoDropDownWidth2.FormattingEnabled = true;
            this.comboAutoDropDownWidth2.Location = new System.Drawing.Point(92, 39);
            this.comboAutoDropDownWidth2.Name = "comboAutoDropDownWidth2";
            this.comboAutoDropDownWidth2.Size = new System.Drawing.Size(410, 21);
            this.comboAutoDropDownWidth2.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 265);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 24);
            this.button1.TabIndex = 4;
            this.button1.Text = "Test Print Sticker";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(397, 265);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(105, 24);
            this.button2.TabIndex = 5;
            this.button2.Text = "Setup Printer";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.EnableDrillDown = false;
            this.crystalReportViewer1.Location = new System.Drawing.Point(15, 66);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.ShowCloseButton = false;
            this.crystalReportViewer1.ShowCopyButton = false;
            this.crystalReportViewer1.ShowExportButton = false;
            this.crystalReportViewer1.ShowGotoPageButton = false;
            this.crystalReportViewer1.ShowGroupTreeButton = false;
            this.crystalReportViewer1.ShowPageNavigateButtons = false;
            this.crystalReportViewer1.ShowParameterPanelButton = false;
            this.crystalReportViewer1.ShowRefreshButton = false;
            this.crystalReportViewer1.ShowTextSearchButton = false;
            this.crystalReportViewer1.ShowZoomButton = false;
            this.crystalReportViewer1.Size = new System.Drawing.Size(487, 193);
            this.crystalReportViewer1.TabIndex = 7;
            this.crystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // TestPrinterFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 301);
            this.Controls.Add(this.crystalReportViewer1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboAutoDropDownWidth2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboAutoDropDownWidth1);
            this.Name = "TestPrinterFrm";
            this.Text = "TestPrinterFrm";
            this.Load += new System.EventHandler(this.TestPrinterFrm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboAutoDropDownWidth1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboAutoDropDownWidth2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
    }
}