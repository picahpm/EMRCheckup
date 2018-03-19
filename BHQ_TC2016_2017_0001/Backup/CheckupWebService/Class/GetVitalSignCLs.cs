using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DBCheckup;

namespace CheckupWebService.Class
{
    public partial class GetVitalSignCLs
    {
        private class VitalSign
        {
            public int OBS_ParRef { get; set; }
            public DateTime OBS_DateTime { get; set; }
            public string PAADM_ADMNo { get; set; }
            public List<VitalSignDtl> VitalSign_Dtl { get; set; }

            public class VitalSignDtl
            {
                public int OBS_Item_DR { get; set; }
                public string OBS_Value { get; set; }
            }
        }

        public void GetVistalSign(int tpr_id, string username)
        {
            using (InhCheckupDataContext dbc = new InhCheckupDataContext())
            {
                DateTime dateNow = globalCls.GetServerDateTime();
                trn_patient_regi tpr = dbc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                if (tpr != null)
                {
                    List<string> locCheckup = dbc.mst_hpc_sites.Where(x => x.mhs_status == 'A' && x.mhs_other_clinic == false).Select(x => x.mhs_code).ToList();
                    locCheckup.Add("01AMSCHK");
                    var res = GetVitalSign(tpr.trn_patient.tpt_hn_no);//.Where(x => locCheckup.Contains(x.Location)).OrderByDescending(x => x.VitalSignDate);
                    trn_basic_measure_hdr hdr = tpr.trn_basic_measure_hdrs.FirstOrDefault();
                    if (hdr == null)
                    {
                        hdr = new trn_basic_measure_hdr();
                        hdr.tbm_create_by = username;
                        hdr.tbm_create_date = dateNow;
                        tpr.trn_basic_measure_hdrs.Add(hdr);
                    }
                    hdr.tbm_vision_left = null;
                    hdr.tbm_vision_right = null;
                    hdr.tbm_glass_or_contact = null;
                    hdr.tbm_vision_lvisual_with_lens = null;
                    hdr.tbm_vision_rvisual_with_lens = null;
                    hdr.tbm_vision_lvisual_out_lens = null;
                    hdr.tbm_vision_rvisual_out_lens = null;
                    hdr.tbm_update_by = username;
                    hdr.tbm_update_date = dateNow;

                    var cur = res.Where(x => x.en == tpr.tpr_en_no).OrderByDescending(x => x.VitalSignDate).FirstOrDefault();
                    if (cur != null)
                    {
                        hdr.tbm_vision_left = ConvertToInt(cur.VisionLeft);
                        hdr.tbm_vision_right = ConvertToInt(cur.VisionRight);
                        hdr.tbm_glass_or_contact = cur.WithLen ? 'Y' : 'N';
                        hdr.tbm_color_blind = cur.ColorBlind == null ? (char?)null : cur.ColorBlind.ToUpper() == "NA" ? 'X' : cur.ColorBlind.ToUpper() == "NO" ? 'N' : cur.ColorBlind.ToUpper() == "AB" ? 'A' : (char?)null;
                        if (hdr.tbm_glass_or_contact == 'Y')
                        {
                            hdr.tbm_vision_lvisual_with_lens = cur.VisionLeft;
                            hdr.tbm_vision_rvisual_with_lens = cur.VisionRight;
                        }
                        else
                        {
                            hdr.tbm_vision_lvisual_out_lens = cur.VisionLeft;
                            hdr.tbm_vision_rvisual_out_lens = cur.VisionRight;
                        }
                    }

                    bool start = false;
                    int count = 0;
                    List<int> list_tbd_id = hdr.trn_basic_measure_dtls.Select(x => x.tbd_id).ToList();
                    foreach (var rs in res)
                    {
                        if (rs.Equals(cur)) start = true;
                        if (start)
                        {
                            trn_basic_measure_dtl dtl = hdr.trn_basic_measure_dtls
                                                           .Where(x => x.tbd_row_id == rs.rowid && 
                                                                       x.tbd_en_no == rs.en &&
                                                                       x.tbd_date == rs.VitalSignDate)
                                                           .FirstOrDefault();
                            if (dtl == null)
                            {
                                dtl = new trn_basic_measure_dtl();
                                dtl.tbd_row_id = rs.rowid;
                                dtl.tbd_create_by = username;
                                dtl.tbd_create_date = dateNow;
                                hdr.trn_basic_measure_dtls.Add(dtl);
                            }
                            else
                            {
                                list_tbd_id.Remove(dtl.tbd_id);
                            }
                            dtl.tbd_en_no = rs.en;
                            dtl.tbd_date = rs.VitalSignDate;
                            dtl.tbd_vision_lt = rs.VisionLeft;
                            dtl.tbd_vision_rt = rs.VisionRight;
                            dtl.tbd_bmi = rs.BMI;
                            dtl.tbd_diastolic = rs.Diastolic;
                            dtl.tbd_height = rs.Height;
                            dtl.tbd_pulse = rs.Pulse;
                            dtl.tbd_rr = rs.RespirationRate;
                            dtl.tbd_systolic = rs.Systolic;
                            dtl.tbd_temp = rs.Temperature;
                            if (rs.WithLen)
                            {
                                dtl.tbd_vision_lvisual_out_lens = null;
                                dtl.tbd_vision_rvisual_out_lens = null;
                                dtl.tbd_vision_lvisual_with_lens = rs.VisionLeft;
                                dtl.tbd_vision_rvisual_with_lens = rs.VisionRight;
                            }
                            else
                            {
                                dtl.tbd_vision_lvisual_out_lens = rs.VisionLeft;
                                dtl.tbd_vision_rvisual_out_lens = rs.VisionRight;
                                dtl.tbd_vision_lvisual_with_lens = null;
                                dtl.tbd_vision_rvisual_with_lens = null;
                            }
                            dtl.tbd_vision_with_lens = rs.WithLen;
                            dtl.tbd_waist = rs.Waist;
                            dtl.tbd_weight = rs.Weight;
                            dtl.tbd_update_by = username;
                            dtl.tbd_update_date = dateNow;
                            count++;
                        }
                        if (count == 3) break;
                    }

                    List<trn_basic_measure_dtl> listDeleteDtl = hdr.trn_basic_measure_dtls.Where(x => list_tbd_id.Contains(x.tbd_id)).ToList();
                    if (listDeleteDtl.Count > 0) dbc.trn_basic_measure_dtls.DeleteAllOnSubmit(listDeleteDtl);

                    try
                    {
                        dbc.SubmitChanges();
                    }
                    catch (System.Data.Linq.ChangeConflictException)
                    {
                        foreach (System.Data.Linq.ObjectChangeConflict occ in dbc.ChangeConflicts)
                        {
                            dbc.Refresh(System.Data.Linq.RefreshMode.KeepChanges, occ.Object);
                        }
                        dbc.SubmitChanges();
                    }
                    //trn_basic_measure_hdr hdr = tpr.trn_basic_measure_hdrs.FirstOrDefault();
                    //if (hdr == null)
                    //{
                    //    hdr = new trn_basic_measure_hdr();
                    //    tpr.trn_basic_measure_hdrs.Add(hdr);
                    //}

                    //EnumerableRowCollection<DataRow> result = retrieveVitalSign(tpr.trn_patient.tpt_hn_no);
                    
                    //List<int> ListOBS_Item_DR = new List<int>
                    //{
                    //    230, 231, 11, 129, 128, 9, 10, 134, 176, 173, 174, 281
                    //};

                    //if (result != null)
                    //{
                    //    if (result.Count() > 0)
                    //    {
                    //        var grpResultByID = result.Where(x => ListOBS_Item_DR.Contains(x.Field<int>("OBS_Item_DR")))
                    //                                  .GroupBy(x => x.Field<int>("OBS_ParRef"))
                    //                                  .Select(x => new VitalSign
                    //                                  {
                    //                                      OBS_ParRef = x.Key,
                    //                                      OBS_DateTime = x.Select(y => y.Field<DateTime>("OBS_Date").Add(y.Field<TimeSpan>("OBS_Time"))).OrderByDescending(y => y).FirstOrDefault(),
                    //                                      PAADM_ADMNo = x.Select(y => y.Field<string>("PAADM_ADMNo")).FirstOrDefault(),
                    //                                      VitalSign_Dtl = x.GroupBy(y => y.Field<DateTime>("OBS_Date").Add(y.Field<TimeSpan>("OBS_Time")))
                    //                                                       .OrderByDescending(y => y.Key).FirstOrDefault()
                    //                                                       .Select(y => new VitalSign.VitalSignDtl
                    //                                                       {
                    //                                                           OBS_Item_DR = y.Field<int>("OBS_Item_DR"),
                    //                                                           OBS_Value = y.Field<string>("OBS_Value")
                    //                                                       }).ToList()
                    //                                  }).ToList()
                    //                                  .OrderByDescending(x => x.OBS_DateTime);

                    //        bool start = false;
                    //        int countRec = 0;
                    //        List<int?> listNewRowID = new List<int?>();
                    //        foreach (var rowID in grpResultByID)
                    //        {
                    //            if (rowID.PAADM_ADMNo == tpr.tpr_en_no)
                    //            {
                    //                start = true;
                    //            }
                    //            if (start)
                    //            {
                    //                countRec = countRec + 1;
                    //                listNewRowID.Add(rowID.OBS_ParRef);
                    //                trn_basic_measure_dtl dtl = hdr.trn_basic_measure_dtls.Where(x => x.tbd_row_id == rowID.OBS_ParRef).FirstOrDefault();
                    //                if (dtl == null)
                    //                {
                    //                    dtl = new trn_basic_measure_dtl();
                    //                    dtl.tbd_en_no = rowID.PAADM_ADMNo;
                    //                    dtl.tbd_row_id = rowID.OBS_ParRef;
                    //                    dtl.tbd_date = rowID.OBS_DateTime;
                    //                    dtl.tbd_create_by = username;
                    //                    dtl.tbd_create_date = dateNow;
                    //                    hdr.trn_basic_measure_dtls.Add(dtl);
                    //                }
                    //                dtl.tbd_weight = null;
                    //                dtl.tbd_height = null;
                    //                dtl.tbd_temp = null;
                    //                dtl.tbd_systolic = null;
                    //                dtl.tbd_diastolic = null;
                    //                dtl.tbd_pulse = null;
                    //                dtl.tbd_rr = null;
                    //                dtl.tbd_bmi = null;
                    //                dtl.tbd_waist = null;
                    //                dtl.tbd_vision_lt = null;
                    //                dtl.tbd_vision_rt = null;
                    //                foreach (var item in rowID.VitalSign_Dtl)
                    //                {
                    //                    switch (item.OBS_Item_DR)
                    //                    {
                    //                        case 230:
                    //                            dtl.tbd_weight = item.OBS_Value;
                    //                            break;
                    //                        case 231:
                    //                            dtl.tbd_height = item.OBS_Value;
                    //                            break;
                    //                        case 11:
                    //                            dtl.tbd_temp = item.OBS_Value;
                    //                            break;
                    //                        case 129:
                    //                            dtl.tbd_systolic = item.OBS_Value;
                    //                            break;
                    //                        case 128:
                    //                            dtl.tbd_diastolic = item.OBS_Value;
                    //                            break;
                    //                        case 9:
                    //                            dtl.tbd_pulse = item.OBS_Value;
                    //                            break;
                    //                        case 10:
                    //                            dtl.tbd_rr = item.OBS_Value;
                    //                            break;
                    //                        case 134:
                    //                            dtl.tbd_bmi = item.OBS_Value;
                    //                            break;
                    //                        case 176:
                    //                            dtl.tbd_waist = item.OBS_Value;
                    //                            break;
                    //                        case 173:
                    //                            dtl.tbd_vision_lt = item.OBS_Value;
                    //                            break;
                    //                        case 174:
                    //                            dtl.tbd_vision_rt = item.OBS_Value;
                    //                            break;
                    //                        // Sumit Edit 23/12/2014
                    //                        case 281:
                    //                            dtl.tbd_vision_with_lens = (item.OBS_Value == "Y" || item.OBS_Value == "y") ? true : false;
                    //                            break;
                    //                    }
                    //                }
                    //                dtl.tbd_update_by = username;
                    //                dtl.tbd_update_date = dateNow;
                    //            }
                    //            if (countRec == 5) break;
                    //        }

                    //        try
                    //        {
                    //            var eyesVS = hdr.trn_basic_measure_dtls
                    //                            .OrderByDescending(x => x.tbd_date)
                    //                            .Select(x => new
                    //                            {
                    //                                x.tbd_vision_rt,
                    //                                x.tbd_vision_lt
                    //                            }).FirstOrDefault();
                    //            hdr.tbm_vision_right = Convert.ToInt32(eyesVS.tbd_vision_rt);
                    //            hdr.tbm_vision_left = Convert.ToInt32(eyesVS.tbd_vision_lt);
                    //        }
                    //        catch
                    //        {

                    //        }
                    //        List<trn_basic_measure_dtl> listDeleteDtl = hdr.trn_basic_measure_dtls.Where(x => !listNewRowID.Contains(x.tbd_row_id)).ToList();
                    //        dbc.trn_basic_measure_dtls.DeleteAllOnSubmit(listDeleteDtl);
                    //        dbc.SubmitChanges();
                    //    }
                    //}
                }
            }
        }
        private int? ConvertToInt(string val)
        {
            int o;
            if (int.TryParse(val, out o))
            {
                return o;
            }
            else
            {
                return null;
            }
        }
        public void GetVistalSignBackground(int tpr_id)
        {
            System.Threading.Thread currentTimeThread = new System.Threading.Thread(delegate()
            {
                GetVistalSign(tpr_id, "WsVS");
            });
            currentTimeThread.IsBackground = true;
            currentTimeThread.Start();
        }

