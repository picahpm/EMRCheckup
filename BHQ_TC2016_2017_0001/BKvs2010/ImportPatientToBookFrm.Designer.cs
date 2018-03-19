namespace BKvs2010
{
    partial class ImportPatientToBookFrm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSearchHN = new UserControlLibrary.TextBoxDataType();
            this.txtSearchEN = new UserControlLibrary.TextBoxDataType();
            this.gridStation = new System.Windows.Forms.DataGridView();
            this.colChk = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colStation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_mvt_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbAlertMsg = new UserControlLibrary.LabelAutoResizeFont();
            this.patientProfileUC1 = new BKvs2010.Usercontrols.PatientProfileUC();
            ((System.ComponentModel.ISupportInitialize)(this.gridStation)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(478, 10);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(70, 25);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnImport
            // 
            this.btnImport.Enabled = false;
            this.btnImport.Location = new System.Drawing.Point(478, 325);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(69, 25);
            this.btnImport.TabIndex = 3;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(278, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "HN :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(278, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "EN :";
            // 
            // txtSearchHN
            // 
            this.txtSearchHN.BackColor = System.Drawing.Color.LightYellow;
            this.txtSearchHN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearchHN.ForeColor = System.Drawing.Color.Black;
            this.txtSearchHN.Location = new System.Drawing.Point(318, 12);
            this.txtSearchHN.Name = "txtSearchHN";
            this.txtSearchHN.Size = new System.Drawing.Size(142, 22);
            this.txtSearchHN.TabIndex = 23;
            this.txtSearchHN.DataType = UserControlLibrary.TextBoxDataType.TypeData.String;
            // 
            // txtSearchEN
            // 
            this.txtSearchEN.BackColor = System.Drawing.Color.LightYellow;
            this.txtSearchEN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearchEN.ForeColor = System.Drawing.Color.Black;
            this.txtSearchEN.Location = new System.Drawing.Point(318, 40);
            this.txtSearchEN.Name = "txtSearchEN";
            this.txtSearchEN.Size = new System.Drawing.Size(142, 22);
            this.txtSearchEN.TabIndex = 24;
            this.txtSearchEN.DataType = UserControlLibrary.TextBoxDataType.TypeData.String;
            // 
            // gridStation
            // 
            this.gridStation.AllowUserToAddRows = false;
            this.gridStation.AllowUserToDeleteRows = false;
            this.gridStation.AllowUserToResizeRows = false;
            this.gridStation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridStation.BackgroundColor = System.Drawing.Color.White;
            this.gridStation.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gridStation.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridStation.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridStation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridStation.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChk,
            this.colStation,
            this.col_mvt_id,
            this.colStatus});
            this.gridStation.Location = new System.Drawing.Point(281, 68);
            this.gridStation.Name = "gridStation";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridStation.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.gridStation.RowHeadersVisible = false;
            this.gridStation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridStation.Size = new System.Drawing.Size(267, 251);
            this.gridStation.TabIndex = 109;
            // 
            // colChk
            // 
            this.colChk.HeaderText = "";
            this.colChk.Name = "colChk";
            this.colChk.ReadOnly = true;
            this.colChk.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colChk.Width = 30;
            // 
            // colStation
            // 
            this.colStation.DataPropertyName = "station_name";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.colStation.DefaultCellStyle = dataGridViewCellStyle2;
            this.colStation.HeaderText = "Station";
            this.colStation.Name = "colStation";
            this.colStation.ReadOnly = true;
            this.colStation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colStation.Width = 168;
            // 
            // col_mvt_id
            // 
            this.col_mvt_id.DataPropertyName = "mvt_id";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Format = "dd/MM/yyyy";
            this.col_mvt_id.DefaultCellStyle = dataGridViewCellStyle3;
            this.col_mvt_id.HeaderText = "mvt_id";
            this.col_mvt_id.Name = "col_mvt_id";
            this.col_mvt_id.ReadOnly = true;
            this.col_mvt_id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.col_mvt_id.Visible = false;
            this.col_mvt_id.Width = 120;
            // 
            // colStatus
            // 
            this.colStatus.DataPropertyName = "status";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colStatus.DefaultCellStyle = dataGridViewCellStyle4;
            this.colStatus.HeaderText = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            this.colStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colStatus.Width = 50;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(460, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 20);
            this.label3.TabIndex = 110;
            this.label3.Text = "*";
            this.label3.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(460, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 20);
            this.label4.TabIndex = 111;
            this.label4.Text = "*";
            this.label4.Visible = false;
            // 
            // lbAlertMsg
            // 
            this.lbAlertMsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbAlertMsg.AutoResizeFont = true;
            this.lbAlertMsg.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbAlertMsg.ForeColor = System.Drawing.Color.Red;
            this.lbAlertMsg.Location = new System.Drawing.Point(278, 325);
            this.lbAlertMsg.MaxFontSize = 11.5F;
            this.lbAlertMsg.Name = "lbAlertMsg";
            this.lbAlertMsg.Size = new System.Drawing.Size(197, 29);
            this.lbAlertMsg.TabIndex = 112;
            this.lbAlertMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // patientProfileUC1
            // 
            this.patientProfileUC1.BackColor = System.Drawing.Color.Transparent;
            this.patientProfileUC1.Location = new System.Drawing.Point(-1, -1);
            this.patientProfileUC1.Name = "patientProfileUC1";
            this.patientProfileUC1.row_id = null;
            this.patientProfileUC1.Size = new System.Drawing.Size(270, 361);
            this.patientProfileUC1.TabIndex = 0;
            this.patientProfileUC1.tpr_id = null;
            // 
            // ImportPatientToBookFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(559, 359);
            this.Controls.Add(this.lbAlertMsg);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtSearchEN);
            this.Controls.Add(this.txtSearchHN);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.patientProfileUC1);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.gridStation);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportPatientToBookFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ImportPatientToBookFrm";
            ((System.ComponentModel.ISupportInitialize)(this.gridStation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Usercontrols.PatientProfileUC patientProfileUC1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private UserControlLibrary.TextBoxDataType txtSearchHN;
        private UserControlLibrary.TextBoxDataType txtSearchEN;
        private System.Windows.Forms.DataGridView gridStation;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colChk;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStation;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_mvt_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private UserControlLibrary.LabelAutoResizeFont lbAlertMsg;




    }
}