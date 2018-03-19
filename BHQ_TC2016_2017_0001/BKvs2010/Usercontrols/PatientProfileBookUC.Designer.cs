namespace BKvs2010.Usercontrols
{
    partial class PatientProfileBookUC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PatientProfileBookUC));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.PatientInfoBS = new System.Windows.Forms.BindingSource(this.components);
            this.lbRetrieve = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblGender = new System.Windows.Forms.Label();
            this.lblAge = new System.Windows.Forms.Label();
            this.lblDOB = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblFullName = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblEN = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblHN = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PatientInfoBS)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.DataBindings.Add(new System.Windows.Forms.Binding("Image", this.PatientInfoBS, "picture", true));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(324, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(93, 100);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 35;
            this.pictureBox1.TabStop = false;
            // 
            // PatientInfoBS
            // 
            this.PatientInfoBS.DataSource = typeof(BKvs2010.Models.PatientInfoModel);
            // 
            // lbRetrieve
            // 
            this.lbRetrieve.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbRetrieve.AutoSize = true;
            this.lbRetrieve.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbRetrieve.Font = new System.Drawing.Font("Tahoma", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbRetrieve.ForeColor = System.Drawing.Color.Red;
            this.lbRetrieve.Location = new System.Drawing.Point(324, 106);
            this.lbRetrieve.Name = "lbRetrieve";
            this.lbRetrieve.Size = new System.Drawing.Size(96, 16);
            this.lbRetrieve.TabIndex = 49;
            this.lbRetrieve.Text = "Retrieve info.";
            this.lbRetrieve.Click += new System.EventHandler(this.lbRetrieve_Click);
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.PatientInfoBS, "address", true));
            this.lblAddress.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblAddress.ForeColor = System.Drawing.Color.White;
            this.lblAddress.Location = new System.Drawing.Point(68, 127);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(61, 14);
            this.lblAddress.TabIndex = 63;
            this.lblAddress.Text = "lblAddress";
            // 
            // lblGender
            // 
            this.lblGender.AutoSize = true;
            this.lblGender.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.PatientInfoBS, "gender", true));
            this.lblGender.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblGender.ForeColor = System.Drawing.Color.White;
            this.lblGender.Location = new System.Drawing.Point(62, 103);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new System.Drawing.Size(58, 14);
            this.lblGender.TabIndex = 62;
            this.lblGender.Text = "lblGender";
            // 
            // lblAge
            // 
            this.lblAge.AutoSize = true;
            this.lblAge.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.PatientInfoBS, "age", true));
            this.lblAge.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblAge.ForeColor = System.Drawing.Color.White;
            this.lblAge.Location = new System.Drawing.Point(175, 79);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(40, 14);
            this.lblAge.TabIndex = 61;
            this.lblAge.Text = "lblAge";
            // 
            // lblDOB
            // 
            this.lblDOB.AutoSize = true;
            this.lblDOB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.PatientInfoBS, "dob", true));
            this.lblDOB.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblDOB.ForeColor = System.Drawing.Color.White;
            this.lblDOB.Location = new System.Drawing.Point(62, 79);
            this.lblDOB.Name = "lblDOB";
            this.lblDOB.Size = new System.Drawing.Size(42, 14);
            this.lblDOB.TabIndex = 60;
            this.lblDOB.Text = "lblDOB";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(7, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 14);
            this.label2.TabIndex = 58;
            this.label2.Text = "HN:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(134, 79);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(35, 14);
            this.label14.TabIndex = 59;
            this.label14.Text = "Age:";
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.PatientInfoBS, "fullname", true));
            this.lblFullName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblFullName.ForeColor = System.Drawing.Color.White;
            this.lblFullName.Location = new System.Drawing.Point(62, 55);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(66, 14);
            this.lblFullName.TabIndex = 57;
            this.lblFullName.Text = "lblFullName";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(7, 127);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 14);
            this.label10.TabIndex = 52;
            this.label10.Text = "Address:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(7, 103);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 14);
            this.label9.TabIndex = 51;
            this.label9.Text = "Gender:";
            // 
            // lblEN
            // 
            this.lblEN.AutoSize = true;
            this.lblEN.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.PatientInfoBS, "en", true));
            this.lblEN.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblEN.ForeColor = System.Drawing.Color.White;
            this.lblEN.Location = new System.Drawing.Point(42, 31);
            this.lblEN.Name = "lblEN";
            this.lblEN.Size = new System.Drawing.Size(33, 14);
            this.lblEN.TabIndex = 50;
            this.lblEN.Text = "lblEN";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(7, 31);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(26, 14);
            this.label13.TabIndex = 55;
            this.label13.Text = "EN:";
            // 
            // lblHN
            // 
            this.lblHN.AutoSize = true;
            this.lblHN.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.PatientInfoBS, "hn", true));
            this.lblHN.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblHN.ForeColor = System.Drawing.Color.White;
            this.lblHN.Location = new System.Drawing.Point(42, 7);
            this.lblHN.Name = "lblHN";
            this.lblHN.Size = new System.Drawing.Size(34, 14);
            this.lblHN.TabIndex = 54;
            this.lblHN.Text = "lblHN";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(7, 79);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 14);
            this.label8.TabIndex = 53;
            this.label8.Text = "DOB:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(7, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 14);
            this.label7.TabIndex = 56;
            this.label7.Text = "Name:";
            // 
            // PatientProfileBookUC
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.HotTrack;
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.lblGender);
            this.Controls.Add(this.lblAge);
            this.Controls.Add(this.lblDOB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.lblFullName);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblEN);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.lblHN);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lbRetrieve);
            this.Controls.Add(this.pictureBox1);
            this.Name = "PatientProfileBookUC";
            this.Size = new System.Drawing.Size(420, 164);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PatientInfoBS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lbRetrieve;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.Label lblDOB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblEN;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblHN;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.BindingSource PatientInfoBS;
    }
}
