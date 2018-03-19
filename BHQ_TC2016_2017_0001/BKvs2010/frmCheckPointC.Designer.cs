namespace BKvs2010
{
    partial class frmCheckPointC
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCheckPointC));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.GridPatientQueue = new System.Windows.Forms.DataGridView();
            this.Coltprid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColSite = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColOut_Site = new System.Windows.Forms.DataGridViewImageColumn();
            this.ColQueue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Colpackage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Colflag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColsendQueue = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ColSendBook = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Colbtn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gridChangeDoc = new System.Windows.Forms.DataGridView();
            this.colNoChange = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQueueNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPatientName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTypeDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDocName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBtnChange = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.btnprintslip = new System.Windows.Forms.Button();
            this.btnSendToCheckB = new System.Windows.Forms.Button();
            this.GVCompleted = new System.Windows.Forms.DataGridView();
            this.colCompNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colComp_tpr_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCompQueue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCompHN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCompEN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCompName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCompDocCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCompDocName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.phm = new System.Windows.Forms.DataGridViewButtonColumn();
            this.bk = new System.Windows.Forms.DataGridViewButtonColumn();
            this.lblPatientCompleted = new System.Windows.Forms.Label();
            this.btnRetriveLab = new System.Windows.Forms.Button();
            this.lbAlertMsg = new UserControlLibrary.LabelAutoResizeFont();
            this.lbLabresult = new System.Windows.Forms.Label();
            this.btnrefresh = new System.Windows.Forms.Button();
            this.GridLab = new System.Windows.Forms.DataGridView();
            this.ColLabNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnlabCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Collab = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CollabStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSendQueueAll = new System.Windows.Forms.Button();
            this.lbListcheckbody = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.uiUserprofile1 = new BKvs2010.Usercontrols.UIUserprofile();
            this.uiMapping1 = new BKvs2010.Usercontrols.UIMapping();
            this.uiFooter1 = new BKvs2010.Usercontrols.UIFooter();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.uiMenuBar1 = new BKvs2010.Usercontrols.UIMenuBar();
            ((System.ComponentModel.ISupportInitialize)(this.GridPatientQueue)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridChangeDoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GVCompleted)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridLab)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(14, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Check Point C";
            // 
            // GridPatientQueue
            // 
            this.GridPatientQueue.AllowUserToAddRows = false;
            this.GridPatientQueue.AllowUserToDeleteRows = false;
            this.GridPatientQueue.AllowUserToResizeRows = false;
            this.GridPatientQueue.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.GridPatientQueue.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.GridPatientQueue.BackgroundColor = System.Drawing.Color.White;
            this.GridPatientQueue.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.GridPatientQueue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.GridPatientQueue.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Coltprid,
            this.colNo,
            this.ColSite,
            this.ColOut_Site,
            this.ColQueue,
            this.Column3,
            this.Column4,
            this.Colpackage,
            this.EN,
            this.Colflag,
            this.ColsendQueue,
            this.ColSendBook,
            this.Colbtn});
            this.GridPatientQueue.Location = new System.Drawing.Point(8, 119);
            this.GridPatientQueue.MultiSelect = false;
            this.GridPatientQueue.Name = "GridPatientQueue";
            this.GridPatientQueue.ReadOnly = true;
            this.GridPatientQueue.RowHeadersVisible = false;
            this.GridPatientQueue.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.GridPatientQueue.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridPatientQueue.Size = new System.Drawing.Size(721, 220);
            this.GridPatientQueue.TabIndex = 3;
            this.GridPatientQueue.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridPatientQueue_CellClick);
            this.GridPatientQueue.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridPatientQueue_CellContentClick);
            this.GridPatientQueue.SelectionChanged += new System.EventHandler(this.GridPatientQueue_SelectionChanged);
            // 
            // Coltprid
            // 
            this.Coltprid.DataPropertyName = "tpr_id";
            this.Coltprid.HeaderText = "tpr_id";
            this.Coltprid.Name = "Coltprid";
            this.Coltprid.ReadOnly = true;
            this.Coltprid.Visible = false;
            this.Coltprid.Width = 58;
            // 
            // colNo
            // 
            this.colNo.DataPropertyName = "no";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colNo.DefaultCellStyle = dataGridViewCellStyle1;
            this.colNo.HeaderText = "No";
            this.colNo.Name = "colNo";
            this.colNo.ReadOnly = true;
            this.colNo.Width = 48;
            // 
            // ColSite
            // 
            this.ColSite.DataPropertyName = "mhs_ename";
            this.ColSite.HeaderText = "Site";
            this.ColSite.Name = "ColSite";
            this.ColSite.ReadOnly = true;
            this.ColSite.Width = 55;
            // 
            // ColOut_Site
            // 
            this.ColOut_Site.DataPropertyName = "out_site";
            this.ColOut_Site.HeaderText = "Out";
            this.ColOut_Site.Name = "ColOut_Site";
            this.ColOut_Site.ReadOnly = true;
            this.ColOut_Site.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ColOut_Site.Width = 53;
            // 
            // ColQueue
            // 
            this.ColQueue.DataPropertyName = "QueueNo";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColQueue.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColQueue.HeaderText = "Queue";
            this.ColQueue.Name = "ColQueue";
            this.ColQueue.ReadOnly = true;
            this.ColQueue.Width = 70;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "HN";
            this.Column3.HeaderText = "HN";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 49;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "FullName";
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.AliceBlue;
            this.Column4.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column4.HeaderText = "Name";
            this.Column4.MinimumWidth = 150;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 150;
            // 
            // Colpackage
            // 
            this.Colpackage.DataPropertyName = "package";
            this.Colpackage.HeaderText = "";
            this.Colpackage.Name = "Colpackage";
            this.Colpackage.ReadOnly = true;
            this.Colpackage.Visible = false;
            this.Colpackage.Width = 19;
            // 
            // EN
            // 
            this.EN.DataPropertyName = "EN";
            this.EN.HeaderText = "EN";
            this.EN.Name = "EN";
            this.EN.ReadOnly = true;
            this.EN.Visible = false;
            this.EN.Width = 47;
            // 
            // Colflag
            // 
            this.Colflag.DataPropertyName = "flag";
            this.Colflag.HeaderText = "Column1";
            this.Colflag.Name = "Colflag";
            this.Colflag.ReadOnly = true;
            this.Colflag.Visible = false;
            this.Colflag.Width = 73;
            // 
            // ColsendQueue
            // 
            this.ColsendQueue.HeaderText = "";
            this.ColsendQueue.Name = "ColsendQueue";
            this.ColsendQueue.ReadOnly = true;
            this.ColsendQueue.Text = "Send Queue";
            this.ColsendQueue.UseColumnTextForButtonValue = true;
            this.ColsendQueue.Width = 5;
            // 
            // ColSendBook
            // 
            this.ColSendBook.HeaderText = "";
            this.ColSendBook.Name = "ColSendBook";
            this.ColSendBook.ReadOnly = true;
            this.ColSendBook.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColSendBook.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ColSendBook.Text = "Send Book";
            this.ColSendBook.UseColumnTextForButtonValue = true;
            this.ColSendBook.Width = 19;
            // 
            // Colbtn
            // 
            this.Colbtn.HeaderText = "";
            this.Colbtn.Name = "Colbtn";
            this.Colbtn.ReadOnly = true;
            this.Colbtn.Text = "ค้างตรวจแพทย์";
            this.Colbtn.UseColumnTextForButtonValue = true;
            this.Colbtn.Width = 5;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.gridChangeDoc);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.btnprintslip);
            this.panel2.Controls.Add(this.btnSendToCheckB);
            this.panel2.Controls.Add(this.GVCompleted);
            this.panel2.Controls.Add(this.lblPatientCompleted);
            this.panel2.Controls.Add(this.btnRetriveLab);
            this.panel2.Controls.Add(this.lbAlertMsg);
            this.panel2.Controls.Add(this.lbLabresult);
            this.panel2.Controls.Add(this.btnrefresh);
            this.panel2.Controls.Add(this.GridLab);
            this.panel2.Controls.Add(this.btnSearch);
            this.panel2.Controls.Add(this.txtSearch);
            this.panel2.Controls.Add(this.btnSendQueueAll);
            this.panel2.Controls.Add(this.lbListcheckbody);
            this.panel2.Controls.Add(this.GridPatientQueue);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.panel2.Location = new System.Drawing.Point(276, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1026, 697);
            this.panel2.TabIndex = 23;
            // 
            // gridChangeDoc
            // 
            this.gridChangeDoc.AllowUserToAddRows = false;
            this.gridChangeDoc.AllowUserToDeleteRows = false;
            this.gridChangeDoc.AllowUserToResizeRows = false;
            this.gridChangeDoc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridChangeDoc.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.gridChangeDoc.BackgroundColor = System.Drawing.Color.White;
            this.gridChangeDoc.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gridChangeDoc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridChangeDoc.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNoChange,
            this.colQueueNo,
            this.colHN,
            this.colEN,
            this.colPatientName,
            this.colTypeDesc,
            this.colDocName,
            this.colBtnChange});
            this.gridChangeDoc.Location = new System.Drawing.Point(8, 365);
            this.gridChangeDoc.Name = "gridChangeDoc";
            this.gridChangeDoc.RowHeadersVisible = false;
            this.gridChangeDoc.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.gridChangeDoc.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridChangeDoc.Size = new System.Drawing.Size(1010, 155);
            this.gridChangeDoc.TabIndex = 253;
            this.gridChangeDoc.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridChangeDoc_CellClick);
            this.gridChangeDoc.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridChangeDoc_CellContentClick);
            // 
            // colNoChange
            // 
            this.colNoChange.DataPropertyName = "no";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colNoChange.DefaultCellStyle = dataGridViewCellStyle4;
            this.colNoChange.HeaderText = "No.";
            this.colNoChange.Name = "colNoChange";
            this.colNoChange.ReadOnly = true;
            this.colNoChange.Width = 52;
            // 
            // colQueueNo
            // 
            this.colQueueNo.DataPropertyName = "queue_no";
            this.colQueueNo.HeaderText = "Queue No";
            this.colQueueNo.Name = "colQueueNo";
            this.colQueueNo.ReadOnly = true;
            this.colQueueNo.Width = 89;
            // 
            // colHN
            // 
            this.colHN.DataPropertyName = "hn_no";
            this.colHN.HeaderText = "HN No.";
            this.colHN.Name = "colHN";
            this.colHN.ReadOnly = true;
            this.colHN.Width = 72;
            // 
            // colEN
            // 
            this.colEN.DataPropertyName = "en_no";
            this.colEN.HeaderText = "EN No.";
            this.colEN.Name = "colEN";
            this.colEN.ReadOnly = true;
            this.colEN.Width = 71;
            // 
            // colPatientName
            // 
            this.colPatientName.DataPropertyName = "patient_name";
            this.colPatientName.HeaderText = "Patient Name";
            this.colPatientName.Name = "colPatientName";
            this.colPatientName.ReadOnly = true;
            this.colPatientName.Width = 109;
            // 
            // colTypeDesc
            // 
            this.colTypeDesc.DataPropertyName = "type_desc";
            this.colTypeDesc.HeaderText = "Condition Type";
            this.colTypeDesc.Name = "colTypeDesc";
            this.colTypeDesc.ReadOnly = true;
            this.colTypeDesc.Width = 118;
            // 
            // colDocName
            // 
            this.colDocName.DataPropertyName = "doctor_name";
            this.colDocName.HeaderText = "Doctor Name";
            this.colDocName.Name = "colDocName";
            this.colDocName.ReadOnly = true;
            this.colDocName.Width = 107;
            // 
            // colBtnChange
            // 
            this.colBtnChange.HeaderText = "";
            this.colBtnChange.Name = "colBtnChange";
            this.colBtnChange.ReadOnly = true;
            this.colBtnChange.Text = "Change Doctor";
            this.colBtnChange.UseColumnTextForButtonValue = true;
            this.colBtnChange.Width = 5;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.SystemColors.HotTrack;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(8, 341);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1010, 23);
            this.label2.TabIndex = 252;
            this.label2.Text = "3.2 รายชื่อผู้รับบริการที่กำลังรอพบแพทย์";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnprintslip
            // 
            this.btnprintslip.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnprintslip.Image = ((System.Drawing.Image)(resources.GetObject("btnprintslip.Image")));
            this.btnprintslip.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnprintslip.Location = new System.Drawing.Point(428, 63);
            this.btnprintslip.Name = "btnprintslip";
            this.btnprintslip.Size = new System.Drawing.Size(96, 25);
            this.btnprintslip.TabIndex = 251;
            this.btnprintslip.Text = "Queue Slip";
            this.btnprintslip.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnprintslip.UseVisualStyleBackColor = true;
            this.btnprintslip.Click += new System.EventHandler(this.btnprintslip_Click);
            // 
            // btnSendToCheckB
            // 
            this.btnSendToCheckB.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSendToCheckB.Image = ((System.Drawing.Image)(resources.GetObject("btnSendToCheckB.Image")));
            this.btnSendToCheckB.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSendToCheckB.Location = new System.Drawing.Point(530, 63);
            this.btnSendToCheckB.Name = "btnSendToCheckB";
            this.btnSendToCheckB.Size = new System.Drawing.Size(124, 25);
            this.btnSendToCheckB.TabIndex = 250;
            this.btnSendToCheckB.Text = "Send to Check B";
            this.btnSendToCheckB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSendToCheckB.UseVisualStyleBackColor = true;
            this.btnSendToCheckB.Click += new System.EventHandler(this.btnSendToCheckB_Click);
            // 
            // GVCompleted
            // 
            this.GVCompleted.AllowUserToAddRows = false;
            this.GVCompleted.AllowUserToDeleteRows = false;
            this.GVCompleted.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GVCompleted.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.GVCompleted.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.GVCompleted.BackgroundColor = System.Drawing.Color.White;
            this.GVCompleted.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.GVCompleted.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.GVCompleted.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCompNo,
            this.colComp_tpr_id,
            this.colCompQueue,
            this.colCompHN,
            this.colCompEN,
            this.colCompName,
            this.colCompDocCode,
            this.colCompDocName,
            this.phm,
            this.bk});
            this.GVCompleted.Location = new System.Drawing.Point(8, 546);
            this.GVCompleted.Name = "GVCompleted";
            this.GVCompleted.ReadOnly = true;
            this.GVCompleted.RowHeadersVisible = false;
            this.GVCompleted.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.GVCompleted.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GVCompleted.Size = new System.Drawing.Size(1010, 150);
            this.GVCompleted.TabIndex = 51;
            this.GVCompleted.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GVCompleted_CellClick);
            this.GVCompleted.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GVCompleted_CellContentClick);
            // 
            // colCompNo
            // 
            this.colCompNo.DataPropertyName = "No";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colCompNo.DefaultCellStyle = dataGridViewCellStyle5;
            this.colCompNo.HeaderText = "No.";
            this.colCompNo.Name = "colCompNo";
            this.colCompNo.ReadOnly = true;
            this.colCompNo.Width = 52;
            // 
            // colComp_tpr_id
            // 
            this.colComp_tpr_id.DataPropertyName = "tpr_id";
            this.colComp_tpr_id.HeaderText = "tpr_id";
            this.colComp_tpr_id.Name = "colComp_tpr_id";
            this.colComp_tpr_id.ReadOnly = true;
            this.colComp_tpr_id.Visible = false;
            this.colComp_tpr_id.Width = 66;
            // 
            // colCompQueue
            // 
            this.colCompQueue.DataPropertyName = "QueueNo";
            this.colCompQueue.HeaderText = "Queue No.";
            this.colCompQueue.Name = "colCompQueue";
            this.colCompQueue.ReadOnly = true;
            this.colCompQueue.Width = 93;
            // 
            // colCompHN
            // 
            this.colCompHN.DataPropertyName = "Hn";
            this.colCompHN.HeaderText = "HN";
            this.colCompHN.Name = "colCompHN";
            this.colCompHN.ReadOnly = true;
            this.colCompHN.Width = 49;
            // 
            // colCompEN
            // 
            this.colCompEN.DataPropertyName = "En";
            this.colCompEN.HeaderText = "EN";
            this.colCompEN.Name = "colCompEN";
            this.colCompEN.ReadOnly = true;
            this.colCompEN.Width = 48;
            // 
            // colCompName
            // 
            this.colCompName.DataPropertyName = "Name";
            this.colCompName.HeaderText = "Name";
            this.colCompName.Name = "colCompName";
            this.colCompName.ReadOnly = true;
            this.colCompName.Width = 66;
            // 
            // colCompDocCode
            // 
            this.colCompDocCode.DataPropertyName = "DoctorCode";
            this.colCompDocCode.HeaderText = "Doctor Code";
            this.colCompDocCode.Name = "colCompDocCode";
            this.colCompDocCode.ReadOnly = true;
            this.colCompDocCode.Width = 103;
            // 
            // colCompDocName
            // 
            this.colCompDocName.DataPropertyName = "DoctorName";
            this.colCompDocName.HeaderText = "Doctor Name";
            this.colCompDocName.Name = "colCompDocName";
            this.colCompDocName.ReadOnly = true;
            this.colCompDocName.Width = 107;
            // 
            // phm
            // 
            this.phm.HeaderText = "";
            this.phm.Name = "phm";
            this.phm.ReadOnly = true;
            this.phm.Text = "Send PHM";
            this.phm.UseColumnTextForButtonValue = true;
            this.phm.Visible = false;
            this.phm.Width = 5;
            // 
            // bk
            // 
            this.bk.HeaderText = "";
            this.bk.Name = "bk";
            this.bk.ReadOnly = true;
            this.bk.Text = "Send Book";
            this.bk.UseColumnTextForButtonValue = true;
            this.bk.Width = 5;
            // 
            // lblPatientCompleted
            // 
            this.lblPatientCompleted.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPatientCompleted.BackColor = System.Drawing.SystemColors.HotTrack;
            this.lblPatientCompleted.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblPatientCompleted.ForeColor = System.Drawing.Color.White;
            this.lblPatientCompleted.Location = new System.Drawing.Point(8, 522);
            this.lblPatientCompleted.Name = "lblPatientCompleted";
            this.lblPatientCompleted.Size = new System.Drawing.Size(1010, 23);
            this.lblPatientCompleted.TabIndex = 50;
            this.lblPatientCompleted.Text = "3.3 รายชื่อผู้รับบริการที่พบแพทย์เรียบร้อยแล้ว";
            this.lblPatientCompleted.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnRetriveLab
            // 
            this.btnRetriveLab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRetriveLab.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnRetriveLab.Image = ((System.Drawing.Image)(resources.GetObject("btnRetriveLab.Image")));
            this.btnRetriveLab.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRetriveLab.Location = new System.Drawing.Point(698, 62);
            this.btnRetriveLab.Name = "btnRetriveLab";
            this.btnRetriveLab.Size = new System.Drawing.Size(121, 25);
            this.btnRetriveLab.TabIndex = 48;
            this.btnRetriveLab.Text = "Retrieve for lab";
            this.btnRetriveLab.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRetriveLab.UseVisualStyleBackColor = true;
            this.btnRetriveLab.Click += new System.EventHandler(this.btnRetriveLab_Click);
            // 
            // lbAlertMsg
            // 
            this.lbAlertMsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbAlertMsg.AutoResizeFont = true;
            this.lbAlertMsg.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbAlertMsg.ForeColor = System.Drawing.Color.Red;
            this.lbAlertMsg.Location = new System.Drawing.Point(15, 6);
            this.lbAlertMsg.MaxFontSize = 11.5F;
            this.lbAlertMsg.Name = "lbAlertMsg";
            this.lbAlertMsg.Size = new System.Drawing.Size(991, 27);
            this.lbAlertMsg.TabIndex = 49;
            this.lbAlertMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbLabresult
            // 
            this.lbLabresult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbLabresult.BackColor = System.Drawing.SystemColors.HotTrack;
            this.lbLabresult.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbLabresult.ForeColor = System.Drawing.Color.White;
            this.lbLabresult.Location = new System.Drawing.Point(735, 95);
            this.lbLabresult.Name = "lbLabresult";
            this.lbLabresult.Size = new System.Drawing.Size(283, 23);
            this.lbLabresult.TabIndex = 21;
            this.lbLabresult.Text = "Lab Result (Total 0 คน)";
            this.lbLabresult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnrefresh
            // 
            this.btnrefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnrefresh.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnrefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnrefresh.Image")));
            this.btnrefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnrefresh.Location = new System.Drawing.Point(825, 62);
            this.btnrefresh.Name = "btnrefresh";
            this.btnrefresh.Size = new System.Drawing.Size(80, 25);
            this.btnrefresh.TabIndex = 48;
            this.btnrefresh.Text = "Refresh";
            this.btnrefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnrefresh.UseVisualStyleBackColor = true;
            this.btnrefresh.Click += new System.EventHandler(this.btnrefresh_Click);
            // 
            // GridLab
            // 
            this.GridLab.AllowUserToAddRows = false;
            this.GridLab.AllowUserToDeleteRows = false;
            this.GridLab.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GridLab.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.GridLab.BackgroundColor = System.Drawing.Color.White;
            this.GridLab.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.GridLab.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.GridLab.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColLabNo,
            this.columnlabCode,
            this.Collab,
            this.CollabStatus});
            this.GridLab.Location = new System.Drawing.Point(735, 119);
            this.GridLab.Name = "GridLab";
            this.GridLab.ReadOnly = true;
            this.GridLab.RowHeadersVisible = false;
            this.GridLab.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.GridLab.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridLab.Size = new System.Drawing.Size(283, 220);
            this.GridLab.TabIndex = 46;
            // 
            // ColLabNo
            // 
            this.ColLabNo.DataPropertyName = "no";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColLabNo.DefaultCellStyle = dataGridViewCellStyle6;
            this.ColLabNo.HeaderText = "No.";
            this.ColLabNo.Name = "ColLabNo";
            this.ColLabNo.ReadOnly = true;
            this.ColLabNo.Width = 52;
            // 
            // columnlabCode
            // 
            this.columnlabCode.DataPropertyName = "LabCode";
            this.columnlabCode.HeaderText = "Code";
            this.columnlabCode.Name = "columnlabCode";
            this.columnlabCode.ReadOnly = true;
            this.columnlabCode.Visible = false;
            this.columnlabCode.Width = 57;
            // 
            // Collab
            // 
            this.Collab.DataPropertyName = "LabName";
            this.Collab.HeaderText = "Name";
            this.Collab.Name = "Collab";
            this.Collab.ReadOnly = true;
            this.Collab.Width = 66;
            // 
            // CollabStatus
            // 
            this.CollabStatus.DataPropertyName = "LabStatus";
            this.CollabStatus.HeaderText = "Status";
            this.CollabStatus.Name = "CollabStatus";
            this.CollabStatus.ReadOnly = true;
            this.CollabStatus.Width = 69;
            // 
            // btnSearch
            // 
            this.btnSearch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSearch.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(185, 63);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(72, 25);
            this.btnSearch.TabIndex = 45;
            this.btnSearch.Text = "Search";
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Location = new System.Drawing.Point(8, 64);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(175, 23);
            this.txtSearch.TabIndex = 44;
            // 
            // btnSendQueueAll
            // 
            this.btnSendQueueAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendQueueAll.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSendQueueAll.Image = ((System.Drawing.Image)(resources.GetObject("btnSendQueueAll.Image")));
            this.btnSendQueueAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSendQueueAll.Location = new System.Drawing.Point(911, 62);
            this.btnSendQueueAll.Name = "btnSendQueueAll";
            this.btnSendQueueAll.Size = new System.Drawing.Size(107, 25);
            this.btnSendQueueAll.TabIndex = 43;
            this.btnSendQueueAll.Text = "Send Queue";
            this.btnSendQueueAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSendQueueAll.UseVisualStyleBackColor = true;
            this.btnSendQueueAll.Click += new System.EventHandler(this.btnSendQueueAll_Click);
            // 
            // lbListcheckbody
            // 
            this.lbListcheckbody.BackColor = System.Drawing.SystemColors.HotTrack;
            this.lbListcheckbody.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbListcheckbody.ForeColor = System.Drawing.Color.White;
            this.lbListcheckbody.Location = new System.Drawing.Point(8, 95);
            this.lbListcheckbody.Name = "lbListcheckbody";
            this.lbListcheckbody.Size = new System.Drawing.Size(721, 23);
            this.lbListcheckbody.TabIndex = 21;
            this.lbListcheckbody.Text = "3.1 รายชื่อผู้รับบริการที่มีผลการตรวจร่างกายครบทุกรายการ";
            this.lbListcheckbody.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.panel5, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 25);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1301, 935);
            this.tableLayoutPanel1.TabIndex = 27;
            // 
            // panel5
            // 
            this.panel5.AutoScroll = true;
            this.panel5.Controls.Add(this.panel2);
            this.panel5.Controls.Add(this.panel1);
            this.panel5.Controls.Add(this.uiFooter1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Margin = new System.Windows.Forms.Padding(0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1302, 935);
            this.panel5.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.uiUserprofile1);
            this.panel1.Controls.Add(this.uiMapping1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(276, 697);
            this.panel1.TabIndex = 25;
            // 
            // uiUserprofile1
            // 
            this.uiUserprofile1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(113)))), ((int)(((byte)(188)))));
            this.uiUserprofile1.Location = new System.Drawing.Point(0, 0);
            this.uiUserprofile1.Margin = new System.Windows.Forms.Padding(0);
            this.uiUserprofile1.Name = "uiUserprofile1";
            this.uiUserprofile1.Size = new System.Drawing.Size(275, 430);
            this.uiUserprofile1.TabIndex = 0;
            // 
            // uiMapping1
            // 
            this.uiMapping1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiMapping1.BackColor = System.Drawing.Color.White;
            this.uiMapping1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.uiMapping1.Location = new System.Drawing.Point(0, 433);
            this.uiMapping1.Name = "uiMapping1";
            this.uiMapping1.Size = new System.Drawing.Size(275, 258);
            this.uiMapping1.TabIndex = 1;
            // 
            // uiFooter1
            // 
            this.uiFooter1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.uiFooter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uiFooter1.IsEditEMR = false;
            this.uiFooter1.Location = new System.Drawing.Point(0, 697);
            this.uiFooter1.mhs_id = null;
            this.uiFooter1.Name = "uiFooter1";
            this.uiFooter1.RoomCode = null;
            this.uiFooter1.Size = new System.Drawing.Size(1302, 238);
            this.uiFooter1.StrSearch = null;
            this.uiFooter1.TabIndex = 24;
            // 
            // timer1
            // 
            this.timer1.Interval = 19000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "exchange.png");
            this.imageList1.Images.SetKeyName(1, "LR.gif");
            this.imageList1.Images.SetKeyName(2, "blank.jpg");
            // 
            // uiMenuBar1
            // 
            this.uiMenuBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiMenuBar1.Location = new System.Drawing.Point(0, 0);
            this.uiMenuBar1.Name = "uiMenuBar1";
            this.uiMenuBar1.Size = new System.Drawing.Size(1301, 25);
            this.uiMenuBar1.TabIndex = 39;
            // 
            // frmCheckPointC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(1240, 960);
            this.ClientSize = new System.Drawing.Size(1318, 742);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.uiMenuBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCheckPointC";
            this.Text = "Check Point C";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCheckPointC_FormClosing);
            this.Load += new System.EventHandler(this.frmCheckPointC_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GridPatientQueue)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridChangeDoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GVCompleted)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridLab)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView GridPatientQueue;
        private Usercontrols.UIUserprofile uiUserprofile1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbListcheckbody;
        private System.Windows.Forms.Button btnSendQueueAll;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel5;
        private Usercontrols.UIMapping uiMapping1;
        private System.Windows.Forms.Panel panel1;
        private Usercontrols.UIFooter uiFooter1;
        private System.Windows.Forms.Button btnrefresh;
        private System.Windows.Forms.Label lbLabresult;
        private UserControlLibrary.LabelAutoResizeFont lbAlertMsg;
        private System.Windows.Forms.Timer timer1;
        private Usercontrols.UIMenuBar uiMenuBar1;
        private System.Windows.Forms.DataGridView GridLab;
        private System.Windows.Forms.Button btnRetriveLab;
        private System.Windows.Forms.Label lblPatientCompleted;
        private System.Windows.Forms.DataGridView GVCompleted;
        private System.Windows.Forms.Button btnSendToCheckB;
        private System.Windows.Forms.Button btnprintslip;
        private System.Windows.Forms.DataGridView gridChangeDoc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNoChange;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQueueNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHN;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEN;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPatientName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTypeDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDocName;
        private System.Windows.Forms.DataGridViewButtonColumn colBtnChange;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColLabNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnlabCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Collab;
        private System.Windows.Forms.DataGridViewTextBoxColumn CollabStatus;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Coltprid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColSite;
        private System.Windows.Forms.DataGridViewImageColumn ColOut_Site;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColQueue;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Colpackage;
        private System.Windows.Forms.DataGridViewTextBoxColumn EN;
        private System.Windows.Forms.DataGridViewTextBoxColumn Colflag;
        private System.Windows.Forms.DataGridViewButtonColumn ColsendQueue;
        private System.Windows.Forms.DataGridViewButtonColumn ColSendBook;
        private System.Windows.Forms.DataGridViewButtonColumn Colbtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCompNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colComp_tpr_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCompQueue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCompHN;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCompEN;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCompName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCompDocCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCompDocName;
        private System.Windows.Forms.DataGridViewButtonColumn phm;
        private System.Windows.Forms.DataGridViewButtonColumn bk;
    }
}