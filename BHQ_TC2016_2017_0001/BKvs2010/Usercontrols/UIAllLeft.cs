using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BKvs2010.Usercontrols
{
    public partial class UIAllLeft : UserControl
    {
        private bool _ableWaitingDoubleClick = true;
        public bool ableWaitingDoubleClick
        {
            get
            {
                return _ableWaitingDoubleClick;
            }
            set
            {
                uiWaitList1.ableDoubleClick = value;
                _ableWaitingDoubleClick = value;
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

        private int? _tpr_id;
        public int? tpr_id
        {
            get
            {
                return _tpr_id;
            }
        }
        public UIAllLeft()
        {
            InitializeComponent();
            uiWaitList1.OnRefreshStatusED += new UIWaitList.RefreshStatusED(RefreshStatusAllLeft);
        }

        public delegate void RefreshStatusED();
        public event RefreshStatusED OnRefreshStatusED;

        private void UIAllLeft_Load(object sender, EventArgs e)
        {

        }
        private void RefreshStatusAllLeft()
        {
            try
            {
                OnRefreshStatusED();
            }
            catch 
            {
            }
        }
        public void LoadDataAll()
        {
            if (Program.CurrentRegis != null)
            {
                LoadDataAll(Program.CurrentRegis.tpr_id);
                _tpr_id = Program.CurrentRegis.tpr_id;
            }
            else
            {
                _tpr_id = null;
                uiUserprofile1.LoadData();
                uiWaitList1.LoadData();
                uiMapping1.GetMapping();
            }
        }
        public void LoadDataAll(int tpr_id)
        {
            uiUserprofile1.LoadData(tpr_id);
            uiMapping1.GetMapping(tpr_id);
            uiWaitList1.LoadData();
            _tpr_id = tpr_id;
        }

        private void uiWaitList1_OnWaitingSuccessProcess(object sender, StatusTransaction isCallQueue, string e)
        {
            _OnWaitingSuccessProcess(isCallQueue, e);
        }
    }
}
