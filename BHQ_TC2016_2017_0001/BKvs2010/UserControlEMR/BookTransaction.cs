using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;

namespace BKvs2010.UserControlEMR
{
    public partial class BookTransaction : UserControl
    {
        public BookTransaction()
        {
            InitializeComponent();
            gvTransaction.AutoGenerateColumns = false;
            gvTransaction.DataBindingComplete += gvTransaction_DataBindingComplete;
            DataGridViewButtonColumn c = (DataGridViewButtonColumn)gvTransaction.Columns["cDel"];
            c.FlatStyle = FlatStyle.Standard;
            c.DefaultCellStyle.ForeColor = Color.Red;
            SetSourceEvent();
        }
        private List<msEvent> ListMsEvent;
        private class msEvent
        {
            public int mbe_id { get; set; }
            public string mbe_tname { get; set; }
            public char? mbe_status { get; set; }
        }
        private void SetSourceEvent()
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    ListMsEvent = cdc.mst_book_events
                                     .Select(x => new msEvent
                                     {
                                         mbe_id = x.mbe_id,
                                         mbe_tname = x.mbe_tname,
                                         mbe_status = x.mbe_status
                                     }).ToList();
                    var source = ListMsEvent.Where(x => x.mbe_status == 'A')
                                            .Select(x => new
                                            {
                                                val = x.mbe_id,
                                                dis = x.mbe_tname
                                            }).ToList();