        private EnumerableRowCollection<DataRow> retrieveVitalSign(string hn)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    List<string> checkupCode = cdc.mst_hpc_sites.Where(x => x.mhs_status == 'A' && x.mhs_other_clinic == false).Select(x => x.mhs_code).ToList();
                    using (Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls())
                    {
                        var result = ws.GetVitalSignByHN(hn).AsEnumerable();
                        var a = result.Where(x => checkupCode.Contains(x.Field<string>("CTLOC_Code"))).ToList();
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<VitalSignResult> GetVitalSign(string hn)
        {
            List<VitalSignResult> results = new List<VitalSignResult>();
            try
            {
                using (Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls())
                {
                    var grpVS = ws.GetVitalSignByHN(hn).AsEnumerable()
                                  .GroupBy(x => new { PAADM_ADMNo = x.Field<string>("PAADM_ADMNo"), OBS_ParRef = x.Field<int>("OBS_ParRef"), OBS_Date = x.Field<DateTime?>("OBS_Date"), OBS_Time = x.Field<TimeSpan?>("OBS_Time"), CTLOC_Code = x.Field<string>("CTLOC_Code") })
                                  .ToList();

                    foreach (var vs in grpVS)
                    {
                        VitalSignResult rs = new VitalSignResult
                        {
                            en = vs.Key.PAADM_ADMNo,
                            rowid = vs.Key.OBS_ParRef,
                            Location = vs.Key.CTLOC_Code,
                            VitalSignDate = vs.Key.OBS_Date == null ? (DateTime?)null : vs.Key.OBS_Time == null ? vs.Key.OBS_Date.Value : vs.Key.OBS_Date.Value.Add(vs.Key.OBS_Time.Value)
                        };
                        foreach (var row in vs)
                        {
                            switch (row.Field<int>("OBS_Item_DR"))
                            {
                                case 230:
                                    rs.Weight = row.Field<string>("OBS_Value");
                                    break;
                                case 231:
                                    rs.Height = row.Field<string>("OBS_Value");
                                    break;
                                case 11:
                                    rs.Temperature = row.Field<string>("OBS_Value");
                                    break;
                                case 129:
                                    rs.Systolic = row.Field<string>("OBS_Value");
                                    break;
                                case 128:
                                    rs.Diastolic = row.Field<string>("OBS_Value");
                                    break;
                                case 9:
                                    rs.Pulse = row.Field<string>("OBS_Value");
                                    break;
                                case 10:
                                    rs.RespirationRate = row.Field<string>("OBS_Value");
                                    break;
                                case 134:
                                    rs.BMI = row.Field<string>("OBS_Value");
                                    break;
                                case 176:
                                    rs.Waist = row.Field<string>("OBS_Value");
                                    break;
                                case 173:
                                    rs.VisionLeft = row.Field<string>("OBS_Value");
                                    break;
                                case 174:
                                    rs.VisionRight = row.Field<string>("OBS_Value");
                                    break;
                                case 281:
                                    rs.WithLen = row.Field<string>("OBS_Value") == null ? false : row.Field<string>("OBS_Value").ToUpper() == "Y";
                                    break;
                                case 179:
                                    rs.ColorBlind = row.Field<string>("OBS_Value");
                                    break;
                            }
                        }
                        results.Add(rs);
                    }
                }
            }
            catch (Exception ex)
            {
                globalCls.MessageError("WS", "GetVitalSign(string hn, int maxtime)", ex.Message);
            }
            return results.OrderByDescending(x => x.VitalSignDate).ToList();
        }
        public List<VitalSignResult> GetVitalSignCheckup(string hn)
        {
            try
            {
                var res = GetVitalSign(hn);
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    var locCheckup = cdc.mst_hpc_sites.Where(x => x.mhs_status == 'A' && x.mhs_other_clinic == false).Select(x => x.mhs_code).ToList();
                    var result = res.Where(x => locCheckup.Contains(x.Location))
                                    .OrderByDescending(x => x.VitalSignDate)
                                    .Take(3).ToList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return new List<VitalSignResult>();
                globalCls.MessageError("WS", "GetVitalSignCheckup(string hn)", ex.Message);
            }
        }

        public void InsertBasicMeasure(int tpr_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    trn_patient_regi regis = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                    var res = GetVitalSignCheckup(regis.trn_patient.tpt_hn_no);

                }
            }
            catch (Exception ex)
            {
                globalCls.MessageError("WS", "InsertBasicMeasure()", ex.Message);
            }
        }
    }
}



public class VitalSignResult
{
    public int rowid { get; set; }
    public string en { get; set; }
    public string Location { get; set; }
    public string Weight { get; set; }
    public string Height { get; set; }
    public string Temperature { get; set; }
    public string Systolic { get; set; }
    public string Diastolic { get; set; }
    public string Pulse { get; set; }
    public string RespirationRate { get; set; }
    public string BMI { get; set; }
    public string Waist { get; set; }
    public string VisionLeft { get; set; }
    public string VisionRight { get; set; }
    public string ColorBlind { get; set; }
    public DateTime? VitalSignDate { get; set; }
    public bool WithLen { get; set; }
}