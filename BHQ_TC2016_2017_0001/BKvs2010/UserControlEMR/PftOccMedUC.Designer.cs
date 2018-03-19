namespace BKvs2010.UserControlEMR
{
    partial class PftOccMedUC
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
            this.bsPatientPFT = new System.Windows.Forms.BindingSource(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.grbChest = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label66 = new System.Windows.Forms.Label();
            this.label65 = new System.Windows.Forms.Label();
            this.label64 = new System.Windows.Forms.Label();
            this.label63 = new System.Windows.Forms.Label();
            this.label62 = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.bsPatientRegis = new System.Windows.Forms.BindingSource(this.components);
            this.plPFTOccMedResult = new UserControlLibrary.PanelRadioBinding();
            this.txtPFTOccMedAbnormal = new System.Windows.Forms.TextBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.txtMatchs4 = new UserControlLibrary.TextBoxDataType();
            this.txtMatchs3 = new UserControlLibrary.TextBoxDataType();
            this.txtMatchs2 = new UserControlLibrary.TextBoxDataType();
            this.txtMatchs1 = new UserControlLibrary.TextBoxDataType();
            this.txtfef_target = new UserControlLibrary.TextBoxDataType();
            this.txttarget_value = new UserControlLibrary.TextBoxDataType();
            this.txtfve1_target = new UserControlLibrary.TextBoxDataType();
            this.txtfvc_target = new UserControlLibrary.TextBoxDataType();
            this.txtfef_active = new UserControlLibrary.TextBoxDataType();
            this.txtactive_value = new UserControlLibrary.TextBoxDataType();
            this.txtfve1_active = new UserControlLibrary.TextBoxDataType();
            this.txtfvc_active = new UserControlLibrary.TextBoxDataType();
            ((System.ComponentModel.ISupportInitialize)(this.bsPatientPFT)).BeginInit();
            this.panel2.SuspendLayout();
            this.grbChest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsPatientRegis)).BeginInit();
            this.plPFTOccMedResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // bsPatientPFT
            // 
            this.bsPatientPFT.DataMember = "trn_pfts";
            this.bsPatientPFT.DataSource = this.bsPatientRegis;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.grbChest);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(733, 285);
            this.panel2.TabIndex = 1;
            // 
            // grbChest
            // 
            this.grbChest.Controls.Add(this.label1);
            this.grbChest.Controls.Add(this.plPFTOccMedResult);
            this.grbChest.Controls.Add(this.label66);
            this.grbChest.Controls.Add(this.label65);
            this.grbChest.Controls.Add(this.label64);
            this.grbChest.Controls.Add(this.txtMatchs4);
            this.grbChest.Controls.Add(this.txtMatchs3);
            this.grbChest.Controls.Add(this.txtMatchs2);
            this.grbChest.Controls.Add(this.txtMatchs1);
            this.grbChest.Controls.Add(this.txtfef_target);
            this.grbChest.Controls.Add(this.txttarget_value);
            this.grbChest.Controls.Add(this.txtfve1_target);
            this.grbChest.Controls.Add(this.txtfvc_target);
            this.grbChest.Controls.Add(this.txtfef_active);
            this.grbChest.Controls.Add(this.txtactive_value);
            this.grbChest.Controls.Add(this.txtfve1_active);
            this.grbChest.Controls.Add(this.txtfvc_active);
            this.grbChest.Controls.Add(this.label63);
            this.grbChest.Controls.Add(this.label62);
            this.grbChest.Controls.Add(this.label61);
            this.grbChest.Controls.Add(this.label60);
            this.grbChest.Location = new System.Drawing.Point(14, 12);
            this.grbChest.Name = "grbChest";
            this.grbChest.Size = new System.Drawing.Size(703, 259);
            this.grbChest.TabIndex = 98;
            this.grbChest.TabStop = false;
            this.grbChest.Text = "ผลการตรวจสภาพปอด (Pulmonary Function Test)";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(18, 173);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 21);
            this.label1.TabIndex = 27;
            this.label1.Text = "ผลการตรวจสภาพปอด";
            // 
            // label66
            // 
            this.label66.Location = new System.Drawing.Point(281, 41);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(74, 16);
            this.label66.TabIndex = 120;
            this.label66.Text = "ร้อยละ %";
            this.label66.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label65
            // 
            this.label65.Location = new System.Drawing.Point(196, 41);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(86, 16);
            this.label65.TabIndex = 119;
            this.label65.Text = "ค่าที่ควรได้";
            this.label65.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label64
            // 
            this.label64.Location = new System.Drawing.Point(127, 41);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(86, 16);
            this.label64.TabIndex = 118;
            this.label64.Text = "ค่าที่วัดได้";
            this.label64.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label63
            // 
            this.label63.Location = new System.Drawing.Point(18, 136);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(86, 16);
            this.label63.TabIndex = 29;
            this.label63.Text = "FEF 25-75%";
            // 
            // label62
            // 
            this.label62.Location = new System.Drawing.Point(18, 114);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(102, 16);
            this.label62.TabIndex = 28;
            this.label62.Text = "FEV1/FVC(%)";
            // 
            // label61
            // 
            this.label61.Location = new System.Drawing.Point(18, 87);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(86, 16);
            this.label61.TabIndex = 27;
            this.label61.Text = "FVE1 (Lit.)";
            // 
            // label60
            // 
            this.label60.Location = new System.Drawing.Point(18, 62);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(86, 16);
            this.label60.TabIndex = 26;
            this.label60.Text = "FVC (Lit.)";
            // 
            // bsPatientRegis
            // 
            this.bsPatientRegis.DataSource = typeof(DBCheckup.trn_patient_regi);
            // 
            // plPFTOccMedResult
            // 
            this.plPFTOccMedResult.Controls.Add(this.txtPFTOccMedAbnormal);
            this.plPFTOccMedResult.Controls.Add(this.radioButton2);
            this.plPFTOccMedResult.Controls.Add(this.radioButton1);
            this.plPFTOccMedResult.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsPatientPFT, "tpf_pft_occmed_result", true));
            this.plPFTOccMedResult.AbleNull = true;
            this.plPFTOccMedResult.Location = new System.Drawing.Point(150, 168);
            this.plPFTOccMedResult.Name = "plPFTOccMedResult";
            this.plPFTOccMedResult.Size = new System.Drawing.Size(538, 74);
            this.plPFTOccMedResult.TabIndex = 121;
            // 
            // txtPFTOccMedAbnormal
            // 
            this.txtPFTOccMedAbnormal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPFTOccMedAbnormal.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsPatientPFT, "tpf_pft_occmed_abnormal", true));
            this.txtPFTOccMedAbnormal.Location = new System.Drawing.Point(163, 3);
            this.txtPFTOccMedAbnormal.Multiline = true;
            this.txtPFTOccMedAbnormal.Name = "txtPFTOccMedAbnormal";
            this.txtPFTOccMedAbnormal.Size = new System.Drawing.Size(372, 68);
            this.txtPFTOccMedAbnormal.TabIndex = 2;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoCheck = false;
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(84, 3);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(81, 20);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Tag = "A";
            this.radioButton2.Text = "Abnormal";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoCheck = false;
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(3, 3);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(67, 20);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.Tag = "N";
            this.radioButton1.Text = "Normal";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // txtMatchs4
            // 
            this.txtMatchs4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMatchs4.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsPatientPFT, "tpfmatch4_value", true));
            this.txtMatchs4.Enabled = false;
            this.txtMatchs4.DataType = UserControlLibrary.TextBoxDataType.TypeData.Integer;
            this.txtMatchs4.Location = new System.Drawing.Point(291, 137);
            this.txtMatchs4.MaxLength = 8;
            this.txtMatchs4.Name = "txtMatchs4";
            this.txtMatchs4.Size = new System.Drawing.Size(55, 23);
            this.txtMatchs4.TabIndex = 117;
            // 
            // txtMatchs3
            // 
            this.txtMatchs3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMatchs3.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsPatientPFT, "tpfmatch3_value", true));
            this.txtMatchs3.Enabled = false;
            this.txtMatchs3.DataType = UserControlLibrary.TextBoxDataType.TypeData.Integer;
            this.txtMatchs3.Location = new System.Drawing.Point(291, 111);
            this.txtMatchs3.MaxLength = 8;
            this.txtMatchs3.Name = "txtMatchs3";
            this.txtMatchs3.Size = new System.Drawing.Size(55, 23);
            this.txtMatchs3.TabIndex = 116;
            // 
            // txtMatchs2
            // 
            this.txtMatchs2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMatchs2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsPatientPFT, "tpfmatch2_value", true));
            this.txtMatchs2.Enabled = false;
            this.txtMatchs2.DataType = UserControlLibrary.TextBoxDataType.TypeData.Integer;
            this.txtMatchs2.Location = new System.Drawing.Point(291, 85);
            this.txtMatchs2.MaxLength = 8;
            this.txtMatchs2.Name = "txtMatchs2";
            this.txtMatchs2.Size = new System.Drawing.Size(55, 23);
            this.txtMatchs2.TabIndex = 115;
            // 
            // txtMatchs1
            // 
            this.txtMatchs1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMatchs1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsPatientPFT, "tpfmatch1_value", true));
            this.txtMatchs1.Enabled = false;
            this.txtMatchs1.DataType = UserControlLibrary.TextBoxDataType.TypeData.Integer;
            this.txtMatchs1.Location = new System.Drawing.Point(291, 60);
            this.txtMatchs1.MaxLength = 8;
            this.txtMatchs1.Name = "txtMatchs1";
            this.txtMatchs1.Size = new System.Drawing.Size(55, 23);
            this.txtMatchs1.TabIndex = 114;
            // 
            // txtfef_target
            // 
            this.txtfef_target.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtfef_target.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsPatientPFT, "tpffef_target", true));
            this.txtfef_target.DataType = UserControlLibrary.TextBoxDataType.TypeData.Decimal;
            this.txtfef_target.Location = new System.Drawing.Point(219, 137);
            this.txtfef_target.MaxLength = 8;
            this.txtfef_target.Name = "txtfef_target";
            this.txtfef_target.Size = new System.Drawing.Size(41, 23);
            this.txtfef_target.TabIndex = 113;
            // 
            // txttarget_value
            // 
            this.txttarget_value.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txttarget_value.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsPatientPFT, "tpftarget_value", true));
            this.txttarget_value.DataType = UserControlLibrary.TextBoxDataType.TypeData.Decimal;
            this.txttarget_value.Location = new System.Drawing.Point(219, 111);
            this.txttarget_value.MaxLength = 8;
            this.txttarget_value.Name = "txttarget_value";
            this.txttarget_value.Size = new System.Drawing.Size(41, 23);
            this.txttarget_value.TabIndex = 111;
            // 
            // txtfve1_target
            // 
            this.txtfve1_target.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtfve1_target.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsPatientPFT, "tpffve1_target", true));
            this.txtfve1_target.DataType = UserControlLibrary.TextBoxDataType.TypeData.Decimal;
            this.txtfve1_target.Location = new System.Drawing.Point(219, 85);
            this.txtfve1_target.MaxLength = 8;
            this.txtfve1_target.Name = "txtfve1_target";
            this.txtfve1_target.Size = new System.Drawing.Size(41, 23);
            this.txtfve1_target.TabIndex = 109;
            // 
            // txtfvc_target
            // 
            this.txtfvc_target.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtfvc_target.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsPatientPFT, "tpffvc_target", true));
            this.txtfvc_target.DataType = UserControlLibrary.TextBoxDataType.TypeData.Decimal;
            this.txtfvc_target.Location = new System.Drawing.Point(219, 60);
            this.txtfvc_target.MaxLength = 8;
            this.txtfvc_target.Name = "txtfvc_target";
            this.txtfvc_target.Size = new System.Drawing.Size(41, 23);
            this.txtfvc_target.TabIndex = 107;
            // 
            // txtfef_active
            // 
            this.txtfef_active.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtfef_active.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsPatientPFT, "tpffef_active", true));
            this.txtfef_active.DataType = UserControlLibrary.TextBoxDataType.TypeData.Decimal;
            this.txtfef_active.Location = new System.Drawing.Point(150, 137);
            this.txtfef_active.MaxLength = 8;
            this.txtfef_active.Name = "txtfef_active";
            this.txtfef_active.Size = new System.Drawing.Size(41, 23);
            this.txtfef_active.TabIndex = 112;
            // 
            // txtactive_value
            // 
            this.txtactive_value.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtactive_value.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsPatientPFT, "tpfactive_value", true));
            this.txtactive_value.DataType = UserControlLibrary.TextBoxDataType.TypeData.Decimal;
            this.txtactive_value.Location = new System.Drawing.Point(150, 111);
            this.txtactive_value.MaxLength = 8;
            this.txtactive_value.Name = "txtactive_value";
            this.txtactive_value.Size = new System.Drawing.Size(41, 23);
            this.txtactive_value.TabIndex = 110;
            // 
            // txtfve1_active
            // 
            this.txtfve1_active.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtfve1_active.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsPatientPFT, "tpffve1_active", true));
            this.txtfve1_active.DataType = UserControlLibrary.TextBoxDataType.TypeData.Decimal;
            this.txtfve1_active.Location = new System.Drawing.Point(150, 85);
            this.txtfve1_active.MaxLength = 8;
            this.txtfve1_active.Name = "txtfve1_active";
            this.txtfve1_active.Size = new System.Drawing.Size(41, 23);
            this.txtfve1_active.TabIndex = 108;
            // 
            // txtfvc_active
            // 
            this.txtfvc_active.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtfvc_active.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsPatientPFT, "tpffvc_active", true));
            this.txtfvc_active.DataType = UserControlLibrary.TextBoxDataType.TypeData.Decimal;
            this.txtfvc_active.Location = new System.Drawing.Point(150, 60);
            this.txtfvc_active.MaxLength = 8;
            this.txtfvc_active.Name = "txtfvc_active";
            this.txtfvc_active.Size = new System.Drawing.Size(41, 23);
            this.txtfvc_active.TabIndex = 106;
            // 
            // PftOccMedUC
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MinimumSize = new System.Drawing.Size(733, 285);
            this.Name = "PftOccMedUC";
            this.Size = new System.Drawing.Size(733, 285);
            ((System.ComponentModel.ISupportInitialize)(this.bsPatientPFT)).EndInit();
            this.panel2.ResumeLayout(false);
            this.grbChest.ResumeLayout(false);
            this.grbChest.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsPatientRegis)).EndInit();
            this.plPFTOccMedResult.ResumeLayout(false);
            this.plPFTOccMedResult.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bsPatientRegis;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.BindingSource bsPatientPFT;
        private System.Windows.Forms.GroupBox grbChest;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.Label label64;
        private UserControlLibrary.TextBoxDataType txtMatchs4;
        private UserControlLibrary.TextBoxDataType txtMatchs3;
        private UserControlLibrary.TextBoxDataType txtMatchs2;
        private UserControlLibrary.TextBoxDataType txtMatchs1;
        private UserControlLibrary.TextBoxDataType txtfef_target;
        private UserControlLibrary.TextBoxDataType txttarget_value;
        private UserControlLibrary.TextBoxDataType txtfve1_target;
        private UserControlLibrary.TextBoxDataType txtfvc_target;
        private UserControlLibrary.TextBoxDataType txtfef_active;
        private UserControlLibrary.TextBoxDataType txtactive_value;
        private UserControlLibrary.TextBoxDataType txtfve1_active;
        private UserControlLibrary.TextBoxDataType txtfvc_active;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.Label label60;
        private UserControlLibrary.PanelRadioBinding plPFTOccMedResult;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPFTOccMedAbnormal;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
    }
}
