namespace BKvs2010
{
    partial class frmChoicePopUp
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
            this.btnToback = new System.Windows.Forms.Button();
            this.btnSendtonext = new System.Windows.Forms.Button();
            this.lbQuestions = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnToback
            // 
            this.btnToback.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnToback.Location = new System.Drawing.Point(58, 87);
            this.btnToback.Name = "btnToback";
            this.btnToback.Size = new System.Drawing.Size(152, 34);
            this.btnToback.TabIndex = 0;
            this.btnToback.Text = "Send To PE";
            this.btnToback.UseVisualStyleBackColor = true;
            this.btnToback.Click += new System.EventHandler(this.btnToback_Click);
            // 
            // btnSendtonext
            // 
            this.btnSendtonext.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSendtonext.Location = new System.Drawing.Point(280, 87);
            this.btnSendtonext.Name = "btnSendtonext";
            this.btnSendtonext.Size = new System.Drawing.Size(170, 34);
            this.btnSendtonext.TabIndex = 0;
            this.btnSendtonext.Text = "Send To Screening";
            this.btnSendtonext.UseVisualStyleBackColor = true;
            this.btnSendtonext.Click += new System.EventHandler(this.btnSendtonext_Click);
            // 
            // lbQuestions
            // 
            this.lbQuestions.AutoSize = true;
            this.lbQuestions.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbQuestions.Location = new System.Drawing.Point(18, 42);
            this.lbQuestions.Name = "lbQuestions";
            this.lbQuestions.Size = new System.Drawing.Size(473, 19);
            this.lbQuestions.TabIndex = 1;
            this.lbQuestions.Text = "ผู้รับบริการเข้าเงื่อนไขในการพบแพทย์ PE คุณยืนยันในการส่งตัวหรือไม่?";
            // 
            // frmChoicePopUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 156);
            this.Controls.Add(this.lbQuestions);
            this.Controls.Add(this.btnSendtonext);
            this.Controls.Add(this.btnToback);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximumSize = new System.Drawing.Size(515, 180);
            this.MinimumSize = new System.Drawing.Size(515, 180);
            this.Name = "frmChoicePopUp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Message Confirm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnToback;
        private System.Windows.Forms.Button btnSendtonext;
        private System.Windows.Forms.Label lbQuestions;
    }
}