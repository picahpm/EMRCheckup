namespace BKvs2010.Usercontrols
{
    partial class PrototypeFavoriteTextBox
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
            this.panel5 = new System.Windows.Forms.Panel();
            this.AddFabvorite = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtResult = new BKvs2010.Usercontrols.IntelliSenseTextBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.AddFabvorite);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(310, 0);
            this.panel5.Margin = new System.Windows.Forms.Padding(0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(45, 55);
            this.panel5.TabIndex = 29;
            // 
            // AddFabvorite
            // 
            this.AddFabvorite.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.AddFabvorite.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AddFabvorite.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.AddFabvorite.ForeColor = System.Drawing.Color.Black;
            this.AddFabvorite.Image = global::BKvs2010.Properties.Resources.add_to_favorites;
            this.AddFabvorite.Location = new System.Drawing.Point(2, 0);
            this.AddFabvorite.Name = "AddFabvorite";
            this.AddFabvorite.Size = new System.Drawing.Size(43, 41);
            this.AddFabvorite.TabIndex = 24;
            this.AddFabvorite.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.AddFabvorite.UseVisualStyleBackColor = false;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.txtResult);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(310, 55);
            this.panel4.TabIndex = 24;
            // 
            // txtResult
            // 
            this.txtResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtResult.Dictionary = null;
            this.txtResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResult.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtResult.Location = new System.Drawing.Point(0, 0);
            this.txtResult.Margin = new System.Windows.Forms.Padding(0);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.RightClickDelete = true;
            this.txtResult.ShortcutsEnabled = false;
            this.txtResult.Size = new System.Drawing.Size(310, 55);
            this.txtResult.TabIndex = 22;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel3.Controls.Add(this.panel4, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel5, 2, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(355, 55);
            this.tableLayoutPanel3.TabIndex = 28;
            // 
            // PrototypeFavoriteTextBox
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tableLayoutPanel3);
            this.Name = "PrototypeFavoriteTextBox";
            this.Size = new System.Drawing.Size(355, 55);
            this.panel5.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button AddFabvorite;
        private System.Windows.Forms.Panel panel4;
        private IntelliSenseTextBox txtResult;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;

    }
}
