using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;
namespace BKvs2010
{
    public partial class frmChoiceRoomCancel : Form
    {
        public frmChoiceRoomCancel()
        {
            InitializeComponent();
        }
        InhCheckupDataContext dbc = new InhCheckupDataContext();

        private bool _complete = true;
        public bool complete
        {
            get
            {
                return _complete;
            }
        }

        private void addDataComboReason()
        {
            var objDocResult = dbc.mst_doc_results
                                  .Where(x => x.mst_doc_result_hdr.mrh_code == "CL"
                                           && x.mst_doc_result_hdr.mrm_id == Program.CurrentRoom.mrm_id)
                                  .Select(x => new
                                  {
                                      x.mdr_code,
                                      x.mdr_tname
                                  }).ToList();

            CBReason.DataSource = objDocResult;
            CBReason.DisplayMember = "mdr_tname";
            CBReason.ValueMember = "mdr_tname";
        }

        private void addDataGridStation()
        {
            var objview = (from t1 in dbc.vw_patient_rooms
                           where t1.mhs_id == Program.CurrentSite.mhs_id
                           && t1.tpr_id == Program.CurrentRegis.tpr_id
                           //&& t1.site_rm == Program.CurrentSite.mhs_id
                           //&& t1.mvt_type_cate == 'M'
                           && t1.mvt_type_cate ==
                            (CallQueue.GetUltrasound_Type(Program.CurrentRegis.tpr_id, Program.CurrentSite.mhs_id) == true ||
                            (t1.mvt_code != "UL" && t1.mvt_code != "UB" && t1.mvt_code != "UW") ? 'M' : t1.mvt_type_cate)
                           select new
                           {
                               t1.mvt_id,
                               t1.mrm_id,
                               t1.mhs_ename,
                               t1.mze_ename,
                               t1.mrm_ename,
                               t1.waiting_person,
                               t1.waiting_time
                           }).ToList();
            gridStation.DataSource = objview;
        }

