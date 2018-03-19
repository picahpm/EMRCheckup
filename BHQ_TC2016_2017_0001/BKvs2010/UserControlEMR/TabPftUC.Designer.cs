namespace BKvs2010.UserControlEMR
{
    partial class TabPftUC
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panelAllData = new System.Windows.Forms.Panel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this._bsPatientPFT = new System.Windows.Forms.BindingSource(this.components);
            this._bsPatientQues = new System.Windows.Forms.BindingSource(this.components);
            this._bsPatientRegis = new System.Windows.Forms.BindingSource(this.components);
            this.pftMainUC1 = new BKvs2010.UserControlEMR.PftMainUC();
            this.pftOccMedUC1 = new BKvs2010.UserControlEMR.PftOccMedUC();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panelAllData.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._bsPatientPFT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._bsPatientQues)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._bsPatientRegis)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1168, 798);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panelAllData);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(1160, 769);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "PFT";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panelAllData
            // 
            this.panelAllData.AutoScroll = true;
            this.panelAllData.AutoScrollMinSize = new System.Drawing.Size(1160, 798);
            this.panelAllData.Controls.Add(this.pftMainUC1);
            this.panelAllData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAllData.Location = new System.Drawing.Point(0, 0);
            this.panelAllData.Name = "panelAllData";
            this.panelAllData.Size = new System.Drawing.Size(1160, 769);
            this.panelAllData.TabIndex = 106;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(1160, 769);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Occ Med.";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pftOccMedUC1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1160, 772);
            this.panel2.TabIndex = 1;
            // 
            // _bsPatientPFT
            // 
            this._bsPatientPFT.DataMember = "trn_pfts";
            this._bsPatientPFT.DataSource = this._bsPatientRegis;
            // 
            // _bsPatientQues
            // 
            this._bsPatientQues.DataMember = "trn_ques_patients";
            this._bsPatientQues.DataSource = this._bsPatientRegis;
            // 
            // _bsPatientRegis
            // 
            this._bsPatientRegis.DataSource = typeof(DBCheckup.trn_patient_regi);
            // 
            // pftMainUC1
            // 
            this.pftMainUC1.BackColor = System.Drawing.Color.Transparent;
            this.pftMainUC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pftMainUC1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.pftMainUC1.isDoctorRoom = false;
            this.pftMainUC1.Location = new System.Drawing.Point(0, 0);
            this.pftMainUC1.Margin = new System.Windows.Forms.Padding(0);
            this.pftMainUC1.MinimumSize = new System.Drawing.Size(1160, 798);
            this.pftMainUC1.Name = "pftMainUC1";
            this.pftMainUC1.PatientRegis = null;
            this.pftMainUC1.Size = new System.Drawing.Size(1160, 798);
            this.pftMainUC1.TabIndex = 0;
            // 
            // pftOccMedUC1
            // 
            this.pftOccMedUC1.BackColor = System.Drawing.Color.Transparent;
            this.pftOccMedUC1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.pftOccMedUC1.Location = new System.Drawing.Point(14, 11);
            this.pftOccMedUC1.Margin = new System.Windows.Forms.Padding(0);
            this.pftOccMedUC1.MinimumSize = new System.Drawing.Size(733, 285);
            this.pftOccMedUC1.Name = "pftOccMedUC1";
            this.pftOccMedUC1.PatientRegis = null;
            this.pftOccMedUC1.Size = new System.Drawing.Size(733, 285);
            this.pftOccMedUC1.TabIndex = 0;
            // 
            // TabPftUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "TabPftUC";
            this.Size = new System.Drawing.Size(1168, 798);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panelAllData.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._bsPatientPFT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._bsPatientQues)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._bsPatientRegis)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource _bsPatientRegis;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.BindingSource _bsPatientQues;
        private System.Windows.Forms.BindingSource _bsPatientPFT;
        private System.Windows.Forms.Panel panelAllData;
        private PftMainUC pftMainUC1;
        private PftOccMedUC pftOccMedUC1;
    }
}
