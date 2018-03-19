namespace BKvs2010
{
    partial class frmReportVaccination
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtPatientName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.bsVaccineDtl = new System.Windows.Forms.BindingSource(this.components);
            this.bsVaccineHdr = new System.Windows.Forms.BindingSource(this.components);
            this.gridDtl = new System.Windows.Forms.DataGridView();
            this.colType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colChkSero = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colSero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colChkVac = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colVaccin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colShowRpt = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTimeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDateOn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSeeAttach = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.trvdidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trvhidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trvdtypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trvdchkserologyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trvdserologyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trvdchkvaccinationrecDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trvdvaccinationrecordDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trvdtimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trvddateonDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trvdflagshowreportDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trvdflagattachedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trnreportvaccinehdrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtDoctor = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDAME = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtVaccination = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dpDateOn = new System.Windows.Forms.DateTimePicker();
            this.chkShow = new System.Windows.Forms.CheckBox();
            this.chkSee = new System.Windows.Forms.CheckBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.cbTime = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSerology = new System.Windows.Forms.TextBox();
            this.chkSero = new System.Windows.Forms.CheckBox();
            this.chkVaccine = new System.Windows.Forms.CheckBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClareAll = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnDetail = new System.Windows.Forms.Panel();
            this.ListAutoComplete = new System.Windows.Forms.ListBox();
            this.lbAutoCompleteDoctor = new System.Windows.Forms.ListBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.bsVaccineDtl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsVaccineHdr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDtl)).BeginInit();
            this.panel1.SuspendLayout();
            this.pnDetail.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtPatientName
            // 
            this.txtPatientName.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtPatientName.Location = new System.Drawing.Point(134, 57);
            this.txtPatientName.Name = "txtPatientName";
            this.txtPatientName.Size = new System.Drawing.Size(701, 23);
            this.txtPatientName.TabIndex = 0;
            this.txtPatientName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPatientName_KeyDown);
            this.txtPatientName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPatientName_KeyUp);
            this.txtPatientName.Leave += new System.EventHandler(this.txtPatientName_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(37, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(326, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(220, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "Document Vaccine Record";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(12, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Type :";
            // 
            // cbType
            // 
            this.cbType.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsVaccineDtl, "trvd_type", true));
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbType.FormattingEnabled = true;
            this.cbType.Location = new System.Drawing.Point(63, 7);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(748, 24);
            this.cbType.TabIndex = 4;
            // 
            // bsVaccineDtl
            // 
            this.bsVaccineDtl.DataMember = "trn_report_vaccine_dtls";
            this.bsVaccineDtl.DataSource = this.bsVaccineHdr;
            this.bsVaccineDtl.CurrentChanged += new System.EventHandler(this.bsVaccineDtl_CurrentChanged);
            // 
            // bsVaccineHdr
            // 
            this.bsVaccineHdr.DataSource = typeof(DBCheckup.trn_report_vaccine_hdr);
            // 
            // gridDtl
            // 
            this.gridDtl.AllowUserToAddRows = false;
            this.gridDtl.AutoGenerateColumns = false;
            this.gridDtl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridDtl.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colType,
            this.colChkSero,
            this.colSero,
            this.colChkVac,
            this.colVaccin,
            this.colShowRpt,
            this.colTime,
            this.colTimeName,
            this.colDateOn,
            this.colSeeAttach,
            this.trvdidDataGridViewTextBoxColumn,
            this.trvhidDataGridViewTextBoxColumn,
            this.trvdtypeDataGridViewTextBoxColumn,
            this.trvdchkserologyDataGridViewTextBoxColumn,
            this.trvdserologyDataGridViewTextBoxColumn,
            this.trvdchkvaccinationrecDataGridViewTextBoxColumn,
            this.trvdvaccinationrecordDataGridViewTextBoxColumn,
            this.trvdtimeDataGridViewTextBoxColumn,
            this.trvddateonDataGridViewTextBoxColumn,
            this.trvdflagshowreportDataGridViewTextBoxColumn,
            this.trvdflagattachedDataGridViewTextBoxColumn,
            this.trnreportvaccinehdrDataGridViewTextBoxColumn});
            this.gridDtl.DataSource = this.bsVaccineDtl;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridDtl.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridDtl.Location = new System.Drawing.Point(39, 148);
            this.gridDtl.Name = "gridDtl";
            this.gridDtl.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridDtl.Size = new System.Drawing.Size(739, 192);
            this.gridDtl.TabIndex = 10;
            this.gridDtl.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridDtl_CellEndEdit);
            // 
            // colType
            // 
            this.colType.DataPropertyName = "trvd_type";
            this.colType.HeaderText = "Type";
            this.colType.Name = "colType";
            this.colType.ReadOnly = true;
            this.colType.Width = 120;
            // 
            // colChkSero
            // 
            this.colChkSero.DataPropertyName = "trvd_chk_serology";
            this.colChkSero.HeaderText = "";
            this.colChkSero.Name = "colChkSero";
            this.colChkSero.ReadOnly = true;
            this.colChkSero.Width = 30;
            // 
            // colSero
            // 
            this.colSero.DataPropertyName = "trvd_serology";
            this.colSero.HeaderText = "Serology";
            this.colSero.Name = "colSero";
            this.colSero.ReadOnly = true;
            this.colSero.Width = 200;
            // 
            // colChkVac
            // 
            this.colChkVac.DataPropertyName = "trvd_chk_vaccination_rec";
            this.colChkVac.HeaderText = "";
            this.colChkVac.Name = "colChkVac";
            this.colChkVac.ReadOnly = true;
            this.colChkVac.Width = 30;
            // 
            // colVaccin
            // 
            this.colVaccin.DataPropertyName = "trvd_vaccination_record";
            this.colVaccin.HeaderText = "Vaccination record";
            this.colVaccin.Name = "colVaccin";
            this.colVaccin.ReadOnly = true;
            this.colVaccin.Width = 200;
            // 
            // colShowRpt
            // 
            this.colShowRpt.DataPropertyName = "trvd_flag_show_report";
            this.colShowRpt.HeaderText = "Show Report";
            this.colShowRpt.Name = "colShowRpt";
            this.colShowRpt.ReadOnly = true;
            this.colShowRpt.Width = 78;
            // 
            // colTime
            // 
            this.colTime.DataPropertyName = "trvd_time";
            this.colTime.HeaderText = "Time";
            this.colTime.Name = "colTime";
            this.colTime.ReadOnly = true;
            this.colTime.Visible = false;
            this.colTime.Width = 150;
            // 
            // colTimeName
            // 
            this.colTimeName.HeaderText = "Time";
            this.colTimeName.Name = "colTimeName";
            this.colTimeName.ReadOnly = true;
            // 
            // colDateOn
            // 
            this.colDateOn.DataPropertyName = "trvd_date_on";
            dataGridViewCellStyle1.Format = "dd/MM/yyyy";
            this.colDateOn.DefaultCellStyle = dataGridViewCellStyle1;
            this.colDateOn.HeaderText = "Date";
            this.colDateOn.Name = "colDateOn";
            this.colDateOn.ReadOnly = true;
            this.colDateOn.Width = 80;
            // 
            // colSeeAttach
            // 
            this.colSeeAttach.DataPropertyName = "trvd_flag_attached";
            this.colSeeAttach.HeaderText = "See attach";
            this.colSeeAttach.Name = "colSeeAttach";
            this.colSeeAttach.ReadOnly = true;
            this.colSeeAttach.Width = 70;
            // 
            // trvdidDataGridViewTextBoxColumn
            // 
            this.trvdidDataGridViewTextBoxColumn.DataPropertyName = "trvd_id";
            this.trvdidDataGridViewTextBoxColumn.HeaderText = "trvd_id";
            this.trvdidDataGridViewTextBoxColumn.Name = "trvdidDataGridViewTextBoxColumn";
            this.trvdidDataGridViewTextBoxColumn.Visible = false;
            // 
            // trvhidDataGridViewTextBoxColumn
            // 
            this.trvhidDataGridViewTextBoxColumn.DataPropertyName = "trvh_id";
            this.trvhidDataGridViewTextBoxColumn.HeaderText = "trvh_id";
            this.trvhidDataGridViewTextBoxColumn.Name = "trvhidDataGridViewTextBoxColumn";
            this.trvhidDataGridViewTextBoxColumn.Visible = false;
            // 
            // trvdtypeDataGridViewTextBoxColumn
            // 
            this.trvdtypeDataGridViewTextBoxColumn.DataPropertyName = "trvd_type";
            this.trvdtypeDataGridViewTextBoxColumn.HeaderText = "trvd_type";
            this.trvdtypeDataGridViewTextBoxColumn.Name = "trvdtypeDataGridViewTextBoxColumn";
            this.trvdtypeDataGridViewTextBoxColumn.Visible = false;
            // 
            // trvdchkserologyDataGridViewTextBoxColumn
            // 
            this.trvdchkserologyDataGridViewTextBoxColumn.DataPropertyName = "trvd_chk_serology";
            this.trvdchkserologyDataGridViewTextBoxColumn.HeaderText = "trvd_chk_serology";
            this.trvdchkserologyDataGridViewTextBoxColumn.Name = "trvdchkserologyDataGridViewTextBoxColumn";
            this.trvdchkserologyDataGridViewTextBoxColumn.Visible = false;
            // 
            // trvdserologyDataGridViewTextBoxColumn
            // 
            this.trvdserologyDataGridViewTextBoxColumn.DataPropertyName = "trvd_serology";
            this.trvdserologyDataGridViewTextBoxColumn.HeaderText = "trvd_serology";
            this.trvdserologyDataGridViewTextBoxColumn.Name = "trvdserologyDataGridViewTextBoxColumn";
            this.trvdserologyDataGridViewTextBoxColumn.Visible = false;
            // 
            // trvdchkvaccinationrecDataGridViewTextBoxColumn
            // 
            this.trvdchkvaccinationrecDataGridViewTextBoxColumn.DataPropertyName = "trvd_chk_vaccination_rec";
            this.trvdchkvaccinationrecDataGridViewTextBoxColumn.HeaderText = "trvd_chk_vaccination_rec";
            this.trvdchkvaccinationrecDataGridViewTextBoxColumn.Name = "trvdchkvaccinationrecDataGridViewTextBoxColumn";
            this.trvdchkvaccinationrecDataGridViewTextBoxColumn.Visible = false;
            // 
            // trvdvaccinationrecordDataGridViewTextBoxColumn
            // 
            this.trvdvaccinationrecordDataGridViewTextBoxColumn.DataPropertyName = "trvd_vaccination_record";
            this.trvdvaccinationrecordDataGridViewTextBoxColumn.HeaderText = "trvd_vaccination_record";
            this.trvdvaccinationrecordDataGridViewTextBoxColumn.Name = "trvdvaccinationrecordDataGridViewTextBoxColumn";
            this.trvdvaccinationrecordDataGridViewTextBoxColumn.Visible = false;
            // 
            // trvdtimeDataGridViewTextBoxColumn
            // 
            this.trvdtimeDataGridViewTextBoxColumn.DataPropertyName = "trvd_time";
            this.trvdtimeDataGridViewTextBoxColumn.HeaderText = "trvd_time";
            this.trvdtimeDataGridViewTextBoxColumn.Name = "trvdtimeDataGridViewTextBoxColumn";
            this.trvdtimeDataGridViewTextBoxColumn.Visible = false;
            // 
            // trvddateonDataGridViewTextBoxColumn
            // 
            this.trvddateonDataGridViewTextBoxColumn.DataPropertyName = "trvd_date_on";
            this.trvddateonDataGridViewTextBoxColumn.HeaderText = "trvd_date_on";
            this.trvddateonDataGridViewTextBoxColumn.Name = "trvddateonDataGridViewTextBoxColumn";
            this.trvddateonDataGridViewTextBoxColumn.Visible = false;
            // 
            // trvdflagshowreportDataGridViewTextBoxColumn
            // 
            this.trvdflagshowreportDataGridViewTextBoxColumn.DataPropertyName = "trvd_flag_show_report";
            this.trvdflagshowreportDataGridViewTextBoxColumn.HeaderText = "trvd_flag_show_report";
            this.trvdflagshowreportDataGridViewTextBoxColumn.Name = "trvdflagshowreportDataGridViewTextBoxColumn";
            this.trvdflagshowreportDataGridViewTextBoxColumn.Visible = false;
            // 
            // trvdflagattachedDataGridViewTextBoxColumn
            // 
            this.trvdflagattachedDataGridViewTextBoxColumn.DataPropertyName = "trvd_flag_attached";
            this.trvdflagattachedDataGridViewTextBoxColumn.HeaderText = "trvd_flag_attached";
            this.trvdflagattachedDataGridViewTextBoxColumn.Name = "trvdflagattachedDataGridViewTextBoxColumn";
            this.trvdflagattachedDataGridViewTextBoxColumn.Visible = false;
            // 
            // trnreportvaccinehdrDataGridViewTextBoxColumn
            // 
            this.trnreportvaccinehdrDataGridViewTextBoxColumn.DataPropertyName = "trn_report_vaccine_hdr";
            this.trnreportvaccinehdrDataGridViewTextBoxColumn.HeaderText = "trn_report_vaccine_hdr";
            this.trnreportvaccinehdrDataGridViewTextBoxColumn.Name = "trnreportvaccinehdrDataGridViewTextBoxColumn";
            this.trnreportvaccinehdrDataGridViewTextBoxColumn.Visible = false;
            // 
            // txtDoctor
            // 
            this.txtDoctor.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtDoctor.Location = new System.Drawing.Point(134, 86);
            this.txtDoctor.Name = "txtDoctor";
            this.txtDoctor.Size = new System.Drawing.Size(701, 23);
            this.txtDoctor.TabIndex = 13;
            this.txtDoctor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDoctor_KeyDown);
            this.txtDoctor.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDoctor_KeyUp);
            this.txtDoctor.Leave += new System.EventHandler(this.txtDoctor_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label6.Location = new System.Drawing.Point(37, 89);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 16);
            this.label6.TabIndex = 12;
            this.label6.Text = "Doctor Name :";
            // 
            // txtDAME
            // 
            this.txtDAME.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsVaccineHdr, "trvh_dame_no", true));
            this.txtDAME.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtDAME.Location = new System.Drawing.Point(134, 115);
            this.txtDAME.MaxLength = 20;
            this.txtDAME.Name = "txtDAME";
            this.txtDAME.Size = new System.Drawing.Size(701, 23);
            this.txtDAME.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label7.Location = new System.Drawing.Point(37, 118);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 16);
            this.label7.TabIndex = 14;
            this.label7.Text = "DAME No :";
            // 
            // txtVaccination
            // 
            this.txtVaccination.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsVaccineDtl, "trvd_vaccination_record", true));
            this.txtVaccination.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtVaccination.Location = new System.Drawing.Point(166, 67);
            this.txtVaccination.Name = "txtVaccination";
            this.txtVaccination.Size = new System.Drawing.Size(645, 23);
            this.txtVaccination.TabIndex = 31;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label8.Location = new System.Drawing.Point(280, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(33, 16);
            this.label8.TabIndex = 29;
            this.label8.Text = "On :";
            // 
            // dpDateOn
            // 
            this.dpDateOn.CustomFormat = "dd MMM yyyy";
            this.dpDateOn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dpDateOn.Location = new System.Drawing.Point(318, 5);
            this.dpDateOn.Name = "dpDateOn";
            this.dpDateOn.Size = new System.Drawing.Size(221, 23);
            this.dpDateOn.TabIndex = 28;
            this.dpDateOn.ValueChanged += new System.EventHandler(this.dpDateOn_ValueChanged);
            // 
            // chkShow
            // 
            this.chkShow.AutoSize = true;
            this.chkShow.Checked = true;
            this.chkShow.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShow.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkShow.Location = new System.Drawing.Point(114, 96);
            this.chkShow.Name = "chkShow";
            this.chkShow.Size = new System.Drawing.Size(112, 20);
            this.chkShow.TabIndex = 27;
            this.chkShow.Text = "Show in report";
            this.chkShow.UseVisualStyleBackColor = true;
            this.chkShow.CheckedChanged += new System.EventHandler(this.chkShow_CheckedChanged);
            // 
            // chkSee
            // 
            this.chkSee.AutoSize = true;
            this.chkSee.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bsVaccineDtl, "trvd_flag_attached", true));
            this.chkSee.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkSee.Location = new System.Drawing.Point(649, 153);
            this.chkSee.Name = "chkSee";
            this.chkSee.Size = new System.Drawing.Size(162, 20);
            this.chkSee.TabIndex = 26;
            this.chkSee.Text = "Note : See attached file";
            this.chkSee.UseVisualStyleBackColor = true;
            this.chkSee.CheckedChanged += new System.EventHandler(this.chkSee_CheckedChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnAdd.Location = new System.Drawing.Point(785, 259);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(51, 23);
            this.btnAdd.TabIndex = 25;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // cbTime
            // 
            this.cbTime.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bsVaccineDtl, "trvd_time", true));
            this.cbTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTime.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbTime.FormattingEnabled = true;
            this.cbTime.Location = new System.Drawing.Point(63, 4);
            this.cbTime.Name = "cbTime";
            this.cbTime.Size = new System.Drawing.Size(211, 24);
            this.cbTime.TabIndex = 24;
            this.cbTime.Validated += new System.EventHandler(this.cbTime_Validated);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label5.Location = new System.Drawing.Point(8, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 16);
            this.label5.TabIndex = 23;
            this.label5.Text = "Time :";
            // 
            // txtSerology
            // 
            this.txtSerology.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsVaccineDtl, "trvd_serology", true));
            this.txtSerology.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtSerology.Location = new System.Drawing.Point(107, 37);
            this.txtSerology.Name = "txtSerology";
            this.txtSerology.Size = new System.Drawing.Size(704, 23);
            this.txtSerology.TabIndex = 22;
            // 
            // chkSero
            // 
            this.chkSero.AutoSize = true;
            this.chkSero.Checked = true;
            this.chkSero.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSero.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkSero.Location = new System.Drawing.Point(15, 39);
            this.chkSero.Name = "chkSero";
            this.chkSero.Size = new System.Drawing.Size(86, 20);
            this.chkSero.TabIndex = 32;
            this.chkSero.Text = "Serology :";
            this.chkSero.UseVisualStyleBackColor = true;
            this.chkSero.CheckedChanged += new System.EventHandler(this.chkSero_CheckedChanged);
            // 
            // chkVaccine
            // 
            this.chkVaccine.AutoSize = true;
            this.chkVaccine.Checked = true;
            this.chkVaccine.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkVaccine.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkVaccine.Location = new System.Drawing.Point(15, 69);
            this.chkVaccine.Name = "chkVaccine";
            this.chkVaccine.Size = new System.Drawing.Size(145, 20);
            this.chkVaccine.TabIndex = 33;
            this.chkVaccine.Text = "Vaccination Record :";
            this.chkVaccine.UseVisualStyleBackColor = true;
            this.chkVaccine.CheckedChanged += new System.EventHandler(this.chkVaccine_CheckedChanged);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnDelete.Location = new System.Drawing.Point(785, 288);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(50, 23);
            this.btnDelete.TabIndex = 34;
            this.btnDelete.Text = "Del";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClareAll
            // 
            this.btnClareAll.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnClareAll.Location = new System.Drawing.Point(784, 317);
            this.btnClareAll.Name = "btnClareAll";
            this.btnClareAll.Size = new System.Drawing.Size(51, 23);
            this.btnClareAll.TabIndex = 35;
            this.btnClareAll.Text = "Clear";
            this.btnClareAll.UseVisualStyleBackColor = true;
            this.btnClareAll.Click += new System.EventHandler(this.btnClareAll_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.cbTime);
            this.panel1.Controls.Add(this.dpDateOn);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Location = new System.Drawing.Point(103, 115);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(721, 32);
            this.panel1.TabIndex = 36;
            this.panel1.EnabledChanged += new System.EventHandler(this.panel1_EnabledChanged);
            // 
            // pnDetail
            // 
            this.pnDetail.Controls.Add(this.label3);
            this.pnDetail.Controls.Add(this.panel1);
            this.pnDetail.Controls.Add(this.cbType);
            this.pnDetail.Controls.Add(this.txtSerology);
            this.pnDetail.Controls.Add(this.chkSee);
            this.pnDetail.Controls.Add(this.chkVaccine);
            this.pnDetail.Controls.Add(this.chkShow);
            this.pnDetail.Controls.Add(this.chkSero);
            this.pnDetail.Controls.Add(this.txtVaccination);
            this.pnDetail.Enabled = false;
            this.pnDetail.Location = new System.Drawing.Point(24, 346);
            this.pnDetail.Name = "pnDetail";
            this.pnDetail.Size = new System.Drawing.Size(837, 176);
            this.pnDetail.TabIndex = 37;
            // 
            // ListAutoComplete
            // 
            this.ListAutoComplete.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.ListAutoComplete.FormattingEnabled = true;
            this.ListAutoComplete.ItemHeight = 16;
            this.ListAutoComplete.Location = new System.Drawing.Point(134, 79);
            this.ListAutoComplete.Name = "ListAutoComplete";
            this.ListAutoComplete.ScrollAlwaysVisible = true;
            this.ListAutoComplete.Size = new System.Drawing.Size(701, 84);
            this.ListAutoComplete.TabIndex = 38;
            this.ListAutoComplete.Visible = false;
            this.ListAutoComplete.Click += new System.EventHandler(this.ListAutoComplete_Click);
            this.ListAutoComplete.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ListAutoComplete_MouseMove);
            // 
            // lbAutoCompleteDoctor
            // 
            this.lbAutoCompleteDoctor.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbAutoCompleteDoctor.FormattingEnabled = true;
            this.lbAutoCompleteDoctor.ItemHeight = 16;
            this.lbAutoCompleteDoctor.Location = new System.Drawing.Point(134, 108);
            this.lbAutoCompleteDoctor.Name = "lbAutoCompleteDoctor";
            this.lbAutoCompleteDoctor.ScrollAlwaysVisible = true;
            this.lbAutoCompleteDoctor.Size = new System.Drawing.Size(701, 84);
            this.lbAutoCompleteDoctor.TabIndex = 39;
            this.lbAutoCompleteDoctor.Visible = false;
            this.lbAutoCompleteDoctor.Click += new System.EventHandler(this.lbAutoCompleteDoctor_Click);
            this.lbAutoCompleteDoctor.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbAutoCompleteDoctor_MouseMove);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSave.Location = new System.Drawing.Point(690, 532);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(70, 27);
            this.btnSave.TabIndex = 40;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Enabled = false;
            this.btnPrint.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnPrint.Location = new System.Drawing.Point(766, 532);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(70, 27);
            this.btnPrint.TabIndex = 41;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // frmReportVaccination
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(873, 571);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.lbAutoCompleteDoctor);
            this.Controls.Add(this.ListAutoComplete);
            this.Controls.Add(this.pnDetail);
            this.Controls.Add(this.btnClareAll);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtDAME);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtDoctor);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.gridDtl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPatientName);
            this.Name = "frmReportVaccination";
            this.Text = "ReportVacination";
            ((System.ComponentModel.ISupportInitialize)(this.bsVaccineDtl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsVaccineHdr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDtl)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnDetail.ResumeLayout(false);
            this.pnDetail.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPatientName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.DataGridView gridDtl;
        private System.Windows.Forms.TextBox txtDoctor;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDAME;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtVaccination;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dpDateOn;
        public System.Windows.Forms.CheckBox chkShow;
        public System.Windows.Forms.CheckBox chkSee;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ComboBox cbTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSerology;
        private System.Windows.Forms.BindingSource bsVaccineHdr;
        private System.Windows.Forms.BindingSource bsVaccineDtl;
        private System.Windows.Forms.CheckBox chkSero;
        private System.Windows.Forms.CheckBox chkVaccine;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClareAll;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnDetail;
        private System.Windows.Forms.ListBox ListAutoComplete;
        private System.Windows.Forms.ListBox lbAutoCompleteDoctor;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.DataGridViewTextBoxColumn colType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colChkSero;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSero;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colChkVac;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVaccin;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colShowRpt;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTimeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDateOn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSeeAttach;
        private System.Windows.Forms.DataGridViewTextBoxColumn trvdidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn trvhidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn trvdtypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn trvdchkserologyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn trvdserologyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn trvdchkvaccinationrecDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn trvdvaccinationrecordDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn trvdtimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn trvddateonDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn trvdflagshowreportDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn trvdflagattachedDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn trnreportvaccinehdrDataGridViewTextBoxColumn;
    }
}