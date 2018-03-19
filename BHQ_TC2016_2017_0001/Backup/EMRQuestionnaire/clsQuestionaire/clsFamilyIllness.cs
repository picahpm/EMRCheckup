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
    public class clsFamilyIllness : clsPersonalIllnessDoYouSmoke
    {
        private char _FAMILY_ID;

        public char FAMILY_ID
        {
            get { return _FAMILY_ID; }
            set { _FAMILY_ID = value; }
        }
        private char _NONE;

        public char NONE
        {
            get { return _NONE; }
            set { _NONE = value; }
        }
        private char _ANEMIA_FAMILY;

        public char ANEMIA_FAMILY
        {
            get { return _ANEMIA_FAMILY; }
            set { _ANEMIA_FAMILY = value; }
        }
        private char _CANCER_FAMILY;

        public char CANCER_FAMILY
        {
            get { return _CANCER_FAMILY; }
            set { _CANCER_FAMILY = value; }
        }
        private char _DIABETES_FAMILY;

        public char DIABETES_FAMILY
        {
            get { return _DIABETES_FAMILY; }
            set { _DIABETES_FAMILY = value; }
        }
        private char _ASTHMA_FAMILY;

        public char ASTHMA_FAMILY
        {
            get { return _ASTHMA_FAMILY; }
            set { _ASTHMA_FAMILY = value; }
        }
        private char _HIGH_BLOOD_PRESSURE_FAMILY;

        public char HIGH_BLOOD_PRESSURE_FAMILY
        {
            get { return _HIGH_BLOOD_PRESSURE_FAMILY; }
            set { _HIGH_BLOOD_PRESSURE_FAMILY = value; }
        }
        private char _ALLERGY_FAMILY;

        public char ALLERGY_FAMILY
        {
            get { return _ALLERGY_FAMILY; }
            set { _ALLERGY_FAMILY = value; }
        }
        private char _CARDIOVASCULAR_FAMILY;

        public char CARDIOVASCULAR_FAMILY
        {
            get { return _CARDIOVASCULAR_FAMILY; }
            set { _CARDIOVASCULAR_FAMILY = value; }
        }
        private char _TUBERCULOSIS_FAMILY;

        public char TUBERCULOSIS_FAMILY
        {
            get { return _TUBERCULOSIS_FAMILY; }
            set { _TUBERCULOSIS_FAMILY = value; }
        }
    }
}
