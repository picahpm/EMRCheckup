namespace BKvs2010
{
    partial class frmChoiceRoomCancel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChoiceRoomCancel));
            this.gridStation = new System.Windows.Forms.DataGridView();
            this.Colselect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colsite = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRoomid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colmvtid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.CBReason = new System.Windows.Forms.ComboBox();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridStation)).BeginInit();
            this.SuspendLayout();
            // 
            // gridStation
            // 
            this.gridStation.AllowUserToAddRows = false;
            this.gridStation.AllowUserToDeleteRows = false;
            this.gridStation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridStation.BackgroundColor = System.Drawing.Color.White;
            this.gridStation.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridStation.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridStation.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Colselect,
            this.Column1,
            this.Column2,
            this.colsite,
            this.Column3,
            this.Column4,
            this.colRoomid,
            this.colmvtid});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridStation.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridStation.Location = new System.Drawing.Point(3, 2);
            this.gridStation.Name = "gridStation";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridStation.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gridStation.RowHeadersVisible = false;
            this.gridStation.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.gridStation.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.gridStation.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.gridStation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridStation.Size = new System.Drawing.Size(568, 227);
            this.gridStation.TabIndex = 6;
            // 
            // Colselect
            // 
            this.Colselect.HeaderText = "select";
            this.Colselect.Name = "Colselect";
            this.Colselect.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Colselect.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Colselect.Width = 45;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column1.DataPropertyName = "mrm_ename";
            this.Column1.HeaderText = "Room Name";
            this.Column1.Name = "Column1";
            this.Column1.Width = 150;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "mze_ename";
            this.Column2.HeaderText = "Zone Name";
            this.Column2.Name = "Column2";
            // 
            // colsite
            // 
            this.colsite.DataPropertyName = "mhs_ename";
            this.colsite.HeaderText = "HPC Site Name";
            this.colsite.Name = "colsite";
            this.colsite.Width = 80;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "waiting_person";
            this.Column3.HeaderText = "Waiting Person";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "waiting_time";
            this.Column4.HeaderText = "Waiting Time";
            this.Column4.Name = "Column4";
            this.Column4.Width = 80;
            // 
            // colRoomid
            // 
            this.colRoomid.DataPropertyName = "mrm_id";
            this.colRoomid.HeaderText = "Roomid";
            this.colRoomid.Name = "colRoomid";
            this.colRoomid.Visible = false;
            // 
            // colmvtid
            // 
            this.colmvtid.DataPropertyName = "mvt_id";
            this.colmvtid.HeaderText = "mvtid";
            this.colmvtid.Name = "colmvtid";
            this.colmvtid.Visible = false;
            this.colmvtid.Width = 5;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(486, 325);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(83, 25);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(403, 325);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(77, 25);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "OK";
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // CBReason
            // 
            this.CBReason.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CBReason.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.CBReason.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CBReason.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.CBReason.FormattingEnabled = true;
            this.CBReason.Location = new System.Drawing.Point(112, 235);
            this.CBReason.Name = "CBReason";
            this.CBReason.Size = new System.Drawing.Size(259, 24);
            this.CBReason.TabIndex = 10;
            // 
            // txtReason
            // 
            this.txtReason.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtReason.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReason.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtReason.Location = new System.Drawing.Point(12, 262);
            this.txtReason.Multiline = true;
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(359, 88);
            this.txtReason.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(12, 238);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "Select Reason :";
            // 
            // frmChoiceRoomCancel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 362);
            this.ControlBox = false;
            this.Controls.Add(this.CBReason);
            this.Controls.Add(this.txtReason);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.gridStation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmChoiceRoomCancel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Choice Room Cancel";
            this.Load += new System.EventHandler(this.frmChoiceRoomCancel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridStation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridStation;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Colselect;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colsite;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRoomid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colmvtid;
        private System.Windows.Forms.ComboBox CBReason;
        private System.Windows.Forms.TextBox txtReason;
        private System.Windows.Forms.Label label2;
    }
}