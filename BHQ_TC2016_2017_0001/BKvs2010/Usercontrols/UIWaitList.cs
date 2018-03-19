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
    public partial class UIWaitList : UserControl
    {
        private bool _ableDoubleClick = true;
        public bool ableDoubleClick
        {
            get
            {
                return _ableDoubleClick;
            }
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

        public UIWaitList()
        {
            InitializeComponent();
            GridWaitingList.AutoGenerateColumns = false;
        }
        public delegate void RefreshStatusED();
        public event RefreshStatusED OnRefreshStatusED;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Program.RefreshWaiting)
            {
                using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                {
                    timer1.Stop();// Stop Timer
                    if (Program.FooterIsclick) LoadData(dbc);
                }
                IsStatusED();
            }
            GC.Collect();
        }

        public void LoadData()
        {
            timer1_Tick(null, null);
        }
        public void LoadData(CheckupDataContext dbc)
        {

            if (Program.CurrentRoom != null)
            {
                try
                {
                    var objwaitlist = new Class.WaitingListCls().getWaitingRoomDtl(Program.CurrentRoom.mrd_id, Program.CurrentUser.mut_id);
                    GridWaitingList.DataSource = new SortableBindingList<waitinglist>(objwaitlist.OrderBy(x => x.index).Select((item, index) => new waitinglist
                    {
                        NO = index + 1,
                        Callstatus = item.holded == true ? "HD" : "",
                        QueueNo = item.tpr_queue_no,
                        HN = item.tpt_hn_no,
                        FullName = item.tpt_othername,
                        tpr_id = item.tpr_id,
                        hilight = item.reserve
                    }).ToList());
                    lbTitle.Text = string.Format("Waiting List (Total {0} คน)", objwaitlist.Count());
                    timer1.Start();// Stat Timer
                }
                catch (Exception ex)
                {
                    Program.MessageError("UIWaitList", "LoadData", ex, false);
                    timer1.Start();
                }
            }
        }
       // public void LoadData( CheckupDataContext dbc)
       // {
            
       //     if (Program.CurrentRoom != null)
       //     {
       //         try
       //         {
       //             DateTime dtnow = Program.GetServerDateTime();
       //             DateTime ResetDate = new DateTime(dtnow.Year, dtnow.Month, dtnow.Day, 0, 0, 0);
       //             TimeSpan timenow = dtnow.TimeOfDay;
       //             //bool? doc_call = dbc.func_get_doctor_call(Program.CurrentUser.mut_username);

       //             ////Added.Akkaradech on 2014-01-08
       //             //var GetpatientInPatientCate = (from mut in dbc.mst_user_types
       //             //                               join muc in dbc.mst_user_cats
       //             //                               on mut.mut_id equals muc.mut_id
       //             //                               join tpc1 in dbc.trn_patient_cats
       //             //                               on muc.mdc_id equals tpc1.mdc_id
       //             //                               join tpq in dbc.trn_patient_queues
       //             //                               on tpc1.tpr_id equals tpq.tpr_id
       //             //                               where
       //             //                               tpq.mrm_id == Program.CurrentRoom.mrm_id 
       //             //                               && tpq.tps_status == "NS"
       //             //                               && tpq.tps_ns_status == "QL"
       //             //                               && tpq.trn_patient_regi.tpr_arrive_date.Value.Date == dtnow.Date
       //             //                               select tpc1.tpr_id).Distinct().ToList();

       //             //int GetpatientInPatientCate = (from mut in dbc.mst_user_types
       //             //                               join muc in dbc.mst_user_cats
       //             //                               on mut.mut_id equals muc.mut_id
       //             //                               join tpc1 in dbc.trn_patient_cats
       //             //                               on muc.mdc_id equals tpc1.mdc_id
       //             //                               join tpq in dbc.trn_patient_queues
       //             //                               on tpc1.tpr_id equals tpq.tpr_id
       //             //                               where
       //             //                               tpq.mrm_id == Program.CurrentRoom.mrm_id
       //             //                               && tpq.tps_status == "NS"
       //             //                               && tpq.tps_ns_status == "QL"
       //             //                               && tpq.trn_patient_regi.tpr_arrive_date.Value.Date == dtnow.Date
       //             //                               select tpc1.tpr_id).Distinct().FirstOrDefault();
       //             ////EndAdded.Akkaradech on 2014-01-08

       //             var objwaitlist = (from t1 in dbc.trn_patient_queues
       //                                where t1.mrm_id == Program.CurrentRoom.mrm_id
       //                                && t1.trn_patient_regi.tpr_arrive_date.Value.Date == dtnow.Date

       //                                && t1.tps_status == "NS"
       //                                && t1.tps_ns_status == "QL"
       //                               // && GetpatientInPatientCate.Contains(t1.trn_patient_regi.tpr_id)
       //                                orderby t1.tps_bm_seq,t1.tps_create_date,t1.tps_hold_date
       //                                select new
       //                                 {
       //                                     mdc_id=t1.trn_patient_regi.mdc_id,
       //                                     mvtID=t1.mvt_id,
       //                                     ReqDoc=t1.trn_patient_regi.tpr_req_doctor,
       //                                     ReqGender = (t1.trn_patient_regi.tpr_req_doc_gender == null) ? 'X' : t1.trn_patient_regi.tpr_req_doc_gender,
       //                                     DocCode=t1.trn_patient_regi.tpr_req_doc_code,
       //                                     //PEDoc = string.IsNullOrEmpty(Convert.ToString((t1.trn_patient_regi.tpr_pe_doc == null) ? ' ' : t1.trn_patient_regi.tpr_pe_doc)) ? 'N' : t1.trn_patient_regi.tpr_pe_doc,
       //                                     PEDoc = (t1.trn_patient_regi.tpr_pe_doc == null) ? 'N' : t1.trn_patient_regi.tpr_pe_doc,
       //                                     //PEDoc = t1.trn_patient_regi.tpr_pe_doc,
       //                                     PEDocCode=t1.trn_patient_regi.tpr_pe_doc_code,
       //                                     NurseCode=t1.trn_patient_regi.tpr_nurse_code,
       //                                     RTN_Nurse=t1.trn_patient_regi.tpr_return_screening,

       //                                     OutDocCode = (t1.trn_patient_regi.tpr_req_inorout_doctor == null || t1.trn_patient_regi.tpr_req_inorout_doctor == "") ? false
       //                                                  : t1.trn_patient_regi.tpr_req_inorout_doctor == "UT" ? true : false,

       //                                     bmSeq=(t1.tps_bm_seq!=null)?t1.tps_bm_seq:99,
       //                                     ordDate=(t1.tps_call_status=="HD")?t1.tps_hold_date:t1.tps_create_date,
       //                                     hold_flag = (t1.tps_call_status == "HD" && (timenow.Subtract(t1.tps_hold_date.Value.TimeOfDay)).TotalMinutes >= 0) ? "Y" : "N",
       //                                     Callstatus=t1.tps_call_status,
       //                                     QueueNo = t1.trn_patient_regi.tpr_queue_no,
       //                                     IsPirot = t1.trn_patient_regi.tpr_patient_type,
       //                                     HN = t1.trn_patient_regi.trn_patient.tpt_hn_no,
       //                                     FullName = t1.trn_patient_regi.trn_patient.tpt_pre_name + t1.trn_patient_regi.trn_patient.tpt_first_name + " " + t1.trn_patient_regi.trn_patient.tpt_last_name,
       //                                     tprid = t1.trn_patient_regi.tpr_id,
       //                                     countmdc = (from t in dbc.trn_patient_cats where t.tpr_id == t1.trn_patient_regi.tpr_id select t.mdc_id).Count(),

       //                                     countInsu = (from t in dbc.trn_patient_cats
       //                                                  join s in dbc.mst_doc_categories on t.mdc_id equals s.mdc_id
       //                                                  where t.tpr_id == t1.trn_patient_regi.tpr_id
       //                                                  && s.mdc_pre_insure == true
       //                                                  select t.mdc_id).Count(),

       //                                     DocCat = (from t in dbc.trn_patient_cats join s in dbc.mst_doc_categories on t.mdc_id equals s.mdc_id where t.tpr_id == t1.trn_patient_regi.tpr_id && s.mdc_code == "MD014" select s.mdc_id).Distinct().Count(),
       //                                     //mdc_flag = (Convert1.ToInt32(GetpatientInPatientCate) == t1.trn_patient_regi.tpr_id) ? 1 : 0
       //                                     mdc_flag = (from mut in dbc.mst_user_types
       //                                                 join muc in dbc.mst_user_cats on mut.mut_id equals muc.mut_id
       //                                                 join tpc1 in dbc.trn_patient_cats on muc.mdc_id equals tpc1.mdc_id
       //                                                 join tpq in dbc.trn_patient_queues on tpc1.tpr_id equals tpq.tpr_id
       //                                                 where tpq.mrm_id == Program.CurrentRoom.mrm_id
       //                                                 && tpq.tpr_id == t1.trn_patient_regi.tpr_id
       //                                                 && tpq.tps_status == "NS"
       //                                                 && tpq.tps_ns_status == "QL"
       //                                                 && tpq.trn_patient_regi.tpr_arrive_date.Value.Date == dtnow.Date
       //                                                 && mut.mut_id == Program.CurrentUser.mut_id
       //                                                 && mut.mut_gender == ((tpq.trn_patient_regi.tpr_req_doc_gender == null) ? mut.mut_gender : tpq.trn_patient_regi.tpr_req_doc_gender)
       //                                                 select tpc1.tpr_id).Count(),
       //                                     vip_hpc = (t1.trn_patient_regi.trn_patient.tpt_vip_hpc == true) ? "Y" : "N",
       //                                     type_PE = ((from t in dbc.mst_events where t.mvt_id == t1.mvt_id select t.mvt_code).FirstOrDefault() == "PE") ? "Y" : "N",
       //                                     type_Lower = t1.trn_patient_regi.tpr_miss_lower,
       //                                     hilight = t1.tps_reserve == null ? false : (bool)t1.tps_reserve
       //                                 }).ToList();

       //                 objwaitlist = (from t1 in objwaitlist
       //                                //where t1.tprid == Convert1.ToInt32(GetpatientInPatientCate.Contains(t1.tprid))
       //                                orderby t1.hold_flag descending, t1.bmSeq, t1.ordDate
       //                                select t1).ToList();

       //             //*********** เช็คว่าเป็น Pirot **
       //             //var v_Avia = objwaitlist.Select(x => x.IsPirot).ToList();
       //             //var v_tprid = objwaitlist.Select(x => x.tprid).ToList();
       //             //var countmdc = (from t in dbc.trn_patient_cats where t.tpr_id == Convert1.ToInt32(v_tprid) select t.mdc_id).Count();
       //             var CurrentmrmCode = (from t1 in dbc.mst_room_hdrs where t1.mrm_id == Program.CurrentRoom.mrm_id select t1).FirstOrDefault();
       //             if (CurrentmrmCode != null)
       //             {
       //                 if (Program.CurrentSite.mhs_code == "01HPC3")
       //                 {
       //                     //objwaitlist = objwaitlist.OrderBy(x => x.bmSeq).ThenByDescending(x => x.vip_hpc).ThenByDescending(x => x.hold_flag).ThenBy(x => x.ordDate).ToList();
       //                     objwaitlist = objwaitlist.OrderByDescending(x => x.vip_hpc).ThenByDescending(x => x.hold_flag).ThenBy(x => x.bmSeq).ThenBy(x => x.ordDate).ToList();
       //                 }
       //                 else if (Program.CurrentSite.mhs_code == "01HPC2")
       //                 {
       //                     //objselect = objselect.OrderBy(x => x.tps_bm_seq).ThenByDescending(x => x.vip_hpc).ThenByDescending(x => x.hold_flag).ThenBy(x => x.st_date).ToList();
       //                     objwaitlist = objwaitlist.OrderByDescending(x => x.vip_hpc).ThenByDescending(x => x.hold_flag).ThenBy(x => x.bmSeq).ThenBy(x => x.ordDate).ToList();
       //                 }

       //                 if (CurrentmrmCode.mrm_code == "SC")
       //                 {
       //                     if (Program.CurrentRoom.mrd_avation == true && Program.CurrentRoom.mrd_pre_insure == true)
       //                     {
       //                         //objwaitlist = objwaitlist.Where(x => (x.IsPirot == '2' || x.IsPirot == '4') && x.countmdc > 0).ToList();

       //                         //objwaitlist = objwaitlist.Where(x => (x.IsPirot == '2' || x.IsPirot == '4') && x.countInsu > 0).ToList();
       //                     }
       //                     else if (Program.CurrentRoom.mrd_avation == true && Program.CurrentRoom.mrd_pre_insure == false)
       //                     {
       //                         //objwaitlist = objwaitlist.Where(x => x.IsPirot == '2' || x.IsPirot == '4').ToList();
       //                         objwaitlist = objwaitlist.Where(x => (x.IsPirot == '2' || x.IsPirot == '4') && x.countInsu == 0).ToList();
       //                     }
       //                     else if (Program.CurrentRoom.mrd_avation == false && Program.CurrentRoom.mrd_pre_insure == true)
       //                     {
       //                         //objwaitlist = objwaitlist.Where(x => x.countmdc > 0 || ((x.IsPirot != '2' && x.IsPirot != '4') && (x.countmdc == 0 || (x.countmdc > 0 && x.DocCat == 1)))).ToList();
       //                         //objselect = objselect.Where(x => countmdc > 0).ToList();

       //                         objwaitlist = objwaitlist.Where(x => (x.IsPirot != '2' && x.IsPirot != '4') || x.countInsu > 0).ToList();
       //                     }
       //                     else
       //                     {
       //                         //objwaitlist = objwaitlist.Where(x => (x.IsPirot != '2' && x.IsPirot != '4') && (x.countmdc == 0 || (x.countmdc > 0 && x.DocCat == 1))).ToList();

       //                         objwaitlist = objwaitlist.Where(x => (x.IsPirot != '2' && x.IsPirot != '4') && x.countInsu == 0).ToList();
       //                     }

       //                     /*if (Program.CurrentSite.mhs_code == "01HPC2")
       //                     {
       //                         objwaitlist = objwaitlist.Where(x => (x.NurseCode == null || (x.NurseCode != null && x.NurseCode == Program.CurrentUser.mut_username))).ToList();
       //                     }*/

       //                     // เพิ่มเงื่อนไขในการเข้าห้องเดิมของ HPC Site 2
       //                     if (Program.CurrentSite.mhs_code == "01HPC2")
       //                     {
       //                         objwaitlist = objwaitlist.Where(x => (x.NurseCode == null || (x.NurseCode != null && x.NurseCode == Program.CurrentUser.mut_username))).ToList();

       //                         objwaitlist = objwaitlist.OrderByDescending(x => x.vip_hpc).ThenByDescending(x => x.RTN_Nurse).ThenByDescending(x => x.hold_flag).ThenBy(x => x.bmSeq).ThenBy(x => x.ordDate).ToList();
       //                     }
       //                     else if (Program.CurrentSite.mhs_code == "01HPC3")
       //                     {
       //                         objwaitlist = objwaitlist.Where(x => (x.NurseCode == null || (x.NurseCode != null && x.NurseCode == Program.CurrentUser.mut_username))).ToList();

       //                         objwaitlist = objwaitlist.OrderByDescending(x => x.vip_hpc).ThenByDescending(x => x.RTN_Nurse).ThenByDescending(x => x.hold_flag).ThenBy(x => x.bmSeq).ThenBy(x => x.ordDate).ToList();
       //                     }

       //                     //if (Program.CurrentRoom.mrd_avation == true)
       //                     //{
       //                     //    objwaitlist = objwaitlist.Where(x => x.IsPirot == '2' || x.IsPirot=='4').ToList();
       //                     //}
       //                     //else
       //                     //{
       //                     //    if (countmdc == 0)
       //                     //    {
       //                     //        objwaitlist = objwaitlist.Where(x => x.IsPirot != '2' && x.IsPirot != '4').ToList();
       //                     //    }
       //                     //}
       //                     //if (Program.CurrentRoom.mrd_pre_insure == true)
       //                     //{
       //                     //   // objwaitlist = objwaitlist.Where(x => x.mdc_id != null || x.mdc_id == null).ToList();
       //                     //   // objwaitlist = objwaitlist.Where(x => countmdc > 0).ToList();
       //                     //}
       //                     //else
       //                     //{
       //                     //    if (Convert.ToChar(v_Avia) == '2' || Convert.ToChar(v_Avia) == '4')
       //                     //    {
       //                     //        // objselect = objselect.Where(x => x.mdc_id == null).ToList();
       //                     //    }
       //                     //    else
       //                     //    {
       //                     //        // objwaitlist = objwaitlist.Where(x => x.mdc_id == null).ToList();
       //                     //        objwaitlist = objwaitlist.Where(x => countmdc == 0).ToList();
       //                     //    }
       //                     //}
       //                 }
       //                 else if (CurrentmrmCode.mrm_code == "DC")
       //                 {
       //                     char datagender = (Program.CurrentUser.mut_gender == null) ? 'Z' : Convert.ToChar(Program.CurrentUser.mut_gender);
       //                     //{//ถ้า Require Doctor
       //                     //objwaitlist = objwaitlist.Where(x => ((x.PEDoc == 'Y' && x.PEDocCode == Program.CurrentUser.mut_username) ||
       //                     //                                     (x.PEDoc == null) || (x.PEDoc == 'N'))
       //                     //                                     && (x.ReqDoc == 'Y' && x.DocCode == Program.CurrentUser.mut_username) ||
       //                     //                                     (x.ReqDoc == 'Y' && x.ReqGender == datagender) ||
       //                     //                                     (x.ReqDoc == 'N')
       //                     //                                 ).ToList();

       //                     //objwaitlist = objwaitlist.Where(x => ((x.PEDoc == 'Y' && x.PEDocCode == Program.CurrentUser.mut_username) ||
       //                     //                                   (x.PEDoc == null && ((x.countmdc > 0 && x.tprid == Convert1.ToInt32(GetpatientInPatientCate.Contains(x.tprid))) || (x.countmdc == 0)) ||
       //                     //                                   (x.PEDoc == 'N' && ((x.countmdc > 0 && x.tprid == Convert1.ToInt32(GetpatientInPatientCate.Contains(x.tprid))) || (x.countmdc == 0))))
       //                     //                                   && (x.ReqDoc == 'Y' && x.DocCode == Program.CurrentUser.mut_username) ||
       //                     //                                   (x.ReqDoc == 'Y' && x.ReqGender == datagender) ||
       //                     //                                   (x.ReqDoc == 'N' && ((x.countmdc > 0 && x.tprid == Convert1.ToInt32(GetpatientInPatientCate.Contains(x.tprid))) || (x.countmdc == 0)))
       //                     //                               )).ToList();

       //                     if (Program.CurrentSite.mhs_code == "01CHK")
       //                     {
       //                         /*if (doc_call == true)
       //                         {
       //                             //objwaitlist = objwaitlist.Where(x => 1 == 1).ToList();

       //                             objwaitlist = objwaitlist.Where(x => ((x.OutDocCode == false && x.PEDoc == 'Y' && x.PEDocCode == Program.CurrentUser.mut_username) ||
       //                                                                 (x.OutDocCode == false && x.PEDoc == null && ((x.countmdc > 0 && x.mdc_flag > 0) || (x.countmdc == 0)) ||
       //                                                                 (x.OutDocCode == false && x.PEDoc == 'N' && ((x.countmdc > 0 && x.mdc_flag > 0) || (x.countmdc == 0))))
       //                                                                 && ((x.PEDoc == 'N' || x.PEDoc == null) && x.OutDocCode == false && x.ReqDoc == 'Y' && x.DocCode == Program.CurrentUser.mut_username) ||
       //                                                                 ((x.PEDoc == 'N' || x.PEDoc == null) && x.OutDocCode == false && x.ReqDoc == 'Y' && x.DocCode == null && x.ReqGender == datagender) ||
       //                                                                 ((x.PEDoc == 'N' || x.PEDoc == null) && x.OutDocCode == false && x.ReqDoc == 'N' && ((x.countmdc > 0 && x.mdc_flag > 0) || (x.countmdc == 0))) ||
       //                                                                 (x.OutDocCode == true && x.type_PE == "Y") ||
       //                                                                (x.OutDocCode == true && x.type_PE == "N" && x.ReqDoc == 'Y' && x.DocCode == Program.CurrentUser.mut_username) ||
       //                                                                (x.OutDocCode == true && x.type_PE == "N" && x.ReqDoc == 'Y' && x.DocCode == null && x.ReqGender == datagender)
       //                                                             )).ToList();
       //                         }
       //                         else
       //                         {
       //                             //objwaitlist = objwaitlist.Where(x => 1 == 2).ToList();
       //                             objwaitlist = objwaitlist.Where(x => (
       //                                                         (x.PEDocCode != null && x.PEDocCode == Program.CurrentUser.mut_username) ||
       //                                                         (x.DocCode != null && x.DocCode == Program.CurrentUser.mut_username) ||
       //                                                         (x.countmdc > 0 && x.mdc_flag > 0)
       //                                                         )).ToList();
       //                         }*/

       //                         objwaitlist = objwaitlist.Where(x => ((x.OutDocCode == false && x.PEDoc == 'Y' && x.PEDocCode == Program.CurrentUser.mut_username) ||
       //                                                              ((x.PEDoc == 'N' || x.PEDoc == null) && x.OutDocCode == false && x.ReqDoc == 'Y' && x.DocCode == Program.CurrentUser.mut_username) ||
       //                                                              ((x.PEDoc == 'N' || x.PEDoc == null) && x.OutDocCode == false && x.ReqDoc == 'Y' && x.DocCode == null && x.ReqGender == datagender) ||
       //                                                              (x.OutDocCode == true && x.type_PE == "Y") ||
       //                                                              (x.OutDocCode == true && x.type_PE == "N" && x.ReqDoc == 'Y' && x.DocCode == Program.CurrentUser.mut_username) ||
       //                                                              (x.OutDocCode == true && x.type_PE == "N" && x.ReqDoc == 'Y' && x.DocCode == null && x.ReqGender == datagender)
       //                                                             )).ToList();
       //                     }
       //                     else if (Program.CurrentSite.mhs_code == "01HPC2")
       //                     {
       //                         /*objwaitlist = objwaitlist.Where(x => ((x.OutDocCode == false && x.PEDoc == 'Y' && x.PEDocCode == Program.CurrentUser.mut_username) ||
       //                                                             (x.OutDocCode == false && x.PEDoc == null && ((x.countmdc > 0 && x.mdc_flag > 0) || (x.countmdc == 0)) ||
       //                                                             (x.OutDocCode == false && x.PEDoc == 'N' && ((x.countmdc > 0 && x.mdc_flag > 0) || (x.countmdc == 0))))
       //                                                             && ((x.PEDoc == 'N' || x.PEDoc == null) && x.OutDocCode == false && x.ReqDoc == 'Y' && x.DocCode == Program.CurrentUser.mut_username) ||
       //                                                             ((x.PEDoc == 'N' || x.PEDoc == null) && x.OutDocCode == false && x.ReqDoc == 'Y' && x.DocCode == null && x.ReqGender == datagender) ||
       //                                                             ((x.PEDoc == 'N' || x.PEDoc == null) && x.OutDocCode == false && x.ReqDoc == 'N' && ((x.countmdc > 0 && x.mdc_flag > 0) || (x.countmdc == 0))) ||
       //                                                             (x.OutDocCode == true && x.type_PE == "Y") ||
       //                                                             (x.OutDocCode == true && x.type_PE == "N" && x.ReqDoc == 'Y' && x.DocCode == Program.CurrentUser.mut_username) ||
       //                                                             (x.OutDocCode == true && x.type_PE == "N" && x.ReqDoc == 'Y' && x.DocCode == null && x.ReqGender == datagender)
       //                                                         )).ToList();*/

       //                         objwaitlist = objwaitlist.Where(x => ((x.OutDocCode == false && x.PEDoc == 'Y' && x.PEDocCode == Program.CurrentUser.mut_username) ||
       //                                                               (x.OutDocCode == false && x.PEDoc == 'N' && x.ReqDoc == 'Y' && x.DocCode == Program.CurrentUser.mut_username) ||
       //                                                               (x.OutDocCode == false && x.PEDoc == 'N' && x.ReqDoc == 'Y' && x.DocCode == null && x.ReqGender == datagender) ||
       //                                                               (x.OutDocCode == false && x.PEDoc == 'N' && x.DocCode == null && ((x.countmdc > 0 && x.mdc_flag > 0) || (x.countmdc == 0))) ||
       //                                                               (x.OutDocCode == true && x.type_PE == "Y") ||
       //                                                               (x.OutDocCode == true && x.type_PE == "N" && x.ReqDoc == 'Y' && x.DocCode == Program.CurrentUser.mut_username) ||
       //                                                               (x.OutDocCode == true && x.type_PE == "N" && x.ReqDoc == 'Y' && x.DocCode == null && x.ReqGender == datagender)
       //                                                         )).ToList();

       //                         objwaitlist = objwaitlist.OrderBy(x => x.bmSeq).ThenByDescending(x => x.vip_hpc).ThenByDescending(x => x.hold_flag).ThenBy(x => x.type_PE).ThenBy(x => x.ordDate).ToList();
       //                     }
       //                     else
       //                     {
       //                         /*objwaitlist = objwaitlist.Where(x => ((x.OutDocCode == false && x.PEDoc == 'Y' && x.PEDocCode == Program.CurrentUser.mut_username) ||
       //                                                        (x.OutDocCode == false && x.PEDoc == null && ((x.countmdc > 0 && x.mdc_flag > 0) || (x.countmdc == 0)) ||
       //                                                        (x.OutDocCode == false && x.PEDoc == 'N' && ((x.countmdc > 0 && x.mdc_flag > 0) || (x.countmdc == 0))))
       //                                                        && ((x.PEDoc == 'N' || x.PEDoc == null) && x.OutDocCode == false && x.ReqDoc == 'Y' && x.DocCode == Program.CurrentUser.mut_username) ||
       //                                                        ((x.PEDoc == 'N' || x.PEDoc == null) && x.OutDocCode == false && x.ReqDoc == 'Y' && x.DocCode == null && x.ReqGender == datagender) ||
       //                                                        ((x.PEDoc == 'N' || x.PEDoc == null) && x.OutDocCode == false && x.ReqDoc == 'N' && ((x.countmdc > 0 && x.mdc_flag > 0) || (x.countmdc == 0))) ||
       //                                                        (x.OutDocCode == true && x.type_PE == "Y") ||
       //                                                         (x.OutDocCode == true && x.type_PE == "N" && x.ReqDoc == 'Y' && x.DocCode == Program.CurrentUser.mut_username) ||
       //                                                         (x.OutDocCode == true && x.type_PE == "N" && x.ReqDoc == 'Y' && x.DocCode == null && x.ReqGender == datagender)
       //                                                    )).ToList();*/

       //                         objwaitlist = objwaitlist.Where(x => ((x.OutDocCode == false && x.PEDoc == 'Y' && x.PEDocCode == Program.CurrentUser.mut_username) ||
       //                                                               (x.OutDocCode == false && x.PEDoc == 'N' && x.ReqDoc == 'Y' && x.DocCode == Program.CurrentUser.mut_username) ||
       //                                                               (x.OutDocCode == false && x.PEDoc == 'N' && x.ReqDoc == 'Y' && x.DocCode == null && x.ReqGender == datagender) ||
       //                                                               (x.OutDocCode == false && x.PEDoc == 'N' && x.DocCode == null && ((x.countmdc > 0 && x.mdc_flag > 0) || (x.countmdc == 0))) ||
       //                                                               (x.OutDocCode == true && x.type_PE == "Y") ||
       //                                                               (x.OutDocCode == true && x.type_PE == "N" && x.ReqDoc == 'Y' && x.DocCode == Program.CurrentUser.mut_username) ||
       //                                                               (x.OutDocCode == true && x.type_PE == "N" && x.ReqDoc == 'Y' && x.DocCode == null && x.ReqGender == datagender)
       //                                                         )).ToList();

       //                         objwaitlist = objwaitlist.OrderBy(x => x.bmSeq).ThenByDescending(x => x.vip_hpc).ThenByDescending(x => x.hold_flag).ThenBy(x => x.type_PE).ThenBy(x => x.ordDate).ToList();
       //                     }

       //                     /*objwaitlist = objwaitlist.Where(x => ((x.PEDoc == 'Y' && x.PEDocCode == Program.CurrentUser.mut_username) ||
       //                                                        (x.PEDoc == null && ((x.countmdc > 0 && x.mdc_flag > 0) || (x.countmdc == 0)) ||
       //                                                        (x.PEDoc == 'N' && ((x.countmdc > 0 && x.mdc_flag > 0) || (x.countmdc == 0))))
       //                                                        && (x.ReqDoc == 'Y' && x.DocCode == Program.CurrentUser.mut_username) ||
       //                                                        (x.ReqDoc == 'Y' && x.ReqGender == datagender) ||
       //                                                        (x.ReqDoc == 'N' && ((x.countmdc > 0 && x.mdc_flag > 0) || (x.countmdc == 0)))
       //                                                    )).ToList();*/
       //                 }
       //                 else if (CurrentmrmCode.mrm_code == "EM")
       //                 {//ถ้า เป็น Eye ให้แยกตาม Type mvt_id
       //                     string strCode = "";
       //                     if (Program.CurrentRoom.mrd_type == 'D')
       //                     {
       //                         strCode = "EM";
       //                     }
       //                     else if (Program.CurrentRoom.mrd_type == 'N')
       //                     {
       //                         strCode = "EN";
       //                     }
       //                     var currentEvent = (from t1 in dbc.mst_events
       //                                         where t1.mvt_code == strCode
       //                                         select t1).FirstOrDefault();
       //                     if (currentEvent != null)
       //                     {
       //                         objwaitlist = objwaitlist.Where(x => x.mvtID == currentEvent.mvt_id).ToList();
       //                     }
       //                 }
       //                 else if (CurrentmrmCode.mrm_code == "TE")
       //                 {//ถ้า เป็น Teeth ให้แยกตาม Type mvt_id
       //                     string strCode = "";
       //                     if (Program.CurrentRoom.mrd_type == 'T')
       //                     {
       //                         strCode = "TX";
       //                     }
       //                     else if (Program.CurrentRoom.mrd_type == 'D')
       //                     {
       //                         strCode = "TE";
       //                     }
       //                     var currentEvent = (from t1 in dbc.mst_events
       //                                         where t1.mvt_code == strCode
       //                                         select t1).FirstOrDefault();
       //                     if (currentEvent != null)
       //                     {
       //                         objwaitlist = objwaitlist.Where(x => x.mvtID == currentEvent.mvt_id).ToList();
       //                     }
       //                 }
       //                 else if (CurrentmrmCode.mrm_code == "UU")
       //                 {
       //                     //objwaitlist = objwaitlist.OrderBy(x => x.bmSeq).ThenByDescending(x => x.type_Lower).ThenByDescending(x => x.hold_flag).ThenBy(x => x.ordDate).ToList();

       //                     if (Program.CurrentSite.mhs_code == "01CHK")
       //                     {
       //                         objwaitlist = objwaitlist.OrderBy(x => x.bmSeq).ThenByDescending(x => x.type_Lower).ThenByDescending(x => x.hold_flag).ThenBy(x => x.ordDate).ToList();
       //                     }
       //                     else
       //                     {
       //                         objwaitlist = objwaitlist.OrderBy(x => x.bmSeq).ThenByDescending(x => x.type_Lower).ThenByDescending(x => x.vip_hpc).ThenByDescending(x => x.hold_flag).ThenBy(x => x.ordDate).ToList();
       //                     }
       //                 }
       //             }
                    
       //             //****************************
       //             GridWaitingList.DataSource = new SortableBindingList<waitinglist>(objwaitlist.Select((item, index) => new waitinglist
       //             {
       //                 NO = index + 1,
       //                 Callstatus=item.Callstatus,
       //                 QueueNo = item.QueueNo,
       //                 HN = item.HN,
       //                 FullName = item.FullName,
       //                 tpr_id = item.tprid,
       //                 hilight = item.hilight
       //             }).ToList());
       //             lbTitle.Text = string.Format("Waiting List (Total {0} คน)", objwaitlist.Count());
       //             timer1.Start();// Stat Timer
       //         }
       //         catch (Exception ex)
       //         {
       //             Program.MessageError("UIWaitList", "LoadData", ex, false);
       //             timer1.Start();
       //         }
       //     }
       //}
        public void IsStatusED()
        {
            timer1.Stop();
            if (Program.IsViewHistory == false)
            {
                if (CallQueue.IsStatusED())
                {
                    OnRefreshStatusED();
                    //MessageBox.Show("Now Status Completed.");
                    Program.CurrentPatient_queue = null;
                }
            }
            timer1.Start();
        }

        private void GridWaitingList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < GridWaitingList.Rows.Count; i++)
            {
                if (GridWaitingList.Rows[i].Cells["Colcallstatus"].Value != null && GridWaitingList.Rows[i].Cells["Colcallstatus"].Value.ToString() == "HD")
                {
                    GridWaitingList.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                    GridWaitingList.Rows[i].DefaultCellStyle.SelectionForeColor = Color.Red;
                }
            }
        }

        private void GridWaitingList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            try
            {
                if (!_ableDoubleClick) return;

                Program.RefreshWaiting = false;
                int tpr_id = (int)(GridWaitingList.Rows[e.RowIndex].Cells["tpr_id"].Value);
                string queueNo = "";
                int tps_id = 0;
                frmBGScreen frmbg = new frmBGScreen();
                frmbg.Show();
                Application.DoEvents();
                StatusTransaction onWaiting = new Class.FunctionDataCls().checkStatusWaiting(tpr_id, Program.CurrentRoom.mrm_id, ref tps_id, ref queueNo);
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
                Program.MessageError("UIWaitList", "GridWaitingList_CellDoubleClick", ex, false);
            }
            finally
            {
                Program.RefreshWaiting = true;
            }
        }

        private void GridWaitingList_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            waitinglist data = (waitinglist)dgv.Rows[e.RowIndex].DataBoundItem;
            switch (data.hilight)
            {
                //case false:
                //    dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(248, 241, 7);
                //    break;
                case true:
                    dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(41, 242, 13);
                    dgv.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(41, 242, 13);
                    break;
            }
        }
    }
    public class waitinglist
    {
        public int NO { get; set; }
        public string QueueNo { get; set; }
        public string HN { get; set; }
        public string FullName { get; set; }
        public string Callstatus { get; set; }
        public int tpr_id { get; set; }
        public bool hilight { get; set; }
    }

}
