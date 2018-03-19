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
    public class clsWorkingHistory :clsMainQuestionaire
    {
        private char _WORKING_HISTORY_PAST_AND_FUTURE;

        public char WORKING_HISTORY_PAST_AND_FUTURE
        {
            get { return _WORKING_HISTORY_PAST_AND_FUTURE; }
            set { _WORKING_HISTORY_PAST_AND_FUTURE = value; }
        }
        private char _WORKING_HISTORY_TYPE;

        public char WORKING_HISTORY_TYPE
        {
            get { return _WORKING_HISTORY_TYPE; }
            set { _WORKING_HISTORY_TYPE = value; }
        }
        private int _WORKING_HISTORY_SUB_TYPE;

        public int WORKING_HISTORY_SUB_TYPE
        {
            get { return _WORKING_HISTORY_SUB_TYPE; }
            set { _WORKING_HISTORY_SUB_TYPE = value; }
        }
        private string _EMPLOYER_DEPARTMENT_ONE;

        public string EMPLOYER_DEPARTMENT_ONE
        {
            get { return _EMPLOYER_DEPARTMENT_ONE; }
            set { _EMPLOYER_DEPARTMENT_ONE = value; }
        }
        private string _EMPLOYER_DEPARTMENT_TWO;

        public string EMPLOYER_DEPARTMENT_TWO
        {
            get { return _EMPLOYER_DEPARTMENT_TWO; }
            set { _EMPLOYER_DEPARTMENT_TWO = value; }
        }
        private string _EMPLOYER_DEPARTMENT_THREE;

        public string EMPLOYER_DEPARTMENT_THREE
        {
            get { return _EMPLOYER_DEPARTMENT_THREE; }
            set { _EMPLOYER_DEPARTMENT_THREE = value; }
        }
        private string _EMPLOYER_DEPARTMENT_FOUR;

        public string EMPLOYER_DEPARTMENT_FOUR
        {
            get { return _EMPLOYER_DEPARTMENT_FOUR; }
            set { _EMPLOYER_DEPARTMENT_FOUR = value; }
        }
        private string _EMPLOYER_DEPARTMENT_FIVE;

        public string EMPLOYER_DEPARTMENT_FIVE
        {
            get { return _EMPLOYER_DEPARTMENT_FIVE; }
            set { _EMPLOYER_DEPARTMENT_FIVE = value; }
        }
        //*/******************************************//

        private string _WORKING_HISTORY_CURRENT_EMPLOYER_DEPARTMENT;

        public string WORKING_HISTORY_CURRENT_EMPLOYER_DEPARTMENT
        {
            get { return _WORKING_HISTORY_CURRENT_EMPLOYER_DEPARTMENT; }
            set { _WORKING_HISTORY_CURRENT_EMPLOYER_DEPARTMENT = value; }
        }

        private string _WORKING_HISTORY_CLASSIFICATION_OF_EMPLOYMENT;

        public string WORKING_HISTORY_CLASSIFICATION_OF_EMPLOYMENT
        {
            get { return _WORKING_HISTORY_CLASSIFICATION_OF_EMPLOYMENT; }
            set { _WORKING_HISTORY_CLASSIFICATION_OF_EMPLOYMENT = value; }
        }

        private string _WORKING_HISTORY_TYPE_OF_WORK;

        public string WORKING_HISTORY_TYPE_OF_WORK
        {
            get { return _WORKING_HISTORY_TYPE_OF_WORK; }
            set { _WORKING_HISTORY_TYPE_OF_WORK = value; }
        }


        private string _WORKING_HISTORY_PERIOD_DATE_FROM;

        public string WORKING_HISTORY_PERIOD_DATE_FROM
        {
            get { return _WORKING_HISTORY_PERIOD_DATE_FROM; }
            set { _WORKING_HISTORY_PERIOD_DATE_FROM = value; }
        }

        private string _WORKING_HISTORY_PERIOD_DATE_TO;

        public string WORKING_HISTORY_PERIOD_DATE_TO
        {
            get { return _WORKING_HISTORY_PERIOD_DATE_TO; }
            set { _WORKING_HISTORY_PERIOD_DATE_TO = value; }
        }

        private string _WORKING_HISTORYWORK_RELATED_HEALTH_HAZARDS;

        public string WORKING_HISTORYWORK_RELATED_HEALTH_HAZARDS
        {
            get { return _WORKING_HISTORYWORK_RELATED_HEALTH_HAZARDS; }
            set { _WORKING_HISTORYWORK_RELATED_HEALTH_HAZARDS = value; }
        }



        private string _WORKING_HISTORYWORK_PPE;

        public string WORKING_HISTORYWORK_PPE
        {
            get { return _WORKING_HISTORYWORK_PPE; }
            set { _WORKING_HISTORYWORK_PPE = value; }
        }
    }
}
