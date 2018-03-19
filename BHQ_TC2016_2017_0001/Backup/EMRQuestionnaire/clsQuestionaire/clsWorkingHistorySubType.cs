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
    public class clsWorkingHistorySubType :clsMainQuestionaire
    {

        private int _WORKING_HISTORY_SUB_TYPE;

        public int WORKING_HISTORY_SUB_TYPE
        {
            get { return _WORKING_HISTORY_SUB_TYPE; }
            set { _WORKING_HISTORY_SUB_TYPE = value; }
        }
        private string _PERIOD_DATE_FORM;

        public string PERIOD_DATE_FORM
        {
            get { return _PERIOD_DATE_FORM; }
            set { _PERIOD_DATE_FORM = value; }
        }
        private string _PERIOD_DATE_TO;

        public string PERIOD_DATE_TO
        {
            get { return _PERIOD_DATE_TO; }
            set { _PERIOD_DATE_TO = value; }
        }
      
    }
}
