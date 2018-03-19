namespace CheckupBO
{
    partial class frmPolicyEdit
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
            this.GridUsertype = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.chkSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Colmsh_ename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbAlertMessage = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnsearch = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.GridUsertype)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // GridUsertype
            // 
            this.GridUsertype.AllowUserToAddRows = false;
            this.GridUsertype.AllowUserToDeleteRows = false;
            this.GridUsertype.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.GridUsertype.BackgroundColor = System.Drawing.Color.White;
            this.GridUsertype.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.GridUsertype.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridUsertype.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            this.GridUsertype.Location = new System.Drawing.Point(3, 53);
            this.GridUsertype.Name = "GridUsertype";
            this.GridUsertype.RowHeadersVisible = false;
            this.GridUsertype.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.GridUsertype.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.GridUsertype.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridUsertype.Size = new System.Drawing.Size(454, 266);
            this.GridUsertype.TabIndex = 2;
            this.GridUsertype.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridUsertype_CellClick);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "no";
            this.Column1.HeaderText = "No.";
            this.Column1.Name = "Column1";
            this.Column1.Width = 35;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "UserID";
            this.Column2.HeaderText = "ID";
            this.Column2.Name = "Column2";
            this.Column2.Width = 60;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "UserName";
            this.Column3.HeaderText = "UserName";
            this.Column3.Name = "Column3";
            this.Column3.Width = 80;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "FullName";
            this.Column4.HeaderText = "FullName";
            this.Column4.Name = "Column4";
            this.Column4.Width = 180;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "UserType";
            this.Column5.HeaderText = "UserType";
            this.Column5.Name = "Column5";
            this.Column5.Width = 80;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(434, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(246, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "สิทธิการเข้าใช้ห้องต่างๆของ User ต่างๆ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(3, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "ข้อมูลผู้ใช้งาน";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(757, 327);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(98, 31);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(861, 327);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(98, 31);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chkSelect,
            this.Colmsh_ename,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9});
            this.dataGridView1.Location = new System.Drawing.Point(463, 29);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(487, 290);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // chkSelect
            // 
            this.chkSelect.HeaderText = "Select";
            this.chkSelect.Name = "chkSelect";
            this.chkSelect.ReadOnly = true;
            this.chkSelect.Width = 40;
            // 
            // Colmsh_ename
            // 
            this.Colmsh_ename.DataPropertyName = "mhs_ename";
            this.Colmsh_ename.HeaderText = "Site";
            this.Colmsh_ename.Name = "Colmsh_ename";
            this.Colmsh_ename.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "mrm_id";
            this.Column6.HeaderText = "mrm_id";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column6.Width = 70;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "mrm_ename";
            this.Column7.HeaderText = "mrm_ename";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column7.Width = 180;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "mrm_effective_date";
            this.Column8.HeaderText = "mrm_effective_date";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column8.Width = 130;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "mrm_expire_date";
            this.Column9.HeaderText = "mrm_expire_date";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column9.Width = 130;
            // 
            // lbAlertMessage
            // 
            this.lbAlertMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbAlertMessage.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbAlertMessage.ForeColor = System.Drawing.Color.Red;
            this.lbAlertMessage.Location = new System.Drawing.Point(12, 329);
            this.lbAlertMessage.Name = "lbAlertMessage";
            this.lbAlertMessage.Size = new System.Drawing.Size(739, 27);
            this.lbAlertMessage.TabIndex = 7;
            this.lbAlertMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Location = new System.Drawing.Point(6, 29);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(159, 20);
            this.txtSearch.TabIndex = 8;
            // 
            // btnsearch
            // 
            this.btnsearch.Location = new System.Drawing.Point(172, 25);
            this.btnsearch.Name = "btnsearch";
            this.btnsearch.Size = new System.Drawing.Size(75, 23);
            this.btnsearch.TabIndex = 9;
            this.btnsearch.Text = "Search";
            this.btnsearch.UseVisualStyleBackColor = true;
            this.btnsearch.Click += new System.EventHandler(this.btnsearch_Click);
            // 
            // frmPolicyEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 371);
            this.Controls.Add(this.btnsearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lbAlertMessage);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.GridUsertype);
            this.Name = "frmPolicyEdit";
            this.Text = "Policy Edit";
            this.Load += new System.EventHandler(this.frmPolicyEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GridUsertype)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView GridUsertype;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chkSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn Colmsh_ename;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.Label lbAlertMessage;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnsearch;

    }
}