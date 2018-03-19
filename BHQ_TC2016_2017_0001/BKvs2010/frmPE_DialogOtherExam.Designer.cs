namespace BKvs2010
{
    partial class frmPE_DialogOtherExam
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPE_DialogOtherExam));
            this.lblMsg = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtOthExRemark = new System.Windows.Forms.TextBox();
            this.DataBinding_trn_doctor_exam = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DataBinding_trn_doctor_exam)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMsg
            // 
            this.lblMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblMsg.ForeColor = System.Drawing.Color.Red;
            this.lblMsg.Location = new System.Drawing.Point(160, 475);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(217, 23);
            this.lblMsg.TabIndex = 99;
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(81, 475);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(73, 25);
            this.button2.TabIndex = 98;
            this.button2.Text = "Cancel";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(6, 475);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(73, 25);
            this.btnSave.TabIndex = 97;
            this.btnSave.Text = "Save";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtOthExRemark
            // 
            this.txtOthExRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOthExRemark.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.DataBinding_trn_doctor_exam, "trxm_exam", true));
            this.txtOthExRemark.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtOthExRemark.Location = new System.Drawing.Point(6, 31);
            this.txtOthExRemark.Multiline = true;
            this.txtOthExRemark.Name = "txtOthExRemark";
            this.txtOthExRemark.Size = new System.Drawing.Size(592, 440);
            this.txtOthExRemark.TabIndex = 100;
            // 
            // DataBinding_trn_doctor_exam
            // 
            this.DataBinding_trn_doctor_exam.DataSource = typeof(DBCheckup.trn_doctor_exam);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(5, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 16);
            this.label1.TabIndex = 101;
            this.label1.Text = "Other Examination";
            // 
            // frmPE_DialogOtherExam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 503);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtOthExRemark);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnSave);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPE_DialogOtherExam";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmDialogPE_OtherExam";
            this.Load += new System.EventHandler(this.frmPE_DialogOtherExam_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataBinding_trn_doctor_exam)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtOthExRemark;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.BindingSource DataBinding_trn_doctor_exam;
    }
}