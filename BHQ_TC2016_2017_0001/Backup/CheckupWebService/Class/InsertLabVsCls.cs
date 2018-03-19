using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBCheckup;

namespace CheckupWebService.Class
{
    public class InsertLabVsCls
    {
        List<APITrakcare.VitalSignResult> vs3time;
        public InsertLabVsCls(List<APITrakcare.VitalSignResult> vs, string en)
        {
            vsunits = new List<VsUnitMapping>
            {
                new VsUnitMapping { code = "BP001", units = new VsUnit { en = "mmHg", th = "มิลลิเมตรปรอท" }},
                new VsUnitMapping { code = "PS001", units = new VsUnit { en = "beat / minute", th = "ครั้งต่อนาที" }},
                new VsUnitMapping { code = "RP001", units = new VsUnit { en = "beat / minute", th = "ครั้งต่อนาที" }},
                new VsUnitMapping { code = "TP001", units = new VsUnit { en = "°C", th = "องศาเซลเซียส" }}
            };
            vs3time = new List<APITrakcare.VitalSignResult>();
            bool start = false;
            int count = 0;
            foreach (var v in vs.OrderByDescending(x => x.vsDate))
            {
                if (v.en == en)
                {
                    start = true;
                }
                if (start)
                {
                    vs3time.Add(v);
                    count++;
                }
                if (count == 3)
                {
                    break;
                }
            }
        }

        public void Insert(ref trn_patient_regi pregis, LabClass.InterpretLab lab, DateTime dateNow, string user)
        {
            var labvs = pregis.trn_patient_lab_vitalsigns.Where(x => x.tplv_lab_code == lab.code).FirstOrDefault();
            if (labvs == null)
            {
                labvs = new trn_patient_lab_vitalsign
                {
                    tplv_lab_code = lab.code,
                    tplv_create_by = user,
                    tplv_create_date = dateNow
                };
                pregis.trn_patient_lab_vitalsigns.Add(labvs);
            }
            switch (lab.code)
            {
                case "BP001":
                    labvs.tplv_result_1 = vs3time.Count() == 0 ? null : (string.IsNullOrEmpty(vs3time[0].systolic) ? "" : vs3time[0].systolic) + "/" +
                                                                        (string.IsNullOrEmpty(vs3time[0].diastolic) ? "" : vs3time[0].diastolic);
                    labvs.tplv_result_2 = vs3time.Count() <= 1 ? null : (string.IsNullOrEmpty(vs3time[1].systolic) ? "" : vs3time[1].systolic) + "/" +
                                                                        (string.IsNullOrEmpty(vs3time[1].diastolic) ? "" : vs3time[1].diastolic);
                    labvs.tplv_result_3 = vs3time.Count() <= 2 ? null : (string.IsNullOrEmpty(vs3time[2].systolic) ? "" : vs3time[2].systolic) + "/" +
                                                                        (string.IsNullOrEmpty(vs3time[2].diastolic) ? "" : vs3time[2].diastolic);
                    break;
                case "PS001":
                    labvs.tplv_lab_unit_th = "ครั้งต่อนาที";
                    labvs.tplv_lab_unit_en = "beat / minute";
                    labvs.tplv_result_1 = vs3time.Count() == 0 ? null : vs3time[0].pulse;
                    labvs.tplv_result_2 = vs3time.Count() <= 1 ? null : vs3time[1].pulse;
                    labvs.tplv_result_3 = vs3time.Count() <= 2 ? null : vs3time[2].pulse;
                    break;
                case "RP001":
                    labvs.tplv_lab_unit_th = "ครั้งต่อนาที";
                    labvs.tplv_lab_unit_en = "beat / minute";
                    labvs.tplv_result_1 = vs3time.Count() == 0 ? null : vs3time[0].respirationrate;
                    labvs.tplv_result_2 = vs3time.Count() <= 1 ? null : vs3time[1].respirationrate;
                    labvs.tplv_result_3 = vs3time.Count() <= 2 ? null : vs3time[2].respirationrate;
                    break;
                case "TP001":
                    labvs.tplv_lab_unit_th = "องศาเซลเซียส";
                    labvs.tplv_lab_unit_en = "°C";
                    labvs.tplv_result_1 = vs3time.Count() == 0 ? null : vs3time[0].temperature;
                    labvs.tplv_result_2 = vs3time.Count() <= 1 ? null : vs3time[1].temperature;
                    labvs.tplv_result_3 = vs3time.Count() <= 2 ? null : vs3time[2].temperature;
                    break;
            }
            var unit = vsunits.Where(x => x.code == lab.code).FirstOrDefault();
            if (unit != null)
            {
                labvs.tplv_lab_unit_th = unit.units.th;
                labvs.tplv_lab_unit_en = unit.units.en;
            }
            labvs.tplv_summary = lab.summary == null ? null : lab.summary.ToString();
            labvs.tplv_lab_name_th = lab.name_th;
            labvs.tplv_lab_name_en = lab.name_en;
            labvs.tplv_lab_result_th = lab.result_th;
            labvs.tplv_lab_result_en = lab.result_en;
            labvs.tplv_normal_range = lab.normalrange;
            labvs.tplv_update_by = user;
            labvs.tplv_update_date = dateNow;
        }

        private List<VsUnitMapping> vsunits;
        private class VsUnitMapping
        {
            public string code { get; set; }
            public VsUnit units { get; set; }
        }
        private class VsUnit
        {
            public string th { get; set; }
            public string en { get; set; }
        }
    }
}