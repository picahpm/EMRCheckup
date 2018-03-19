using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace UserControlLibrary
{
    public class PanelRadioBinding : Panel
    {
        public PanelRadioBinding()
        {
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }

        public event OnTagChanged TagChanged;
        public delegate void OnTagChanged(object sender, EventArgs e);
        private void _TagChaged()
        {
            if (TagChanged == null) return;
            TagChanged(this, null);
        }

        private object _Tag;
        [Bindable(true)]
        public new object Tag
        {
            get { return _Tag; }
            set
            {
                bool found = false;
                foreach (Control ctrl in Controls)
                {
                    RadioButton curRd = ctrl as RadioButton;
                    if (curRd != null)
                    {
                        if (found)
                        {
                            curRd.Checked = false;
                        }
                        else
                        {
                            if (value == null)
                            {
                                if (curRd.Tag == null)
                                {
                                    curRd.Checked = true;
                                }
                                else
                                {
                                    curRd.Checked = false;
                                }
                            }
                            else
                            {
                                if (value.GetType() == typeof(System.DBNull))
                                {
                                    curRd.Checked = false;
                                }
                                else
                                {
                                    if (curRd.Tag == null)
                                    {
                                        curRd.Checked = false;
                                    }
                                    else
                                    {
                                        object tmpTag = null;
                                        if (value.GetType() == typeof(string))
                                        {
                                            tmpTag = curRd.Tag.ToString();
                                        }
                                        else if (value.GetType() == typeof(char))
                                        {
                                            tmpTag = Convert.ToChar(curRd.Tag);
                                        }
                                        else if (value.GetType() == typeof(char?))
                                        {
                                            tmpTag = (Nullable<char>)(Convert.ToChar(curRd.Tag));
                                        }
                                        else if (value.GetType() == typeof(Nullable<char>))
                                        {
                                            tmpTag = (Nullable<char>)(Convert.ToChar(curRd.Tag));
                                        }
                                        if (tmpTag.Equals(value))
                                        {
                                            curRd.Checked = true;
                                            found = true;
                                        }
                                        else
                                        {
                                            curRd.Checked = false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (found)
                {
                    _Tag = value;
                }
                else
                {
                    _Tag = null;
                }
                foreach (Binding bind in DataBindings)
                {
                    if (bind.PropertyName == "Tag")
                    {
                        bind.WriteValue();
                        break;
                    }
                }
            }
        }

        private Type TagBindingType
        {
            get
            {
                foreach (Binding bind in DataBindings)
                {
                    if (bind.PropertyName == "Tag")
                    {
                        try
                        {
                            var tmp = TypeDescriptor.GetProperties(bind.BindingManagerBase.Current)[bind.BindingMemberInfo.BindingField];
                            Type type = tmp.PropertyType;
                            return type;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
                return null;
            }
        }

        private bool _AbleNull = false;
        public bool AbleNull
        {
            get { return _AbleNull; }
            set
            {
                _AbleNull = value;
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
                    _ReadOnly = value;
                }
            }
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            if (e.Control.GetType() == typeof(RadioButton))
            {
                RadioButton rd = (RadioButton)e.Control;
                rd.Checked = false;
                rd.AutoCheck = false;
                rd.Click += new EventHandler(rd_Click);
            }
        }

        private void rd_Click(object sender, EventArgs e)
        {
            if (!_ReadOnly)
            {
                RadioButton rd = sender as RadioButton;
                object newValue = null;
                if (rd != null)
                {
                    if (!rd.Checked)
                    {
                        foreach (Control ctrl in Controls)
                        {
                            RadioButton curCtrl = ctrl as RadioButton;
                            if (curCtrl != null)
                            {
                                if (curCtrl != rd)
                                {
                                    curCtrl.Checked = false;
                                }
                            }
                        }
                        rd.Checked = true;
                        newValue = rd.Tag;
                    }
                    else
                    {
                        if (_AbleNull)
                        {
                            rd.Checked = false;
                            newValue = null;
                        }
                        else
                        {
                            return;
                        }
                    }

                    if (newValue == null)
                    {
                        _Tag = null;
                    }
                    else
                    {
                        if (TagBindingType == null)
                        {
                            _Tag = newValue;
                        }
                        else if (TagBindingType == typeof(string))
                        {
                            _Tag = newValue.ToString();
                        }
                        else if (TagBindingType == typeof(char))
                        {
                            _Tag = Convert.ToChar(newValue);
                        }
                        else if (TagBindingType == typeof(char?))
                        {
                            _Tag = (Nullable<char>)(Convert.ToChar(newValue));
                        }
                    }
                    _TagChaged();

                    foreach (Binding bind in DataBindings)
                    {
                        if (bind.PropertyName == "Tag")
                        {
                            bind.WriteValue();
                            break;
                        }
                    }
                }
            }
        }

        int _borderWidth = 1;
        //[Browsable(true)]
        //[DefaultValue(1)]
        //public int BorderWidth
        //{
        //    get { return _borderWidth; }
        //    set { _borderWidth = value; Invalidate(); }
        //}

        int _roundCornerRadius = 10;
        //[Browsable(true)]
        //[DefaultValue(15)]
        //public int RoundCornerRadius
        //{
        //    get { return _roundCornerRadius; }
        //    set { _roundCornerRadius = Math.Abs(value); Invalidate(); }
        //}

        Color _borderColor = Color.Silver;
        //[Browsable(true)]
        //[DefaultValue("Color.Silver")]
        //public Color BorderColor
        //{
        //    get { return _borderColor; }
        //    set { _borderColor = value; Invalidate(); }
        //}

        private bool _borderRadius = false;
        public bool BorderRadius
        {
            get { return _borderRadius; }
            set
            {
                _borderRadius = value;
                Invalidate();
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            if (_borderRadius)
            {
                int tmpSoundCornerRadius = Math.Min(Math.Min(_roundCornerRadius, this.Width - 2), this.Height - 2);
                if (this.Width > 1 && this.Height > 1)
                {
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                    Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
                    Rectangle rect2 = new Rectangle(_borderWidth, _borderWidth, rect.Width - (_borderWidth * 2), rect.Height - (_borderWidth * 2));

                    GraphicsPath graphPath = PanelBorderGraphics.GetRoundPath(rect, tmpSoundCornerRadius);
                    GraphicsPath graphPath2 = PanelBorderGraphics.GetRoundPath(rect2, tmpSoundCornerRadius);

                    e.Graphics.DrawPath(new Pen(Color.FromArgb(180, this._borderColor), _borderWidth), graphPath);
                    e.Graphics.DrawPath(new Pen(Color.White, _borderWidth), graphPath2);
                }
            }
        }
    }

    internal class PanelBorderGraphics
    {
        public static GraphicsPath GetRoundPath(Rectangle r, int depth)
        {
            GraphicsPath graphPath = new GraphicsPath();

            graphPath.AddArc(r.X, r.Y, depth, depth, 180, 90);
            graphPath.AddArc(r.X + r.Width - depth, r.Y, depth, depth, 270, 90);
            graphPath.AddArc(r.X + r.Width - depth, r.Y + r.Height - depth, depth, depth, 0, 90);
            graphPath.AddArc(r.X, r.Y + r.Height - depth, depth, depth, 90, 90);
            graphPath.AddLine(r.X, r.Y + r.Height - depth, r.X, r.Y + depth / 2);

            return graphPath;
        }
    }
}
