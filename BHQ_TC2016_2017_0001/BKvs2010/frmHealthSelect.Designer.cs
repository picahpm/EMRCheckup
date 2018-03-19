namespace BKvs2010
{
    partial class frmHealthSelect
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
            this.GvHealthSelect = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.GvHealthSelect)).BeginInit();
            this.SuspendLayout();
            // 
            // GvHealthSelect
            // 
            this.GvHealthSelect.AllowUserToAddRows = false;
            this.GvHealthSelect.AllowUserToDeleteRows = false;
            this.GvHealthSelect.BackgroundColor = System.Drawing.Color.White;
            this.GvHealthSelect.CausesValidation = false;
            this.GvHealthSelect.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GvHealthSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GvHealthSelect.GridColor = System.Drawing.SystemColors.Control;
            this.GvHealthSelect.Location = new System.Drawing.Point(0, 0);
            this.GvHealthSelect.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GvHealthSelect.Name = "GvHealthSelect";
            this.GvHealthSelect.ReadOnly = true;
            this.GvHealthSelect.RowHeadersVisible = false;
            this.GvHealthSelect.Size = new System.Drawing.Size(284, 362);
            this.GvHealthSelect.TabIndex = 0;
            this.GvHealthSelect.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GvHealthSelect_CellContentDoubleClick);
            // 
            // frmHealthSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 362);
            this.Controls.Add(this.GvHealthSelect);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmHealthSelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SelectHealthProgram";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmHealthSelect_FormClosed);
            this.Load += new System.EventHandler(this.frmHealthSelect_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GvHealthSelect)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView GvHealthSelect;
    }
}