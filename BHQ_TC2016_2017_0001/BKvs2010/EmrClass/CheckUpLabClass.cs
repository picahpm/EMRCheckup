using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DBCheckup;

namespace BKvs2010.EmrClass
{
    public class CheckUpLabClass
    {
        #region Transfer data from WS_GetCheckUpLab run BackgroundWorker

        private static int _tpr_id { get; set; }
        public static void retrieveLabToPatientLab(List<string> LabCode, int tpr_id)
        {
            try
            {
                using (InhCheckupDataContext cdt = new InhCheckupDataContext())
                {
                    trn_patient_regi tpr = cdt.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                    using (Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls())
                    {
                        // get lab code ที่ไม่มีใน trn_patient_lab check by en
                        var _labCode = LabCode.Where(x => !(tpr.trn_patient_labs.Where(y => y.tpl_en_no == tpr.tpr_en_no
                                                                                            && LabCode.Contains(y.tpl_lab_no))
                                                            .Select(y => y.tpl_lab_no)).Contains(x));
                        //

                        // get จาก web service แล้ว insert to trn_patient_lab ตรงๆ ไม่ผ่าน tmp
                        // insert เฉพาะ lab ที่ต้องการและ ยังไม่เคย insert to trn_patient_lab
                        DateTime date = Program.GetServerDateTime();
                        var result = ws.GetCheckupLab(tpr.trn_patient.tpt_hn_no, date.AddMonths(-1).ToString("yyyy-MM-dd"), date.ToString("yyyy-MM-dd")).AsEnumerable();
                        List<trn_patient_lab> tpl = result.Where(x => x.Field<string>("EPI_NO") == tpr.tpr_en_no
                                                    && _labCode.Contains(x.Field<string>("CTTC_CDE")))
                                                    .Select(x => new trn_patient_lab
                                                    {
                                                        tpr_id = tpr_id,
                                                        tpl_rowid = x.Field<string>("EPVISVisitNumber"),
                                                        tpl_hn_no = x.Field<string>("DBT_CDE"),
                                                        tpl_en_no = x.Field<string>("EPI_NO"),
                                                        tpl_patient_age = Convert.ToInt32(x.Field<string>("EPVIS_AGE")),
                                                        tpl_patient_sex = x.Field<string>("EPVIS_SEX") == "Male" ? "M" : x.Field<string>("EPVIS_SEX") == "Female" ? "F" : "",
                                                        tpl_lab_date = new DateTime(
                                                                       Convert.ToInt32(x.Field<string>("EPIVISDateOfCollection").Substring(0, 4)),
                                                                       Convert.ToInt32(x.Field<string>("EPIVISDateOfCollection").Substring(5, 2)),
                                                                       Convert.ToInt32(x.Field<string>("EPIVISDateOfCollection").Substring(8, 2)),
                                                                       (Convert.ToInt32(x.Field<string>("EPIVISTimeOfCollection")) / 60),
                                                                       (Convert.ToInt32(x.Field<string>("EPIVISTimeOfCollection")) % 60),
                                                                       0
                                                                       ),
                                                        tpl_head_lab_no = x.Field<string>("CTTS_CDE"),
                                                        tpl_head_lab = x.Field<string>("CTTS_NME"),
                                                        tpl_lab_no = x.Field<string>("CTTC_CDE"),
                                                        tpl_lab_name = x.Field<string>("CTTC_DES"),
                                                        tpl_lab_value = x.Field<string>("TST_DTA"),
                                                        tpl_lab_unit = x.Field<string>("CTTC_UNT"),
                                                        tpl_lab_range = x.Field<string>("NML"),
                                                        tpl_lab_rsl = x.Field<string>("RSL"),
                                                        tpl_lab_hl = x.Field<string>("HL") == "H" ? 1 : x.Field<string>("HL") == "L" ? -1 : 0,
                                                        tpl_lab_frs = x.Field<string>("CTTC_RSL_FMT") == "N" ? 1 : x.Field<string>("CTTC_RSL_FMT") == "S" ? 2 : -1,
                                                        tpl_status = x.Field<string>("Status") == "" ? 'E' : 'I',
                                                        tpl_mhs_id = cdt.mst_hpc_sites.Where(y => y.mhs_code == x.Field<string>("CTHOS_CDE")).Select(y => y.mhs_id).FirstOrDefault(),
                                                        tpl_mhs_tname = cdt.mst_hpc_sites.Where(y => y.mhs_code == x.Field<string>("CTHOS_CDE")).Select(y => y.mhs_tname).FirstOrDefault(),
                                                        tpl_mhs_ename = cdt.mst_hpc_sites.Where(y => y.mhs_code == x.Field<string>("CTHOS_CDE")).Select(y => y.mhs_ename).FirstOrDefault(),
                                                        tpl_create_date = Program.GetServerDateTime(),
                                                        tpl_update_date = Program.GetServerDateTime(),
                                                        tpl_lab_result_seq = x.Field<int>("sequen")
                                                    }).ToList();
                        
                        if (tpl.Count > 0)
                        {
                            cdt.trn_patient_labs.InsertAllOnSubmit(tpl);
                            cdt.SubmitChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}