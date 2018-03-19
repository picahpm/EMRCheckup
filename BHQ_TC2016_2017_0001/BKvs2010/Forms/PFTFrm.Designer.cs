namespace BKvs2010.Forms
{
    partial class PFTFrm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.uiFooter1 = new BKvs2010.Usercontrols.UIFooter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnPrintPreview = new System.Windows.Forms.Button();
            this.btnSendDoc = new System.Windows.Forms.Button();
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
            this.tabPftUC1 = new BKvs2010.UserControlEMR.TabPftUC();
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
            this.tableLayoutPanel2.Controls.Add(this.panel3, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.panel2, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.panel7, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(270, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 212F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1010, 678);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.AutoSize = true;
            this.panel3.Controls.Add(this.uiFooter1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 466);
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
            this.panel2.Controls.Add(this.btnPrintPreview);
            this.panel2.Controls.Add(this.btnSendDoc);
            this.panel2.Controls.Add(this.btnPrint);
            this.panel2.Controls.Add(this.btnSendToCheckB);
            this.panel2.Controls.Add(this.btnSendAuto);
            this.panel2.Controls.Add(this.btnSendManual);
            this.panel2.Controls.Add(this.btnSaveDraft);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 435);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1010, 31);
            this.panel2.TabIndex = 2;
            // 
            // btnPrintPreview
            // 
            this.btnPrintPreview.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
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
            // btnSendDoc
            // 
            this.btnSendDoc.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSendDoc.Image = global::BKvs2010.Properties.Resources.btnSendDoc_Image;
            this.btnSendDoc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSendDoc.Location = new System.Drawing.Point(750, 3);
            this.btnSendDoc.Name = "btnSendDoc";
            this.btnSendDoc.Size = new System.Drawing.Size(137, 25);
            this.btnSendDoc.TabIndex = 292;
            this.btnSendDoc.Text = "Send to Doc. Scan";
            this.btnSendDoc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSendDoc.UseVisualStyleBackColor = true;
            this.btnSendDoc.Click += new System.EventHandler(this.btnSendDoc_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnPrint.Image = global::BKvs2010.Properties.Resources.btnPrint_Image;
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.Location = new System.Drawing.Point(659, 3);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(85, 25);
            this.btnPrint.TabIndex = 291;
            this.btnPrint.Text = "Print    ";
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnSendToCheckB
            // 
            this.btnSendToCheckB.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSendToCheckB.Image = global::BKvs2010.Properties.Resources.btnSendToCheckB_Image;
            this.btnSendToCheckB.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSendToCheckB.Location = new System.Drawing.Point(431, 3);
            this.btnSendToCheckB.Name = "btnSendToCheckB";
            this.btnSendToCheckB.Size = new System.Drawing.Size(127, 25);
            this.btnSendToCheckB.TabIndex = 290;
            this.btnSendToCheckB.Text = "Send to Check B";
            this.btnSendToCheckB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSendToCheckB.UseVisualStyleBackColor = true;
            this.btnSendToCheckB.Click += new System.EventHandler(this.btnSendToCheckB_Click);
            // 
            // btnSendAuto
            // 
            this.btnSendAuto.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSendAuto.Image = global::BKvs2010.Properties.Resources.btnSendAuto_Image;
            this.btnSendAuto.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSendAuto.Location = new System.Drawing.Point(285, 3);
            this.btnSendAuto.Name = "btnSendAuto";
            this.btnSendAuto.Size = new System.Drawing.Size(140, 25);
            this.btnSendAuto.TabIndex = 289;
            this.btnSendAuto.Text = "Save && Send Auto";
            this.btnSendAuto.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSendAuto.UseVisualStyleBackColor = true;
            this.btnSendAuto.Click += new System.EventHandler(this.btnSendAuto_Click);
            // 
            // btnSendManual
            // 
            this.btnSendManual.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSendManual.Image = global::BKvs2010.Properties.Resources.btnSendManual_Image;
            this.btnSendManual.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSendManual.Location = new System.Drawing.Point(125, 3);
            this.btnSendManual.Name = "btnSendManual";
            this.btnSendManual.Size = new System.Drawing.Size(154, 25);
            this.btnSendManual.TabIndex = 288;
            this.btnSendManual.Text = "Save && Send Manual";
            this.btnSendManual.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSendManual.UseVisualStyleBackColor = true;
            this.btnSendManual.Click += new System.EventHandler(this.btnSendManual_Click);
            // 
            // btnSaveDraft
            // 
            this.btnSaveDraft.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSaveDraft.Image = global::BKvs2010.Properties.Resources.btnSaveDraft_Image;
            this.btnSaveDraft.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveDraft.Location = new System.Drawing.Point(7, 3);
            this.btnSaveDraft.Name = "btnSaveDraft";
            this.btnSaveDraft.Size = new System.Drawing.Size(112, 25);
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
            this.panel1.AutoScrollMinSize = new System.Drawing.Size(920, 675);
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.tabPftUC1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(2, 52);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1006, 381);
            this.panel1.TabIndex = 1;
            // 
            // tabPftUC1
            // 
            this.tabPftUC1.BackColor = System.Drawing.Color.Transparent;
            this.tabPftUC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPftUC1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.tabPftUC1.Location = new System.Drawing.Point(0, 0);
            this.tabPftUC1.Margin = new System.Windows.Forms.Padding(0);
            this.tabPftUC1.Name = "tabPftUC1";
            this.tabPftUC1.PatientRegis = null;
            this.tabPftUC1.Size = new System.Drawing.Size(989, 675);
            this.tabPftUC1.TabIndex = 0;
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
            this.tableLayoutPanel3.Size = new System.Drawing.Size(270, 678);
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
            this.panel6.Size = new System.Drawing.Size(266, 79);
            this.panel6.TabIndex = 2;
            // 
            // newPatientMappingUC1
            // 
            this.newPatientMappingUC1.BackColor = System.Drawing.Color.White;
            this.newPatientMappingUC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.newPatientMappingUC1.Location = new System.Drawing.Point(0, 0);
            this.newPatientMappingUC1.Name = "newPatientMappingUC1";
            this.newPatientMappingUC1.Size = new System.Drawing.Size(266, 79);
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
            // PFTFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1280, 702);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(1278, 726);
            this.Name = "PFTFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ระบบ EMR Check-up Pathway - PFT";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tpridChanged += new BKvs2010.Forms.CheckupInheriteFrm.TpridChanged(this.PFTFrm_tpridChanged);
            this.mrdidChanged += new BKvs2010.Forms.CheckupInheriteFrm.MrdidChanged(this.PFTFrm_mrdidChanged);
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.groupQueue.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
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
        private System.Windows.Forms.Button btnSendDoc;
        private System.Windows.Forms.Button btnPrintPreview;
        private System.Windows.Forms.Button btnCallQueue;
        private System.Windows.Forms.Button btnHold;
        private System.Windows.Forms.Button btnCancel;
        private UserControlEMR.TabPftUC tabPftUC1;
    }
}