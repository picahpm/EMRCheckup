namespace BKvs2010.Usercontrols
{
    partial class UIGridChangeDoctor
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

        #region Component Designer generated code

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lbDesc = new System.Windows.Forms.Label();
            this.lbDocName = new System.Windows.Forms.Label();
            this.lbLogTime = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbCompleteRE = new System.Windows.Forms.Label();
            this.lbWating = new System.Windows.Forms.Label();
            this.gridData = new System.Windows.Forms.DataGridView();
            this.lbCompletePE = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbPoint = new System.Windows.Forms.Label();
            this.colNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQueueNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colChageDoc = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ColPoint = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColPackName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbDocName.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridData)).BeginInit();
            this.SuspendLayout();
            // 
            // lbDesc
            // 
            this.lbDesc.BackColor = System.Drawing.Color.Black;
            this.lbDesc.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbDesc.ForeColor = System.Drawing.Color.White;
            this.lbDesc.Location = new System.Drawing.Point(0, 0);
            this.lbDesc.Name = "lbDesc";
            this.lbDesc.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.lbDesc.Size = new System.Drawing.Size(590, 30);
            this.lbDesc.TabIndex = 0;
            this.lbDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbDocName
            // 
            this.lbDocName.BackColor = System.Drawing.Color.RoyalBlue;
            this.lbDocName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbDocName.Controls.Add(this.lbLogTime);
            this.lbDocName.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbDocName.ForeColor = System.Drawing.Color.White;
            this.lbDocName.Location = new System.Drawing.Point(0, 30);
            this.lbDocName.Name = "lbDocName";
            this.lbDocName.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.lbDocName.Size = new System.Drawing.Size(590, 30);
            this.lbDocName.TabIndex = 1;
            this.lbDocName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbLogTime
            // 
            this.lbLogTime.BackColor = System.Drawing.Color.Transparent;
            this.lbLogTime.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbLogTime.ForeColor = System.Drawing.Color.White;
            this.lbLogTime.Location = new System.Drawing.Point(462, 5);
            this.lbLogTime.Name = "lbLogTime";
            this.lbLogTime.Size = new System.Drawing.Size(121, 19);
            this.lbLogTime.TabIndex = 2;
            this.lbLogTime.Text = "Log in time : --:--";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(0, 59);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label1.Size = new System.Drawing.Size(118, 29);
            this.label1.TabIndex = 3;
            this.label1.Text = "Completed Case";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbCompleteRE
            // 
            this.lbCompleteRE.BackColor = System.Drawing.Color.White;
            this.lbCompleteRE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbCompleteRE.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbCompleteRE.ForeColor = System.Drawing.Color.Black;
            this.lbCompleteRE.Location = new System.Drawing.Point(407, 59);
            this.lbCompleteRE.Name = "lbCompleteRE";
            this.lbCompleteRE.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.lbCompleteRE.Size = new System.Drawing.Size(183, 29);
            this.lbCompleteRE.TabIndex = 4;
            this.lbCompleteRE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbWating
            // 
            this.lbWating.BackColor = System.Drawing.Color.White;
            this.lbWating.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbWating.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbWating.ForeColor = System.Drawing.Color.Black;
            this.lbWating.Location = new System.Drawing.Point(407, 87);
            this.lbWating.Name = "lbWating";
            this.lbWating.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.lbWating.Size = new System.Drawing.Size(183, 29);
            this.lbWating.TabIndex = 6;
            this.lbWating.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gridData
            // 
            this.gridData.AllowUserToAddRows = false;
            this.gridData.AllowUserToDeleteRows = false;
            this.gridData.AllowUserToResizeColumns = false;
            this.gridData.AllowUserToResizeRows = false;
            this.gridData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.gridData.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.gridData.BackgroundColor = System.Drawing.Color.White;
            this.gridData.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gridData.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNo,
            this.colHN,
            this.colQueueNo,
            this.colName,
            this.Column1,
            this.colType,
            this.colTime,
            this.colChageDoc,
            this.ColPoint,
            this.ColPackName});
            this.gridData.Location = new System.Drawing.Point(0, 115);
            this.gridData.MultiSelect = false;
            this.gridData.Name = "gridData";
            this.gridData.RowHeadersVisible = false;
            this.gridData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gridData.Size = new System.Drawing.Size(590, 179);
            this.gridData.TabIndex = 7;
            this.gridData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridData_CellContentClick);
            this.gridData.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.gridData_RowPrePaint);
            // 
            // lbCompletePE
            // 
            this.lbCompletePE.BackColor = System.Drawing.Color.White;
            this.lbCompletePE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbCompletePE.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbCompletePE.ForeColor = System.Drawing.Color.Black;
            this.lbCompletePE.Location = new System.Drawing.Point(171, 59);
            this.lbCompletePE.Name = "lbCompletePE";
            this.lbCompletePE.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.lbCompletePE.Size = new System.Drawing.Size(183, 29);
            this.lbCompletePE.TabIndex = 8;
            this.lbCompletePE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.LightSteelBlue;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(117, 59);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label3.Size = new System.Drawing.Size(55, 29);
            this.label3.TabIndex = 9;
            this.label3.Text = "PE :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.LightSteelBlue;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(353, 59);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label5.Size = new System.Drawing.Size(55, 29);
            this.label5.TabIndex = 10;
            this.label5.Text = "RE :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(117, 87);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label2.Size = new System.Drawing.Size(55, 29);
            this.label2.TabIndex = 12;
            this.label2.Text = "Point :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.LightSteelBlue;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(0, 87);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label6.Size = new System.Drawing.Size(118, 29);
            this.label6.TabIndex = 11;
            this.label6.Text = "Summary";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.LightSteelBlue;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(353, 87);
            this.label7.Name = "label7";
            this.label7.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label7.Size = new System.Drawing.Size(55, 29);
            this.label7.TabIndex = 15;
            this.label7.Text = "Case :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbPoint
            // 
            this.lbPoint.BackColor = System.Drawing.Color.White;
            this.lbPoint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbPoint.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbPoint.ForeColor = System.Drawing.Color.Black;
            this.lbPoint.Location = new System.Drawing.Point(171, 87);
            this.lbPoint.Name = "lbPoint";
            this.lbPoint.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.lbPoint.Size = new System.Drawing.Size(183, 29);
            this.lbPoint.TabIndex = 14;
            this.lbPoint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // colNo
            // 
            this.colNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colNo.DataPropertyName = "no";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.colNo.DefaultCellStyle = dataGridViewCellStyle2;
            this.colNo.HeaderText = "No.";
            this.colNo.Name = "colNo";
            this.colNo.ReadOnly = true;
            this.colNo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colNo.Width = 30;
            // 
            // colHN
            // 
            this.colHN.DataPropertyName = "hn";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.colHN.DefaultCellStyle = dataGridViewCellStyle3;
            this.colHN.HeaderText = "HN";
            this.colHN.Name = "colHN";
            this.colHN.ReadOnly = true;
            this.colHN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colHN.Width = 30;
            // 
            // colQueueNo
            // 
            this.colQueueNo.DataPropertyName = "queue_no";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colQueueNo.DefaultCellStyle = dataGridViewCellStyle4;
            this.colQueueNo.HeaderText = "Q.No.";
            this.colQueueNo.Name = "colQueueNo";
            this.colQueueNo.Width = 65;
            // 
            // colName
            // 
            this.colName.DataPropertyName = "name";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.colName.DefaultCellStyle = dataGridViewCellStyle5;
            this.colName.HeaderText = "Name";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            this.colName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colName.Width = 47;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "bfRoom";
            this.Column1.HeaderText = "Prev. Room";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 80;
            // 
            // colType
            // 
            this.colType.DataPropertyName = "type";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colType.DefaultCellStyle = dataGridViewCellStyle6;
            this.colType.HeaderText = "Type";
            this.colType.Name = "colType";
            this.colType.ReadOnly = true;
            this.colType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colType.Visible = false;
            this.colType.Width = 42;
            // 
            // colTime
            // 
            this.colTime.DataPropertyName = "time";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle7.Format = "HH:mm:ss";
            this.colTime.DefaultCellStyle = dataGridViewCellStyle7;
            this.colTime.HeaderText = "Time";
            this.colTime.Name = "colTime";
            this.colTime.ReadOnly = true;
            this.colTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colTime.Width = 43;
            // 
            // colChageDoc
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.colChageDoc.DefaultCellStyle = dataGridViewCellStyle8;
            this.colChageDoc.HeaderText = "Manage Q";
            this.colChageDoc.Name = "colChageDoc";
            this.colChageDoc.ReadOnly = true;
            this.colChageDoc.Text = "Change";
            this.colChageDoc.UseColumnTextForButtonValue = true;
            this.colChageDoc.Visible = false;
            this.colChageDoc.Width = 72;
            // 
            // ColPoint
            // 
            this.ColPoint.DataPropertyName = "point";
            this.ColPoint.HeaderText = "Point";
            this.ColPoint.Name = "ColPoint";
            this.ColPoint.Width = 61;
            // 
            // ColPackName
            // 
            this.ColPackName.DataPropertyName = "package_name";
            this.ColPackName.HeaderText = "Package Name";
            this.ColPackName.Name = "ColPackName";
            this.ColPackName.Width = 117;
            // 
            // UIGridChangeDoctor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lbPoint);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbCompletePE);
            this.Controls.Add(this.gridData);
            this.Controls.Add(this.lbWating);
            this.Controls.Add(this.lbCompleteRE);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbDocName);
            this.Controls.Add(this.lbDesc);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "UIGridChangeDoctor";
            this.Size = new System.Drawing.Size(590, 294);
            this.Resize += new System.EventHandler(this.UIGridChangeDoctor_Resize);
            this.lbDocName.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbDesc;
        private System.Windows.Forms.Label lbDocName;
        private System.Windows.Forms.Label lbLogTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbCompleteRE;
        private System.Windows.Forms.Label lbWating;
        private System.Windows.Forms.DataGridView gridData;
        private System.Windows.Forms.Label lbCompletePE;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbPoint;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHN;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQueueNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTime;
        private System.Windows.Forms.DataGridViewButtonColumn colChageDoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColPoint;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColPackName;
    }
}
