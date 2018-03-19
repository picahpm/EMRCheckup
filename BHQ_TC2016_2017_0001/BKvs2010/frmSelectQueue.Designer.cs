namespace BKvs2010
{
    partial class frmSelectQueue
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSelectQueue));
            this.DDQueue = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnok = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DDQueue
            // 
            this.DDQueue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.DDQueue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DDQueue.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.DDQueue.FormattingEnabled = true;
            this.DDQueue.Location = new System.Drawing.Point(5, 29);
            this.DDQueue.Name = "DDQueue";
            this.DDQueue.Size = new System.Drawing.Size(190, 24);
            this.DDQueue.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Order Queue :";
            // 
            // btnok
            // 
            this.btnok.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnok.Image = ((System.Drawing.Image)(resources.GetObject("btnok.Image")));
            this.btnok.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnok.Location = new System.Drawing.Point(0, 66);
            this.btnok.Name = "btnok";
            this.btnok.Size = new System.Drawing.Size(207, 31);
            this.btnok.TabIndex = 0;
            this.btnok.Text = "OK";
            this.btnok.UseVisualStyleBackColor = true;
            this.btnok.Click += new System.EventHandler(this.btnok_Click);
            // 
            // frmSelectQueue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(207, 97);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DDQueue);
            this.Controls.Add(this.btnok);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmSelectQueue";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Queue";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSelectQueue_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSelectQueue_FormClosed);
            this.Load += new System.EventHandler(this.SelectQueue_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnok;
        private System.Windows.Forms.ComboBox DDQueue;
        private System.Windows.Forms.Label label1;
    }
}