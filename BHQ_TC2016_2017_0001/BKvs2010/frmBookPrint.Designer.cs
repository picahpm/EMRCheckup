namespace BKvs2010
{
    partial class frmBookPrint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBookPrint));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.chkBF = new System.Windows.Forms.CheckBox();
            this.chkBP = new System.Windows.Forms.CheckBox();
            this.chkBC = new System.Windows.Forms.CheckBox();
            this.chkNB = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column9 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colArrive = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLanguage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPackage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPreview = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colPrintDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrintBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colConfirmDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.confirmBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFinish = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_tpb_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.btnPrintAll = new System.Windows.Forms.Button();
            this.lbAlert = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtSearch.Location = new System.Drawing.Point(192, 17);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(393, 23);
            this.txtSearch.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.chkBF);
            this.groupBox1.Controls.Add(this.chkBP);
            this.groupBox1.Controls.Add(this.chkBC);
            this.groupBox1.Controls.Add(this.chkNB);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dateTimePicker2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtSearch);
            this.groupBox1.Location = new System.Drawing.Point(12, 6);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(832, 103);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search";
            // 
            // btnClear
            // 
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClear.Location = new System.Drawing.Point(661, 67);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 12;
            this.btnClear.Text = "Default";
            this.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(661, 40);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 11;
            this.btnSearch.Text = "Search";
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(142, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "Status";
            // 
            // chkBF
            // 
            this.chkBF.Checked = true;
            this.chkBF.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBF.Location = new System.Drawing.Point(554, 70);
            this.chkBF.Name = "chkBF";
            this.chkBF.Size = new System.Drawing.Size(87, 20);
            this.chkBF.TabIndex = 9;
            this.chkBF.Text = "Complete";
            this.chkBF.UseVisualStyleBackColor = true;
            // 
            // chkBP
            // 
            this.chkBP.Checked = true;
            this.chkBP.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBP.Location = new System.Drawing.Point(456, 70);
            this.chkBP.Name = "chkBP";
            this.chkBP.Size = new System.Drawing.Size(91, 20);
            this.chkBP.TabIndex = 8;
            this.chkBP.Text = "Book Print";
            this.chkBP.UseVisualStyleBackColor = true;
            // 
            // chkBC
            // 
            this.chkBC.Checked = true;
            this.chkBC.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBC.Location = new System.Drawing.Point(353, 70);
            this.chkBC.Name = "chkBC";
            this.chkBC.Size = new System.Drawing.Size(97, 20);
            this.chkBC.TabIndex = 7;
            this.chkBC.Text = "Approved";
            this.chkBC.UseVisualStyleBackColor = true;
            // 
            // chkNB
            // 
            this.chkNB.Checked = true;
            this.chkNB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNB.Location = new System.Drawing.Point(192, 70);
            this.chkNB.Name = "chkNB";
            this.chkNB.Size = new System.Drawing.Size(155, 20);
            this.chkNB.TabIndex = 6;
            this.chkNB.Text = "Waiting for Approved";
            this.chkNB.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(341, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Confirm Book Date";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Checked = false;
            this.dateTimePicker2.CustomFormat = " ";
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Location = new System.Drawing.Point(461, 44);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.ShowCheckBox = true;
            this.dateTimePicker2.Size = new System.Drawing.Size(124, 23);
            this.dateTimePicker2.TabIndex = 4;
            this.dateTimePicker2.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(117, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Arrive Date";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Checked = false;
            this.dateTimePicker1.CustomFormat = " ";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(192, 44);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.ShowCheckBox = true;
            this.dateTimePicker1.Size = new System.Drawing.Size(124, 23);
            this.dateTimePicker1.TabIndex = 2;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(107, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name/HN/EN";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column9,
            this.colNo,
            this.colHn,
            this.colEn,
            this.colName,
            this.colArrive,
            this.colType,
            this.colLanguage,
            this.colPackage,
            this.colPreview,
            this.colPrintDate,
            this.colPrintBy,
            this.colConfirmDate,
            this.confirmBy,
            this.colFinish,
            this.colPath,
            this.col_tpb_id});
            this.dataGridView1.Location = new System.Drawing.Point(13, 132);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(831, 372);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Column9
            // 
            this.Column9.Frozen = true;
            this.Column9.HeaderText = "";
            this.Column9.Name = "Column9";
            this.Column9.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column9.Width = 30;
            // 
            // colNo
            // 
            this.colNo.DataPropertyName = "no";
            this.colNo.Frozen = true;
            this.colNo.HeaderText = "No.";
            this.colNo.Name = "colNo";
            this.colNo.ReadOnly = true;
            this.colNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colNo.Width = 33;
            // 
            // colHn
            // 
            this.colHn.DataPropertyName = "hn";
            this.colHn.Frozen = true;
            this.colHn.HeaderText = "HN";
            this.colHn.Name = "colHn";
            this.colHn.ReadOnly = true;
            this.colHn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colEn
            // 
            this.colEn.DataPropertyName = "en";
            this.colEn.Frozen = true;
            this.colEn.HeaderText = "EN";
            this.colEn.Name = "colEn";
            this.colEn.ReadOnly = true;
            this.colEn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colName
            // 
            this.colName.DataPropertyName = "name";
            this.colName.Frozen = true;
            this.colName.HeaderText = "Name";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            this.colName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colName.Width = 150;
            // 
            // colArrive
            // 
            this.colArrive.DataPropertyName = "arriveDate";
            dataGridViewCellStyle1.Format = "dd/MM/yyyy";
            dataGridViewCellStyle1.NullValue = null;
            this.colArrive.DefaultCellStyle = dataGridViewCellStyle1;
            this.colArrive.Frozen = true;
            this.colArrive.HeaderText = "Arrive Date";
            this.colArrive.Name = "colArrive";
            this.colArrive.ReadOnly = true;
            this.colArrive.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colArrive.Width = 90;
            // 
            // colType
            // 
            this.colType.DataPropertyName = "type";
            this.colType.Frozen = true;
            this.colType.HeaderText = "Type";
            this.colType.Name = "colType";
            this.colType.ReadOnly = true;
            this.colType.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colType.Width = 50;
            // 
            // colLanguage
            // 
            this.colLanguage.DataPropertyName = "language";
            this.colLanguage.Frozen = true;
            this.colLanguage.HeaderText = "Language";
            this.colLanguage.Name = "colLanguage";
            this.colLanguage.ReadOnly = true;
            this.colLanguage.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colLanguage.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colLanguage.Width = 70;
            // 
            // colPackage
            // 
            this.colPackage.DataPropertyName = "package";
            this.colPackage.HeaderText = "Package";
            this.colPackage.Name = "colPackage";
            this.colPackage.ReadOnly = true;
            this.colPackage.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colPackage.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colPreview
            // 
            this.colPreview.DataPropertyName = "preview";
            this.colPreview.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.colPreview.HeaderText = "Preview";
            this.colPreview.Name = "colPreview";
            this.colPreview.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colPreview.Width = 60;
            // 
            // colPrintDate
            // 
            this.colPrintDate.DataPropertyName = "printDate";
            dataGridViewCellStyle2.Format = "dd/MM/yyyy";
            this.colPrintDate.DefaultCellStyle = dataGridViewCellStyle2;
            this.colPrintDate.HeaderText = "Print Date";
            this.colPrintDate.Name = "colPrintDate";
            this.colPrintDate.ReadOnly = true;
            this.colPrintDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colPrintDate.Width = 90;
            // 
            // colPrintBy
            // 
            this.colPrintBy.DataPropertyName = "printBy";
            this.colPrintBy.HeaderText = "Print By";
            this.colPrintBy.Name = "colPrintBy";
            this.colPrintBy.ReadOnly = true;
            this.colPrintBy.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colConfirmDate
            // 
            this.colConfirmDate.DataPropertyName = "confirmDate";
            dataGridViewCellStyle3.Format = "dd/MM/yyyy";
            this.colConfirmDate.DefaultCellStyle = dataGridViewCellStyle3;
            this.colConfirmDate.HeaderText = "Confirm Date";
            this.colConfirmDate.Name = "colConfirmDate";
            this.colConfirmDate.ReadOnly = true;
            this.colConfirmDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colConfirmDate.Width = 90;
            // 
            // confirmBy
            // 
            this.confirmBy.DataPropertyName = "confirmBy";
            this.confirmBy.HeaderText = "Confirm By";
            this.confirmBy.Name = "confirmBy";
            this.confirmBy.ReadOnly = true;
            this.confirmBy.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colFinish
            // 
            this.colFinish.DataPropertyName = "finish";
            this.colFinish.HeaderText = "Finish";
            this.colFinish.Name = "colFinish";
            this.colFinish.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colFinish.Width = 60;
            // 
            // colPath
            // 
            this.colPath.DataPropertyName = "pathFile";
            this.colPath.HeaderText = "";
            this.colPath.Name = "colPath";
            this.colPath.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colPath.Visible = false;
            // 
            // col_tpb_id
            // 
            this.col_tpb_id.DataPropertyName = "tpb_id";
            this.col_tpb_id.HeaderText = "";
            this.col_tpb_id.Name = "col_tpb_id";
            this.col_tpb_id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.col_tpb_id.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 16);
            this.label5.TabIndex = 13;
            this.label5.Text = "Book";
            // 
            // btnPrintAll
            // 
            this.btnPrintAll.Location = new System.Drawing.Point(744, 510);
            this.btnPrintAll.Name = "btnPrintAll";
            this.btnPrintAll.Size = new System.Drawing.Size(100, 40);
            this.btnPrintAll.TabIndex = 13;
            this.btnPrintAll.Text = "Print All";
            this.btnPrintAll.UseVisualStyleBackColor = true;
            this.btnPrintAll.Click += new System.EventHandler(this.btnPrintAll_Click);
            // 
            // lbAlert
            // 
            this.lbAlert.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbAlert.ForeColor = System.Drawing.Color.Red;
            this.lbAlert.Location = new System.Drawing.Point(850, 90);
            this.lbAlert.Name = "lbAlert";
            this.lbAlert.Size = new System.Drawing.Size(337, 19);
            this.lbAlert.TabIndex = 232;
            // 
            // frmBookPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 562);
            this.Controls.Add(this.lbAlert);
            this.Controls.Add(this.btnPrintAll);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "frmBookPrint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Book Report";
            this.Load += new System.EventHandler(this.frmBookPrint_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkBF;
        private System.Windows.Forms.CheckBox chkBP;
        private System.Windows.Forms.CheckBox chkBC;
        private System.Windows.Forms.CheckBox chkNB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnPrintAll;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHn;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEn;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colArrive;
        private System.Windows.Forms.DataGridViewTextBoxColumn colType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLanguage;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPackage;
        private System.Windows.Forms.DataGridViewButtonColumn colPreview;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrintDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrintBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn colConfirmDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn confirmBy;
        private System.Windows.Forms.DataGridViewButtonColumn colFinish;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_tpb_id;
        private System.Windows.Forms.Label lbAlert;
    }
}