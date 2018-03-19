using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BKvs2010.Usercontrols
{
    public class FavoriteDoctorTextBox : PrototypeFavoriteTextBox
    {
        public event OnBtnFavoriteClick BtnFavoriteClick;
        public delegate void OnBtnFavoriteClick(object sender, string e);
        private void _btnFavoriteClick(object sender, EventArgs e)
        {
            // we'll explain this in a minute
            if (BtnFavoriteClick != null)
                BtnFavoriteClick(this, Text);
        }

        public FavoriteDoctorTextBox()
        {
            ButtonPosition = PrototypeFavoriteTextBoxButtonPosition.BottomRight;
            base.BtnFavoriteClick += _btnFavoriteClick;
        }

        public List<string> Dictionary { get { return AutoCompleteListThList; } set { AutoCompleteListThList = value; } }

        public string AutoCompleteType { get; set; }
        public int? MutId { get; set; }

        public bool ReadOnly { get { return Enabled; } set { Enabled = value; } }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FavoriteDoctorTextBox
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Name = "FavoriteDoctorTextBox";
            this.ResumeLayout(false);
        }

        
    }
}