                    source.Insert(0, new { val = 0, dis = "Please Select Any Event." });
                    comboEvent.DataSource = source;
                    comboEvent.DisplayMember = "dis";
                    comboEvent.ValueMember = "val";
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("BookTransaction()", "SetSourceEvent()", ex, false);
            }
        }

        private System.Data.Linq.EntitySet<trn_book_event> _ListBookEvent;
        public System.Data.Linq.EntitySet<trn_book_event> ListBookEvent
        {
            get { return _ListBookEvent; }
            set
            {
                _ListBookEvent = value;
                if (_ListBookEvent == null)
                {
                    gvTransaction.DataSource = trnbookeventBindingSource;
                    trnbookeventBindingSource.DataSource = new System.Data.Linq.EntitySet<trn_book_event>();
                    SetDateTime();
                    panel1.Enabled = false;
                    trnbookeventBindingSource.Sort = "tbe_create_date DESC";
                }
                else
                {
                    gvTransaction.DataSource = trnbookeventBindingSource;
                    trnbookeventBindingSource.DataSource = _ListBookEvent;
                    SetDateTime();
                    panel1.Enabled = true;
                    trnbookeventBindingSource.Sort = "tbe_create_date DESC";
                }
                try
                {
                    comboEvent.SelectedValue = 0;
                    txtTransaction.Text = "";
                    txtTime.Text = DateTime.Now.TimeOfDay.ToString();
                }
                catch
                {

                }
            }
        }
        private void SetDateTime()
        {
            var dateNow = DateTime.Now;
            dateTransaction.Value = dateNow.Date;
            txtTime.Text = dateNow.TimeOfDay.ToString();
        }

        private void btnAddTransaction_Click(object sender, EventArgs e)
        {
            try
            {
                if (ConvertToInt(comboEvent.SelectedValue) == 0)
                {
                    comboEvent.Focus();
                }
                else
                {
                    gvTransaction.DataSource = trnbookeventBindingSource;
                    trnbookeventBindingSource.DataSource = new System.Data.Linq.EntitySet<trn_book_event>();
                    var dateNow = DateTime.Now;
                    _ListBookEvent.Add(new trn_book_event
                    {
                        mbe_id = ConvertToInt(comboEvent.SelectedValue),
                        tbe_remark = txtTransaction.Text,
                        tbe_date = dateTransaction.Value.Date.Add(ConvertToTime(txtTime.Text)),
                        tbe_active = true,
                        tbe_create_by = Program.CurrentUser.mut_username,
                        tbe_create_date = dateNow,
                        tbe_update_by = Program.CurrentUser.mut_username,
                        tbe_update_date = dateNow
                    });
                    comboEvent.SelectedValue = 0;
                    txtTransaction.Text = "";

                    gvTransaction.DataSource = trnbookeventBindingSource;
                    trnbookeventBindingSource.DataSource = _ListBookEvent;
                    panel1.Enabled = true;
                    trnbookeventBindingSource.Sort = "tbe_create_date DESC";
                    //setRowNo();
                }
            }
            catch
            {

            }
        }

        private TimeSpan ConvertToTime(string txt)
        {
            try
            {
                var arr = txt.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                return new TimeSpan(ConvertToInt(arr[0]), ConvertToInt(arr[1]), 0);
            }
            catch
            {
                return new TimeSpan(0, 0, 0);
            }
        }
        private int ConvertToInt(object val)
        {
            try
            {
                return Convert.ToInt32(val);
            }
            catch
            {
                return 0;
            }
        }

        private void gvTransaction_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gvTransaction.Columns[e.ColumnIndex].Name == "cDel")
            {
                trn_book_event row = gvTransaction.Rows[e.RowIndex].DataBoundItem as trn_book_event;
                if (row != null)
                {
                    row.tbe_active = false;
                }
            }
        }
        private void gvTransaction_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (_ListBookEvent != null)
            {
                int indx = _ListBookEvent.Where(x => x.tbe_active == true).Count();
                foreach (DataGridViewRow row in gvTransaction.Rows)
                {
                    trn_book_event ev = row.DataBoundItem as trn_book_event;
                    trnbookeventBindingSource.SuspendBinding();
                    if (ev.tbe_active == true)
                    {
                        row.Visible = true;
                        row.Cells[0].Value = indx;
                        indx--;
                    }
                    else
                    {
                        row.Visible = false;
                    }
                    trnbookeventBindingSource.ResumeBinding();
                    var tname = ListMsEvent.Where(x => x.mbe_id == ev.mbe_id).Select(x => x.mbe_tname).FirstOrDefault();
                    row.Cells["cEventName"].Value = string.IsNullOrEmpty(tname) ? "" : tname;
                    row.Cells["TransactionShow"].Value = ev.tbe_remark;
                    try
                    {
                        using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                        {
                            var uname = cdc.mst_user_types.Where(x => x.mut_username == ev.tbe_create_by).Select(x => x.mut_fullname).FirstOrDefault();
                            row.Cells["colUser"].Value = string.IsNullOrEmpty(uname) ? "" : uname;
                        }
                    }
                    catch
                    {

                    }
                }
            }
        }

        private void gvTransaction_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //if (e.Value == null)
            //    return;
            //var s = e.Graphics.MeasureString(e.Value.ToString(), gvTransaction.Font);
            //if (s.Width > gvTransaction.Columns[e.ColumnIndex].Width)
            //{
            //    using (Brush gridBrush = new SolidBrush(gvTransaction.GridColor), backColorBrush = new SolidBrush(e.CellStyle.BackColor))
            //    {
            //        e.Graphics.FillRectangle(backColorBrush, e.CellBounds);
            //        e.Graphics.DrawString(e.Value.ToString(), gvTransaction.Font, Brushes.Black, e.CellBounds, StringFormat.GenericDefault);

            //        var height = Convert.ToInt32(e.Graphics.MeasureString(e.Value.ToString(), gvTransaction.Font, gvTransaction.Columns[e.ColumnIndex].Width).Height);
            //        gvTransaction.Rows[e.RowIndex].Height = height > gvTransaction.Rows[e.RowIndex].Height ? height : gvTransaction.Rows[e.RowIndex].Height;
            //        //(int)(s.Height * Math.Ceiling(s.Width / gvTransaction.Columns[e.ColumnIndex].Width));
            //        e.Handled = true;
            //    }
            //}
        }

        private void gvTransaction_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var gv = sender as DataGridView;
            if (gv != null)
            {
                int rowHeight = 0;
                for (int i = 0; i < gv.Columns.Count; i++)
                {
                    if (gv[i, e.RowIndex].Visible)
                    {
                        try
                        {
                            var str = gv[i, e.RowIndex].Value.ToString();
                            var s = e.Graphics.MeasureString(str, gv.DefaultCellStyle.Font);

                            var h = (int)((Math.Ceiling(s.Height) + 2) * (Math.Ceiling(Math.Ceiling(s.Width) / gv.Columns[i].Width)));
                            if (h > rowHeight) rowHeight = h;
                        }
                        catch
                        {

                        }
                    }
                }
                gv.Rows[e.RowIndex].Height = rowHeight;
            }
        }
    }
}
