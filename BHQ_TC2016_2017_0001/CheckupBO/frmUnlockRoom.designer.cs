namespace CheckupBO
{
    partial class frmUnlockRoom
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUnlockRoom));
            this.btnUnlockRoom = new System.Windows.Forms.Button();
            this.GridSiteRoom = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColmhsID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColmrmID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.DDRoom = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.DDSite = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnsearch = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.GridRoomLock = new System.Windows.Forms.DataGridView();
            this.lbmsgAlert = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Colselect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Colmrd_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUsername = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.GridSiteRoom)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridRoomLock)).BeginInit();
            this.SuspendLayout();
            // 
            // btnUnlockRoom
            // 
            this.btnUnlockRoom.Location = new System.Drawing.Point(793, 67);
            this.btnUnlockRoom.Name = "btnUnlockRoom";
            this.btnUnlockRoom.Size = new System.Drawing.Size(107, 40);
            this.btnUnlockRoom.TabIndex = 1;
            this.btnUnlockRoom.Text = "Unlock Room";
            this.btnUnlockRoom.UseVisualStyleBackColor = true;
            this.btnUnlockRoom.Click += new System.EventHandler(this.btnUnlockRoom_Click);
            // 
            // GridSiteRoom
            // 
            this.GridSiteRoom.AllowUserToAddRows = false;
            this.GridSiteRoom.AllowUserToDeleteRows = false;
            this.GridSiteRoom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.GridSiteRoom.BackgroundColor = System.Drawing.Color.White;
            this.GridSiteRoom.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.GridSiteRoom.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridSiteRoom.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.ColmhsID,
            this.ColmrmID});
            this.GridSiteRoom.Location = new System.Drawing.Point(12, 125);
            this.GridSiteRoom.Name = "GridSiteRoom";
            this.GridSiteRoom.ReadOnly = true;
            this.GridSiteRoom.RowHeadersVisible = false;
            this.GridSiteRoom.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.GridSiteRoom.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.GridSiteRoom.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridSiteRoom.Size = new System.Drawing.Size(290, 321);
            this.GridSiteRoom.TabIndex = 2;
            this.GridSiteRoom.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridSiteRoom_CellClick);
            this.GridSiteRoom.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.GridSiteRoom_DataBindingComplete);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "No.";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 30;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "siteName";
            this.Column2.HeaderText = "Site";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 80;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "RoomName";
            this.Column3.HeaderText = "Station";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 160;
            // 
            // ColmhsID
            // 
            this.ColmhsID.DataPropertyName = "mhsId";
            this.ColmhsID.HeaderText = "mhsID";
            this.ColmhsID.Name = "ColmhsID";
            this.ColmhsID.ReadOnly = true;
            this.ColmhsID.Visible = false;
            // 
            // ColmrmID
            // 
            this.ColmrmID.DataPropertyName = "mrmId";
            this.ColmrmID.HeaderText = "mrm_id";
            this.ColmrmID.Name = "ColmrmID";
            this.ColmrmID.ReadOnly = true;
            this.ColmrmID.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.DDRoom);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.DDSite);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnClear);
            this.groupBox2.Controls.Add(this.btnsearch);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.groupBox2.Location = new System.Drawing.Point(12, 3);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(660, 55);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Search";
            // 
            // DDRoom
            // 
            this.DDRoom.FormattingEnabled = true;
            this.DDRoom.Location = new System.Drawing.Point(297, 20);
            this.DDRoom.Name = "DDRoom";
            this.DDRoom.Size = new System.Drawing.Size(140, 24);
            this.DDRoom.TabIndex = 27;
            this.DDRoom.SelectedIndexChanged += new System.EventHandler(this.DDRoom_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(31, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "HPC Site : ";
            // 
            // DDSite
            // 
            this.DDSite.FormattingEnabled = true;
            this.DDSite.Location = new System.Drawing.Point(103, 20);
            this.DDSite.Name = "DDSite";
            this.DDSite.Size = new System.Drawing.Size(120, 24);
            this.DDSite.TabIndex = 26;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(234, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Station : ";
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnClear.Location = new System.Drawing.Point(556, 20);
            this.btnClear.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(80, 25);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "&Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnsearch
            // 
            this.btnsearch.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnsearch.Location = new System.Drawing.Point(474, 20);
            this.btnsearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnsearch.Name = "btnsearch";
            this.btnsearch.Size = new System.Drawing.Size(80, 25);
            this.btnsearch.TabIndex = 4;
            this.btnsearch.Text = "&Search";
            this.btnsearch.UseVisualStyleBackColor = true;
            this.btnsearch.Click += new System.EventHandler(this.btnsearch_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(12, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 16);
            this.label3.TabIndex = 28;
            this.label3.Text = "Station";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.Location = new System.Drawing.Point(268, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 16);
            this.label4.TabIndex = 29;
            this.label4.Text = "Room";
            // 
            // GridRoomLock
            // 
            this.GridRoomLock.AllowUserToAddRows = false;
            this.GridRoomLock.AllowUserToDeleteRows = false;
            this.GridRoomLock.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.GridRoomLock.BackgroundColor = System.Drawing.Color.White;
            this.GridRoomLock.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.GridRoomLock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridRoomLock.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.Colselect,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.Colmrd_id,
            this.colUsername,
            this.colName});
            this.GridRoomLock.Location = new System.Drawing.Point(315, 125);
            this.GridRoomLock.Name = "GridRoomLock";
            this.GridRoomLock.RowHeadersVisible = false;
            this.GridRoomLock.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.GridRoomLock.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.GridRoomLock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.GridRoomLock.Size = new System.Drawing.Size(585, 320);
            this.GridRoomLock.TabIndex = 30;
            this.GridRoomLock.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.GridRoomLock_DataBindingComplete);
            // 
            // lbmsgAlert
            // 
            this.lbmsgAlert.ForeColor = System.Drawing.Color.Red;
            this.lbmsgAlert.Location = new System.Drawing.Point(12, 67);
            this.lbmsgAlert.Name = "lbmsgAlert";
            this.lbmsgAlert.Size = new System.Drawing.Size(504, 28);
            this.lbmsgAlert.TabIndex = 31;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "No.";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 30;
            // 
            // Colselect
            // 
            this.Colselect.HeaderText = "Select";
            this.Colselect.Name = "Colselect";
            this.Colselect.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Colselect.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Colselect.Width = 50;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "mrd_ename";
            this.dataGridViewTextBoxColumn2.HeaderText = "Room";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 180;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "countperson";
            this.dataGridViewTextBoxColumn3.HeaderText = "Person";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 55;
            // 
            // Colmrd_id
            // 
            this.Colmrd_id.DataPropertyName = "mrd_id";
            this.Colmrd_id.HeaderText = "mrdid";
            this.Colmrd_id.Name = "Colmrd_id";
            this.Colmrd_id.Visible = false;
            // 
            // colUsername
            // 
            this.colUsername.DataPropertyName = "username";
            this.colUsername.HeaderText = "Username";
            this.colUsername.Name = "colUsername";
            this.colUsername.ReadOnly = true;
            this.colUsername.Width = 85;
            // 
            // colName
            // 
            this.colName.DataPropertyName = "name";
            this.colName.HeaderText = "Name";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            this.colName.Width = 165;
            // 
            // frmUnlockRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(912, 462);
            this.Controls.Add(this.lbmsgAlert);
            this.Controls.Add(this.GridRoomLock);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.GridSiteRoom);
            this.Controls.Add(this.btnUnlockRoom);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmUnlockRoom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Unlock Room";
            this.Load += new System.EventHandler(this.frmUnlockRoom_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GridSiteRoom)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridRoomLock)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUnlockRoom;
        private System.Windows.Forms.DataGridView GridSiteRoom;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnsearch;
        private System.Windows.Forms.ComboBox DDRoom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox DDSite;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView GridRoomLock;
        private System.Windows.Forms.Label lbmsgAlert;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColmhsID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColmrmID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Colselect;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Colmrd_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUsername;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
    }
}