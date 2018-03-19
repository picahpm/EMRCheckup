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
    public class clsMainQuestionaire
    {
        private string _HN;

        public string HN
        {
            get { return _HN; }
            set { _HN = value; }
        }
        private string _KEY_GEN;

        public string KEY_GEN
        {
            get { return _KEY_GEN; }
            set { _KEY_GEN = value; }
        }
        private string _CREATE_DATE;

        public string CREATE_DATE
        {
            get { return _CREATE_DATE; }
            set { _CREATE_DATE = value; }
        }
        private string _UPDATE_DATE;

        public string UPDATE_DATE
        {
            get { return _UPDATE_DATE; }
            set { _UPDATE_DATE = value; }
        }
        private string _CREATE_BY;

        public string CREATE_BY
        {
            get { return _CREATE_BY; }
            set { _CREATE_BY = value; }
        }
        private string _UPDATE_BY;

        public string UPDATE_BY
        {
            get { return _UPDATE_BY; }
            set { _UPDATE_BY = value; }
        }
        private string _LANGUAGE;

        public string LANGUAGE
        {
            get { return _LANGUAGE; }
            set { _LANGUAGE = value; }
        }
        private char _DRAFT;

        public char DRAFT
        {
            get { return _DRAFT; }
            set { _DRAFT = value; }
        }
    }
}
