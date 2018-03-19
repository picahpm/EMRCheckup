namespace BKvs2010
{
    partial class frmLogin
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtusername = new System.Windows.Forms.TextBox();
            this.txtpassword = new System.Windows.Forms.TextBox();
            this.CBSite = new UserControlLibrary.ComboAutoDropDownWidth();
            this.BtnLogin = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.lbAlertMsg = new UserControlLibrary.LabelAutoResizeFont();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(9, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "UserName :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(13, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Password :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(19, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "HPC Site :";
            // 
            // txtusername
            // 
            this.txtusername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtusername.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtusername.Location = new System.Drawing.Point(92, 94);
            this.txtusername.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtusername.Name = "txtusername";
            this.txtusername.Size = new System.Drawing.Size(289, 23);
            this.txtusername.TabIndex = 0;
            this.txtusername.TextChanged += new System.EventHandler(this.txtusername_TextChanged);
            // 
            // txtpassword
            // 
            this.txtpassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtpassword.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtpassword.Location = new System.Drawing.Point(92, 124);
            this.txtpassword.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtpassword.Name = "txtpassword";
            this.txtpassword.PasswordChar = '*';
            this.txtpassword.Size = new System.Drawing.Size(289, 23);
            this.txtpassword.TabIndex = 1;
            // 
            // CBSite
            // 
            this.CBSite.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.CBSite.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CBSite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBSite.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.CBSite.FormattingEnabled = true;
            this.CBSite.Location = new System.Drawing.Point(92, 154);
            this.CBSite.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CBSite.Name = "CBSite";
            this.CBSite.RightClickDelete = true;
            this.CBSite.Size = new System.Drawing.Size(289, 24);
            this.CBSite.TabIndex = 2;
            // 
            // BtnLogin
            // 
            this.BtnLogin.Location = new System.Drawing.Point(92, 192);
            this.BtnLogin.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnLogin.Name = "BtnLogin";
            this.BtnLogin.Size = new System.Drawing.Size(87, 25);
            this.BtnLogin.TabIndex = 4;
            this.BtnLogin.Text = "Login";
            this.BtnLogin.UseVisualStyleBackColor = true;
            this.BtnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(185, 192);
            this.BtnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(87, 25);
            this.BtnCancel.TabIndex = 5;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(DBCheckup.mst_user_type);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Location = new System.Drawing.Point(12, 10);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(369, 43);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(302, 196);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(79, 16);
            this.linkLabel1.TabIndex = 8;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "User Manual";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // lbAlertMsg
            // 
            this.lbAlertMsg.AutoResizeFont = true;
            this.lbAlertMsg.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbAlertMsg.ForeColor = System.Drawing.Color.Red;
            this.lbAlertMsg.Location = new System.Drawing.Point(12, 62);
            this.lbAlertMsg.MaxFontSize = 11.5F;
            this.lbAlertMsg.Name = "lbAlertMsg";
            this.lbAlertMsg.Size = new System.Drawing.Size(369, 21);
            this.lbAlertMsg.TabIndex = 7;
            this.lbAlertMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmLogin
            // 
            this.AcceptButton = this.BtnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(393, 242);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.lbAlertMsg);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnLogin);
            this.Controls.Add(this.CBSite);
            this.Controls.Add(this.txtpassword);
            this.Controls.Add(this.txtusername);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EMR Check-Up";
            this.Load += new System.EventHandler(this.frmLogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtusername;
        private System.Windows.Forms.TextBox txtpassword;
        private UserControlLibrary.ComboAutoDropDownWidth CBSite;
        private System.Windows.Forms.Button BtnLogin;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private UserControlLibrary.LabelAutoResizeFont lbAlertMsg;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}