namespace BKvs2010
{
    partial class frmChoiceRoom
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChoiceRoom));
            this.btnSelect = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.DDsiteToSend = new System.Windows.Forms.ComboBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Colsend = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ColName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColmrmCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColzoneName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colsite = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColWaittingPerson = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColWaitingTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRoomid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colmvtid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colmvtcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSelect
            // 
            this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelect.Location = new System.Drawing.Point(892, 283);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(107, 29);
            this.btnSelect.TabIndex = 1;
            this.btnSelect.Text = "OK";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Visible = false;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToOrderColumns = true;
            this.dataGridView2.AllowUserToResizeColumns = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView2.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView2.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView2.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView2.ColumnHeadersHeight = 29;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Colsend,
            this.ColName,
            this.ColmrmCode,
            this.ColzoneName,
            this.colsite,
            this.ColWaittingPerson,
            this.ColWaitingTime,
            this.colRoomid,
            this.colmvtid,
            this.colmvtcode,
            this.colVIP});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 30.25F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView2.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView2.Location = new System.Drawing.Point(5, 5);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 30.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dataGridView2.RowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridView2.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView2.RowTemplate.Height = 25;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(849, 303);
            this.dataGridView2.TabIndex = 5;
            this.dataGridView2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellClick);
            this.dataGridView2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellContentClick);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(861, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 14);
            this.label1.TabIndex = 45;
            this.label1.Text = "Send to Site";
            // 
            // DDsiteToSend
            // 
            this.DDsiteToSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DDsiteToSend.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.DDsiteToSend.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.DDsiteToSend.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.DDsiteToSend.FormattingEnabled = true;
            this.DDsiteToSend.Location = new System.Drawing.Point(861, 28);
            this.DDsiteToSend.Name = "DDsiteToSend";
            this.DDsiteToSend.Size = new System.Drawing.Size(140, 24);
            this.DDsiteToSend.TabIndex = 44;
            this.DDsiteToSend.SelectedValueChanged += new System.EventHandler(this.DDsiteToSend_SelectedValueChanged);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Colsend
            // 
            this.Colsend.HeaderText = "Send";
            this.Colsend.Name = "Colsend";
            this.Colsend.ReadOnly = true;
            this.Colsend.Text = "send";
            this.Colsend.UseColumnTextForButtonValue = true;
            this.Colsend.Width = 48;
            // 
            // ColName
            // 
            this.ColName.DataPropertyName = "mrm_ename";
            this.ColName.HeaderText = "Room Name";
            this.ColName.Name = "ColName";
            this.ColName.ReadOnly = true;
            this.ColName.Width = 119;
            // 
            // ColmrmCode
            // 
            this.ColmrmCode.DataPropertyName = "mrm_code";
            this.ColmrmCode.HeaderText = "mrmCode";
            this.ColmrmCode.Name = "ColmrmCode";
            this.ColmrmCode.ReadOnly = true;
            this.ColmrmCode.Visible = false;
            // 
            // ColzoneName
            // 
            this.ColzoneName.DataPropertyName = "mze_ename";
            this.ColzoneName.HeaderText = "Zone Name";
            this.ColzoneName.Name = "ColzoneName";
            this.ColzoneName.ReadOnly = true;
            this.ColzoneName.Width = 111;
            // 
            // colsite
            // 
            this.colsite.DataPropertyName = "mhs_ename";
            this.colsite.HeaderText = "HPC Site Name";
            this.colsite.Name = "colsite";
            this.colsite.ReadOnly = true;
            this.colsite.Width = 138;
            // 
            // ColWaittingPerson
            // 
            this.ColWaittingPerson.DataPropertyName = "waiting_person";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.ColWaittingPerson.DefaultCellStyle = dataGridViewCellStyle3;
            this.ColWaittingPerson.HeaderText = "Waiting Person (Person)";
            this.ColWaittingPerson.Name = "ColWaittingPerson";
            this.ColWaittingPerson.ReadOnly = true;
            this.ColWaittingPerson.Width = 196;
            // 
            // ColWaitingTime
            // 
            this.ColWaitingTime.DataPropertyName = "waiting_time";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.ColWaitingTime.DefaultCellStyle = dataGridViewCellStyle4;
            this.ColWaitingTime.HeaderText = "Waiting Time (Min)";
            this.ColWaitingTime.Name = "ColWaitingTime";
            this.ColWaitingTime.ReadOnly = true;
            this.ColWaitingTime.Width = 157;
            // 
            // colRoomid
            // 
            this.colRoomid.DataPropertyName = "mrm_id";
            this.colRoomid.HeaderText = "Roomid";
            this.colRoomid.Name = "colRoomid";
            this.colRoomid.ReadOnly = true;
            this.colRoomid.Visible = false;
            this.colRoomid.Width = 86;
            // 
            // colmvtid
            // 
            this.colmvtid.DataPropertyName = "mvt_id";
            this.colmvtid.HeaderText = "mvtid";
            this.colmvtid.Name = "colmvtid";
            this.colmvtid.ReadOnly = true;
            this.colmvtid.Visible = false;
            this.colmvtid.Width = 68;
            // 
            // colmvtcode
            // 
            this.colmvtcode.DataPropertyName = "mvt_code";
            this.colmvtcode.HeaderText = "mvtcode";
            this.colmvtcode.Name = "colmvtcode";
            this.colmvtcode.ReadOnly = true;
            this.colmvtcode.Visible = false;
            this.colmvtcode.Width = 90;
            // 
            // colVIP
            // 
            this.colVIP.DataPropertyName = "vip";
            this.colVIP.HeaderText = "VIP";
            this.colVIP.Name = "colVIP";
            this.colVIP.ReadOnly = true;
            this.colVIP.Width = 55;
            // 
            // frmChoiceRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 320);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DDsiteToSend);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.btnSelect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmChoiceRoom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Next Room";
            this.Load += new System.EventHandler(this.frmChoiceRoom_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox DDsiteToSend;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridViewButtonColumn Colsend;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColmrmCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColzoneName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colsite;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColWaittingPerson;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColWaitingTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRoomid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colmvtid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colmvtcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVIP;
    }
}