using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;

namespace BKvs2010.Usercontrols
{
    public partial class newWaitingListStationUC : UserControl
    {
        public newWaitingListStationUC()
        {
            InitializeComponent();
            GridWaitingList.AutoGenerateColumns = false;
        }

        private bool _LargeSize = false;
        public bool LargeSize
        {
            get { return _LargeSize; }
            set
            {
                if (value != _LargeSize)
                {
                    if (value)
                    {
                        this.Column2.Width = 120;
                        this.Column3.Width = 100;
                        this.Column4.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                    else
                    {
                        this.Column2.Width = 45;
                        this.Column3.Width = 75;
                        this.Column4.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                    _LargeSize = value;
                }
            }
        }

        private int? _mrm_id { get; set; }
        private int? _mrd_id;
        public int? mrd_id
        {
            get { return _mrd_id; }
            set
            {
                if (value == null)
                {
                    _mrm_id = null;
                    _mrd_id = null;
                    GridWaitingList.DataSource = new SortableBindingList<Class.WaitingListCls.WaitingListDtl>();
                }
                else
                {
                    if (value != _mrd_id)
                    {
                        using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                        {
                            mst_room_dtl mstRoomDtl = cdc.mst_room_dtls.Where(x => x.mrd_id == value).FirstOrDefault();
                            if (mstRoomDtl == null)
                            {
                                _mrm_id = null;
                                _mrd_id = null;
                            }
                            else
                            {
                                _mrm_id = mstRoomDtl.mrm_id;
                                _mrd_id = mstRoomDtl.mrd_id;
                            }
                        }
                    }
                }
            }
        }
        public int? mut_id { get; set; }
        
        private bool _ableDoubleClick = true;
        public bool ableDoubleClick
        {
            get { return _ableDoubleClick; }
            set
            {
                _ableDoubleClick = value;
            }
        }

        public event OnSuccessProcess OnWaitingSuccessProcess;
        public delegate void OnSuccessProcess(object sender, StatusTransaction isCallQueue, string e);
        private void _OnWaitingSuccessProcess(StatusTransaction isCallQueue, string e)
        {
            // Make sure someone is listening to event
            if (OnWaitingSuccessProcess == null) return;
            OnWaitingSuccessProcess(this, isCallQueue, e);
        }

        public void LoadData()
        {
            SortableBindingList<Class.WaitingListCls.WaitingListDtl> data = new SortableBindingList<Class.WaitingListCls.WaitingListDtl>(new Class.WaitingListCls().getWaitingRoomDtl(this._mrd_id, this.mut_id));
            GridWaitingList.DataSource = data;
        }

        private void GridWaitingList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_mrm_id == null || _mrd_id == null || mut_id == null)
            {

            }
            else
            {
                if (e.RowIndex < 0) return;
                try
                {
                    if (!_ableDoubleClick) return;
                    DataGridView dgv = (DataGridView)sender;
                    Class.WaitingListCls.WaitingListDtl data = (Class.WaitingListCls.WaitingListDtl)dgv.Rows[e.RowIndex].DataBoundItem;
                    int tpr_id = data.tpr_id;
                    string queueNo = "";
                    int tps_id = 0;
                    frmBGScreen frmbg = new frmBGScreen();
                    frmbg.Show();
                    Application.DoEvents();

                    StatusTransaction onWaiting = new Class.FunctionDataCls().checkStatusWaiting(tpr_id, (int)_mrm_id, ref tps_id, ref queueNo);
                    frmbg.Close();
                    if (onWaiting == StatusTransaction.False)
                    {
                        _OnWaitingSuccessProcess(StatusTransaction.False, queueNo + " อยู่ในสถานะที่ไม่สามารถดำเนินการได้ กรุณาตรวจสอบ");
                    }
                    else
                    {
                        string messageAlert = "";
                        frmManageWaiting frmWaiting = new frmManageWaiting();
                        StatusTransaction isCallQ = frmWaiting.isCallQueue(tps_id, ref messageAlert);
                        if (isCallQ == StatusTransaction.True)
                        {

                        }
                        _OnWaitingSuccessProcess(isCallQ, messageAlert);
                    }
                }
                catch (Exception ex)
                {
                    Program.MessageError(this.Name, "GridWaitingList_CellDoubleClick", ex, false);
                }
                finally
                {
                    Program.RefreshWaiting = true;
                }
            }
        }
        private void GridWaitingList_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            Class.WaitingListCls.WaitingListDtl data = (Class.WaitingListCls.WaitingListDtl)dgv.Rows[e.RowIndex].DataBoundItem;
            switch (data.reserve)
            {
                case true:
                    dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(41, 242, 13);
                    dgv.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(41, 242, 13);
                    break;
            }
            switch (data.holded)
            {
                case true:
                    dgv.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Red;
                    dgv.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Red;
                    break;
            }
        }
    }
}
