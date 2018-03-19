namespace BKvs2010
{
    partial class frmViewscan
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.positionToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.imageSizeToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.zoomToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblPage = new System.Windows.Forms.Label();
            this.btnFirst = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.imageBox = new imgbox.Windows.Forms.ImageBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.imageBox);
            this.panel1.Location = new System.Drawing.Point(35, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(702, 707);
            this.panel1.TabIndex = 1;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.positionToolStripStatusLabel,
            this.imageSizeToolStripStatusLabel,
            this.zoomToolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 745);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(770, 22);
            this.statusStrip.TabIndex = 3;
            // 
            // positionToolStripStatusLabel
            // 
            this.positionToolStripStatusLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.positionToolStripStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.positionToolStripStatusLabel.Name = "positionToolStripStatusLabel";
            this.positionToolStripStatusLabel.Size = new System.Drawing.Size(4, 17);
            // 
            // imageSizeToolStripStatusLabel
            // 
            this.imageSizeToolStripStatusLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.imageSizeToolStripStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.imageSizeToolStripStatusLabel.Name = "imageSizeToolStripStatusLabel";
            this.imageSizeToolStripStatusLabel.Size = new System.Drawing.Size(4, 17);
            // 
            // zoomToolStripStatusLabel
            // 
            this.zoomToolStripStatusLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.zoomToolStripStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.zoomToolStripStatusLabel.Name = "zoomToolStripStatusLabel";
            this.zoomToolStripStatusLabel.Size = new System.Drawing.Size(4, 17);
            // 
            // lblPage
            // 
            this.lblPage.AutoSize = true;
            this.lblPage.Location = new System.Drawing.Point(702, 9);
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new System.Drawing.Size(35, 13);
            this.lblPage.TabIndex = 4;
            this.lblPage.Text = "label1";
            // 
            // btnFirst
            // 
            this.btnFirst.Location = new System.Drawing.Point(291, 1);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(75, 21);
            this.btnFirst.TabIndex = 6;
            this.btnFirst.Text = "<<";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(372, 1);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 21);
            this.btnNext.TabIndex = 5;
            this.btnNext.Text = ">>";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // imageBox
            // 
            this.imageBox.AutoScroll = true;
            this.imageBox.AutoSize = false;
            this.imageBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBox.Location = new System.Drawing.Point(0, 0);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(702, 707);
            this.imageBox.TabIndex = 0;
            this.imageBox.Zoom = 80;
            this.imageBox.ZoomIncrement = 5;
            this.imageBox.ZoomChanged += new System.EventHandler(this.imageBox_ZoomChanged);
            this.imageBox.Scroll += new System.Windows.Forms.ScrollEventHandler(this.imageBox_Scroll);
            this.imageBox.Resize += new System.EventHandler(this.imageBox_Resize);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(466, 1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 20);
            this.button1.TabIndex = 5;
            this.button1.Text = "chk";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmViewscan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 767);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnFirst);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.lblPage);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "frmViewscan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmViewscan_Load);
            this.panel1.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private imgbox.Windows.Forms.ImageBox imageBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel positionToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel imageSizeToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel zoomToolStripStatusLabel;
        private System.Windows.Forms.Label lblPage;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button button1;

    }
}