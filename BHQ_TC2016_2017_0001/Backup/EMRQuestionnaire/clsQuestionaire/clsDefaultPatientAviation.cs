using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace EMRQuestionnaire.clsQuestionaire
{
    public class clsDefaultPatientAviation
    {
        public string tpt_hn_no { get; set; }
        public int tpr_id { get; set; }
        public int tpt_id { get; set; }
        public string tpt_nation_code { get; set; }
        public string tpt_nation_desc { get; set; }
        public string tpt_dob { get; set; }
        public char? tpt_gender { get; set; }        
        public char? tpt_married { get; set; }
        public string tpt_fullname { get; set; }        
    }
}
