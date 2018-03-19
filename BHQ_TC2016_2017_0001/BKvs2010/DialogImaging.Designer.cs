namespace BKvs2010
{
    partial class DialogImaging
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogImaging));
            this.panel1 = new System.Windows.Forms.Panel();
            this.ChkSummaryResult = new UserControlLibrary.CheckBoxBinding();
            this.DoctorXrayBS = new System.Windows.Forms.BindingSource(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.txtPacURL = new System.Windows.Forms.TextBox();
            this.listViewImgChest = new System.Windows.Forms.ListView();
            this.btndelimg = new System.Windows.Forms.Button();
            this.chknotshowrpt = new UserControlLibrary.CheckBoxBinding();
            this.label3 = new System.Windows.Forms.Label();
            this.panelRadioBinding1 = new UserControlLibrary.PanelRadioBinding();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.favoriteTextBox1 = new BKvs2010.Usercontrols.FavoriteTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtConsult = new System.Windows.Forms.TextBox();
            this.linkPac = new System.Windows.Forms.LinkLabel();
            this.pb = new System.Windows.Forms.PictureBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblMsg = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.DoctorXrayImage = new System.Windows.Forms.BindingSource(this.components);
            this.imageListSmall = new System.Windows.Forms.ImageList(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DoctorXrayBS)).BeginInit();
            this.panelRadioBinding1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DoctorXrayImage)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoScrollMinSize = new System.Drawing.Size(885, 652);
            this.panel1.Controls.Add(this.ChkSummaryResult);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtPacURL);
            this.panel1.Controls.Add(this.listViewImgChest);
            this.panel1.Controls.Add(this.btndelimg);
            this.panel1.Controls.Add(this.chknotshowrpt);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.panelRadioBinding1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtConsult);
            this.panel1.Controls.Add(this.linkPac);
            this.panel1.Controls.Add(this.pb);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(885, 652);
            this.panel1.TabIndex = 0;
            // 
            // ChkSummaryResult
            // 
            this.ChkSummaryResult.AutoSize = true;
            this.ChkSummaryResult.BindingType = UserControlLibrary.CheckBoxBinding.TypeData.Boolean;
            this.ChkSummaryResult.BindingValue = null;
            this.ChkSummaryResult.CheckedFalseValue = "false";
            this.ChkSummaryResult.CheckedTrueValue = "true";
            this.ChkSummaryResult.DataBindings.Add(new System.Windows.Forms.Binding("BindingValue", this.DoctorXrayBS, "trxr_is_abdomen", true));
            this.ChkSummaryResult.Location = new System.Drawing.Point(327, 6);
            this.ChkSummaryResult.Name = "ChkSummaryResult";
            this.ChkSummaryResult.Size = new System.Drawing.Size(117, 19);
            this.ChkSummaryResult.TabIndex = 106;
            this.ChkSummaryResult.Text = "Abdomen Result";
            this.ChkSummaryResult.UseVisualStyleBackColor = true;
            // 
            // DoctorXrayBS
            // 
            this.DoctorXrayBS.DataSource = typeof(DBCheckup.trn_doctor_xray);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Aquamarine;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label5.Location = new System.Drawing.Point(15, 626);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 23);
            this.label5.TabIndex = 105;
            this.label5.Text = "PACs URL:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPacURL
            // 
            this.txtPacURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPacURL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPacURL.Location = new System.Drawing.Point(90, 626);
            this.txtPacURL.Multiline = true;
            this.txtPacURL.Name = "txtPacURL";
            this.txtPacURL.ReadOnly = true;
            this.txtPacURL.Size = new System.Drawing.Size(780, 24);
            this.txtPacURL.TabIndex = 104;
            // 
            // listViewImgChest
            // 
            this.listViewImgChest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewImgChest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listViewImgChest.CheckBoxes = true;
            this.listViewImgChest.Location = new System.Drawing.Point(15, 496);
            this.listViewImgChest.MultiSelect = false;
            this.listViewImgChest.Name = "listViewImgChest";
            this.listViewImgChest.ShowItemToolTips = true;
            this.listViewImgChest.Size = new System.Drawing.Size(855, 126);
            this.listViewImgChest.TabIndex = 103;
            this.listViewImgChest.UseCompatibleStateImageBehavior = false;
            // 
            // btndelimg
            // 
            this.btndelimg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btndelimg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btndelimg.Image = ((System.Drawing.Image)(resources.GetObject("btndelimg.Image")));
            this.btndelimg.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btndelimg.Location = new System.Drawing.Point(770, 469);
            this.btndelimg.Name = "btndelimg";
            this.btndelimg.Size = new System.Drawing.Size(100, 25);
            this.btndelimg.TabIndex = 101;
            this.btndelimg.Text = "Delete Image";
            this.btndelimg.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btndelimg.UseVisualStyleBackColor = true;
            this.btndelimg.Visible = false;
            this.btndelimg.Click += new System.EventHandler(this.btndelimg_Click);
            // 
            // chknotshowrpt
            // 
            this.chknotshowrpt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chknotshowrpt.AutoSize = true;
            this.chknotshowrpt.BindingType = UserControlLibrary.CheckBoxBinding.TypeData.Char;
            this.chknotshowrpt.BindingValue = null;
            this.chknotshowrpt.CheckedFalseValue = "N";
            this.chknotshowrpt.CheckedTrueValue = "Y";
            this.chknotshowrpt.DataBindings.Add(new System.Windows.Forms.Binding("BindingValue", this.DoctorXrayBS, "trxr_not_show_rpt", true));
            this.chknotshowrpt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chknotshowrpt.Location = new System.Drawing.Point(746, 341);
            this.chknotshowrpt.Name = "chknotshowrpt";
            this.chknotshowrpt.Size = new System.Drawing.Size(119, 19);
            this.chknotshowrpt.TabIndex = 22;
            this.chknotshowrpt.Tag = "";
            this.chknotshowrpt.Text = "Not Show Report";
            this.chknotshowrpt.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.Color.Aquamarine;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(15, 339);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(722, 23);
            this.label3.TabIndex = 21;
            this.label3.Text = "Conclusion and Recommendation";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelRadioBinding1
            // 
            this.panelRadioBinding1.AbleNull = true;
            this.panelRadioBinding1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelRadioBinding1.BorderRadius = true;
            this.panelRadioBinding1.Controls.Add(this.radioButton1);
            this.panelRadioBinding1.Controls.Add(this.radioButton2);
            this.panelRadioBinding1.Controls.Add(this.favoriteTextBox1);
            this.panelRadioBinding1.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.DoctorXrayBS, "trxr_summary", true));
            this.panelRadioBinding1.Location = new System.Drawing.Point(15, 366);
            this.panelRadioBinding1.Name = "panelRadioBinding1";
            this.panelRadioBinding1.ReadOnly = false;
            this.panelRadioBinding1.Size = new System.Drawing.Size(855, 99);
            this.panelRadioBinding1.TabIndex = 20;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoCheck = false;
            this.radioButton1.AutoSize = true;
            this.radioButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.radioButton1.Location = new System.Drawing.Point(6, 4);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(66, 19);
            this.radioButton1.TabIndex = 11;
            this.radioButton1.TabStop = true;
            this.radioButton1.Tag = "N";
            this.radioButton1.Text = "Normal";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoCheck = false;
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.radioButton2.Location = new System.Drawing.Point(93, 4);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(78, 19);
            this.radioButton2.TabIndex = 12;
            this.radioButton2.TabStop = true;
            this.radioButton2.Tag = "A";
            this.radioButton2.Text = "Abnormal";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.Visible = false;
            // 
            // favoriteTextBox1
            // 
            this.favoriteTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.favoriteTextBox1.AutoCompleteListThList = null;
            this.favoriteTextBox1.BackColor = System.Drawing.Color.Transparent;
            this.favoriteTextBox1.ButtonPosition = BKvs2010.Usercontrols.PrototypeFavoriteTextBox.PrototypeFavoriteTextBoxButtonPosition.TopRight;
            this.favoriteTextBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.DoctorXrayBS, "trxr_doc_result_thai", true));
            this.favoriteTextBox1.FavoriteOrder = null;
            this.favoriteTextBox1.FavoriteType = "CON";
            this.favoriteTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.favoriteTextBox1.ListFabvorite = ((System.Collections.ObjectModel.ObservableCollection<string>)(resources.GetObject("favoriteTextBox1.ListFabvorite")));
            this.favoriteTextBox1.Location = new System.Drawing.Point(6, 28);
            this.favoriteTextBox1.Name = "favoriteTextBox1";
            this.favoriteTextBox1.Size = new System.Drawing.Size(845, 65);
            this.favoriteTextBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.Aquamarine;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(326, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(544, 23);
            this.label1.TabIndex = 19;
            this.label1.Text = "Result";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtConsult
            // 
            this.txtConsult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConsult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtConsult.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.DoctorXrayBS, "trxr_result", true));
            this.txtConsult.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtConsult.Location = new System.Drawing.Point(326, 53);
            this.txtConsult.Multiline = true;
            this.txtConsult.Name = "txtConsult";
            this.txtConsult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtConsult.Size = new System.Drawing.Size(544, 282);
            this.txtConsult.TabIndex = 18;
            // 
            // linkPac
            // 
            this.linkPac.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkPac.AutoSize = true;
            this.linkPac.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.linkPac.Location = new System.Drawing.Point(767, 8);
            this.linkPac.Name = "linkPac";
            this.linkPac.Size = new System.Drawing.Size(103, 15);
            this.linkPac.TabIndex = 17;
            this.linkPac.TabStop = true;
            this.linkPac.Text = "View PACs Image";
            this.linkPac.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkPac_LinkClicked);
            // 
            // pb
            // 
            this.pb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pb.Location = new System.Drawing.Point(15, 30);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(305, 305);
            this.pb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb.TabIndex = 16;
            this.pb.TabStop = false;
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(254, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(67, 25);
            this.btnDelete.TabIndex = 15;
            this.btnDelete.Text = "Delete";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(180, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(67, 25);
            this.btnAdd.TabIndex = 14;
            this.btnAdd.Text = "Add";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Visible = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(12, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Image";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.BackColor = System.Drawing.Color.Aquamarine;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.Location = new System.Drawing.Point(15, 469);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(855, 23);
            this.label4.TabIndex = 102;
            this.label4.Text = "Images List";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMsg
            // 
            this.lblMsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblMsg.ForeColor = System.Drawing.Color.Red;
            this.lblMsg.Location = new System.Drawing.Point(170, 9);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(700, 23);
            this.lblMsg.TabIndex = 108;
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(88, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(73, 30);
            this.btnCancel.TabIndex = 107;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(12, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(70, 30);
            this.btnSave.TabIndex = 106;
            this.btnSave.Text = "Save";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // DoctorXrayImage
            // 
            this.DoctorXrayImage.DataMember = "trn_doctor_xray_images";
            this.DoctorXrayImage.DataSource = this.DoctorXrayBS;
            // 
            // imageListSmall
            // 
            this.imageListSmall.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageListSmall.ImageSize = new System.Drawing.Size(16, 16);
            this.imageListSmall.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(885, 692);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblMsg);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 652);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(885, 40);
            this.panel2.TabIndex = 1;
            // 
            // DialogImaging
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(885, 692);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "DialogImaging";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DialogImaging";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DoctorXrayBS)).EndInit();
            this.panelRadioBinding1.ResumeLayout(false);
            this.panelRadioBinding1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DoctorXrayImage)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkPac;
        private System.Windows.Forms.PictureBox pb;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private UserControlLibrary.PanelRadioBinding panelRadioBinding1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtConsult;
        private System.Windows.Forms.Label label3;
        private UserControlLibrary.CheckBoxBinding chknotshowrpt;
        private Usercontrols.FavoriteTextBox favoriteTextBox1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView listViewImgChest;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPacURL;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.BindingSource DoctorXrayBS;
        private System.Windows.Forms.BindingSource DoctorXrayImage;
        private System.Windows.Forms.ImageList imageListSmall;
        private System.Windows.Forms.Button btndelimg;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private UserControlLibrary.CheckBoxBinding ChkSummaryResult;
    }
}