namespace BKvs2010
{
    partial class DialogAssessmentAndPlan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogAssessmentAndPlan));
            this.lbAlertMsg = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabAssessmentAndPlanUC1 = new BKvs2010.UserControlEMR.TabAssessmentAndPlanUC();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbAlertMsg
            // 
            this.lbAlertMsg.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbAlertMsg.ForeColor = System.Drawing.Color.Red;
            this.lbAlertMsg.Location = new System.Drawing.Point(163, 6);
            this.lbAlertMsg.Name = "lbAlertMsg";
            this.lbAlertMsg.Size = new System.Drawing.Size(217, 23);
            this.lbAlertMsg.TabIndex = 94;
            this.lbAlertMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(84, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(73, 25);
            this.btnCancel.TabIndex = 93;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(9, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(73, 25);
            this.btnSave.TabIndex = 92;
            this.btnSave.Text = "Save";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tabAssessmentAndPlanUC1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(975, 742);
            this.tableLayoutPanel1.TabIndex = 95;
            // 
            // tabAssessmentAndPlanUC1
            // 
            this.tabAssessmentAndPlanUC1.AutoScroll = true;
            this.tabAssessmentAndPlanUC1.AutoScrollMinSize = new System.Drawing.Size(975, 644);
            this.tabAssessmentAndPlanUC1.BackColor = System.Drawing.Color.Transparent;
            this.tabAssessmentAndPlanUC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabAssessmentAndPlanUC1.Location = new System.Drawing.Point(0, 0);
            this.tabAssessmentAndPlanUC1.Margin = new System.Windows.Forms.Padding(0);
            this.tabAssessmentAndPlanUC1.MinimumSize = new System.Drawing.Size(975, 650);
            this.tabAssessmentAndPlanUC1.Name = "tabAssessmentAndPlanUC1";
            this.tabAssessmentAndPlanUC1.PatientRegis = null;
            this.tabAssessmentAndPlanUC1.Size = new System.Drawing.Size(975, 707);
            this.tabAssessmentAndPlanUC1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.lbAlertMsg);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 707);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(975, 35);
            this.panel1.TabIndex = 1;
            // 
            // DialogAssessmentAndPlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(975, 742);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "DialogAssessmentAndPlan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DialogAssessmentAndPlan";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControlEMR.TabAssessmentAndPlanUC tabAssessmentAndPlanUC1;
        private System.Windows.Forms.Label lbAlertMsg;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
    }
}