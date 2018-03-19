namespace BKvs2010.UserControlEMR
{
    partial class ExamCheckupUC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExamCheckupUC));
            this.panel15 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDefault = new System.Windows.Forms.Button();
            this.OtherResult = new BKvs2010.Usercontrols.ExamResultUI();
            this.bsDoctorCheckup = new System.Windows.Forms.BindingSource(this.components);
            this.bsDoctorHdr = new System.Windows.Forms.BindingSource(this.components);
            this.bsPatientRegis = new System.Windows.Forms.BindingSource(this.components);
            this.GAResult = new BKvs2010.Usercontrols.ExamResultUI();
            this.HEENTResult = new BKvs2010.Usercontrols.ExamResultUI();
            this.THROATResult = new BKvs2010.Usercontrols.ExamResultUI();
            this.AbdomenResult = new BKvs2010.Usercontrols.ExamResultUI();
            this.NOSEResult = new BKvs2010.Usercontrols.ExamResultUI();
            this.ChestResult = new BKvs2010.Usercontrols.ExamResultUI();
            this.HEADResult = new BKvs2010.Usercontrols.ExamResultUI();
            this.CVSResult = new BKvs2010.Usercontrols.ExamResultUI();
            this.EARSResult = new BKvs2010.Usercontrols.ExamResultUI();
            this.NeuroResult = new BKvs2010.Usercontrols.ExamResultUI();
            this.EYESResult = new BKvs2010.Usercontrols.ExamResultUI();
            this.ExtremitiesResult = new BKvs2010.Usercontrols.ExamResultUI();
            this.panel15.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsDoctorCheckup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsDoctorHdr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPatientRegis)).BeginInit();
            this.SuspendLayout();
            // 
            // panel15
            // 
            this.panel15.AutoScroll = true;
            this.panel15.AutoScrollMinSize = new System.Drawing.Size(973, 494);
            this.panel15.Controls.Add(this.panel1);
            this.panel15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel15.Location = new System.Drawing.Point(0, 0);
            this.panel15.Margin = new System.Windows.Forms.Padding(0);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(973, 494);
            this.panel15.TabIndex = 47;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDefault);
            this.panel1.Controls.Add(this.OtherResult);
            this.panel1.Controls.Add(this.GAResult);
            this.panel1.Controls.Add(this.HEENTResult);
            this.panel1.Controls.Add(this.THROATResult);
            this.panel1.Controls.Add(this.AbdomenResult);
            this.panel1.Controls.Add(this.NOSEResult);
            this.panel1.Controls.Add(this.ChestResult);
            this.panel1.Controls.Add(this.HEADResult);
            this.panel1.Controls.Add(this.CVSResult);
            this.panel1.Controls.Add(this.EARSResult);
            this.panel1.Controls.Add(this.NeuroResult);
            this.panel1.Controls.Add(this.EYESResult);
            this.panel1.Controls.Add(this.ExtremitiesResult);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(973, 494);
            this.panel1.TabIndex = 58;
            // 
            // btnDefault
            // 
            this.btnDefault.Location = new System.Drawing.Point(840, 6);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(126, 23);
            this.btnDefault.TabIndex = 1476;
            this.btnDefault.Text = "DEFAULT NORMAL";
            this.btnDefault.UseVisualStyleBackColor = true;
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // OtherResult
            // 
            this.OtherResult.AutoCompleteListThList = ((System.Collections.Generic.List<string>)(resources.GetObject("OtherResult.AutoCompleteListThList")));
            this.OtherResult.AutoCompleteType = null;
            this.OtherResult.DataBindings.Add(new System.Windows.Forms.Binding("ResultTH", this.bsDoctorCheckup, "trcp_other_remark", true));
            this.OtherResult.DataBindings.Add(new System.Windows.Forms.Binding("SummaryFlag", this.bsDoctorCheckup, "trcp_other", true));
            this.OtherResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.OtherResult.LableBackGroundColor = System.Drawing.Color.LightGreen;
            this.OtherResult.LableText = "Other";
            this.OtherResult.Language = BKvs2010.Language.TH;
            this.OtherResult.Location = new System.Drawing.Point(3, 424);
            this.OtherResult.Name = "OtherResult";
            this.OtherResult.ReadOnly = false;
            this.OtherResult.ResultTH = "";
            this.OtherResult.Size = new System.Drawing.Size(480, 66);
            this.OtherResult.SummaryFlag = null;
            this.OtherResult.TabIndex = 53;
            // 
            // bsDoctorCheckup
            // 
            this.bsDoctorCheckup.DataMember = "trn_doctor_checkups";
            this.bsDoctorCheckup.DataSource = this.bsDoctorHdr;
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
            // GAResult
            // 
            this.GAResult.AutoCompleteListThList = ((System.Collections.Generic.List<string>)(resources.GetObject("GAResult.AutoCompleteListThList")));
            this.GAResult.AutoCompleteType = null;
            this.GAResult.DataBindings.Add(new System.Windows.Forms.Binding("ResultTH", this.bsDoctorCheckup, "trcp_ga_remark", true));
            this.GAResult.DataBindings.Add(new System.Windows.Forms.Binding("SummaryFlag", this.bsDoctorCheckup, "trcp_ga", true));
            this.GAResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.GAResult.LableBackGroundColor = System.Drawing.Color.LightGreen;
            this.GAResult.LableText = "General Appearance";
            this.GAResult.Language = BKvs2010.Language.TH;
            this.GAResult.Location = new System.Drawing.Point(3, 3);
            this.GAResult.Name = "GAResult";
            this.GAResult.ReadOnly = false;
            this.GAResult.ResultTH = "";
            this.GAResult.Size = new System.Drawing.Size(480, 66);
            this.GAResult.SummaryFlag = null;
            this.GAResult.TabIndex = 46;
            // 
            // HEENTResult
            // 
            this.HEENTResult.AutoCompleteListThList = ((System.Collections.Generic.List<string>)(resources.GetObject("HEENTResult.AutoCompleteListThList")));
            this.HEENTResult.AutoCompleteType = null;
            this.HEENTResult.DataBindings.Add(new System.Windows.Forms.Binding("SummaryFlag", this.bsDoctorCheckup, "trcp_heent", true));
            this.HEENTResult.DataBindings.Add(new System.Windows.Forms.Binding("ResultTH", this.bsDoctorCheckup, "trcp_heent_remark", true));
            this.HEENTResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.HEENTResult.LableBackGroundColor = System.Drawing.Color.LightGreen;
            this.HEENTResult.LableText = "HEENT";
            this.HEENTResult.Language = BKvs2010.Language.TH;
            this.HEENTResult.Location = new System.Drawing.Point(494, 41);
            this.HEENTResult.Name = "HEENTResult";
            this.HEENTResult.ReadOnly = true;
            this.HEENTResult.ResultTH = "";
            this.HEENTResult.Size = new System.Drawing.Size(470, 66);
            this.HEENTResult.SummaryFlag = null;
            this.HEENTResult.TabIndex = 54;
            // 
            // THROATResult
            // 
            this.THROATResult.AutoCompleteListThList = ((System.Collections.Generic.List<string>)(resources.GetObject("THROATResult.AutoCompleteListThList")));
            this.THROATResult.AutoCompleteType = null;
            this.THROATResult.DataBindings.Add(new System.Windows.Forms.Binding("ResultTH", this.bsDoctorCheckup, "trcp_heent_throat", true));
            this.THROATResult.DataBindings.Add(new System.Windows.Forms.Binding("SummaryFlag", this.bsDoctorCheckup, "trcp_h_throat", true));
            this.THROATResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.THROATResult.LableBackGroundColor = System.Drawing.Color.Aquamarine;
            this.THROATResult.LableText = "THROAT";
            this.THROATResult.Language = BKvs2010.Language.TH;
            this.THROATResult.Location = new System.Drawing.Point(534, 391);
            this.THROATResult.Name = "THROATResult";
            this.THROATResult.ReadOnly = false;
            this.THROATResult.ResultTH = "";
            this.THROATResult.Size = new System.Drawing.Size(430, 66);
            this.THROATResult.SummaryFlag = null;
            this.THROATResult.TabIndex = 57;
            // 
            // AbdomenResult
            // 
            this.AbdomenResult.AutoCompleteListThList = ((System.Collections.Generic.List<string>)(resources.GetObject("AbdomenResult.AutoCompleteListThList")));
            this.AbdomenResult.AutoCompleteType = null;
            this.AbdomenResult.DataBindings.Add(new System.Windows.Forms.Binding("SummaryFlag", this.bsDoctorCheckup, "trcp_abdomen", true));
            this.AbdomenResult.DataBindings.Add(new System.Windows.Forms.Binding("ResultTH", this.bsDoctorCheckup, "trcp_abdomen_remark", true));
            this.AbdomenResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.AbdomenResult.LableBackGroundColor = System.Drawing.Color.LightGreen;
            this.AbdomenResult.LableText = "Abdomen";
            this.AbdomenResult.Language = BKvs2010.Language.TH;
            this.AbdomenResult.Location = new System.Drawing.Point(3, 213);
            this.AbdomenResult.Name = "AbdomenResult";
            this.AbdomenResult.ReadOnly = false;
            this.AbdomenResult.ResultTH = "";
            this.AbdomenResult.Size = new System.Drawing.Size(480, 66);
            this.AbdomenResult.SummaryFlag = null;
            this.AbdomenResult.TabIndex = 47;
            // 
            // NOSEResult
            // 
            this.NOSEResult.AutoCompleteListThList = ((System.Collections.Generic.List<string>)(resources.GetObject("NOSEResult.AutoCompleteListThList")));
            this.NOSEResult.AutoCompleteType = null;
            this.NOSEResult.DataBindings.Add(new System.Windows.Forms.Binding("SummaryFlag", this.bsDoctorCheckup, "trcp_h_nose", true));
            this.NOSEResult.DataBindings.Add(new System.Windows.Forms.Binding("ResultTH", this.bsDoctorCheckup, "trcp_heent_nose", true));
            this.NOSEResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.NOSEResult.LableBackGroundColor = System.Drawing.Color.Aquamarine;
            this.NOSEResult.LableText = "NOSE";
            this.NOSEResult.Language = BKvs2010.Language.TH;
            this.NOSEResult.Location = new System.Drawing.Point(534, 321);
            this.NOSEResult.Name = "NOSEResult";
            this.NOSEResult.ReadOnly = false;
            this.NOSEResult.ResultTH = "";
            this.NOSEResult.Size = new System.Drawing.Size(430, 66);
            this.NOSEResult.SummaryFlag = null;
            this.NOSEResult.TabIndex = 56;
            // 
            // ChestResult
            // 
            this.ChestResult.AutoCompleteListThList = ((System.Collections.Generic.List<string>)(resources.GetObject("ChestResult.AutoCompleteListThList")));
            this.ChestResult.AutoCompleteType = null;
            this.ChestResult.DataBindings.Add(new System.Windows.Forms.Binding("ResultTH", this.bsDoctorCheckup, "trcp_chest_remark", true));
            this.ChestResult.DataBindings.Add(new System.Windows.Forms.Binding("SummaryFlag", this.bsDoctorCheckup, "trcp_chest", true));
            this.ChestResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.ChestResult.LableBackGroundColor = System.Drawing.Color.LightGreen;
            this.ChestResult.LableText = "Chest";
            this.ChestResult.Language = BKvs2010.Language.TH;
            this.ChestResult.Location = new System.Drawing.Point(3, 73);
            this.ChestResult.Name = "ChestResult";
            this.ChestResult.ReadOnly = false;
            this.ChestResult.ResultTH = "";
            this.ChestResult.Size = new System.Drawing.Size(480, 66);
            this.ChestResult.SummaryFlag = null;
            this.ChestResult.TabIndex = 48;
            // 
            // HEADResult
            // 
            this.HEADResult.AutoCompleteListThList = ((System.Collections.Generic.List<string>)(resources.GetObject("HEADResult.AutoCompleteListThList")));
            this.HEADResult.AutoCompleteType = null;
            this.HEADResult.DataBindings.Add(new System.Windows.Forms.Binding("ResultTH", this.bsDoctorCheckup, "trcp_heent_head", true));
            this.HEADResult.DataBindings.Add(new System.Windows.Forms.Binding("SummaryFlag", this.bsDoctorCheckup, "trcp_h_head", true));
            this.HEADResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.HEADResult.LableBackGroundColor = System.Drawing.Color.Aquamarine;
            this.HEADResult.LableText = "HEAD";
            this.HEADResult.Language = BKvs2010.Language.TH;
            this.HEADResult.Location = new System.Drawing.Point(534, 111);
            this.HEADResult.Name = "HEADResult";
            this.HEADResult.ReadOnly = false;
            this.HEADResult.ResultTH = "";
            this.HEADResult.Size = new System.Drawing.Size(430, 66);
            this.HEADResult.SummaryFlag = null;
            this.HEADResult.TabIndex = 53;
            // 
            // CVSResult
            // 
            this.CVSResult.AutoCompleteListThList = ((System.Collections.Generic.List<string>)(resources.GetObject("CVSResult.AutoCompleteListThList")));
            this.CVSResult.AutoCompleteType = null;
            this.CVSResult.DataBindings.Add(new System.Windows.Forms.Binding("SummaryFlag", this.bsDoctorCheckup, "trcp_cvs", true));
            this.CVSResult.DataBindings.Add(new System.Windows.Forms.Binding("ResultTH", this.bsDoctorCheckup, "trcp_cvs_remark", true));
            this.CVSResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.CVSResult.LableBackGroundColor = System.Drawing.Color.LightGreen;
            this.CVSResult.LableText = "Cardiovascular System";
            this.CVSResult.Language = BKvs2010.Language.TH;
            this.CVSResult.Location = new System.Drawing.Point(3, 143);
            this.CVSResult.Name = "CVSResult";
            this.CVSResult.ReadOnly = false;
            this.CVSResult.ResultTH = "";
            this.CVSResult.Size = new System.Drawing.Size(480, 66);
            this.CVSResult.SummaryFlag = null;
            this.CVSResult.TabIndex = 49;
            // 
            // EARSResult
            // 
            this.EARSResult.AutoCompleteListThList = ((System.Collections.Generic.List<string>)(resources.GetObject("EARSResult.AutoCompleteListThList")));
            this.EARSResult.AutoCompleteType = null;
            this.EARSResult.DataBindings.Add(new System.Windows.Forms.Binding("ResultTH", this.bsDoctorCheckup, "trcp_heent_ears", true));
            this.EARSResult.DataBindings.Add(new System.Windows.Forms.Binding("SummaryFlag", this.bsDoctorCheckup, "trcp_h_ears", true));
            this.EARSResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.EARSResult.LableBackGroundColor = System.Drawing.Color.Aquamarine;
            this.EARSResult.LableText = "EARS";
            this.EARSResult.Language = BKvs2010.Language.TH;
            this.EARSResult.Location = new System.Drawing.Point(534, 251);
            this.EARSResult.Name = "EARSResult";
            this.EARSResult.ReadOnly = false;
            this.EARSResult.ResultTH = "";
            this.EARSResult.Size = new System.Drawing.Size(430, 66);
            this.EARSResult.SummaryFlag = null;
            this.EARSResult.TabIndex = 55;
            // 
            // NeuroResult
            // 
            this.NeuroResult.AutoCompleteListThList = ((System.Collections.Generic.List<string>)(resources.GetObject("NeuroResult.AutoCompleteListThList")));
            this.NeuroResult.AutoCompleteType = null;
            this.NeuroResult.DataBindings.Add(new System.Windows.Forms.Binding("SummaryFlag", this.bsDoctorCheckup, "trcp_neuro", true));
            this.NeuroResult.DataBindings.Add(new System.Windows.Forms.Binding("ResultTH", this.bsDoctorCheckup, "trcp_neuro_remark", true));
            this.NeuroResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.NeuroResult.LableBackGroundColor = System.Drawing.Color.LightGreen;
            this.NeuroResult.LableText = "Neuro";
            this.NeuroResult.Language = BKvs2010.Language.TH;
            this.NeuroResult.Location = new System.Drawing.Point(3, 353);
            this.NeuroResult.Name = "NeuroResult";
            this.NeuroResult.ReadOnly = false;
            this.NeuroResult.ResultTH = "";
            this.NeuroResult.Size = new System.Drawing.Size(480, 66);
            this.NeuroResult.SummaryFlag = null;
            this.NeuroResult.TabIndex = 51;
            // 
            // EYESResult
            // 
            this.EYESResult.AutoCompleteListThList = ((System.Collections.Generic.List<string>)(resources.GetObject("EYESResult.AutoCompleteListThList")));
            this.EYESResult.AutoCompleteType = null;
            this.EYESResult.DataBindings.Add(new System.Windows.Forms.Binding("SummaryFlag", this.bsDoctorCheckup, "trcp_h_eyes", true));
            this.EYESResult.DataBindings.Add(new System.Windows.Forms.Binding("ResultTH", this.bsDoctorCheckup, "trcp_heent_eyes", true));
            this.EYESResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.EYESResult.LableBackGroundColor = System.Drawing.Color.Aquamarine;
            this.EYESResult.LableText = "EYES";
            this.EYESResult.Language = BKvs2010.Language.TH;
            this.EYESResult.Location = new System.Drawing.Point(534, 181);
            this.EYESResult.Name = "EYESResult";
            this.EYESResult.ReadOnly = false;
            this.EYESResult.ResultTH = "";
            this.EYESResult.Size = new System.Drawing.Size(430, 66);
            this.EYESResult.SummaryFlag = null;
            this.EYESResult.TabIndex = 54;
            // 
            // ExtremitiesResult
            // 
            this.ExtremitiesResult.AutoCompleteListThList = ((System.Collections.Generic.List<string>)(resources.GetObject("ExtremitiesResult.AutoCompleteListThList")));
            this.ExtremitiesResult.AutoCompleteType = null;
            this.ExtremitiesResult.DataBindings.Add(new System.Windows.Forms.Binding("ResultTH", this.bsDoctorCheckup, "trcp_ext_remark", true));
            this.ExtremitiesResult.DataBindings.Add(new System.Windows.Forms.Binding("SummaryFlag", this.bsDoctorCheckup, "trcp_extremities", true));
            this.ExtremitiesResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.ExtremitiesResult.LableBackGroundColor = System.Drawing.Color.LightGreen;
            this.ExtremitiesResult.LableText = "Extremities";
            this.ExtremitiesResult.Language = BKvs2010.Language.TH;
            this.ExtremitiesResult.Location = new System.Drawing.Point(3, 283);
            this.ExtremitiesResult.Name = "ExtremitiesResult";
            this.ExtremitiesResult.ReadOnly = false;
            this.ExtremitiesResult.ResultTH = "";
            this.ExtremitiesResult.Size = new System.Drawing.Size(480, 66);
            this.ExtremitiesResult.SummaryFlag = null;
            this.ExtremitiesResult.TabIndex = 50;
            // 
            // ExamCheckupUC
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.panel15);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ExamCheckupUC";
            this.Size = new System.Drawing.Size(973, 494);
            this.panel15.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bsDoctorCheckup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsDoctorHdr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPatientRegis)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel15;
        private Usercontrols.ExamResultUI GAResult;
        private Usercontrols.ExamResultUI NeuroResult;
        private Usercontrols.ExamResultUI ExtremitiesResult;
        private Usercontrols.ExamResultUI CVSResult;
        private Usercontrols.ExamResultUI ChestResult;
        private Usercontrols.ExamResultUI AbdomenResult;
        private Usercontrols.ExamResultUI OtherResult;
        private System.Windows.Forms.BindingSource bsPatientRegis;
        private System.Windows.Forms.BindingSource bsDoctorCheckup;
        private System.Windows.Forms.BindingSource bsDoctorHdr;
        private Usercontrols.ExamResultUI THROATResult;
        private Usercontrols.ExamResultUI HEENTResult;
        private Usercontrols.ExamResultUI NOSEResult;
        private Usercontrols.ExamResultUI HEADResult;
        private Usercontrols.ExamResultUI EARSResult;
        private Usercontrols.ExamResultUI EYESResult;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDefault;
    }
}
