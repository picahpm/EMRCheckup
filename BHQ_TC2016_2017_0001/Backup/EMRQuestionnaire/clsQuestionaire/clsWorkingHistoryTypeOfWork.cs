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
    public class clsWorkingHistoryTypeOfWork :clsMainQuestionaire
    {
        private char _OFFICE_WORK;

        public char OFFICE_WORK
        {
            get { return _OFFICE_WORK; }
            set { _OFFICE_WORK = value; }
        }
        private char _ONSHORE_WORK;

        public char ONSHORE_WORK
        {
            get { return _ONSHORE_WORK; }
            set { _ONSHORE_WORK = value; }
        }
        private char _OFFSHORE_WORK;

        public char OFFSHORE_WORK
        {
            get { return _OFFSHORE_WORK; }
            set { _OFFSHORE_WORK = value; }
        }
    }
}
