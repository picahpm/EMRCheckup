namespace BKvs2010
{
    partial class PopupAddLab
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnSaveLab = new System.Windows.Forms.Button();
            this.lblEpisode = new System.Windows.Forms.Label();
            this.dgvEnList = new System.Windows.Forms.DataGridView();
            this.Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.EN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Visitdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEnList)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSaveLab
            // 
            this.btnSaveLab.Location = new System.Drawing.Point(197, 218);
            this.btnSaveLab.Name = "btnSaveLab";
            this.btnSaveLab.Size = new System.Drawing.Size(75, 23);
            this.btnSaveLab.TabIndex = 1;
            this.btnSaveLab.Text = "OK";
            this.btnSaveLab.UseVisualStyleBackColor = true;
            this.btnSaveLab.Click += new System.EventHandler(this.btnSaveLab_Click);
            // 
            // lblEpisode
            // 
            this.lblEpisode.AutoSize = true;
            this.lblEpisode.Location = new System.Drawing.Point(12, 9);
            this.lblEpisode.Name = "lblEpisode";
            this.lblEpisode.Size = new System.Drawing.Size(35, 13);
            this.lblEpisode.TabIndex = 2;
            this.lblEpisode.Text = "label1";
            // 
            // dgvEnList
            // 
            this.dgvEnList.AllowUserToAddRows = false;
            this.dgvEnList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEnList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Select,
            this.EN,
            this.Visitdate});
            this.dgvEnList.Location = new System.Drawing.Point(12, 25);
            this.dgvEnList.Name = "dgvEnList";
            this.dgvEnList.RowHeadersVisible = false;
            this.dgvEnList.Size = new System.Drawing.Size(260, 187);
            this.dgvEnList.TabIndex = 3;
            // 
            // Select
            // 
            this.Select.HeaderText = "Select";
            this.Select.Name = "Select";
            this.Select.Width = 50;
            // 
            // EN
            // 
            this.EN.DataPropertyName = "tpl_en_no";
            this.EN.HeaderText = "EN";
            this.EN.Name = "EN";
            this.EN.ReadOnly = true;
            // 
            // Visitdate
            // 
            this.Visitdate.DataPropertyName = "tpl_lab_date";
            dataGridViewCellStyle1.Format = "d";
            dataGridViewCellStyle1.NullValue = null;
            this.Visitdate.DefaultCellStyle = dataGridViewCellStyle1;
            this.Visitdate.HeaderText = "Visitdate";
            this.Visitdate.Name = "Visitdate";
            this.Visitdate.ReadOnly = true;
            // 
            // PopupAddLab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 245);
            this.Controls.Add(this.dgvEnList);
            this.Controls.Add(this.lblEpisode);
            this.Controls.Add(this.btnSaveLab);
            this.Name = "PopupAddLab";
            this.Text = "AddLab";
            ((System.ComponentModel.ISupportInitialize)(this.dgvEnList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSaveLab;
        private System.Windows.Forms.Label lblEpisode;
        private System.Windows.Forms.DataGridView dgvEnList;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Select;
        private System.Windows.Forms.DataGridViewTextBoxColumn EN;
        private System.Windows.Forms.DataGridViewTextBoxColumn Visitdate;
    }
}