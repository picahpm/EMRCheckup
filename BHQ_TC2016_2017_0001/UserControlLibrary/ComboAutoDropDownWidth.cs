using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using GlobalHook;

namespace UserControlLibrary
{
    public class ComboAutoDropDownWidth : ComboBox
    {
        private bool _RightClickDelete = true;
        public bool RightClickDelete { get { return _RightClickDelete; } set { _RightClickDelete = value; } }
        private Timer ti;
        private string deleteText;

        public event OnRightClickDropDown RightClickDropDown;
        public delegate void OnRightClickDropDown(object sender, string e);
        private void _rightClickDropDown(string e)
        {
            if (RightClickDropDown == null) return;
            RightClickDropDown(this, e);
        }

        const uint WM_SETCURSOR = 32;
        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        private static extern IntPtr SendMessage(
        IntPtr hWnd, uint Msg, int wParam, int lParam);

        private bool _AutoDropDownWidth = true;
        public bool AutoDropDownWidth
        {
            get { return _AutoDropDownWidth; }
            set
            {
                _AutoDropDownWidth = value;
                if (value)
                {
                    DrawMode = System.Windows.Forms.DrawMode.Normal;
                }
                else
                {
                    DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
                }
            }
        }
        private void GlobalMouseRightClick(object sender, MouseEventArgs e)
        {
            if (RightClickDelete)
            {
                if (e.Button == MouseButtons.Right)
                {
                    Point pCmb = PointToScreen(Point.Empty);
                    if (DroppedDown && e.X > pCmb.X && e.X < pCmb.X + DropDownWidth && e.Y > pCmb.Y + Height &&
                        e.Y <
                        pCmb.Y + Height + ((Items.Count > MaxDropDownItems ? MaxDropDownItems : Items.Count) * ItemHeight))
                    {
                        deleteText = GetItemText(SelectedItem);
                        DroppedDown = false;
                        ti = new Timer();
                        ti.Enabled = true;
                        ti.Interval = 1;
                        ti.Tick += ti_Tick;
                        ti.Start();
                    }
                }
            }
        }
        private void ti_Tick(object sender, EventArgs e)
        {
            ti.Stop();
            _rightClickDropDown(deleteText);
            //ContextMenu cm = new ContextMenu();
            //cm.MenuItems.Add("Delete : " + deleteText + " ?", cm_click);
            //cm.Show(Parent, PointToClient(Cursor.Position));
        }
        //private void cm_click(object sender, EventArgs e)
        //{
        //    _rightClickDropDown(deleteText);
        //}

        protected override void OnDropDown(EventArgs e)
        {
            base.OnDropDown(e);
            if (_AutoDropDownWidth)
            {
                int width = DropDownWidth;
                Graphics g = CreateGraphics();
                Font font = Font;
                int vertScrollBarWidth =
                    Items.Count > MaxDropDownItems
                    ? SystemInformation.VerticalScrollBarWidth : 0;

                foreach (object o in Items)
                {
                    string s = GetItemText(o);
                    int newWidth = (int)(((int)g.MeasureString(s, font).Width
                        + vertScrollBarWidth) * 1.1);
                    if (width < newWidth)
                    {
                        width = newWidth;
                    }
                }
                DropDownWidth = width;
            }
            else
            {
                DropDownWidth = 332;
                //SendMessage(Handle, CB_SETITEMHEIGHT, 2, 100); 
            }
            if (DropDownStyle == ComboBoxStyle.DropDown)
                SendMessage(Handle, WM_SETCURSOR, 0, 0);
            HookManager.MouseDown += GlobalMouseRightClick;
        }
        protected override void OnDropDownClosed(EventArgs e)
        {
            base.OnDropDownClosed(e);
            HookManager.MouseDown -= GlobalMouseRightClick;
        }

        public const int CB_SETITEMHEIGHT = 0x0153;
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);
            if (!_AutoDropDownWidth)
            {
                string txt = "";
                if (!string.IsNullOrEmpty(DisplayMember))
                {
                    var val = TypeDescriptor.GetProperties(Items[e.Index])[DisplayMember].GetValue(Items[e.Index]);
                    if (val != null)
                    {
                        txt = val.ToString();
                    }
                }
                else
                {
                    txt = Items[e.Index].ToString();
                }
                int h = (int)e.Graphics.MeasureString(txt, Font, 332).Height;
                Color c = new Color();
                if (e.Index % 2 == 0)
                {
                    c = Color.FromArgb(211, 225, 231);
                    //c = Color.FromArgb(203, 236, 250);
                }
                else
                {
                    c = Color.White;
                }

                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.Blue), e.Bounds);
                }
                else
                {
                    e.Graphics.FillRectangle(new SolidBrush(c), e.Bounds);
                }
                e.Graphics.DrawString(txt, e.Font, new SolidBrush(e.ForeColor), e.Bounds);
                e.DrawFocusRectangle();
            }
        }
        protected override void OnMeasureItem(MeasureItemEventArgs e)
        {
            base.OnMeasureItem(e);
            if (!_AutoDropDownWidth)
            {
                string txt = "";
                if (!string.IsNullOrEmpty(DisplayMember))
                {
                    var val = TypeDescriptor.GetProperties(Items[e.Index])[DisplayMember].GetValue(Items[e.Index]);
                    if (val != null)
                    {
                        txt = val.ToString();
                    }
                }
                else
                {
                    txt = Items[e.Index].ToString();
                }
                e.ItemHeight = (int)e.Graphics.MeasureString(txt, Font, 332).Height;
            }
        }
    }
}
