namespace BKvs2010
{
    partial class DialogPHM
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.personalHealthManagmentUC1 = new BKvs2010.UserControlEMR.PersonalHealthManagmentUC();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbAlertMsg = new UserControlLibrary.LabelAutoResizeFont();
            this.btnSave = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(983, 742);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.personalHealthManagmentUC1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(983, 700);
            this.panel1.TabIndex = 0;
            // 
            // personalHealthManagmentUC1
            // 
            this.personalHealthManagmentUC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.personalHealthManagmentUC1.Location = new System.Drawing.Point(0, 0);
            this.personalHealthManagmentUC1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.personalHealthManagmentUC1.Name = "personalHealthManagmentUC1";
            this.personalHealthManagmentUC1.PatientRegis = null;
            this.personalHealthManagmentUC1.Size = new System.Drawing.Size(983, 700);
            this.personalHealthManagmentUC1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lbAlertMsg);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 700);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(983, 42);
            this.panel2.TabIndex = 1;
            // 
            // lbAlertMsg
            // 
            this.lbAlertMsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbAlertMsg.AutoResizeFont = true;
            this.lbAlertMsg.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbAlertMsg.ForeColor = System.Drawing.Color.Red;
            this.lbAlertMsg.Location = new System.Drawing.Point(112, 6);
            this.lbAlertMsg.MaxFontSize = 11.5F;
            this.lbAlertMsg.Name = "lbAlertMsg";
            this.lbAlertMsg.Size = new System.Drawing.Size(859, 30);
            this.lbAlertMsg.TabIndex = 87;
            this.lbAlertMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(12, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(94, 32);
            this.btnSave.TabIndex = 86;
            this.btnSave.Text = "Save Data";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // DialogPHM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(983, 742);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "DialogPHM";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dialog Personal Health Management";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private UserControlEMR.PersonalHealthManagmentUC personalHealthManagmentUC1;
        private UserControlLibrary.LabelAutoResizeFont lbAlertMsg;
        private System.Windows.Forms.Button btnSave;
    }
}