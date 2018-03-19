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
    public class clsGeneralInformation : clsMainQuestionaire
    {
        private string _EMPLOYMENT_DATE;

        public string EMPLOYMENT_DATE
        {
            get { return _EMPLOYMENT_DATE; }
            set { _EMPLOYMENT_DATE = value; }
        }
        private string _EMPLOYER;

        public string EMPLOYER
        {
            get { return _EMPLOYER; }
            set { _EMPLOYER = value; }
        }
        private string _WORK_LOCATION;

        public string WORK_LOCATION
        {
            get { return _WORK_LOCATION; }
            set { _WORK_LOCATION = value; }
        }
        private string _EMAIL_ADDRESS;

        public string EMAIL_ADDRESS
        {
            get { return _EMAIL_ADDRESS; }
            set { _EMAIL_ADDRESS = value; }
        }
        private string _FUNCTIONAL_GROUP;

        public string FUNCTIONAL_GROUP
        {
            get { return _FUNCTIONAL_GROUP; }
            set { _FUNCTIONAL_GROUP = value; }
        }
        private string _CURRENT_POSITION;

        public string CURRENT_POSITION
        {
            get { return _CURRENT_POSITION; }
            set { _CURRENT_POSITION = value; }
        }
        private string _TELEPHONE;

        public string TELEPHONE
        {
            get { return _TELEPHONE; }
            set { _TELEPHONE = value; }
        }
    }
}
