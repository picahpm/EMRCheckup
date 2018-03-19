namespace BKvs2010.UserControlEMR
{
    partial class EKGUC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EKGUC));
            this.panel1 = new System.Windows.Forms.Panel();
            this.chknotshowrpt = new UserControlLibrary.CheckBoxBinding();
            this.bsDoctorEKG = new System.Windows.Forms.BindingSource(this.components);
            this.bsDoctorHDR = new System.Windows.Forms.BindingSource(this.components);
            this.bsPatientRegis = new System.Windows.Forms.BindingSource(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.favoriteTextBox1 = new BKvs2010.Usercontrols.FavoriteTextBox();
            this.autoCompleteUC1 = new UserControlLibrary.TextBoxAutoComplete();
            this.pnlsummary = new UserControlLibrary.PanelRadioBinding();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsDoctorEKG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsDoctorHDR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPatientRegis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.pnlsummary.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chknotshowrpt);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(874, 442);
            this.panel1.TabIndex = 0;
            // 
            // chknotshowrpt
            // 
            this.chknotshowrpt.AutoSize = true;
            this.chknotshowrpt.BindingType = UserControlLibrary.CheckBoxBinding.TypeData.Char;
            this.chknotshowrpt.BindingValue = null;
            this.chknotshowrpt.CheckedFalseValue = "N";
            this.chknotshowrpt.CheckedTrueValue = "Y";
            this.chknotshowrpt.DataBindings.Add(new System.Windows.Forms.Binding("BindingValue", this.bsDoctorEKG, "trek_not_show_rpt", true));
            this.chknotshowrpt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chknotshowrpt.Location = new System.Drawing.Point(190, 13);
            this.chknotshowrpt.Name = "chknotshowrpt";
            this.chknotshowrpt.Size = new System.Drawing.Size(124, 20);
            this.chknotshowrpt.TabIndex = 101;
            this.chknotshowrpt.Text = "Not Show Report";
            this.chknotshowrpt.UseVisualStyleBackColor = true;
            // 
            // bsDoctorEKG
            // 
            this.bsDoctorEKG.DataMember = "trn_doctor_ekgs";
            this.bsDoctorEKG.DataSource = this.bsDoctorHDR;
            // 
            // bsDoctorHDR
            // 
            this.bsDoctorHDR.DataMember = "trn_doctor_hdrs";
            this.bsDoctorHDR.DataSource = this.bsPatientRegis;
            // 
            // bsPatientRegis
            // 
            this.bsPatientRegis.DataSource = typeof(DBCheckup.trn_patient_regi);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(73, 75);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(176, 179);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 100;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.favoriteTextBox1);
            this.groupBox2.Controls.Add(this.autoCompleteUC1);
            this.groupBox2.Controls.Add(this.pnlsummary);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(320, 7);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(542, 425);
            this.groupBox2.TabIndex = 99;
            this.groupBox2.TabStop = false;
            // 
            // favoriteTextBox1
            // 
            this.favoriteTextBox1.AutoCompleteListThList = ((System.Collections.Generic.List<string>)(resources.GetObject("favoriteTextBox1.AutoCompleteListThList")));
            this.favoriteTextBox1.BackColor = System.Drawing.Color.Transparent;
            this.favoriteTextBox1.ButtonPosition = BKvs2010.Usercontrols.PrototypeFavoriteTextBox.PrototypeFavoriteTextBoxButtonPosition.TopRight;
            this.favoriteTextBox1.FavoriteOrder = "EK";
            this.favoriteTextBox1.FavoriteType = "CON";
            this.favoriteTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.favoriteTextBox1.ListFabvorite = ((System.Collections.ObjectModel.ObservableCollection<string>)(resources.GetObject("favoriteTextBox1.ListFabvorite")));
            this.favoriteTextBox1.Location = new System.Drawing.Point(6, 139);
            this.favoriteTextBox1.Name = "favoriteTextBox1";
            this.favoriteTextBox1.Size = new System.Drawing.Size(530, 280);
            this.favoriteTextBox1.TabIndex = 113;
            this.favoriteTextBox1.BtnFavoriteClick += new BKvs2010.Usercontrols.PrototypeFavoriteTextBox.OnBtnFavoriteClick(this.favoriteTextBox1_btnFavoriteClick);
            // 
            // autoCompleteUC1
            // 
            this.autoCompleteUC1.AutoDropDownWidth = true;
            this.autoCompleteUC1.DataSource = null;
            this.autoCompleteUC1.DisplayMember = "";
            this.autoCompleteUC1.DropDownWidth = 0;
            this.autoCompleteUC1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.autoCompleteUC1.FreeText = false;
            this.autoCompleteUC1.Location = new System.Drawing.Point(163, 37);
            this.autoCompleteUC1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.autoCompleteUC1.MinTextSearch = 2;
            this.autoCompleteUC1.Multiline = false;
            this.autoCompleteUC1.Name = "autoCompleteUC1";
            this.autoCompleteUC1.ReadOnly = false;
            this.autoCompleteUC1.SelectedItem = null;
            this.autoCompleteUC1.SelectedValue = null;
            this.autoCompleteUC1.Size = new System.Drawing.Size(373, 21);
            this.autoCompleteUC1.TabIndex = 112;
            this.autoCompleteUC1.ValueMember = "";
            // 
            // pnlsummary
            // 
            this.pnlsummary.AbleNull = true;
            this.pnlsummary.BorderRadius = false;
            this.pnlsummary.Controls.Add(this.radioButton3);
            this.pnlsummary.Controls.Add(this.radioButton2);
            this.pnlsummary.Controls.Add(this.radioButton1);
            this.pnlsummary.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsDoctorEKG, "trek_summary", true));
            this.pnlsummary.Location = new System.Drawing.Point(163, 63);
            this.pnlsummary.Name = "pnlsummary";
            this.pnlsummary.ReadOnly = false;
            this.pnlsummary.Size = new System.Drawing.Size(373, 77);
            this.pnlsummary.TabIndex = 3;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoCheck = false;
            this.radioButton3.AutoSize = true;
            this.radioButton3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.radioButton3.Location = new System.Drawing.Point(3, 55);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(277, 20);
            this.radioButton3.TabIndex = 5;
            this.radioButton3.Tag = "C";
            this.radioButton3.Text = "Abnormal finding please consult cardiologist";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.Visible = false;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoCheck = false;
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.radioButton2.Location = new System.Drawing.Point(3, 29);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(295, 20);
            this.radioButton2.TabIndex = 4;
            this.radioButton2.Tag = "A";
            this.radioButton2.Text = "Abnormal finding please see clinical correlation";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.Visible = false;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoCheck = false;
            this.radioButton1.AutoSize = true;
            this.radioButton1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.radioButton1.Location = new System.Drawing.Point(3, 3);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(138, 20);
            this.radioButton1.TabIndex = 3;
            this.radioButton1.Tag = "N";
            this.radioButton1.Text = "Within Normal Limit";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.LightGreen;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(6, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(151, 36);
            this.label3.TabIndex = 1;
            this.label3.Text = "Conclusion and Recommendation :";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.LightGreen;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(6, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "Cardiologist :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.LightGreen;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(1, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(541, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Conclusion";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // EKGUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "EKGUC";
            this.Size = new System.Drawing.Size(874, 442);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsDoctorEKG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsDoctorHDR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPatientRegis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.pnlsummary.ResumeLayout(false);
            this.pnlsummary.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private UserControlLibrary.CheckBoxBinding chknotshowrpt;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private UserControlLibrary.PanelRadioBinding pnlsummary;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private UserControlLibrary.TextBoxAutoComplete autoCompleteUC1;
        private System.Windows.Forms.BindingSource bsPatientRegis;
        private System.Windows.Forms.BindingSource bsDoctorEKG;
        private System.Windows.Forms.BindingSource bsDoctorHDR;
        private Usercontrols.FavoriteTextBox favoriteTextBox1;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;

    }
}
