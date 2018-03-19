namespace BKvs2010.UserControlEMR
{
    partial class AdditionalItemUC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdditionalItemUC));
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkCompanyLoad = new System.Windows.Forms.CheckBox();
            this.PackageAC = new UserControlLibrary.TextBoxAutoComplete();
            this.bsPatientBookHdr = new System.Windows.Forms.BindingSource(this.components);
            this.bsPatientRegis = new System.Windows.Forms.BindingSource(this.components);
            this.label15 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.GridPackage = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColPackageName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColPackageID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAdditionItem = new System.Windows.Forms.Button();
            this.txtAdditionItem = new System.Windows.Forms.TextBox();
            this.gvAdditionItem = new System.Windows.Forms.DataGridView();
            this.tpai_add_item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDel = new System.Windows.Forms.DataGridViewButtonColumn();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.chkItem6 = new System.Windows.Forms.CheckBox();
            this.chkItem11 = new System.Windows.Forms.CheckBox();
            this.chkItem5 = new System.Windows.Forms.CheckBox();
            this.chkItem10 = new System.Windows.Forms.CheckBox();
            this.chkItem4 = new System.Windows.Forms.CheckBox();
            this.chkItem9 = new System.Windows.Forms.CheckBox();
            this.chkItem3 = new System.Windows.Forms.CheckBox();
            this.chkItem8 = new System.Windows.Forms.CheckBox();
            this.chkItem7 = new System.Windows.Forms.CheckBox();
            this.chkItem2 = new System.Windows.Forms.CheckBox();
            this.chkItem1 = new System.Windows.Forms.CheckBox();
            this.bsPatientAddItem = new System.Windows.Forms.BindingSource(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsPatientBookHdr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPatientRegis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridPackage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAdditionItem)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsPatientAddItem)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkCompanyLoad);
            this.panel1.Controls.Add(this.PackageAC);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.label43);
            this.panel1.Controls.Add(this.GridPackage);
            this.panel1.Controls.Add(this.btnAdditionItem);
            this.panel1.Controls.Add(this.txtAdditionItem);
            this.panel1.Controls.Add(this.gvAdditionItem);
            this.panel1.Controls.Add(this.groupBox5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(365, 663);
            this.panel1.TabIndex = 0;
            // 
            // chkCompanyLoad
            // 
            this.chkCompanyLoad.AutoSize = true;
            this.chkCompanyLoad.Location = new System.Drawing.Point(161, 33);
            this.chkCompanyLoad.Name = "chkCompanyLoad";
            this.chkCompanyLoad.Size = new System.Drawing.Size(70, 17);
            this.chkCompanyLoad.TabIndex = 258;
            this.chkCompanyLoad.Tag = "1";
            this.chkCompanyLoad.Text = "Company";
            this.chkCompanyLoad.UseVisualStyleBackColor = true;
            this.chkCompanyLoad.CheckedChanged += new System.EventHandler(this.chkCompanyLoad_CheckedChanged);
            // 
            // PackageAC
            // 
            this.PackageAC.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PackageAC.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsPatientBookHdr, "tpbh_package_name", true));
            this.PackageAC.DataSource = null;
            this.PackageAC.DisplayMember = "";
            this.PackageAC.DropDownWidth = 0;
            this.PackageAC.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.PackageAC.FreeText = true;
            this.PackageAC.Location = new System.Drawing.Point(161, 4);
            this.PackageAC.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.PackageAC.MinTextSearch = 2;
            this.PackageAC.Multiline = false;
            this.PackageAC.Name = "PackageAC";
            this.PackageAC.ReadOnly = false;
            this.PackageAC.SelectedItem = null;
            this.PackageAC.SelectedValue = null;
            this.PackageAC.Size = new System.Drawing.Size(199, 23);
            this.PackageAC.TabIndex = 257;
            this.PackageAC.ValueMember = "";
            // 
            // bsPatientBookHdr
            // 
            this.bsPatientBookHdr.DataMember = "trn_patient_book_hdrs";
            this.bsPatientBookHdr.DataSource = this.bsPatientRegis;
            // 
            // bsPatientRegis
            // 
            this.bsPatientRegis.DataSource = typeof(DBCheckup.trn_patient_regi);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label15.Location = new System.Drawing.Point(5, 33);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(55, 16);
            this.label15.TabIndex = 256;
            this.label15.Text = "Package";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label43.Location = new System.Drawing.Point(5, 6);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(150, 16);
            this.label43.TabIndex = 250;
            this.label43.Text = "Health CheckUp Program";
            // 
            // GridPackage
            // 
            this.GridPackage.AllowUserToAddRows = false;
            this.GridPackage.AllowUserToDeleteRows = false;
            this.GridPackage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GridPackage.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.GridPackage.BackgroundColor = System.Drawing.Color.White;
            this.GridPackage.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.GridPackage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.GridPackage.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.ColPackageName,
            this.ColPackageID});
            this.GridPackage.GridColor = System.Drawing.Color.White;
            this.GridPackage.Location = new System.Drawing.Point(5, 51);
            this.GridPackage.Name = "GridPackage";
            this.GridPackage.ReadOnly = true;
            this.GridPackage.RowHeadersVisible = false;
            this.GridPackage.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.GridPackage.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.GridPackage.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridPackage.Size = new System.Drawing.Size(355, 100);
            this.GridPackage.TabIndex = 255;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "check";
            this.Column1.HeaderText = "Select";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 43;
            // 
            // ColPackageName
            // 
            this.ColPackageName.DataPropertyName = "PackageName";
            this.ColPackageName.HeaderText = "Package";
            this.ColPackageName.Name = "ColPackageName";
            this.ColPackageName.ReadOnly = true;
            this.ColPackageName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColPackageName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColPackageName.Width = 56;
            // 
            // ColPackageID
            // 
            this.ColPackageID.DataPropertyName = "PackageID";
            this.ColPackageID.HeaderText = "PackageID";
            this.ColPackageID.Name = "ColPackageID";
            this.ColPackageID.ReadOnly = true;
            this.ColPackageID.Visible = false;
            this.ColPackageID.Width = 86;
            // 
            // btnAdditionItem
            // 
            this.btnAdditionItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdditionItem.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnAdditionItem.Image = ((System.Drawing.Image)(resources.GetObject("btnAdditionItem.Image")));
            this.btnAdditionItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdditionItem.Location = new System.Drawing.Point(297, 329);
            this.btnAdditionItem.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAdditionItem.Name = "btnAdditionItem";
            this.btnAdditionItem.Size = new System.Drawing.Size(63, 25);
            this.btnAdditionItem.TabIndex = 249;
            this.btnAdditionItem.Text = "      Add";
            this.btnAdditionItem.UseVisualStyleBackColor = true;
            this.btnAdditionItem.Click += new System.EventHandler(this.btnAdditionItem_Click);
            // 
            // txtAdditionItem
            // 
            this.txtAdditionItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAdditionItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAdditionItem.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtAdditionItem.Location = new System.Drawing.Point(5, 331);
            this.txtAdditionItem.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtAdditionItem.Name = "txtAdditionItem";
            this.txtAdditionItem.Size = new System.Drawing.Size(280, 23);
            this.txtAdditionItem.TabIndex = 251;
            // 
            // gvAdditionItem
            // 
            this.gvAdditionItem.AllowUserToAddRows = false;
            this.gvAdditionItem.AllowUserToDeleteRows = false;
            this.gvAdditionItem.AllowUserToResizeColumns = false;
            this.gvAdditionItem.AllowUserToResizeRows = false;
            this.gvAdditionItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvAdditionItem.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gvAdditionItem.BackgroundColor = System.Drawing.Color.White;
            this.gvAdditionItem.ColumnHeadersHeight = 20;
            this.gvAdditionItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gvAdditionItem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tpai_add_item,
            this.colDel});
            this.gvAdditionItem.GridColor = System.Drawing.Color.White;
            this.gvAdditionItem.Location = new System.Drawing.Point(5, 358);
            this.gvAdditionItem.MultiSelect = false;
            this.gvAdditionItem.Name = "gvAdditionItem";
            this.gvAdditionItem.ReadOnly = true;
            this.gvAdditionItem.RowHeadersVisible = false;
            this.gvAdditionItem.RowHeadersWidth = 40;
            this.gvAdditionItem.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.gvAdditionItem.RowTemplate.ReadOnly = true;
            this.gvAdditionItem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvAdditionItem.Size = new System.Drawing.Size(354, 300);
            this.gvAdditionItem.TabIndex = 253;
            this.gvAdditionItem.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvAdditionItem_CellContentClick);
            // 
            // tpai_add_item
            // 
            this.tpai_add_item.DataPropertyName = "tpai_add_item";
            this.tpai_add_item.HeaderText = "Addition Item";
            this.tpai_add_item.Name = "tpai_add_item";
            this.tpai_add_item.ReadOnly = true;
            this.tpai_add_item.Width = 93;
            // 
            // colDel
            // 
            this.colDel.HeaderText = "";
            this.colDel.Name = "colDel";
            this.colDel.ReadOnly = true;
            this.colDel.Text = "Del.";
            this.colDel.UseColumnTextForButtonValue = true;
            this.colDel.Width = 5;
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.chkItem6);
            this.groupBox5.Controls.Add(this.chkItem11);
            this.groupBox5.Controls.Add(this.chkItem5);
            this.groupBox5.Controls.Add(this.chkItem10);
            this.groupBox5.Controls.Add(this.chkItem4);
            this.groupBox5.Controls.Add(this.chkItem9);
            this.groupBox5.Controls.Add(this.chkItem3);
            this.groupBox5.Controls.Add(this.chkItem8);
            this.groupBox5.Controls.Add(this.chkItem7);
            this.groupBox5.Controls.Add(this.chkItem2);
            this.groupBox5.Controls.Add(this.chkItem1);
            this.groupBox5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.groupBox5.Location = new System.Drawing.Point(5, 161);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(355, 165);
            this.groupBox5.TabIndex = 252;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = " Addition Item";
            // 
            // chkItem6
            // 
            this.chkItem6.AutoSize = true;
            this.chkItem6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkItem6.Location = new System.Drawing.Point(6, 137);
            this.chkItem6.Name = "chkItem6";
            this.chkItem6.Size = new System.Drawing.Size(65, 20);
            this.chkItem6.TabIndex = 0;
            this.chkItem6.Tag = "6";
            this.chkItem6.Text = "HbA1C";
            this.chkItem6.UseVisualStyleBackColor = true;
            this.chkItem6.CheckedChanged += new System.EventHandler(this.chkItem_CheckedChanged);
            // 
            // chkItem11
            // 
            this.chkItem11.AutoSize = true;
            this.chkItem11.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkItem11.Location = new System.Drawing.Point(219, 137);
            this.chkItem11.Name = "chkItem11";
            this.chkItem11.Size = new System.Drawing.Size(100, 20);
            this.chkItem11.TabIndex = 0;
            this.chkItem11.Tag = "11";
            this.chkItem11.Text = "Anti HAV IgG";
            this.chkItem11.UseVisualStyleBackColor = true;
            this.chkItem11.CheckedChanged += new System.EventHandler(this.chkItem_CheckedChanged);
            // 
            // chkItem5
            // 
            this.chkItem5.AutoSize = true;
            this.chkItem5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkItem5.Location = new System.Drawing.Point(6, 114);
            this.chkItem5.Name = "chkItem5";
            this.chkItem5.Size = new System.Drawing.Size(50, 20);
            this.chkItem5.TabIndex = 0;
            this.chkItem5.Tag = "5";
            this.chkItem5.Text = "PSA";
            this.chkItem5.UseVisualStyleBackColor = true;
            this.chkItem5.CheckedChanged += new System.EventHandler(this.chkItem_CheckedChanged);
            // 
            // chkItem10
            // 
            this.chkItem10.AutoSize = true;
            this.chkItem10.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkItem10.Location = new System.Drawing.Point(219, 114);
            this.chkItem10.Name = "chkItem10";
            this.chkItem10.Size = new System.Drawing.Size(77, 20);
            this.chkItem10.TabIndex = 0;
            this.chkItem10.Tag = "10";
            this.chkItem10.Text = "Anti HCV";
            this.chkItem10.UseVisualStyleBackColor = true;
            this.chkItem10.CheckedChanged += new System.EventHandler(this.chkItem_CheckedChanged);
            // 
            // chkItem4
            // 
            this.chkItem4.AutoSize = true;
            this.chkItem4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkItem4.Location = new System.Drawing.Point(6, 91);
            this.chkItem4.Name = "chkItem4";
            this.chkItem4.Size = new System.Drawing.Size(49, 20);
            this.chkItem4.TabIndex = 0;
            this.chkItem4.Tag = "4";
            this.chkItem4.Text = "AFP";
            this.chkItem4.UseVisualStyleBackColor = true;
            this.chkItem4.CheckedChanged += new System.EventHandler(this.chkItem_CheckedChanged);
            // 
            // chkItem9
            // 
            this.chkItem9.AutoSize = true;
            this.chkItem9.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkItem9.Location = new System.Drawing.Point(219, 91);
            this.chkItem9.Name = "chkItem9";
            this.chkItem9.Size = new System.Drawing.Size(74, 20);
            this.chkItem9.TabIndex = 0;
            this.chkItem9.Tag = "9";
            this.chkItem9.Text = "Anti Hbs";
            this.chkItem9.UseVisualStyleBackColor = true;
            this.chkItem9.CheckedChanged += new System.EventHandler(this.chkItem_CheckedChanged);
            // 
            // chkItem3
            // 
            this.chkItem3.AutoSize = true;
            this.chkItem3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkItem3.Location = new System.Drawing.Point(6, 68);
            this.chkItem3.Name = "chkItem3";
            this.chkItem3.Size = new System.Drawing.Size(50, 20);
            this.chkItem3.TabIndex = 0;
            this.chkItem3.Tag = "3";
            this.chkItem3.Text = "CEA";
            this.chkItem3.UseVisualStyleBackColor = true;
            this.chkItem3.CheckedChanged += new System.EventHandler(this.chkItem_CheckedChanged);
            // 
            // chkItem8
            // 
            this.chkItem8.AutoSize = true;
            this.chkItem8.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkItem8.Location = new System.Drawing.Point(219, 68);
            this.chkItem8.Name = "chkItem8";
            this.chkItem8.Size = new System.Drawing.Size(128, 20);
            this.chkItem8.TabIndex = 0;
            this.chkItem8.Tag = "8";
            this.chkItem8.Text = "Ultrasound Whole";
            this.chkItem8.UseVisualStyleBackColor = true;
            this.chkItem8.CheckedChanged += new System.EventHandler(this.chkItem_CheckedChanged);
            // 
            // chkItem7
            // 
            this.chkItem7.AutoSize = true;
            this.chkItem7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkItem7.Location = new System.Drawing.Point(219, 45);
            this.chkItem7.Name = "chkItem7";
            this.chkItem7.Size = new System.Drawing.Size(46, 20);
            this.chkItem7.TabIndex = 0;
            this.chkItem7.Tag = "7";
            this.chkItem7.Text = "ABI";
            this.chkItem7.UseVisualStyleBackColor = true;
            this.chkItem7.CheckedChanged += new System.EventHandler(this.chkItem_CheckedChanged);
            // 
            // chkItem2
            // 
            this.chkItem2.AutoSize = true;
            this.chkItem2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkItem2.Location = new System.Drawing.Point(6, 45);
            this.chkItem2.Name = "chkItem2";
            this.chkItem2.Size = new System.Drawing.Size(127, 20);
            this.chkItem2.TabIndex = 0;
            this.chkItem2.Tag = "2";
            this.chkItem2.Text = "Ultrasound Lower";
            this.chkItem2.UseVisualStyleBackColor = true;
            this.chkItem2.CheckedChanged += new System.EventHandler(this.chkItem_CheckedChanged);
            // 
            // chkItem1
            // 
            this.chkItem1.AutoSize = true;
            this.chkItem1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkItem1.Location = new System.Drawing.Point(6, 22);
            this.chkItem1.Name = "chkItem1";
            this.chkItem1.Size = new System.Drawing.Size(236, 20);
            this.chkItem1.TabIndex = 0;
            this.chkItem1.Tag = "1";
            this.chkItem1.Text = "Mammogram with Ultrasound Breast";
            this.chkItem1.UseVisualStyleBackColor = true;
            this.chkItem1.CheckedChanged += new System.EventHandler(this.chkItem_CheckedChanged);
            // 
            // bsPatientAddItem
            // 
            this.bsPatientAddItem.DataMember = "trn_patient_add_items";
            this.bsPatientAddItem.DataSource = this.bsPatientRegis;
            // 
            // AdditionalItemUC
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.panel1);
            this.Name = "AdditionalItemUC";
            this.Size = new System.Drawing.Size(365, 663);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsPatientBookHdr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPatientRegis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridPackage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAdditionItem)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsPatientAddItem)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.DataGridView GridPackage;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColPackageName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColPackageID;
        private System.Windows.Forms.Button btnAdditionItem;
        private System.Windows.Forms.TextBox txtAdditionItem;
        private System.Windows.Forms.DataGridView gvAdditionItem;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox chkItem6;
        private System.Windows.Forms.CheckBox chkItem11;
        private System.Windows.Forms.CheckBox chkItem5;
        private System.Windows.Forms.CheckBox chkItem10;
        private System.Windows.Forms.CheckBox chkItem4;
        private System.Windows.Forms.CheckBox chkItem9;
        private System.Windows.Forms.CheckBox chkItem3;
        private System.Windows.Forms.CheckBox chkItem8;
        private System.Windows.Forms.CheckBox chkItem7;
        private System.Windows.Forms.CheckBox chkItem2;
        private System.Windows.Forms.CheckBox chkItem1;
        private System.Windows.Forms.BindingSource bsPatientRegis;
        private System.Windows.Forms.BindingSource bsPatientAddItem;
        private System.Windows.Forms.BindingSource bsPatientBookHdr;
        private System.Windows.Forms.DataGridViewTextBoxColumn tpai_add_item;
        private System.Windows.Forms.DataGridViewButtonColumn colDel;
        private UserControlLibrary.TextBoxAutoComplete PackageAC;
        private System.Windows.Forms.CheckBox chkCompanyLoad;

    }
}
