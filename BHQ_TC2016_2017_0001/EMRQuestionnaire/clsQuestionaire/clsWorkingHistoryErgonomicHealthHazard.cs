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
    public class clsWorkingHistoryErgonomicHealthHazard : clsWorkingHistoryPsychologicalHealthHazard
    {
        private char _NO_ERGONOMIC_HEALTH_HAZARD;

        public char NO_ERGONOMIC_HEALTH_HAZARD
        {
            get { return _NO_ERGONOMIC_HEALTH_HAZARD; }
            set { _NO_ERGONOMIC_HEALTH_HAZARD = value; }
        }
        private char _POOR_POSTURE;

        public char POOR_POSTURE
        {
            get { return _POOR_POSTURE; }
            set { _POOR_POSTURE = value; }
        }
        private char _INAPPROPRIATE;

        public char INAPPROPRIATE
        {
            get { return _INAPPROPRIATE; }
            set { _INAPPROPRIATE = value; }
        }
        private char _REPEATING;

        public char REPEATING
        {
            get { return _REPEATING; }
            set { _REPEATING = value; }
        }
    }
}
