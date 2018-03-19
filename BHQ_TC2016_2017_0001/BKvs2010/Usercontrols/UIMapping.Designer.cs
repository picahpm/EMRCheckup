namespace BKvs2010.Usercontrols
{
    partial class UIMapping
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIMapping));
            this.PanelMapping = new System.Windows.Forms.Panel();
            this.lbTitleMapple = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // PanelMapping
            // 
            this.PanelMapping.AutoScroll = true;
            this.PanelMapping.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelMapping.Location = new System.Drawing.Point(0, 21);
            this.PanelMapping.Name = "PanelMapping";
            this.PanelMapping.Size = new System.Drawing.Size(249, 103);
            this.PanelMapping.TabIndex = 0;
            // 
            // lbTitleMapple
            // 
            this.lbTitleMapple.BackColor = System.Drawing.Color.RoyalBlue;
            this.lbTitleMapple.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbTitleMapple.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbTitleMapple.ForeColor = System.Drawing.Color.White;
            this.lbTitleMapple.Location = new System.Drawing.Point(0, 0);
            this.lbTitleMapple.Name = "lbTitleMapple";
            this.lbTitleMapple.Size = new System.Drawing.Size(249, 21);
            this.lbTitleMapple.TabIndex = 2;
            this.lbTitleMapple.Text = "Mapping";
            this.lbTitleMapple.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "photo_portrait.png");
            this.imageList1.Images.SetKeyName(1, "arrow_right_blue.png");
            this.imageList1.Images.SetKeyName(2, "arrow_down_blue.png");
            this.imageList1.Images.SetKeyName(3, "arrow_left_blue.png");
            this.imageList1.Images.SetKeyName(4, "arrow_left_blue.png");
            this.imageList1.Images.SetKeyName(5, "Allowright.png");
            this.imageList1.Images.SetKeyName(6, "arrow_down_blue.png");
            this.imageList1.Images.SetKeyName(7, "arrow_right_blue.png");
            // 
            // UIMapping
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.PanelMapping);
            this.Controls.Add(this.lbTitleMapple);
            this.Name = "UIMapping";
            this.Size = new System.Drawing.Size(249, 124);
            this.Load += new System.EventHandler(this.UIMapping_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelMapping;
        private System.Windows.Forms.Label lbTitleMapple;
        private System.Windows.Forms.ImageList imageList1;
    }
}
