namespace BKvs2010
{
    partial class frmPopupRemark
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
            this.txtValue = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtValue
            // 
            this.txtValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtValue.Location = new System.Drawing.Point(7, 12);
            this.txtValue.Multiline = true;
            this.txtValue.Name = "txtValue";
            this.txtValue.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtValue.Size = new System.Drawing.Size(191, 20);
            this.txtValue.TabIndex = 3;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(201, 8);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(67, 29);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // frmPopupRemark
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(274, 45);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.btnOK);
            this.Name = "frmPopupRemark";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Remark";
            this.Load += new System.EventHandler(this.frmPopupRemark_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Button btnOK;
    }
}