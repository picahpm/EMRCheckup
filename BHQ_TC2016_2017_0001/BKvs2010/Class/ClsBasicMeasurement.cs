using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using DBCheckup;

namespace BKvs2010
{
    public class ClsBasicMeasurement
    {
        public static void SaveBasicMeasurment(int tpr_id, bool _IsCallLab)
        {
            if (_IsCallLab == true)
            {
                using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                {
                    trn_patient_regi tpr = dbc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                    if (tpr != null)
                    {
                        trn_basic_measure_hdr hdr = tpr.trn_basic_measure_hdrs.FirstOrDefault();
                        if (hdr == null)
                        {
                            hdr = new trn_basic_measure_hdr();
                            tpr.trn_basic_measure_hdrs.Add(hdr);
                        }

                        List<trn_basic_measure_dtl> dtl = hdr.trn_basic_measure_dtls.ToList();
                        if (dtl.Count() > 0)
                        {
                            dbc.trn_basic_measure_dtls.DeleteAllOnSubmit(dtl);
                        }

                        EmrClass.GetDataFromWSTrakCare tk = new EmrClass.GetDataFromWSTrakCare();
                        List<trn_basic_measure_dtl> newBmDtl = tk.getVitalSignByHN(tpr.trn_patient.tpt_hn_no);
                        if (newBmDtl != null && newBmDtl.Count() > 0)
                        {
                            trn_basic_measure_dtl last_basic_dtl = newBmDtl.OrderByDescending(x => x.tbd_date).FirstOrDefault();
                            if (last_basic_dtl != null)
                            {
                                hdr.tbm_glass_or_contact = last_basic_dtl.tbd_vision_with_lens == true ? 'Y' : 'N';
                            }
                            if (newBmDtl != null && newBmDtl.Count > 0) hdr.trn_basic_measure_dtls.AddRange(newBmDtl);
                        }
                        dbc.SubmitChanges();
                    }

                    //if (Program.CurrentRegis != null)
                    //{

                        //var objbmhdr = dbc.trn_basic_measure_hdrs.Where(c => c.tpr_id == tpr_id).ToList();

                        //dbc.trn_basic_measure_dtls.DeleteAllOnSubmit(bmDtl);
                        //if (objbmhdr.Count > 0)
                        //{
                            
                            //del data in trn_basic_measurement by tbm_id
                            //var objdeldtl = dbc.trn_basic_measure_dtls.Where(c => c.tbm_id == objbmhdr[0].tbm_id);
                            //dbc.trn_basic_measure_dtls.DeleteAllOnSubmit(objdeldtl);
                            //foreach (var data in objdeldtl)
                            //{
                            //    dbc.trn_basic_measure_dtls.DeleteOnSubmit(data);
                            //}
                            //dbc.SubmitChanges();

                        //    Ws_GetDataByTrak.WS_GetDataBytrakSoapClient ws = new Ws_GetDataByTrak.WS_GetDataBytrakSoapClient();
                            
                        //    var hn = dbc.trn_patients.Where(c => c.tpt_id == Program.CurrentRegis.tpt_id).FirstOrDefault();
                            
                        //    //Search By HN
                        //    DataTable dt = ws.GetVitalSignByHN(hn.tpt_hn_no);

                        //    if (dt.Rows.Count != 0)
                        //    {
                        //        //ค้นหาวันที่ 5 วันล่าสุด จาก ws
                        //        var objseldatetop5 = (from myRow in dt.AsEnumerable()
                        //                              select myRow).GroupBy(c => c.Field<DateTime>("OBS_Date")).Take(5).ToList();

                        //        for (int i = 0; i < objseldatetop5.Count; i++)
                        //        {
                        //            //ค้นหาข้อมูลตามวันที่
                        //            trn_basic_measure_dtl objnew = new trn_basic_measure_dtl();

                        //            var objseldata = (from myRow in dt.AsEnumerable()
                        //                              where myRow.Field<DateTime>("OBS_Date") == objseldatetop5[i].Key
                        //                              select myRow).ToList();

                        //            //save data to trn_basic_measurement_dlt
                        //            foreach (var data in objseldata)
                        //            {
                        //                switch ((int)data["OBS_Item_DR"])
                        //                {
                        //                    case 230:
                        //                        objnew.tbd_weight = (string)data["OBS_Value"];
                        //                        break;
                        //                    case 231:
                        //                        objnew.tbd_height = (string)data["OBS_Value"];
                        //                        break;
                        //                    case 11:
                        //                        objnew.tbd_temp = (string)data["OBS_Value"];
                        //                        break;
                        //                    case 129:
                        //                        objnew.tbd_systolic = (string)data["OBS_Value"];
                        //                        break;
                        //                    case 128:
                        //                        objnew.tbd_diastolic = (string)data["OBS_Value"];
                        //                        break;
                        //                    case 9:
                        //                        objnew.tbd_pulse = (string)data["OBS_Value"];
                        //                        break;
                        //                    case 10:
                        //                        objnew.tbd_rr = (string)data["OBS_Value"];
                        //                        break;
                        //                    case 134:
                        //                        objnew.tbd_bmi = (string)data["OBS_Value"];
                        //                        break;
                        //                    case 176:
                        //                        objnew.tbd_waist = (string)data["OBS_Value"];
                        //                        break;
                        //                    case 173:
                        //                        objnew.tbd_vision_lt = (string)data["OBS_Value"];
                        //                        break;
                        //                    case 174:
                        //                        objnew.tbd_vision_rt = (string)data["OBS_Value"];
                        //                        break;
                        //                }

                        //                string _DateTime = String.Format("{0:yyyy/MM/dd}", data["OBS_Date"]);

                        //                objnew.tbd_date = Convert.ToDateTime(_DateTime + " " + data["OBS_Time"]);
                        //                objnew.tbm_id = objbmhdr[0].tbm_id;
                        //                objnew.tbd_create_by = Program.CurrentUser.mut_username;
                        //                objnew.tbd_create_date = Program.GetServerDateTime();

                        //                objnew.tbd_update_by = Program.CurrentUser.mut_username;
                        //                objnew.tbd_update_date = Program.GetServerDateTime();
                        //            }

                        //            dbc.trn_basic_measure_dtls.InsertOnSubmit(objnew);
                        //        }
                        //    }

                        //    dbc.SubmitChanges();
                        //}

                    //} //if cur regis
                } //using
            }
        }
    }
}
    
