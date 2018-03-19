namespace BKvs2010.Usercontrols
{
    partial class ExamResultUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExamResultUI));
            this.lbDesc = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtResult = new BKvs2010.Usercontrols.FavoriteDoctorTextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelSummary = new UserControlLibrary.PanelRadioBinding();
            this.rdoNormal = new System.Windows.Forms.RadioButton();
            this.rdoAbnormal = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelSummary.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbDesc
            // 
            this.lbDesc.BackColor = System.Drawing.Color.LightGreen;
            this.lbDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbDesc.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbDesc.Location = new System.Drawing.Point(0, 0);
            this.lbDesc.Margin = new System.Windows.Forms.Padding(0);
            this.lbDesc.Name = "lbDesc";
            this.lbDesc.Size = new System.Drawing.Size(250, 22);
            this.lbDesc.TabIndex = 20;
            this.lbDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(411, 66);
            this.tableLayoutPanel1.TabIndex = 23;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tableLayoutPanel3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 22);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(411, 44);
            this.panel3.TabIndex = 25;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(411, 44);
            this.tableLayoutPanel3.TabIndex = 25;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.txtResult);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(411, 44);
            this.panel4.TabIndex = 24;
            // 
            // txtResult
            // 
            this.txtResult.AutoCompleteListThList = null;
            this.txtResult.AutoCompleteType = null;
            this.txtResult.BackColor = System.Drawing.Color.Transparent;
            this.txtResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtResult.ButtonPosition = BKvs2010.Usercontrols.PrototypeFavoriteTextBox.PrototypeFavoriteTextBoxButtonPosition.BottomRight;
            this.txtResult.Dictionary = null;
            this.txtResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResult.Enabled = false;
            this.txtResult.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtResult.ListFabvorite = ((System.Collections.ObjectModel.ObservableCollection<string>)(resources.GetObject("txtResult.ListFabvorite")));
            this.txtResult.Location = new System.Drawing.Point(0, 0);
            this.txtResult.MutId = null;
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.Size = new System.Drawing.Size(411, 44);
            this.txtResult.TabIndex = 22;
            this.txtResult.TextChanged += new System.EventHandler(this.txtResult_TextChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 161F));
            this.tableLayoutPanel2.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(411, 22);
            this.tableLayoutPanel2.TabIndex = 24;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panelSummary);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(250, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(161, 22);
            this.panel2.TabIndex = 1;
            // 
            // panelSummary
            // 
            this.panelSummary.AbleNull = true;
            this.panelSummary.BackColor = System.Drawing.Color.LightGreen;
            this.panelSummary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSummary.Controls.Add(this.rdoNormal);
            this.panelSummary.Controls.Add(this.rdoAbnormal);
            this.panelSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSummary.Location = new System.Drawing.Point(0, 0);
            this.panelSummary.Name = "panelSummary";
            this.panelSummary.ReadOnly = false;
            this.panelSummary.Size = new System.Drawing.Size(161, 22);
            this.panelSummary.TabIndex = 21;
            // 
            // rdoNormal
            // 
            this.rdoNormal.AutoCheck = false;
            this.rdoNormal.AutoSize = true;
            this.rdoNormal.BackColor = System.Drawing.Color.Transparent;
            this.rdoNormal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.rdoNormal.Location = new System.Drawing.Point(9, 2);
            this.rdoNormal.Name = "rdoNormal";
            this.rdoNormal.Size = new System.Drawing.Size(58, 17);
            this.rdoNormal.TabIndex = 2;
            this.rdoNormal.Tag = "N";
            this.rdoNormal.Text = "Normal";
            this.rdoNormal.UseVisualStyleBackColor = false;
            // 
            // rdoAbnormal
            // 
            this.rdoAbnormal.AutoCheck = false;
            this.rdoAbnormal.AutoSize = true;
            this.rdoAbnormal.BackColor = System.Drawing.Color.Transparent;
            this.rdoAbnormal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.rdoAbnormal.Location = new System.Drawing.Point(79, 2);
            this.rdoAbnormal.Name = "rdoAbnormal";
            this.rdoAbnormal.Size = new System.Drawing.Size(70, 17);
            this.rdoAbnormal.TabIndex = 3;
            this.rdoAbnormal.Tag = "A";
            this.rdoAbnormal.Text = "Abnormal";
            this.rdoAbnormal.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbDesc);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(250, 22);
            this.panel1.TabIndex = 0;
            // 
            // ExamResultUI
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ExamResultUI";
            this.Size = new System.Drawing.Size(411, 66);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panelSummary.ResumeLayout(false);
            this.panelSummary.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbDesc;
        private UserControlLibrary.PanelRadioBinding panelSummary;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rdoNormal;
        private System.Windows.Forms.RadioButton rdoAbnormal;
        private BKvs2010.Usercontrols.FavoriteDoctorTextBox txtResult;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panel4;
    }
}
