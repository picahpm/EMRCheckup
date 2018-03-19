namespace BKvs2010
{
    partial class frmLabProfile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLabProfile));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnother = new System.Windows.Forms.Button();
            this.btnStoolExamination = new System.Windows.Forms.Button();
            this.btnUrineExamination = new System.Windows.Forms.Button();
            this.btnImmunology = new System.Windows.Forms.Button();
            this.btnBiochemistry = new System.Windows.Forms.Button();
            this.btnHeamatology = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.panelSummary = new System.Windows.Forms.Panel();
            this.rdAB = new System.Windows.Forms.RadioButton();
            this.rdN = new System.Windows.Forms.RadioButton();
            this.lbAlertMsg = new UserControlLibrary.LabelAutoResizeFont();
            this.btnSaveAsDraft = new System.Windows.Forms.Button();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.PatientAssHdrbindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.txtpatientEduction = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.gvLabProfile = new System.Windows.Forms.DataGridView();
            this.colNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colItemSet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTestItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColResult = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSummary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtLabDate = new System.Windows.Forms.TextBox();
            this.txtLabNumber = new System.Windows.Forms.TextBox();
            this.txtPaAge = new System.Windows.Forms.TextBox();
            this.txtOrderCode = new System.Windows.Forms.TextBox();
            this.lblHlab = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.uiProfileHorizontal1 = new BKvs2010.Usercontrols.UIProfileHorizontal();
            this.panel1.SuspendLayout();
            this.panelSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PatientAssHdrbindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLabProfile)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnother);
            this.panel1.Controls.Add(this.btnStoolExamination);
            this.panel1.Controls.Add(this.btnUrineExamination);
            this.panel1.Controls.Add(this.btnImmunology);
            this.panel1.Controls.Add(this.btnBiochemistry);
            this.panel1.Controls.Add(this.btnHeamatology);
            this.panel1.Controls.Add(this.btnNext);
            this.panel1.Controls.Add(this.btnPrevious);
            this.panel1.Controls.Add(this.panelSummary);
            this.panel1.Controls.Add(this.lbAlertMsg);
            this.panel1.Controls.Add(this.btnSaveAsDraft);
            this.panel1.Controls.Add(this.textBox13);
            this.panel1.Controls.Add(this.txtpatientEduction);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.gvLabProfile);
            this.panel1.Controls.Add(this.txtLabDate);
            this.panel1.Controls.Add(this.txtLabNumber);
            this.panel1.Controls.Add(this.txtPaAge);
            this.panel1.Controls.Add(this.txtOrderCode);
            this.panel1.Controls.Add(this.lblHlab);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 127);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(978, 602);
            this.panel1.TabIndex = 0;
            // 
            // btnother
            // 
            this.btnother.Enabled = false;
            this.btnother.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnother.Location = new System.Drawing.Point(644, 4);
            this.btnother.Name = "btnother";
            this.btnother.Size = new System.Drawing.Size(139, 25);
            this.btnother.TabIndex = 47;
            this.btnother.Text = "Other Lab";
            this.btnother.UseVisualStyleBackColor = true;
            this.btnother.Click += new System.EventHandler(this.btnother_Click);
            // 
            // btnStoolExamination
            // 
            this.btnStoolExamination.Enabled = false;
            this.btnStoolExamination.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnStoolExamination.Location = new System.Drawing.Point(499, 4);
            this.btnStoolExamination.Name = "btnStoolExamination";
            this.btnStoolExamination.Size = new System.Drawing.Size(139, 25);
            this.btnStoolExamination.TabIndex = 47;
            this.btnStoolExamination.Text = "Stool Examination";
            this.btnStoolExamination.UseVisualStyleBackColor = true;
            this.btnStoolExamination.Click += new System.EventHandler(this.btnStoolExamination_Click);
            // 
            // btnUrineExamination
            // 
            this.btnUrineExamination.Enabled = false;
            this.btnUrineExamination.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnUrineExamination.Location = new System.Drawing.Point(357, 4);
            this.btnUrineExamination.Name = "btnUrineExamination";
            this.btnUrineExamination.Size = new System.Drawing.Size(136, 25);
            this.btnUrineExamination.TabIndex = 47;
            this.btnUrineExamination.Text = "Urine Examination";
            this.btnUrineExamination.UseVisualStyleBackColor = true;
            this.btnUrineExamination.Click += new System.EventHandler(this.btnUrineExamination_Click);
            // 
            // btnImmunology
            // 
            this.btnImmunology.Enabled = false;
            this.btnImmunology.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnImmunology.Location = new System.Drawing.Point(241, 4);
            this.btnImmunology.Name = "btnImmunology";
            this.btnImmunology.Size = new System.Drawing.Size(110, 25);
            this.btnImmunology.TabIndex = 47;
            this.btnImmunology.Text = "Immunology";
            this.btnImmunology.UseVisualStyleBackColor = true;
            this.btnImmunology.Click += new System.EventHandler(this.btnImmunology_Click);
            // 
            // btnBiochemistry
            // 
            this.btnBiochemistry.Enabled = false;
            this.btnBiochemistry.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnBiochemistry.Location = new System.Drawing.Point(125, 4);
            this.btnBiochemistry.Name = "btnBiochemistry";
            this.btnBiochemistry.Size = new System.Drawing.Size(110, 25);
            this.btnBiochemistry.TabIndex = 47;
            this.btnBiochemistry.Text = "Biochemistry";
            this.btnBiochemistry.UseVisualStyleBackColor = true;
            this.btnBiochemistry.Click += new System.EventHandler(this.btnBiochemistry_Click);
            // 
            // btnHeamatology
            // 
            this.btnHeamatology.Enabled = false;
            this.btnHeamatology.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnHeamatology.Location = new System.Drawing.Point(9, 4);
            this.btnHeamatology.Name = "btnHeamatology";
            this.btnHeamatology.Size = new System.Drawing.Size(110, 25);
            this.btnHeamatology.TabIndex = 47;
            this.btnHeamatology.Text = "Haematology";
            this.btnHeamatology.UseVisualStyleBackColor = true;
            this.btnHeamatology.Click += new System.EventHandler(this.btnHeamatology_Click);
            // 
            // btnNext
            // 
            this.btnNext.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnNext.Location = new System.Drawing.Point(880, 3);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(95, 23);
            this.btnNext.TabIndex = 46;
            this.btnNext.Text = "Next >>>";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Visible = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnPrevious.Location = new System.Drawing.Point(818, 4);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(104, 23);
            this.btnPrevious.TabIndex = 45;
            this.btnPrevious.Text = "<<<Previous";
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Visible = false;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // panelSummary
            // 
            this.panelSummary.Controls.Add(this.rdAB);
            this.panelSummary.Controls.Add(this.rdN);
            this.panelSummary.Location = new System.Drawing.Point(159, 418);
            this.panelSummary.Name = "panelSummary";
            this.panelSummary.Size = new System.Drawing.Size(200, 25);
            this.panelSummary.TabIndex = 4;
            // 
            // rdAB
            // 
            this.rdAB.AutoSize = true;
            this.rdAB.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdAB.Location = new System.Drawing.Point(87, 3);
            this.rdAB.Name = "rdAB";
            this.rdAB.Size = new System.Drawing.Size(81, 20);
            this.rdAB.TabIndex = 28;
            this.rdAB.TabStop = true;
            this.rdAB.Tag = "A";
            this.rdAB.Text = "Abnormal";
            this.rdAB.UseVisualStyleBackColor = true;
            // 
            // rdN
            // 
            this.rdN.AutoSize = true;
            this.rdN.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdN.Location = new System.Drawing.Point(3, 3);
            this.rdN.Name = "rdN";
            this.rdN.Size = new System.Drawing.Size(67, 20);
            this.rdN.TabIndex = 27;
            this.rdN.TabStop = true;
            this.rdN.Tag = "N";
            this.rdN.Text = "Normal";
            this.rdN.UseVisualStyleBackColor = true;
            // 
            // lbAlertMsg
            // 
            this.lbAlertMsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbAlertMsg.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbAlertMsg.ForeColor = System.Drawing.Color.Red;
            this.lbAlertMsg.Location = new System.Drawing.Point(232, 573);
            this.lbAlertMsg.Name = "lbAlertMsg";
            this.lbAlertMsg.Size = new System.Drawing.Size(736, 23);
            this.lbAlertMsg.TabIndex = 44;
            this.lbAlertMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSaveAsDraft
            // 
            this.btnSaveAsDraft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveAsDraft.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.btnSaveAsDraft.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveAsDraft.Image")));
            this.btnSaveAsDraft.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveAsDraft.Location = new System.Drawing.Point(159, 574);
            this.btnSaveAsDraft.Name = "btnSaveAsDraft";
            this.btnSaveAsDraft.Size = new System.Drawing.Size(67, 25);
            this.btnSaveAsDraft.TabIndex = 7;
            this.btnSaveAsDraft.Text = "Save";
            this.btnSaveAsDraft.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSaveAsDraft.UseVisualStyleBackColor = true;
            this.btnSaveAsDraft.Click += new System.EventHandler(this.btnSaveAsDraft_Click);
            // 
            // textBox13
            // 
            this.textBox13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox13.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.PatientAssHdrbindingSource1, "tpeh_remark", true));
            this.textBox13.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.textBox13.Location = new System.Drawing.Point(158, 508);
            this.textBox13.Multiline = true;
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new System.Drawing.Size(810, 60);
            this.textBox13.TabIndex = 6;
            // 
            // PatientAssHdrbindingSource1
            // 
            this.PatientAssHdrbindingSource1.DataSource = typeof(DBCheckup.trn_patient_ass_hdr);
            this.PatientAssHdrbindingSource1.CurrentChanged += new System.EventHandler(this.PatientAssHdrbindingSource1_CurrentChanged);
            // 
            // txtpatientEduction
            // 
            this.txtpatientEduction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtpatientEduction.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtpatientEduction.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.PatientAssHdrbindingSource1, "tpeh_pat_education", true));
            this.txtpatientEduction.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtpatientEduction.Location = new System.Drawing.Point(158, 445);
            this.txtpatientEduction.Multiline = true;
            this.txtpatientEduction.Name = "txtpatientEduction";
            this.txtpatientEduction.Size = new System.Drawing.Size(810, 60);
            this.txtpatientEduction.TabIndex = 5;
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label16.BackColor = System.Drawing.Color.LightGreen;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label16.ForeColor = System.Drawing.Color.Black;
            this.label16.Location = new System.Drawing.Point(9, 445);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(130, 20);
            this.label16.TabIndex = 26;
            this.label16.Text = "Patient Education";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label17.BackColor = System.Drawing.Color.LightGreen;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label17.ForeColor = System.Drawing.Color.Black;
            this.label17.Location = new System.Drawing.Point(10, 508);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(130, 20);
            this.label17.TabIndex = 25;
            this.label17.Text = "Remark";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label15.BackColor = System.Drawing.Color.LightGreen;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(9, 421);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(130, 20);
            this.label15.TabIndex = 20;
            this.label15.Text = "Summary";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gvLabProfile
            // 
            this.gvLabProfile.AllowUserToAddRows = false;
            this.gvLabProfile.AllowUserToDeleteRows = false;
            this.gvLabProfile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvLabProfile.BackgroundColor = System.Drawing.Color.White;
            this.gvLabProfile.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvLabProfile.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gvLabProfile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gvLabProfile.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNo,
            this.colItemSet,
            this.colTestItem,
            this.ColItemName,
            this.colValue,
            this.Column2,
            this.colUnit,
            this.ColResult,
            this.colSummary});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvLabProfile.DefaultCellStyle = dataGridViewCellStyle6;
            this.gvLabProfile.Location = new System.Drawing.Point(9, 83);
            this.gvLabProfile.Name = "gvLabProfile";
            this.gvLabProfile.ReadOnly = true;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvLabProfile.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.gvLabProfile.RowHeadersVisible = false;
            this.gvLabProfile.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.gvLabProfile.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.gvLabProfile.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.gvLabProfile.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.gvLabProfile.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvLabProfile.Size = new System.Drawing.Size(960, 332);
            this.gvLabProfile.TabIndex = 17;
            this.gvLabProfile.Sorted += new System.EventHandler(this.dataGridView1_Sorted);
            // 
            // colNo
            // 
            this.colNo.DataPropertyName = "No";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colNo.DefaultCellStyle = dataGridViewCellStyle2;
            this.colNo.HeaderText = "No.";
            this.colNo.Name = "colNo";
            this.colNo.ReadOnly = true;
            this.colNo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colNo.Width = 30;
            // 
            // colItemSet
            // 
            this.colItemSet.DataPropertyName = "ItemSet";
            this.colItemSet.HeaderText = "Item Set";
            this.colItemSet.Name = "colItemSet";
            this.colItemSet.ReadOnly = true;
            this.colItemSet.Width = 150;
            // 
            // colTestItem
            // 
            this.colTestItem.DataPropertyName = "ItemCode";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.colTestItem.DefaultCellStyle = dataGridViewCellStyle3;
            this.colTestItem.HeaderText = "Item Code";
            this.colTestItem.Name = "colTestItem";
            this.colTestItem.ReadOnly = true;
            this.colTestItem.Visible = false;
            this.colTestItem.Width = 80;
            // 
            // ColItemName
            // 
            this.ColItemName.DataPropertyName = "ItemName";
            this.ColItemName.HeaderText = "Item Name";
            this.ColItemName.Name = "ColItemName";
            this.ColItemName.ReadOnly = true;
            this.ColItemName.Width = 200;
            // 
            // colValue
            // 
            this.colValue.DataPropertyName = "Value";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colValue.DefaultCellStyle = dataGridViewCellStyle4;
            this.colValue.HeaderText = "Value";
            this.colValue.Name = "colValue";
            this.colValue.ReadOnly = true;
            this.colValue.Width = 70;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "NRange";
            this.Column2.HeaderText = "Normal Range";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 150;
            // 
            // colUnit
            // 
            this.colUnit.DataPropertyName = "Units";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colUnit.DefaultCellStyle = dataGridViewCellStyle5;
            this.colUnit.HeaderText = "Units";
            this.colUnit.Name = "colUnit";
            this.colUnit.ReadOnly = true;
            // 
            // ColResult
            // 
            this.ColResult.DataPropertyName = "LabResult";
            this.ColResult.HeaderText = "Result";
            this.ColResult.Name = "ColResult";
            this.ColResult.ReadOnly = true;
            this.ColResult.Width = 390;
            // 
            // colSummary
            // 
            this.colSummary.DataPropertyName = "Summary";
            this.colSummary.HeaderText = "Summary";
            this.colSummary.Name = "colSummary";
            this.colSummary.ReadOnly = true;
            this.colSummary.Width = 65;
            // 
            // txtLabDate
            // 
            this.txtLabDate.BackColor = System.Drawing.Color.White;
            this.txtLabDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLabDate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtLabDate.Location = new System.Drawing.Point(307, 54);
            this.txtLabDate.Name = "txtLabDate";
            this.txtLabDate.ReadOnly = true;
            this.txtLabDate.Size = new System.Drawing.Size(133, 23);
            this.txtLabDate.TabIndex = 1;
            // 
            // txtLabNumber
            // 
            this.txtLabNumber.BackColor = System.Drawing.Color.White;
            this.txtLabNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLabNumber.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.PatientAssHdrbindingSource1, "tpeh_lab_number", true));
            this.txtLabNumber.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtLabNumber.Location = new System.Drawing.Point(536, 54);
            this.txtLabNumber.Name = "txtLabNumber";
            this.txtLabNumber.ReadOnly = true;
            this.txtLabNumber.Size = new System.Drawing.Size(149, 23);
            this.txtLabNumber.TabIndex = 2;
            // 
            // txtPaAge
            // 
            this.txtPaAge.BackColor = System.Drawing.Color.White;
            this.txtPaAge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPaAge.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.PatientAssHdrbindingSource1, "tpeh_age_of_test", true));
            this.txtPaAge.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtPaAge.Location = new System.Drawing.Point(827, 54);
            this.txtPaAge.Name = "txtPaAge";
            this.txtPaAge.ReadOnly = true;
            this.txtPaAge.Size = new System.Drawing.Size(141, 23);
            this.txtPaAge.TabIndex = 3;
            // 
            // txtOrderCode
            // 
            this.txtOrderCode.BackColor = System.Drawing.Color.White;
            this.txtOrderCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOrderCode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.PatientAssHdrbindingSource1, "tpeh_order_code", true));
            this.txtOrderCode.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtOrderCode.Location = new System.Drawing.Point(92, 54);
            this.txtOrderCode.Name = "txtOrderCode";
            this.txtOrderCode.ReadOnly = true;
            this.txtOrderCode.Size = new System.Drawing.Size(142, 23);
            this.txtOrderCode.TabIndex = 0;
            // 
            // lblHlab
            // 
            this.lblHlab.BackColor = System.Drawing.SystemColors.HotTrack;
            this.lblHlab.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblHlab.ForeColor = System.Drawing.Color.Transparent;
            this.lblHlab.Location = new System.Drawing.Point(0, 31);
            this.lblHlab.Name = "lblHlab";
            this.lblHlab.Size = new System.Drawing.Size(978, 20);
            this.lblHlab.TabIndex = 8;
            this.lblHlab.Text = "Lab Profile (Glu, Chol, Tri, HDL, LDL)";
            this.lblHlab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.LightGreen;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(691, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(130, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "Age at Time of Test";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.LightGreen;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(240, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Lab Date";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.LightGreen;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(445, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Lab Number";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.LightGreen;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(10, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Order Code";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.uiProfileHorizontal1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 124F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(984, 732);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // uiProfileHorizontal1
            // 
            this.uiProfileHorizontal1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(113)))), ((int)(((byte)(188)))));
            this.uiProfileHorizontal1.Location = new System.Drawing.Point(3, 3);
            this.uiProfileHorizontal1.Name = "uiProfileHorizontal1";
            this.uiProfileHorizontal1.Size = new System.Drawing.Size(978, 117);
            this.uiProfileHorizontal1.TabIndex = 1;
            // 
            // frmLabProfile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 732);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "frmLabProfile";
            this.Text = "ระบบ EMR Check-up Pathway - Lab profile";
            this.Load += new System.EventHandler(this.frmLabProfile_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelSummary.ResumeLayout(false);
            this.panelSummary.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PatientAssHdrbindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLabProfile)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtLabDate;
        private System.Windows.Forms.TextBox txtPaAge;
        private System.Windows.Forms.TextBox txtOrderCode;
        private System.Windows.Forms.TextBox txtLabNumber;
        private System.Windows.Forms.Label lblHlab;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private BKvs2010.Usercontrols.UIProfileHorizontal uiProfileHorizontal1;
        private System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.TextBox txtpatientEduction;
        private System.Windows.Forms.RadioButton rdAB;
        private System.Windows.Forms.RadioButton rdN;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DataGridView gvLabProfile;
        private System.Windows.Forms.Button btnSaveAsDraft;
        private System.Windows.Forms.BindingSource PatientAssHdrbindingSource1;
        private UserControlLibrary.LabelAutoResizeFont lbAlertMsg;
        private System.Windows.Forms.Panel panelSummary;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItemSet;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTestItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSummary;
        private System.Windows.Forms.Button btnImmunology;
        private System.Windows.Forms.Button btnBiochemistry;
        private System.Windows.Forms.Button btnHeamatology;
        private System.Windows.Forms.Button btnStoolExamination;
        private System.Windows.Forms.Button btnUrineExamination;
        private System.Windows.Forms.Button btnother;
    }
}