namespace CallDataTakeCare
{
    partial class Form1
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
            this.btnStartTime = new System.Windows.Forms.Button();
            this.lberror = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnStartTime
            // 
            this.btnStartTime.Location = new System.Drawing.Point(382, 221);
            this.btnStartTime.Name = "btnStartTime";
            this.btnStartTime.Size = new System.Drawing.Size(92, 26);
            this.btnStartTime.TabIndex = 2;
            this.btnStartTime.Text = "Stop Retrieve";
            this.btnStartTime.UseVisualStyleBackColor = true;
            this.btnStartTime.Click += new System.EventHandler(this.btnStartTime_Click);
            // 
            // lberror
            // 
            this.lberror.BackColor = System.Drawing.Color.White;
            this.lberror.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lberror.ForeColor = System.Drawing.Color.Red;
            this.lberror.Location = new System.Drawing.Point(74, 9);
            this.lberror.Name = "lberror";
            this.lberror.Size = new System.Drawing.Size(400, 201);
            this.lberror.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Message :";
            // 
            // RetrieveTextFileFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 259);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lberror);
            this.Controls.Add(this.btnStartTime);
            this.Name = "RetrieveTextFileFrm";
            this.Text = "Call Data From Take Care";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStartTime;
        private System.Windows.Forms.Label lberror;
        private System.Windows.Forms.Label label1;
    }
}

