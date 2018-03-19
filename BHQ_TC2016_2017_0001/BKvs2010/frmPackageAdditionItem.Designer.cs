namespace BKvs2010
{
    partial class frmPackageAdditionItem
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gvPackageAdditionItem = new System.Windows.Forms.DataGridView();
            this.index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AddItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Del = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tpai_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gvPackageAdditionItem)).BeginInit();
            this.SuspendLayout();
            // 
            // gvPackageAdditionItem
            // 
            this.gvPackageAdditionItem.AllowUserToAddRows = false;
            this.gvPackageAdditionItem.BackgroundColor = System.Drawing.Color.White;
            this.gvPackageAdditionItem.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gvPackageAdditionItem.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvPackageAdditionItem.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gvPackageAdditionItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvPackageAdditionItem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.index,
            this.AddItem,
            this.Del,
            this.tpai_id});
            this.gvPackageAdditionItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvPackageAdditionItem.Location = new System.Drawing.Point(0, 0);
            this.gvPackageAdditionItem.Name = "gvPackageAdditionItem";
            this.gvPackageAdditionItem.RowHeadersVisible = false;
            this.gvPackageAdditionItem.Size = new System.Drawing.Size(488, 394);
            this.gvPackageAdditionItem.TabIndex = 0;
            this.gvPackageAdditionItem.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvPackageAdditionItem_CellClick);
            this.gvPackageAdditionItem.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.gvPackageAdditionItem_DataBindingComplete);
            // 
            // index
            // 
            this.index.DataPropertyName = "index";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.index.DefaultCellStyle = dataGridViewCellStyle2;
            this.index.HeaderText = "No.";
            this.index.Name = "index";
            this.index.Width = 35;
            // 
            // AddItem
            // 
            this.AddItem.DataPropertyName = "AddItem";
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.AddItem.DefaultCellStyle = dataGridViewCellStyle3;
            this.AddItem.HeaderText = "Addition Item";
            this.AddItem.Name = "AddItem";
            this.AddItem.Width = 350;
            // 
            // Del
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Del.DefaultCellStyle = dataGridViewCellStyle4;
            this.Del.HeaderText = "Delete";
            this.Del.Name = "Del";
            this.Del.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Del.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Del.Text = "delete";
            this.Del.UseColumnTextForButtonValue = true;
            // 
            // tpai_id
            // 
            this.tpai_id.DataPropertyName = "tpai_id";
            this.tpai_id.HeaderText = "id";
            this.tpai_id.Name = "tpai_id";
            this.tpai_id.Visible = false;
            // 
            // frmPackageAdditionItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 394);
            this.Controls.Add(this.gvPackageAdditionItem);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmPackageAdditionItem";
            this.Text = "Package and Addition Item";
            ((System.ComponentModel.ISupportInitialize)(this.gvPackageAdditionItem)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gvPackageAdditionItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn index;
        private System.Windows.Forms.DataGridViewTextBoxColumn AddItem;
        private System.Windows.Forms.DataGridViewButtonColumn Del;
        private System.Windows.Forms.DataGridViewTextBoxColumn tpai_id;
    }
}