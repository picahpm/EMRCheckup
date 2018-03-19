using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckupWebService.Class
{
    public class VitalSignUnitCls
    {
        public VitalSignUnitResult Get()
        {
            return new VitalSignUnitResult()
            {
                vitalsigns = new List<VitalSign>
                {
                    new VitalSign { code = "BP001", unitlangs = new UnitLang { en = "mmHg", th = "มิลลิเมตรปรอท" }},
                    new VitalSign { code = "PS001", unitlangs = new UnitLang { en = "beat / minute", th = "ครั้งต่อนาที" }},
                    new VitalSign { code = "RP001", unitlangs = new UnitLang { en = "beat / minute", th = "ครั้งต่อนาที" }},
                    new VitalSign { code = "TP001", unitlangs = new UnitLang { en = "°C", th = "องศาเซลเซียส" }}
                }
                //new VsUnitMapping { code = "BP001", units = new VsUnit { en = "mmHg", th = "มิลลิเมตรปรอท" }},
                //new VsUnitMapping { code = "PS001", units = new VsUnit { en = "beat / minute", th = "ครั้งต่อนาที" }},
                //new VsUnitMapping { code = "RP001", units = new VsUnit { en = "beat / minute", th = "ครั้งต่อนาที" }},
                //new VsUnitMapping { code = "TP001", units = new VsUnit { en = "°C", th = "องศาเซลเซียส" }}
            };
        }
    }

    public class VitalSignUnitResult
    {
        public List<VitalSign> vitalsigns { get; set; }
    }
    public class VitalSign
    {
        public string code { get; set; }
        public UnitLang unitlangs { get; set; }
    }
    public class UnitLang
    {
        public string th { get; set; }
        public string en { get; set; }
    }
}