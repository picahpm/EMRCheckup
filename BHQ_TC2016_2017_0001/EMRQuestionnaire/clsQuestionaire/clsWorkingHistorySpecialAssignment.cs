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
    public class clsWorkingHistorySpecialAssignment :clsMainQuestionaire
    {

        private char _FIRE_FIGHTING_STAFF;

        public char FIRE_FIGHTING_STAFF
        {
            get { return _FIRE_FIGHTING_STAFF; }
            set { _FIRE_FIGHTING_STAFF = value; }
        }
        private char _CONFINED_SPACE_WORKER;

        public char CONFINED_SPACE_WORKER
        {
            get { return _CONFINED_SPACE_WORKER; }
            set { _CONFINED_SPACE_WORKER = value; }
        }
        private char _PROFESSIONAL_DRIVER;

        public char PROFESSIONAL_DRIVER
        {
            get { return _PROFESSIONAL_DRIVER; }
            set { _PROFESSIONAL_DRIVER = value; }
        }
        private char _LABORATORY_TECHNICIAN;

        public char LABORATORY_TECHNICIAN
        {
            get { return _LABORATORY_TECHNICIAN; }
            set { _LABORATORY_TECHNICIAN = value; }
        }
        private char _CRANE_OPERATOR;

        public char CRANE_OPERATOR
        {
            get { return _CRANE_OPERATOR; }
            set { _CRANE_OPERATOR = value; }
        }
        private char _PAINTER;

        public char PAINTER
        {
            get { return _PAINTER; }
            set { _PAINTER = value; }
        }
        private char _CATERING_AND_FOOD;

        public char CATERING_AND_FOOD
        {
            get { return _CATERING_AND_FOOD; }
            set { _CATERING_AND_FOOD = value; }
        }
        private char _OTHERS;

        public char OTHERS
        {
            get { return _OTHERS; }
            set { _OTHERS = value; }
        }
        private string _OTHERS_DETAILS;

        public string OTHERS_DETAILS
        {
            get { return _OTHERS_DETAILS; }
            set { _OTHERS_DETAILS = value; }
        }

    }
}
