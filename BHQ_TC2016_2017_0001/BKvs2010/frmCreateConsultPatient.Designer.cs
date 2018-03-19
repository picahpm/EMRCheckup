namespace BKvs2010
{
    partial class frmCreateConsultPatient
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCreateConsultPatient));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbAlert = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dataNo = new System.Windows.Forms.Label();
            this.lbdataAlergy = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.datavipLevel = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.picPatient = new System.Windows.Forms.PictureBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnContinue = new System.Windows.Forms.Button();
            this.lbHN = new System.Windows.Forms.Label();
            this.lbEN = new System.Windows.Forms.Label();
            this.lbName = new System.Windows.Forms.Label();
            this.lbGender = new System.Windows.Forms.Label();
            this.lbDOB = new System.Windows.Forms.Label();
            this.lbAge = new System.Windows.Forms.Label();
            this.lbNation = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPatient)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.962983F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.712231F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 72.66187F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.571428F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(295, 525);
            this.tableLayoutPanel1.TabIndex = 43;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.txtSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(289, 41);
            this.panel1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "HN.";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(207, 7);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 25);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Location = new System.Drawing.Point(36, 8);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(165, 23);
            this.txtSearch.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lbAlert);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 50);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(289, 45);
            this.panel2.TabIndex = 1;
            // 
            // lbAlert
            // 
            this.lbAlert.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbAlert.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbAlert.ForeColor = System.Drawing.Color.Red;
            this.lbAlert.Location = new System.Drawing.Point(0, 0);
            this.lbAlert.Name = "lbAlert";
            this.lbAlert.Size = new System.Drawing.Size(289, 45);
            this.lbAlert.TabIndex = 0;
            this.lbAlert.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(113)))), ((int)(((byte)(188)))));
            this.panel3.Controls.Add(this.lbNation);
            this.panel3.Controls.Add(this.lbAge);
            this.panel3.Controls.Add(this.lbDOB);
            this.panel3.Controls.Add(this.lbGender);
            this.panel3.Controls.Add(this.lbName);
            this.panel3.Controls.Add(this.lbEN);
            this.panel3.Controls.Add(this.lbHN);
            this.panel3.Controls.Add(this.dataNo);
            this.panel3.Controls.Add(this.lbdataAlergy);
            this.panel3.Controls.Add(this.label35);
            this.panel3.Controls.Add(this.label34);
            this.panel3.Controls.Add(this.label33);
            this.panel3.Controls.Add(this.label32);
            this.panel3.Controls.Add(this.label31);
            this.panel3.Controls.Add(this.label30);
            this.panel3.Controls.Add(this.datavipLevel);
            this.panel3.Controls.Add(this.label22);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.picPatient);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 101);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(289, 375);
            this.panel3.TabIndex = 2;
            // 
            // dataNo
            // 
            this.dataNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataNo.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dataNo.ForeColor = System.Drawing.Color.Cyan;
            this.dataNo.Location = new System.Drawing.Point(6, 19);
            this.dataNo.Name = "dataNo";
            this.dataNo.Size = new System.Drawing.Size(171, 25);
            this.dataNo.TabIndex = 49;
            this.dataNo.Text = "NO. ";
            // 
            // lbdataAlergy
            // 
            this.lbdataAlergy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbdataAlergy.Location = new System.Drawing.Point(7, 300);
            this.lbdataAlergy.Multiline = true;
            this.lbdataAlergy.Name = "lbdataAlergy";
            this.lbdataAlergy.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.lbdataAlergy.Size = new System.Drawing.Size(273, 67);
            this.lbdataAlergy.TabIndex = 54;
            // 
            // label35
            // 
            this.label35.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label35.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label35.ForeColor = System.Drawing.Color.Black;
            this.label35.Location = new System.Drawing.Point(7, 112);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(68, 18);
            this.label35.TabIndex = 48;
            this.label35.Text = "HN:";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label34
            // 
            this.label34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label34.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label34.ForeColor = System.Drawing.Color.Black;
            this.label34.Location = new System.Drawing.Point(7, 135);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(68, 18);
            this.label34.TabIndex = 51;
            this.label34.Text = "EN:";
            this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label33
            // 
            this.label33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label33.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label33.ForeColor = System.Drawing.Color.Black;
            this.label33.Location = new System.Drawing.Point(7, 159);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(68, 18);
            this.label33.TabIndex = 50;
            this.label33.Text = "FullName:";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label32
            // 
            this.label32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label32.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label32.ForeColor = System.Drawing.Color.Black;
            this.label32.Location = new System.Drawing.Point(7, 184);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(68, 18);
            this.label32.TabIndex = 47;
            this.label32.Text = "Gender :";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label31
            // 
            this.label31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label31.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label31.ForeColor = System.Drawing.Color.Black;
            this.label31.Location = new System.Drawing.Point(7, 207);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(68, 18);
            this.label31.TabIndex = 46;
            this.label31.Text = "DOB :";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label30
            // 
            this.label30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label30.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label30.ForeColor = System.Drawing.Color.Black;
            this.label30.Location = new System.Drawing.Point(7, 231);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(68, 18);
            this.label30.TabIndex = 43;
            this.label30.Text = "Age :";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // datavipLevel
            // 
            this.datavipLevel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.datavipLevel.BackColor = System.Drawing.Color.Transparent;
            this.datavipLevel.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.datavipLevel.ForeColor = System.Drawing.Color.Yellow;
            this.datavipLevel.Location = new System.Drawing.Point(7, 45);
            this.datavipLevel.Name = "datavipLevel";
            this.datavipLevel.Size = new System.Drawing.Size(170, 18);
            this.datavipLevel.TabIndex = 52;
            this.datavipLevel.Text = "xxxx";
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label22.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label22.ForeColor = System.Drawing.Color.Black;
            this.label22.Location = new System.Drawing.Point(7, 255);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(68, 18);
            this.label22.TabIndex = 44;
            this.label22.Text = "Nation :";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.LightGreen;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(7, 279);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(274, 18);
            this.label1.TabIndex = 45;
            this.label1.Text = "Allergy :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // picPatient
            // 
            this.picPatient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picPatient.Image = ((System.Drawing.Image)(resources.GetObject("picPatient.Image")));
            this.picPatient.Location = new System.Drawing.Point(182, 7);
            this.picPatient.Name = "picPatient";
            this.picPatient.Size = new System.Drawing.Size(100, 100);
            this.picPatient.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPatient.TabIndex = 53;
            this.picPatient.TabStop = false;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnCancel);
            this.panel4.Controls.Add(this.btnContinue);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 482);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(289, 40);
            this.panel4.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(167, 8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(83, 25);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnContinue
            // 
            this.btnContinue.Enabled = false;
            this.btnContinue.Location = new System.Drawing.Point(47, 8);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(114, 25);
            this.btnContinue.TabIndex = 2;
            this.btnContinue.Text = "Process Continue";
            this.btnContinue.UseVisualStyleBackColor = true;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // lbHN
            // 
            this.lbHN.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbHN.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lbHN.Location = new System.Drawing.Point(81, 112);
            this.lbHN.Name = "lbHN";
            this.lbHN.Size = new System.Drawing.Size(191, 20);
            this.lbHN.TabIndex = 1;
            this.lbHN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbEN
            // 
            this.lbEN.ForeColor = System.Drawing.Color.White;
            this.lbEN.Location = new System.Drawing.Point(81, 135);
            this.lbEN.Name = "lbEN";
            this.lbEN.Size = new System.Drawing.Size(191, 20);
            this.lbEN.TabIndex = 55;
            this.lbEN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbName
            // 
            this.lbName.ForeColor = System.Drawing.Color.White;
            this.lbName.Location = new System.Drawing.Point(81, 159);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(191, 20);
            this.lbName.TabIndex = 56;
            this.lbName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbGender
            // 
            this.lbGender.ForeColor = System.Drawing.Color.White;
            this.lbGender.Location = new System.Drawing.Point(81, 183);
            this.lbGender.Name = "lbGender";
            this.lbGender.Size = new System.Drawing.Size(191, 20);
            this.lbGender.TabIndex = 57;
            this.lbGender.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbDOB
            // 
            this.lbDOB.ForeColor = System.Drawing.Color.White;
            this.lbDOB.Location = new System.Drawing.Point(81, 205);
            this.lbDOB.Name = "lbDOB";
            this.lbDOB.Size = new System.Drawing.Size(191, 20);
            this.lbDOB.TabIndex = 58;
            this.lbDOB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbAge
            // 
            this.lbAge.ForeColor = System.Drawing.Color.White;
            this.lbAge.Location = new System.Drawing.Point(81, 231);
            this.lbAge.Name = "lbAge";
            this.lbAge.Size = new System.Drawing.Size(191, 20);
            this.lbAge.TabIndex = 59;
            this.lbAge.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbNation
            // 
            this.lbNation.ForeColor = System.Drawing.Color.White;
            this.lbNation.Location = new System.Drawing.Point(81, 253);
            this.lbNation.Name = "lbNation";
            this.lbNation.Size = new System.Drawing.Size(191, 20);
            this.lbNation.TabIndex = 60;
            this.lbNation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmCreateConsultPatient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 525);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCreateConsultPatient";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ConsultPatient";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPatient)).EndInit();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label dataNo;
        private System.Windows.Forms.TextBox lbdataAlergy;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label datavipLevel;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picPatient;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lbAlert;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.Label lbHN;
        private System.Windows.Forms.Label lbNation;
        private System.Windows.Forms.Label lbAge;
        private System.Windows.Forms.Label lbDOB;
        private System.Windows.Forms.Label lbGender;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.Label lbEN;

    }
}