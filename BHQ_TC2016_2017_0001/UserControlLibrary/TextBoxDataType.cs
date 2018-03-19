using System;
using System.Windows.Forms;

namespace UserControlLibrary
{
    public class TextBoxDataType : TextBox
    {
        public TextBoxDataType()
        {

        }

        public enum TypeData
        {
            Integer,
            Decimal,
            String
        }
        private TypeData _DataType;
        public TypeData DataType
        {
            get { return _DataType; }
            set
            {
                if (value != _DataType)
                {
                    _DataType = value;
                }
            }
        }

        public new string Text
        {
            get { return base.Text; }
            set
            {
                if (value != base.Text)
                {
                    switch (_DataType)
                    {
                        case TypeData.String:
                            base.Text = value.ConvertToString();
                            break;
                        case TypeData.Integer:
                            base.Text = value.ConvertToStringInteger();
                            break;
                        case TypeData.Decimal:
                            base.Text = value.ConvertToStringDouble();
                            break;
                    }
                }
            }
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            int cInt = Convert.ToInt32(e.KeyChar);
            switch (_DataType)
            {
                case TypeData.Decimal:
                    if (cInt == 46)
                    {
                        if (base.Text.Contains(".") && !base.Text.Substring(base.SelectionStart, base.SelectionLength).Contains("."))
                        {
                            e.Handled = true;
                        }
                    }
                    else if (cInt == 45)
                    {
                        if (this.Text.Contains("-") && !this.Text.Substring(this.SelectionStart, this.SelectionLength).Contains("-"))
                        {
                            e.Handled = true;
                        }
                    }
                    else if (!(cInt >= 48 && cInt <= 57 || cInt == 46 || cInt == 8))
                    {
                        e.Handled = true;
                    }
                    break;
                case TypeData.Integer:
                    if (cInt == 46)
                    {
                        e.Handled = true;
                    }
                    else if (cInt == 45)
                    {
                        if (this.Text.Contains("-") && !this.Text.Substring(this.SelectionStart, this.SelectionLength).Contains("-"))
                        {
                            e.Handled = true;
                        }
                    }
                    else if (!(cInt >= 48 && cInt <= 57 || cInt == 8))
                    {
                        e.Handled = true;
                    }
                    break;
            }
            base.OnKeyPress(e);
        }

        private const int WM_PASTE = 0x032;
        protected override void WndProc(ref Message m)
        {
            if (m.Msg != WM_PASTE)
            {
                base.WndProc(ref m);
            }
            else
            {
                switch (_DataType)
                {
                    case TypeData.String:
                        base.Text = Clipboard.GetText().ConvertToString();
                        break;
                    case TypeData.Integer:
                        base.Text = Clipboard.GetText().ConvertToStringInteger();
                        break;
                    case TypeData.Decimal:
                        base.Text = Clipboard.GetText().ConvertToStringDouble();
                        break;
                }
            }
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.Text = base.Text.Trim();
            base.OnLostFocus(e);
        }

        protected override void OnBindingContextChanged(EventArgs e)
        {
            base.OnBindingContextChanged(e);
            foreach (Binding bind in this.DataBindings)
            {
                if (bind.PropertyName == "Text")
                {
                    bind.Parse -= new ConvertEventHandler(bind_Parse);
                    bind.Parse += new ConvertEventHandler(bind_Parse);
                    bind.Format -= new ConvertEventHandler(bind_Format);
                    bind.Format += new ConvertEventHandler(bind_Format);
                }
            }
        }
        private void bind_Format(object sender, ConvertEventArgs e)
        {
            try
            {
                if (e.Value == null)
                {
                    e.Value = "";
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void bind_Parse(object sender, ConvertEventArgs e)
        {
            try
            {
                var val = e.Value.ToString();
                if (val == "")
                {
                    e.Value = null;
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
