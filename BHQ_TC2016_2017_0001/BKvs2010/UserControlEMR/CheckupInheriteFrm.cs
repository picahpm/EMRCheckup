using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;

namespace BKvs2010.UserControlEMR
{
    public partial class CheckupInheriteFrm : Form
    {
        InhCheckupDataContext cdc;

        private int? _tpr_id = null;
        public int? tpr_id
        {
            get { return _tpr_id; }
            set
            {
                if (value != _tpr_id)
                {
                    if (value == null)
                    {
                        Program.CurrentRegis = null;
                        Program.CurrentPatient_queue = null;
                    }
                    _tpr_id = value;
                    OnTpridChanged(value, null);
                }
            }
        }

        private int? _mvt_id = null;
        public int? mvt_id
        {
            get { return _mvt_id; }
            set
            {
                if (_mvt_id != value)
                {
                    _mvt_id = value;
                    OnMvtidChanged(value);
                }
            }
        }
        public string username { get; set; }
        
        public enum tpr_idStatus
        {
            NSWR,
            WK,
            Nothing
        }
        public enum formStatus
        {
            isFooter,
            isStation
        }

        private formStatus _FormStatus = formStatus.isFooter;
        public formStatus FormStatus 
        {
            get { return _FormStatus; }
            set
            {
                if (value != _FormStatus)
                {
                    _FormStatus = value;
                }
            }
        }

        public int? mut_id { get; set; }
        private int? _lug_id = null;
        public int? lug_id
        {
            get { return _lug_id; }
            set
            {
                if (_lug_id != value)
                {
                    _lug_id = value;
                    if (value != null)
                    {
                        CheckKickedUser();
                    }
                }
            }
        }
        private void CheckKickedUser()
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    log_user_login lug = cdc.log_user_logins
                                            .Where(x => x.lug_id == _lug_id)
                                            .FirstOrDefault();
                    if (lug.lug_end_date != null)
                    {
                        this.Close();
                        OnKickedUser(false);
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "CheckKickedUser", ex, false);
            }
            Timer timer = new Timer();
            timer.Enabled = true;
            timer.Interval = 5000;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            Timer timer = (Timer)sender;
            timer.Stop();
            timer.Dispose();
            if (!this.IsDisposed && _lug_id != null)
            {
                CheckKickedUser();
            }
        }

        public event TpridChanged tpridChanged;
        public delegate void TpridChanged(object sender, int? e, tpr_idStatus? status);
        private void OnTpridChanged(int? e, tpr_idStatus? status)
        {
            if (tpridChanged == null) return;
            tpridChanged(this, e, status);
        }

        private int? _mrd_id = null;
        public int? mrd_id
        {
            get { return _mrd_id; }
            set
            {
                if (value != _mrd_id)
                {
                    if (value != null)
                    {
                        if (this.FormStatus == formStatus.isStation)
                        {
                            cdc = new InhCheckupDataContext();

                            trn_patient_queue PatientQueue = cdc.trn_patient_queues
                                                                .Where(x => x.mrd_id == value &&
                                                                            ((x.tps_status == "NS" && x.tps_ns_status == "WR") ||
                                                                             (x.tps_status == "WK"))).FirstOrDefault();

                            if (PatientQueue != null)
                            {
                                this._tpr_id = PatientQueue.tpr_id;
                                Program.CurrentRegis = PatientQueue.trn_patient_regi;
                                Program.CurrentPatient_queue = PatientQueue;
                                if (PatientQueue.tps_status == "NS" && PatientQueue.tps_ns_status == "WR")
                                {
                                    OnTpridChanged(PatientQueue.tpr_id, tpr_idStatus.NSWR);
                                }
                                else
                                {
                                    OnTpridChanged(PatientQueue.tpr_id, tpr_idStatus.WK);
                                }
                            }
                            else
                            {
                                OnTpridChanged(null, null);
                            }
                        }
                    }
                    else
                    {
                        _FormStatus = formStatus.isFooter;
                    }
                    _mrd_id = value;
                    OnmrdidChanged(value);
                }
            }
        }

        public event MrdidChanged mrdidChanged;
        public delegate void MrdidChanged(object sender, int? e);
        private void OnmrdidChanged(int? e)
        {
            if (mrdidChanged == null) return;
            mrdidChanged(this, e);
        }

        public event MvtidChanged mvtidChanged;
        public delegate void MvtidChanged(object sender, int? e);
        private void OnMvtidChanged(int? e)
        {
            if (mvtidChanged == null) return;
            mvtidChanged(this, e);
        }

        public event KickedUser kickedUser;
        public delegate void KickedUser(object sender, bool e);
        private void OnKickedUser(bool e)
        {
            if (kickedUser == null) return;
            kickedUser(this, e);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            this.Dispose();
        }
    }
}
