namespace BKvs2010
{
    partial class frmCumulative
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlContain = new System.Windows.Forms.Panel();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lblwarning = new System.Windows.Forms.Label();
            this.gvtmp = new System.Windows.Forms.DataGridView();
            this.uiProfileHorizontal1 = new BKvs2010.Usercontrols.UIProfileHorizontal();
            this.lblhead = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbValue = new UserControlLibrary.ComboAutoDropDownWidth();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.Colno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColEN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColTestItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColLab = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColRef = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDDMMYY1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDDMMYY2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDDMMYY3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDDMMYY4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDDMMYY5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.displaychart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvtmp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.displaychart1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pnlContain);
            this.panel2.Controls.Add(this.chart1);
            this.panel2.Controls.Add(this.lblwarning);
            this.panel2.Controls.Add(this.gvtmp);
            this.panel2.Controls.Add(this.uiProfileHorizontal1);
            this.panel2.Controls.Add(this.lblhead);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.cmbValue);
            this.panel2.Controls.Add(this.dgvData);
            this.panel2.Controls.Add(this.displaychart1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(984, 732);
            this.panel2.TabIndex = 235;
            // 
            // pnlContain
            // 
            this.pnlContain.AutoScroll = true;
            this.pnlContain.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlContain.Location = new System.Drawing.Point(702, 117);
            this.pnlContain.Name = "pnlContain";
            this.pnlContain.Size = new System.Drawing.Size(282, 615);
            this.pnlContain.TabIndex = 243;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(735, 183);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(102, 47);
            this.chart1.TabIndex = 244;
            this.chart1.Text = "chart1";
            this.chart1.Visible = false;
            this.chart1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseClick);
            // 
            // lblwarning
            // 
            this.lblwarning.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblwarning.AutoSize = true;
            this.lblwarning.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblwarning.ForeColor = System.Drawing.Color.Red;
            this.lblwarning.Location = new System.Drawing.Point(267, 402);
            this.lblwarning.Name = "lblwarning";
            this.lblwarning.Size = new System.Drawing.Size(44, 18);
            this.lblwarning.TabIndex = 242;
            this.lblwarning.Text = "label1";
            this.lblwarning.Visible = false;
            // 
            // gvtmp
            // 
            this.gvtmp.AllowUserToAddRows = false;
            this.gvtmp.AllowUserToDeleteRows = false;
            this.gvtmp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvtmp.Location = new System.Drawing.Point(716, 183);
            this.gvtmp.Name = "gvtmp";
            this.gvtmp.ReadOnly = true;
            this.gvtmp.RowHeadersVisible = false;
            this.gvtmp.Size = new System.Drawing.Size(39, 23);
            this.gvtmp.TabIndex = 238;
            this.gvtmp.Visible = false;
            // 
            // uiProfileHorizontal1
            // 
            this.uiProfileHorizontal1.BackColor = System.Drawing.SystemColors.Control;
            this.uiProfileHorizontal1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiProfileHorizontal1.Location = new System.Drawing.Point(0, 0);
            this.uiProfileHorizontal1.Name = "uiProfileHorizontal1";
            this.uiProfileHorizontal1.Size = new System.Drawing.Size(984, 117);
            this.uiProfileHorizontal1.TabIndex = 0;
            // 
            // lblhead
            // 
            this.lblhead.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblhead.BackColor = System.Drawing.SystemColors.HotTrack;
            this.lblhead.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblhead.ForeColor = System.Drawing.Color.Transparent;
            this.lblhead.Location = new System.Drawing.Point(2, 119);
            this.lblhead.Name = "lblhead";
            this.lblhead.Size = new System.Drawing.Size(694, 25);
            this.lblhead.TabIndex = 217;
            this.lblhead.Text = "Cumulative ";
            this.lblhead.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(8, 400);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 23);
            this.label2.TabIndex = 232;
            this.label2.Text = "Show Point &Labels:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbValue
            // 
            this.cmbValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbValue.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbValue.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbValue.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cmbValue.FormattingEnabled = true;
            this.cmbValue.Items.AddRange(new object[] {
            "None",
            "ShowValue"});
            this.cmbValue.Location = new System.Drawing.Point(130, 401);
            this.cmbValue.Name = "cmbValue";
            this.cmbValue.Size = new System.Drawing.Size(121, 21);
            this.cmbValue.TabIndex = 231;
            this.cmbValue.SelectedIndexChanged += new System.EventHandler(this.cmbValue_SelectedIndexChanged);
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.BackgroundColor = System.Drawing.Color.White;
            this.dgvData.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Colno,
            this.ColEN,
            this.ColTestItem,
            this.ColValue,
            this.ColUnit,
            this.ColLab,
            this.ColRef,
            this.ColDDMMYY1,
            this.ColDDMMYY2,
            this.ColDDMMYY3,
            this.ColDDMMYY4,
            this.ColDDMMYY5});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvData.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvData.Location = new System.Drawing.Point(0, 147);
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvData.RowHeadersVisible = false;
            this.dgvData.RowHeadersWidth = 50;
            this.dgvData.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(696, 249);
            this.dgvData.TabIndex = 218;
            this.dgvData.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvData_CellClick);
            this.dgvData.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvData_ColumnHeaderMouseClick);
            this.dgvData.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvData_DataBindingComplete);
            // 
            // Colno
            // 
            this.Colno.DataPropertyName = "No";
            this.Colno.HeaderText = "No.";
            this.Colno.Name = "Colno";
            this.Colno.ReadOnly = true;
            this.Colno.Width = 30;
            // 
            // ColEN
            // 
            this.ColEN.DataPropertyName = "EN";
            this.ColEN.HeaderText = "EN";
            this.ColEN.Name = "ColEN";
            this.ColEN.ReadOnly = true;
            this.ColEN.Width = 5;
            // 
            // ColTestItem
            // 
            this.ColTestItem.DataPropertyName = "TestItem";
            this.ColTestItem.HeaderText = "TestItem";
            this.ColTestItem.Name = "ColTestItem";
            this.ColTestItem.ReadOnly = true;
            // 
            // ColValue
            // 
            this.ColValue.DataPropertyName = "Value";
            this.ColValue.HeaderText = "Value";
            this.ColValue.Name = "ColValue";
            this.ColValue.ReadOnly = true;
            // 
            // ColUnit
            // 
            this.ColUnit.DataPropertyName = "Unit";
            this.ColUnit.HeaderText = "Unit";
            this.ColUnit.Name = "ColUnit";
            this.ColUnit.ReadOnly = true;
            this.ColUnit.Width = 60;
            // 
            // ColLab
            // 
            this.ColLab.DataPropertyName = "Lab";
            this.ColLab.HeaderText = "Lab";
            this.ColLab.Name = "ColLab";
            this.ColLab.ReadOnly = true;
            // 
            // ColRef
            // 
            this.ColRef.DataPropertyName = "Ref";
            this.ColRef.HeaderText = "Ref";
            this.ColRef.Name = "ColRef";
            this.ColRef.ReadOnly = true;
            this.ColRef.Width = 60;
            // 
            // ColDDMMYY1
            // 
            this.ColDDMMYY1.DataPropertyName = "DDMMYY1";
            this.ColDDMMYY1.HeaderText = "DDMMYY1";
            this.ColDDMMYY1.Name = "ColDDMMYY1";
            this.ColDDMMYY1.ReadOnly = true;
            this.ColDDMMYY1.Width = 80;
            // 
            // ColDDMMYY2
            // 
            this.ColDDMMYY2.DataPropertyName = "DDMMYY2";
            this.ColDDMMYY2.HeaderText = "DDMMYY2";
            this.ColDDMMYY2.Name = "ColDDMMYY2";
            this.ColDDMMYY2.ReadOnly = true;
            this.ColDDMMYY2.Width = 80;
            // 
            // ColDDMMYY3
            // 
            this.ColDDMMYY3.DataPropertyName = "DDMMYY3";
            this.ColDDMMYY3.HeaderText = "DDMMYY3";
            this.ColDDMMYY3.Name = "ColDDMMYY3";
            this.ColDDMMYY3.ReadOnly = true;
            this.ColDDMMYY3.Width = 80;
            // 
            // ColDDMMYY4
            // 
            this.ColDDMMYY4.DataPropertyName = "DDMMYY4";
            this.ColDDMMYY4.HeaderText = "DDMMYY4";
            this.ColDDMMYY4.Name = "ColDDMMYY4";
            this.ColDDMMYY4.ReadOnly = true;
            this.ColDDMMYY4.Width = 80;
            // 
            // ColDDMMYY5
            // 
            this.ColDDMMYY5.DataPropertyName = "DDMMYY5";
            this.ColDDMMYY5.HeaderText = "DDMMYY5";
            this.ColDDMMYY5.Name = "ColDDMMYY5";
            this.ColDDMMYY5.ReadOnly = true;
            this.ColDDMMYY5.Width = 80;
            // 
            // displaychart1
            // 
            this.displaychart1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.displaychart1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(223)))), ((int)(((byte)(193)))));
            this.displaychart1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            this.displaychart1.BorderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(64)))), ((int)(((byte)(1)))));
            this.displaychart1.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.displaychart1.BorderlineWidth = 2;
            this.displaychart1.BorderSkin.SkinStyle = System.Windows.Forms.DataVisualization.Charting.BorderSkinStyle.Emboss;
            chartArea2.Area3DStyle.Inclination = 40;
            chartArea2.Area3DStyle.IsClustered = true;
            chartArea2.Area3DStyle.IsRightAngleAxes = false;
            chartArea2.Area3DStyle.LightStyle = System.Windows.Forms.DataVisualization.Charting.LightStyle.Realistic;
            chartArea2.Area3DStyle.Perspective = 9;
            chartArea2.Area3DStyle.Rotation = 25;
            chartArea2.Area3DStyle.WallWidth = 3;
            chartArea2.AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
            chartArea2.AxisX.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea2.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea2.AxisY.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
            chartArea2.AxisY.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea2.AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea2.BackColor = System.Drawing.Color.OldLace;
            chartArea2.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            chartArea2.BackSecondaryColor = System.Drawing.Color.White;
            chartArea2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea2.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea2.Name = "Default";
            chartArea2.ShadowColor = System.Drawing.Color.Transparent;
            this.displaychart1.ChartAreas.Add(chartArea2);
            legend2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            legend2.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            legend2.Name = "Legend2";
            legend3.Name = "Legend3";
            this.displaychart1.Legends.Add(legend2);
            this.displaychart1.Legends.Add(legend3);
            this.displaychart1.Location = new System.Drawing.Point(3, 424);
            this.displaychart1.Name = "displaychart1";
            series2.BorderWidth = 3;
            series2.ChartArea = "Default";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            series2.Legend = "Legend2";
            series2.LegendText = "-";
            series2.MarkerSize = 8;
            series2.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series2.Name = "sr";
            series2.ShadowOffset = 2;
            series3.ChartArea = "Default";
            series3.Legend = "Legend3";
            series3.Name = "sr2";
            this.displaychart1.Series.Add(series2);
            this.displaychart1.Series.Add(series3);
            this.displaychart1.Size = new System.Drawing.Size(693, 293);
            this.displaychart1.TabIndex = 219;
            title1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            title1.Name = "Title1";
            this.displaychart1.Titles.Add(title1);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(984, 732);
            this.panel1.TabIndex = 1;
            // 
            // frmCumulative
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 732);
            this.Controls.Add(this.panel1);
            this.Name = "frmCumulative";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cumulative";
            this.Load += new System.EventHandler(this.frmCumulative_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvtmp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.displaychart1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private BKvs2010.Usercontrols.UIProfileHorizontal uiProfileHorizontal1;
        private System.Windows.Forms.Label lblhead;
        private System.Windows.Forms.Label label2;
        private UserControlLibrary.ComboAutoDropDownWidth cmbValue;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.DataVisualization.Charting.Chart displaychart1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView gvtmp;
        private System.Windows.Forms.Label lblwarning;
        private System.Windows.Forms.Panel pnlContain;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Colno;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColEN;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColTestItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColLab;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColRef;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDDMMYY1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDDMMYY2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDDMMYY3;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDDMMYY4;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDDMMYY5;


    }
}