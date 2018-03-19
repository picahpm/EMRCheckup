namespace BKvs2010.UserControlEMR
{
    partial class BookTransaction
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelRadioBinding2 = new UserControlLibrary.PanelRadioBinding();
            this.gvTransaction = new System.Windows.Forms.DataGridView();
            this.cNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cEvent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cEventName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cTran = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TransactionShow = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDel = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tbeidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tpridDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mbeidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tberemarkDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbedateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbeactiveDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbecreatebyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbecreatedateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbeupdatebyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbeupdatedateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mstbookeventDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trnpatientregiDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trnbookeventBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panelRadioBinding1 = new UserControlLibrary.PanelRadioBinding();
            this.label14 = new System.Windows.Forms.Label();
            this.txtTime = new System.Windows.Forms.MaskedTextBox();
            this.dateTransaction = new System.Windows.Forms.DateTimePicker();
            this.btnAddTransaction = new System.Windows.Forms.Button();
            this.txtTransaction = new System.Windows.Forms.TextBox();
            this.comboEvent = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelRadioBinding2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvTransaction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trnbookeventBindingSource)).BeginInit();
            this.panelRadioBinding1.SuspendLayout();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoScrollMinSize = new System.Drawing.Size(428, 677);
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(428, 677);
            this.panel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panelRadioBinding2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panelRadioBinding1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel7, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 144F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(428, 677);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panelRadioBinding2
            // 
            this.panelRadioBinding2.AbleNull = false;
            this.panelRadioBinding2.BorderRadius = false;
            this.panelRadioBinding2.Controls.Add(this.gvTransaction);
            this.panelRadioBinding2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRadioBinding2.Location = new System.Drawing.Point(0, 170);
            this.panelRadioBinding2.Margin = new System.Windows.Forms.Padding(0);
            this.panelRadioBinding2.Name = "panelRadioBinding2";
            this.panelRadioBinding2.ReadOnly = false;
            this.panelRadioBinding2.Size = new System.Drawing.Size(428, 507);
            this.panelRadioBinding2.TabIndex = 8;
            // 
            // gvTransaction
            // 
            this.gvTransaction.AllowUserToAddRows = false;
            this.gvTransaction.AllowUserToDeleteRows = false;
            this.gvTransaction.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvTransaction.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvTransaction.AutoGenerateColumns = false;
            this.gvTransaction.BackgroundColor = System.Drawing.Color.White;
            this.gvTransaction.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvTransaction.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvTransaction.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvTransaction.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cNo,
            this.cEvent,
            this.cEventName,
            this.cTran,
            this.TransactionShow,
            this.colUser,
            this.cDate,
            this.cTime,
            this.cDel,
            this.tbeidDataGridViewTextBoxColumn,
            this.tpridDataGridViewTextBoxColumn,
            this.mbeidDataGridViewTextBoxColumn,
            this.tberemarkDataGridViewTextBoxColumn,
            this.tbedateDataGridViewTextBoxColumn,
            this.tbeactiveDataGridViewTextBoxColumn,
            this.tbecreatebyDataGridViewTextBoxColumn,
            this.tbecreatedateDataGridViewTextBoxColumn,
            this.tbeupdatebyDataGridViewTextBoxColumn,
            this.tbeupdatedateDataGridViewTextBoxColumn,
            this.mstbookeventDataGridViewTextBoxColumn,
            this.trnpatientregiDataGridViewTextBoxColumn});
            this.gvTransaction.DataSource = this.trnbookeventBindingSource;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvTransaction.DefaultCellStyle = dataGridViewCellStyle6;
            this.gvTransaction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvTransaction.Location = new System.Drawing.Point(0, 0);
            this.gvTransaction.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gvTransaction.Name = "gvTransaction";
            this.gvTransaction.ReadOnly = true;
            this.gvTransaction.RowHeadersVisible = false;
            this.gvTransaction.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvTransaction.Size = new System.Drawing.Size(428, 507);
            this.gvTransaction.TabIndex = 50;
            this.gvTransaction.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvTransaction_CellContentClick);
            this.gvTransaction.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.gvTransaction_CellPainting);
            this.gvTransaction.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.gvTransaction_RowPrePaint);
            // 
            // cNo
            // 
            this.cNo.HeaderText = "No.";
            this.cNo.Name = "cNo";
            this.cNo.ReadOnly = true;
            this.cNo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.cNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.cNo.Width = 30;
            // 
            // cEvent
            // 
            this.cEvent.DataPropertyName = "mbe_id";
            this.cEvent.HeaderText = "Event";
            this.cEvent.Name = "cEvent";
            this.cEvent.ReadOnly = true;
            this.cEvent.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.cEvent.Visible = false;
            // 
            // cEventName
            // 
            this.cEventName.HeaderText = "Event";
            this.cEventName.Name = "cEventName";
            this.cEventName.ReadOnly = true;
            this.cEventName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.cEventName.Width = 80;
            // 
            // cTran
            // 
            this.cTran.DataPropertyName = "tbe_remark";
            this.cTran.HeaderText = "Transaction";
            this.cTran.Name = "cTran";
            this.cTran.ReadOnly = true;
            this.cTran.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.cTran.Visible = false;
            this.cTran.Width = 290;
            // 
            // TransactionShow
            // 
            this.TransactionShow.HeaderText = "Transaction";
            this.TransactionShow.Name = "TransactionShow";
            this.TransactionShow.ReadOnly = true;
            this.TransactionShow.Width = 170;
            // 
            // colUser
            // 
            this.colUser.HeaderText = "User";
            this.colUser.Name = "colUser";
            this.colUser.ReadOnly = true;
            this.colUser.Width = 80;
            // 
            // cDate
            // 
            this.cDate.DataPropertyName = "tbe_date";
            dataGridViewCellStyle3.Format = "dd/MM/yyyy";
            this.cDate.DefaultCellStyle = dataGridViewCellStyle3;
            this.cDate.HeaderText = "Date";
            this.cDate.Name = "cDate";
            this.cDate.ReadOnly = true;
            this.cDate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.cDate.Width = 70;
            // 
            // cTime
            // 
            this.cTime.DataPropertyName = "tbe_date";
            dataGridViewCellStyle4.Format = "HH:mm";
            this.cTime.DefaultCellStyle = dataGridViewCellStyle4;
            this.cTime.HeaderText = "Time";
            this.cTime.Name = "cTime";
            this.cTime.ReadOnly = true;
            this.cTime.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.cTime.Width = 60;
            // 
            // cDel
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Red;
            this.cDel.DefaultCellStyle = dataGridViewCellStyle5;
            this.cDel.HeaderText = "";
            this.cDel.Name = "cDel";
            this.cDel.ReadOnly = true;
            this.cDel.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.cDel.Text = "X";
            this.cDel.UseColumnTextForButtonValue = true;
            this.cDel.Visible = false;
            this.cDel.Width = 25;
            // 
            // tbeidDataGridViewTextBoxColumn
            // 
            this.tbeidDataGridViewTextBoxColumn.DataPropertyName = "tbe_id";
            this.tbeidDataGridViewTextBoxColumn.HeaderText = "tbe_id";
            this.tbeidDataGridViewTextBoxColumn.Name = "tbeidDataGridViewTextBoxColumn";
            this.tbeidDataGridViewTextBoxColumn.ReadOnly = true;
            this.tbeidDataGridViewTextBoxColumn.Visible = false;
            // 
            // tpridDataGridViewTextBoxColumn
            // 
            this.tpridDataGridViewTextBoxColumn.DataPropertyName = "tpr_id";
            this.tpridDataGridViewTextBoxColumn.HeaderText = "tpr_id";
            this.tpridDataGridViewTextBoxColumn.Name = "tpridDataGridViewTextBoxColumn";
            this.tpridDataGridViewTextBoxColumn.ReadOnly = true;
            this.tpridDataGridViewTextBoxColumn.Visible = false;
            // 
            // mbeidDataGridViewTextBoxColumn
            // 
            this.mbeidDataGridViewTextBoxColumn.DataPropertyName = "mbe_id";
            this.mbeidDataGridViewTextBoxColumn.HeaderText = "mbe_id";
            this.mbeidDataGridViewTextBoxColumn.Name = "mbeidDataGridViewTextBoxColumn";
            this.mbeidDataGridViewTextBoxColumn.ReadOnly = true;
            this.mbeidDataGridViewTextBoxColumn.Visible = false;
            // 
            // tberemarkDataGridViewTextBoxColumn
            // 
            this.tberemarkDataGridViewTextBoxColumn.DataPropertyName = "tbe_remark";
            this.tberemarkDataGridViewTextBoxColumn.HeaderText = "tbe_remark";
            this.tberemarkDataGridViewTextBoxColumn.Name = "tberemarkDataGridViewTextBoxColumn";
            this.tberemarkDataGridViewTextBoxColumn.ReadOnly = true;
            this.tberemarkDataGridViewTextBoxColumn.Visible = false;
            // 
            // tbedateDataGridViewTextBoxColumn
            // 
            this.tbedateDataGridViewTextBoxColumn.DataPropertyName = "tbe_date";
            this.tbedateDataGridViewTextBoxColumn.HeaderText = "tbe_date";
            this.tbedateDataGridViewTextBoxColumn.Name = "tbedateDataGridViewTextBoxColumn";
            this.tbedateDataGridViewTextBoxColumn.ReadOnly = true;
            this.tbedateDataGridViewTextBoxColumn.Visible = false;
            // 
            // tbeactiveDataGridViewTextBoxColumn
            // 
            this.tbeactiveDataGridViewTextBoxColumn.DataPropertyName = "tbe_active";
            this.tbeactiveDataGridViewTextBoxColumn.HeaderText = "tbe_active";
            this.tbeactiveDataGridViewTextBoxColumn.Name = "tbeactiveDataGridViewTextBoxColumn";
            this.tbeactiveDataGridViewTextBoxColumn.ReadOnly = true;
            this.tbeactiveDataGridViewTextBoxColumn.Visible = false;
            // 
            // tbecreatebyDataGridViewTextBoxColumn
            // 
            this.tbecreatebyDataGridViewTextBoxColumn.DataPropertyName = "tbe_create_by";
            this.tbecreatebyDataGridViewTextBoxColumn.HeaderText = "tbe_create_by";
            this.tbecreatebyDataGridViewTextBoxColumn.Name = "tbecreatebyDataGridViewTextBoxColumn";
            this.tbecreatebyDataGridViewTextBoxColumn.ReadOnly = true;
            this.tbecreatebyDataGridViewTextBoxColumn.Visible = false;
            // 
            // tbecreatedateDataGridViewTextBoxColumn
            // 
            this.tbecreatedateDataGridViewTextBoxColumn.DataPropertyName = "tbe_create_date";
            this.tbecreatedateDataGridViewTextBoxColumn.HeaderText = "tbe_create_date";
            this.tbecreatedateDataGridViewTextBoxColumn.Name = "tbecreatedateDataGridViewTextBoxColumn";
            this.tbecreatedateDataGridViewTextBoxColumn.ReadOnly = true;
            this.tbecreatedateDataGridViewTextBoxColumn.Visible = false;
            // 
            // tbeupdatebyDataGridViewTextBoxColumn
            // 
            this.tbeupdatebyDataGridViewTextBoxColumn.DataPropertyName = "tbe_update_by";
            this.tbeupdatebyDataGridViewTextBoxColumn.HeaderText = "tbe_update_by";
            this.tbeupdatebyDataGridViewTextBoxColumn.Name = "tbeupdatebyDataGridViewTextBoxColumn";
            this.tbeupdatebyDataGridViewTextBoxColumn.ReadOnly = true;
            this.tbeupdatebyDataGridViewTextBoxColumn.Visible = false;
            // 
            // tbeupdatedateDataGridViewTextBoxColumn
            // 
            this.tbeupdatedateDataGridViewTextBoxColumn.DataPropertyName = "tbe_update_date";
            this.tbeupdatedateDataGridViewTextBoxColumn.HeaderText = "tbe_update_date";
            this.tbeupdatedateDataGridViewTextBoxColumn.Name = "tbeupdatedateDataGridViewTextBoxColumn";
            this.tbeupdatedateDataGridViewTextBoxColumn.ReadOnly = true;
            this.tbeupdatedateDataGridViewTextBoxColumn.Visible = false;
            // 
            // mstbookeventDataGridViewTextBoxColumn
            // 
            this.mstbookeventDataGridViewTextBoxColumn.DataPropertyName = "mst_book_event";
            this.mstbookeventDataGridViewTextBoxColumn.HeaderText = "mst_book_event";
            this.mstbookeventDataGridViewTextBoxColumn.Name = "mstbookeventDataGridViewTextBoxColumn";
            this.mstbookeventDataGridViewTextBoxColumn.ReadOnly = true;
            this.mstbookeventDataGridViewTextBoxColumn.Visible = false;
            // 
            // trnpatientregiDataGridViewTextBoxColumn
            // 
            this.trnpatientregiDataGridViewTextBoxColumn.DataPropertyName = "trn_patient_regi";
            this.trnpatientregiDataGridViewTextBoxColumn.HeaderText = "trn_patient_regi";
            this.trnpatientregiDataGridViewTextBoxColumn.Name = "trnpatientregiDataGridViewTextBoxColumn";
            this.trnpatientregiDataGridViewTextBoxColumn.ReadOnly = true;
            this.trnpatientregiDataGridViewTextBoxColumn.Visible = false;
            // 
            // trnbookeventBindingSource
            // 
            this.trnbookeventBindingSource.DataSource = typeof(DBCheckup.trn_book_event);
            this.trnbookeventBindingSource.Filter = "tbe_active = 1";
            // 
            // panelRadioBinding1
            // 
            this.panelRadioBinding1.AbleNull = false;
            this.panelRadioBinding1.BorderRadius = true;
            this.panelRadioBinding1.Controls.Add(this.label14);
            this.panelRadioBinding1.Controls.Add(this.txtTime);
            this.panelRadioBinding1.Controls.Add(this.dateTransaction);
            this.panelRadioBinding1.Controls.Add(this.btnAddTransaction);
            this.panelRadioBinding1.Controls.Add(this.txtTransaction);
            this.panelRadioBinding1.Controls.Add(this.comboEvent);
            this.panelRadioBinding1.Controls.Add(this.label21);
            this.panelRadioBinding1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRadioBinding1.Location = new System.Drawing.Point(3, 3);
            this.panelRadioBinding1.Name = "panelRadioBinding1";
            this.panelRadioBinding1.ReadOnly = false;
            this.panelRadioBinding1.Size = new System.Drawing.Size(422, 138);
            this.panelRadioBinding1.TabIndex = 3;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(13, 111);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 13);
            this.label14.TabIndex = 246;
            this.label14.Text = "DateTime";
            // 
            // txtTime
            // 
            this.txtTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTime.Location = new System.Drawing.Point(195, 107);
            this.txtTime.Mask = "00:00";
            this.txtTime.Name = "txtTime";
            this.txtTime.Size = new System.Drawing.Size(50, 20);
            this.txtTime.TabIndex = 245;
            this.txtTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dateTransaction
            // 
            this.dateTransaction.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTransaction.Location = new System.Drawing.Point(79, 107);
            this.dateTransaction.Name = "dateTransaction";
            this.dateTransaction.Size = new System.Drawing.Size(110, 20);
            this.dateTransaction.TabIndex = 243;
            // 
            // btnAddTransaction
            // 
            this.btnAddTransaction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddTransaction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnAddTransaction.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddTransaction.Location = new System.Drawing.Point(305, 105);
            this.btnAddTransaction.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAddTransaction.Name = "btnAddTransaction";
            this.btnAddTransaction.Size = new System.Drawing.Size(101, 26);
            this.btnAddTransaction.TabIndex = 244;
            this.btnAddTransaction.Text = "Add Transaction";
            this.btnAddTransaction.UseVisualStyleBackColor = true;
            this.btnAddTransaction.Click += new System.EventHandler(this.btnAddTransaction_Click);
            // 
            // txtTransaction
            // 
            this.txtTransaction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTransaction.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTransaction.Location = new System.Drawing.Point(16, 38);
            this.txtTransaction.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTransaction.Multiline = true;
            this.txtTransaction.Name = "txtTransaction";
            this.txtTransaction.Size = new System.Drawing.Size(390, 61);
            this.txtTransaction.TabIndex = 242;
            // 
            // comboEvent
            // 
            this.comboEvent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboEvent.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboEvent.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboEvent.FormattingEnabled = true;
            this.comboEvent.Location = new System.Drawing.Point(58, 9);
            this.comboEvent.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboEvent.Name = "comboEvent";
            this.comboEvent.Size = new System.Drawing.Size(348, 21);
            this.comboEvent.TabIndex = 241;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(13, 12);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(35, 13);
            this.label21.TabIndex = 235;
            this.label21.Text = "Event";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.label5);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(0, 144);
            this.panel7.Margin = new System.Windows.Forms.Padding(0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(428, 26);
            this.panel7.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.RoyalBlue;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(428, 26);
            this.label5.TabIndex = 224;
            this.label5.Text = " Transaction";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BookTransaction
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "BookTransaction";
            this.Size = new System.Drawing.Size(428, 677);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelRadioBinding2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvTransaction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trnbookeventBindingSource)).EndInit();
            this.panelRadioBinding1.ResumeLayout(false);
            this.panelRadioBinding1.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private UserControlLibrary.PanelRadioBinding panelRadioBinding1;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboEvent;
        private System.Windows.Forms.TextBox txtTransaction;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.MaskedTextBox txtTime;
        private System.Windows.Forms.DateTimePicker dateTransaction;
        private System.Windows.Forms.Button btnAddTransaction;
        private UserControlLibrary.PanelRadioBinding panelRadioBinding2;
        private System.Windows.Forms.DataGridView gvTransaction;
        private System.Windows.Forms.BindingSource trnbookeventBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn cNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn cEvent;
        private System.Windows.Forms.DataGridViewTextBoxColumn cEventName;
        private System.Windows.Forms.DataGridViewTextBoxColumn cTran;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransactionShow;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn cTime;
        private System.Windows.Forms.DataGridViewButtonColumn cDel;
        private System.Windows.Forms.DataGridViewTextBoxColumn tbeidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tpridDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mbeidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tberemarkDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tbedateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tbeactiveDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tbecreatebyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tbecreatedateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tbeupdatebyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tbeupdatedateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mstbookeventDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn trnpatientregiDataGridViewTextBoxColumn;
    }
}
