namespace BKvs2010.UserControlEMR
{
    partial class OffShoreAircrewExamUC
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlClassific = new UserControlLibrary.PanelRadioBinding();
            this.rdFN = new System.Windows.Forms.RadioButton();
            this.rdFM = new System.Windows.Forms.RadioButton();
            this.rdFJ = new System.Windows.Forms.RadioButton();
            this.rdTUF = new System.Windows.Forms.RadioButton();
            this.rdPU = new System.Windows.Forms.RadioButton();
            this.bsDoctorOffShore = new System.Windows.Forms.BindingSource(this.components);
            this.bsDoctorHdr = new System.Windows.Forms.BindingSource(this.components);
            this.bsPatientRegis = new System.Windows.Forms.BindingSource(this.components);
            this.label45 = new System.Windows.Forms.Label();
            this.pnlParticular = new UserControlLibrary.PanelRadioBinding();
            this.rdNH = new System.Windows.Forms.RadioButton();
            this.rdHB = new System.Windows.Forms.RadioButton();
            this.rdFI = new System.Windows.Forms.RadioButton();
            this.txtWeekMonth = new UserControlLibrary.TextBoxDataType();
            this.rdFA = new System.Windows.Forms.RadioButton();
            this.label275 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.pnlDeclared = new UserControlLibrary.PanelRadioBinding();
            this.rdFO = new System.Windows.Forms.RadioButton();
            this.rdUO = new System.Windows.Forms.RadioButton();
            this.rdTF = new System.Windows.Forms.RadioButton();
            this.rdTU = new System.Windows.Forms.RadioButton();
            this.label43 = new System.Windows.Forms.Label();
            this.chkDiabetic = new UserControlLibrary.CheckBoxBinding();
            this.label42 = new System.Windows.Forms.Label();
            this.chkECK = new UserControlLibrary.CheckBoxBinding();
            this.chkCiga = new UserControlLibrary.CheckBoxBinding();
            this.label276 = new System.Windows.Forms.Label();
            this.label274 = new System.Windows.Forms.Label();
            this.txtresult = new System.Windows.Forms.TextBox();
            this.txtSummary = new System.Windows.Forms.TextBox();
            this.txtRecom = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.pnlClassific.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsDoctorOffShore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsDoctorHdr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPatientRegis)).BeginInit();
            this.pnlParticular.SuspendLayout();
            this.pnlDeclared.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScrollMargin = new System.Drawing.Size(991, 495);
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.pnlClassific);
            this.panel1.Controls.Add(this.label45);
            this.panel1.Controls.Add(this.pnlParticular);
            this.panel1.Controls.Add(this.label44);
            this.panel1.Controls.Add(this.pnlDeclared);
            this.panel1.Controls.Add(this.label43);
            this.panel1.Controls.Add(this.chkDiabetic);
            this.panel1.Controls.Add(this.label42);
            this.panel1.Controls.Add(this.chkECK);
            this.panel1.Controls.Add(this.chkCiga);
            this.panel1.Controls.Add(this.label276);
            this.panel1.Controls.Add(this.label274);
            this.panel1.Controls.Add(this.txtresult);
            this.panel1.Controls.Add(this.txtSummary);
            this.panel1.Controls.Add(this.txtRecom);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(991, 495);
            this.panel1.TabIndex = 0;
            // 
            // pnlClassific
            // 
            this.pnlClassific.AbleNull = false;
            this.pnlClassific.Controls.Add(this.rdFN);
            this.pnlClassific.Controls.Add(this.rdFM);
            this.pnlClassific.Controls.Add(this.rdFJ);
            this.pnlClassific.Controls.Add(this.rdTUF);
            this.pnlClassific.Controls.Add(this.rdPU);
            this.pnlClassific.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsDoctorOffShore, "tros_class_id", true));
            this.pnlClassific.Location = new System.Drawing.Point(38, 362);
            this.pnlClassific.Name = "pnlClassific";
            this.pnlClassific.ReadOnly = false;
            this.pnlClassific.Size = new System.Drawing.Size(947, 75);
            this.pnlClassific.TabIndex = 174;
            // 
            // rdFN
            // 
            this.rdFN.AutoCheck = false;
            this.rdFN.AutoSize = true;
            this.rdFN.Location = new System.Drawing.Point(6, 6);
            this.rdFN.Name = "rdFN";
            this.rdFN.Size = new System.Drawing.Size(152, 17);
            this.rdFN.TabIndex = 136;
            this.rdFN.TabStop = true;
            this.rdFN.Tag = "FN";
            this.rdFN.Text = "Fit to fly with no health risk.";
            this.rdFN.UseVisualStyleBackColor = true;
            // 
            // rdFM
            // 
            this.rdFM.AutoCheck = false;
            this.rdFM.AutoSize = true;
            this.rdFM.Location = new System.Drawing.Point(6, 29);
            this.rdFM.Name = "rdFM";
            this.rdFM.Size = new System.Drawing.Size(302, 17);
            this.rdFM.TabIndex = 137;
            this.rdFM.TabStop = true;
            this.rdFM.Tag = "FM";
            this.rdFM.Text = "Fit to fly with minor health risk. (Slightly abnormal Lab result)";
            this.rdFM.UseVisualStyleBackColor = true;
            // 
            // rdFJ
            // 
            this.rdFJ.AutoCheck = false;
            this.rdFJ.AutoSize = true;
            this.rdFJ.Location = new System.Drawing.Point(6, 53);
            this.rdFJ.Name = "rdFJ";
            this.rdFJ.Size = new System.Drawing.Size(433, 17);
            this.rdFJ.TabIndex = 140;
            this.rdFJ.TabStop = true;
            this.rdFJ.Tag = "FJ";
            this.rdFJ.Text = "Fit to fly with major health risk. (Significantly abnormal lab result. Approved b" +
    "y specialist)";
            this.rdFJ.UseVisualStyleBackColor = true;
            // 
            // rdTUF
            // 
            this.rdTUF.AutoCheck = false;
            this.rdTUF.AutoSize = true;
            this.rdTUF.Location = new System.Drawing.Point(473, 6);
            this.rdTUF.Name = "rdTUF";
            this.rdTUF.Size = new System.Drawing.Size(385, 17);
            this.rdTUF.TabIndex = 138;
            this.rdTUF.TabStop = true;
            this.rdTUF.Tag = "TU";
            this.rdTUF.Text = "Temporary unfit to fly. (Acute illness, chronic illness with acute exacerbation.)" +
    "";
            this.rdTUF.UseVisualStyleBackColor = true;
            // 
            // rdPU
            // 
            this.rdPU.AutoCheck = false;
            this.rdPU.AutoSize = true;
            this.rdPU.Location = new System.Drawing.Point(473, 29);
            this.rdPU.Name = "rdPU";
            this.rdPU.Size = new System.Drawing.Size(134, 17);
            this.rdPU.TabIndex = 139;
            this.rdPU.TabStop = true;
            this.rdPU.Tag = "PU";
            this.rdPU.Text = "Permanently unfit to fly.";
            this.rdPU.UseVisualStyleBackColor = true;
            // 
            // bsDoctorOffShore
            // 
            this.bsDoctorOffShore.DataMember = "trn_doctor_off_shores";
            this.bsDoctorOffShore.DataSource = this.bsDoctorHdr;
            // 
            // bsDoctorHdr
            // 
            this.bsDoctorHdr.DataMember = "trn_doctor_hdrs";
            this.bsDoctorHdr.DataSource = this.bsPatientRegis;
            // 
            // bsPatientRegis
            // 
            this.bsPatientRegis.DataSource = typeof(DBCheckup.trn_patient_regi);
            // 
            // label45
            // 
            this.label45.BackColor = System.Drawing.Color.LightGreen;
            this.label45.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label45.Location = new System.Drawing.Point(6, 6);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(979, 25);
            this.label45.TabIndex = 148;
            this.label45.Text = "Declared";
            this.label45.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlParticular
            // 
            this.pnlParticular.AbleNull = false;
            this.pnlParticular.Controls.Add(this.rdNH);
            this.pnlParticular.Controls.Add(this.rdHB);
            this.pnlParticular.Controls.Add(this.rdFI);
            this.pnlParticular.Controls.Add(this.txtWeekMonth);
            this.pnlParticular.Controls.Add(this.rdFA);
            this.pnlParticular.Controls.Add(this.label275);
            this.pnlParticular.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsDoctorOffShore, "tros_particular_id", true));
            this.pnlParticular.Location = new System.Drawing.Point(38, 118);
            this.pnlParticular.Name = "pnlParticular";
            this.pnlParticular.ReadOnly = false;
            this.pnlParticular.Size = new System.Drawing.Size(929, 49);
            this.pnlParticular.TabIndex = 173;
            // 
            // rdNH
            // 
            this.rdNH.AutoCheck = false;
            this.rdNH.AutoSize = true;
            this.rdNH.Location = new System.Drawing.Point(3, 5);
            this.rdNH.Name = "rdNH";
            this.rdNH.Size = new System.Drawing.Size(391, 17);
            this.rdNH.TabIndex = 131;
            this.rdNH.TabStop = true;
            this.rdNH.Tag = "NH";
            this.rdNH.Text = "He has no antibodies for Hepatitis B, so vaccination is strongly recommended.";
            this.rdNH.UseVisualStyleBackColor = true;
            // 
            // rdHB
            // 
            this.rdHB.AutoCheck = false;
            this.rdHB.AutoSize = true;
            this.rdHB.Location = new System.Drawing.Point(3, 27);
            this.rdHB.Name = "rdHB";
            this.rdHB.Size = new System.Drawing.Size(336, 17);
            this.rdHB.TabIndex = 132;
            this.rdHB.TabStop = true;
            this.rdHB.Tag = "HB";
            this.rdHB.Text = "He has antibodies for Hepatitis B, so vaccination is not necessary.";
            this.rdHB.UseVisualStyleBackColor = true;
            // 
            // rdFI
            // 
            this.rdFI.AutoCheck = false;
            this.rdFI.AutoSize = true;
            this.rdFI.Location = new System.Drawing.Point(486, 5);
            this.rdFI.Name = "rdFI";
            this.rdFI.Size = new System.Drawing.Size(192, 17);
            this.rdFI.TabIndex = 133;
            this.rdFI.TabStop = true;
            this.rdFI.Tag = "FI";
            this.rdFI.Text = "Should follow-up with your doctor in";
            this.rdFI.UseVisualStyleBackColor = true;
            // 
            // txtWeekMonth
            // 
            this.txtWeekMonth.BackColor = System.Drawing.Color.White;
            this.txtWeekMonth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtWeekMonth.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsDoctorOffShore, "tros_followup", true));
            this.txtWeekMonth.DataType = UserControlLibrary.TextBoxDataType.TypeData.Integer;
            this.txtWeekMonth.Enabled = false;
            this.txtWeekMonth.Location = new System.Drawing.Point(723, 2);
            this.txtWeekMonth.Multiline = true;
            this.txtWeekMonth.Name = "txtWeekMonth";
            this.txtWeekMonth.Size = new System.Drawing.Size(50, 23);
            this.txtWeekMonth.TabIndex = 111;
            // 
            // rdFA
            // 
            this.rdFA.AutoCheck = false;
            this.rdFA.AutoSize = true;
            this.rdFA.Location = new System.Drawing.Point(486, 27);
            this.rdFA.Name = "rdFA";
            this.rdFA.Size = new System.Drawing.Size(279, 17);
            this.rdFA.TabIndex = 134;
            this.rdFA.TabStop = true;
            this.rdFA.Tag = "FA";
            this.rdFA.Text = "Should follow-up with your doctor as soon as possible.";
            this.rdFA.UseVisualStyleBackColor = true;
            // 
            // label275
            // 
            this.label275.AutoSize = true;
            this.label275.Location = new System.Drawing.Point(778, 7);
            this.label275.Name = "label275";
            this.label275.Size = new System.Drawing.Size(80, 13);
            this.label275.TabIndex = 135;
            this.label275.Text = "weeks/months.";
            // 
            // label44
            // 
            this.label44.BackColor = System.Drawing.Color.LightGreen;
            this.label44.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label44.Location = new System.Drawing.Point(6, 90);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(979, 25);
            this.label44.TabIndex = 149;
            this.label44.Text = "Particular comments && Recomendations";
            this.label44.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlDeclared
            // 
            this.pnlDeclared.AbleNull = false;
            this.pnlDeclared.Controls.Add(this.rdFO);
            this.pnlDeclared.Controls.Add(this.rdUO);
            this.pnlDeclared.Controls.Add(this.rdTF);
            this.pnlDeclared.Controls.Add(this.rdTU);
            this.pnlDeclared.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsDoctorOffShore, "tros_declare_id", true));
            this.pnlDeclared.Location = new System.Drawing.Point(38, 34);
            this.pnlDeclared.Name = "pnlDeclared";
            this.pnlDeclared.ReadOnly = false;
            this.pnlDeclared.Size = new System.Drawing.Size(751, 52);
            this.pnlDeclared.TabIndex = 172;
            // 
            // rdFO
            // 
            this.rdFO.AutoCheck = false;
            this.rdFO.AutoSize = true;
            this.rdFO.Location = new System.Drawing.Point(3, 5);
            this.rdFO.Name = "rdFO";
            this.rdFO.Size = new System.Drawing.Size(121, 17);
            this.rdFO.TabIndex = 126;
            this.rdFO.TabStop = true;
            this.rdFO.Tag = "FO";
            this.rdFO.Text = "Fit to work off-shore.";
            this.rdFO.UseVisualStyleBackColor = true;
            // 
            // rdUO
            // 
            this.rdUO.AutoCheck = false;
            this.rdUO.AutoSize = true;
            this.rdUO.Location = new System.Drawing.Point(3, 29);
            this.rdUO.Name = "rdUO";
            this.rdUO.Size = new System.Drawing.Size(132, 17);
            this.rdUO.TabIndex = 127;
            this.rdUO.TabStop = true;
            this.rdUO.Tag = "UO";
            this.rdUO.Text = "Unfit to work off-shore.";
            this.rdUO.UseVisualStyleBackColor = true;
            // 
            // rdTF
            // 
            this.rdTF.AutoCheck = false;
            this.rdTF.AutoSize = true;
            this.rdTF.Location = new System.Drawing.Point(319, 5);
            this.rdTF.Name = "rdTF";
            this.rdTF.Size = new System.Drawing.Size(171, 17);
            this.rdTF.TabIndex = 128;
            this.rdTF.TabStop = true;
            this.rdTF.Tag = "TF";
            this.rdTF.Text = "Temporary fit to work off-shore.";
            this.rdTF.UseVisualStyleBackColor = true;
            // 
            // rdTU
            // 
            this.rdTU.AutoCheck = false;
            this.rdTU.AutoSize = true;
            this.rdTU.Location = new System.Drawing.Point(319, 29);
            this.rdTU.Name = "rdTU";
            this.rdTU.Size = new System.Drawing.Size(329, 17);
            this.rdTU.TabIndex = 129;
            this.rdTU.TabStop = true;
            this.rdTU.Tag = "TU";
            this.rdTU.Text = "Temporary unfit to work off-shore. So, please see recommended.";
            this.rdTU.UseVisualStyleBackColor = true;
            // 
            // label43
            // 
            this.label43.BackColor = System.Drawing.Color.LightGreen;
            this.label43.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label43.Location = new System.Drawing.Point(6, 170);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(979, 25);
            this.label43.TabIndex = 150;
            this.label43.Text = "Summary";
            this.label43.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkDiabetic
            // 
            this.chkDiabetic.AutoSize = true;
            this.chkDiabetic.BindingType = UserControlLibrary.CheckBoxBinding.TypeData.Boolean;
            this.chkDiabetic.BindingValue = null;
            this.chkDiabetic.CheckedFalseValue = "false";
            this.chkDiabetic.CheckedTrueValue = "true";
            this.chkDiabetic.DataBindings.Add(new System.Windows.Forms.Binding("BindingValue", this.bsDoctorOffShore, "tros_diabet", true));
            this.chkDiabetic.Location = new System.Drawing.Point(177, 468);
            this.chkDiabetic.Name = "chkDiabetic";
            this.chkDiabetic.Size = new System.Drawing.Size(65, 17);
            this.chkDiabetic.TabIndex = 171;
            this.chkDiabetic.Text = "Diabetic";
            this.chkDiabetic.UseVisualStyleBackColor = true;
            // 
            // label42
            // 
            this.label42.BackColor = System.Drawing.Color.LightGreen;
            this.label42.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label42.Location = new System.Drawing.Point(6, 252);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(979, 25);
            this.label42.TabIndex = 151;
            this.label42.Text = "Recommendations";
            this.label42.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkECK
            // 
            this.chkECK.AutoSize = true;
            this.chkECK.BindingType = UserControlLibrary.CheckBoxBinding.TypeData.Boolean;
            this.chkECK.BindingValue = null;
            this.chkECK.CheckedFalseValue = "false";
            this.chkECK.CheckedTrueValue = "true";
            this.chkECK.DataBindings.Add(new System.Windows.Forms.Binding("BindingValue", this.bsDoctorOffShore, "tros_ecg_lvh", true));
            this.chkECK.Location = new System.Drawing.Point(317, 468);
            this.chkECK.Name = "chkECK";
            this.chkECK.Size = new System.Drawing.Size(72, 17);
            this.chkECK.TabIndex = 170;
            this.chkECK.Text = "ECG-LVH";
            this.chkECK.UseVisualStyleBackColor = true;
            // 
            // chkCiga
            // 
            this.chkCiga.AutoSize = true;
            this.chkCiga.BindingType = UserControlLibrary.CheckBoxBinding.TypeData.Boolean;
            this.chkCiga.BindingValue = null;
            this.chkCiga.CheckedFalseValue = "false";
            this.chkCiga.CheckedTrueValue = "true";
            this.chkCiga.DataBindings.Add(new System.Windows.Forms.Binding("BindingValue", this.bsDoctorOffShore, "tros_cigaret", true));
            this.chkCiga.Location = new System.Drawing.Point(38, 468);
            this.chkCiga.Name = "chkCiga";
            this.chkCiga.Size = new System.Drawing.Size(73, 17);
            this.chkCiga.TabIndex = 169;
            this.chkCiga.Text = "Cigarettes";
            this.chkCiga.UseVisualStyleBackColor = true;
            // 
            // label276
            // 
            this.label276.BackColor = System.Drawing.Color.LightGreen;
            this.label276.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label276.Location = new System.Drawing.Point(6, 440);
            this.label276.Name = "label276";
            this.label276.Size = new System.Drawing.Size(979, 25);
            this.label276.TabIndex = 168;
            this.label276.Text = "CRI (Coronary Heart Disease Risk Factor Prediction Chart)";
            this.label276.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label274
            // 
            this.label274.BackColor = System.Drawing.Color.LightGreen;
            this.label274.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label274.Location = new System.Drawing.Point(6, 334);
            this.label274.Name = "label274";
            this.label274.Size = new System.Drawing.Size(979, 25);
            this.label274.TabIndex = 167;
            this.label274.Text = "Classification (สำหรับการบินไทย) : ";
            this.label274.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtresult
            // 
            this.txtresult.BackColor = System.Drawing.Color.White;
            this.txtresult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtresult.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsDoctorOffShore, "tros_result", true));
            this.txtresult.Location = new System.Drawing.Point(517, 468);
            this.txtresult.Multiline = true;
            this.txtresult.Name = "txtresult";
            this.txtresult.Size = new System.Drawing.Size(431, 23);
            this.txtresult.TabIndex = 158;
            // 
            // txtSummary
            // 
            this.txtSummary.BackColor = System.Drawing.Color.White;
            this.txtSummary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSummary.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsDoctorOffShore, "tros_summary", true));
            this.txtSummary.Location = new System.Drawing.Point(6, 194);
            this.txtSummary.Multiline = true;
            this.txtSummary.Name = "txtSummary";
            this.txtSummary.Size = new System.Drawing.Size(979, 55);
            this.txtSummary.TabIndex = 159;
            // 
            // txtRecom
            // 
            this.txtRecom.BackColor = System.Drawing.Color.White;
            this.txtRecom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRecom.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsDoctorOffShore, "tros_recomment", true));
            this.txtRecom.Location = new System.Drawing.Point(6, 276);
            this.txtRecom.Multiline = true;
            this.txtRecom.Name = "txtRecom";
            this.txtRecom.Size = new System.Drawing.Size(979, 55);
            this.txtRecom.TabIndex = 160;
            // 
            // OffShoreAircrewExamUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "OffShoreAircrewExamUC";
            this.Size = new System.Drawing.Size(991, 495);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlClassific.ResumeLayout(false);
            this.pnlClassific.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsDoctorOffShore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsDoctorHdr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPatientRegis)).EndInit();
            this.pnlParticular.ResumeLayout(false);
            this.pnlParticular.PerformLayout();
            this.pnlDeclared.ResumeLayout(false);
            this.pnlDeclared.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private UserControlLibrary.PanelRadioBinding pnlClassific;
        private System.Windows.Forms.RadioButton rdFN;
        private System.Windows.Forms.RadioButton rdFM;
        private System.Windows.Forms.RadioButton rdFJ;
        private System.Windows.Forms.RadioButton rdTUF;
        private System.Windows.Forms.RadioButton rdPU;
        private System.Windows.Forms.Label label45;
        private UserControlLibrary.PanelRadioBinding pnlParticular;
        private System.Windows.Forms.RadioButton rdNH;
        private System.Windows.Forms.RadioButton rdHB;
        private System.Windows.Forms.RadioButton rdFI;
        private UserControlLibrary.TextBoxDataType txtWeekMonth;
        private System.Windows.Forms.RadioButton rdFA;
        private System.Windows.Forms.Label label275;
        private System.Windows.Forms.Label label44;
        private UserControlLibrary.PanelRadioBinding pnlDeclared;
        private System.Windows.Forms.RadioButton rdFO;
        private System.Windows.Forms.RadioButton rdUO;
        private System.Windows.Forms.RadioButton rdTF;
        private System.Windows.Forms.RadioButton rdTU;
        private System.Windows.Forms.Label label43;
        private UserControlLibrary.CheckBoxBinding chkDiabetic;
        private System.Windows.Forms.Label label42;
        private UserControlLibrary.CheckBoxBinding chkECK;
        private UserControlLibrary.CheckBoxBinding chkCiga;
        private System.Windows.Forms.Label label276;
        private System.Windows.Forms.Label label274;
        private System.Windows.Forms.TextBox txtresult;
        private System.Windows.Forms.TextBox txtSummary;
        private System.Windows.Forms.TextBox txtRecom;
        private System.Windows.Forms.BindingSource bsPatientRegis;
        private System.Windows.Forms.BindingSource bsDoctorHdr;
        private System.Windows.Forms.BindingSource bsDoctorOffShore;
    }
}
