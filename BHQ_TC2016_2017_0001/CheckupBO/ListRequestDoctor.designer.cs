namespace CheckupBO
{
    partial class ListRequestDoctor
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
            this.CurrentDate = new System.Windows.Forms.DateTimePicker();
            this.cmbCondition = new System.Windows.Forms.ComboBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.gridDetail = new System.Windows.Forms.DataGridView();
            this.colNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQueue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPatientName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGenderDoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReqDocName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCreateRegis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTimeRegis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDocBefore = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDocAfter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCreateCheckC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTimeCheckC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // CurrentDate
            // 
            this.CurrentDate.Checked = false;
            this.CurrentDate.Location = new System.Drawing.Point(978, 13);
            this.CurrentDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CurrentDate.Name = "CurrentDate";
            this.CurrentDate.Size = new System.Drawing.Size(207, 23);
            this.CurrentDate.TabIndex = 0;
            // 
            // cmbCondition
            // 
            this.cmbCondition.FormattingEnabled = true;
            this.cmbCondition.Location = new System.Drawing.Point(12, 12);
            this.cmbCondition.Name = "cmbCondition";
            this.cmbCondition.Size = new System.Drawing.Size(196, 24);
            this.cmbCondition.TabIndex = 1;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(214, 12);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(503, 23);
            this.txtSearch.TabIndex = 2;
            // 
            // gridDetail
            // 
            this.gridDetail.AllowUserToAddRows = false;
            this.gridDetail.AllowUserToDeleteRows = false;
            this.gridDetail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.gridDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNo,
            this.colQueue,
            this.colHN,
            this.colPatientName,
            this.colGenderDoc,
            this.colReqDocName,
            this.colCreateRegis,
            this.colTimeRegis,
            this.colDocBefore,
            this.colDocAfter,
            this.colCreateCheckC,
            this.colTimeCheckC});
            this.gridDetail.Location = new System.Drawing.Point(12, 42);
            this.gridDetail.Name = "gridDetail";
            this.gridDetail.Size = new System.Drawing.Size(1173, 544);
            this.gridDetail.TabIndex = 3;
            // 
            // colNo
            // 
            this.colNo.HeaderText = "No.";
            this.colNo.Name = "colNo";
            this.colNo.ReadOnly = true;
            this.colNo.Width = 52;
            // 
            // colQueue
            // 
            this.colQueue.HeaderText = "Queue";
            this.colQueue.Name = "colQueue";
            this.colQueue.ReadOnly = true;
            this.colQueue.Width = 70;
            // 
            // colHN
            // 
            this.colHN.HeaderText = "HN";
            this.colHN.Name = "colHN";
            this.colHN.ReadOnly = true;
            this.colHN.Width = 49;
            // 
            // colPatientName
            // 
            this.colPatientName.HeaderText = "Patient Name";
            this.colPatientName.Name = "colPatientName";
            this.colPatientName.ReadOnly = true;
            // 
            // colGenderDoc
            // 
            this.colGenderDoc.HeaderText = "Gender Doctor";
            this.colGenderDoc.Name = "colGenderDoc";
            this.colGenderDoc.ReadOnly = true;
            this.colGenderDoc.Width = 106;
            // 
            // colReqDocName
            // 
            this.colReqDocName.HeaderText = "Request Doctor Name";
            this.colReqDocName.Name = "colReqDocName";
            this.colReqDocName.ReadOnly = true;
            this.colReqDocName.Width = 98;
            // 
            // colCreateRegis
            // 
            this.colCreateRegis.HeaderText = "Create By";
            this.colCreateRegis.Name = "colCreateRegis";
            this.colCreateRegis.ReadOnly = true;
            this.colCreateRegis.Width = 81;
            // 
            // colTimeRegis
            // 
            this.colTimeRegis.HeaderText = "Time";
            this.colTimeRegis.Name = "colTimeRegis";
            this.colTimeRegis.ReadOnly = true;
            this.colTimeRegis.Width = 62;
            // 
            // colDocBefore
            // 
            this.colDocBefore.HeaderText = "Doctor Before";
            this.colDocBefore.Name = "colDocBefore";
            this.colDocBefore.ReadOnly = true;
            this.colDocBefore.Width = 102;
            // 
            // colDocAfter
            // 
            this.colDocAfter.HeaderText = "Doctor After";
            this.colDocAfter.Name = "colDocAfter";
            this.colDocAfter.ReadOnly = true;
            this.colDocAfter.Width = 94;
            // 
            // colCreateCheckC
            // 
            this.colCreateCheckC.HeaderText = "Create By";
            this.colCreateCheckC.Name = "colCreateCheckC";
            this.colCreateCheckC.ReadOnly = true;
            this.colCreateCheckC.Width = 81;
            // 
            // colTimeCheckC
            // 
            this.colTimeCheckC.HeaderText = "Time";
            this.colTimeCheckC.Name = "colTimeCheckC";
            this.colTimeCheckC.ReadOnly = true;
            this.colTimeCheckC.Width = 62;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(723, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 24);
            this.button1.TabIndex = 4;
            this.button1.Text = "Search";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // ListRequestDoctor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1197, 598);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.gridDetail);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.cmbCondition);
            this.Controls.Add(this.CurrentDate);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ListRequestDoctor";
            this.Text = "ListRequestDoctor";
            ((System.ComponentModel.ISupportInitialize)(this.gridDetail)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker CurrentDate;
        private System.Windows.Forms.ComboBox cmbCondition;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.DataGridView gridDetail;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQueue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHN;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPatientName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGenderDoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReqDocName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCreateRegis;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTimeRegis;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDocBefore;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDocAfter;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCreateCheckC;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTimeCheckC;
        private System.Windows.Forms.Button button1;
    }
}