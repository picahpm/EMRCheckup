namespace BKvs2010
{
    partial class popUpLogView
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Sitename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRoom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTimein = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTimeOut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Sitename,
            this.colRoom,
            this.colIP,
            this.colTimein,
            this.colTimeOut});
            this.dataGridView1.GridColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.Location = new System.Drawing.Point(1, 1);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(316, 202);
            this.dataGridView1.TabIndex = 0;
            // 
            // Sitename
            // 
            this.Sitename.DataPropertyName = "Sitename";
            this.Sitename.HeaderText = "Site";
            this.Sitename.Name = "Sitename";
            this.Sitename.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Sitename.Width = 70;
            // 
            // colRoom
            // 
            this.colRoom.DataPropertyName = "room";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colRoom.DefaultCellStyle = dataGridViewCellStyle2;
            this.colRoom.HeaderText = "Room";
            this.colRoom.Name = "colRoom";
            this.colRoom.ReadOnly = true;
            this.colRoom.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colRoom.Width = 70;
            // 
            // colIP
            // 
            this.colIP.DataPropertyName = "ip";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colIP.DefaultCellStyle = dataGridViewCellStyle3;
            this.colIP.HeaderText = "IP";
            this.colIP.Name = "colIP";
            this.colIP.ReadOnly = true;
            this.colIP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colIP.Visible = false;
            this.colIP.Width = 80;
            // 
            // colTimein
            // 
            this.colTimein.DataPropertyName = "time_in";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Format = "HH:mm:ss";
            dataGridViewCellStyle4.NullValue = null;
            this.colTimein.DefaultCellStyle = dataGridViewCellStyle4;
            this.colTimein.HeaderText = "Time in";
            this.colTimein.Name = "colTimein";
            this.colTimein.ReadOnly = true;
            this.colTimein.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colTimein.Width = 80;
            // 
            // colTimeOut
            // 
            this.colTimeOut.DataPropertyName = "time_out";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Format = "HH:mm:ss";
            dataGridViewCellStyle5.NullValue = null;
            this.colTimeOut.DefaultCellStyle = dataGridViewCellStyle5;
            this.colTimeOut.HeaderText = "Time out";
            this.colTimeOut.Name = "colTimeOut";
            this.colTimeOut.ReadOnly = true;
            this.colTimeOut.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colTimeOut.Width = 80;
            // 
            // popUpLogView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(314, 205);
            this.Controls.Add(this.dataGridView1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximumSize = new System.Drawing.Size(320, 229);
            this.MinimumSize = new System.Drawing.Size(320, 229);
            this.Name = "popUpLogView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Log Login";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sitename;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRoom;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTimein;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTimeOut;

    }
}