namespace BKvs2010
{
    partial class frmMoreDetail_Diagnosis
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMoreDetail_Diagnosis));
            this.GvDiagnosis = new System.Windows.Forms.DataGridView();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblMsgAlert = new System.Windows.Forms.Label();
            this.btnFind = new System.Windows.Forms.Button();
            this.dtpDateE = new System.Windows.Forms.DateTimePicker();
            this.dtpDateS = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.GvDiagnosis)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GvDiagnosis
            // 
            this.GvDiagnosis.AllowUserToAddRows = false;
            this.GvDiagnosis.AllowUserToDeleteRows = false;
            this.GvDiagnosis.BackgroundColor = System.Drawing.Color.White;
            this.GvDiagnosis.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GvDiagnosis.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.GvDiagnosis.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvDiagnosis.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GvDiagnosis.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GvDiagnosis.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column15,
            this.Column17,
            this.Column16,
            this.Column18,
            this.Column19,
            this.Column20});
            this.GvDiagnosis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GvDiagnosis.Location = new System.Drawing.Point(0, 41);
            this.GvDiagnosis.Name = "GvDiagnosis";
            this.GvDiagnosis.RowHeadersVisible = false;
            this.GvDiagnosis.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GvDiagnosis.Size = new System.Drawing.Size(723, 466);
            this.GvDiagnosis.TabIndex = 2;
            // 
            // Column15
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column15.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column15.HeaderText = "No";
            this.Column15.Name = "Column15";
            this.Column15.Width = 30;
            // 
            // Column17
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column17.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column17.HeaderText = "Date";
            this.Column17.Name = "Column17";
            // 
            // Column16
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column16.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column16.HeaderText = "Code";
            this.Column16.Name = "Column16";
            // 
            // Column18
            // 
            this.Column18.HeaderText = "Description";
            this.Column18.Name = "Column18";
            this.Column18.Width = 250;
            // 
            // Column19
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column19.DefaultCellStyle = dataGridViewCellStyle5;
            this.Column19.HeaderText = "Type";
            this.Column19.Name = "Column19";
            // 
            // Column20
            // 
            this.Column20.HeaderText = "Doctor";
            this.Column20.Name = "Column20";
            this.Column20.Width = 130;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblMsgAlert);
            this.panel1.Controls.Add(this.btnFind);
            this.panel1.Controls.Add(this.dtpDateE);
            this.panel1.Controls.Add(this.dtpDateS);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(723, 41);
            this.panel1.TabIndex = 3;
            // 
            // lblMsgAlert
            // 
            this.lblMsgAlert.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblMsgAlert.ForeColor = System.Drawing.Color.Red;
            this.lblMsgAlert.Location = new System.Drawing.Point(413, 9);
            this.lblMsgAlert.Name = "lblMsgAlert";
            this.lblMsgAlert.Size = new System.Drawing.Size(300, 23);
            this.lblMsgAlert.TabIndex = 3;
            this.lblMsgAlert.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnFind
            // 
            this.btnFind.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.btnFind.Image = ((System.Drawing.Image)(resources.GetObject("btnFind.Image")));
            this.btnFind.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFind.Location = new System.Drawing.Point(348, 8);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(59, 25);
            this.btnFind.TabIndex = 2;
            this.btnFind.Text = "Find";
            this.btnFind.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // dtpDateE
            // 
            this.dtpDateE.CustomFormat = "dd/MM/yyyy";
            this.dtpDateE.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dtpDateE.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateE.Location = new System.Drawing.Point(221, 9);
            this.dtpDateE.Name = "dtpDateE";
            this.dtpDateE.Size = new System.Drawing.Size(120, 23);
            this.dtpDateE.TabIndex = 1;
            // 
            // dtpDateS
            // 
            this.dtpDateS.CustomFormat = "dd/MM/yyyy";
            this.dtpDateS.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dtpDateS.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateS.Location = new System.Drawing.Point(57, 9);
            this.dtpDateS.Name = "dtpDateS";
            this.dtpDateS.Size = new System.Drawing.Size(120, 23);
            this.dtpDateS.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(183, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "- To";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "From";
            // 
            // frmMoreDetail_Diagnosis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 507);
            this.Controls.Add(this.GvDiagnosis);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMoreDetail_Diagnosis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ระบบ EMR Check-up Pathway - Diagnosis MoreDetail";
            this.Load += new System.EventHandler(this.frmMoreDetail_Diagnosis_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GvDiagnosis)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView GvDiagnosis;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column17;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column16;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column18;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column19;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column20;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dtpDateE;
        private System.Windows.Forms.DateTimePicker dtpDateS;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Label lblMsgAlert;
    }
}