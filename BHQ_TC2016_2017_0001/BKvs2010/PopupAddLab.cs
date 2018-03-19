using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;
using System.IO;
using System.Diagnostics;
using BKvs2010.EmrClass;

namespace BKvs2010
{
    public partial class PopupAddLab : Form
    {
        List<string> _listEN;
        string _hn;
        int _tprIDCurrent;
        InhCheckupDataContext dbc;
        public PopupAddLab()
        {
            InitializeComponent();
        }
        public PopupAddLab( string hn, int tprIDCurrent)
        {
            InitializeComponent();
           
            _hn = hn;
            _tprIDCurrent = tprIDCurrent;
           
          
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                trn_patient_regi patient_regis = cdc.trn_patient_regis
                                                               .Where(x => x.tpr_id == _tprIDCurrent)
                                                               .FirstOrDefault();


                var objregis = from t1 in cdc.trn_patient_labs
                               where t1.tpl_hn_no == _hn && t1.tpl_en_no != patient_regis.tpr_en_no
                               select new { t1.tpl_en_no, t1.tpl_lab_date };
               // var objregisGrp = objregis.GroupBy(t => new { t.tpl_en_no, t.tpl_lab_date }).ToList();
                var objregisGrp = objregis.GroupBy(t => new { t.tpl_en_no, t.tpl_lab_date }).OrderByDescending( d => d.First().tpl_lab_date).ToList();

                DataTable dt = new DataTable("result");
                dt.Columns.Add("tpl_en_no");
                dt.Columns.Add("tpl_lab_date");
                
                foreach (var group in objregisGrp)
                {
                    var groupKey = group.Key;
                        DataRow dr = dt.NewRow();
                        dr["tpl_en_no"] = groupKey.tpl_en_no;
                        try
                        {
                            dr["tpl_lab_date"] = groupKey.tpl_lab_date.Value.ToShortDateString();
                        }
                        catch (Exception ex) { }
                        dt.Rows.Add(dr);
                }


                if (dt.Rows.Count == 0)
                    lblEpisode.Text = "Not found lab";
                else
                {
                    lblEpisode.Text = "Please select episode for add lab";
                    dgvEnList.DataSource = dt;

                }
            }
         
        }

