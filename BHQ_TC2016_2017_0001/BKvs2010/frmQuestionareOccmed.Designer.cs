namespace BKvs2010
{
    partial class frmQuestionareOccmed
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQuestionareOccmed));
            this.panel1 = new System.Windows.Forms.Panel();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.panel12 = new System.Windows.Forms.Panel();
            this.btnSendDoc = new System.Windows.Forms.Button();
            this.radEng = new System.Windows.Forms.RadioButton();
            this.btnPrintPreview = new System.Windows.Forms.Button();
            this.radThai = new System.Windows.Forms.RadioButton();
            this.lblMsg = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.trnquespatientBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.trnNewquespatientBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel1.SuspendLayout();
            this.panel12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trnquespatientBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trnNewquespatientBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.webBrowser1);
            this.panel1.Controls.Add(this.panel12);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1134, 742);
            this.panel1.TabIndex = 6;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 37);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(1134, 669);
            this.webBrowser1.TabIndex = 5;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // panel12
            // 
            this.panel12.BackColor = System.Drawing.Color.White;
            this.panel12.Controls.Add(this.btnSendDoc);
            this.panel12.Controls.Add(this.radEng);
            this.panel12.Controls.Add(this.btnPrintPreview);
            this.panel12.Controls.Add(this.radThai);
            this.panel12.Controls.Add(this.lblMsg);
            this.panel12.Controls.Add(this.btnPrint);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel12.Location = new System.Drawing.Point(0, 706);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(1134, 36);
            this.panel12.TabIndex = 4;
            // 
            // btnSendDoc
            // 
            this.btnSendDoc.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSendDoc.Image = ((System.Drawing.Image)(resources.GetObject("btnSendDoc.Image")));
            this.btnSendDoc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSendDoc.Location = new System.Drawing.Point(5, 3);
            this.btnSendDoc.Name = "btnSendDoc";
            this.btnSendDoc.Size = new System.Drawing.Size(128, 30);
            this.btnSendDoc.TabIndex = 235;
            this.btnSendDoc.Text = "SEND TO DOC SCAN";
            this.btnSendDoc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSendDoc.UseVisualStyleBackColor = true;
            this.btnSendDoc.Click += new System.EventHandler(this.btnSendDoc_Click);
            // 
            // radEng
            // 
            this.radEng.AutoSize = true;
            this.radEng.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.radEng.Location = new System.Drawing.Point(776, 9);
            this.radEng.Name = "radEng";
            this.radEng.Size = new System.Drawing.Size(108, 17);
            this.radEng.TabIndex = 52;
            this.radEng.TabStop = true;
            this.radEng.Tag = "E";
            this.radEng.Text = "English Language";
            this.radEng.UseVisualStyleBackColor = true;
            // 
            // btnPrintPreview
            // 
            this.btnPrintPreview.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnPrintPreview.Image = ((System.Drawing.Image)(resources.GetObject("btnPrintPreview.Image")));
            this.btnPrintPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrintPreview.Location = new System.Drawing.Point(256, 3);
            this.btnPrintPreview.Name = "btnPrintPreview";
            this.btnPrintPreview.Size = new System.Drawing.Size(110, 30);
            this.btnPrintPreview.TabIndex = 50;
            this.btnPrintPreview.Text = "Print Preview";
            this.btnPrintPreview.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrintPreview.UseVisualStyleBackColor = true;
            this.btnPrintPreview.Click += new System.EventHandler(this.btnPrintPreview_Click);
            // 
            // radThai
            // 
            this.radThai.AutoSize = true;
            this.radThai.Checked = true;
            this.radThai.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.radThai.Location = new System.Drawing.Point(675, 10);
            this.radThai.Name = "radThai";
            this.radThai.Size = new System.Drawing.Size(95, 17);
            this.radThai.TabIndex = 51;
            this.radThai.TabStop = true;
            this.radThai.Tag = "T";
            this.radThai.Text = "Thai Language";
            this.radThai.UseVisualStyleBackColor = true;
            // 
            // lblMsg
            // 
            this.lblMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMsg.AutoSize = true;
            this.lblMsg.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblMsg.ForeColor = System.Drawing.Color.Red;
            this.lblMsg.Location = new System.Drawing.Point(890, 8);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(119, 18);
            this.lblMsg.TabIndex = 36;
            this.lblMsg.Text = "Save Complete";
            this.lblMsg.Visible = false;
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.ImageIndex = 14;
            this.btnPrint.ImageList = this.imageList1;
            this.btnPrint.Location = new System.Drawing.Point(136, 3);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnPrint.Size = new System.Drawing.Size(117, 30);
            this.btnPrint.TabIndex = 2;
            this.btnPrint.Text = "Print Preview";
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "add2.png");
            this.imageList1.Images.SetKeyName(1, "arrow_left_green.gif");
            this.imageList1.Images.SetKeyName(2, "arrow_right_green.gif");
            this.imageList1.Images.SetKeyName(3, "cancel.png");
            this.imageList1.Images.SetKeyName(4, "disk_blue.png");
            this.imageList1.Images.SetKeyName(5, "disks.png");
            this.imageList1.Images.SetKeyName(6, "document_add.png");
            this.imageList1.Images.SetKeyName(7, "document_into.jpg");
            this.imageList1.Images.SetKeyName(8, "document_out.png");
            this.imageList1.Images.SetKeyName(9, "document_view.png");
            this.imageList1.Images.SetKeyName(10, "folder.png");
            this.imageList1.Images.SetKeyName(11, "folder_sent_mail.png");
            this.imageList1.Images.SetKeyName(12, "folder_txt.png");
            this.imageList1.Images.SetKeyName(13, "history.gif");
            this.imageList1.Images.SetKeyName(14, "printer.png");
            this.imageList1.Images.SetKeyName(15, "SandC.png");
            this.imageList1.Images.SetKeyName(16, "sound.bmp");
            this.imageList1.Images.SetKeyName(17, "sound1.bmp");
            this.imageList1.Images.SetKeyName(18, "stopwatch_pause.png");
            this.imageList1.Images.SetKeyName(19, "Summary.png");
            this.imageList1.Images.SetKeyName(20, "view.png");
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1134, 37);
            this.label1.TabIndex = 1;
            this.label1.Text = "แบบบันทึกการตรวจสุขภาพอาชีวอนามัย (Occupational Medicine Checkup Record )";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trnquespatientBindingSource
            // 
            this.trnquespatientBindingSource.DataSource = typeof(DBCheckup.trn_ques_patient);
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            // 
            // frmQuestionareOccmed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1134, 742);
            this.Controls.Add(this.panel1);
            this.Name = "frmQuestionareOccmed";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmQuestionareOccmed";
            this.Load += new System.EventHandler(this.frmQuestionareOccmed_Load);
            this.panel1.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            this.panel12.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trnquespatientBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trnNewquespatientBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Button btnSendDoc;
        private System.Windows.Forms.RadioButton radEng;
        private System.Windows.Forms.Button btnPrintPreview;
        private System.Windows.Forms.RadioButton radThai;
        public System.Windows.Forms.Label lblMsg;
        public System.Windows.Forms.Button btnPrint;
        public System.Windows.Forms.ImageList imageList1;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.BindingSource trnquespatientBindingSource;
        public System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.BindingSource trnNewquespatientBindingSource;
        private System.Windows.Forms.WebBrowser webBrowser1;
    }
}