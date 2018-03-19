namespace BKvs2010.Usercontrols
{
    partial class newWaitingListStationUC
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
            this.lbTitle = new System.Windows.Forms.Label();
            this.GridWaitingList = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.GridWaitingList)).BeginInit();
            this.SuspendLayout();
            // 
            // lbTitle
            // 
            this.lbTitle.BackColor = System.Drawing.Color.RoyalBlue;
            this.lbTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbTitle.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbTitle.ForeColor = System.Drawing.Color.White;
            this.lbTitle.Location = new System.Drawing.Point(0, 0);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(280, 21);
            this.lbTitle.TabIndex = 4;
            this.lbTitle.Text = "Waiting List";
            this.lbTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GridWaitingList
            // 
            this.GridWaitingList.AllowUserToAddRows = false;
            this.GridWaitingList.AllowUserToDeleteRows = false;
            this.GridWaitingList.AllowUserToResizeRows = false;
            this.GridWaitingList.BackgroundColor = System.Drawing.Color.White;
            this.GridWaitingList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GridWaitingList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.GridWaitingList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.GridWaitingList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.GridWaitingList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridWaitingList.Location = new System.Drawing.Point(0, 21);
            this.GridWaitingList.MultiSelect = false;
            this.GridWaitingList.Name = "GridWaitingList";
            this.GridWaitingList.ReadOnly = true;
            this.GridWaitingList.RowHeadersVisible = false;
            this.GridWaitingList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridWaitingList.Size = new System.Drawing.Size(280, 104);
            this.GridWaitingList.TabIndex = 5;
            this.GridWaitingList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridWaitingList_CellDoubleClick);
            this.GridWaitingList.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.GridWaitingList_RowPrePaint);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "index";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column1.HeaderText = "No.";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 30;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "tpr_queue_no";
            this.Column2.HeaderText = "Q No.";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 45;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "tpt_hn_no";
            this.Column3.HeaderText = "HN";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 75;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "tpt_othername";
            this.Column4.HeaderText = "Name";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 140;
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            // 
            // newWaitingListStationUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.GridWaitingList);
            this.Controls.Add(this.lbTitle);
            this.Name = "newWaitingListStationUC";
            this.Size = new System.Drawing.Size(280, 125);
            ((System.ComponentModel.ISupportInitialize)(this.GridWaitingList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.DataGridView GridWaitingList;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
    }
}
