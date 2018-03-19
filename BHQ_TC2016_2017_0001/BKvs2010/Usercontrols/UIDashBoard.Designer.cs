namespace BKvs2010.Usercontrols
{
    partial class UIDashBoard
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
            this.pnDash = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnDash
            // 
            this.pnDash.AutoScroll = true;
            this.pnDash.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnDash.Location = new System.Drawing.Point(14, 14);
            this.pnDash.Margin = new System.Windows.Forms.Padding(14);
            this.pnDash.Name = "pnDash";
            this.pnDash.Size = new System.Drawing.Size(1182, 328);
            this.pnDash.TabIndex = 0;
            // 
            // UIDashBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnDash);
            this.Name = "UIDashBoard";
            this.Padding = new System.Windows.Forms.Padding(14, 14, 0, 14);
            this.Size = new System.Drawing.Size(1196, 356);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnDash;

    }
}
