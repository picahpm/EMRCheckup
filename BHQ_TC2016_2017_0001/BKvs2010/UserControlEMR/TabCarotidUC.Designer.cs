namespace BKvs2010.UserControlEMR
{
    partial class TabCarotidUC
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.carotidUC1 = new BKvs2010.UserControlEMR.CarotidUC();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.carotidDuplexReport1 = new BKvs2010.UserControlEMR.CarotidDuplexReport();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.autoCompleteUC1 = new UserControlLibrary.TextBoxAutoComplete();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(999, 580);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(991, 551);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "ผลจากการตรวจเครื่องมือ";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoScrollMinSize = new System.Drawing.Size(1126, 615);
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.carotidUC1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(985, 545);
            this.panel1.TabIndex = 1;
            // 
            // carotidUC1
            // 
            this.carotidUC1.BackColor = System.Drawing.Color.Transparent;
            this.carotidUC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.carotidUC1.Location = new System.Drawing.Point(0, 0);
            this.carotidUC1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.carotidUC1.MinimumSize = new System.Drawing.Size(1126, 615);
            this.carotidUC1.Name = "carotidUC1";
            this.carotidUC1.PatientRegis = null;
            this.carotidUC1.Size = new System.Drawing.Size(1126, 615);
            this.carotidUC1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.AutoScroll = true;
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(997, 593);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Carotid Duplex Report";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.AutoScrollMinSize = new System.Drawing.Size(902, 733);
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.carotidDuplexReport1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(991, 587);
            this.panel2.TabIndex = 0;
            // 
            // carotidDuplexReport1
            // 
            this.carotidDuplexReport1.BackColor = System.Drawing.Color.Transparent;
            this.carotidDuplexReport1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.carotidDuplexReport1.Location = new System.Drawing.Point(0, 0);
            this.carotidDuplexReport1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.carotidDuplexReport1.MinimumSize = new System.Drawing.Size(1126, 585);
            this.carotidDuplexReport1.Name = "carotidDuplexReport1";
            this.carotidDuplexReport1.PatientRegis = null;
            this.carotidDuplexReport1.Size = new System.Drawing.Size(1126, 733);
            this.carotidDuplexReport1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1005, 619);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.autoCompleteUC1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(999, 27);
            this.panel3.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.tabControl1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 36);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(999, 580);
            this.panel4.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.LightGreen;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Location = new System.Drawing.Point(0, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 22);
            this.label7.TabIndex = 157;
            this.label7.Text = "Doctor Name :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // autoCompleteUC1
            // 
            this.autoCompleteUC1.DataSource = null;
            this.autoCompleteUC1.DisplayMember = "";
            this.autoCompleteUC1.DropDownWidth = 0;
            this.autoCompleteUC1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.autoCompleteUC1.FreeText = false;
            this.autoCompleteUC1.Location = new System.Drawing.Point(98, 4);
            this.autoCompleteUC1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.autoCompleteUC1.MinTextSearch = 2;
            this.autoCompleteUC1.Name = "autoCompleteUC1";
            this.autoCompleteUC1.SelectedItem = null;
            this.autoCompleteUC1.SelectedValue = null;
            this.autoCompleteUC1.Size = new System.Drawing.Size(369, 22);
            this.autoCompleteUC1.TabIndex = 156;
            this.autoCompleteUC1.ValueMember = "";
            // 
            // TabCarotidUC
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "TabCarotidUC";
            this.Size = new System.Drawing.Size(1005, 619);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel1;
        private CarotidUC carotidUC1;
        private System.Windows.Forms.Panel panel2;
        private CarotidDuplexReport carotidDuplexReport1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label7;
        private UserControlLibrary.TextBoxAutoComplete autoCompleteUC1;
    }
}
