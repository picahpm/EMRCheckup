namespace BKvs2010
{
    partial class frmReportChestXRay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReportChestXRay));
            this.txtConsult = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.favoriteTextBox1 = new BKvs2010.Usercontrols.FavoriteTextBox();
            this.chknotshowrpt = new System.Windows.Forms.CheckBox();
            this.pnsummary = new System.Windows.Forms.Panel();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.pb = new System.Windows.Forms.PictureBox();
            this.cmPast = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pastToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.listViewImgChest = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.lblMsg = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btndelimg = new System.Windows.Forms.Button();
            this.txtPacURL = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.linkLabelURL = new System.Windows.Forms.LinkLabel();
            this.groupBox1.SuspendLayout();
            this.pnsummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb)).BeginInit();
            this.cmPast.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtConsult
            // 
            this.txtConsult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtConsult.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtConsult.Location = new System.Drawing.Point(309, 52);
            this.txtConsult.Multiline = true;
            this.txtConsult.Name = "txtConsult";
            this.txtConsult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtConsult.Size = new System.Drawing.Size(384, 240);
            this.txtConsult.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Aquamarine;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(309, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(384, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "Result";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(157, 2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(70, 25);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Add";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(230, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(70, 25);
            this.button2.TabIndex = 4;
            this.button2.Text = "Delete";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(14, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Image";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Aquamarine;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(14, 295);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(543, 23);
            this.label3.TabIndex = 6;
            this.label3.Text = "Conclusion and Recommendation";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.favoriteTextBox1);
            this.groupBox1.Controls.Add(this.chknotshowrpt);
            this.groupBox1.Controls.Add(this.pnsummary);
            this.groupBox1.Location = new System.Drawing.Point(12, 305);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(681, 138);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // favoriteTextBox1
            // 
            this.favoriteTextBox1.AutoCompleteListThList = ((System.Collections.Generic.List<string>)(resources.GetObject("favoriteTextBox1.AutoCompleteListThList")));
            this.favoriteTextBox1.BackColor = System.Drawing.Color.Transparent;
            this.favoriteTextBox1.FavoriteOrder = "";
            this.favoriteTextBox1.FavoriteType = "CON";
            this.favoriteTextBox1.ListFabvorite = ((System.Collections.ObjectModel.ObservableCollection<string>)(resources.GetObject("favoriteTextBox1.ListFabvorite")));
            this.favoriteTextBox1.Location = new System.Drawing.Point(6, 48);
            this.favoriteTextBox1.Name = "favoriteTextBox1";
            this.favoriteTextBox1.Size = new System.Drawing.Size(669, 84);
            this.favoriteTextBox1.TabIndex = 14;
            this.favoriteTextBox1.BtnFavoriteClick += new BKvs2010.Usercontrols.FavoriteTextBox.OnBtnFavoriteClick(this.favoriteTextBox1_btnFavoriteClick);
            // 
            // chknotshowrpt
            // 
            this.chknotshowrpt.AutoSize = true;
            this.chknotshowrpt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chknotshowrpt.Location = new System.Drawing.Point(551, -3);
            this.chknotshowrpt.Name = "chknotshowrpt";
            this.chknotshowrpt.Size = new System.Drawing.Size(124, 20);
            this.chknotshowrpt.TabIndex = 11;
            this.chknotshowrpt.Tag = "";
            this.chknotshowrpt.Text = "Not Show Report";
            this.chknotshowrpt.UseVisualStyleBackColor = true;
            // 
            // pnsummary
            // 
            this.pnsummary.Controls.Add(this.radioButton1);
            this.pnsummary.Controls.Add(this.radioButton2);
            this.pnsummary.Location = new System.Drawing.Point(6, 17);
            this.pnsummary.Name = "pnsummary";
            this.pnsummary.Size = new System.Drawing.Size(250, 25);
            this.pnsummary.TabIndex = 13;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.radioButton1.Location = new System.Drawing.Point(3, 1);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(67, 20);
            this.radioButton1.TabIndex = 9;
            this.radioButton1.TabStop = true;
            this.radioButton1.Tag = "N";
            this.radioButton1.Text = "Normal";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.radioButton2.Location = new System.Drawing.Point(90, 2);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(81, 20);
            this.radioButton2.TabIndex = 10;
            this.radioButton2.TabStop = true;
            this.radioButton2.Tag = "A";
            this.radioButton2.Text = "Abnormal";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label5.Location = new System.Drawing.Point(14, 458);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "Delete Image";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(14, 652);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(70, 30);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Save";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pb
            // 
            this.pb.ContextMenuStrip = this.cmPast;
            this.pb.Location = new System.Drawing.Point(14, 29);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(286, 263);
            this.pb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb.TabIndex = 13;
            this.pb.TabStop = false;
            this.pb.DragDrop += new System.Windows.Forms.DragEventHandler(this.pb_DragDrop);
            this.pb.DragEnter += new System.Windows.Forms.DragEventHandler(this.pb_DragEnter);
            // 
            // cmPast
            // 
            this.cmPast.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pastToolStripMenuItem});
            this.cmPast.Name = "cmPast";
            this.cmPast.Size = new System.Drawing.Size(102, 26);
            this.cmPast.Text = "Past";
            // 
            // pastToolStripMenuItem
            // 
            this.pastToolStripMenuItem.Name = "pastToolStripMenuItem";
            this.pastToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.pastToolStripMenuItem.Text = "Paste";
            this.pastToolStripMenuItem.Click += new System.EventHandler(this.pastToolStripMenuItem_Click);
            // 
            // listViewImgChest
            // 
            this.listViewImgChest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listViewImgChest.CheckBoxes = true;
            this.listViewImgChest.Location = new System.Drawing.Point(14, 478);
            this.listViewImgChest.MultiSelect = false;
            this.listViewImgChest.Name = "listViewImgChest";
            this.listViewImgChest.ShowItemToolTips = true;
            this.listViewImgChest.Size = new System.Drawing.Size(679, 123);
            this.listViewImgChest.TabIndex = 14;
            this.listViewImgChest.UseCompatibleStateImageBehavior = false;
            this.listViewImgChest.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listViewImgChest_ItemCheck);
            this.listViewImgChest.SelectedIndexChanged += new System.EventHandler(this.listViewImgChest_SelectedIndexChanged);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // lblMsg
            // 
            this.lblMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblMsg.ForeColor = System.Drawing.Color.Red;
            this.lblMsg.Location = new System.Drawing.Point(169, 656);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(217, 23);
            this.lblMsg.TabIndex = 99;
            this.lblMsg.Text = "Save Complete";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblMsg.Visible = false;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(90, 652);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(73, 30);
            this.button1.TabIndex = 98;
            this.button1.Text = "Cancel";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btndelimg
            // 
            this.btndelimg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btndelimg.Image = ((System.Drawing.Image)(resources.GetObject("btndelimg.Image")));
            this.btndelimg.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btndelimg.Location = new System.Drawing.Point(593, 449);
            this.btndelimg.Name = "btndelimg";
            this.btndelimg.Size = new System.Drawing.Size(100, 25);
            this.btndelimg.TabIndex = 100;
            this.btndelimg.Text = "Delete Image";
            this.btndelimg.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btndelimg.UseVisualStyleBackColor = true;
            this.btndelimg.Click += new System.EventHandler(this.btndelimg_Click);
            // 
            // txtPacURL
            // 
            this.txtPacURL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPacURL.Location = new System.Drawing.Point(14, 622);
            this.txtPacURL.Multiline = true;
            this.txtPacURL.Name = "txtPacURL";
            this.txtPacURL.Size = new System.Drawing.Size(679, 24);
            this.txtPacURL.TabIndex = 101;
            this.txtPacURL.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TextBox_MouseClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label6.Location = new System.Drawing.Point(14, 604);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 16);
            this.label6.TabIndex = 10;
            this.label6.Text = "PACs URL :";
            // 
            // linkLabelURL
            // 
            this.linkLabelURL.AutoSize = true;
            this.linkLabelURL.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.linkLabelURL.LinkColor = System.Drawing.Color.Black;
            this.linkLabelURL.Location = new System.Drawing.Point(578, 6);
            this.linkLabelURL.Name = "linkLabelURL";
            this.linkLabelURL.Size = new System.Drawing.Size(109, 16);
            this.linkLabelURL.TabIndex = 102;
            this.linkLabelURL.TabStop = true;
            this.linkLabelURL.Text = "View PACs Image";
            this.linkLabelURL.VisitedLinkColor = System.Drawing.Color.Black;
            this.linkLabelURL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelURL_LinkClicked);
            // 
            // frmReportChestXRay
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(700, 650);
            this.ClientSize = new System.Drawing.Size(705, 691);
            this.Controls.Add(this.linkLabelURL);
            this.Controls.Add(this.txtPacURL);
            this.Controls.Add(this.btndelimg);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listViewImgChest);
            this.Controls.Add(this.pb);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtConsult);
            this.Name = "frmReportChestXRay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ระบบ EMR Check-up Pathway 2012 V.1 [Chest X-ray]";
            this.Load += new System.EventHandler(this.frmReportChestXRay_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnsummary.ResumeLayout(false);
            this.pnsummary.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb)).EndInit();
            this.cmPast.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtConsult;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chknotshowrpt;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.PictureBox pb;
        private System.Windows.Forms.Panel pnsummary;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.ListView listViewImgChest;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btndelimg;
        private System.Windows.Forms.TextBox txtPacURL;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.LinkLabel linkLabelURL;
        private System.Windows.Forms.ContextMenuStrip cmPast;
        private System.Windows.Forms.ToolStripMenuItem pastToolStripMenuItem;
        private Usercontrols.FavoriteTextBox favoriteTextBox1;
    }
}