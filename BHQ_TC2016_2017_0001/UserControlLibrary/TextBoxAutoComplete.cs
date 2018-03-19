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
    public partial class TextBoxAutoComplete : UserControl
    {
        public TextBoxAutoComplete()
        {
            InitializeComponent();
            txtBox = new TextBox();
            txtBox.Dock = DockStyle.Fill;
            txtBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtBox.AutoCompleteMode = AutoCompleteMode.Append;
            txtBox.TextChanged += new EventHandler(txtBox_TextChanged);
            txtBox.KeyDown += new KeyEventHandler(txtBox_KeyDown);
            txtBox.Leave += new EventHandler(txtBox_LostFocus);
            txtBox.Resize += new EventHandler(txtBox_Resize);
            txtBox.MouseWheel += new MouseEventHandler(txtBox_MouseWheel);
            this.Controls.Add(txtBox);
            cmbBox = new UserControlLibrary.ComboAutoDropDownWidth();
            cmbBox.TabStop = false;
            cmbBox.DropDownHeight = 200;
            cmbBox.SelectionChangeCommitted += new EventHandler(cmbBox_SelectionChangeCommitted);
            this.Controls.Add(cmbBox);
        }

        public delegate void OnSelectedValueChanged(object sender, object e);
        public event OnSelectedValueChanged SelectedValueChanged;
        private void _SelectedValueChanged(object e)
        {
            // Make sure someone is listening to event
            if (SelectedValueChanged == null) return;
            SelectedValueChanged(this, e);
        }

        public bool AutoDropDownWidth
        {
            get { return cmbBox.AutoDropDownWidth; }
            set { cmbBox.AutoDropDownWidth = value; }
        }

        TextBox txtBox;
        UserControlLibrary.ComboAutoDropDownWidth cmbBox;

        [Bindable(true)]
        public string Text
        {
            get { return this.txtBox.Text; }
            set
            {
                if (FreeText)
                {
                    txtBox.TextChanged -= txtBox_TextChanged;
                    txtBox.Text = value;
                    txtBox.TextChanged += txtBox_TextChanged;
                }
                else
                {
                    txtBox.Text = value;
                }
            }
        }

        public bool Multiline
        {
            get { return txtBox.Multiline; }
            set { txtBox.Multiline = value; }
        }

        private bool _ReadOnly;
        public bool ReadOnly
        {
            get { return _ReadOnly; }
            set
            {
                _ReadOnly = value;
                txtBox.ReadOnly = _ReadOnly;
            }
        }

        private string txtSearch
        {
            set
            {
                try
                {
                    _SelectedItem = null;
                    _SelectedValue = null;
                    if (_DataSource != null)
                    {
                        if (value.Length >= _MinTextSearch)
                        {
                            var cmbSource = DataTableSource.AsEnumerable()
                                                           .Where(x => x.Field<string>(_DisplayMember).Trim().ToUpper().Contains(value.ToUpper()))
                                                           .ToList();

                            if (cmbSource.Count() > 0)
                            {
                                cmbBox.DataSource = cmbSource.CopyToDataTable();
                                cmbBox.DisplayMember = _DisplayMember;
                                cmbBox.ValueMember = _ValueMember;
                                if (DropDownWidth > 0)
                                {
                                    cmbBox.DropDownWidth = DropDownWidth;
                                }
                                cmbBox.DroppedDown = true;
                            }
                            else
                            {
                                cmbBox.DroppedDown = false;
                                if (value.Trim() == "")
                                {
                                    setItem(null);
                                }
                            }
                        }
                        else
                        {
                            cmbBox.DroppedDown = false;
                            if (value.Trim() == "")
                            {
                                setItem(null);
                            }
                        }
                    }
                }
                catch
                {
                    setItem(null);
                }
            }
        }
        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txtSearch = txt.Text.Trim();
        }
        public void txtBox_LostFocus(object sender, EventArgs e)
        {
            if (cmbBox.DroppedDown)
            {
                if (!cmbBox.Focused)
                {
                    var cmbSel = cmbBox.Items[0];
                    setItem(cmbSel);
                    cmbBox.DroppedDown = false;
                }
            }
            else
            {
                string txt = txtBox.Text;
                checkText(txt);
            }
            foreach (Binding bind in DataBindings)
            {
                if (bind.PropertyName == "Text")
                {
                    bind.WriteValue();
                }
            }
        }
        private void txtBox_Resize(object sender, EventArgs e)
        {
            this.Height = txtBox.Height;
        }
        private void txtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (cmbBox.DroppedDown)
            {
                switch (e.KeyCode)
                {
                    case Keys.Down:
                        if (cmbBox.SelectedIndex < cmbBox.Items.Count - 1)
                        {
                            cmbBox.SelectedIndex = cmbBox.SelectedIndex + 1;
                        }
                        e.Handled = true;
                        break;
                    case Keys.Up:
                        if (cmbBox.SelectedIndex > 0)
                        {
                            cmbBox.SelectedIndex = cmbBox.SelectedIndex - 1;
                        }
                        e.Handled = true;
                        break;
                    case Keys.Enter:
                        setItem(cmbBox.SelectedItem);
                        txtBox.SelectionStart = 0;
                        txtBox.SelectionLength = txtBox.Text.Length;
                        cmbBox.DroppedDown = false;
                        e.Handled = true;
                        break;
                }
            }
        }
        private void txtBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (cmbBox.DroppedDown)
            {
                ((HandledMouseEventArgs)e).Handled = true;
                if (e.Delta > 0)
                {
                    if (cmbBox.SelectedIndex > 0)
                    {
                        cmbBox.SelectedIndex = cmbBox.SelectedIndex - 1;
                    }
                }
                else
                {
                    if (cmbBox.SelectedIndex < cmbBox.Items.Count - 1)
                    {
                        cmbBox.SelectedIndex = cmbBox.SelectedIndex + 1;
                    }
                }
            }
        }
        private void cmbBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            setItem(cmbBox.SelectedItem);
            txtBox.Focus();
        }

        public int DropDownWidth { get; set; }
        private int _MinTextSearch = 2;
        public int MinTextSearch
        {
            get { return _MinTextSearch; }
            set
            {
                if (_MinTextSearch != value)
                {
                    _MinTextSearch = value;
                }
            }
        }
        private string _DisplayMember = "";
        public string DisplayMember
        {
            get { return _DisplayMember; }
            set
            {
                _DisplayMember = value;
            }
        }
        private string _ValueMember = "";
        public string ValueMember
        {
            get { return _ValueMember; }
            set
            {
                _ValueMember = value;
            }
        }
        private DataTable DataTableSource;
        private object _DataSource;
        public object DataSource
        {
            get { return _DataSource; }
            set
            {
                try
                {
                    setItem(null);
                    if (value != null)
                    {
                        bool canConv = false;
                        if (value is ICollection)
                        {
                            if (((ICollection)value).Count > 0)
                            {
                                canConv = true;
                            }
                        }
                        else
                        {
                            canConv = true;
                        }
                        if (canConv == true)
                        {
                            if (value is IEnumerable)
                            {
                                var result = ((IEnumerable)value).GetEnumerator();
                                result.MoveNext();
                                if (result.Current != null)
                                {
                                    DataTableSource = new DataTable();
                                    Type myType = result.Current.GetType();
                                    foreach (PropertyInfo po in myType.GetProperties())
                                    {
                                        try
                                        {
                                            DataTableSource.Columns.Add(po.Name, po.PropertyType);
                                        }
                                        catch
                                        {
                                            DataTableSource.Columns.Add(po.Name, Nullable.GetUnderlyingType(
                                                                                    po.PropertyType) ?? po.PropertyType);
                                        }
                                    }
                                    do
                                    {
                                        DataRow dr = DataTableSource.NewRow();
                                        foreach (PropertyInfo po in myType.GetProperties())
                                        {
                                            if (po.Name == _DisplayMember)
                                            {
                                                if (typeof(string) == po.GetType())
                                                {
                                                    var poValue = po.GetValue(result.Current, null);
                                                    if (poValue == null)
                                                    {
                                                        dr[po.Name] = "";
                                                    }
                                                    else
                                                    {
                                                        dr[po.Name] = po.GetValue(result.Current, null);
                                                    }
                                                }
                                                else
                                                {
                                                    var poValue = po.GetValue(result.Current, null);
                                                    if (poValue == null)
                                                    {
                                                        dr[po.Name] = DBNull.Value;
                                                    }
                                                    else
                                                    {
                                                        dr[po.Name] = po.GetValue(result.Current, null);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                var poValue = po.GetValue(result.Current, null);
                                                if (poValue == null)
                                                {
                                                    dr[po.Name] = DBNull.Value;
                                                }
                                                else
                                                {
                                                    dr[po.Name] = po.GetValue(result.Current, null);
                                                }
                                            }
                                        }
                                        DataTableSource.Rows.Add(dr);
                                    }
                                    while (result.MoveNext());
                                }
                            }
                        }
                    }
                    _DataSource = value;
                }
                catch
                {
                    _DataSource = null;
                }
            }
        }
        public bool FreeText { get; set; }

        private object _SelectedItem;
        public object SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                try
                {
                    if (value != null)
                    {
                        if (!value.Equals(_SelectedItem))
                        {
                            var dis = TypeDescriptor.GetProperties(value)[_DisplayMember].GetValue(value);
                            txtBox.TextChanged -= new EventHandler(txtBox_TextChanged);
                            if (dis != null)
                            {
                                txtBox.Text = dis.ToString();
                            }
                            else
                            {
                                txtBox.Text = "";
                            }
                            txtBox.TextChanged += new EventHandler(txtBox_TextChanged);
                            _SelectedValue = TypeDescriptor.GetProperties(value)[_ValueMember].GetValue(value);
                            foreach (Binding bind in DataBindings)
                            {
                                if (bind.PropertyName == "SelectedValue")
                                {
                                    bind.WriteValue();
                                }
                            }
                            _SelectedItem = value;
                        }
                    }
                    else
                    {
                        setItem(null);
                    }
                }
                catch
                {
                    setItem(null);
                }
            }
        }
        private object _SelectedValue;

        [Bindable(true)]
        public object SelectedValue
        {
            get { return _SelectedValue; }
            set
            {
                if (value != null)
                {
                    if (!value.Equals(_SelectedValue))
                    {
                        if (DataTableSource == null)
                        {
                            setItem(null);
                        }
                        else if (DataTableSource.Rows.Count == 0)
                        {
                            setItem(null);
                        }
                        else
                        {
                            var cmbSource = DataTableSource.AsEnumerable()
                                                           .Where(x => x.Field<object>(_ValueMember).Equals(value))
                                                           .FirstOrDefault();
                            setItem(cmbSource);
                        }
                    }
                }
                else
                {
                    setItem(null);
                }
            }
        }

        private void checkText(string txt)
        {
            try
            {
                if (_DataSource != null)
                {
                    var result = ((IEnumerable)_DataSource).GetEnumerator();
                    result.MoveNext();
                    bool matchSource = false;
                    do
                    {
                        var dis = TypeDescriptor.GetProperties(result.Current)[_DisplayMember].GetValue(result.Current);
                        if (dis.ToString().ToUpper().Equals(txt.ToUpper()))
                        {
                            var val = TypeDescriptor.GetProperties(result.Current)[_ValueMember].GetValue(result.Current);
                            _SelectedValue = val;
                            foreach (Binding bind in DataBindings)
                            {
                                if (bind.PropertyName == "SelectedValue")
                                {
                                    bind.WriteValue();
                                }
                            }
                            txtBox.TextChanged -= new EventHandler(txtBox_TextChanged);
                            if (dis == null)
                            {
                                txtBox.Text = "";
                            }
                            else
                            {
                                txtBox.Text = dis.ToString();
                            }
                            txtBox.TextChanged += new EventHandler(txtBox_TextChanged);
                            _SelectedItem = result.Current;
                            matchSource = true;
                            break;
                        }
                    }
                    while (result.MoveNext());
                    if (!FreeText)
                    {
                        if (!matchSource)
                        {
                            setItem(null);
                        }
                    }
                    else
                    {
                        if (!matchSource)
                        {
                            _SelectedItem = null;
                            _SelectedValue = null;
                            foreach (Binding bind in DataBindings)
                            {
                                if (bind.PropertyName == "SelectedValue")
                                {
                                    bind.WriteValue();
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (!FreeText)
                    {
                        setItem(null);
                    }
                    else
                    {
                        _SelectedItem = null;
                        _SelectedValue = null;
                        foreach (Binding bind in DataBindings)
                        {
                            if (bind.PropertyName == "SelectedValue")
                            {
                                bind.WriteValue();
                            }
                        }
                    }
                }
            }
            catch
            {

            }
        }
        private void setItem(object item)
        {
            try
            {
                if (item == null)
                {
                    txtBox.TextChanged -= new EventHandler(txtBox_TextChanged);
                    txtBox.Text = "";
                    txtBox.TextChanged += new EventHandler(txtBox_TextChanged);
                    _SelectedItem = null;
                    _SelectedValue = null;
                    foreach (Binding bind in DataBindings)
                    {
                        if (bind.PropertyName == "SelectedValue")
                        {
                            bind.WriteValue();
                        }
                    }
                    _SelectedValueChanged(null);
                }
                else
                {
                    object itemVal;
                    if (item.GetType() == typeof(DataRow))
                    {
                        DataRow row = (DataRow)item;
                        itemVal = row.Field<object>(_ValueMember);
                    }
                    else
                    {
                        itemVal = TypeDescriptor.GetProperties(item)[_ValueMember].GetValue(item);
                    }

                    var result = ((IEnumerable)_DataSource).GetEnumerator();
                    
                    result.MoveNext();
                    do
                    {
                        var val = TypeDescriptor.GetProperties(result.Current)[_ValueMember].GetValue(result.Current);
                        if (val.Equals(itemVal))
                        {
                            _SelectedValue = val;
                            foreach (Binding bind in DataBindings)
                            {
                                if (bind.PropertyName == "SelectedValue")
                                {
                                    bind.WriteValue();
                                }
                            }
                            var dis = TypeDescriptor.GetProperties(result.Current)[_DisplayMember].GetValue(result.Current);
                            txtBox.TextChanged -= new EventHandler(txtBox_TextChanged);
                            if (dis == null)
                            {
                                txtBox.Text = "";
                            }
                            else
                            {
                                txtBox.Text = dis.ToString();
                            }
                            txtBox.TextChanged += new EventHandler(txtBox_TextChanged);
                            _SelectedItem = result.Current;
                            _SelectedValueChanged(result.Current);
                            break;
                        }
                    }
                    while (result.MoveNext());
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            try
            {
                txtBox.Font = this.Font;
            }
            catch
            {

            }
        }
        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            this.txtBox.Enabled = this.Enabled;
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            try
            {
                txtBox.Width = this.Width;
                cmbBox.Width = this.Width;

                txtBox.Height = this.Height;
            }
            catch
            {

            }
        }
    }
}
