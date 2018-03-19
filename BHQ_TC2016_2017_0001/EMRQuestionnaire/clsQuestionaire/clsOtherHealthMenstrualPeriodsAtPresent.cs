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
    public class clsOtherHealthMenstrualPeriodsAtPresent :clsMainQuestionaire
    {
        private char _MENOPAUSE;

        public char MENOPAUSE
        {
            get { return _MENOPAUSE; }
            set { _MENOPAUSE = value; }
        }
        private char _YES;

        public char YES
        {
            get { return _YES; }
            set { _YES = value; }
        }
        private string _DATE_FORM;

        public string DATE_FORM
        {
            get { return _DATE_FORM; }
            set { _DATE_FORM = value; }
        }
        private string _DATE_TO;

        public string DATE_TO
        {
            get { return _DATE_TO; }
            set { _DATE_TO = value; }
        }
        private char _NORMAL;

        public char NORMAL
        {
            get { return _NORMAL; }
            set { _NORMAL = value; }
        }
        private char _ABNORMAL;

        public char ABNORMAL
        {
            get { return _ABNORMAL; }
            set { _ABNORMAL = value; }
        }
        private char _PRE_NO;

        public char PRE_NO
        {
            get { return _PRE_NO; }
            set { _PRE_NO = value; }
        }
        private char _PRE_PREGNANCY;

        public char PRE_PREGNANCY
        {
            get { return _PRE_PREGNANCY; }
            set { _PRE_PREGNANCY = value; }
        }
        private char _PRE_SUSPECTED;

        public char PRE_SUSPECTED
        {
            get { return _PRE_SUSPECTED; }
            set { _PRE_SUSPECTED = value; }
        }
        private char _HAS_YES;

        public char HAS_YES
        {
            get { return _HAS_YES; }
            set { _HAS_YES = value; }
        }
        private char _HAS_NO;

        public char HAS_NO
        {
            get { return _HAS_NO; }
            set { _HAS_NO = value; }
        }
        private char _HAS_NOT_SURE;

        public char HAS_NOT_SURE
        {
            get { return _HAS_NOT_SURE; }
            set { _HAS_NOT_SURE = value; }
        }
    }
}
