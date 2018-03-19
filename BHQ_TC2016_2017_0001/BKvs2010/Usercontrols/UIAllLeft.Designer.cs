namespace BKvs2010.Usercontrols
{
    partial class UIAllLeft
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.uiUserprofile1 = new BKvs2010.Usercontrols.UIUserprofile();
            this.uiMapping1 = new BKvs2010.Usercontrols.UIMapping();
            this.uiWaitList1 = new BKvs2010.Usercontrols.UIWaitList();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.uiUserprofile1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.uiMapping1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.uiWaitList1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 439F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(275, 900);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // uiUserprofile1
            // 
            this.uiUserprofile1.BackColor = System.Drawing.Color.LightCyan;
            this.uiUserprofile1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiUserprofile1.Location = new System.Drawing.Point(3, 3);
            this.uiUserprofile1.Name = "uiUserprofile1";
            this.uiUserprofile1.Size = new System.Drawing.Size(269, 433);
            this.uiUserprofile1.TabIndex = 5;
            // 
            // uiMapping1
            // 
            this.uiMapping1.BackColor = System.Drawing.Color.White;
            this.uiMapping1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.uiMapping1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiMapping1.Location = new System.Drawing.Point(3, 742);
            this.uiMapping1.Name = "uiMapping1";
            this.uiMapping1.Size = new System.Drawing.Size(269, 155);
            this.uiMapping1.TabIndex = 1;
            // 
            // uiWaitList1
            // 
            this.uiWaitList1.ableDoubleClick = false;
            this.uiWaitList1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.uiWaitList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiWaitList1.Location = new System.Drawing.Point(3, 442);
            this.uiWaitList1.Name = "uiWaitList1";
            this.uiWaitList1.Size = new System.Drawing.Size(269, 294);
            this.uiWaitList1.TabIndex = 2;
            this.uiWaitList1.OnWaitingSuccessProcess += new BKvs2010.Usercontrols.UIWaitList.OnSuccessProcess(this.uiWaitList1_OnWaitingSuccessProcess);
            // 
            // UIAllLeft
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UIAllLeft";
            this.Size = new System.Drawing.Size(275, 900);
            this.Load += new System.EventHandler(this.UIAllLeft_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UIMapping uiMapping1;
        private UIWaitList uiWaitList1;
        private UIUserprofile uiUserprofile1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
