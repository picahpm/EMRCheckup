namespace BKvs2010.Usercontrols
{
    partial class newWaitingListUC
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
            this.DGV_Waiting = new System.Windows.Forms.DataGridView();
            this.colNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBtnCancelQueue = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colSendToCheckB = new System.Windows.Forms.DataGridViewButtonColumn();
            this.lbtitle1 = new System.Windows.Forms.Label();
            this.label242 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Waiting)).BeginInit();
            this.SuspendLayout();
            // 
            // DGV_Waiting
            // 
            this.DGV_Waiting.AllowUserToAddRows = false;
            this.DGV_Waiting.AllowUserToDeleteRows = false;
            this.DGV_Waiting.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.DGV_Waiting.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.DGV_Waiting.BackgroundColor = System.Drawing.Color.White;
            this.DGV_Waiting.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DGV_Waiting.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.DGV_Waiting.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_Waiting.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNo,
            this.colHN,
            this.colName,
            this.colBtnCancelQueue,
            this.colSendToCheckB});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGV_Waiting.DefaultCellStyle = dataGridViewCellStyle1;
            this.DGV_Waiting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGV_Waiting.Location = new System.Drawing.Point(0, 22);
            this.DGV_Waiting.Name = "DGV_Waiting";
            this.DGV_Waiting.ReadOnly = true;
            this.DGV_Waiting.RowHeadersVisible = false;
            this.DGV_Waiting.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGV_Waiting.Size = new System.Drawing.Size(273, 211);
            this.DGV_Waiting.TabIndex = 9;
            this.DGV_Waiting.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGV_Waiting_CellContentClick);
            this.DGV_Waiting.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.DGV_Waiting_RowPrePaint);
            // 
            // colNo
            // 
            this.colNo.DataPropertyName = "priority";
            this.colNo.HeaderText = "NO";
            this.colNo.Name = "colNo";
            this.colNo.ReadOnly = true;
            this.colNo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colNo.Width = 29;
            // 
            // colHN
            // 
            this.colHN.DataPropertyName = "tpt_hn_no";
            this.colHN.HeaderText = "HN";
            this.colHN.Name = "colHN";
            this.colHN.ReadOnly = true;
            this.colHN.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colHN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colHN.Width = 29;
            // 
            // colName
            // 
            this.colName.DataPropertyName = "tpt_othername";
            this.colName.HeaderText = "Name";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            this.colName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colName.Width = 41;
            // 
            // colBtnCancelQueue
            // 
            this.colBtnCancelQueue.HeaderText = "";
            this.colBtnCancelQueue.Name = "colBtnCancelQueue";
            this.colBtnCancelQueue.ReadOnly = true;
            this.colBtnCancelQueue.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colBtnCancelQueue.Text = "Skip";
            this.colBtnCancelQueue.UseColumnTextForButtonValue = true;
            this.colBtnCancelQueue.Width = 5;
            // 
            // colSendToCheckB
            // 
            this.colSendToCheckB.HeaderText = "";
            this.colSendToCheckB.Name = "colSendToCheckB";
            this.colSendToCheckB.ReadOnly = true;
            this.colSendToCheckB.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colSendToCheckB.Text = "Send to CheckB";
            this.colSendToCheckB.UseColumnTextForButtonValue = true;
            this.colSendToCheckB.Width = 5;
            // 
            // lbtitle1
            // 
            this.lbtitle1.BackColor = System.Drawing.Color.RoyalBlue;
            this.lbtitle1.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbtitle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbtitle1.ForeColor = System.Drawing.Color.White;
            this.lbtitle1.Location = new System.Drawing.Point(0, 0);
            this.lbtitle1.Name = "lbtitle1";
            this.lbtitle1.Size = new System.Drawing.Size(273, 22);
            this.lbtitle1.TabIndex = 10;
            this.lbtitle1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label242
            // 
            this.label242.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label242.AutoSize = true;
            this.label242.Font = new System.Drawing.Font("Tahoma", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label242.Location = new System.Drawing.Point(20, -639);
            this.label242.Name = "label242";
            this.label242.Size = new System.Drawing.Size(841, 18);
            this.label242.TabIndex = 29;
            this.label242.Text = "แสดง Waiting Queue ของแต่ละ Station                                              " +
                "                                                                                " +
                "             ";
            this.label242.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // newWaitingListUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.DGV_Waiting);
            this.Controls.Add(this.label242);
            this.Controls.Add(this.lbtitle1);
            this.Name = "newWaitingListUC";
            this.Size = new System.Drawing.Size(273, 233);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Waiting)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DGV_Waiting;
        private System.Windows.Forms.Label lbtitle1;
        private System.Windows.Forms.Label label242;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHN;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewButtonColumn colBtnCancelQueue;
        private System.Windows.Forms.DataGridViewButtonColumn colSendToCheckB;
    }
}
