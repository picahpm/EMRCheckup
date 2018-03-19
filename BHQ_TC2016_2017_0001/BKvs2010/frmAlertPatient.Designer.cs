namespace BKvs2010
{
    partial class frmAlertPatient
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAlertPatient));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lbdata = new System.Windows.Forms.Label();
            this.txtPatientAlert = new System.Windows.Forms.TextBox();
            this.btnAddPatientAlert = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.PatientAlertBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.PatientBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.GridPatientAlert = new System.Windows.Forms.DataGridView();
            this.GridPatientAlertSearch = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tpa_alert = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tpaidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tptidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mutidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tpaalertDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tpastatusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tpa_create_by = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tpacreatedateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tpaupdatebyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Colfullname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tpaupdatedateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trnpatientDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.PatientAlertBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PatientBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridPatientAlert)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridPatientAlertSearch)).BeginInit();
            this.SuspendLayout();
            // 
            // lbdata
            // 
            this.lbdata.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbdata.Location = new System.Drawing.Point(12, 187);
            this.lbdata.Name = "lbdata";
            this.lbdata.Size = new System.Drawing.Size(204, 33);
            this.lbdata.TabIndex = 0;
            // 
            // txtPatientAlert
            // 
            this.txtPatientAlert.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPatientAlert.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPatientAlert.Location = new System.Drawing.Point(5, 7);
            this.txtPatientAlert.Name = "txtPatientAlert";
            this.txtPatientAlert.Size = new System.Drawing.Size(426, 20);
            this.txtPatientAlert.TabIndex = 32;
            this.txtPatientAlert.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtPatientAlert_MouseClick);
            this.txtPatientAlert.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPatientAlert_KeyUp);
            // 
            // btnAddPatientAlert
            // 
            this.btnAddPatientAlert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddPatientAlert.Location = new System.Drawing.Point(434, 7);
            this.btnAddPatientAlert.Name = "btnAddPatientAlert";
            this.btnAddPatientAlert.Size = new System.Drawing.Size(33, 21);
            this.btnAddPatientAlert.TabIndex = 33;
            this.btnAddPatientAlert.Text = "+";
            this.btnAddPatientAlert.UseVisualStyleBackColor = true;
            this.btnAddPatientAlert.Click += new System.EventHandler(this.btnAddPatientAlert_Click);
            // 
            // btnSave
            // 
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(362, 221);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(105, 32);
            this.btnSave.TabIndex = 35;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // PatientAlertBindingSource
            // 
            this.PatientAlertBindingSource.DataMember = "trn_patient_alerts";
            this.PatientAlertBindingSource.DataSource = this.PatientBindingSource;
            // 
            // PatientBindingSource
            // 
            this.PatientBindingSource.DataSource = typeof(DBCheckup.trn_patient);
            // 
            // GridPatientAlert
            // 
            this.GridPatientAlert.AllowUserToAddRows = false;
            this.GridPatientAlert.AllowUserToDeleteRows = false;
            this.GridPatientAlert.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GridPatientAlert.AutoGenerateColumns = false;
            this.GridPatientAlert.BackgroundColor = System.Drawing.Color.White;
            this.GridPatientAlert.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridPatientAlert.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GridPatientAlert.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridPatientAlert.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tpa_alert,
            this.tpaidDataGridViewTextBoxColumn,
            this.tptidDataGridViewTextBoxColumn,
            this.mutidDataGridViewTextBoxColumn,
            this.tpaalertDataGridViewTextBoxColumn,
            this.tpastatusDataGridViewTextBoxColumn,
            this.tpa_create_by,
            this.tpacreatedateDataGridViewTextBoxColumn,
            this.tpaupdatebyDataGridViewTextBoxColumn,
            this.Colfullname,
            this.tpaupdatedateDataGridViewTextBoxColumn,
            this.trnpatientDataGridViewTextBoxColumn});
            this.GridPatientAlert.DataSource = this.PatientAlertBindingSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridPatientAlert.DefaultCellStyle = dataGridViewCellStyle2;
            this.GridPatientAlert.Location = new System.Drawing.Point(5, 34);
            this.GridPatientAlert.MultiSelect = false;
            this.GridPatientAlert.Name = "GridPatientAlert";
            this.GridPatientAlert.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridPatientAlert.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.GridPatientAlert.RowHeadersVisible = false;
            this.GridPatientAlert.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.GridPatientAlert.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridPatientAlert.Size = new System.Drawing.Size(461, 181);
            this.GridPatientAlert.TabIndex = 36;
            this.GridPatientAlert.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridPatientAlert_CellContentClick);
            this.GridPatientAlert.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.GridPatientAlert_DataBindingComplete);
            // 
            // GridPatientAlertSearch
            // 
            this.GridPatientAlertSearch.AllowUserToAddRows = false;
            this.GridPatientAlertSearch.AllowUserToDeleteRows = false;
            this.GridPatientAlertSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GridPatientAlertSearch.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridPatientAlertSearch.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.GridPatientAlertSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridPatientAlertSearch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridPatientAlertSearch.DefaultCellStyle = dataGridViewCellStyle5;
            this.GridPatientAlertSearch.Location = new System.Drawing.Point(5, 57);
            this.GridPatientAlertSearch.Name = "GridPatientAlertSearch";
            this.GridPatientAlertSearch.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridPatientAlertSearch.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.GridPatientAlertSearch.RowHeadersVisible = false;
            this.GridPatientAlertSearch.Size = new System.Drawing.Size(461, 187);
            this.GridPatientAlertSearch.TabIndex = 41;
            this.GridPatientAlertSearch.Visible = false;
            this.GridPatientAlertSearch.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridPatientAlertSearch_CellClick);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.DataPropertyName = "MessageAlert";
            this.Column1.HeaderText = "Message Alert";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // tpa_alert
            // 
            this.tpa_alert.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.tpa_alert.DataPropertyName = "tpa_alert";
            this.tpa_alert.HeaderText = "Message";
            this.tpa_alert.Name = "tpa_alert";
            this.tpa_alert.ReadOnly = true;
            this.tpa_alert.Visible = false;
            // 
            // tpaidDataGridViewTextBoxColumn
            // 
            this.tpaidDataGridViewTextBoxColumn.DataPropertyName = "tpa_id";
            this.tpaidDataGridViewTextBoxColumn.HeaderText = "tpa_id";
            this.tpaidDataGridViewTextBoxColumn.Name = "tpaidDataGridViewTextBoxColumn";
            this.tpaidDataGridViewTextBoxColumn.ReadOnly = true;
            this.tpaidDataGridViewTextBoxColumn.Visible = false;
            // 
            // tptidDataGridViewTextBoxColumn
            // 
            this.tptidDataGridViewTextBoxColumn.DataPropertyName = "tpt_id";
            this.tptidDataGridViewTextBoxColumn.HeaderText = "tpt_id";
            this.tptidDataGridViewTextBoxColumn.Name = "tptidDataGridViewTextBoxColumn";
            this.tptidDataGridViewTextBoxColumn.ReadOnly = true;
            this.tptidDataGridViewTextBoxColumn.Visible = false;
            // 
            // mutidDataGridViewTextBoxColumn
            // 
            this.mutidDataGridViewTextBoxColumn.DataPropertyName = "mut_id";
            this.mutidDataGridViewTextBoxColumn.HeaderText = "mut_id";
            this.mutidDataGridViewTextBoxColumn.Name = "mutidDataGridViewTextBoxColumn";
            this.mutidDataGridViewTextBoxColumn.ReadOnly = true;
            this.mutidDataGridViewTextBoxColumn.Visible = false;
            // 
            // tpaalertDataGridViewTextBoxColumn
            // 
            this.tpaalertDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.tpaalertDataGridViewTextBoxColumn.DataPropertyName = "tpa_alert";
            this.tpaalertDataGridViewTextBoxColumn.HeaderText = "Patient Alert Message";
            this.tpaalertDataGridViewTextBoxColumn.Name = "tpaalertDataGridViewTextBoxColumn";
            this.tpaalertDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tpastatusDataGridViewTextBoxColumn
            // 
            this.tpastatusDataGridViewTextBoxColumn.DataPropertyName = "tpa_status";
            this.tpastatusDataGridViewTextBoxColumn.HeaderText = "tpa_status";
            this.tpastatusDataGridViewTextBoxColumn.Name = "tpastatusDataGridViewTextBoxColumn";
            this.tpastatusDataGridViewTextBoxColumn.ReadOnly = true;
            this.tpastatusDataGridViewTextBoxColumn.Visible = false;
            // 
            // tpa_create_by
            // 
            this.tpa_create_by.DataPropertyName = "tpa_create_by";
            this.tpa_create_by.HeaderText = "tpa_create_by";
            this.tpa_create_by.Name = "tpa_create_by";
            this.tpa_create_by.ReadOnly = true;
            this.tpa_create_by.Visible = false;
            // 
            // tpacreatedateDataGridViewTextBoxColumn
            // 
            this.tpacreatedateDataGridViewTextBoxColumn.DataPropertyName = "tpa_create_date";
            this.tpacreatedateDataGridViewTextBoxColumn.HeaderText = "tpa_create_date";
            this.tpacreatedateDataGridViewTextBoxColumn.Name = "tpacreatedateDataGridViewTextBoxColumn";
            this.tpacreatedateDataGridViewTextBoxColumn.ReadOnly = true;
            this.tpacreatedateDataGridViewTextBoxColumn.Visible = false;
            // 
            // tpaupdatebyDataGridViewTextBoxColumn
            // 
            this.tpaupdatebyDataGridViewTextBoxColumn.DataPropertyName = "tpa_update_by";
            this.tpaupdatebyDataGridViewTextBoxColumn.HeaderText = "IDUpdate By";
            this.tpaupdatebyDataGridViewTextBoxColumn.Name = "tpaupdatebyDataGridViewTextBoxColumn";
            this.tpaupdatebyDataGridViewTextBoxColumn.ReadOnly = true;
            this.tpaupdatebyDataGridViewTextBoxColumn.Visible = false;
            // 
            // Colfullname
            // 
            this.Colfullname.HeaderText = "Create By";
            this.Colfullname.Name = "Colfullname";
            this.Colfullname.ReadOnly = true;
            this.Colfullname.Width = 150;
            // 
            // tpaupdatedateDataGridViewTextBoxColumn
            // 
            this.tpaupdatedateDataGridViewTextBoxColumn.DataPropertyName = "tpa_update_date";
            this.tpaupdatedateDataGridViewTextBoxColumn.HeaderText = "tpa_update_date";
            this.tpaupdatedateDataGridViewTextBoxColumn.Name = "tpaupdatedateDataGridViewTextBoxColumn";
            this.tpaupdatedateDataGridViewTextBoxColumn.ReadOnly = true;
            this.tpaupdatedateDataGridViewTextBoxColumn.Visible = false;
            // 
            // trnpatientDataGridViewTextBoxColumn
            // 
            this.trnpatientDataGridViewTextBoxColumn.DataPropertyName = "trn_patient";
            this.trnpatientDataGridViewTextBoxColumn.HeaderText = "trn_patient";
            this.trnpatientDataGridViewTextBoxColumn.Name = "trnpatientDataGridViewTextBoxColumn";
            this.trnpatientDataGridViewTextBoxColumn.ReadOnly = true;
            this.trnpatientDataGridViewTextBoxColumn.Visible = false;
            // 
            // frmAlertPatient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 256);
            this.Controls.Add(this.GridPatientAlertSearch);
            this.Controls.Add(this.GridPatientAlert);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtPatientAlert);
            this.Controls.Add(this.btnAddPatientAlert);
            this.Controls.Add(this.lbdata);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAlertPatient";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Partient Alert";
            this.Load += new System.EventHandler(this.frmAlertPatient_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PatientAlertBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PatientBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridPatientAlert)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridPatientAlertSearch)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbdata;
        private System.Windows.Forms.TextBox txtPatientAlert;
        private System.Windows.Forms.Button btnAddPatientAlert;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.BindingSource PatientAlertBindingSource;
        private System.Windows.Forms.DataGridView GridPatientAlert;
        private System.Windows.Forms.BindingSource PatientBindingSource;
        private System.Windows.Forms.DataGridView GridPatientAlertSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn tpa_alert;
        private System.Windows.Forms.DataGridViewTextBoxColumn tpaidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tptidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mutidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tpaalertDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tpastatusDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tpa_create_by;
        private System.Windows.Forms.DataGridViewTextBoxColumn tpacreatedateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tpaupdatebyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Colfullname;
        private System.Windows.Forms.DataGridViewTextBoxColumn tpaupdatedateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn trnpatientDataGridViewTextBoxColumn;
    }
}