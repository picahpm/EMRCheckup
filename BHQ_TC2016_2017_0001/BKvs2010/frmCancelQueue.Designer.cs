namespace BKvs2010
{
    partial class frmCancelQueue
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.gridStation = new System.Windows.Forms.DataGridView();
            this.colBtn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colSite = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colZoneName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRoomName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWaitPerson = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWaitingTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_mhs_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_mrm_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_mvt_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbSite = new System.Windows.Forms.ComboBox();
            this.btnSendAuto = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdPending = new System.Windows.Forms.RadioButton();
            this.rdSkip = new System.Windows.Forms.RadioButton();
            this.rdNA = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridStation)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 81.56371F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.43629F));
            this.tableLayoutPanel2.Controls.Add(this.gridStation, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel3, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 64);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1036, 345);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // gridStation
            // 
            this.gridStation.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(236)))), ((int)(((byte)(251)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.gridStation.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridStation.BackgroundColor = System.Drawing.Color.White;
            this.gridStation.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gridStation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridStation.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colBtn,
            this.colSite,
            this.colZoneName,
            this.colRoomName,
            this.colWaitPerson,
            this.colWaitingTime,
            this.col_mhs_id,
            this.col_mrm_id,
            this.col_mvt_id,
            this.colVIP});
            this.gridStation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridStation.Location = new System.Drawing.Point(3, 3);
            this.gridStation.MultiSelect = false;
            this.gridStation.Name = "gridStation";
            this.gridStation.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.gridStation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridStation.Size = new System.Drawing.Size(839, 339);
            this.gridStation.TabIndex = 0;
            this.gridStation.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridStation_CellContentClick);
            // 
            // colBtn
            // 
            this.colBtn.HeaderText = "";
            this.colBtn.Name = "colBtn";
            this.colBtn.Text = "Send Manual";
            this.colBtn.UseColumnTextForButtonValue = true;
            this.colBtn.Width = 90;
            // 
            // colSite
            // 
            this.colSite.DataPropertyName = "mhs_ename";
            this.colSite.HeaderText = "Site";
            this.colSite.Name = "colSite";
            this.colSite.ReadOnly = true;
            // 
            // colZoneName
            // 
            this.colZoneName.DataPropertyName = "mze_ename";
            this.colZoneName.HeaderText = "Zone";
            this.colZoneName.Name = "colZoneName";
            this.colZoneName.ReadOnly = true;
            // 
            // colRoomName
            // 
            this.colRoomName.DataPropertyName = "mrm_ename";
            this.colRoomName.HeaderText = "Room";
            this.colRoomName.Name = "colRoomName";
            this.colRoomName.ReadOnly = true;
            // 
            // colWaitPerson
            // 
            this.colWaitPerson.DataPropertyName = "waiting_person";
            this.colWaitPerson.HeaderText = "Waiting Person (person)";
            this.colWaitPerson.Name = "colWaitPerson";
            this.colWaitPerson.ReadOnly = true;
            this.colWaitPerson.Width = 180;
            // 
            // colWaitingTime
            // 
            this.colWaitingTime.DataPropertyName = "waiting_time";
            this.colWaitingTime.HeaderText = "Waiting Time (min)";
            this.colWaitingTime.Name = "colWaitingTime";
            this.colWaitingTime.ReadOnly = true;
            this.colWaitingTime.Width = 150;
            // 
            // col_mhs_id
            // 
            this.col_mhs_id.DataPropertyName = "mhs_id";
            this.col_mhs_id.HeaderText = "mhs_id";
            this.col_mhs_id.Name = "col_mhs_id";
            this.col_mhs_id.ReadOnly = true;
            this.col_mhs_id.Visible = false;
            // 
            // col_mrm_id
            // 
            this.col_mrm_id.DataPropertyName = "mrm_id";
            this.col_mrm_id.HeaderText = "mrm_id";
            this.col_mrm_id.Name = "col_mrm_id";
            this.col_mrm_id.ReadOnly = true;
            this.col_mrm_id.Visible = false;
            // 
            // col_mvt_id
            // 
            this.col_mvt_id.DataPropertyName = "mvt_id";
            this.col_mvt_id.HeaderText = "mvt_id";
            this.col_mvt_id.Name = "col_mvt_id";
            this.col_mvt_id.ReadOnly = true;
            this.col_mvt_id.Visible = false;
            // 
            // colVIP
            // 
            this.colVIP.DataPropertyName = "vip";
            this.colVIP.HeaderText = "VIP";
            this.colVIP.Name = "colVIP";
            this.colVIP.ReadOnly = true;
            this.colVIP.Width = 55;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.cmbSite);
            this.panel3.Controls.Add(this.btnSendAuto);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(848, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(185, 339);
            this.panel3.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(8, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Please Select Site";
            // 
            // cmbSite
            // 
            this.cmbSite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSite.FormattingEnabled = true;
            this.cmbSite.Location = new System.Drawing.Point(11, 61);
            this.cmbSite.Name = "cmbSite";
            this.cmbSite.Size = new System.Drawing.Size(164, 24);
            this.cmbSite.TabIndex = 1;
            this.cmbSite.SelectedValueChanged += new System.EventHandler(this.cmbSite_SelectedValueChanged);
            // 
            // btnSendAuto
            // 
            this.btnSendAuto.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSendAuto.Location = new System.Drawing.Point(37, 287);
            this.btnSendAuto.Name = "btnSendAuto";
            this.btnSendAuto.Size = new System.Drawing.Size(113, 35);
            this.btnSendAuto.TabIndex = 0;
            this.btnSendAuto.Text = "Send Auto";
            this.btnSendAuto.UseVisualStyleBackColor = true;
            this.btnSendAuto.Click += new System.EventHandler(this.btnSendAuto_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rdPending);
            this.panel1.Controls.Add(this.rdSkip);
            this.panel1.Controls.Add(this.rdNA);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1036, 55);
            this.panel1.TabIndex = 0;
            // 
            // rdPending
            // 
            this.rdPending.AutoSize = true;
            this.rdPending.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.rdPending.ForeColor = System.Drawing.Color.Blue;
            this.rdPending.Location = new System.Drawing.Point(266, 15);
            this.rdPending.Name = "rdPending";
            this.rdPending.Size = new System.Drawing.Size(160, 27);
            this.rdPending.TabIndex = 2;
            this.rdPending.TabStop = true;
            this.rdPending.Text = "Pending Station";
            this.rdPending.UseVisualStyleBackColor = true;
            // 
            // rdSkip
            // 
            this.rdSkip.AutoSize = true;
            this.rdSkip.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.rdSkip.ForeColor = System.Drawing.Color.Blue;
            this.rdSkip.Location = new System.Drawing.Point(133, 15);
            this.rdSkip.Name = "rdSkip";
            this.rdSkip.Size = new System.Drawing.Size(127, 27);
            this.rdSkip.TabIndex = 1;
            this.rdSkip.TabStop = true;
            this.rdSkip.Text = "Skip Station";
            this.rdSkip.UseVisualStyleBackColor = true;
            // 
            // rdNA
            // 
            this.rdNA.AutoSize = true;
            this.rdNA.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.rdNA.ForeColor = System.Drawing.Color.Blue;
            this.rdNA.Location = new System.Drawing.Point(68, 15);
            this.rdNA.Name = "rdNA";
            this.rdNA.Size = new System.Drawing.Size(59, 27);
            this.rdNA.TabIndex = 0;
            this.rdNA.TabStop = true;
            this.rdNA.Text = "N/A";
            this.rdNA.UseVisualStyleBackColor = true;
            this.rdNA.CheckedChanged += new System.EventHandler(this.rdNA_CheckedChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1042, 412);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // frmCancelQueue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1042, 412);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmCancelQueue";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Skip&Pending Station";
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridStation)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.DataGridView gridStation;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnSendAuto;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rdPending;
        private System.Windows.Forms.RadioButton rdSkip;
        private System.Windows.Forms.RadioButton rdNA;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox cmbSite;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewButtonColumn colBtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSite;
        private System.Windows.Forms.DataGridViewTextBoxColumn colZoneName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRoomName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWaitPerson;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWaitingTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_mhs_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_mrm_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_mvt_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVIP;

    }
}