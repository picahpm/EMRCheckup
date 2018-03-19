using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Forms;

namespace BKvs2010.Usercontrols
{
    public partial class PrototypeFavoriteTextBox : UserControl
    {
        public PrototypeFavoriteTextBox()
        {
            InitializeComponent();
            ListFabvorite = new ObservableCollection<string>();
            txtResult.RightClickDelete = true;
            txtResult.TextChanged += txtResult_TextChanged;
            txtResult.RightClickDropDown += txtResult_RightClickDropDown;
            AddFabvorite.Click += AddFabvorite_Click;
        }
        private void txtResult_TextChanged(object sender, EventArgs e)
        {
            if (TextChanged != null)
                TextChanged(this, e);
        }

        public event EventHandler TextChanged;

        public delegate void OnBtnFavoriteClick(object sender, EventArgs e);
        public event OnBtnFavoriteClick BtnFavoriteClick;
        private void _btnFavoriteClick(object sender, EventArgs e)
        {
            // we'll explain this in a minute
            if (BtnFavoriteClick != null)
                BtnFavoriteClick(this, e);
        }

        public delegate void OnRightClickDropDown(object sender, string e);
        public event OnRightClickDropDown RightClickDropDown;
        private void _rightClickDropDown(string e)
        {
            // we'll explain this in a minute
            if (RightClickDropDown != null)
                RightClickDropDown(this, e);
        }

        public List<SplitResult> GetListStringTh()
        {
            return txtResult.GetListWord();
        }

        public ObservableCollection<string> ListFabvorite { get; set; }

        [Bindable(true)]
        public override string Text
        {
            get
            {
                return txtResult.Text;
            }
            set
            {
                txtResult.Text = value;
            }
        }

        private void txtResult_RightClickDropDown(object sender, string e)
        {
            _rightClickDropDown(e);
        }

        public List<string> AutoCompleteListThList
        {
            get { return txtResult.Dictionary; }
            set { txtResult.Dictionary = value; }
        }
        private void AddFabvorite_Click(object sender, EventArgs e)
        {
            _btnFavoriteClick(sender, e);
        }

        public List<SplitResult> GetListWord()
        {
            return txtResult.GetListWord();
        }

        public enum PrototypeFavoriteTextBoxButtonPosition
        {
            TopRight,
            BottomRight,
            TopLeft,
            BottomLeft
        }
        private PrototypeFavoriteTextBoxButtonPosition _ButtonPosition = PrototypeFavoriteTextBoxButtonPosition.TopRight;
        public PrototypeFavoriteTextBoxButtonPosition ButtonPosition
        {
            get { return _ButtonPosition; }
            set
            {
                if (value != _ButtonPosition)
                {
                    _ButtonPosition = value;
                    switch (value)
                    {
                        case PrototypeFavoriteTextBoxButtonPosition.TopRight:
                            tableLayoutPanel3.ColumnStyles[0].Width = 0;
                            tableLayoutPanel3.ColumnStyles[2].Width = 46;
                            tableLayoutPanel3.Controls.Add(panel5, 2, 0);
                            AddFabvorite.Left = panel5.Width - AddFabvorite.Width;
                            AddFabvorite.Top = 0;
                            //tableLayoutPanel3.SetCellPosition(panel5, new TableLayoutPanelCellPosition(3, 0));
                            //tableLayoutPanel3.SetCellPosition(null, new TableLayoutPanelCellPosition(1, 0));
                            break;
                        case PrototypeFavoriteTextBoxButtonPosition.BottomRight:
                            tableLayoutPanel3.ColumnStyles[0].Width = 0;
                            tableLayoutPanel3.ColumnStyles[2].Width = 46;
                            tableLayoutPanel3.Controls.Add(panel5, 2, 0);
                            AddFabvorite.Left = panel5.Width - AddFabvorite.Width;
                            AddFabvorite.Top = panel5.Height - AddFabvorite.Height;
                            break;
                        case PrototypeFavoriteTextBoxButtonPosition.TopLeft:
                            tableLayoutPanel3.ColumnStyles[0].Width = 46;
                            tableLayoutPanel3.ColumnStyles[2].Width = 0;
                            tableLayoutPanel3.Controls.Add(panel5, 0, 0);
                            AddFabvorite.Left = 0;
                            AddFabvorite.Top = 0;
                            break;
                        case PrototypeFavoriteTextBoxButtonPosition.BottomLeft:
                            tableLayoutPanel3.ColumnStyles[0].Width = 46;
                            tableLayoutPanel3.ColumnStyles[2].Width = 0;
                            tableLayoutPanel3.Controls.Add(panel5, 0, 0);
                            AddFabvorite.Left = 0;
                            AddFabvorite.Top = panel5.Height - AddFabvorite.Height;
                            break;
                    }
                }
            }
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            txtResult.Font = Font;
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            switch (_ButtonPosition)
            {
                case PrototypeFavoriteTextBoxButtonPosition.TopRight:
                    AddFabvorite.Left = panel5.Width - AddFabvorite.Width;
                    AddFabvorite.Top = 0;
                    //tableLayoutPanel3.SetCellPosition(panel5, new TableLayoutPanelCellPosition(3, 0));
                    //tableLayoutPanel3.SetCellPosition(null, new TableLayoutPanelCellPosition(1, 0));
                    break;
                case PrototypeFavoriteTextBoxButtonPosition.BottomRight:
                    AddFabvorite.Left = panel5.Width - AddFabvorite.Width;
                    AddFabvorite.Top = panel5.Height - AddFabvorite.Height;
                    break;
                case PrototypeFavoriteTextBoxButtonPosition.TopLeft:
                    AddFabvorite.Left = 0;
                    AddFabvorite.Top = 0;
                    break;
                case PrototypeFavoriteTextBoxButtonPosition.BottomLeft:
                    AddFabvorite.Left = 0;
                    AddFabvorite.Top = panel5.Height - AddFabvorite.Height;
                    break;
            }
        }
    }
}