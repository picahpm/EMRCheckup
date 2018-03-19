namespace CallDataTakeCareArrived
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
            this.components = new System.ComponentModel.Container();
            this.btnCallData = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnStartTime = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lberror = new System.Windows.Forms.Label();
            this.chkWebservice = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCallData
            // 
            this.btnCallData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCallData.Location = new System.Drawing.Point(769, 12);
            this.btnCallData.Name = "btnCallData";
            this.btnCallData.Size = new System.Drawing.Size(118, 42);
            this.btnCallData.TabIndex = 0;
            this.btnCallData.Text = "Manual Load Data";
            this.btnCallData.UseVisualStyleBackColor = true;
            this.btnCallData.Click += new System.EventHandler(this.btnCallData_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(4, 68);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(570, 322);
            this.dataGridView1.TabIndex = 1;
            // 
            // timer1
            // 
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnStartTime
            // 
            this.btnStartTime.Location = new System.Drawing.Point(12, 12);
            this.btnStartTime.Name = "btnStartTime";
            this.btnStartTime.Size = new System.Drawing.Size(131, 50);
            this.btnStartTime.TabIndex = 2;
            this.btnStartTime.Text = "Start Auto Download";
            this.btnStartTime.UseVisualStyleBackColor = true;
            this.btnStartTime.Click += new System.EventHandler(this.btnStartTime_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(591, 68);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(306, 322);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // lberror
            // 
            this.lberror.ForeColor = System.Drawing.Color.Red;
            this.lberror.Location = new System.Drawing.Point(149, 2);
            this.lberror.Name = "lberror";
            this.lberror.Size = new System.Drawing.Size(614, 60);
            this.lberror.TabIndex = 4;
            this.lberror.Text = "Message";
            // 
            // chkWebservice
            // 
            this.chkWebservice.AutoSize = true;
            this.chkWebservice.Checked = true;
            this.chkWebservice.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWebservice.Location = new System.Drawing.Point(231, 37);
            this.chkWebservice.Name = "chkWebservice";
            this.chkWebservice.Size = new System.Drawing.Size(154, 17);
            this.chkWebservice.TabIndex = 5;
            this.chkWebservice.Text = "Load data from webservice";
            this.chkWebservice.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 395);
            this.Controls.Add(this.chkWebservice);
            this.Controls.Add(this.lberror);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnStartTime);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnCallData);
            this.Name = "Form1";
            this.Text = "Call Data From Take Care (Arrived)";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCallData;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnStartTime;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lberror;
        private System.Windows.Forms.CheckBox chkWebservice;
    }
}

