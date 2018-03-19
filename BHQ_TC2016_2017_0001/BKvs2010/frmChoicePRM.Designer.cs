namespace BKvs2010
{
    partial class frmChoicePRM
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
            this.lbQuestions = new System.Windows.Forms.Label();
            this.btnSendBook = new System.Windows.Forms.Button();
            this.btnToC = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbQuestions
            // 
            this.lbQuestions.AutoSize = true;
            this.lbQuestions.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbQuestions.Location = new System.Drawing.Point(61, 34);
            this.lbQuestions.Name = "lbQuestions";
            this.lbQuestions.Size = new System.Drawing.Size(364, 19);
            this.lbQuestions.TabIndex = 4;
            this.lbQuestions.Text = "ผู้รับบริการซื้อ PRM Package คุณต้องการส่งผู้บริการไป";
            // 
            // btnSendBook
            // 
            this.btnSendBook.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSendBook.Location = new System.Drawing.Point(255, 75);
            this.btnSendBook.Name = "btnSendBook";
            this.btnSendBook.Size = new System.Drawing.Size(170, 34);
            this.btnSendBook.TabIndex = 3;
            this.btnSendBook.Text = "Send To Book";
            this.btnSendBook.UseVisualStyleBackColor = true;
            this.btnSendBook.Click += new System.EventHandler(this.btnSendBook_Click);
            // 
            // btnToC
            // 
            this.btnToC.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnToC.ForeColor = System.Drawing.Color.Red;
            this.btnToC.Location = new System.Drawing.Point(65, 75);
            this.btnToC.Name = "btnToC";
            this.btnToC.Size = new System.Drawing.Size(160, 34);
            this.btnToC.TabIndex = 2;
            this.btnToC.Text = "Send To Check point C";
            this.btnToC.UseVisualStyleBackColor = true;
            this.btnToC.Click += new System.EventHandler(this.btnToC_Click);
            // 
            // frmChoicePRM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 156);
            this.Controls.Add(this.lbQuestions);
            this.Controls.Add(this.btnSendBook);
            this.Controls.Add(this.btnToC);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmChoicePRM";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Message Confirm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbQuestions;
        private System.Windows.Forms.Button btnSendBook;
        private System.Windows.Forms.Button btnToC;
    }
}