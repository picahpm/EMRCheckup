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
    public class clsWorkingHistoryPsychologicalHealthHazard : clsWorkingHistoryChemicalHealthHazard
    {
        private char _NO;

        public char NO
        {
            get { return _NO; }
            set { _NO = value; }
        }
        private char _YES;

        public char YES
        {
            get { return _YES; }
            set { _YES = value; }
        }
    }
}
