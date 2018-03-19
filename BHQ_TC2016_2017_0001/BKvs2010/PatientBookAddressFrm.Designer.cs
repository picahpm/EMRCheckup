namespace BKvs2010
{
    partial class PatientBookAddressFrm
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
            this.label65 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.lbAlertMsg = new UserControlLibrary.LabelAutoResizeFont();
            this.pnName = new UserControlLibrary.PanelRadioBinding();
            this.rdPerson = new System.Windows.Forms.RadioButton();
            this.rdTH = new System.Windows.Forms.RadioButton();
            this.rdEN = new System.Windows.Forms.RadioButton();
            this.pnAddress = new UserControlLibrary.PanelRadioBinding();
            this.rdOT = new System.Windows.Forms.RadioButton();
            this.rdHome = new System.Windows.Forms.RadioButton();
            this.rdCom = new System.Windows.Forms.RadioButton();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.TxtReceiver = new System.Windows.Forms.TextBox();
            this.pnName.SuspendLayout();
            this.pnAddress.SuspendLayout();
            this.SuspendLayout();
            // 
            // label65
            // 
            this.label65.BackColor = System.Drawing.Color.LightGreen;
            this.label65.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label65.Location = new System.Drawing.Point(12, 36);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(67, 22);
            this.label65.TabIndex = 141;
            this.label65.Text = "ชื่อผู้รับ :";
            this.label65.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.LightGreen;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(12, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 22);
            this.label1.TabIndex = 143;
            this.label1.Text = "ที่อยู่ :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(308, 215);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(76, 32);
            this.btnSave.TabIndex = 144;
            this.btnSave.Text = "Save Data";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(390, 215);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(62, 32);
            this.button1.TabIndex = 145;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Location = new System.Drawing.Point(458, 215);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(62, 32);
            this.btnPrint.TabIndex = 147;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // lbAlertMsg
            // 
            this.lbAlertMsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbAlertMsg.AutoResizeFont = true;
            this.lbAlertMsg.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbAlertMsg.ForeColor = System.Drawing.Color.Red;
            this.lbAlertMsg.Location = new System.Drawing.Point(12, 215);
            this.lbAlertMsg.MaxFontSize = 11.5F;
            this.lbAlertMsg.Name = "lbAlertMsg";
            this.lbAlertMsg.Size = new System.Drawing.Size(290, 30);
            this.lbAlertMsg.TabIndex = 146;
            this.lbAlertMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnName
            // 
            this.pnName.AbleNull = false;
            this.pnName.BorderRadius = true;
            this.pnName.Controls.Add(this.rdPerson);
            this.pnName.Controls.Add(this.rdTH);
            this.pnName.Controls.Add(this.rdEN);
            this.pnName.Location = new System.Drawing.Point(85, 9);
            this.pnName.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.pnName.Name = "pnName";
            this.pnName.ReadOnly = false;
            this.pnName.Size = new System.Drawing.Size(264, 24);
            this.pnName.TabIndex = 148;
            // 
            // rdPerson
            // 
            this.rdPerson.AutoCheck = false;
            this.rdPerson.AutoSize = true;
            this.rdPerson.Location = new System.Drawing.Point(147, 3);
            this.rdPerson.Name = "rdPerson";
            this.rdPerson.Size = new System.Drawing.Size(98, 17);
            this.rdPerson.TabIndex = 245;
            this.rdPerson.TabStop = true;
            this.rdPerson.Tag = "CP";
            this.rdPerson.Text = "Contact Person";
            this.rdPerson.UseVisualStyleBackColor = true;
            // 
            // rdTH
            // 
            this.rdTH.AutoCheck = false;
            this.rdTH.AutoSize = true;
            this.rdTH.Location = new System.Drawing.Point(19, 3);
            this.rdTH.Name = "rdTH";
            this.rdTH.Size = new System.Drawing.Size(46, 17);
            this.rdTH.TabIndex = 243;
            this.rdTH.TabStop = true;
            this.rdTH.Tag = "TH";
            this.rdTH.Text = "Thai";
            this.rdTH.UseVisualStyleBackColor = true;
            // 
            // rdEN
            // 
            this.rdEN.AutoCheck = false;
            this.rdEN.AutoSize = true;
            this.rdEN.Location = new System.Drawing.Point(84, 3);
            this.rdEN.Name = "rdEN";
            this.rdEN.Size = new System.Drawing.Size(44, 17);
            this.rdEN.TabIndex = 244;
            this.rdEN.TabStop = true;
            this.rdEN.Tag = "EN";
            this.rdEN.Text = "Eng";
            this.rdEN.UseVisualStyleBackColor = true;
            // 
            // pnAddress
            // 
            this.pnAddress.AbleNull = false;
            this.pnAddress.BorderRadius = true;
            this.pnAddress.Controls.Add(this.rdOT);
            this.pnAddress.Controls.Add(this.rdHome);
            this.pnAddress.Controls.Add(this.rdCom);
            this.pnAddress.Location = new System.Drawing.Point(85, 61);
            this.pnAddress.Name = "pnAddress";
            this.pnAddress.ReadOnly = false;
            this.pnAddress.Size = new System.Drawing.Size(264, 25);
            this.pnAddress.TabIndex = 254;
            // 
            // rdOT
            // 
            this.rdOT.AutoCheck = false;
            this.rdOT.AutoSize = true;
            this.rdOT.Location = new System.Drawing.Point(193, 4);
            this.rdOT.Name = "rdOT";
            this.rdOT.Size = new System.Drawing.Size(51, 17);
            this.rdOT.TabIndex = 248;
            this.rdOT.TabStop = true;
            this.rdOT.Tag = "O";
            this.rdOT.Text = "Other";
            this.rdOT.UseVisualStyleBackColor = true;
            // 
            // rdHome
            // 
            this.rdHome.AutoCheck = false;
            this.rdHome.AutoSize = true;
            this.rdHome.Location = new System.Drawing.Point(19, 4);
            this.rdHome.Name = "rdHome";
            this.rdHome.Size = new System.Drawing.Size(53, 17);
            this.rdHome.TabIndex = 246;
            this.rdHome.TabStop = true;
            this.rdHome.Tag = "H";
            this.rdHome.Text = "Home";
            this.rdHome.UseVisualStyleBackColor = true;
            // 
            // rdCom
            // 
            this.rdCom.AutoCheck = false;
            this.rdCom.AutoSize = true;
            this.rdCom.Location = new System.Drawing.Point(98, 4);
            this.rdCom.Name = "rdCom";
            this.rdCom.Size = new System.Drawing.Size(69, 17);
            this.rdCom.TabIndex = 247;
            this.rdCom.TabStop = true;
            this.rdCom.Tag = "C";
            this.rdCom.Text = "Company";
            this.rdCom.UseVisualStyleBackColor = true;
            // 
            // txtAddress
            // 
            this.txtAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddress.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtAddress.Location = new System.Drawing.Point(85, 91);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.ReadOnly = true;
            this.txtAddress.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAddress.Size = new System.Drawing.Size(435, 118);
            this.txtAddress.TabIndex = 255;
            // 
            // TxtReceiver
            // 
            this.TxtReceiver.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtReceiver.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtReceiver.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.TxtReceiver.Location = new System.Drawing.Point(85, 36);
            this.TxtReceiver.Name = "TxtReceiver";
            this.TxtReceiver.Size = new System.Drawing.Size(435, 22);
            this.TxtReceiver.TabIndex = 256;
            // 
            // PatientBookAddressFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 262);
            this.Controls.Add(this.TxtReceiver);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.pnAddress);
            this.Controls.Add(this.pnName);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.lbAlertMsg);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label65);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(548, 300);
            this.MinimumSize = new System.Drawing.Size(548, 266);
            this.Name = "PatientBookAddressFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PatientBookAddressFrm";
            this.pnName.ResumeLayout(false);
            this.pnName.PerformLayout();
            this.pnAddress.ResumeLayout(false);
            this.pnAddress.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button button1;
        private UserControlLibrary.LabelAutoResizeFont lbAlertMsg;
        private System.Windows.Forms.Button btnPrint;
        private UserControlLibrary.PanelRadioBinding pnName;
        private System.Windows.Forms.RadioButton rdTH;
        private System.Windows.Forms.RadioButton rdEN;
        private UserControlLibrary.PanelRadioBinding pnAddress;
        private System.Windows.Forms.RadioButton rdOT;
        private System.Windows.Forms.RadioButton rdHome;
        private System.Windows.Forms.RadioButton rdCom;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox TxtReceiver;
        private System.Windows.Forms.RadioButton rdPerson;

    }
}