using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace BKvs2010.Usercontrols
{
    [DefaultProperty("LableText")]
    public partial class ExamResultUI : UserControl
    {
        public event OnBtnFavoriteClick BtnFavoriteClick;
        public delegate void OnBtnFavoriteClick(object sender, string e);
        private void _btnFavoriteClick(string e)
        {
            if (BtnFavoriteClick == null) return;
            BtnFavoriteClick(this, e);
        }

        public event OnRightClickDropDown RightClickDropDown;
        public delegate void OnRightClickDropDown(object sender, string e);
        private void _rightClickDropDown(string e)
        {
            if (RightClickDropDown == null) return;
            RightClickDropDown(this, e);
        }

        public ExamResultUI()
        {
            InitializeComponent();
            txtResult.BtnFavoriteClick += txtResult_BtnFavoriteClick;
            txtResult.RightClickDropDown += txtResult_RightClickDropDown;
            panelSummary.TagChanged += panelSummary_TagChanged;
        }
        private void panelSummary_TagChanged(object sender, EventArgs e)
        {
            foreach (Binding bind in this.DataBindings)
            {
                if (bind.PropertyName == "SummaryFlag")
                {
                    bind.WriteValue();
                }
            }
        }
        private void txtResult_BtnFavoriteClick(object sender, string e)
        {
            _btnFavoriteClick(e);
        }
        private void txtResult_RightClickDropDown(object sender, string e)
        {
            _rightClickDropDown(e);
        }

        public string AutoCompleteType { get; set; }
        public List<string> AutoCompleteListThList
        {
            get { return txtResult.Dictionary; }
            set { txtResult.Dictionary = value; }
        }

        private Language _Language = Language.TH;
        public Language Language
        {
            get { return _Language; }
            set
            {
                if (value != _Language)
                {
                    if (value == Language.TH)
                    {
                        txtResult.Visible = true;
                        //txtResult.Text = _ResultTH;
                    }
                    else if (value == Language.EN)
                    {
                        txtResult.Visible = false;
                        //txtResult.Text = _ResultEN;
                    }
                    _Language = value;
                }
            }
        }

        private Color _LableBackGroundColor = Color.LightGreen;
        public Color LableBackGroundColor
        {
            get { return _LableBackGroundColor; }
            set
            {
                if (value != _LableBackGroundColor)
                {
                    lbDesc.BackColor = value;
                    panelSummary.BackColor = value;
                    _LableBackGroundColor = value;
                }
            }
        }

        private bool _ReadOnly = false;
        public bool ReadOnly
        {
            get { return _ReadOnly; }
            set
            {
                if (value != _ReadOnly)
                {
                    txtResult.Enabled = !value;
                    panelSummary.ReadOnly = value;
                    _ReadOnly = value;
                }
            }
        }

        [Bindable(true)]
        public char? SummaryFlag
        {
            get
            {
                if (panelSummary.Tag == null)
                {
                    return null;
                }
                else
                {
                    return Convert.ToChar(panelSummary.Tag);
                }
            }
            set
            {
                txtResult.Enabled = value != null;
                if (value == null)
                {
                    panelSummary.Tag = (char?)null;
                }
                else
                {
                    panelSummary.Tag = Convert.ToChar(value);
                }
            }
        }

        [Bindable(true)]
        public string ResultTH
        {
            get { return txtResult.Text; }
            set
            {
                if (value != txtResult.Text)
                {
                    txtResult.Text = value;
                }
            }
        }

        [Browsable(true)]
        public string LableText
        {
            get { return lbDesc.Text; }
            set { lbDesc.Text = value; }
        }

        private void txtResult_TextChanged(object sender, EventArgs e)
        {
            BKvs2010.Usercontrols.FavoriteDoctorTextBox txtBox = (BKvs2010.Usercontrols.FavoriteDoctorTextBox)sender;
            foreach (Binding bind in this.DataBindings)
            {
                if (bind.PropertyName == "ResultTH")
                {
                    bind.WriteValue();
                }
            }
        }

        public List<SplitResult> GetListStringTh()
        {
            return txtResult.GetListWord();
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            txtResult.Font = Font;
        }
    }
}