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
    public class clsPatientINFO : clsMainQuestionaire
    {

        private string _TITLE_NAME_TH;

        public string TITLE_NAME_TH
        {
            get { return _TITLE_NAME_TH; }
            set { _TITLE_NAME_TH = value; }
        }
        private string _TITLE_NAME_EN;

        public string TITLE_NAME_EN
        {
            get { return _TITLE_NAME_EN; }
            set { _TITLE_NAME_EN = value; }
        }
        private string _FIRST_NAME_TH;

        public string FIRST_NAME_TH
        {
            get { return _FIRST_NAME_TH; }
            set { _FIRST_NAME_TH = value; }
        }
        private string _FIRST_NAME_EN;

        public string FIRST_NAME_EN
        {
            get { return _FIRST_NAME_EN; }
            set { _FIRST_NAME_EN = value; }
        }
        private string _LAST_NAME_TH;

        public string LAST_NAME_TH
        {
            get { return _LAST_NAME_TH; }
            set { _LAST_NAME_TH = value; }
        }
        private string _LAST_NAME_EN;

        public string LAST_NAME_EN
        {
            get { return _LAST_NAME_EN; }
            set { _LAST_NAME_EN = value; }
        }
        private string _FULL_NAME;

        public string FULL_NAME
        {
            get { return _FULL_NAME; }
            set { _FULL_NAME = value; }
        }

        private string _FULL_NAME_EN;

        public string FULL_NAME_EN
        {
            get { return _FULL_NAME_EN; }
            set { _FULL_NAME_EN = value; }
        }

        private string _ROOM;

        public string ROOM
        {
            get { return _ROOM; }
            set { _ROOM = value; }
        }
        private string _PHYSICIAN;

        public string PHYSICIAN
        {
            get { return _PHYSICIAN; }
            set { _PHYSICIAN = value; }
        }
        private string _VISIT_DATE;

        public string VISIT_DATE
        {
            get { return _VISIT_DATE; }
            set { _VISIT_DATE = value; }
        }
        private string _DEPARTMENT;

        public string DEPARTMENT
        {
            get { return _DEPARTMENT; }
            set { _DEPARTMENT = value; }
        }
        private string _BIRTH_DATE;

        public string BIRTH_DATE
        {
            get { return _BIRTH_DATE; }
            set { _BIRTH_DATE = value; }
        }
        private string _AGE;

        public string AGE
        {
            get { return _AGE; }
            set { _AGE = value; }
        }
        private string _SEX;

        public string SEX
        {
            get { return _SEX; }
            set { _SEX = value; }
        }
        private string _ALLERGIES;

        public string ALLERGIES
        {
            get { return _ALLERGIES; }
            set { _ALLERGIES = value; }
        }




    }
}