        private void btnSaveLab_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                string username = Program.CurrentUser == null ? "" : Program.CurrentUser.mut_username;
                DateTime dateNow = Program.GetServerDateTime();
                foreach (DataGridViewRow row in dgvEnList.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["Select"].FormattedValue))
                      {
                        int? tpr_id = null;
                        ImportPatient(ref tpr_id, row.Cells["EN"].Value.ToString());
                        StatusTransaction tran = importPatient((int)tpr_id);
                        //merge to main en
                        using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                        {
                            var tpegNew = cdc.trn_patient_ass_grps.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                            var tpegCurrent = cdc.trn_patient_ass_grps.Where(x => x.tpr_id == _tprIDCurrent).FirstOrDefault();
                            foreach (var hdrNew in tpegNew.trn_patient_ass_hdrs)
                            {
                                //add lab header
                                bool isMergeHdr = true;
                                var hdrCurrent = tpegCurrent.trn_patient_ass_hdrs.Where(x => x.tpeh_order_code == hdrNew.tpeh_order_code).FirstOrDefault();
                                if (hdrCurrent == null)//new Hdr
                                {
                                    isMergeHdr = false;
                                    hdrCurrent = new trn_patient_ass_hdr
                                    {
                                        tpeh_order_code = hdrNew.tpeh_order_code,
                                        tpeh_order_name = hdrNew.tpeh_order_name,
                                        tpeh_create_by = username,
                                        tpeh_create_date = dateNow,
                                        tpeh_summary = hdrNew.tpeh_summary,
                                        tpeh_pat_education = hdrNew.tpeh_pat_education,
                                        tpeh_status = hdrNew.tpeh_status,
                                        tpeh_update_by = username,
                                        tpeh_update_date = dateNow
                                    };
                                    tpegCurrent.trn_patient_ass_hdrs.Add(hdrCurrent);
                                }
                                //end add lab header
                                foreach (var dtlNew in hdrNew.trn_patient_ass_dtls)
                                {
                                    var dtlCurrent = hdrCurrent.trn_patient_ass_dtls.Where(x => x.tped_lab_code == dtlNew.tped_lab_code && x.tped_lab_AddLabEN == row.Cells["EN"].Value.ToString()).FirstOrDefault();
                                    if (dtlCurrent == null)
                                    {
                                        dtlCurrent = new trn_patient_ass_dtl
                                        {
                                            tped_lab_code = dtlNew.tped_lab_code,
                                            tped_lab_name = dtlNew.tped_lab_name,
                                            tped_create_by = username,
                                            tped_create_date = dateNow
                                        };
                                        hdrCurrent.trn_patient_ass_dtls.Add(dtlCurrent);
                                    }
                                    dtlCurrent.tped_lab_value = dtlNew.tped_lab_value;
                                    dtlCurrent.mlr_id = dtlNew.mlr_id;
                                    dtlCurrent.tped_summary = dtlNew.tped_summary;
                                    dtlCurrent.tped_lab_unit = dtlNew.tped_lab_unit;
                                    dtlCurrent.tped_lab_nrange = dtlNew.tped_lab_nrange;
                                    dtlCurrent.tped_lab_result_eng = dtlNew.tped_lab_result_eng;
                                    dtlCurrent.tped_lab_result_thai = dtlNew.tped_lab_result_thai;
                                    dtlCurrent.tped_status = dtlNew.tped_status;
                                    dtlCurrent.tped_update_by = username;
                                    dtlCurrent.tped_update_date = dateNow;
                                    dtlCurrent.tped_lab_result_status = "DF";
                                    dtlCurrent.tped_lab_AddLabEN = row.Cells["EN"].Value.ToString();
                                }
                                //compute tpeh_pat_education
                                if (isMergeHdr)
                                {
                                    //var edu = hdrCurrent.trn_patient_ass_dtls.Where(x => x.tped_summary == 'A' && !string.IsNullOrEmpty(x.tped_lab_result_thai)).Select(x => x.tped_lab_result_thai).ToList();
                                    var edu = hdrCurrent.trn_patient_ass_dtls.Where(x => x.tped_summary == 'A' && !string.IsNullOrEmpty(x.tped_lab_result_thai)).Select(x => x.tped_lab_result_thai).Distinct().ToList();   
                                    hdrCurrent.tpeh_pat_education = string.Join(", ", edu);
                                }
                            
                            }
                            cdc.SubmitChanges();//for each header
                        }
                  }
                }
                
                MessageBox.Show("Save completed");
                
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally 
            {
                Cursor.Current = Cursors.Default;
                this.Close();
            }
        } 
         private bool ImportPatient(ref int? tpr_id,string en)
        {
            try
            {
                tpr_id = null;
                using (Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls())
                {
                   
                    using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                        {
                            trn_patient_regi patient_regis = cdc.trn_patient_regis
                                                                .Where(x => x.tpr_en_no == en)
                                                                .FirstOrDefault();
                            if (patient_regis == null)
                            {
                                var resultArrived = ws.GetPTArrivedCheckUpFilter(string.Empty, en, string.Empty).AsEnumerable() 
                                                      .Select(x => new tmp_getptarrived
                                                      {
                                                          paadm_type_of_patient_calc = x.Field<string>("PAADM_Type_of_Patient_Calc"),
                                                          paadm_rowid = x.Field<int>("PAADM_RowID").ToString(),
                                                          appt_rowid = x.Field<string>("APPT_RowId"),
                                                          appt_arrivaltime = x.Field<TimeSpan>("APPT_ArrivalTime").ToString(),
                                                          paadm_admdate = x.Field<DateTime?>("PAADM_AdmDate"),
                                                          allergy_eng = new APITrakcare.GetAllergyCls().GetByHN(x.Field<string>("PAPMI_No")),//call webserice
                                                          paadm_admno = x.Field<string>("PAADM_ADMNo"),
                                                          papmi_no = x.Field<string>("PAPMI_No"),
                                                          ttl_desc = x.Field<string>("TTL_Desc"),
                                                          papmi_name = x.Field<string>("PAPMI_Name"),
                                                          papmi_name2 = x.Field<string>("PAPMI_Name2"),
                                                          appt_transdate = x.Field<DateTime?>("APPT_TransDate"),
                                                          appt_datesearch = x.Field<DateTime?>("APPT_DateSearch"),
                                                          paadm_admtime = x.Field<TimeSpan>("PAADM_admTime").ToString(),
                                                          ctloc_code = Program.CurrentSite.mhs_code,
                                                          ctloc_desc = x.Field<string>("CTLOC_Desc"),
                                                          penstype_code = x.Field<string>("PENSTYPE_Code"),
                                                          penstype_desc = x.Field<string>("PENSTYPE_Desc"),
                                                          ser_rowid = x.Field<int>("SER_RowId"),
                                                          ser_desc = x.Field<string>("SER_Desc"),
                                                          ctnat_code = x.Field<string>("CTNAT_Code"),
                                                          ctnat_desc = x.Field<string>("CTNAT_Desc"),
                                                          ctsex_code = x.Field<string>("CTSEX_Code"),
                                                          ctsex_desc = x.Field<string>("CTSEX_Desc"),
                                                          papmi_dob = x.Field<DateTime?>("PAPMI_DOB"),
                                                          paper_photo = new EmrClass.GetPatientImage().getByPath(x.Field<string>("PAPMI_No")),

                                                          paper_ageyr = x.Field<string>("PAPER_AgeYr"),
                                                          paper_agemth = x.Field<string>("PAPER_AgeMth"),
                                                          paper_ageday = x.Field<string>("PAPER_AgeDay"),
                                                          paper_stname = x.Field<string>("PAPER_StName"),
                                                          citarea_desc = x.Field<string>("CITAREA_Desc"),
                                                          prov_desc = x.Field<string>("PROV_Desc"),
                                                          ctcit_desc = x.Field<string>("CTCIT_Desc"),
                                                          ctzip_code = x.Field<string>("CTZIP_Code"),
                                                          paper_id = x.Field<string>("PAPER_ID"),
                                                          paper_telo = x.Field<string>("PAPER_TelO"),
                                                          paper_telh = x.Field<string>("PAPER_TelH"),
                                                          paper_mobphone = x.Field<string>("PAPER_MobPhone"),
                                                          paper_email = x.Field<string>("PAPER_Email"),

                                                          ctmar_desc = x.Field<string>("CTMAR_Desc"),
                                                          paper_name5 = x.Table.Columns.Contains("PAPMI_Name5") 
                                                                        ? x.Field<string>("PAPMI_Name5") 
                                                                        : x.Table.Columns.Contains("PAPER_Name5") 
                                                                        ? x.Field<string>("PAPER_Name5") 
                                                                        : "",
                                                          paper_name6 = x.Table.Columns.Contains("PAPMI_Name6") 
                                                                        ? x.Field<string>("PAPMI_Name6") 
                                                                        : x.Table.Columns.Contains("PAPER_Name6") 
                                                                        ? x.Field<string>("PAPER_Name6")
                                                                        : "",
                                                          paper_name7 = x.Table.Columns.Contains("PAPMI_Name7") 
                                                                        ? x.Field<string>("PAPMI_Name7") 
                                                                        : x.Table.Columns.Contains("PAPER_Name7") 
                                                                        ? x.Field<string>("PAPER_Name7") 
                                                                        : "",
                                                          papmi_dob_text = x.Field<string>("papmi_dob_text")
                                                      }).FirstOrDefault();
                                if (resultArrived == null)
                                {
                                    MessageBox.Show("ไม่พบ Episode นี้" , "Warning!!");
                                    return false;
                                }
                                else
                                {
                                    bool regis = new EmrClass.FunctionDataCls().RegisterPatient(resultArrived);
                                    if (!regis)
                                    {
                                        MessageBox.Show("โปรดลองอีกครั้ง");
                                        return false;
                                    }
                                    else
                                    {
                                        patient_regis = cdc.trn_patient_regis
                                                            .Where(x => x.tpr_en_no == en)
                                                            .FirstOrDefault();
                                    }
                                }
                            }

                            tpr_id = patient_regis.tpr_id;
                            return true;
                       
                    }
                }
            }
            catch
            {
                MessageBox.Show("โปรดลองอีกครั้ง");
                return false;
            }
        }

         public StatusTransaction importPatient(int tpr_id)
         {
             try
             {
                 using (InhCheckupDataContext contxt = new InhCheckupDataContext())
                 {
                     DateTime dateNow = Program.GetServerDateTime();
                     //List<trn_RefreshLabHistory> his = contxt.trn_RefreshLabHistories.Where(x => x.tpr_id == tpr_id).ToList();
                     //his.ForEach(x => x.status = true);
                     //contxt.SubmitChanges();

                     trn_patient_regi patient_regis = contxt.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                     //using (Service.WS_CheckupCls wsCheckup = new Service.WS_CheckupCls())
                     //{
                     //    wsCheckup.InsertDBEmrCheckupResultXray(patient_regis.trn_patient.tpt_hn_no, patient_regis.tpr_en_no, dateNow.AddYears(-5), dateNow, true);
                     //}
                 }

                 using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                 {
                     try
                     {
                         string username = Program.CurrentUser == null ? "" : Program.CurrentUser.mut_username;
                         DateTime dateNow = Program.GetServerDateTime();
                         cdc.Connection.Open();
                         DbTransaction trans = cdc.Connection.BeginTransaction();
                         cdc.Transaction = trans;

                         trn_patient_regi patient_regis = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();

                         patient_regis.trn_patient_queues.ToList().ForEach(x => { x.tps_status = "ED"; x.tps_ns_status = null; x.tps_update_by = username; x.tps_update_date = dateNow; });

                         int enRowID = Convert.ToInt32(patient_regis.tpr_en_rowid);
                         EmrClass.GetPTPackageCls PackageCls = new EmrClass.GetPTPackageCls();
                         EnumerableRowCollection<DataRow> getPTPackage = PackageCls.GetPTPackage(enRowID);
                         PackageCls.AddPatientOrderItem(ref patient_regis, username, dateNow, getPTPackage);
                         PackageCls.AddPatientOrderSet(ref patient_regis, username, dateNow, getPTPackage);
                         List<MapOrderEvent> mapOrder = PackageCls.MapEvent(getPTPackage);
                         PackageCls.AddPatientEvent(ref patient_regis, username, dateNow, mapOrder);
                         PackageCls.AddPatientPlan(ref patient_regis, username, dateNow, mapOrder);
                         PackageCls.skipReqDoctorOutDepartment(ref patient_regis);
                         PackageCls.CompleteEcho(ref patient_regis);
                         PackageCls.skipChangeEstToEcho(ref patient_regis, patient_regis.mhs_id);
                         PackageCls.checkOrderPMR(ref patient_regis, patient_regis.mhs_id);

                         patient_regis.tpr_status = "WB";
                         patient_regis.tpr_pe_status = "RS";
                         try
                         {
                             cdc.SubmitChanges();
                         }
                         catch (System.Data.Linq.ChangeConflictException)
                         {
                             foreach (System.Data.Linq.ObjectChangeConflict occ in cdc.ChangeConflicts)
                             {
                                 cdc.Refresh(System.Data.Linq.RefreshMode.KeepChanges, occ.Object);
                             }
                             cdc.SubmitChanges();
                         }
                         PackageCls.setRelationOrderSet(ref patient_regis);
                         cdc.SubmitChanges();
                         cdc.Transaction.Commit();

                         using (Service.WS_CheckupCls ws = new Service.WS_CheckupCls())
                         {
                             ws.retrieveVitalSign(tpr_id, username);
                             ws.getCheckUpLabResult("", tpr_id);
                         }
                         return StatusTransaction.True;
                     }
                     catch (Exception ex)
                     {
                         //cdc.Transaction.Rollback();
                         Program.MessageError("ImportPatientToBookCls", "importPatient", ex, false);
                         return StatusTransaction.Error;
                     }
                     finally
                     {
                         //cdc.Connection.Close();
                     }
                 }
             }
             catch (Exception ex)
             {
                 Program.MessageError("ImportPatientToBookCls", "importPatient", ex, false);
                 return StatusTransaction.Error;
             }
         }

         private void btnCancel_Click(object sender, EventArgs e)
         {
             this.Close();
         }
    }
}
