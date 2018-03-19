namespace BKvs2010
{
    partial class frmPE_OutQueue
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPE_OutQueue));
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.GV_Null_Result = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Coltprid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbTitleUI = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.UIProfileHorizontal1 = new BKvs2010.Usercontrols.UIProfileHorizontal();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.subjectiveUC1 = new BKvs2010.UserControlEMR.SubjectiveUC();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.objectiveUC1 = new BKvs2010.UserControlEMR.ObjectiveUC();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.panel7 = new System.Windows.Forms.Panel();
            this.tabAssessmentAndPlanUC1 = new BKvs2010.UserControlEMR.TabAssessmentAndPlanUC();
            this.lbAlertMsg = new UserControlLibrary.LabelAutoResizeFont();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSendAuto = new System.Windows.Forms.Button();
            this.btnSaveDraft = new System.Windows.Forms.Button();
            this.tableLayoutPanel5.SuspendLayout();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GV_Null_Result)).BeginInit();
            this.panel6.SuspendLayout();
            this.panel8.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.panel9, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.panel6, 1, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1349, 960);
            this.tableLayoutPanel5.TabIndex = 5;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.btnRefresh);
            this.panel9.Controls.Add(this.GV_Null_Result);
            this.panel9.Controls.Add(this.lbTitleUI);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(0, 0);
            this.panel9.Margin = new System.Windows.Forms.Padding(0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(300, 960);
            this.panel9.TabIndex = 266;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnRefresh.Location = new System.Drawing.Point(181, 5);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(118, 35);
            this.btnRefresh.TabIndex = 267;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // GV_Null_Result
            // 
            this.GV_Null_Result.AllowUserToAddRows = false;
            this.GV_Null_Result.AllowUserToDeleteRows = false;
            this.GV_Null_Result.BackgroundColor = System.Drawing.Color.White;
            this.GV_Null_Result.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GV_Null_Result.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GV_Null_Result.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GV_Null_Result.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.GV_Null_Result.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.EN,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.Coltprid});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GV_Null_Result.DefaultCellStyle = dataGridViewCellStyle2;
            this.GV_Null_Result.Location = new System.Drawing.Point(0, 64);
            this.GV_Null_Result.MultiSelect = false;
            this.GV_Null_Result.Name = "GV_Null_Result";
            this.GV_Null_Result.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GV_Null_Result.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.GV_Null_Result.RowHeadersVisible = false;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.GV_Null_Result.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.GV_Null_Result.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.GV_Null_Result.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GV_Null_Result.Size = new System.Drawing.Size(299, 896);
            this.GV_Null_Result.TabIndex = 266;
            this.GV_Null_Result.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GV_Null_Result_CellClick);
            this.GV_Null_Result.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.GV_Null_Result_DataBindingComplete);
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "No.";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 35;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "HN";
            this.dataGridViewTextBoxColumn6.HeaderText = "HN";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 80;
            // 
            // EN
            // 
            this.EN.DataPropertyName = "colEN";
            this.EN.HeaderText = "EN";
            this.EN.Name = "EN";
            this.EN.ReadOnly = true;
            this.EN.Visible = false;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "FullName";
            this.dataGridViewTextBoxColumn7.HeaderText = "ชื่อ-สกุล";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 150;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "ArriveDate";
            this.dataGridViewTextBoxColumn8.HeaderText = "วันที่ตรวจ";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Width = 80;
            // 
            // Coltprid
            // 
            this.Coltprid.DataPropertyName = "tprid";
            this.Coltprid.HeaderText = "tpr_id";
            this.Coltprid.Name = "Coltprid";
            this.Coltprid.ReadOnly = true;
            this.Coltprid.Visible = false;
            // 
            // lbTitleUI
            // 
            this.lbTitleUI.BackColor = System.Drawing.Color.RoyalBlue;
            this.lbTitleUI.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbTitleUI.ForeColor = System.Drawing.Color.White;
            this.lbTitleUI.Location = new System.Drawing.Point(0, 43);
            this.lbTitleUI.Name = "lbTitleUI";
            this.lbTitleUI.Size = new System.Drawing.Size(300, 21);
            this.lbTitleUI.TabIndex = 265;
            this.lbTitleUI.Text = "ผู้รับบริการที่หมอไม่ได้อ่านผล (ทั้งหมด x คน) ";
            this.lbTitleUI.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel6.Controls.Add(this.UIProfileHorizontal1);
            this.panel6.Controls.Add(this.panel8);
            this.panel6.Controls.Add(this.panel1);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(300, 0);
            this.panel6.Margin = new System.Windows.Forms.Padding(0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1049, 960);
            this.panel6.TabIndex = 3;
            // 
            // UIProfileHorizontal1
            // 
            this.UIProfileHorizontal1.BackColor = System.Drawing.SystemColors.Control;
            this.UIProfileHorizontal1.Dock = System.Windows.Forms.DockStyle.Top;
            this.UIProfileHorizontal1.Location = new System.Drawing.Point(0, 0);
            this.UIProfileHorizontal1.Margin = new System.Windows.Forms.Padding(0);
            this.UIProfileHorizontal1.Name = "UIProfileHorizontal1";
            this.UIProfileHorizontal1.Size = new System.Drawing.Size(1045, 122);
            this.UIProfileHorizontal1.TabIndex = 109;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.label11);
            this.panel8.Controls.Add(this.tabControl1);
            this.panel8.Controls.Add(this.lbAlertMsg);
            this.panel8.Location = new System.Drawing.Point(0, 120);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(1011, 805);
            this.panel8.TabIndex = 108;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label11.Location = new System.Drawing.Point(3, 7);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(163, 18);
            this.label11.TabIndex = 107;
            this.label11.Text = "Doctor (PE && Result)";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.tabControl1.Location = new System.Drawing.Point(0, 32);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1006, 770);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Tag = "History";
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.panel3);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(998, 741);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Tag = "1";
            this.tabPage1.Text = "Subjective";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.subjectiveUC1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(992, 735);
            this.panel3.TabIndex = 0;
            // 
            // subjectiveUC1
            // 
            this.subjectiveUC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.subjectiveUC1.Location = new System.Drawing.Point(0, 0);
            this.subjectiveUC1.Name = "subjectiveUC1";
            this.subjectiveUC1.PatientRegis = null;
            this.subjectiveUC1.Size = new System.Drawing.Size(992, 735);
            this.subjectiveUC1.TabIndex = 0;
            this.subjectiveUC1.username = null;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Transparent;
            this.tabPage2.Controls.Add(this.panel4);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(192, 71);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Tag = "2";
            this.tabPage2.Text = "Objective";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.objectiveUC1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(186, 65);
            this.panel4.TabIndex = 0;
            // 
            // objectiveUC1
            // 
            this.objectiveUC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectiveUC1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.objectiveUC1.Location = new System.Drawing.Point(0, 0);
            this.objectiveUC1.Name = "objectiveUC1";
            this.objectiveUC1.PatientRegis = null;
            this.objectiveUC1.Size = new System.Drawing.Size(186, 65);
            this.objectiveUC1.TabIndex = 0;
            this.objectiveUC1.username = null;
            // 
            // tabPage3
            // 
            this.tabPage3.AutoScroll = true;
            this.tabPage3.AutoScrollMargin = new System.Drawing.Size(0, 10);
            this.tabPage3.Controls.Add(this.panel7);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(192, 71);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Tag = "3";
            this.tabPage3.Text = "Assessment and Plan";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.tabAssessmentAndPlanUC1);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(3, 3);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(186, 65);
            this.panel7.TabIndex = 125;
            // 
            // tabAssessmentAndPlanUC1
            // 
            this.tabAssessmentAndPlanUC1.BackColor = System.Drawing.Color.Transparent;
            this.tabAssessmentAndPlanUC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabAssessmentAndPlanUC1.Location = new System.Drawing.Point(0, 0);
            this.tabAssessmentAndPlanUC1.Margin = new System.Windows.Forms.Padding(0);
            this.tabAssessmentAndPlanUC1.MinimumSize = new System.Drawing.Size(975, 650);
            this.tabAssessmentAndPlanUC1.Name = "tabAssessmentAndPlanUC1";
            this.tabAssessmentAndPlanUC1.PatientRegis = null;
            this.tabAssessmentAndPlanUC1.Size = new System.Drawing.Size(975, 650);
            this.tabAssessmentAndPlanUC1.TabIndex = 0;
            // 
            // lbAlertMsg
            // 
            this.lbAlertMsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbAlertMsg.AutoResizeFont = true;
            this.lbAlertMsg.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbAlertMsg.ForeColor = System.Drawing.Color.Red;
            this.lbAlertMsg.Location = new System.Drawing.Point(172, 5);
            this.lbAlertMsg.MaxFontSize = 11.5F;
            this.lbAlertMsg.Name = "lbAlertMsg";
            this.lbAlertMsg.Size = new System.Drawing.Size(819, 20);
            this.lbAlertMsg.TabIndex = 101;
            this.lbAlertMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSendAuto);
            this.panel1.Controls.Add(this.btnSaveDraft);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 927);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1045, 29);
            this.panel1.TabIndex = 3;
            // 
            // btnSendAuto
            // 
            this.btnSendAuto.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSendAuto.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSendAuto.ImageIndex = 11;
            this.btnSendAuto.Location = new System.Drawing.Point(147, 2);
            this.btnSendAuto.Name = "btnSendAuto";
            this.btnSendAuto.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnSendAuto.Size = new System.Drawing.Size(140, 25);
            this.btnSendAuto.TabIndex = 29;
            this.btnSendAuto.Text = "SAVE && CONFIRM";
            this.btnSendAuto.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSendAuto.UseVisualStyleBackColor = true;
            this.btnSendAuto.Click += new System.EventHandler(this.btnSendAuto_Click);
            // 
            // btnSaveDraft
            // 
            this.btnSaveDraft.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSaveDraft.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveDraft.ImageIndex = 5;
            this.btnSaveDraft.Location = new System.Drawing.Point(4, 2);
            this.btnSaveDraft.Name = "btnSaveDraft";
            this.btnSaveDraft.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnSaveDraft.Size = new System.Drawing.Size(140, 25);
            this.btnSaveDraft.TabIndex = 30;
            this.btnSaveDraft.Text = "SAVE AS DRAFT";
            this.btnSaveDraft.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSaveDraft.UseVisualStyleBackColor = true;
            this.btnSaveDraft.Click += new System.EventHandler(this.btnSaveDraft_Click);
            // 
            // frmPE_OutQueue
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(1240, 960);
            this.ClientSize = new System.Drawing.Size(1366, 742);
            this.Controls.Add(this.tableLayoutPanel5);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPE_OutQueue";
            this.Text = "Doctor (Result) PE";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmPE_Load);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GV_Null_Result)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        public System.Windows.Forms.Panel panel6;
        public System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Button btnSendAuto;
        public System.Windows.Forms.Button btnSaveDraft;
        public System.Windows.Forms.TabControl tabControl1;
        public System.Windows.Forms.TabPage tabPage1;
        public System.Windows.Forms.TabPage tabPage2;
        public System.Windows.Forms.TabPage tabPage3;
        public UserControlLibrary.LabelAutoResizeFont lbAlertMsg;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.DataGridView GV_Null_Result;
        private System.Windows.Forms.Label lbTitleUI;
        private System.Windows.Forms.Panel panel8;
        private Usercontrols.UIProfileHorizontal UIProfileHorizontal1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn EN;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Coltprid;
        private System.Windows.Forms.Panel panel3;
        private UserControlEMR.SubjectiveUC subjectiveUC1;
        private System.Windows.Forms.Panel panel4;
        private UserControlEMR.ObjectiveUC objectiveUC1;
        private UserControlEMR.TabAssessmentAndPlanUC tabAssessmentAndPlanUC1;

    }
}