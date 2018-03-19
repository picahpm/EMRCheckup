namespace BKvs2010
{
    partial class frmPatientStatus
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPatientStatus));
            this.uiFooter1 = new BKvs2010.Usercontrols.UIFooter();
            this.SuspendLayout();
            // 
            // uiFooter1
            // 
            this.uiFooter1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.uiFooter1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiFooter1.Location = new System.Drawing.Point(0, 0);
            this.uiFooter1.Name = "uiFooter1";
            this.uiFooter1.RoomCode = null;
            this.uiFooter1.Size = new System.Drawing.Size(1247, 670);
            this.uiFooter1.TabIndex = 0;
            // 
            // frmPatientStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1247, 670);
            this.Controls.Add(this.uiFooter1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPatientStatus";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Page Patient Status";
            this.Load += new System.EventHandler(this.frmPatientStatus_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Usercontrols.UIFooter uiFooter1;
    }
}