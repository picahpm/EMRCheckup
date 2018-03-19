using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace BKvs2010.Usercontrols
{
    public partial class IntelliSenseTextBox : TextBox
    {
        public bool RightClickDelete
        {
            get { return combobox.RightClickDelete; }
            set { combobox.RightClickDelete = value; }
        }

        public event OnRightClickDropDown RightClickDropDown;
        public delegate void OnRightClickDropDown(object sender, string e);
        private void _RightClickDropDown(string e)
        {
            if (RightClickDropDown == null) return;
            RightClickDropDown(this, e);
        }

        #region Class Members
        List<string> dictionary;
        UserControlLibrary.ComboAutoDropDownWidth combobox;
        #endregion

        #region Extern functions
        [DllImport("user32")]
        private static extern int GetCaretPos(out Point p);

        #endregion

        #region Constructors

        public IntelliSenseTextBox()
            : base()
        {
            Visible = true;

            ShortcutsEnabled = false;
            combobox = new UserControlLibrary.ComboAutoDropDownWidth();
            combobox.Parent = this;
            combobox.SelectionChangeCommitted += combobox_SelectionChangeCommitted;
            combobox.RightClickDropDown += combobox_RightClickDropDown;
            combobox.Visible = false;
            Controls.Add(combobox);
            combobox.BringToFront();
            dictionary = new List<string>();
        }

        private void combobox_RightClickDropDown(object sender, string e)
        {
            _RightClickDropDown(e);
        }
        #endregion

        #region Properties

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            Text = Text.Trim();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyValue == (Char)Keys.Down)
            {
                if (combobox.DroppedDown == true)
                {
                    if (combobox.SelectedIndex < combobox.Items.Count - 1) combobox.SelectedIndex = combobox.SelectedIndex + 1;
                    e.Handled = true;
                }
                else
                {
                    //if (SelectionStart == 0 || Text.Substring(SelectionStart - 1, 1) == " ")
                    //{
                    //    Point cp;
                    //    GetCaretPos(out cp);

                    //    List<string> lstTemp = new List<string>();

                    //    combobox.SetBounds(cp.X, cp.Y, 150, 50);

                    //    lstTemp = dictionary
                    //        .Select(r => r)
                    //        .OrderBy(x => x)
                    //        .ToList();

                    //    combobox.DataSource = lstTemp;

                    //    if (lstTemp.Count != 0)
                    //    {
                    //        combobox.DroppedDown = true;
                    //    }
                    //    else
                    //    {
                    //        combobox.DroppedDown = false;
                    //    }
                    //    e.Handled = true;
                    //}
                }
            }
            else if (e.KeyValue == (Char)Keys.Up)
            {
                if (combobox.DroppedDown == true)
                {
                    if (combobox.SelectedIndex > 0) combobox.SelectedIndex = combobox.SelectedIndex - 1;
                    e.Handled = true;
                }
            }
        }
        bool changeKeyEnter = false;
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (combobox.DroppedDown == true)
                {
                    SetText();
                }
                else
                {
                    changeKeyEnter = true;
                    Text += Environment.NewLine;
                    SelectionStart = Text.Length;
                }
                e.Handled = true;
            }
            else if (e.KeyChar == (char)Keys.Escape)
            {
                combobox.DroppedDown = false;
                e.Handled = true;
            }
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.X))
            {
                Cut();
            }
            else if (e.KeyData == (Keys.Control | Keys.C))
            {
                Copy();
            }
            else if (e.KeyData == (Keys.Control | Keys.V))
            {
                Paste();
            }
            else if (e.KeyData == (Keys.Control | Keys.A))
            {
                SelectAll();
            }
            base.OnKeyUp(e);
        }
        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            if (mevent.Button == MouseButtons.Right)
            {
                ContextMenu cm = new ContextMenu();
                cm.MenuItems.Add("Cut", delegate
                {
                    Cut();
                });
                cm.MenuItems.Add("Copy", delegate
                {
                    Copy();
                });
                cm.MenuItems.Add("Paste", delegate
                {
                    Paste();
                });
                cm.Show(Parent, PointToClient(Cursor.Position));
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            if (changeKeyEnter)
            {
                changeKeyEnter = false;
            }
            else
            {
                if (this.Focused)
                {
                    Point cp;
                    GetCaretPos(out cp);
                    var results = SplitText.GetSplitText(Text, new List<string> { " ", Environment.NewLine }, SelectionStart);
                    var resultCursor = results.Where(x => x.IsCursor).FirstOrDefault();

                    List<string> lstTemp = new List<string>();

                    combobox.SetBounds(cp.X, cp.Y, 150, 50);

                    if (resultCursor != null && resultCursor.Word != "")
                    {
                        lstTemp = dictionary.Where(n => n.ToUpper().Contains(resultCursor.Word.ToUpper()))
                            .Select(r => r)
                            .OrderBy(x => x.StartsWith(resultCursor.Word) ? 0 : 1)
                            .ThenBy(x => x.ToUpper().StartsWith(resultCursor.Word.ToUpper()) ? 0 : 1)
                            .ThenBy(x => x.IndexOf(resultCursor.Word))
                            .ThenBy(x => x.ToUpper().IndexOf(resultCursor.Word.ToUpper()))
                            .ThenBy(x => x)
                            .ToList();
                        //var TempFilteredList = dictionary.Where(n => n.ToUpper().Contains(GetLastString(Text).ToUpper())).Select(r => r);
                    }

                    combobox.DataSource = lstTemp;

                    if (lstTemp.Count != 0)
                    {
                        combobox.DroppedDown = true;
                    }
                    else
                    {
                        combobox.DroppedDown = false;
                    }
                }
            }
        }
        private void SetAutoComplete(string word)
        {
            Point cp;
            GetCaretPos(out cp);
            var results = SplitText.GetSplitText(word, new List<string> { " ", Environment.NewLine }, SelectionStart);
            var resultCursor = results.Where(x => x.IsCursor).FirstOrDefault();

            List<string> lstTemp = new List<string>();

            combobox.SetBounds(cp.X, cp.Y, 150, 50);

            if (resultCursor != null && resultCursor.Word != "")
            {
                lstTemp = dictionary.Where(n => n.ToUpper().Contains(resultCursor.Word.ToUpper()))
                    .Select(r => r)
                    .OrderBy(x => x.StartsWith(resultCursor.Word) ? 0 : 1)
                    .ThenBy(x => x.ToUpper().StartsWith(resultCursor.Word.ToUpper()) ? 0 : 1)
                    .ThenBy(x => x.IndexOf(resultCursor.Word))
                    .ThenBy(x => x.ToUpper().IndexOf(resultCursor.Word.ToUpper()))
                    .ThenBy(x => x)
                    .ToList();
                //var TempFilteredList = dictionary.Where(n => n.ToUpper().Contains(GetLastString(Text).ToUpper())).Select(r => r);
            }

            combobox.DataSource = lstTemp;

            if (lstTemp.Count != 0)
            {
                combobox.DroppedDown = true;
            }
            else
            {
                combobox.DroppedDown = false;
            }
        }
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            combobox.Font = Font;
        }

        public List<string> Dictionary
        {
            get { return dictionary; }
            set { dictionary = value; }
        }
        #endregion

        #region Methods
        private void combobox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SetText();
        }

        private void SetText()
        {
            var results = SplitText.GetSplitText(Text, new List<string> { " ", Environment.NewLine }, SelectionStart);
            var resultCursor = results.Where(x => x.IsCursor).FirstOrDefault();
            if (resultCursor != null)
            {
                resultCursor.Word = combobox.SelectedItem.ToString();
            }
            var newSelectIndex = 0;
            var newText = "";
            var find = false;
            foreach (var rs in results)
            {
                if (!find)
                {
                    if (!rs.IsCursor)
                    {
                        newSelectIndex = newSelectIndex + rs.Word.Length + rs.Conjuction.Length;
                    }
                    else
                    {
                        find = true;
                        newSelectIndex = newSelectIndex + rs.Word.Length;
                    }
                }
                newText = newText + rs.Word + rs.Conjuction;
            }
            Text = newText;
            Select(newSelectIndex, 0);
            Focus();
            combobox.DroppedDown = false;
        }

        public List<SplitResult> GetListWord()
        {
            return SplitText.GetSplitText(Text, new List<string> { " ", Environment.NewLine }, SelectionStart);
        }
        #endregion
    }

    public class SplitResult
    {
        public string Word { get; set; }
        public string Conjuction { get; set; }
        public bool IsCursor { get; set; }
    }
    public static class SplitText
    {
        public static List<SplitResult> GetSplitText(string text, List<string> keyWords, int cursorIndex)
        {
            var tempText = text;
            var rs = new List<SplitResult>();
            do
            {
                var word = tempText;
                var conjuction = "";
                var index = tempText.Length;
                var startCursor = text.Length - tempText.Length;
                var isCursor = false;
                foreach (var keyWord in keyWords)
                {
                    var cursor = tempText.IndexOf(keyWord);
                    if (cursor >= 0 && cursor < index)
                    {
                        index = cursor;
                        word = tempText.Substring(0, index);
                        conjuction = keyWord;
                    }
                }
                tempText = tempText.Substring(index + conjuction.Length);
                var endCursor = text.Length - tempText.Length - conjuction.Length;
                if (cursorIndex >= startCursor && cursorIndex <= endCursor) isCursor = true;
                rs.Add(new SplitResult { Word = word, Conjuction = conjuction, IsCursor = isCursor });
            } while (tempText != "");
            return rs;
        }
    }
}
