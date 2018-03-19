namespace BKvs2010
{
    partial class frmAviationMedCerReport
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.bsAviaMedCer = new System.Windows.Forms.BindingSource(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.txtPatientName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbRptType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtReccom = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtLimit = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtDoctor = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.ListAutoComplete = new System.Windows.Forms.ListBox();
            this.pnPurpose = new System.Windows.Forms.Panel();
            this.rdStudent = new System.Windows.Forms.RadioButton();
            this.rdPreEmp = new System.Windows.Forms.RadioButton();
            this.rdAircrewMed = new System.Windows.Forms.RadioButton();
            this.pnAirMedCer = new System.Windows.Forms.Panel();
            this.rdFPT = new System.Windows.Forms.RadioButton();
            this.rdUPT = new System.Windows.Forms.RadioButton();
            this.rdTUPT = new System.Windows.Forms.RadioButton();
            this.rdFPA = new System.Windows.Forms.RadioButton();
            this.rdUPA = new System.Windows.Forms.RadioButton();
            this.rdTUPA = new System.Windows.Forms.RadioButton();
            this.pnAS = new System.Windows.Forms.Panel();
            this.txtAsRemark = new System.Windows.Forms.TextBox();
            this.rdOther = new System.Windows.Forms.RadioButton();
            this.rdAero = new System.Windows.Forms.RadioButton();
            this.rdCabin = new System.Windows.Forms.RadioButton();
            this.rdPPL = new System.Windows.Forms.RadioButton();
            this.rdCPL = new System.Windows.Forms.RadioButton();
            this.rdATPL = new System.Windows.Forms.RadioButton();
            this.lbAutoCompleteDoctor = new System.Windows.Forms.ListBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.bsAviaMedCer)).BeginInit();
            this.pnPurpose.SuspendLayout();
            this.pnAirMedCer.SuspendLayout();
            this.pnAS.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(238, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(289, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "AVIATION MEDICAL CERTIFICATE";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(29, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "DATE : ";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bsAviaMedCer, "tram_date_print", true));
            this.dateTimePicker1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dateTimePicker1.Location = new System.Drawing.Point(87, 40);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(233, 23);
            this.dateTimePicker1.TabIndex = 2;
            // 
            // bsAviaMedCer
            // 
            this.bsAviaMedCer.DataSource = typeof(DBCheckup.trn_report_aviation_medical);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(29, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "For : ";
            // 
            // txtPatientName
            // 
            this.txtPatientName.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtPatientName.Location = new System.Drawing.Point(32, 120);
            this.txtPatientName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtPatientName.Name = "txtPatientName";
            this.txtPatientName.Size = new System.Drawing.Size(701, 23);
            this.txtPatientName.TabIndex = 4;
            this.txtPatientName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPatientName_KeyDown);
            this.txtPatientName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPatientName_KeyUp);
            this.txtPatientName.Leave += new System.EventHandler(this.txtPatientName_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.Location = new System.Drawing.Point(29, 153);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "Report Type : ";
            // 
            // cmbRptType
            // 
            this.cmbRptType.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bsAviaMedCer, "mrt_id", true));
            this.cmbRptType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRptType.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cmbRptType.FormattingEnabled = true;
            this.cmbRptType.Location = new System.Drawing.Point(32, 173);
            this.cmbRptType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbRptType.Name = "cmbRptType";
            this.cmbRptType.Size = new System.Drawing.Size(701, 24);
            this.cmbRptType.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label5.Location = new System.Drawing.Point(3, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(204, 16);
            this.label5.TabIndex = 7;
            this.label5.Text = "Purpose of Physical Examination : ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label6.Location = new System.Drawing.Point(29, 265);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(191, 16);
            this.label6.TabIndex = 10;
            this.label6.Text = "Recommendations & Comments :";
            // 
            // txtReccom
            // 
            this.txtReccom.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsAviaMedCer, "tram_reccommendtion", true));
            this.txtReccom.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtReccom.Location = new System.Drawing.Point(32, 284);
            this.txtReccom.Multiline = true;
            this.txtReccom.Name = "txtReccom";
            this.txtReccom.Size = new System.Drawing.Size(701, 84);
            this.txtReccom.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label7.Location = new System.Drawing.Point(29, 372);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 16);
            this.label7.TabIndex = 12;
            this.label7.Text = "Limitation : ";
            // 
            // txtLimit
            // 
            this.txtLimit.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsAviaMedCer, "tram_limitation", true));
            this.txtLimit.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtLimit.Location = new System.Drawing.Point(32, 391);
            this.txtLimit.Multiline = true;
            this.txtLimit.Name = "txtLimit";
            this.txtLimit.Size = new System.Drawing.Size(701, 69);
            this.txtLimit.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label8.Location = new System.Drawing.Point(29, 464);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 16);
            this.label8.TabIndex = 14;
            this.label8.Text = "Assessment :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label9.Location = new System.Drawing.Point(3, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(177, 16);
            this.label9.TabIndex = 16;
            this.label9.Text = "Aircrew Medical Certiificate : ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label10.Location = new System.Drawing.Point(29, 571);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 16);
            this.label10.TabIndex = 17;
            this.label10.Text = "As : ";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label11.Location = new System.Drawing.Point(29, 633);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(91, 16);
            this.label11.TabIndex = 19;
            this.label11.Text = "Doctor Name :";
            // 
            // txtDoctor
            // 
            this.txtDoctor.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtDoctor.Location = new System.Drawing.Point(32, 653);
            this.txtDoctor.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDoctor.Name = "txtDoctor";
            this.txtDoctor.Size = new System.Drawing.Size(701, 23);
            this.txtDoctor.TabIndex = 20;
            this.txtDoctor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDoctor_KeyDown);
            this.txtDoctor.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDoctor_KeyUp);
            this.txtDoctor.Leave += new System.EventHandler(this.txtDoctor_Leave);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSave.Location = new System.Drawing.Point(587, 701);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(70, 27);
            this.btnSave.TabIndex = 21;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ListAutoComplete
            // 
            this.ListAutoComplete.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.ListAutoComplete.FormattingEnabled = true;
            this.ListAutoComplete.ItemHeight = 16;
            this.ListAutoComplete.Location = new System.Drawing.Point(32, 142);
            this.ListAutoComplete.Name = "ListAutoComplete";
            this.ListAutoComplete.ScrollAlwaysVisible = true;
            this.ListAutoComplete.Size = new System.Drawing.Size(701, 84);
            this.ListAutoComplete.TabIndex = 22;
            this.ListAutoComplete.Visible = false;
            this.ListAutoComplete.Click += new System.EventHandler(this.ListAutoComplete_Click);
            this.ListAutoComplete.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ListAutoComplete_MouseMove);
            // 
            // pnPurpose
            // 
            this.pnPurpose.Controls.Add(this.rdStudent);
            this.pnPurpose.Controls.Add(this.rdPreEmp);
            this.pnPurpose.Controls.Add(this.rdAircrewMed);
            this.pnPurpose.Controls.Add(this.label5);
            this.pnPurpose.Location = new System.Drawing.Point(32, 204);
            this.pnPurpose.Name = "pnPurpose";
            this.pnPurpose.Size = new System.Drawing.Size(701, 57);
            this.pnPurpose.TabIndex = 23;
            this.pnPurpose.Tag = "A";
            // 
            // rdStudent
            // 
            this.rdStudent.AutoSize = true;
            this.rdStudent.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.rdStudent.Location = new System.Drawing.Point(445, 28);
            this.rdStudent.Name = "rdStudent";
            this.rdStudent.Size = new System.Drawing.Size(70, 20);
            this.rdStudent.TabIndex = 10;
            this.rdStudent.TabStop = true;
            this.rdStudent.Tag = "S";
            this.rdStudent.Text = "Student";
            this.rdStudent.UseVisualStyleBackColor = true;
            // 
            // rdPreEmp
            // 
            this.rdPreEmp.AutoSize = true;
            this.rdPreEmp.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.rdPreEmp.Location = new System.Drawing.Point(255, 28);
            this.rdPreEmp.Name = "rdPreEmp";
            this.rdPreEmp.Size = new System.Drawing.Size(120, 20);
            this.rdPreEmp.TabIndex = 9;
            this.rdPreEmp.TabStop = true;
            this.rdPreEmp.Tag = "P";
            this.rdPreEmp.Text = "Pre-employment";
            this.rdPreEmp.UseVisualStyleBackColor = true;
            // 
            // rdAircrewMed
            // 
            this.rdAircrewMed.AutoSize = true;
            this.rdAircrewMed.Checked = true;
            this.rdAircrewMed.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.rdAircrewMed.Location = new System.Drawing.Point(6, 28);
            this.rdAircrewMed.Name = "rdAircrewMed";
            this.rdAircrewMed.Size = new System.Drawing.Size(179, 20);
            this.rdAircrewMed.TabIndex = 8;
            this.rdAircrewMed.TabStop = true;
            this.rdAircrewMed.Tag = "A";
            this.rdAircrewMed.Text = "Aircrew Medical Certificate";
            this.rdAircrewMed.UseVisualStyleBackColor = true;
            // 
            // pnAirMedCer
            // 
            this.pnAirMedCer.Controls.Add(this.rdFPT);
            this.pnAirMedCer.Controls.Add(this.rdUPT);
            this.pnAirMedCer.Controls.Add(this.rdTUPT);
            this.pnAirMedCer.Controls.Add(this.rdFPA);
            this.pnAirMedCer.Controls.Add(this.rdUPA);
            this.pnAirMedCer.Controls.Add(this.rdTUPA);
            this.pnAirMedCer.Controls.Add(this.label9);
            this.pnAirMedCer.Location = new System.Drawing.Point(32, 483);
            this.pnAirMedCer.Name = "pnAirMedCer";
            this.pnAirMedCer.Size = new System.Drawing.Size(701, 78);
            this.pnAirMedCer.TabIndex = 24;
            this.pnAirMedCer.Tag = "TUPA";
            // 
            // rdFPT
            // 
            this.rdFPT.AutoSize = true;
            this.rdFPT.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.rdFPT.Location = new System.Drawing.Point(509, 52);
            this.rdFPT.Name = "rdFPT";
            this.rdFPT.Size = new System.Drawing.Size(152, 20);
            this.rdFPT.TabIndex = 27;
            this.rdFPT.TabStop = true;
            this.rdFPT.Tag = "FPT";
            this.rdFPT.Text = "Fit to perform training";
            this.rdFPT.UseVisualStyleBackColor = true;
            // 
            // rdUPT
            // 
            this.rdUPT.AutoSize = true;
            this.rdUPT.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.rdUPT.Location = new System.Drawing.Point(292, 52);
            this.rdUPT.Name = "rdUPT";
            this.rdUPT.Size = new System.Drawing.Size(164, 20);
            this.rdUPT.TabIndex = 26;
            this.rdUPT.TabStop = true;
            this.rdUPT.Tag = "UPT";
            this.rdUPT.Text = "Unfit to perform training";
            this.rdUPT.UseVisualStyleBackColor = true;
            // 
            // rdTUPT
            // 
            this.rdTUPT.AutoSize = true;
            this.rdTUPT.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.rdTUPT.Location = new System.Drawing.Point(9, 52);
            this.rdTUPT.Name = "rdTUPT";
            this.rdTUPT.Size = new System.Drawing.Size(230, 20);
            this.rdTUPT.TabIndex = 25;
            this.rdTUPT.TabStop = true;
            this.rdTUPT.Tag = "TUPT";
            this.rdTUPT.Text = "Temporary unfit to perform training";
            this.rdTUPT.UseVisualStyleBackColor = true;
            // 
            // rdFPA
            // 
            this.rdFPA.AutoSize = true;
            this.rdFPA.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.rdFPA.Location = new System.Drawing.Point(509, 26);
            this.rdFPA.Name = "rdFPA";
            this.rdFPA.Size = new System.Drawing.Size(180, 20);
            this.rdFPA.TabIndex = 18;
            this.rdFPA.TabStop = true;
            this.rdFPA.Tag = "FPA";
            this.rdFPA.Text = "Fit to perform aircrew duty";
            this.rdFPA.UseVisualStyleBackColor = true;
            // 
            // rdUPA
            // 
            this.rdUPA.AutoSize = true;
            this.rdUPA.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.rdUPA.Location = new System.Drawing.Point(292, 26);
            this.rdUPA.Name = "rdUPA";
            this.rdUPA.Size = new System.Drawing.Size(192, 20);
            this.rdUPA.TabIndex = 17;
            this.rdUPA.TabStop = true;
            this.rdUPA.Tag = "UPA";
            this.rdUPA.Text = "Unfit to perform aircrew duty";
            this.rdUPA.UseVisualStyleBackColor = true;
            // 
            // rdTUPA
            // 
            this.rdTUPA.AutoSize = true;
            this.rdTUPA.Checked = true;
            this.rdTUPA.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.rdTUPA.Location = new System.Drawing.Point(9, 26);
            this.rdTUPA.Name = "rdTUPA";
            this.rdTUPA.Size = new System.Drawing.Size(258, 20);
            this.rdTUPA.TabIndex = 11;
            this.rdTUPA.TabStop = true;
            this.rdTUPA.Tag = "TUPA";
            this.rdTUPA.Text = "Temporary unfit to perform aircrew duty";
            this.rdTUPA.UseVisualStyleBackColor = true;
            // 
            // pnAS
            // 
            this.pnAS.Controls.Add(this.txtAsRemark);
            this.pnAS.Controls.Add(this.rdOther);
            this.pnAS.Controls.Add(this.rdAero);
            this.pnAS.Controls.Add(this.rdCabin);
            this.pnAS.Controls.Add(this.rdPPL);
            this.pnAS.Controls.Add(this.rdCPL);
            this.pnAS.Controls.Add(this.rdATPL);
            this.pnAS.Location = new System.Drawing.Point(66, 566);
            this.pnAS.Name = "pnAS";
            this.pnAS.Size = new System.Drawing.Size(667, 59);
            this.pnAS.TabIndex = 25;
            this.pnAS.Tag = "ATPL";
            // 
            // txtAsRemark
            // 
            this.txtAsRemark.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsAviaMedCer, "tram_ass_as_remark", true));
            this.txtAsRemark.Enabled = false;
            this.txtAsRemark.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtAsRemark.Location = new System.Drawing.Point(71, 28);
            this.txtAsRemark.Name = "txtAsRemark";
            this.txtAsRemark.Size = new System.Drawing.Size(586, 23);
            this.txtAsRemark.TabIndex = 34;
            // 
            // rdOther
            // 
            this.rdOther.AutoSize = true;
            this.rdOther.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.rdOther.Location = new System.Drawing.Point(7, 31);
            this.rdOther.Name = "rdOther";
            this.rdOther.Size = new System.Drawing.Size(58, 20);
            this.rdOther.TabIndex = 33;
            this.rdOther.TabStop = true;
            this.rdOther.Tag = "OT";
            this.rdOther.Text = "Other";
            this.rdOther.UseVisualStyleBackColor = true;
            this.rdOther.CheckedChanged += new System.EventHandler(this.rdOther_CheckedChanged);
            // 
            // rdAero
            // 
            this.rdAero.AutoSize = true;
            this.rdAero.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.rdAero.Location = new System.Drawing.Point(513, 3);
            this.rdAero.Name = "rdAero";
            this.rdAero.Size = new System.Drawing.Size(151, 20);
            this.rdAero.TabIndex = 32;
            this.rdAero.TabStop = true;
            this.rdAero.Tag = "Aero";
            this.rdAero.Text = "Aeronautical Engineer";
            this.rdAero.UseVisualStyleBackColor = true;
            // 
            // rdCabin
            // 
            this.rdCabin.AutoSize = true;
            this.rdCabin.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.rdCabin.Location = new System.Drawing.Point(374, 3);
            this.rdCabin.Name = "rdCabin";
            this.rdCabin.Size = new System.Drawing.Size(116, 20);
            this.rdCabin.TabIndex = 31;
            this.rdCabin.TabStop = true;
            this.rdCabin.Tag = "Cabin";
            this.rdCabin.Text = "Cabin attendant";
            this.rdCabin.UseVisualStyleBackColor = true;
            // 
            // rdPPL
            // 
            this.rdPPL.AutoSize = true;
            this.rdPPL.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.rdPPL.Location = new System.Drawing.Point(258, 3);
            this.rdPPL.Name = "rdPPL";
            this.rdPPL.Size = new System.Drawing.Size(93, 20);
            this.rdPPL.TabIndex = 30;
            this.rdPPL.TabStop = true;
            this.rdPPL.Tag = "PPL";
            this.rdPPL.Text = "Private Pilot";
            this.rdPPL.UseVisualStyleBackColor = true;
            // 
            // rdCPL
            // 
            this.rdCPL.AutoSize = true;
            this.rdCPL.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.rdCPL.Location = new System.Drawing.Point(113, 3);
            this.rdCPL.Name = "rdCPL";
            this.rdCPL.Size = new System.Drawing.Size(122, 20);
            this.rdCPL.TabIndex = 29;
            this.rdCPL.TabStop = true;
            this.rdCPL.Tag = "CPL";
            this.rdCPL.Text = "Commercial Pilot";
            this.rdCPL.UseVisualStyleBackColor = true;
            // 
            // rdATPL
            // 
            this.rdATPL.AutoSize = true;
            this.rdATPL.Checked = true;
            this.rdATPL.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.rdATPL.Location = new System.Drawing.Point(7, 3);
            this.rdATPL.Name = "rdATPL";
            this.rdATPL.Size = new System.Drawing.Size(83, 20);
            this.rdATPL.TabIndex = 28;
            this.rdATPL.TabStop = true;
            this.rdATPL.Tag = "ATPL";
            this.rdATPL.Text = "ATPL Pilot";
            this.rdATPL.UseVisualStyleBackColor = true;
            // 
            // lbAutoCompleteDoctor
            // 
            this.lbAutoCompleteDoctor.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbAutoCompleteDoctor.FormattingEnabled = true;
            this.lbAutoCompleteDoctor.ItemHeight = 16;
            this.lbAutoCompleteDoctor.Location = new System.Drawing.Point(32, 675);
            this.lbAutoCompleteDoctor.Name = "lbAutoCompleteDoctor";
            this.lbAutoCompleteDoctor.ScrollAlwaysVisible = true;
            this.lbAutoCompleteDoctor.Size = new System.Drawing.Size(701, 84);
            this.lbAutoCompleteDoctor.TabIndex = 28;
            this.lbAutoCompleteDoctor.Visible = false;
            this.lbAutoCompleteDoctor.Click += new System.EventHandler(this.lbAutoCompleteDoctor_Click);
            this.lbAutoCompleteDoctor.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbAutoCompleteDoctor_MouseMove);
            // 
            // btnPrint
            // 
            this.btnPrint.Enabled = false;
            this.btnPrint.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnPrint.Location = new System.Drawing.Point(663, 701);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(70, 27);
            this.btnPrint.TabIndex = 29;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bsAviaMedCer, "tram_date_exam", true));
            this.dateTimePicker2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dateTimePicker2.Location = new System.Drawing.Point(160, 71);
            this.dateTimePicker2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(233, 23);
            this.dateTimePicker2.TabIndex = 31;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label12.Location = new System.Drawing.Point(29, 76);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(125, 16);
            this.label12.TabIndex = 30;
            this.label12.Text = "DATE Examinaiton : ";
            // 
            // frmAviationMedCerReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 760);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.lbAutoCompleteDoctor);
            this.Controls.Add(this.pnAS);
            this.Controls.Add(this.pnAirMedCer);
            this.Controls.Add(this.ListAutoComplete);
            this.Controls.Add(this.pnPurpose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtDoctor);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtLimit);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtReccom);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbRptType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPatientName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnPrint);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmAviationMedCerReport";
            this.Text = "Form7";
            this.Load += new System.EventHandler(this.frmAviationMedCerReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bsAviaMedCer)).EndInit();
            this.pnPurpose.ResumeLayout(false);
            this.pnPurpose.PerformLayout();
            this.pnAirMedCer.ResumeLayout(false);
            this.pnAirMedCer.PerformLayout();
            this.pnAS.ResumeLayout(false);
            this.pnAS.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPatientName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbRptType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtReccom;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtLimit;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtDoctor;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ListBox ListAutoComplete;
        private System.Windows.Forms.Panel pnPurpose;
        private System.Windows.Forms.RadioButton rdStudent;
        private System.Windows.Forms.RadioButton rdPreEmp;
        private System.Windows.Forms.RadioButton rdAircrewMed;
        private System.Windows.Forms.Panel pnAirMedCer;
        private System.Windows.Forms.RadioButton rdFPT;
        private System.Windows.Forms.RadioButton rdUPT;
        private System.Windows.Forms.RadioButton rdTUPT;
        private System.Windows.Forms.RadioButton rdFPA;
        private System.Windows.Forms.RadioButton rdUPA;
        private System.Windows.Forms.RadioButton rdTUPA;
        private System.Windows.Forms.Panel pnAS;
        private System.Windows.Forms.RadioButton rdAero;
        private System.Windows.Forms.RadioButton rdCabin;
        private System.Windows.Forms.RadioButton rdPPL;
        private System.Windows.Forms.RadioButton rdCPL;
        private System.Windows.Forms.RadioButton rdATPL;
        private System.Windows.Forms.ListBox lbAutoCompleteDoctor;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.BindingSource bsAviaMedCer;
        private System.Windows.Forms.RadioButton rdOther;
        private System.Windows.Forms.TextBox txtAsRemark;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label12;
    }
}