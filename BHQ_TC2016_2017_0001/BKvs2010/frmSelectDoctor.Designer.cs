namespace BKvs2010
{
    partial class frmSelectDoctor
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancelReqDoc = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.labWarning = new System.Windows.Forms.Label();
            this.autoCompDoctor = new UserControlLibrary.TextBoxAutoComplete();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Please Typing Doctor Name";
            // 
            // btnCancelReqDoc
            // 
            this.btnCancelReqDoc.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnCancelReqDoc.Location = new System.Drawing.Point(321, 59);
            this.btnCancelReqDoc.Name = "btnCancelReqDoc";
            this.btnCancelReqDoc.Size = new System.Drawing.Size(215, 26);
            this.btnCancelReqDoc.TabIndex = 2;
            this.btnCancelReqDoc.Text = "Cancel Request Doctor";
            this.btnCancelReqDoc.UseVisualStyleBackColor = true;
            this.btnCancelReqDoc.Click += new System.EventHandler(this.btnCancelReqDoc_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSelect.Location = new System.Drawing.Point(321, 91);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(120, 26);
            this.btnSelect.TabIndex = 3;
            this.btnSelect.Text = "Select Doctor";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnCancel.Location = new System.Drawing.Point(447, 91);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(89, 26);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // labWarning
            // 
            this.labWarning.AutoSize = true;
            this.labWarning.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.labWarning.ForeColor = System.Drawing.Color.Red;
            this.labWarning.Location = new System.Drawing.Point(15, 59);
            this.labWarning.MaximumSize = new System.Drawing.Size(250, 0);
            this.labWarning.Name = "labWarning";
            this.labWarning.Size = new System.Drawing.Size(62, 16);
            this.labWarning.TabIndex = 5;
            this.labWarning.Text = "Warning";
            // 
            // autoCompDoctor
            // 
            this.autoCompDoctor.BackColor = System.Drawing.Color.Transparent;
            this.autoCompDoctor.DataSource = null;
            this.autoCompDoctor.DisplayMember = null;
            this.autoCompDoctor.Location = new System.Drawing.Point(207, 16);
            this.autoCompDoctor.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.autoCompDoctor.MinTextSearch = 2;
            this.autoCompDoctor.Name = "autoCompDoctor";
            this.autoCompDoctor.SelectedValue = null;
            this.autoCompDoctor.Size = new System.Drawing.Size(329, 23);
            this.autoCompDoctor.TabIndex = 0;
            this.autoCompDoctor.ValueMember = null;
            // 
            // frmSelectDoctor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 133);
            this.Controls.Add(this.labWarning);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.btnCancelReqDoc);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.autoCompDoctor);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSelectDoctor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmSelectDoctor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UserControlLibrary.TextBoxAutoComplete autoCompDoctor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancelReqDoc;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label labWarning;
    }
}