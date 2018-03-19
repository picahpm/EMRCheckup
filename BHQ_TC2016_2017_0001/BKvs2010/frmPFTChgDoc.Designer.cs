namespace BKvs2010
{
    partial class frmPFTChgDoc
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
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lbTitleResult = new System.Windows.Forms.Label();
            this.lbTitleWaitResult = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDoctorCheck = new System.Windows.Forms.TextBox();
            this.txtArriveDate = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtENno = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtHN = new System.Windows.Forms.TextBox();
            this.GridWaitResult = new System.Windows.Forms.DataGridView();
            this.ColNo1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColHN1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColFullName1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColArriveDate1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColCreateBy1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridReportResult = new System.Windows.Forms.DataGridView();
            this.ColNo2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColHNno2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.DateTimeAlert = new System.Windows.Forms.DateTimePicker();
            this.ch_ResultAlert = new System.Windows.Forms.CheckBox();
            this.btnViewResultByDoctor = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtDocName = new System.Windows.Forms.TextBox();
            this.txtDocCode = new System.Windows.Forms.TextBox();
            this.GridDoctorName = new System.Windows.Forms.DataGridView();
            this.lbMsgAlert = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridWaitResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridReportResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridDoctorName)).BeginInit();
            this.SuspendLayout();
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(382, 228);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(52, 16);
            this.label10.TabIndex = 33;
            this.label10.Text = "Remark";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(368, 174);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 16);
            this.label9.TabIndex = 27;
            this.label9.Text = "Doc Name";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(372, 148);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 16);
            this.label8.TabIndex = 24;
            this.label8.Text = "Doc Code";
            // 
            // lbTitleResult
            // 
            this.lbTitleResult.BackColor = System.Drawing.Color.Blue;
            this.lbTitleResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbTitleResult.ForeColor = System.Drawing.Color.White;
            this.lbTitleResult.Location = new System.Drawing.Point(6, 309);
            this.lbTitleResult.Name = "lbTitleResult";
            this.lbTitleResult.Size = new System.Drawing.Size(772, 25);
            this.lbTitleResult.TabIndex = 23;
            this.lbTitleResult.Text = "รายชื่อผู้ป่วยที่รออ่านผล ทั้งหมด XX คน";
            this.lbTitleResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbTitleWaitResult
            // 
            this.lbTitleWaitResult.BackColor = System.Drawing.Color.Blue;
            this.lbTitleWaitResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbTitleWaitResult.ForeColor = System.Drawing.Color.White;
            this.lbTitleWaitResult.Location = new System.Drawing.Point(6, 6);
            this.lbTitleWaitResult.Name = "lbTitleWaitResult";
            this.lbTitleWaitResult.Size = new System.Drawing.Size(329, 25);
            this.lbTitleWaitResult.TabIndex = 22;
            this.lbTitleWaitResult.Text = "รายชื่อผู้ป่วยที่ส่งตรวจแต่ผลยังไม่ออก ทั้งหมด XX คน";
            this.lbTitleWaitResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 105);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 16);
            this.label7.TabIndex = 9;
            this.label7.Text = "แพทย์ผู้ตรวจ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 79);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 16);
            this.label6.TabIndex = 8;
            this.label6.Text = "Arrive Date";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtDoctorCheck);
            this.groupBox1.Controls.Add(this.txtArriveDate);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.txtENno);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtHN);
            this.groupBox1.Location = new System.Drawing.Point(341, 5);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(437, 133);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Patient Profile";
            // 
            // txtDoctorCheck
            // 
            this.txtDoctorCheck.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDoctorCheck.Location = new System.Drawing.Point(99, 103);
            this.txtDoctorCheck.Name = "txtDoctorCheck";
            this.txtDoctorCheck.Size = new System.Drawing.Size(321, 23);
            this.txtDoctorCheck.TabIndex = 13;
            // 
            // txtArriveDate
            // 
            this.txtArriveDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtArriveDate.Location = new System.Drawing.Point(99, 77);
            this.txtArriveDate.Name = "txtArriveDate";
            this.txtArriveDate.Size = new System.Drawing.Size(112, 23);
            this.txtArriveDate.TabIndex = 12;
            // 
            // txtName
            // 
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.Location = new System.Drawing.Point(99, 51);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(321, 23);
            this.txtName.TabIndex = 11;
            // 
            // txtENno
            // 
            this.txtENno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtENno.Location = new System.Drawing.Point(297, 25);
            this.txtENno.Name = "txtENno";
            this.txtENno.Size = new System.Drawing.Size(123, 23);
            this.txtENno.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(52, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 16);
            this.label5.TabIndex = 7;
            this.label5.Text = "Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(248, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "EN No.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(46, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "HN No.";
            // 
            // txtHN
            // 
            this.txtHN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHN.Location = new System.Drawing.Point(99, 24);
            this.txtHN.Name = "txtHN";
            this.txtHN.Size = new System.Drawing.Size(143, 23);
            this.txtHN.TabIndex = 0;
            // 
            // GridWaitResult
            // 
            this.GridWaitResult.AllowUserToAddRows = false;
            this.GridWaitResult.AllowUserToDeleteRows = false;
            this.GridWaitResult.BackgroundColor = System.Drawing.Color.White;
            this.GridWaitResult.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.GridWaitResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridWaitResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColNo1,
            this.ColHN1,
            this.ColFullName1,
            this.ColArriveDate1,
            this.ColCreateBy1});
            this.GridWaitResult.Location = new System.Drawing.Point(6, 30);
            this.GridWaitResult.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GridWaitResult.Name = "GridWaitResult";
            this.GridWaitResult.ReadOnly = true;
            this.GridWaitResult.RowHeadersVisible = false;
            this.GridWaitResult.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.GridWaitResult.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.GridWaitResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridWaitResult.Size = new System.Drawing.Size(329, 275);
            this.GridWaitResult.TabIndex = 19;
            this.GridWaitResult.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridWaitResult_CellClick);
            this.GridWaitResult.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.GridWaitResult_DataBindingComplete);
            // 
            // ColNo1
            // 
            this.ColNo1.HeaderText = "No";
            this.ColNo1.Name = "ColNo1";
            this.ColNo1.ReadOnly = true;
            this.ColNo1.Width = 30;
            // 
            // ColHN1
            // 
            this.ColHN1.DataPropertyName = "HNno";
            this.ColHN1.HeaderText = "HN";
            this.ColHN1.Name = "ColHN1";
            this.ColHN1.ReadOnly = true;
            // 
            // ColFullName1
            // 
            this.ColFullName1.DataPropertyName = "FullName ";
            this.ColFullName1.HeaderText = "Name";
            this.ColFullName1.Name = "ColFullName1";
            this.ColFullName1.ReadOnly = true;
            // 
            // ColArriveDate1
            // 
            this.ColArriveDate1.DataPropertyName = "ArriveDate";
            this.ColArriveDate1.HeaderText = "Arrive Date";
            this.ColArriveDate1.Name = "ColArriveDate1";
            this.ColArriveDate1.ReadOnly = true;
            // 
            // ColCreateBy1
            // 
            this.ColCreateBy1.DataPropertyName = "CreateBy ";
            this.ColCreateBy1.HeaderText = "Create By";
            this.ColCreateBy1.Name = "ColCreateBy1";
            this.ColCreateBy1.ReadOnly = true;
            // 
            // GridReportResult
            // 
            this.GridReportResult.AllowUserToAddRows = false;
            this.GridReportResult.AllowUserToDeleteRows = false;
            this.GridReportResult.BackgroundColor = System.Drawing.Color.White;
            this.GridReportResult.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.GridReportResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridReportResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColNo2,
            this.ColHNno2,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11,
            this.Column12,
            this.Column13,
            this.Column14});
            this.GridReportResult.Location = new System.Drawing.Point(6, 330);
            this.GridReportResult.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GridReportResult.Name = "GridReportResult";
            this.GridReportResult.ReadOnly = true;
            this.GridReportResult.RowHeadersVisible = false;
            this.GridReportResult.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.GridReportResult.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.GridReportResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridReportResult.Size = new System.Drawing.Size(772, 228);
            this.GridReportResult.TabIndex = 20;
            this.GridReportResult.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridReportResult_CellClick);
            this.GridReportResult.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.GridReportResult_DataBindingComplete);
            // 
            // ColNo2
            // 
            this.ColNo2.HeaderText = "No";
            this.ColNo2.Name = "ColNo2";
            this.ColNo2.ReadOnly = true;
            this.ColNo2.Width = 30;
            // 
            // ColHNno2
            // 
            this.ColHNno2.DataPropertyName = "HN";
            this.ColHNno2.HeaderText = "HN";
            this.ColHNno2.Name = "ColHNno2";
            this.ColHNno2.ReadOnly = true;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Name";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Arrive Date";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "Request By";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "Consult To";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            // 
            // Column12
            // 
            this.Column12.HeaderText = "แจ้งผล";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            // 
            // Column13
            // 
            this.Column13.HeaderText = "วันที่แจ้งผล";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            // 
            // Column14
            // 
            this.Column14.HeaderText = "หมายเหตุ";
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            // 
            // txtRemark
            // 
            this.txtRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemark.Enabled = false;
            this.txtRemark.Location = new System.Drawing.Point(440, 224);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(321, 23);
            this.txtRemark.TabIndex = 40;
            // 
            // DateTimeAlert
            // 
            this.DateTimeAlert.Enabled = false;
            this.DateTimeAlert.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DateTimeAlert.Location = new System.Drawing.Point(561, 198);
            this.DateTimeAlert.Name = "DateTimeAlert";
            this.DateTimeAlert.Size = new System.Drawing.Size(104, 23);
            this.DateTimeAlert.TabIndex = 39;
            // 
            // ch_ResultAlert
            // 
            this.ch_ResultAlert.AutoSize = true;
            this.ch_ResultAlert.Enabled = false;
            this.ch_ResultAlert.Location = new System.Drawing.Point(469, 199);
            this.ch_ResultAlert.Name = "ch_ResultAlert";
            this.ch_ResultAlert.Size = new System.Drawing.Size(64, 20);
            this.ch_ResultAlert.TabIndex = 38;
            this.ch_ResultAlert.Text = "แจ้งผล";
            this.ch_ResultAlert.UseVisualStyleBackColor = true;
            // 
            // btnViewResultByDoctor
            // 
            this.btnViewResultByDoctor.Enabled = false;
            this.btnViewResultByDoctor.Location = new System.Drawing.Point(548, 253);
            this.btnViewResultByDoctor.Name = "btnViewResultByDoctor";
            this.btnViewResultByDoctor.Size = new System.Drawing.Size(157, 25);
            this.btnViewResultByDoctor.TabIndex = 37;
            this.btnViewResultByDoctor.Text = "แพทย์ผู้ตรวจเป็นผู้อ่านผล";
            this.btnViewResultByDoctor.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(440, 254);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(102, 25);
            this.btnSave.TabIndex = 36;
            this.btnSave.Text = "บันทึกข้อมูล";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtDocName
            // 
            this.txtDocName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDocName.Location = new System.Drawing.Point(440, 170);
            this.txtDocName.Name = "txtDocName";
            this.txtDocName.Size = new System.Drawing.Size(321, 23);
            this.txtDocName.TabIndex = 35;
            this.txtDocName.Click += new System.EventHandler(this.txtDocName_Click);
            this.txtDocName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDocName_KeyUp);
            // 
            // txtDocCode
            // 
            this.txtDocCode.BackColor = System.Drawing.Color.White;
            this.txtDocCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDocCode.Location = new System.Drawing.Point(440, 144);
            this.txtDocCode.Name = "txtDocCode";
            this.txtDocCode.ReadOnly = true;
            this.txtDocCode.Size = new System.Drawing.Size(112, 23);
            this.txtDocCode.TabIndex = 34;
            // 
            // GridDoctorName
            // 
            this.GridDoctorName.AllowUserToAddRows = false;
            this.GridDoctorName.AllowUserToDeleteRows = false;
            this.GridDoctorName.BackgroundColor = System.Drawing.Color.White;
            this.GridDoctorName.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.GridDoctorName.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridDoctorName.Location = new System.Drawing.Point(440, 194);
            this.GridDoctorName.Name = "GridDoctorName";
            this.GridDoctorName.ReadOnly = true;
            this.GridDoctorName.RowHeadersVisible = false;
            this.GridDoctorName.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.GridDoctorName.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.GridDoctorName.Size = new System.Drawing.Size(322, 142);
            this.GridDoctorName.TabIndex = 112;
            this.GridDoctorName.Visible = false;
            this.GridDoctorName.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.GridDoctorName_CellMouseClick);
            this.GridDoctorName.Leave += new System.EventHandler(this.GridDoctorName_Leave);
            // 
            // lbMsgAlert
            // 
            this.lbMsgAlert.ForeColor = System.Drawing.Color.Red;
            this.lbMsgAlert.Location = new System.Drawing.Point(341, 282);
            this.lbMsgAlert.Name = "lbMsgAlert";
            this.lbMsgAlert.Size = new System.Drawing.Size(434, 22);
            this.lbMsgAlert.TabIndex = 113;
            this.lbMsgAlert.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmPFTChgDoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.GridDoctorName);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.DateTimeAlert);
            this.Controls.Add(this.ch_ResultAlert);
            this.Controls.Add(this.btnViewResultByDoctor);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtDocName);
            this.Controls.Add(this.txtDocCode);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lbTitleResult);
            this.Controls.Add(this.lbTitleWaitResult);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.GridWaitResult);
            this.Controls.Add(this.GridReportResult);
            this.Controls.Add(this.lbMsgAlert);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmPFTChgDoc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PFT Change Doc";
            this.Load += new System.EventHandler(this.frmPFTChgDoc_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridWaitResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridReportResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridDoctorName)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lbTitleResult;
        private System.Windows.Forms.Label lbTitleWaitResult;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtHN;
        private System.Windows.Forms.DataGridView GridWaitResult;
        private System.Windows.Forms.DataGridView GridReportResult;
        private System.Windows.Forms.TextBox txtDoctorCheck;
        private System.Windows.Forms.TextBox txtArriveDate;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtENno;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.DateTimePicker DateTimeAlert;
        private System.Windows.Forms.CheckBox ch_ResultAlert;
        private System.Windows.Forms.Button btnViewResultByDoctor;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtDocName;
        private System.Windows.Forms.TextBox txtDocCode;
        private System.Windows.Forms.DataGridView GridDoctorName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColNo1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColHN1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColFullName1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColArriveDate1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColCreateBy1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColNo2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColHNno2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.Label lbMsgAlert;
    }
}