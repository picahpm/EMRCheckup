namespace BKvs2010.UserControlEMR
{
    partial class AddAdmitionFrm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.GridData = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpdate = new System.Windows.Forms.DateTimePicker();
            this.txttime = new System.Windows.Forms.MaskedTextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnfind = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.bsPatient = new System.Windows.Forms.BindingSource(this.components);
            this.bsAdmition = new System.Windows.Forms.BindingSource(this.components);
            this.AdmDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AdmTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Loc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDoctor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColICD10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.GridData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPatient)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsAdmition)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.LightGreen;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(191, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "Time :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.LightGreen;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(355, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "ICD10 :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.LightGreen;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(12, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 14);
            this.label3.TabIndex = 1;
            this.label3.Text = "Location :";
            // 
            // txtDescription
            // 
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescription.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtDescription.Location = new System.Drawing.Point(409, 33);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(216, 32);
            this.txtDescription.TabIndex = 2;
            // 
            // txtLocation
            // 
            this.txtLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLocation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtLocation.Location = new System.Drawing.Point(87, 33);
            this.txtLocation.Multiline = true;
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(230, 32);
            this.txtLocation.TabIndex = 2;
            // 
            // GridData
            // 
            this.GridData.AllowUserToAddRows = false;
            this.GridData.AllowUserToDeleteRows = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.GridData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.GridData.BackgroundColor = System.Drawing.Color.White;
            this.GridData.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.GridData.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.GridData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.GridData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AdmDate,
            this.AdmTime,
            this.Loc,
            this.ColDoctor,
            this.ColICD10});
            this.GridData.Location = new System.Drawing.Point(8, 71);
            this.GridData.MultiSelect = false;
            this.GridData.Name = "GridData";
            this.GridData.ReadOnly = true;
            this.GridData.RowHeadersVisible = false;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.Blue;
            this.GridData.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.GridData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridData.Size = new System.Drawing.Size(617, 206);
            this.GridData.TabIndex = 71;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.LightGreen;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.Location = new System.Drawing.Point(12, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 14);
            this.label4.TabIndex = 1;
            this.label4.Text = "Date :";
            // 
            // dtpdate
            // 
            this.dtpdate.CustomFormat = "dd/MM/yyyy";
            this.dtpdate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dtpdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpdate.Location = new System.Drawing.Point(87, 7);
            this.dtpdate.Name = "dtpdate";
            this.dtpdate.Size = new System.Drawing.Size(98, 22);
            this.dtpdate.TabIndex = 72;
            // 
            // txttime
            // 
            this.txttime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txttime.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txttime.Location = new System.Drawing.Point(243, 7);
            this.txttime.Mask = "00:00:00";
            this.txttime.Name = "txttime";
            this.txttime.Size = new System.Drawing.Size(74, 22);
            this.txttime.TabIndex = 74;
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClear.Location = new System.Drawing.Point(535, 2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(90, 27);
            this.btnClear.TabIndex = 76;
            this.btnClear.Text = "Clear Find";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnfind
            // 
            this.btnfind.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnfind.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnfind.Location = new System.Drawing.Point(474, 2);
            this.btnfind.Name = "btnfind";
            this.btnfind.Size = new System.Drawing.Size(55, 27);
            this.btnfind.TabIndex = 70;
            this.btnfind.Text = "Find";
            this.btnfind.UseVisualStyleBackColor = true;
            this.btnfind.Click += new System.EventHandler(this.btnfind_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(409, 2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(59, 27);
            this.btnAdd.TabIndex = 70;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // bsPatient
            // 
            this.bsPatient.DataSource = typeof(DBCheckup.trn_patient);
            // 
            // bsAdmition
            // 
            this.bsAdmition.DataMember = "trn_patient_admits";
            this.bsAdmition.DataSource = this.bsPatient;
            // 
            // AdmDate
            // 
            this.AdmDate.DataPropertyName = "tpd_chk_date";
            dataGridViewCellStyle8.Format = "dd/MM/yyyy";
            dataGridViewCellStyle8.NullValue = null;
            this.AdmDate.DefaultCellStyle = dataGridViewCellStyle8;
            this.AdmDate.HeaderText = "Adm Date";
            this.AdmDate.Name = "AdmDate";
            this.AdmDate.ReadOnly = true;
            // 
            // AdmTime
            // 
            this.AdmTime.DataPropertyName = "tpd_chk_date";
            dataGridViewCellStyle9.Format = "T";
            dataGridViewCellStyle9.NullValue = null;
            this.AdmTime.DefaultCellStyle = dataGridViewCellStyle9;
            this.AdmTime.HeaderText = "Adm Time";
            this.AdmTime.Name = "AdmTime";
            this.AdmTime.ReadOnly = true;
            // 
            // Loc
            // 
            this.Loc.DataPropertyName = "tpd_location";
            this.Loc.HeaderText = "Location";
            this.Loc.Name = "Loc";
            this.Loc.ReadOnly = true;
            this.Loc.Width = 150;
            // 
            // ColDoctor
            // 
            this.ColDoctor.DataPropertyName = "tpd_doctor_name";
            this.ColDoctor.HeaderText = "Doctor";
            this.ColDoctor.Name = "ColDoctor";
            this.ColDoctor.ReadOnly = true;
            this.ColDoctor.Width = 250;
            // 
            // ColICD10
            // 
            this.ColICD10.DataPropertyName = "tpd_icd10";
            this.ColICD10.HeaderText = "ICD10";
            this.ColICD10.Name = "ColICD10";
            this.ColICD10.ReadOnly = true;
            this.ColICD10.Width = 248;
            // 
            // AddAdmitionFrm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(634, 285);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.txttime);
            this.Controls.Add(this.dtpdate);
            this.Controls.Add(this.GridData);
            this.Controls.Add(this.btnfind);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtLocation);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddAdmitionFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add New Admition / More Detail";
            ((System.ComponentModel.ISupportInitialize)(this.GridData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPatient)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsAdmition)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnfind;
        public System.Windows.Forms.DataGridView GridData;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpdate;
        private System.Windows.Forms.MaskedTextBox txttime;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.BindingSource bsPatient;
        private System.Windows.Forms.BindingSource bsAdmition;
        private System.Windows.Forms.DataGridViewTextBoxColumn AdmDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn AdmTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Loc;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDoctor;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColICD10;
    }
}