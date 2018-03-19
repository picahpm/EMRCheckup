namespace BKvs2010.UserControlEMR
{
    partial class ExamOccMedUC
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
            this.panel15 = new System.Windows.Forms.Panel();
            this.HEENTResult = new BKvs2010.Usercontrols.ExamResultUI();
            this.bsDoctorOccMed = new System.Windows.Forms.BindingSource(this.components);
            this.bsPatientRegis = new System.Windows.Forms.BindingSource(this.components);
            this.THROATResult = new BKvs2010.Usercontrols.ExamResultUI();
            this.NOSEResult = new BKvs2010.Usercontrols.ExamResultUI();
            this.HEADResult = new BKvs2010.Usercontrols.ExamResultUI();
            this.EARSResult = new BKvs2010.Usercontrols.ExamResultUI();
            this.EYESResult = new BKvs2010.Usercontrols.ExamResultUI();
            this.NeuroResult = new BKvs2010.Usercontrols.ExamResultUI();
            this.CVSResult = new BKvs2010.Usercontrols.ExamResultUI();
            this.ChestResult = new BKvs2010.Usercontrols.ExamResultUI();
            this.AbdomenResult = new BKvs2010.Usercontrols.ExamResultUI();
            this.GAResult = new BKvs2010.Usercontrols.ExamResultUI();
            this.tblPanalOccMed = new System.Windows.Forms.TableLayoutPanel();
            this.panel12 = new System.Windows.Forms.Panel();
            this.OtherResult = new BKvs2010.Usercontrols.ExamResultUI();
            this.panalMusculos = new System.Windows.Forms.Panel();
            this.MusculoskeletonResult = new BKvs2010.Usercontrols.ExamResultUI();
            this.panel15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsDoctorOccMed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPatientRegis)).BeginInit();
            this.tblPanalOccMed.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panalMusculos.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel15
            // 
            this.panel15.AutoScroll = true;
            this.panel15.Controls.Add(this.HEENTResult);
            this.panel15.Controls.Add(this.THROATResult);
            this.panel15.Controls.Add(this.NOSEResult);
            this.panel15.Controls.Add(this.HEADResult);
            this.panel15.Controls.Add(this.EARSResult);
            this.panel15.Controls.Add(this.EYESResult);
            this.panel15.Controls.Add(this.NeuroResult);
            this.panel15.Controls.Add(this.CVSResult);
            this.panel15.Controls.Add(this.ChestResult);
            this.panel15.Controls.Add(this.AbdomenResult);
            this.panel15.Controls.Add(this.GAResult);
            this.panel15.Controls.Add(this.tblPanalOccMed);
            this.panel15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel15.Location = new System.Drawing.Point(0, 0);
            this.panel15.Margin = new System.Windows.Forms.Padding(0);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(974, 566);
            this.panel15.TabIndex = 47;
            // 
            // HEENTResult
            // 
            this.HEENTResult.AutoCompleteListThList = null;
            this.HEENTResult.AutoCompleteType = null;
            this.HEENTResult.DataBindings.Add(new System.Windows.Forms.Binding("ResultTH", this.bsDoctorOccMed, "tdom_heent_remark", true));
            this.HEENTResult.DataBindings.Add(new System.Windows.Forms.Binding("SummaryFlag", this.bsDoctorOccMed, "tdom_heent", true));
            this.HEENTResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.HEENTResult.LableBackGroundColor = System.Drawing.Color.LightGreen;
            this.HEENTResult.LableText = "HEENT";
            this.HEENTResult.Language = BKvs2010.Language.TH;
            this.HEENTResult.Location = new System.Drawing.Point(496, 10);
            this.HEENTResult.Name = "HEENTResult";
            this.HEENTResult.ReadOnly = true;
            this.HEENTResult.ResultTH = "";
            this.HEENTResult.Size = new System.Drawing.Size(470, 66);
            this.HEENTResult.SummaryFlag = null;
            this.HEENTResult.TabIndex = 54;
            // 
            // bsDoctorOccMed
            // 
            this.bsDoctorOccMed.DataMember = "trn_doctor_occ_meds";
            this.bsDoctorOccMed.DataSource = this.bsPatientRegis;
            // 
            // bsPatientRegis
            // 
            this.bsPatientRegis.DataSource = typeof(DBCheckup.trn_patient_regi);
            // 
            // THROATResult
            // 
            this.THROATResult.AutoCompleteListThList = null;
            this.THROATResult.AutoCompleteType = null;
            this.THROATResult.DataBindings.Add(new System.Windows.Forms.Binding("ResultTH", this.bsDoctorOccMed, "tdom_heent_throat", true));
            this.THROATResult.DataBindings.Add(new System.Windows.Forms.Binding("SummaryFlag", this.bsDoctorOccMed, "tdom_h_throat", true));
            this.THROATResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.THROATResult.LableBackGroundColor = System.Drawing.Color.Aquamarine;
            this.THROATResult.LableText = "THROAT";
            this.THROATResult.Language = BKvs2010.Language.TH;
            this.THROATResult.Location = new System.Drawing.Point(536, 360);
            this.THROATResult.Name = "THROATResult";
            this.THROATResult.ReadOnly = false;
            this.THROATResult.ResultTH = "";
            this.THROATResult.Size = new System.Drawing.Size(430, 66);
            this.THROATResult.SummaryFlag = null;
            this.THROATResult.TabIndex = 57;
            // 
            // NOSEResult
            // 
            this.NOSEResult.AutoCompleteListThList = null;
            this.NOSEResult.AutoCompleteType = null;
            this.NOSEResult.DataBindings.Add(new System.Windows.Forms.Binding("ResultTH", this.bsDoctorOccMed, "tdom_heent_nose", true));
            this.NOSEResult.DataBindings.Add(new System.Windows.Forms.Binding("SummaryFlag", this.bsDoctorOccMed, "tdom_h_nose", true));
            this.NOSEResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.NOSEResult.LableBackGroundColor = System.Drawing.Color.Aquamarine;
            this.NOSEResult.LableText = "NOSE";
            this.NOSEResult.Language = BKvs2010.Language.TH;
            this.NOSEResult.Location = new System.Drawing.Point(536, 290);
            this.NOSEResult.Name = "NOSEResult";
            this.NOSEResult.ReadOnly = false;
            this.NOSEResult.ResultTH = "";
            this.NOSEResult.Size = new System.Drawing.Size(430, 66);
            this.NOSEResult.SummaryFlag = null;
            this.NOSEResult.TabIndex = 56;
            // 
            // HEADResult
            // 
            this.HEADResult.AutoCompleteListThList = null;
            this.HEADResult.AutoCompleteType = null;
            this.HEADResult.DataBindings.Add(new System.Windows.Forms.Binding("ResultTH", this.bsDoctorOccMed, "tdom_heent_head", true));
            this.HEADResult.DataBindings.Add(new System.Windows.Forms.Binding("SummaryFlag", this.bsDoctorOccMed, "tdom_h_head", true));
            this.HEADResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.HEADResult.LableBackGroundColor = System.Drawing.Color.Aquamarine;
            this.HEADResult.LableText = "HEAD";
            this.HEADResult.Language = BKvs2010.Language.TH;
            this.HEADResult.Location = new System.Drawing.Point(536, 80);
            this.HEADResult.Name = "HEADResult";
            this.HEADResult.ReadOnly = false;
            this.HEADResult.ResultTH = "";
            this.HEADResult.Size = new System.Drawing.Size(430, 66);
            this.HEADResult.SummaryFlag = null;
            this.HEADResult.TabIndex = 53;
            // 
            // EARSResult
            // 
            this.EARSResult.AutoCompleteListThList = null;
            this.EARSResult.AutoCompleteType = null;
            this.EARSResult.DataBindings.Add(new System.Windows.Forms.Binding("ResultTH", this.bsDoctorOccMed, "tdom_heent_ears", true));
            this.EARSResult.DataBindings.Add(new System.Windows.Forms.Binding("SummaryFlag", this.bsDoctorOccMed, "tdom_h_ears", true));
            this.EARSResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.EARSResult.LableBackGroundColor = System.Drawing.Color.Aquamarine;
            this.EARSResult.LableText = "EARS";
            this.EARSResult.Language = BKvs2010.Language.TH;
            this.EARSResult.Location = new System.Drawing.Point(536, 220);
            this.EARSResult.Name = "EARSResult";
            this.EARSResult.ReadOnly = false;
            this.EARSResult.ResultTH = "";
            this.EARSResult.Size = new System.Drawing.Size(430, 66);
            this.EARSResult.SummaryFlag = null;
            this.EARSResult.TabIndex = 55;
            // 
            // EYESResult
            // 
            this.EYESResult.AutoCompleteListThList = null;
            this.EYESResult.AutoCompleteType = null;
            this.EYESResult.DataBindings.Add(new System.Windows.Forms.Binding("ResultTH", this.bsDoctorOccMed, "tdom_heent_eyes", true));
            this.EYESResult.DataBindings.Add(new System.Windows.Forms.Binding("SummaryFlag", this.bsDoctorOccMed, "tdom_h_eyes", true));
            this.EYESResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.EYESResult.LableBackGroundColor = System.Drawing.Color.Aquamarine;
            this.EYESResult.LableText = "EYES";
            this.EYESResult.Language = BKvs2010.Language.TH;
            this.EYESResult.Location = new System.Drawing.Point(536, 150);
            this.EYESResult.Name = "EYESResult";
            this.EYESResult.ReadOnly = false;
            this.EYESResult.ResultTH = "";
            this.EYESResult.Size = new System.Drawing.Size(430, 66);
            this.EYESResult.SummaryFlag = null;
            this.EYESResult.TabIndex = 54;
            // 
            // NeuroResult
            // 
            this.NeuroResult.AutoCompleteListThList = null;
            this.NeuroResult.AutoCompleteType = null;
            this.NeuroResult.DataBindings.Add(new System.Windows.Forms.Binding("ResultTH", this.bsDoctorOccMed, "tdom_neuro_remark", true));
            this.NeuroResult.DataBindings.Add(new System.Windows.Forms.Binding("SummaryFlag", this.bsDoctorOccMed, "tdom_neuro", true));
            this.NeuroResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.NeuroResult.LableBackGroundColor = System.Drawing.Color.LightGreen;
            this.NeuroResult.LableText = "Neuro";
            this.NeuroResult.Language = BKvs2010.Language.TH;
            this.NeuroResult.Location = new System.Drawing.Point(5, 285);
            this.NeuroResult.Name = "NeuroResult";
            this.NeuroResult.ReadOnly = false;
            this.NeuroResult.ResultTH = "";
            this.NeuroResult.Size = new System.Drawing.Size(480, 66);
            this.NeuroResult.SummaryFlag = null;
            this.NeuroResult.TabIndex = 51;
            // 
            // CVSResult
            // 
            this.CVSResult.AutoCompleteListThList = null;
            this.CVSResult.AutoCompleteType = null;
            this.CVSResult.DataBindings.Add(new System.Windows.Forms.Binding("ResultTH", this.bsDoctorOccMed, "tdom_cvs_remark", true));
            this.CVSResult.DataBindings.Add(new System.Windows.Forms.Binding("SummaryFlag", this.bsDoctorOccMed, "tdom_cvs", true));
            this.CVSResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.CVSResult.LableBackGroundColor = System.Drawing.Color.LightGreen;
            this.CVSResult.LableText = "Cardiovascular System";
            this.CVSResult.Language = BKvs2010.Language.TH;
            this.CVSResult.Location = new System.Drawing.Point(5, 215);
            this.CVSResult.Name = "CVSResult";
            this.CVSResult.ReadOnly = false;
            this.CVSResult.ResultTH = "";
            this.CVSResult.Size = new System.Drawing.Size(480, 66);
            this.CVSResult.SummaryFlag = null;
            this.CVSResult.TabIndex = 49;
            // 
            // ChestResult
            // 
            this.ChestResult.AutoCompleteListThList = null;
            this.ChestResult.AutoCompleteType = null;
            this.ChestResult.DataBindings.Add(new System.Windows.Forms.Binding("ResultTH", this.bsDoctorOccMed, "tdom_chest_remark", true));
            this.ChestResult.DataBindings.Add(new System.Windows.Forms.Binding("SummaryFlag", this.bsDoctorOccMed, "tdom_chest", true));
            this.ChestResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.ChestResult.LableBackGroundColor = System.Drawing.Color.LightGreen;
            this.ChestResult.LableText = "Chest";
            this.ChestResult.Language = BKvs2010.Language.TH;
            this.ChestResult.Location = new System.Drawing.Point(5, 145);
            this.ChestResult.Name = "ChestResult";
            this.ChestResult.ReadOnly = false;
            this.ChestResult.ResultTH = "";
            this.ChestResult.Size = new System.Drawing.Size(480, 66);
            this.ChestResult.SummaryFlag = null;
            this.ChestResult.TabIndex = 48;
            // 
            // AbdomenResult
            // 
            this.AbdomenResult.AutoCompleteListThList = null;
            this.AbdomenResult.AutoCompleteType = null;
            this.AbdomenResult.DataBindings.Add(new System.Windows.Forms.Binding("ResultTH", this.bsDoctorOccMed, "tdom_abdomen_remark", true));
            this.AbdomenResult.DataBindings.Add(new System.Windows.Forms.Binding("SummaryFlag", this.bsDoctorOccMed, "tdom_abdomen", true));
            this.AbdomenResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.AbdomenResult.LableBackGroundColor = System.Drawing.Color.LightGreen;
            this.AbdomenResult.LableText = "Abdomen";
            this.AbdomenResult.Language = BKvs2010.Language.TH;
            this.AbdomenResult.Location = new System.Drawing.Point(5, 75);
            this.AbdomenResult.Name = "AbdomenResult";
            this.AbdomenResult.ReadOnly = false;
            this.AbdomenResult.ResultTH = "";
            this.AbdomenResult.Size = new System.Drawing.Size(480, 66);
            this.AbdomenResult.SummaryFlag = null;
            this.AbdomenResult.TabIndex = 47;
            // 
            // GAResult
            // 
            this.GAResult.AutoCompleteListThList = null;
            this.GAResult.AutoCompleteType = null;
            this.GAResult.DataBindings.Add(new System.Windows.Forms.Binding("ResultTH", this.bsDoctorOccMed, "tdom_ga_remark", true));
            this.GAResult.DataBindings.Add(new System.Windows.Forms.Binding("SummaryFlag", this.bsDoctorOccMed, "tdom_ga", true));
            this.GAResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.GAResult.LableBackGroundColor = System.Drawing.Color.LightGreen;
            this.GAResult.LableText = "General Appearance";
            this.GAResult.Language = BKvs2010.Language.TH;
            this.GAResult.Location = new System.Drawing.Point(5, 5);
            this.GAResult.Name = "GAResult";
            this.GAResult.ReadOnly = false;
            this.GAResult.ResultTH = "";
            this.GAResult.Size = new System.Drawing.Size(480, 66);
            this.GAResult.SummaryFlag = null;
            this.GAResult.TabIndex = 46;
            // 
            // tblPanalOccMed
            // 
            this.tblPanalOccMed.ColumnCount = 1;
            this.tblPanalOccMed.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblPanalOccMed.Controls.Add(this.panel12, 0, 1);
            this.tblPanalOccMed.Controls.Add(this.panalMusculos, 0, 0);
            this.tblPanalOccMed.Location = new System.Drawing.Point(5, 355);
            this.tblPanalOccMed.Margin = new System.Windows.Forms.Padding(0);
            this.tblPanalOccMed.Name = "tblPanalOccMed";
            this.tblPanalOccMed.RowCount = 2;
            this.tblPanalOccMed.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblPanalOccMed.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblPanalOccMed.Size = new System.Drawing.Size(480, 136);
            this.tblPanalOccMed.TabIndex = 45;
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.OtherResult);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel12.Location = new System.Drawing.Point(0, 70);
            this.panel12.Margin = new System.Windows.Forms.Padding(0);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(480, 70);
            this.panel12.TabIndex = 0;
            // 
            // OtherResult
            // 
            this.OtherResult.AutoCompleteListThList = null;
            this.OtherResult.AutoCompleteType = null;
            this.OtherResult.DataBindings.Add(new System.Windows.Forms.Binding("ResultTH", this.bsDoctorOccMed, "tdom_other_remark", true));
            this.OtherResult.DataBindings.Add(new System.Windows.Forms.Binding("SummaryFlag", this.bsDoctorOccMed, "tdom_other", true));
            this.OtherResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.OtherResult.LableBackGroundColor = System.Drawing.Color.LightGreen;
            this.OtherResult.LableText = "Other";
            this.OtherResult.Language = BKvs2010.Language.TH;
            this.OtherResult.Location = new System.Drawing.Point(0, 0);
            this.OtherResult.Name = "OtherResult";
            this.OtherResult.ReadOnly = false;
            this.OtherResult.ResultTH = "";
            this.OtherResult.Size = new System.Drawing.Size(480, 66);
            this.OtherResult.SummaryFlag = null;
            this.OtherResult.TabIndex = 53;
            // 
            // panalMusculos
            // 
            this.panalMusculos.Controls.Add(this.MusculoskeletonResult);
            this.panalMusculos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panalMusculos.Location = new System.Drawing.Point(0, 0);
            this.panalMusculos.Margin = new System.Windows.Forms.Padding(0);
            this.panalMusculos.Name = "panalMusculos";
            this.panalMusculos.Size = new System.Drawing.Size(480, 70);
            this.panalMusculos.TabIndex = 2;
            // 
            // MusculoskeletonResult
            // 
            this.MusculoskeletonResult.AutoCompleteListThList = null;
            this.MusculoskeletonResult.AutoCompleteType = null;
            this.MusculoskeletonResult.DataBindings.Add(new System.Windows.Forms.Binding("ResultTH", this.bsDoctorOccMed, "tdom_musculos_remark", true));
            this.MusculoskeletonResult.DataBindings.Add(new System.Windows.Forms.Binding("SummaryFlag", this.bsDoctorOccMed, "tdom_musculos", true));
            this.MusculoskeletonResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.MusculoskeletonResult.LableBackGroundColor = System.Drawing.Color.LightGreen;
            this.MusculoskeletonResult.LableText = "Musculoskeleton system";
            this.MusculoskeletonResult.Language = BKvs2010.Language.TH;
            this.MusculoskeletonResult.Location = new System.Drawing.Point(0, 0);
            this.MusculoskeletonResult.Name = "MusculoskeletonResult";
            this.MusculoskeletonResult.ReadOnly = false;
            this.MusculoskeletonResult.ResultTH = "";
            this.MusculoskeletonResult.Size = new System.Drawing.Size(480, 66);
            this.MusculoskeletonResult.SummaryFlag = null;
            this.MusculoskeletonResult.TabIndex = 52;
            // 
            // ExamOccMedUC
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.panel15);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MinimumSize = new System.Drawing.Size(974, 566);
            this.Name = "ExamOccMedUC";
            this.Size = new System.Drawing.Size(974, 566);
            this.panel15.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bsDoctorOccMed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPatientRegis)).EndInit();
            this.tblPanalOccMed.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            this.panalMusculos.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.TableLayoutPanel tblPanalOccMed;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Panel panalMusculos;
        private Usercontrols.ExamResultUI GAResult;
        private Usercontrols.ExamResultUI NeuroResult;
        private Usercontrols.ExamResultUI CVSResult;
        private Usercontrols.ExamResultUI ChestResult;
        private Usercontrols.ExamResultUI AbdomenResult;
        private Usercontrols.ExamResultUI OtherResult;
        private Usercontrols.ExamResultUI MusculoskeletonResult;
        private System.Windows.Forms.BindingSource bsPatientRegis;
        private System.Windows.Forms.BindingSource bsDoctorOccMed;
        private Usercontrols.ExamResultUI THROATResult;
        private Usercontrols.ExamResultUI HEENTResult;
        private Usercontrols.ExamResultUI NOSEResult;
        private Usercontrols.ExamResultUI HEADResult;
        private Usercontrols.ExamResultUI EARSResult;
        private Usercontrols.ExamResultUI EYESResult;
    }
}
