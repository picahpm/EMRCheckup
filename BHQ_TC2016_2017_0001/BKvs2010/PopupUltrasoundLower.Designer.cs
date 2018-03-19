namespace BKvs2010
{
    partial class PopupUltrasoundLower
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnBeforeStation = new System.Windows.Forms.Button();
            this.btnAskMeLater = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnAfterStation = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(60, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(300, 44);
            this.label1.TabIndex = 0;
            this.label1.Text = "ผู้รับบริการยังไม่ได้ตรวจ Ultrasound Lower ผู้รับบริการพร้อมตรวจหลังจาก Station น" +
                "ี้หรือไม่?";
            // 
            // btnBeforeStation
            // 
            this.btnBeforeStation.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnBeforeStation.ForeColor = System.Drawing.Color.Red;
            this.btnBeforeStation.Location = new System.Drawing.Point(118, 69);
            this.btnBeforeStation.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnBeforeStation.Name = "btnBeforeStation";
            this.btnBeforeStation.Size = new System.Drawing.Size(175, 30);
            this.btnBeforeStation.TabIndex = 1;
            this.btnBeforeStation.Text = "พร้อมตรวจ ณ ตอนนี้";
            this.btnBeforeStation.UseVisualStyleBackColor = true;
            this.btnBeforeStation.Click += new System.EventHandler(this.btnBeforeStation_Click);
            // 
            // btnAskMeLater
            // 
            this.btnAskMeLater.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnAskMeLater.Location = new System.Drawing.Point(118, 145);
            this.btnAskMeLater.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAskMeLater.Name = "btnAskMeLater";
            this.btnAskMeLater.Size = new System.Drawing.Size(175, 30);
            this.btnAskMeLater.TabIndex = 2;
            this.btnAskMeLater.Text = "ไม่พร้อมตรวจ";
            this.btnAskMeLater.UseVisualStyleBackColor = true;
            this.btnAskMeLater.Click += new System.EventHandler(this.btnAskMeLater_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(18, 23);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(31, 31);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // btnAfterStation
            // 
            this.btnAfterStation.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnAfterStation.ForeColor = System.Drawing.Color.Blue;
            this.btnAfterStation.Location = new System.Drawing.Point(118, 107);
            this.btnAfterStation.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAfterStation.Name = "btnAfterStation";
            this.btnAfterStation.Size = new System.Drawing.Size(175, 30);
            this.btnAfterStation.TabIndex = 4;
            this.btnAfterStation.Text = "พร้อมตรวจหลังออกจากห้อง";
            this.btnAfterStation.UseVisualStyleBackColor = true;
            this.btnAfterStation.Click += new System.EventHandler(this.btnAferStation_Click);
            // 
            // PopupUltrasoundLower
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(378, 184);
            this.ControlBox = false;
            this.Controls.Add(this.btnAfterStation);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnAskMeLater);
            this.Controls.Add(this.btnBeforeStation);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(384, 208);
            this.Name = "PopupUltrasoundLower";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Call Queue Alert";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBeforeStation;
        private System.Windows.Forms.Button btnAskMeLater;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnAfterStation;
    }
}