using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BKvs2010.Models
{
    public class CorpMedicalRptModels
    {
        public bool book { get; set; }
        public bool offShore { get; set; }
        public bool summary { get; set; }
        public bool copyToCorp { get; set; }
        public bool cert5 { get; set; }
        public bool cert6 { get; set; }
        public bool other { get; set; }
        public string remarkOther { get; set; }
        public string rptMst { get; set; }
        public string mstTo { get; set; }
        public string rptCopy { get; set; }
        public string copryTo { get; set; }

        public void Clear()
        {
            book = false;
            offShore = false;
            summary = false;
            copyToCorp = false;
            cert5 = false;
            cert6 = false;
            other = false;
            remarkOther = "";
            rptMst = "";
            mstTo = "";
            rptCopy = "";
            copryTo = "";
        }
    }
}
