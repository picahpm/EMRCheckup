namespace BKvs2010.Usercontrols
{
    partial class UIQueue
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lbtitle11 = new System.Windows.Forms.Label();
            this.DGVStation = new System.Windows.Forms.DataGridView();
            this.NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Station = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbSummaryTime = new System.Windows.Forms.Label();
            this.lbsummaryQTY = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DGVStation)).BeginInit();
            this.SuspendLayout();
            // 
            // lbtitle11
            // 
            this.lbtitle11.BackColor = System.Drawing.Color.RoyalBlue;
            this.lbtitle11.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbtitle11.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbtitle11.ForeColor = System.Drawing.Color.White;
            this.lbtitle11.Location = new System.Drawing.Point(0, 0);
            this.lbtitle11.Name = "lbtitle11";
            this.lbtitle11.Size = new System.Drawing.Size(280, 20);
            this.lbtitle11.TabIndex = 9;
            this.lbtitle11.Text = "จำนวนคิวและระยะเวลาการรอคอยแต่ละ Station";
            this.lbtitle11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DGVStation
            // 
            this.DGVStation.AllowUserToAddRows = false;
            this.DGVStation.AllowUserToDeleteRows = false;
            this.DGVStation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.DGVStation.BackgroundColor = System.Drawing.Color.White;
            this.DGVStation.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DGVStation.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.DGVStation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.DGVStation.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NO,
            this.Station,
            this.Column1,
            this.Column2});
            this.DGVStation.Location = new System.Drawing.Point(0, 20);
            this.DGVStation.Name = "DGVStation";
            this.DGVStation.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVStation.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.DGVStation.RowHeadersVisible = false;
            this.DGVStation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGVStation.Size = new System.Drawing.Size(280, 335);
            this.DGVStation.TabIndex = 10;
            // 
            // NO
            // 
            this.NO.DataPropertyName = "NO";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.NO.DefaultCellStyle = dataGridViewCellStyle4;
            this.NO.HeaderText = "NO";
            this.NO.Name = "NO";
            this.NO.ReadOnly = true;
            this.NO.Width = 30;
            // 
            // Station
            // 
            this.Station.DataPropertyName = "ename";
            this.Station.HeaderText = "Station";
            this.Station.Name = "Station";
            this.Station.ReadOnly = true;
            this.Station.Width = 130;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "waiting";
            this.Column1.HeaderText = "จำนวนคิว";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 60;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "time";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle5;
            this.Column2.HeaderText = "เวลา";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 45;
            // 
            // lbSummaryTime
            // 
            this.lbSummaryTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbSummaryTime.BackColor = System.Drawing.Color.White;
            this.lbSummaryTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbSummaryTime.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbSummaryTime.Location = new System.Drawing.Point(202, 358);
            this.lbSummaryTime.Name = "lbSummaryTime";
            this.lbSummaryTime.Size = new System.Drawing.Size(56, 24);
            this.lbSummaryTime.TabIndex = 12;
            this.lbSummaryTime.Text = "0";
            this.lbSummaryTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbsummaryQTY
            // 
            this.lbsummaryQTY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbsummaryQTY.BackColor = System.Drawing.Color.White;
            this.lbsummaryQTY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbsummaryQTY.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbsummaryQTY.Location = new System.Drawing.Point(144, 358);
            this.lbsummaryQTY.Name = "lbsummaryQTY";
            this.lbsummaryQTY.Size = new System.Drawing.Size(52, 24);
            this.lbsummaryQTY.TabIndex = 13;
            this.lbsummaryQTY.Text = "0";
            this.lbsummaryQTY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(92, 364);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Summary";
            // 
            // UIQueue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.lbSummaryTime);
            this.Controls.Add(this.lbsummaryQTY);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DGVStation);
            this.Controls.Add(this.lbtitle11);
            this.Name = "UIQueue";
            this.Size = new System.Drawing.Size(280, 385);
            ((System.ComponentModel.ISupportInitialize)(this.DGVStation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbtitle11;
        private System.Windows.Forms.DataGridView DGVStation;
        private System.Windows.Forms.DataGridViewTextBoxColumn NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn Station;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Label lbSummaryTime;
        private System.Windows.Forms.Label lbsummaryQTY;
        private System.Windows.Forms.Label label3;

    }
}