        private void frmChoiceRoomCancel_Load(object sender, EventArgs e)
        {
            addDataComboReason();
            addDataGridStation();
            // Add Button in Gridview
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            int tpr_id = Program.CurrentRegis.tpr_id;
            StatusTransaction checkPatientOnCheckB = new Class.FunctionDataCls().checkStatusPatientOnCheckPointB(tpr_id, Program.CurrentRoom.mrm_id);
            if (checkPatientOnCheckB == StatusTransaction.True)
            {
                Class.ClsPendingOnStation cls = new Class.ClsPendingOnStation();
                List<int> list_mvt_id = new List<int>();
                foreach (DataGridViewRow dr in gridStation.Rows)
                {
                    if (dr.Cells[0].Value != null)
                    {
                        if ((bool)dr.Cells[Colselect.Name].Value == true)
                        {
                            int mvt_id = Convert.ToInt32(dr.Cells["colmvtid"].Value);
                            list_mvt_id.Add(mvt_id);
                        }
                    }
                }
                //int mhs_id = Program.CurrentSite.mhs_id;
                StatusTransaction pandingOnCheckB = new Class.ClsPendingOnStation().pendingPatientCheckpointB(list_mvt_id);
                if (pandingOnCheckB == StatusTransaction.Error)
                {
                    MessageBox.Show("Alert.", "กรุณา Cancel อีกครั้ง", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (pandingOnCheckB == StatusTransaction.True)
                {
                    this.DialogResult = DialogResult.OK;
                }
            }
            else if (checkPatientOnCheckB == StatusTransaction.False)
            {
                MessageBox.Show("คนไข้ไม่ได้อยู่ในสถานะ ที่จะยกเลิก Station ได้", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.Cancel;
            }

            //string strReason="";
            //strReason = CBReason.SelectedValue.ToString();
            //if (txtReason.Text.Trim().Length > 0)
            //{
            //    strReason += txtReason.Text;
            //}
            //if (strReason.Trim() == "")
            //{
            //    txtReason.Focus();
            //    return;
            //}

            //bool isupdate = false;

            //for (int iRow = 0; iRow <= dataGridView2.Rows.Count - 1; iRow++)
            //{
                
            //    if (dataGridView2[0, iRow].Value != null && Convert.ToBoolean(dataGridView2[0, iRow].Value) == true)
            //    {
            //        int mvtid = Convert.ToInt32(dataGridView2["colmvtid", iRow].Value);
            //        int mrmid = Convert.ToInt32(dataGridView2["colRoomid", iRow].Value);

            //        //noina modufy

            //         int count_te = (from data in dbc.mst_room_hdrs where data.mrm_id == mrmid && data.mrm_code == "TE" select data).Count();
            //         if (count_te == 1)
            //         {
            //             var objpp = (from t1 in dbc.trn_patient_plans
            //                          where t1.tpr_id == Program.CurrentRegis.tpr_id
            //                          && t1.mvt_id == mvtid
            //                          orderby t1.tpr_id descending
            //                          select t1).ToList();
            //             if (objpp != null)
            //             {
            //                 foreach (var dr in objpp)
            //                 {
            //                     dr.tpl_status = 'C';
            //                     dbc.SubmitChanges();
            //                 }
            //             }
            //         }
            //         else
            //         {
            //             //หา mvtid ของ mrmid ชุดนั้นๆ ออกมา ก่อน แล้ว นำไปอัพเดต ที trn_patient_plans tpl_status = C
            //             var objmre = (from mre in dbc.mst_room_events where mre.mrm_id == mrmid select mre).ToList();
            //             if (objmre != null)
            //             {
            //                 foreach (var dr in objmre)
            //                 {
            //                     var objpp = (from t1 in dbc.trn_patient_plans
            //                                  where t1.tpr_id == Program.CurrentRegis.tpr_id
            //                                  && t1.mvt_id == dr.mvt_id
            //                                  orderby t1.tpr_id descending
            //                                  select t1).ToList();
            //                     if (objpp != null)
            //                     {
            //                         foreach (var dr2 in objpp)
            //                         {
            //                             dr2.tpl_status = 'C';
            //                             dbc.SubmitChanges();
            //                         }
            //                     }
            //                 }
            //         }
                    
            //        }

                    //code orginal comment by noina
                    //trn_patient_plan objpp = (from t1 in dbc.trn_patient_plans 
                    //                          where t1.tpr_id == Program.CurrentRegis.tpr_id 
                    //                          && t1.mvt_id == mvtid
                    //                          orderby t1.tpr_id descending
                    //                          select t1).FirstOrDefault() ;

                    //objpp.tpl_status = 'C';
                    //end
                    
            //        isupdate = true;
            //    }
            //}

            //noina comment
            //if (isupdate)
            //{//Updte trn_patient_queues Reason
            //    int mvtid = Program.Getmvt_id(dbc, "CB");
            //    DateTime dtnow = Program.GetServerDateTime();
            //    trn_patient_queue currentQueue=(from t1 in dbc.trn_patient_queues 
            //                                    where t1.tpr_id==Program.CurrentPatient_queue.tpr_id
            //                                    && t1.mvt_id == mvtid
            //                                    select t1).FirstOrDefault();
            //    if (currentQueue != null)
            //    {
            //        currentQueue.tps_cancel_remark = strReason;
            //        currentQueue.tps_update_by = Program.CurrentUser.mut_username;
            //        currentQueue.tps_update_date = dtnow;
            //        int objcount = (from t1 in dbc.trn_patient_plans
            //                                  where t1.tpr_id == Program.CurrentRegis.tpr_id
            //                                  && t1.tpl_status=='N'
            //                                  orderby t1.tpr_id descending
            //                                  select t1).Count();
            //        if (objcount == 0)
            //        {
            //            currentQueue.tps_status = "CL";
            //            currentQueue.tps_ns_status = null;
            //            currentQueue.tps_start_date = null;
            //            currentQueue.tps_end_date = null;
            //            currentQueue.tps_cancel_date = dtnow;

            //            Program.CurrentPatient_queue = null;
            //            Program.CurrentRegis = null;
            //        }
            //            dbc.SubmitChanges();
            //    }
            //}
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
