using System;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using System.Collections;

namespace UserControlLibrary
{
    public partial class AutoCompleteTextBox : TextBox
    {
        public AutoCompleteTextBox() : base()
        {
            Visible = true;

            cBox = new ComboAutoDropDownWidth();
            cBox.Parent = this;
            cBox.SelectionChangeCommitted += cBox_SelectionChangeCommitted;
            cBox.Visible = false;
            cBox.TabStop = false;
            cBox.DropDownHeight = 200;
            Controls.Add(cBox);
            cBox.BringToFront();
        }
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            cBox.Font = Font;
        }
        private bool flagSetText = false;
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            if (!flagSetText)
            {
                string txtSearch = Text.Trim();
                AutoComplete(txtSearch);
            }
        }
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (cBox.DroppedDown)
            {
                if (e.Delta > 0)
                {
                    if (cBox.SelectedIndex > 0) cBox.SelectedIndex = cBox.SelectedIndex - 1;
                }
                else
                {
                    if (cBox.SelectedIndex < cBox.Items.Count - 1) cBox.SelectedIndex = cBox.SelectedIndex + 1;
                }
                ((HandledMouseEventArgs)e).Handled = true;
            }
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            switch (e.KeyData)
            {
                case Keys.Down:
                    if (cBox.DroppedDown && cBox.SelectedIndex < cBox.Items.Count - 1) cBox.SelectedIndex = cBox.SelectedIndex + 1;
                    e.Handled = true;
                    break;
                case Keys.Up:
                    if (cBox.DroppedDown && cBox.SelectedIndex > 0) cBox.SelectedIndex = cBox.SelectedIndex - 1;
                    e.Handled = true;
                    break;
                case Keys.Escape:
                    if (cBox.DroppedDown) cBox.DroppedDown = false;
                    e.Handled = true;
                    break;
            }
        }
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (cBox.DroppedDown)
                {
                    _SetItem(cBox.SelectedItem);
                    SelectionStart = 0;
                    SelectionLength = Text.Length;
                    cBox.DroppedDown = false;
                }
                e.Handled = true;
            }
            base.OnKeyPress(e);
        }
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            if (FreeText)
            {
                string str = Text.Trim();
                object obj = SearchByText(str);
                _SetItem(obj);
                if (cBox.DroppedDown) cBox.DroppedDown = false;
            }
            else
            {
                if (cBox.DroppedDown)
                {
                    if (!cBox.Focused)
                    {
                        _SetItem(cBox.SelectedItem);
                        cBox.DroppedDown = false;
                    }
                }
                else
                {
                    string str = Text.Trim();
                    object obj = SearchByText(str);
                    _SetItem(obj);
                }
            }
            Text = Text.Trim();
        }

        private void cBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _SetItem(cBox.SelectedItem);
            Focus();
        }

        private ComboAutoDropDownWidth cBox;

        public bool AutoDropDownWidth
        {
            get { return cBox.AutoDropDownWidth; }
            set 
            {
                cBox.AutoDropDownWidth = value;
            }
        }

        private object _DataSource;
        public object DataSource
        {
            get { return _DataSource; }
            set { _DataSource = value; }
        }

        public string DisplayMember
        {
            get { return cBox.DisplayMember; }
            set { cBox.DisplayMember = value; }
        }
        public string ValueMember
        {
            get { return cBox.ValueMember; }
            set { cBox.ValueMember = value; }
        }

        private object _SelectedValue;
        [Bindable(true)]
        public object SelectedValue
        {
            get { return _SelectedValue; }
            set
            {
                object obj = SearchByValue(value);
                _SetItem(obj);
            }
        }

        private object _SelectedItem;
        public object SelectedItem
        {
            get { return _SelectedItem; }
        }

        [Bindable(true)]
        public new string Text
        {
            get { return base.Text; }
            set
            {
                string txt = value.Trim();
                object obj = SearchByText(txt);
                _SetItem(obj);
                if (FreeText)
                {
                    base.Text = value;
                }
            }
        }

        private int _MinTextSearch = 2;
        public int MinTextSearch
        {
            get { return _MinTextSearch; }
            set { _MinTextSearch = value; }
        }

        public bool FreeText { get; set; }

        public int DropDownHeight
        {
            get { return cBox.DropDownHeight; }
            set { cBox.DropDownHeight = value; }
        }

        private void AutoComplete(string txt)
        {
            cBox.Items.Clear();
            if (txt.Length >= _MinTextSearch)
            {
                if (_DataSource != null)
                {
                    if (_DataSource is IEnumerable || (_DataSource is ICollection && ((ICollection)_DataSource).Count > 0))
                    {
                        var result = ((IEnumerable)_DataSource).GetEnumerator();
                        result.MoveNext();
                        if (result.Current != null)
                        {
                            do
                            {
                                try
                                {
                                    var val = TypeDescriptor.GetProperties(result.Current)[cBox.DisplayMember].GetValue(result.Current);
                                    if (val != null)
                                    {
                                        string str = val.ToString();
                                        if (str.ToUpper().Contains(txt.ToUpper()))
                                        {
                                            cBox.Items.Add(result.Current);
                                        }
                                    }
                                }
                                catch
                                {

                                }
                            }
                            while (result.MoveNext());
                        }
                    }
                }
            }
            cBox.DroppedDown = cBox.Items.Count > 0;
        }
        private void _SetItem(object obj)
        {
            flagSetText = true;
            if (obj == null)
            {
                if (_SelectedItem != null)
                {
                    _SelectedItem = null;
                    _SelectedValue = null;
                    if (!FreeText)
                    {
                        base.Text = "";
                    }
                    foreach (Binding bind in DataBindings)
                    {
                        if (bind.PropertyName == "SelectedValue" || bind.PropertyName == "Text")
                        {
                            bind.WriteValue();
                        }
                    }
                    _OnSelectedValueChanged(new EventAutoCompleteChangedArgs()
                    {
                        SelectedItem = _SelectedItem,
                        SelectedValue = _SelectedValue,
                        SelectedText = base.Text
                    });
                }
                else
                {
                    if (!FreeText)
                    {
                        base.Text = "";
                    }
                }
            }
            else
            {
                if (_SelectedItem == null || !obj.Equals(_SelectedItem))
                {
                    _SelectedItem = obj;
                    _SelectedValue = TypeDescriptor.GetProperties(obj)[cBox.ValueMember].GetValue(obj);
                    var dis = TypeDescriptor.GetProperties(obj)[cBox.DisplayMember].GetValue(obj);
                    if (dis != null)
                    {
                        base.Text = dis.ToString();
                    }
                    else
                    {
                        base.Text = "";
                    }
                    foreach (Binding bind in DataBindings)
                    {
                        if (bind.PropertyName == "SelectedValue" || bind.PropertyName == "Text")
                        {
                            bind.WriteValue();
                        }
                    }
                    _OnSelectedValueChanged(new EventAutoCompleteChangedArgs()
                    {
                        SelectedItem = _SelectedItem,
                        SelectedValue = _SelectedValue,
                        SelectedText = base.Text
                    });
                }
            }
            flagSetText = false;
        }
        private object SearchByText(string txt)
        {
            if (!string.IsNullOrEmpty(txt))
            {
                if (_DataSource != null)
                {
                    if (_DataSource is IEnumerable || (_DataSource is ICollection && ((ICollection)_DataSource).Count > 0))
                    {
                        var result = _DataSource as IEnumerable;
                        if (result != null)
                        {
                            foreach (object element in result)
                            {
                                try
                                {
                                    var val = TypeDescriptor.GetProperties(element)[cBox.DisplayMember].GetValue(element);
                                    if (val != null)
                                    {
                                        string str = val.ToString();
                                        if (str.ToUpper().Trim() == txt.ToUpper())
                                        {
                                            return element;
                                        }
                                    }
                                }
                                catch
                                {

                                }
                            }
                        }
                        //var result = ((IEnumerable)_DataSource).GetEnumerator();
                        //result.MoveNext();
                        //if (result.Current != null)
                        //{
                        //    do
                        //    {
                        //        try
                        //        {
                        //            var val = TypeDescriptor.GetProperties(result.Current)[cBox.DisplayMember].GetValue(result.Current);
                        //            if (val != null)
                        //            {
                        //                string str = val.ToString();
                        //                if (str.ToUpper().Trim() == txt.ToUpper())
                        //                {
                        //                    return result.Current;
                        //                }
                        //            }
                        //        }
                        //        catch
                        //        {

                        //        }
                        //    }
                        //    while (result.MoveNext());
                        //}
                    }
                }
            }
            return null;
        }
        private object SearchByValue(object obj)
        {
            if (obj != null)
            {
                if (_DataSource != null)
                {
                    if (_DataSource is IEnumerable || (_DataSource is ICollection && ((ICollection)_DataSource).Count > 0))
                    {
                        var result = _DataSource as IEnumerable;
                        if (result != null)
                        {
                            foreach (object element in result)
                            {
                                try
                                {
                                    var val = TypeDescriptor.GetProperties(element)[cBox.ValueMember].GetValue(element);
                                    if (val != null)
                                    {
                                        if (val.Equals(obj))
                                        {
                                            return element;
                                        }
                                    }
                                }
                                catch
                                {

                                }
                            }
                        }
                        //var result = ((IEnumerable)_DataSource).GetEnumerator();
                        //result.MoveNext();
                        //try
                        //{
                        //    if (result != null && result.Current != null)
                        //    {
                        //        do
                        //        {
                        //            try
                        //            {
                        //                var val = TypeDescriptor.GetProperties(result.Current)[cBox.ValueMember].GetValue(result.Current);
                        //                if (val != null)
                        //                {
                        //                    if (val.Equals(obj))
                        //                    {
                        //                        return result.Current;
                        //                    }
                        //                }
                        //            }
                        //            catch
                        //            {

                        //            }
                        //        }
                        //        while (result.MoveNext());
                        //    }
                        //}
                    }
                }
            }
            return null;
        }

        public delegate void OnSelectedValueChanged(object sender, EventAutoCompleteChangedArgs e);
        public event OnSelectedValueChanged SelectedValueChanged;
        private void _OnSelectedValueChanged(EventAutoCompleteChangedArgs e)
        {
            if (SelectedValueChanged != null)
                SelectedValueChanged(this, e);
        }
    }

    public class EventAutoCompleteChangedArgs : EventArgs
    {
        public object SelectedItem { get; set; }
        public object SelectedValue { get; set; }
        public string SelectedText { get; set; }
    }
}
