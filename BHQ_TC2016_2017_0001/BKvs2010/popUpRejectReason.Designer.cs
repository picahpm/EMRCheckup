namespace BKvs2010
{
    partial class popUpRejectReason
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
            this.btnOK = new System.Windows.Forms.Button();
            this.txtRejectReason = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(383, 223);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtRejectReason
            // 
            this.txtRejectReason.BackColor = System.Drawing.Color.White;
            this.txtRejectReason.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRejectReason.Location = new System.Drawing.Point(12, 12);
            this.txtRejectReason.Multiline = true;
            this.txtRejectReason.Name = "txtRejectReason";
            this.txtRejectReason.ReadOnly = true;
            this.txtRejectReason.Size = new System.Drawing.Size(446, 202);
            this.txtRejectReason.TabIndex = 144;
            // 
            // popUpRejectReason
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(470, 258);
            this.Controls.Add(this.txtRejectReason);
            this.Controls.Add(this.btnOK);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MinimumSize = new System.Drawing.Size(320, 229);
            this.Name = "popUpRejectReason";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reason";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtRejectReason;


    }
}