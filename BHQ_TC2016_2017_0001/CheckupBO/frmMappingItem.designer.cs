namespace CheckupBO
{
    partial class frmMappingItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMappingItem));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClearUserName = new System.Windows.Forms.Button();
            this.btnSearchUsername = new System.Windows.Forms.Button();
            this.GridOrderplan = new System.Windows.Forms.DataGridView();
            this.h1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.h2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.h3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.h4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mopidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mvtidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mopitemrowidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mopoditemcodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mopoditemnameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mopcreatebyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mopcreatedateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mopupdatebyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mopupdatedateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.msteventDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PackageItembindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.GBAddEdit = new System.Windows.Forms.GroupBox();
            this.DD_EventName = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtItemRowID = new System.Windows.Forms.TextBox();
            this.txtItemName = new System.Windows.Forms.TextBox();
            this.txtItemCode = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.lbmsgalert = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnEditUser = new System.Windows.Forms.Button();
            this.btnAddNewUser = new System.Windows.Forms.Button();
            this.btnClaear_cancel = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridOrderplan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PackageItembindingSource1)).BeginInit();
            this.GBAddEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtUsername);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnClearUserName);
            this.groupBox2.Controls.Add(this.btnSearchUsername);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.groupBox2.Location = new System.Drawing.Point(12, 5);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(422, 48);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Search";
            // 
            // txtUsername
            // 
            this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUsername.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtUsername.Location = new System.Drawing.Point(86, 18);
            this.txtUsername.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(137, 23);
            this.txtUsername.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(4, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Item Name : ";
            // 
            // btnClearUserName
            // 
            this.btnClearUserName.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnClearUserName.Location = new System.Drawing.Point(323, 14);
            this.btnClearUserName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClearUserName.Name = "btnClearUserName";
            this.btnClearUserName.Size = new System.Drawing.Size(87, 28);
            this.btnClearUserName.TabIndex = 5;
            this.btnClearUserName.Text = "&Clear";
            this.btnClearUserName.UseVisualStyleBackColor = true;
            this.btnClearUserName.Click += new System.EventHandler(this.btnClearUserName_Click);
            // 
            // btnSearchUsername
            // 
            this.btnSearchUsername.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSearchUsername.Location = new System.Drawing.Point(230, 14);
            this.btnSearchUsername.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSearchUsername.Name = "btnSearchUsername";
            this.btnSearchUsername.Size = new System.Drawing.Size(87, 28);
            this.btnSearchUsername.TabIndex = 4;
            this.btnSearchUsername.Text = "&Search";
            this.btnSearchUsername.UseVisualStyleBackColor = true;
            this.btnSearchUsername.Click += new System.EventHandler(this.btnSearchUsername_Click);
            // 
            // GridOrderplan
            // 
            this.GridOrderplan.AllowUserToAddRows = false;
            this.GridOrderplan.AllowUserToDeleteRows = false;
            this.GridOrderplan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GridOrderplan.AutoGenerateColumns = false;
            this.GridOrderplan.BackgroundColor = System.Drawing.Color.White;
            this.GridOrderplan.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.GridOrderplan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridOrderplan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.h1,
            this.h2,
            this.h3,
            this.h4,
            this.mopidDataGridViewTextBoxColumn,
            this.mvtidDataGridViewTextBoxColumn,
            this.mopitemrowidDataGridViewTextBoxColumn,
            this.mopoditemcodeDataGridViewTextBoxColumn,
            this.mopoditemnameDataGridViewTextBoxColumn,
            this.mopcreatebyDataGridViewTextBoxColumn,
            this.mopcreatedateDataGridViewTextBoxColumn,
            this.mopupdatebyDataGridViewTextBoxColumn,
            this.mopupdatedateDataGridViewTextBoxColumn,
            this.msteventDataGridViewTextBoxColumn});
            this.GridOrderplan.DataSource = this.PackageItembindingSource1;
            this.GridOrderplan.GridColor = System.Drawing.Color.DimGray;
            this.GridOrderplan.Location = new System.Drawing.Point(12, 57);
            this.GridOrderplan.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GridOrderplan.Name = "GridOrderplan";
            this.GridOrderplan.ReadOnly = true;
            this.GridOrderplan.RowHeadersVisible = false;
            this.GridOrderplan.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.GridOrderplan.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.GridOrderplan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridOrderplan.Size = new System.Drawing.Size(658, 219);
            this.GridOrderplan.TabIndex = 7;
            this.GridOrderplan.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.GridOrderplan_DataBindingComplete);
            // 
            // h1
            // 
            this.h1.HeaderText = "No.";
            this.h1.Name = "h1";
            this.h1.ReadOnly = true;
            this.h1.Width = 30;
            // 
            // h2
            // 
            this.h2.DataPropertyName = "mop_item_row_id";
            this.h2.HeaderText = "Item row id";
            this.h2.Name = "h2";
            this.h2.ReadOnly = true;
            // 
            // h3
            // 
            this.h3.DataPropertyName = "mop_od_item_code";
            this.h3.HeaderText = "Item code";
            this.h3.Name = "h3";
            this.h3.ReadOnly = true;
            // 
            // h4
            // 
            this.h4.DataPropertyName = "mop_od_item_name";
            this.h4.HeaderText = "Item Name";
            this.h4.Name = "h4";
            this.h4.ReadOnly = true;
            this.h4.Width = 200;
            // 
            // mopidDataGridViewTextBoxColumn
            // 
            this.mopidDataGridViewTextBoxColumn.DataPropertyName = "mop_id";
            this.mopidDataGridViewTextBoxColumn.HeaderText = "mop_id";
            this.mopidDataGridViewTextBoxColumn.Name = "mopidDataGridViewTextBoxColumn";
            this.mopidDataGridViewTextBoxColumn.ReadOnly = true;
            this.mopidDataGridViewTextBoxColumn.Visible = false;
            // 
            // mvtidDataGridViewTextBoxColumn
            // 
            this.mvtidDataGridViewTextBoxColumn.DataPropertyName = "mvt_id";
            this.mvtidDataGridViewTextBoxColumn.HeaderText = "mvt_id";
            this.mvtidDataGridViewTextBoxColumn.Name = "mvtidDataGridViewTextBoxColumn";
            this.mvtidDataGridViewTextBoxColumn.ReadOnly = true;
            this.mvtidDataGridViewTextBoxColumn.Visible = false;
            // 
            // mopitemrowidDataGridViewTextBoxColumn
            // 
            this.mopitemrowidDataGridViewTextBoxColumn.DataPropertyName = "mop_item_row_id";
            this.mopitemrowidDataGridViewTextBoxColumn.HeaderText = "mop_item_row_id";
            this.mopitemrowidDataGridViewTextBoxColumn.Name = "mopitemrowidDataGridViewTextBoxColumn";
            this.mopitemrowidDataGridViewTextBoxColumn.ReadOnly = true;
            this.mopitemrowidDataGridViewTextBoxColumn.Visible = false;
            // 
            // mopoditemcodeDataGridViewTextBoxColumn
            // 
            this.mopoditemcodeDataGridViewTextBoxColumn.DataPropertyName = "mop_od_item_code";
            this.mopoditemcodeDataGridViewTextBoxColumn.HeaderText = "mop_od_item_code";
            this.mopoditemcodeDataGridViewTextBoxColumn.Name = "mopoditemcodeDataGridViewTextBoxColumn";
            this.mopoditemcodeDataGridViewTextBoxColumn.ReadOnly = true;
            this.mopoditemcodeDataGridViewTextBoxColumn.Visible = false;
            // 
            // mopoditemnameDataGridViewTextBoxColumn
            // 
            this.mopoditemnameDataGridViewTextBoxColumn.DataPropertyName = "mop_od_item_name";
            this.mopoditemnameDataGridViewTextBoxColumn.HeaderText = "mop_od_item_name";
            this.mopoditemnameDataGridViewTextBoxColumn.Name = "mopoditemnameDataGridViewTextBoxColumn";
            this.mopoditemnameDataGridViewTextBoxColumn.ReadOnly = true;
            this.mopoditemnameDataGridViewTextBoxColumn.Visible = false;
            // 
            // mopcreatebyDataGridViewTextBoxColumn
            // 
            this.mopcreatebyDataGridViewTextBoxColumn.DataPropertyName = "mop_create_by";
            this.mopcreatebyDataGridViewTextBoxColumn.HeaderText = "mop_create_by";
            this.mopcreatebyDataGridViewTextBoxColumn.Name = "mopcreatebyDataGridViewTextBoxColumn";
            this.mopcreatebyDataGridViewTextBoxColumn.ReadOnly = true;
            this.mopcreatebyDataGridViewTextBoxColumn.Visible = false;
            // 
            // mopcreatedateDataGridViewTextBoxColumn
            // 
            this.mopcreatedateDataGridViewTextBoxColumn.DataPropertyName = "mop_create_date";
            this.mopcreatedateDataGridViewTextBoxColumn.HeaderText = "mop_create_date";
            this.mopcreatedateDataGridViewTextBoxColumn.Name = "mopcreatedateDataGridViewTextBoxColumn";
            this.mopcreatedateDataGridViewTextBoxColumn.ReadOnly = true;
            this.mopcreatedateDataGridViewTextBoxColumn.Visible = false;
            // 
            // mopupdatebyDataGridViewTextBoxColumn
            // 
            this.mopupdatebyDataGridViewTextBoxColumn.DataPropertyName = "mop_update_by";
            this.mopupdatebyDataGridViewTextBoxColumn.HeaderText = "mop_update_by";
            this.mopupdatebyDataGridViewTextBoxColumn.Name = "mopupdatebyDataGridViewTextBoxColumn";
            this.mopupdatebyDataGridViewTextBoxColumn.ReadOnly = true;
            this.mopupdatebyDataGridViewTextBoxColumn.Visible = false;
            // 
            // mopupdatedateDataGridViewTextBoxColumn
            // 
            this.mopupdatedateDataGridViewTextBoxColumn.DataPropertyName = "mop_update_date";
            this.mopupdatedateDataGridViewTextBoxColumn.HeaderText = "mop_update_date";
            this.mopupdatedateDataGridViewTextBoxColumn.Name = "mopupdatedateDataGridViewTextBoxColumn";
            this.mopupdatedateDataGridViewTextBoxColumn.ReadOnly = true;
            this.mopupdatedateDataGridViewTextBoxColumn.Visible = false;
            // 
            // msteventDataGridViewTextBoxColumn
            // 
            this.msteventDataGridViewTextBoxColumn.DataPropertyName = "mst_event";
            this.msteventDataGridViewTextBoxColumn.HeaderText = "mst_event";
            this.msteventDataGridViewTextBoxColumn.Name = "msteventDataGridViewTextBoxColumn";
            this.msteventDataGridViewTextBoxColumn.ReadOnly = true;
            this.msteventDataGridViewTextBoxColumn.Visible = false;
            // 
            // PackageItembindingSource1
            // 
            this.PackageItembindingSource1.DataSource = typeof(DBCheckup.mst_order_plan);
            this.PackageItembindingSource1.CurrentChanged += new System.EventHandler(this.PackageItembindingSource1_CurrentChanged);
            // 
            // GBAddEdit
            // 
            this.GBAddEdit.Controls.Add(this.DD_EventName);
            this.GBAddEdit.Controls.Add(this.label11);
            this.GBAddEdit.Controls.Add(this.label10);
            this.GBAddEdit.Controls.Add(this.label9);
            this.GBAddEdit.Controls.Add(this.label3);
            this.GBAddEdit.Controls.Add(this.txtItemRowID);
            this.GBAddEdit.Controls.Add(this.txtItemName);
            this.GBAddEdit.Controls.Add(this.txtItemCode);
            this.GBAddEdit.Enabled = false;
            this.GBAddEdit.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.GBAddEdit.Location = new System.Drawing.Point(14, 301);
            this.GBAddEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GBAddEdit.Name = "GBAddEdit";
            this.GBAddEdit.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GBAddEdit.Size = new System.Drawing.Size(658, 152);
            this.GBAddEdit.TabIndex = 25;
            this.GBAddEdit.TabStop = false;
            this.GBAddEdit.Text = "Add Item";
            // 
            // DD_EventName
            // 
            this.DD_EventName.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.PackageItembindingSource1, "mvt_id", true));
            this.DD_EventName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DD_EventName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DD_EventName.FormattingEnabled = true;
            this.DD_EventName.Location = new System.Drawing.Point(425, 20);
            this.DD_EventName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DD_EventName.Name = "DD_EventName";
            this.DD_EventName.Size = new System.Drawing.Size(220, 24);
            this.DD_EventName.TabIndex = 34;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label11.Location = new System.Drawing.Point(9, 24);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(87, 16);
            this.label11.TabIndex = 32;
            this.label11.Text = "Item Row Id :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label10.Location = new System.Drawing.Point(335, 24);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 16);
            this.label10.TabIndex = 31;
            this.label10.Text = "Event Name :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label9.Location = new System.Drawing.Point(16, 75);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 16);
            this.label9.TabIndex = 27;
            this.label9.Text = "Item Name :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(20, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 16);
            this.label3.TabIndex = 17;
            this.label3.Text = "Item Code :";
            // 
            // txtItemRowID
            // 
            this.txtItemRowID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtItemRowID.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.PackageItembindingSource1, "mop_item_row_id", true));
            this.txtItemRowID.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtItemRowID.Location = new System.Drawing.Point(98, 21);
            this.txtItemRowID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtItemRowID.Name = "txtItemRowID";
            this.txtItemRowID.Size = new System.Drawing.Size(220, 23);
            this.txtItemRowID.TabIndex = 13;
            // 
            // txtItemName
            // 
            this.txtItemName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtItemName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.PackageItembindingSource1, "mop_od_item_name", true));
            this.txtItemName.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtItemName.Location = new System.Drawing.Point(98, 75);
            this.txtItemName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtItemName.Multiline = true;
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.Size = new System.Drawing.Size(220, 68);
            this.txtItemName.TabIndex = 11;
            // 
            // txtItemCode
            // 
            this.txtItemCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtItemCode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.PackageItembindingSource1, "mop_od_item_code", true));
            this.txtItemCode.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtItemCode.Location = new System.Drawing.Point(98, 48);
            this.txtItemCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.Size = new System.Drawing.Size(220, 23);
            this.txtItemCode.TabIndex = 8;
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.button5.Location = new System.Drawing.Point(560, 9);
            this.button5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(110, 40);
            this.button5.TabIndex = 6;
            this.button5.Text = "Import";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Visible = false;
            // 
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.button6.Location = new System.Drawing.Point(440, 9);
            this.button6.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(110, 40);
            this.button6.TabIndex = 26;
            this.button6.Text = "Export";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Visible = false;
            // 
            // lbmsgalert
            // 
            this.lbmsgalert.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbmsgalert.ForeColor = System.Drawing.Color.Red;
            this.lbmsgalert.Location = new System.Drawing.Point(12, 280);
            this.lbmsgalert.Name = "lbmsgalert";
            this.lbmsgalert.Size = new System.Drawing.Size(658, 20);
            this.lbmsgalert.TabIndex = 31;
            this.lbmsgalert.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(676, 129);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(87, 28);
            this.btnSave.TabIndex = 34;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnEditUser
            // 
            this.btnEditUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditUser.Location = new System.Drawing.Point(676, 93);
            this.btnEditUser.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnEditUser.Name = "btnEditUser";
            this.btnEditUser.Size = new System.Drawing.Size(87, 28);
            this.btnEditUser.TabIndex = 33;
            this.btnEditUser.Text = "Edit";
            this.btnEditUser.UseVisualStyleBackColor = true;
            this.btnEditUser.Click += new System.EventHandler(this.btnEditUser_Click);
            // 
            // btnAddNewUser
            // 
            this.btnAddNewUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNewUser.Location = new System.Drawing.Point(676, 57);
            this.btnAddNewUser.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAddNewUser.Name = "btnAddNewUser";
            this.btnAddNewUser.Size = new System.Drawing.Size(87, 28);
            this.btnAddNewUser.TabIndex = 32;
            this.btnAddNewUser.Text = "Add";
            this.btnAddNewUser.UseVisualStyleBackColor = true;
            this.btnAddNewUser.Click += new System.EventHandler(this.btnAddNewUser_Click);
            // 
            // btnClaear_cancel
            // 
            this.btnClaear_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClaear_cancel.Location = new System.Drawing.Point(676, 199);
            this.btnClaear_cancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClaear_cancel.Name = "btnClaear_cancel";
            this.btnClaear_cancel.Size = new System.Drawing.Size(87, 28);
            this.btnClaear_cancel.TabIndex = 35;
            this.btnClaear_cancel.Text = "Cancel";
            this.btnClaear_cancel.UseVisualStyleBackColor = true;
            this.btnClaear_cancel.Click += new System.EventHandler(this.btnClaear_cancel_Click);
            // 
            // btnDel
            // 
            this.btnDel.Enabled = false;
            this.btnDel.Location = new System.Drawing.Point(676, 164);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(87, 28);
            this.btnDel.TabIndex = 36;
            this.btnDel.Text = "Delete";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // frmMappingItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 462);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnEditUser);
            this.Controls.Add(this.btnAddNewUser);
            this.Controls.Add(this.btnClaear_cancel);
            this.Controls.Add(this.lbmsgalert);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.GBAddEdit);
            this.Controls.Add(this.GridOrderplan);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmMappingItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mapping Item";
            this.Load += new System.EventHandler(this.frmMappingItem_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridOrderplan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PackageItembindingSource1)).EndInit();
            this.GBAddEdit.ResumeLayout(false);
            this.GBAddEdit.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClearUserName;
        private System.Windows.Forms.Button btnSearchUsername;
        private System.Windows.Forms.DataGridView GridOrderplan;
        private System.Windows.Forms.GroupBox GBAddEdit;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtItemRowID;
        private System.Windows.Forms.TextBox txtItemName;
        private System.Windows.Forms.TextBox txtItemCode;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.ComboBox DD_EventName;
        private System.Windows.Forms.Label lbmsgalert;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnEditUser;
        private System.Windows.Forms.Button btnAddNewUser;
        private System.Windows.Forms.Button btnClaear_cancel;
        private System.Windows.Forms.BindingSource PackageItembindingSource1;
        private System.Windows.Forms.DataGridViewTextBoxColumn h1;
        private System.Windows.Forms.DataGridViewTextBoxColumn h2;
        private System.Windows.Forms.DataGridViewTextBoxColumn h3;
        private System.Windows.Forms.DataGridViewTextBoxColumn h4;
        private System.Windows.Forms.DataGridViewTextBoxColumn mopidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mvtidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mopitemrowidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mopoditemcodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mopoditemnameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mopcreatebyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mopcreatedateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mopupdatebyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mopupdatedateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn msteventDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button btnDel;
    }
}