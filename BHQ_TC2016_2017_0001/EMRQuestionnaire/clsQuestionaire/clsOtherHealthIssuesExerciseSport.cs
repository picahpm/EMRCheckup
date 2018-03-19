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
    public class clsOtherHealthIssuesExerciseSport : clsOtherHealthIssuesfavoritefood
    {
        private string _DDMMYY;

        public string DDMMYY
        {
            get { return _DDMMYY; }
            set { _DDMMYY = value; }
        }
        private string _INJURY_OR_ILLNESS;

        public string INJURY_OR_ILLNESS
        {
            get { return _INJURY_OR_ILLNESS; }
            set { _INJURY_OR_ILLNESS = value; }
        }
        private string _CAUSE_OF_INJURY;

        public string CAUSE_OF_INJURY
        {
            get { return _CAUSE_OF_INJURY; }
            set { _CAUSE_OF_INJURY = value; }
        }
        private string _DISABLED;

        public string DISABLED
        {
            get { return _DISABLED; }
            set { _DISABLED = value; }
        }
        private string _LOSS_OF_LIMBS;

        public string LOSS_OF_LIMBS
        {
            get { return _LOSS_OF_LIMBS; }
            set { _LOSS_OF_LIMBS = value; }
        }
        private string _LESS_THAN_THREE;

        public string LESS_THAN_THREE
        {
            get { return _LESS_THAN_THREE; }
            set { _LESS_THAN_THREE = value; }
        }
        private string _MORE_THAN_THREE;

        public string MORE_THAN_THREE
        {
            get { return _MORE_THAN_THREE; }
            set { _MORE_THAN_THREE = value; }
        }
    }
}
