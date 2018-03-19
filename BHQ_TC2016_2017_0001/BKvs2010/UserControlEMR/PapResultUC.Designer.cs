namespace BKvs2010.UserControlEMR
{
    partial class PapResultUC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PapResultUC));
            this.label11 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtOther = new System.Windows.Forms.TextBox();
            this.DDsuggustion = new UserControlLibrary.ComboAutoDropDownWidth();
            this.label12 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.bsObstetricsDoc = new System.Windows.Forms.BindingSource(this.components);
            this.bsPatientRegis = new System.Windows.Forms.BindingSource(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnOtherAb = new UserControlLibrary.PanelRadioBinding();
            this.pnOther = new System.Windows.Forms.Panel();
            this.chkOtherAbFungus = new UserControlLibrary.CheckBoxBinding();
            this.txtOtherAbRemark = new System.Windows.Forms.TextBox();
            this.chkOtherAbBateria = new UserControlLibrary.CheckBoxBinding();
            this.chkOtherAbOther = new UserControlLibrary.CheckBoxBinding();
            this.rbOtherAbS = new System.Windows.Forms.RadioButton();
            this.chkOtherAb = new System.Windows.Forms.CheckBox();
            this.rbOtherAbM = new System.Windows.Forms.RadioButton();
            this.rbOtherAbN = new System.Windows.Forms.RadioButton();
            this.pnCellResult = new UserControlLibrary.PanelRadioBinding();
            this.txtResultAbRemark = new System.Windows.Forms.TextBox();
            this.rbCellResultO = new System.Windows.Forms.RadioButton();
            this.rbCellResultH = new System.Windows.Forms.RadioButton();
            this.rbCellResultL = new System.Windows.Forms.RadioButton();
            this.rbCellResultA = new System.Windows.Forms.RadioButton();
            this.panel7 = new System.Windows.Forms.Panel();
            this.rbCellDummy = new System.Windows.Forms.RadioButton();
            this.rbCellResultN = new System.Windows.Forms.RadioButton();
            this.chkHVPRec = new UserControlLibrary.CheckBoxBinding();
            this.pnRecommend = new UserControlLibrary.PanelRadioBinding();
            this.chkRec = new System.Windows.Forms.CheckBox();
            this.rbRecFU = new System.Windows.Forms.RadioButton();
            this.rbRecFR = new System.Windows.Forms.RadioButton();
            this.txtRecRemark = new System.Windows.Forms.TextBox();
            this.rbRecFO = new System.Windows.Forms.RadioButton();
            this.pnRecOther = new UserControlLibrary.PanelRadioBinding();
            this.rbRecOtherTS = new System.Windows.Forms.RadioButton();
            this.rbRecOtherTR = new System.Windows.Forms.RadioButton();
            this.rbRecOtherNT = new System.Windows.Forms.RadioButton();
            this.pnHPVTest = new UserControlLibrary.PanelRadioBinding();
            this.rbHPVTestNT = new System.Windows.Forms.RadioButton();
            this.txtHPVRemark = new System.Windows.Forms.TextBox();
            this.rbHPVTestNH = new System.Windows.Forms.RadioButton();
            this.rbHPVTestHR = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsObstetricsDoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPatientRegis)).BeginInit();
            this.panel1.SuspendLayout();
            this.pnOtherAb.SuspendLayout();
            this.pnOther.SuspendLayout();
            this.pnCellResult.SuspendLayout();
            this.panel7.SuspendLayout();
            this.pnRecommend.SuspendLayout();
            this.pnRecOther.SuspendLayout();
            this.pnHPVTest.SuspendLayout();
            this.SuspendLayout();
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.SystemColors.HotTrack;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(0, 10);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(450, 21);
            this.label11.TabIndex = 9;
            this.label11.Text = "ผลการตรวจ / Lab result";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.SystemColors.HotTrack;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(-3, 10);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(440, 21);
            this.label14.TabIndex = 11;
            this.label14.Text = "คำแนะนำทางการแพทย์ / Medical recommendation";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pnOtherAb);
            this.groupBox1.Controls.Add(this.pnCellResult);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Location = new System.Drawing.Point(3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(450, 393);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pnRecommend);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.pnRecOther);
            this.groupBox2.Location = new System.Drawing.Point(456, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(437, 393);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.pnHPVTest);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Location = new System.Drawing.Point(3, 394);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(890, 120);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.SystemColors.HotTrack;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(0, 10);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(890, 21);
            this.label15.TabIndex = 13;
            this.label15.Text = "ผลการตรวจ / Lab result";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.groupBox5);
            this.groupBox4.Controls.Add(this.chkHVPRec);
            this.groupBox4.Controls.Add(this.DDsuggustion);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Location = new System.Drawing.Point(3, 514);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(890, 214);
            this.groupBox4.TabIndex = 15;
            this.groupBox4.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtOther);
            this.groupBox5.Location = new System.Drawing.Point(456, 35);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(428, 170);
            this.groupBox5.TabIndex = 13;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "อื่นๆ";
            // 
            // txtOther
            // 
            this.txtOther.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOther.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsObstetricsDoc, "obd_doc_result_thai", true));
            this.txtOther.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtOther.Location = new System.Drawing.Point(10, 17);
            this.txtOther.MaxLength = 300;
            this.txtOther.Multiline = true;
            this.txtOther.Name = "txtOther";
            this.txtOther.Size = new System.Drawing.Size(412, 146);
            this.txtOther.TabIndex = 1;
            // 
            // DDsuggustion
            // 
            this.DDsuggustion.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bsObstetricsDoc, "mdr_id", true));
            this.DDsuggustion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DDsuggustion.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.DDsuggustion.FormattingEnabled = true;
            this.DDsuggustion.Location = new System.Drawing.Point(6, 70);
            this.DDsuggustion.Name = "DDsuggustion";
            this.DDsuggustion.Size = new System.Drawing.Size(121, 24);
            this.DDsuggustion.TabIndex = 11;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label12.Location = new System.Drawing.Point(4, 35);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(408, 37);
            this.label12.TabIndex = 10;
            this.label12.Text = "ควรตรวจหาเซลล์มะเร็งปากมดลูกซ้ำในครั้งต่อไป\r\nRecommendation for the next Thin Pre" +
                "p Pap Test within";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.SystemColors.HotTrack;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(0, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(890, 21);
            this.label8.TabIndex = 8;
            this.label8.Text = "คำแนะนำ";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bsObstetricsDoc
            // 
            this.bsObstetricsDoc.DataMember = "trn_obstetric_docs";
            this.bsObstetricsDoc.DataSource = this.bsPatientRegis;
            // 
            // bsPatientRegis
            // 
            this.bsPatientRegis.DataSource = typeof(DBCheckup.trn_patient_regi);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.MinimumSize = new System.Drawing.Size(897, 730);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(897, 730);
            this.panel1.TabIndex = 16;
            // 
            // pnOtherAb
            // 
            this.pnOtherAb.Controls.Add(this.pnOther);
            this.pnOtherAb.Controls.Add(this.rbOtherAbS);
            this.pnOtherAb.Controls.Add(this.chkOtherAb);
            this.pnOtherAb.Controls.Add(this.rbOtherAbM);
            this.pnOtherAb.Controls.Add(this.rbOtherAbN);
            this.pnOtherAb.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsObstetricsDoc, "obd_other_ab", true));
            this.pnOtherAb.AbleNull = false;
            this.pnOtherAb.Location = new System.Drawing.Point(3, 202);
            this.pnOtherAb.Name = "pnOtherAb";
            this.pnOtherAb.Size = new System.Drawing.Size(441, 185);
            this.pnOtherAb.TabIndex = 11;
            // 
            // pnOther
            // 
            this.pnOther.Controls.Add(this.chkOtherAbFungus);
            this.pnOther.Controls.Add(this.txtOtherAbRemark);
            this.pnOther.Controls.Add(this.chkOtherAbBateria);
            this.pnOther.Controls.Add(this.chkOtherAbOther);
            this.pnOther.Location = new System.Drawing.Point(44, 105);
            this.pnOther.Name = "pnOther";
            this.pnOther.Size = new System.Drawing.Size(380, 76);
            this.pnOther.TabIndex = 4;
            // 
            // chkOtherAbFungus
            // 
            this.chkOtherAbFungus.AutoSize = true;
            this.chkOtherAbFungus.BindingType = UserControlLibrary.CheckBoxBinding.TypeData.Char;
            this.chkOtherAbFungus.BindingValue = null;
            this.chkOtherAbFungus.CheckedFalseValue = "N";
            this.chkOtherAbFungus.CheckedTrueValue = "Y";
            this.chkOtherAbFungus.DataBindings.Add(new System.Windows.Forms.Binding("BindingValue", this.bsObstetricsDoc, "obd_other_ab_fungus", true));
            this.chkOtherAbFungus.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkOtherAbFungus.Location = new System.Drawing.Point(2, 2);
            this.chkOtherAbFungus.Name = "chkOtherAbFungus";
            this.chkOtherAbFungus.Size = new System.Drawing.Size(157, 20);
            this.chkOtherAbFungus.TabIndex = 0;
            this.chkOtherAbFungus.Text = "ตรวจพบเชื้อรา / Fungas";
            this.chkOtherAbFungus.UseVisualStyleBackColor = true;
            this.chkOtherAbFungus.CheckedChanged += new System.EventHandler(this.chkOtherAbFungus_CheckedChanged);
            // 
            // txtOtherAbRemark
            // 
            this.txtOtherAbRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOtherAbRemark.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsObstetricsDoc, "obd_other_ab_other_remark", true));
            this.txtOtherAbRemark.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtOtherAbRemark.Location = new System.Drawing.Point(121, 50);
            this.txtOtherAbRemark.MaxLength = 100;
            this.txtOtherAbRemark.Multiline = true;
            this.txtOtherAbRemark.Name = "txtOtherAbRemark";
            this.txtOtherAbRemark.Size = new System.Drawing.Size(257, 23);
            this.txtOtherAbRemark.TabIndex = 3;
            this.txtOtherAbRemark.TextChanged += new System.EventHandler(this.txtOtherAbRemark_TextChanged);
            // 
            // chkOtherAbBateria
            // 
            this.chkOtherAbBateria.AutoSize = true;
            this.chkOtherAbBateria.BindingType = UserControlLibrary.CheckBoxBinding.TypeData.Char;
            this.chkOtherAbBateria.BindingValue = null;
            this.chkOtherAbBateria.CheckedFalseValue = "N";
            this.chkOtherAbBateria.CheckedTrueValue = "Y";
            this.chkOtherAbBateria.DataBindings.Add(new System.Windows.Forms.Binding("BindingValue", this.bsObstetricsDoc, "obd_other_ab_bacteria", true));
            this.chkOtherAbBateria.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkOtherAbBateria.Location = new System.Drawing.Point(2, 28);
            this.chkOtherAbBateria.Name = "chkOtherAbBateria";
            this.chkOtherAbBateria.Size = new System.Drawing.Size(251, 20);
            this.chkOtherAbBateria.TabIndex = 1;
            this.chkOtherAbBateria.Text = "ตรวจพบเชื้อแบคทีเรีย / Bacteria vaginitis";
            this.chkOtherAbBateria.UseVisualStyleBackColor = true;
            this.chkOtherAbBateria.CheckedChanged += new System.EventHandler(this.chkOtherAbBateria_CheckedChanged);
            // 
            // chkOtherAbOther
            // 
            this.chkOtherAbOther.AutoSize = true;
            this.chkOtherAbOther.BindingType = UserControlLibrary.CheckBoxBinding.TypeData.Char;
            this.chkOtherAbOther.BindingValue = null;
            this.chkOtherAbOther.CheckedFalseValue = "N";
            this.chkOtherAbOther.CheckedTrueValue = "Y";
            this.chkOtherAbOther.DataBindings.Add(new System.Windows.Forms.Binding("BindingValue", this.bsObstetricsDoc, "obd_other_ab_other", true));
            this.chkOtherAbOther.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkOtherAbOther.Location = new System.Drawing.Point(2, 54);
            this.chkOtherAbOther.Name = "chkOtherAbOther";
            this.chkOtherAbOther.Size = new System.Drawing.Size(113, 20);
            this.chkOtherAbOther.TabIndex = 2;
            this.chkOtherAbOther.Text = "ตรวจพบเชื้ออื่นๆ";
            this.chkOtherAbOther.UseVisualStyleBackColor = true;
            this.chkOtherAbOther.CheckedChanged += new System.EventHandler(this.chkOtherAbOther_CheckedChanged);
            // 
            // rbOtherAbS
            // 
            this.rbOtherAbS.AutoSize = true;
            this.rbOtherAbS.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.rbOtherAbS.Location = new System.Drawing.Point(27, 80);
            this.rbOtherAbS.Name = "rbOtherAbS";
            this.rbOtherAbS.Size = new System.Drawing.Size(243, 20);
            this.rbOtherAbS.TabIndex = 3;
            this.rbOtherAbS.TabStop = true;
            this.rbOtherAbS.Tag = "S";
            this.rbOtherAbS.Text = "มีการอักเสบมาก / Severe inflammation";
            this.rbOtherAbS.UseVisualStyleBackColor = true;
            this.rbOtherAbS.CheckedChanged += new System.EventHandler(this.rbOtherAbS_CheckedChanged);
            // 
            // chkOtherAb
            // 
            this.chkOtherAb.AutoSize = true;
            this.chkOtherAb.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkOtherAb.Location = new System.Drawing.Point(6, 2);
            this.chkOtherAb.Name = "chkOtherAb";
            this.chkOtherAb.Size = new System.Drawing.Size(286, 20);
            this.chkOtherAb.TabIndex = 0;
            this.chkOtherAb.Text = "สิ่งตรวจพบอื่นๆ / Other abnormalities detected";
            this.chkOtherAb.UseVisualStyleBackColor = true;
            this.chkOtherAb.CheckedChanged += new System.EventHandler(this.chkOtherAb_CheckedChanged);
            // 
            // rbOtherAbM
            // 
            this.rbOtherAbM.AutoSize = true;
            this.rbOtherAbM.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.rbOtherAbM.Location = new System.Drawing.Point(27, 54);
            this.rbOtherAbM.Name = "rbOtherAbM";
            this.rbOtherAbM.Size = new System.Drawing.Size(387, 20);
            this.rbOtherAbM.TabIndex = 2;
            this.rbOtherAbM.TabStop = true;
            this.rbOtherAbM.Tag = "M";
            this.rbOtherAbM.Text = "มีการอักเสบเล็กน้อย ถึงปานกลาง / Mid to moderate inflammation";
            this.rbOtherAbM.UseVisualStyleBackColor = true;
            this.rbOtherAbM.CheckedChanged += new System.EventHandler(this.rbOtherAbM_CheckedChanged);
            // 
            // rbOtherAbN
            // 
            this.rbOtherAbN.AutoSize = true;
            this.rbOtherAbN.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.rbOtherAbN.Location = new System.Drawing.Point(27, 28);
            this.rbOtherAbN.Name = "rbOtherAbN";
            this.rbOtherAbN.Size = new System.Drawing.Size(211, 20);
            this.rbOtherAbN.TabIndex = 1;
            this.rbOtherAbN.TabStop = true;
            this.rbOtherAbN.Tag = "N";
            this.rbOtherAbN.Text = "ไม่มีการอักเสบ / No inflammation";
            this.rbOtherAbN.UseVisualStyleBackColor = true;
            this.rbOtherAbN.CheckedChanged += new System.EventHandler(this.rbOtherAbN_CheckedChanged);
            // 
            // pnCellResult
            // 
            this.pnCellResult.Controls.Add(this.txtResultAbRemark);
            this.pnCellResult.Controls.Add(this.rbCellResultO);
            this.pnCellResult.Controls.Add(this.rbCellResultH);
            this.pnCellResult.Controls.Add(this.rbCellResultL);
            this.pnCellResult.Controls.Add(this.rbCellResultA);
            this.pnCellResult.Controls.Add(this.panel7);
            this.pnCellResult.Controls.Add(this.rbCellResultN);
            this.pnCellResult.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsObstetricsDoc, "obd_result", true));
            this.pnCellResult.AbleNull = false;
            this.pnCellResult.Location = new System.Drawing.Point(3, 32);
            this.pnCellResult.Name = "pnCellResult";
            this.pnCellResult.Size = new System.Drawing.Size(441, 164);
            this.pnCellResult.TabIndex = 10;
            // 
            // txtResultAbRemark
            // 
            this.txtResultAbRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtResultAbRemark.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsObstetricsDoc, "obd_result_ab_remark", true));
            this.txtResultAbRemark.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtResultAbRemark.Location = new System.Drawing.Point(123, 134);
            this.txtResultAbRemark.MaxLength = 100;
            this.txtResultAbRemark.Multiline = true;
            this.txtResultAbRemark.Name = "txtResultAbRemark";
            this.txtResultAbRemark.Size = new System.Drawing.Size(298, 23);
            this.txtResultAbRemark.TabIndex = 6;
            this.txtResultAbRemark.TextChanged += new System.EventHandler(this.txtResultAbRemark_TextChanged);
            // 
            // rbCellResultO
            // 
            this.rbCellResultO.AutoSize = true;
            this.rbCellResultO.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.rbCellResultO.Location = new System.Drawing.Point(27, 138);
            this.rbCellResultO.Name = "rbCellResultO";
            this.rbCellResultO.Size = new System.Drawing.Size(90, 20);
            this.rbCellResultO.TabIndex = 5;
            this.rbCellResultO.TabStop = true;
            this.rbCellResultO.Tag = "O";
            this.rbCellResultO.Text = "อื่นๆ Others";
            this.rbCellResultO.UseVisualStyleBackColor = true;
            this.rbCellResultO.CheckedChanged += new System.EventHandler(this.rbCellResultO_CheckedChanged);
            // 
            // rbCellResultH
            // 
            this.rbCellResultH.AutoSize = true;
            this.rbCellResultH.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.rbCellResultH.Location = new System.Drawing.Point(27, 112);
            this.rbCellResultH.Name = "rbCellResultH";
            this.rbCellResultH.Size = new System.Drawing.Size(384, 20);
            this.rbCellResultH.TabIndex = 4;
            this.rbCellResultH.TabStop = true;
            this.rbCellResultH.Tag = "H";
            this.rbCellResultH.Text = "ตรวจพบเซลล์ผิดปกติที่เป็นเกรดสูง / High - grade cellular atypical";
            this.rbCellResultH.UseVisualStyleBackColor = true;
            this.rbCellResultH.CheckedChanged += new System.EventHandler(this.rbCellResultH_CheckedChanged);
            // 
            // rbCellResultL
            // 
            this.rbCellResultL.AutoSize = true;
            this.rbCellResultL.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.rbCellResultL.Location = new System.Drawing.Point(27, 86);
            this.rbCellResultL.Name = "rbCellResultL";
            this.rbCellResultL.Size = new System.Drawing.Size(373, 20);
            this.rbCellResultL.TabIndex = 3;
            this.rbCellResultL.TabStop = true;
            this.rbCellResultL.Tag = "L";
            this.rbCellResultL.Text = "ตรวจพบเซลล์ผิดปกติที่เป็นเกรดต่ำ / Low grade cellular atypical";
            this.rbCellResultL.UseVisualStyleBackColor = true;
            this.rbCellResultL.CheckedChanged += new System.EventHandler(this.rbCellResultL_CheckedChanged);
            // 
            // rbCellResultA
            // 
            this.rbCellResultA.AutoSize = true;
            this.rbCellResultA.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.rbCellResultA.Location = new System.Drawing.Point(27, 60);
            this.rbCellResultA.Name = "rbCellResultA";
            this.rbCellResultA.Size = new System.Drawing.Size(395, 20);
            this.rbCellResultA.TabIndex = 2;
            this.rbCellResultA.TabStop = true;
            this.rbCellResultA.Tag = "A";
            this.rbCellResultA.Text = "มีเซลล์ผิดปกติเล็กน้อย / Atypical cell of undetermined significance";
            this.rbCellResultA.UseVisualStyleBackColor = true;
            this.rbCellResultA.CheckedChanged += new System.EventHandler(this.rbCellResultA_CheckedChanged);
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.rbCellDummy);
            this.panel7.Location = new System.Drawing.Point(3, 28);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(330, 26);
            this.panel7.TabIndex = 1;
            // 
            // rbCellDummy
            // 
            this.rbCellDummy.AutoSize = true;
            this.rbCellDummy.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.rbCellDummy.Location = new System.Drawing.Point(3, 2);
            this.rbCellDummy.Name = "rbCellDummy";
            this.rbCellDummy.Size = new System.Drawing.Size(322, 20);
            this.rbCellDummy.TabIndex = 1;
            this.rbCellDummy.TabStop = true;
            this.rbCellDummy.Text = "ตรวจพบเซลล์ผิดปกติ / Abnormal cells were detected";
            this.rbCellDummy.UseVisualStyleBackColor = true;
            this.rbCellDummy.CheckedChanged += new System.EventHandler(this.rbCellDummy_CheckedChanged);
            // 
            // rbCellResultN
            // 
            this.rbCellResultN.AutoSize = true;
            this.rbCellResultN.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.rbCellResultN.Location = new System.Drawing.Point(6, 2);
            this.rbCellResultN.Name = "rbCellResultN";
            this.rbCellResultN.Size = new System.Drawing.Size(328, 20);
            this.rbCellResultN.TabIndex = 0;
            this.rbCellResultN.TabStop = true;
            this.rbCellResultN.Tag = "N";
            this.rbCellResultN.Text = "ไม่พบเซลล์ผิดปกติ / No abnormal cells were detected";
            this.rbCellResultN.UseVisualStyleBackColor = true;
            this.rbCellResultN.CheckedChanged += new System.EventHandler(this.rbCellResultN_CheckedChanged);
            // 
            // chkHVPRec
            // 
            this.chkHVPRec.BindingType = UserControlLibrary.CheckBoxBinding.TypeData.Char;
            this.chkHVPRec.BindingValue = null;
            this.chkHVPRec.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkHVPRec.CheckedFalseValue = "N";
            this.chkHVPRec.CheckedTrueValue = "Y";
            this.chkHVPRec.DataBindings.Add(new System.Windows.Forms.Binding("BindingValue", this.bsObstetricsDoc, "obd_hpv_recommend", true));
            this.chkHVPRec.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkHVPRec.Location = new System.Drawing.Point(6, 100);
            this.chkHVPRec.Name = "chkHVPRec";
            this.chkHVPRec.Size = new System.Drawing.Size(440, 105);
            this.chkHVPRec.TabIndex = 12;
            this.chkHVPRec.Text = resources.GetString("chkHVPRec.Text");
            this.chkHVPRec.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkHVPRec.UseVisualStyleBackColor = true;
            // 
            // pnRecommend
            // 
            this.pnRecommend.Controls.Add(this.chkRec);
            this.pnRecommend.Controls.Add(this.rbRecFU);
            this.pnRecommend.Controls.Add(this.rbRecFR);
            this.pnRecommend.Controls.Add(this.txtRecRemark);
            this.pnRecommend.Controls.Add(this.rbRecFO);
            this.pnRecommend.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsObstetricsDoc, "obd_recommend", true));
            this.pnRecommend.AbleNull = false;
            this.pnRecommend.Location = new System.Drawing.Point(3, 32);
            this.pnRecommend.Name = "pnRecommend";
            this.pnRecommend.Size = new System.Drawing.Size(428, 145);
            this.pnRecommend.TabIndex = 12;
            // 
            // chkRec
            // 
            this.chkRec.AutoSize = true;
            this.chkRec.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkRec.Location = new System.Drawing.Point(3, 3);
            this.chkRec.Name = "chkRec";
            this.chkRec.Size = new System.Drawing.Size(385, 20);
            this.chkRec.TabIndex = 0;
            this.chkRec.Text = "ควรปรึกษาสูตินรีแพทย์ / Physician consultation is recommended";
            this.chkRec.UseVisualStyleBackColor = true;
            this.chkRec.CheckedChanged += new System.EventHandler(this.chkRec_CheckedChanged);
            // 
            // rbRecFU
            // 
            this.rbRecFU.AutoSize = true;
            this.rbRecFU.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.rbRecFU.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.rbRecFU.Location = new System.Drawing.Point(25, 71);
            this.rbRecFU.Name = "rbRecFU";
            this.rbRecFU.Size = new System.Drawing.Size(375, 36);
            this.rbRecFU.TabIndex = 2;
            this.rbRecFU.TabStop = true;
            this.rbRecFU.Tag = "FU";
            this.rbRecFU.Text = "พบสูตินรีแพทย์โดยเร็ว เพื่อทำการตรวจวินิจฉัยและรักษาเพิ่มเติม / \r\nFurther investi" +
                "gation is urgently required";
            this.rbRecFU.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.rbRecFU.UseVisualStyleBackColor = true;
            this.rbRecFU.CheckedChanged += new System.EventHandler(this.rbRecFU_CheckedChanged);
            // 
            // rbRecFR
            // 
            this.rbRecFR.AutoSize = true;
            this.rbRecFR.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.rbRecFR.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.rbRecFR.Location = new System.Drawing.Point(25, 29);
            this.rbRecFR.Name = "rbRecFR";
            this.rbRecFR.Size = new System.Drawing.Size(281, 36);
            this.rbRecFR.TabIndex = 1;
            this.rbRecFR.TabStop = true;
            this.rbRecFR.Tag = "FR";
            this.rbRecFR.Text = "พบสูตินรีแพทย์เพื่อทำการตรวจวินิจฉัยเพิ่มเติม / \r\nFurther investigation is requir" +
                "ed";
            this.rbRecFR.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.rbRecFR.UseVisualStyleBackColor = true;
            this.rbRecFR.CheckedChanged += new System.EventHandler(this.rbRecFR_CheckedChanged);
            // 
            // txtRecRemark
            // 
            this.txtRecRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRecRemark.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsObstetricsDoc, "obd_recommend_remark", true));
            this.txtRecRemark.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtRecRemark.Location = new System.Drawing.Point(121, 109);
            this.txtRecRemark.MaxLength = 100;
            this.txtRecRemark.Multiline = true;
            this.txtRecRemark.Name = "txtRecRemark";
            this.txtRecRemark.Size = new System.Drawing.Size(295, 23);
            this.txtRecRemark.TabIndex = 4;
            this.txtRecRemark.TextChanged += new System.EventHandler(this.txtRecRemark_TextChanged);
            // 
            // rbRecFO
            // 
            this.rbRecFO.AutoSize = true;
            this.rbRecFO.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.rbRecFO.Location = new System.Drawing.Point(25, 113);
            this.rbRecFO.Name = "rbRecFO";
            this.rbRecFO.Size = new System.Drawing.Size(90, 20);
            this.rbRecFO.TabIndex = 3;
            this.rbRecFO.TabStop = true;
            this.rbRecFO.Tag = "FO";
            this.rbRecFO.Text = "อื่นๆ Others";
            this.rbRecFO.UseVisualStyleBackColor = true;
            this.rbRecFO.CheckedChanged += new System.EventHandler(this.rbRecFO_CheckedChanged);
            // 
            // pnRecOther
            // 
            this.pnRecOther.Controls.Add(this.rbRecOtherTS);
            this.pnRecOther.Controls.Add(this.rbRecOtherTR);
            this.pnRecOther.Controls.Add(this.rbRecOtherNT);
            this.pnRecOther.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsObstetricsDoc, "obd_recommend_other", true));
            this.pnRecOther.AbleNull = false;
            this.pnRecOther.Location = new System.Drawing.Point(3, 202);
            this.pnRecOther.Name = "pnRecOther";
            this.pnRecOther.Size = new System.Drawing.Size(428, 162);
            this.pnRecOther.TabIndex = 3;
            // 
            // rbRecOtherTS
            // 
            this.rbRecOtherTS.AutoSize = true;
            this.rbRecOtherTS.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.rbRecOtherTS.Location = new System.Drawing.Point(3, 124);
            this.rbRecOtherTS.Name = "rbRecOtherTS";
            this.rbRecOtherTS.Size = new System.Drawing.Size(297, 20);
            this.rbRecOtherTS.TabIndex = 2;
            this.rbRecOtherTS.TabStop = true;
            this.rbRecOtherTS.Tag = "TS";
            this.rbRecOtherTS.Text = "ควรพบแพทย์รับยารักษา / Treatment is required.";
            this.rbRecOtherTS.UseVisualStyleBackColor = true;
            // 
            // rbRecOtherTR
            // 
            this.rbRecOtherTR.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.rbRecOtherTR.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.rbRecOtherTR.Location = new System.Drawing.Point(3, 80);
            this.rbRecOtherTR.Name = "rbRecOtherTR";
            this.rbRecOtherTR.Size = new System.Drawing.Size(286, 36);
            this.rbRecOtherTR.TabIndex = 1;
            this.rbRecOtherTR.TabStop = true;
            this.rbRecOtherTR.Tag = "TR";
            this.rbRecOtherTR.Text = "ถ้ามีอาการผิดปกติให้มาพบสูตินรีแพทย์ / Treatment is required if symptoms exist.";
            this.rbRecOtherTR.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.rbRecOtherTR.UseVisualStyleBackColor = true;
            // 
            // rbRecOtherNT
            // 
            this.rbRecOtherNT.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.rbRecOtherNT.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.rbRecOtherNT.Location = new System.Drawing.Point(3, 1);
            this.rbRecOtherNT.Name = "rbRecOtherNT";
            this.rbRecOtherNT.Size = new System.Drawing.Size(411, 73);
            this.rbRecOtherNT.TabIndex = 0;
            this.rbRecOtherNT.TabStop = true;
            this.rbRecOtherNT.Tag = "NT";
            this.rbRecOtherNT.Text = resources.GetString("rbRecOtherNT.Text");
            this.rbRecOtherNT.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.rbRecOtherNT.UseVisualStyleBackColor = true;
            // 
            // pnHPVTest
            // 
            this.pnHPVTest.Controls.Add(this.rbHPVTestNT);
            this.pnHPVTest.Controls.Add(this.txtHPVRemark);
            this.pnHPVTest.Controls.Add(this.rbHPVTestNH);
            this.pnHPVTest.Controls.Add(this.rbHPVTestHR);
            this.pnHPVTest.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsObstetricsDoc, "obd_hpv_test", true));
            this.pnHPVTest.AbleNull = false;
            this.pnHPVTest.Location = new System.Drawing.Point(3, 33);
            this.pnHPVTest.Name = "pnHPVTest";
            this.pnHPVTest.Size = new System.Drawing.Size(881, 79);
            this.pnHPVTest.TabIndex = 14;
            // 
            // rbHPVTestNT
            // 
            this.rbHPVTestNT.AutoSize = true;
            this.rbHPVTestNT.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.rbHPVTestNT.Location = new System.Drawing.Point(7, 2);
            this.rbHPVTestNT.Name = "rbHPVTestNT";
            this.rbHPVTestNT.Size = new System.Drawing.Size(201, 20);
            this.rbHPVTestNT.TabIndex = 0;
            this.rbHPVTestNT.TabStop = true;
            this.rbHPVTestNT.Tag = "NT";
            this.rbHPVTestNT.Text = "ไม่ได้ตรวจ / Did not do the test";
            this.rbHPVTestNT.UseVisualStyleBackColor = true;
            // 
            // txtHPVRemark
            // 
            this.txtHPVRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHPVRemark.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsObstetricsDoc, "obd_hpv_test_remark", true));
            this.txtHPVRemark.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtHPVRemark.Location = new System.Drawing.Point(315, 51);
            this.txtHPVRemark.MaxLength = 100;
            this.txtHPVRemark.Multiline = true;
            this.txtHPVRemark.Name = "txtHPVRemark";
            this.txtHPVRemark.Size = new System.Drawing.Size(539, 23);
            this.txtHPVRemark.TabIndex = 3;
            this.txtHPVRemark.TextChanged += new System.EventHandler(this.txtHPVRemark_TextChanged);
            // 
            // rbHPVTestNH
            // 
            this.rbHPVTestNH.AutoSize = true;
            this.rbHPVTestNH.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.rbHPVTestNH.Location = new System.Drawing.Point(7, 28);
            this.rbHPVTestNH.Name = "rbHPVTestNH";
            this.rbHPVTestNH.Size = new System.Drawing.Size(393, 20);
            this.rbHPVTestNH.TabIndex = 1;
            this.rbHPVTestNH.TabStop = true;
            this.rbHPVTestNH.Tag = "NH";
            this.rbHPVTestNH.Text = "ตรวจไม่พบไวรัสกลุ่มเสียง / No high risk groups of HPV is detected";
            this.rbHPVTestNH.UseVisualStyleBackColor = true;
            // 
            // rbHPVTestHR
            // 
            this.rbHPVTestHR.AutoSize = true;
            this.rbHPVTestHR.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.rbHPVTestHR.Location = new System.Drawing.Point(7, 54);
            this.rbHPVTestHR.Name = "rbHPVTestHR";
            this.rbHPVTestHR.Size = new System.Drawing.Size(302, 20);
            this.rbHPVTestHR.TabIndex = 2;
            this.rbHPVTestHR.TabStop = true;
            this.rbHPVTestHR.Tag = "HR";
            this.rbHPVTestHR.Text = "ตรวจพบไวรัสกลุ่มเสี่ยง / High risk HPV is detected";
            this.rbHPVTestHR.UseVisualStyleBackColor = true;
            this.rbHPVTestHR.CheckedChanged += new System.EventHandler(this.rbHPVTestHR_CheckedChanged);
            // 
            // PapResultUC
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(897, 730);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "PapResultUC";
            this.Size = new System.Drawing.Size(897, 730);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsObstetricsDoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPatientRegis)).EndInit();
            this.panel1.ResumeLayout(false);
            this.pnOtherAb.ResumeLayout(false);
            this.pnOtherAb.PerformLayout();
            this.pnOther.ResumeLayout(false);
            this.pnOther.PerformLayout();
            this.pnCellResult.ResumeLayout(false);
            this.pnCellResult.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.pnRecommend.ResumeLayout(false);
            this.pnRecommend.PerformLayout();
            this.pnRecOther.ResumeLayout(false);
            this.pnRecOther.PerformLayout();
            this.pnHPVTest.ResumeLayout(false);
            this.pnHPVTest.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox groupBox1;
        private UserControlLibrary.PanelRadioBinding pnCellResult;
        private System.Windows.Forms.TextBox txtResultAbRemark;
        private System.Windows.Forms.RadioButton rbCellResultO;
        private System.Windows.Forms.RadioButton rbCellResultH;
        private System.Windows.Forms.RadioButton rbCellResultL;
        private System.Windows.Forms.RadioButton rbCellResultA;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.RadioButton rbCellDummy;
        private System.Windows.Forms.RadioButton rbCellResultN;
        private System.Windows.Forms.GroupBox groupBox2;
        private UserControlLibrary.PanelRadioBinding pnRecommend;
        private System.Windows.Forms.CheckBox chkRec;
        private System.Windows.Forms.RadioButton rbRecFU;
        private System.Windows.Forms.RadioButton rbRecFR;
        private System.Windows.Forms.TextBox txtRecRemark;
        private System.Windows.Forms.RadioButton rbRecFO;
        private UserControlLibrary.PanelRadioBinding pnOtherAb;
        private System.Windows.Forms.Panel pnOther;
        private UserControlLibrary.CheckBoxBinding chkOtherAbFungus;
        private System.Windows.Forms.TextBox txtOtherAbRemark;
        private UserControlLibrary.CheckBoxBinding chkOtherAbBateria;
        private UserControlLibrary.CheckBoxBinding chkOtherAbOther;
        private System.Windows.Forms.RadioButton rbOtherAbS;
        private System.Windows.Forms.CheckBox chkOtherAb;
        private System.Windows.Forms.RadioButton rbOtherAbM;
        private System.Windows.Forms.RadioButton rbOtherAbN;
        private UserControlLibrary.PanelRadioBinding pnRecOther;
        private System.Windows.Forms.RadioButton rbRecOtherTS;
        private System.Windows.Forms.RadioButton rbRecOtherTR;
        private System.Windows.Forms.RadioButton rbRecOtherNT;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label15;
        private UserControlLibrary.PanelRadioBinding pnHPVTest;
        private System.Windows.Forms.RadioButton rbHPVTestNT;
        private System.Windows.Forms.TextBox txtHPVRemark;
        private System.Windows.Forms.RadioButton rbHPVTestNH;
        private System.Windows.Forms.RadioButton rbHPVTestHR;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox5;
        private UserControlLibrary.CheckBoxBinding chkHVPRec;
        private UserControlLibrary.ComboAutoDropDownWidth DDsuggustion;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtOther;
        private System.Windows.Forms.BindingSource bsPatientRegis;
        private System.Windows.Forms.BindingSource bsObstetricsDoc;
        private System.Windows.Forms.Panel panel1;
    }
}
