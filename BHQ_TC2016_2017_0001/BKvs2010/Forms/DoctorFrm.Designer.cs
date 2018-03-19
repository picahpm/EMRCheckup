namespace BKvs2010.Forms
{
    partial class DoctorFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DoctorFrm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.uiFooter1 = new BKvs2010.Usercontrols.UIFooter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSendToDocscan = new System.Windows.Forms.Button();
            this.ChOutstanding = new System.Windows.Forms.CheckBox();
            this.btnPrintPreview = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnSendToCheckB = new System.Windows.Forms.Button();
            this.btnSendAuto = new System.Windows.Forms.Button();
            this.btnSendManual = new System.Windows.Forms.Button();
            this.btnSaveDraft = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.lbAlertMsg = new UserControlLibrary.LabelAutoResizeFont();
            this.label161 = new System.Windows.Forms.Label();
            this.label242 = new System.Windows.Forms.Label();
            this.groupQueue = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnHold = new System.Windows.Forms.Button();
            this.btnCallQueue = new System.Windows.Forms.Button();
            this.btnReady = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel12 = new System.Windows.Forms.Panel();
            this.subjectiveUC1 = new BKvs2010.UserControlEMR.SubjectiveUC();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel8 = new System.Windows.Forms.Panel();
            this.objectiveUC1 = new BKvs2010.UserControlEMR.ObjectiveUC();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.panel10 = new System.Windows.Forms.Panel();
            this.additionalItemUC1 = new BKvs2010.UserControlEMR.AdditionalItemUC();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.panel11 = new System.Windows.Forms.Panel();
            this.tabAssessmentAndPlanUC1 = new BKvs2010.UserControlEMR.TabAssessmentAndPlanUC();
            this.panel9 = new System.Windows.Forms.Panel();
            this.autoCompleteUC1 = new UserControlLibrary.TextBoxAutoComplete();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.newPatientMappingUC1 = new BKvs2010.Usercontrols.newPatientMappingUC();
            this.panel5 = new System.Windows.Forms.Panel();
            this.newWaitingListStationUC1 = new BKvs2010.Usercontrols.newWaitingListStationUC();
            this.panel4 = new System.Windows.Forms.Panel();
            this.newPatientProfileStationUC1 = new BKvs2010.Usercontrols.newPatientProfileStationUC();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel7.SuspendLayout();
            this.groupQueue.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel12.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel8.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.panel10.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel9.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 270F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1280, 678);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.panel3, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.panel2, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.panel7, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.panel9, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(270, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 212F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1010, 735);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.AutoSize = true;
            this.panel3.Controls.Add(this.uiFooter1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 523);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1010, 212);
            this.panel3.TabIndex = 3;
            // 
            // uiFooter1
            // 
            this.uiFooter1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.uiFooter1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiFooter1.IsEditEMR = false;
            this.uiFooter1.Location = new System.Drawing.Point(0, 0);
            this.uiFooter1.mhs_id = null;
            this.uiFooter1.Name = "uiFooter1";
            this.uiFooter1.RoomCode = null;
            this.uiFooter1.Size = new System.Drawing.Size(1010, 212);
            this.uiFooter1.StrSearch = null;
            this.uiFooter1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.btnSendToDocscan);
            this.panel2.Controls.Add(this.ChOutstanding);
            this.panel2.Controls.Add(this.btnPrintPreview);
            this.panel2.Controls.Add(this.btnPrint);
            this.panel2.Controls.Add(this.btnSendToCheckB);
            this.panel2.Controls.Add(this.btnSendAuto);
            this.panel2.Controls.Add(this.btnSendManual);
            this.panel2.Controls.Add(this.btnSaveDraft);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 492);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1010, 31);
            this.panel2.TabIndex = 2;
            // 
            // btnSendToDocscan
            // 
            this.btnSendToDocscan.Enabled = false;
            this.btnSendToDocscan.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSendToDocscan.Image = ((System.Drawing.Image)(resources.GetObject("btnSendToDocscan.Image")));
            this.btnSendToDocscan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSendToDocscan.Location = new System.Drawing.Point(752, 3);
            this.btnSendToDocscan.Name = "btnSendToDocscan";
            this.btnSendToDocscan.Size = new System.Drawing.Size(135, 25);
            this.btnSendToDocscan.TabIndex = 90;
            this.btnSendToDocscan.Text = "Send to Doc. Scan";
            this.btnSendToDocscan.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSendToDocscan.UseVisualStyleBackColor = true;
            this.btnSendToDocscan.Click += new System.EventHandler(this.btnSendToDocscan_Click);
            // 
            // ChOutstanding
            // 
            this.ChOutstanding.AutoSize = true;
            this.ChOutstanding.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.ChOutstanding.ForeColor = System.Drawing.Color.Red;
            this.ChOutstanding.Location = new System.Drawing.Point(3, 5);
            this.ChOutstanding.Name = "ChOutstanding";
            this.ChOutstanding.Size = new System.Drawing.Size(191, 20);
            this.ChOutstanding.TabIndex = 294;
            this.ChOutstanding.Text = "ยังค้างตรวจผู้รับบริการท่านนี้อยู่";
            this.ChOutstanding.UseVisualStyleBackColor = true;
            // 
            // btnPrintPreview
            // 
            this.btnPrintPreview.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnPrintPreview.Image = global::BKvs2010.Properties.Resources.btnPrintPreview_Image;
            this.btnPrintPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrintPreview.Location = new System.Drawing.Point(893, 3);
            this.btnPrintPreview.Name = "btnPrintPreview";
            this.btnPrintPreview.Size = new System.Drawing.Size(110, 25);
            this.btnPrintPreview.TabIndex = 293;
            this.btnPrintPreview.Text = "Print Preview";
            this.btnPrintPreview.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrintPreview.UseVisualStyleBackColor = true;
            this.btnPrintPreview.Click += new System.EventHandler(this.btnPrintPreview_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnPrint.Image = global::BKvs2010.Properties.Resources.btnPrint_Image;
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.Location = new System.Drawing.Point(802, 3);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(85, 25);
            this.btnPrint.TabIndex = 291;
            this.btnPrint.Text = "Print    ";
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Visible = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnSendToCheckB
            // 
            this.btnSendToCheckB.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSendToCheckB.Image = global::BKvs2010.Properties.Resources.btnSendToCheckB_Image;
            this.btnSendToCheckB.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSendToCheckB.Location = new System.Drawing.Point(604, 3);
            this.btnSendToCheckB.Name = "btnSendToCheckB";
            this.btnSendToCheckB.Size = new System.Drawing.Size(125, 25);
            this.btnSendToCheckB.TabIndex = 290;
            this.btnSendToCheckB.Text = "Send to Check B";
            this.btnSendToCheckB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSendToCheckB.UseVisualStyleBackColor = true;
            this.btnSendToCheckB.Click += new System.EventHandler(this.btnSendToCheckB_Click);
            // 
            // btnSendAuto
            // 
            this.btnSendAuto.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSendAuto.Image = global::BKvs2010.Properties.Resources.btnSendAuto_Image;
            this.btnSendAuto.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSendAuto.Location = new System.Drawing.Point(463, 3);
            this.btnSendAuto.Name = "btnSendAuto";
            this.btnSendAuto.Size = new System.Drawing.Size(135, 25);
            this.btnSendAuto.TabIndex = 289;
            this.btnSendAuto.Text = "Save && Send Auto";
            this.btnSendAuto.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSendAuto.UseVisualStyleBackColor = true;
            this.btnSendAuto.Click += new System.EventHandler(this.btnSendAuto_Click);
            // 
            // btnSendManual
            // 
            this.btnSendManual.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSendManual.Image = global::BKvs2010.Properties.Resources.btnSaveDraft_Image;
            this.btnSendManual.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSendManual.Location = new System.Drawing.Point(312, 3);
            this.btnSendManual.Name = "btnSendManual";
            this.btnSendManual.Size = new System.Drawing.Size(145, 25);
            this.btnSendManual.TabIndex = 288;
            this.btnSendManual.Text = "Save && Send Manual";
            this.btnSendManual.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSendManual.UseVisualStyleBackColor = true;
            this.btnSendManual.Click += new System.EventHandler(this.btnSendManual_Click);
            // 
            // btnSaveDraft
            // 
            this.btnSaveDraft.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSaveDraft.Image = global::BKvs2010.Properties.Resources.btnSaveDraft_Image;
            this.btnSaveDraft.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveDraft.Location = new System.Drawing.Point(200, 3);
            this.btnSaveDraft.Name = "btnSaveDraft";
            this.btnSaveDraft.Size = new System.Drawing.Size(106, 25);
            this.btnSaveDraft.TabIndex = 287;
            this.btnSaveDraft.Text = "Save as Draft";
            this.btnSaveDraft.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSaveDraft.UseVisualStyleBackColor = true;
            this.btnSaveDraft.Click += new System.EventHandler(this.btnSaveDraft_Click);
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.lbAlertMsg);
            this.panel7.Controls.Add(this.label161);
            this.panel7.Controls.Add(this.label242);
            this.panel7.Controls.Add(this.groupQueue);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Margin = new System.Windows.Forms.Padding(0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1010, 50);
            this.panel7.TabIndex = 0;
            // 
            // lbAlertMsg
            // 
            this.lbAlertMsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbAlertMsg.AutoResizeFont = true;
            this.lbAlertMsg.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbAlertMsg.ForeColor = System.Drawing.Color.Red;
            this.lbAlertMsg.Location = new System.Drawing.Point(7, 9);
            this.lbAlertMsg.MaxFontSize = 11.5F;
            this.lbAlertMsg.Name = "lbAlertMsg";
            this.lbAlertMsg.Size = new System.Drawing.Size(589, 31);
            this.lbAlertMsg.TabIndex = 85;
            this.lbAlertMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label161
            // 
            this.label161.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label161.AutoSize = true;
            this.label161.Font = new System.Drawing.Font("Tahoma", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label161.Location = new System.Drawing.Point(4, 15);
            this.label161.Name = "label161";
            this.label161.Size = new System.Drawing.Size(0, 18);
            this.label161.TabIndex = 26;
            this.label161.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label242
            // 
            this.label242.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label242.AutoSize = true;
            this.label242.Font = new System.Drawing.Font("Tahoma", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label242.Location = new System.Drawing.Point(2, 21);
            this.label242.Name = "label242";
            this.label242.Size = new System.Drawing.Size(0, 18);
            this.label242.TabIndex = 28;
            this.label242.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // groupQueue
            // 
            this.groupQueue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupQueue.Controls.Add(this.btnCancel);
            this.groupQueue.Controls.Add(this.btnHold);
            this.groupQueue.Controls.Add(this.btnCallQueue);
            this.groupQueue.Controls.Add(this.btnReady);
            this.groupQueue.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.groupQueue.Location = new System.Drawing.Point(602, 2);
            this.groupQueue.Name = "groupQueue";
            this.groupQueue.Size = new System.Drawing.Size(404, 46);
            this.groupQueue.TabIndex = 83;
            this.groupQueue.TabStop = false;
            this.groupQueue.Text = "Queue";
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnCancel.Image = global::BKvs2010.Properties.Resources.btnCancel_Image;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(303, 15);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(96, 25);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Skip      ";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnHold
            // 
            this.btnHold.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnHold.Image = global::BKvs2010.Properties.Resources.btnHold_Image;
            this.btnHold.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHold.Location = new System.Drawing.Point(204, 15);
            this.btnHold.Name = "btnHold";
            this.btnHold.Size = new System.Drawing.Size(96, 25);
            this.btnHold.TabIndex = 7;
            this.btnHold.Text = "Hold Queue";
            this.btnHold.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnHold.UseVisualStyleBackColor = true;
            this.btnHold.Click += new System.EventHandler(this.btnHold_Click);
            // 
            // btnCallQueue
            // 
            this.btnCallQueue.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnCallQueue.Image = global::BKvs2010.Properties.Resources.btnCallQueue_Image;
            this.btnCallQueue.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCallQueue.Location = new System.Drawing.Point(105, 15);
            this.btnCallQueue.Name = "btnCallQueue";
            this.btnCallQueue.Size = new System.Drawing.Size(96, 25);
            this.btnCallQueue.TabIndex = 6;
            this.btnCallQueue.Text = "Call Queue";
            this.btnCallQueue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCallQueue.UseVisualStyleBackColor = true;
            this.btnCallQueue.Click += new System.EventHandler(this.btnCallQueue_Click);
            // 
            // btnReady
            // 
            this.btnReady.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnReady.Location = new System.Drawing.Point(6, 15);
            this.btnReady.Name = "btnReady";
            this.btnReady.Size = new System.Drawing.Size(96, 25);
            this.btnReady.TabIndex = 5;
            this.btnReady.Text = "Ready";
            this.btnReady.UseVisualStyleBackColor = true;
            this.btnReady.Click += new System.EventHandler(this.btnReady_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.panel1.Location = new System.Drawing.Point(2, 85);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1006, 405);
            this.panel1.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1006, 405);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.Tag = "History";
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.panel12);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(998, 376);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Subjective";
            // 
            // panel12
            // 
            this.panel12.AutoScroll = true;
            this.panel12.AutoScrollMinSize = new System.Drawing.Size(972, 630);
            this.panel12.Controls.Add(this.subjectiveUC1);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel12.Location = new System.Drawing.Point(0, 0);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(998, 376);
            this.panel12.TabIndex = 0;
            // 
            // subjectiveUC1
            // 
            this.subjectiveUC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.subjectiveUC1.Location = new System.Drawing.Point(0, 0);
            this.subjectiveUC1.Name = "subjectiveUC1";
            this.subjectiveUC1.PatientRegis = null;
            this.subjectiveUC1.Size = new System.Drawing.Size(981, 630);
            this.subjectiveUC1.TabIndex = 0;
            this.subjectiveUC1.username = null;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Transparent;
            this.tabPage2.Controls.Add(this.panel8);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(192, 71);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Objective";
            // 
            // panel8
            // 
            this.panel8.AutoScroll = true;
            this.panel8.AutoScrollMinSize = new System.Drawing.Size(980, 621);
            this.panel8.Controls.Add(this.objectiveUC1);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Margin = new System.Windows.Forms.Padding(0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(192, 71);
            this.panel8.TabIndex = 0;
            // 
            // objectiveUC1
            // 
            this.objectiveUC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectiveUC1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.objectiveUC1.Location = new System.Drawing.Point(0, 0);
            this.objectiveUC1.Name = "objectiveUC1";
            this.objectiveUC1.PatientRegis = null;
            this.objectiveUC1.Size = new System.Drawing.Size(980, 621);
            this.objectiveUC1.TabIndex = 0;
            this.objectiveUC1.username = null;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.panel10);
            this.tabPage5.Location = new System.Drawing.Point(4, 25);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(192, 71);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Package and Addition Item";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // panel10
            // 
            this.panel10.AutoScroll = true;
            this.panel10.AutoScrollMinSize = new System.Drawing.Size(365, 663);
            this.panel10.Controls.Add(this.additionalItemUC1);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.panel10.Location = new System.Drawing.Point(0, 0);
            this.panel10.Margin = new System.Windows.Forms.Padding(0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(192, 71);
            this.panel10.TabIndex = 0;
            // 
            // additionalItemUC1
            // 
            this.additionalItemUC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.additionalItemUC1.language = "TH";
            this.additionalItemUC1.Location = new System.Drawing.Point(0, 0);
            this.additionalItemUC1.Name = "additionalItemUC1";
            this.additionalItemUC1.PatientRegis = null;
            this.additionalItemUC1.Size = new System.Drawing.Size(365, 663);
            this.additionalItemUC1.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.AutoScroll = true;
            this.tabPage3.Controls.Add(this.panel11);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(192, 71);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Assessment and Plan";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // panel11
            // 
            this.panel11.AutoScroll = true;
            this.panel11.AutoScrollMinSize = new System.Drawing.Size(975, 650);
            this.panel11.Controls.Add(this.tabAssessmentAndPlanUC1);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel11.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.panel11.Location = new System.Drawing.Point(0, 0);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(192, 71);
            this.panel11.TabIndex = 0;
            // 
            // tabAssessmentAndPlanUC1
            // 
            this.tabAssessmentAndPlanUC1.BackColor = System.Drawing.Color.Transparent;
            this.tabAssessmentAndPlanUC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabAssessmentAndPlanUC1.Location = new System.Drawing.Point(0, 0);
            this.tabAssessmentAndPlanUC1.Margin = new System.Windows.Forms.Padding(0);
            this.tabAssessmentAndPlanUC1.MinimumSize = new System.Drawing.Size(975, 650);
            this.tabAssessmentAndPlanUC1.Name = "tabAssessmentAndPlanUC1";
            this.tabAssessmentAndPlanUC1.PatientRegis = null;
            this.tabAssessmentAndPlanUC1.Size = new System.Drawing.Size(975, 650);
            this.tabAssessmentAndPlanUC1.TabIndex = 0;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.autoCompleteUC1);
            this.panel9.Controls.Add(this.label1);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(3, 53);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(1004, 27);
            this.panel9.TabIndex = 4;
            // 
            // autoCompleteUC1
            // 
            this.autoCompleteUC1.DataSource = null;
            this.autoCompleteUC1.DisplayMember = "";
            this.autoCompleteUC1.DropDownWidth = 0;
            this.autoCompleteUC1.Enabled = false;
            this.autoCompleteUC1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.autoCompleteUC1.FreeText = false;
            this.autoCompleteUC1.Location = new System.Drawing.Point(81, 4);
            this.autoCompleteUC1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.autoCompleteUC1.MinTextSearch = 2;
            this.autoCompleteUC1.Name = "autoCompleteUC1";
            this.autoCompleteUC1.SelectedItem = null;
            this.autoCompleteUC1.SelectedValue = null;
            this.autoCompleteUC1.Size = new System.Drawing.Size(373, 22);
            this.autoCompleteUC1.TabIndex = 159;
            this.autoCompleteUC1.ValueMember = "";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.LightGreen;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(0, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 22);
            this.label1.TabIndex = 158;
            this.label1.Text = "Doctor Name :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.panel6, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.panel5, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 364F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 231F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(270, 735);
            this.tableLayoutPanel3.TabIndex = 4;
            // 
            // panel6
            // 
            this.panel6.AutoSize = true;
            this.panel6.Controls.Add(this.newPatientMappingUC1);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(2, 597);
            this.panel6.Margin = new System.Windows.Forms.Padding(2);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(266, 136);
            this.panel6.TabIndex = 2;
            // 
            // newPatientMappingUC1
            // 
            this.newPatientMappingUC1.BackColor = System.Drawing.Color.White;
            this.newPatientMappingUC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.newPatientMappingUC1.Location = new System.Drawing.Point(0, 0);
            this.newPatientMappingUC1.Name = "newPatientMappingUC1";
            this.newPatientMappingUC1.Size = new System.Drawing.Size(266, 136);
            this.newPatientMappingUC1.TabIndex = 0;
            this.newPatientMappingUC1.tpr_id = null;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.newWaitingListStationUC1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(2, 366);
            this.panel5.Margin = new System.Windows.Forms.Padding(2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(266, 227);
            this.panel5.TabIndex = 1;
            // 
            // newWaitingListStationUC1
            // 
            this.newWaitingListStationUC1.ableDoubleClick = true;
            this.newWaitingListStationUC1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.newWaitingListStationUC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.newWaitingListStationUC1.LargeSize = false;
            this.newWaitingListStationUC1.Location = new System.Drawing.Point(0, 0);
            this.newWaitingListStationUC1.mrd_id = null;
            this.newWaitingListStationUC1.mut_id = null;
            this.newWaitingListStationUC1.Name = "newWaitingListStationUC1";
            this.newWaitingListStationUC1.Size = new System.Drawing.Size(266, 227);
            this.newWaitingListStationUC1.TabIndex = 0;
            this.newWaitingListStationUC1.OnWaitingSuccessProcess += new BKvs2010.Usercontrols.newWaitingListStationUC.OnSuccessProcess(this.newWaitingListStationUC1_OnWaitingSuccessProcess);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.newPatientProfileStationUC1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(2, 2);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(266, 360);
            this.panel4.TabIndex = 0;
            // 
            // newPatientProfileStationUC1
            // 
            this.newPatientProfileStationUC1.BackColor = System.Drawing.Color.Transparent;
            this.newPatientProfileStationUC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.newPatientProfileStationUC1.Location = new System.Drawing.Point(0, 0);
            this.newPatientProfileStationUC1.Name = "newPatientProfileStationUC1";
            this.newPatientProfileStationUC1.Size = new System.Drawing.Size(266, 360);
            this.newPatientProfileStationUC1.TabIndex = 0;
            this.newPatientProfileStationUC1.tpr_id = null;
            // 
            // DoctorFrm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1280, 702);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.MinimumSize = new System.Drawing.Size(1278, 726);
            this.Name = "DoctorFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ระบบ EMR Check-up Pathway - Doctor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tpridChanged += new BKvs2010.Forms.CheckupInheriteFrm.TpridChanged(this.DoctorFrm_tpridChanged);
            this.mrdidChanged += new BKvs2010.Forms.CheckupInheriteFrm.MrdidChanged(this.DoctorFrm_mrdidChanged);
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.groupQueue.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel7;
        private UserControlLibrary.LabelAutoResizeFont lbAlertMsg;
        private System.Windows.Forms.Label label161;
        private System.Windows.Forms.Label label242;
        private System.Windows.Forms.GroupBox groupQueue;
        private System.Windows.Forms.Button btnReady;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panel6;
        private Usercontrols.newPatientMappingUC newPatientMappingUC1;
        private System.Windows.Forms.Panel panel5;
        private Usercontrols.newWaitingListStationUC newWaitingListStationUC1;
        private System.Windows.Forms.Panel panel4;
        private Usercontrols.newPatientProfileStationUC newPatientProfileStationUC1;
        private Usercontrols.UIFooter uiFooter1;
        private System.Windows.Forms.Button btnSaveDraft;
        private System.Windows.Forms.Button btnSendManual;
        private System.Windows.Forms.Button btnSendAuto;
        private System.Windows.Forms.Button btnSendToCheckB;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnPrintPreview;
        private System.Windows.Forms.Button btnCallQueue;
        private System.Windows.Forms.Button btnHold;
        private System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.TabControl tabControl1;
        public System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel panel12;
        private UserControlEMR.SubjectiveUC subjectiveUC1;
        public System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel8;
        private UserControlEMR.ObjectiveUC objectiveUC1;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Panel panel10;
        private UserControlEMR.AdditionalItemUC additionalItemUC1;
        public System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Panel panel11;
        private UserControlEMR.TabAssessmentAndPlanUC tabAssessmentAndPlanUC1;
        private System.Windows.Forms.CheckBox ChOutstanding;
        private System.Windows.Forms.Button btnSendToDocscan;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label1;
        private UserControlLibrary.TextBoxAutoComplete autoCompleteUC1;
    }
}