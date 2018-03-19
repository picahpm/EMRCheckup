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
    public class clsPersonalIllnessMedicationThatYouAreTaking : clsPersonalIllnessMedicationRegularly
    {
        private char _HEART_DISEASE_MEDICATION;

        public char HEART_DISEASE_MEDICATION
        {
            get { return _HEART_DISEASE_MEDICATION; }
            set { _HEART_DISEASE_MEDICATION = value; }
        }
        private char _HIGH_BLOOD_PRESSURE_MEDICATION;

        public char HIGH_BLOOD_PRESSURE_MEDICATION
        {
            get { return _HIGH_BLOOD_PRESSURE_MEDICATION; }
            set { _HIGH_BLOOD_PRESSURE_MEDICATION = value; }
        }
        private char _HIGH_BLOOD_LIPIDS_MEDICATION;

        public char HIGH_BLOOD_LIPIDS_MEDICATION
        {
            get { return _HIGH_BLOOD_LIPIDS_MEDICATION; }
            set { _HIGH_BLOOD_LIPIDS_MEDICATION = value; }
        }
        private char _BLOOD_THINNER;

        public char BLOOD_THINNER
        {
            get { return _BLOOD_THINNER; }
            set { _BLOOD_THINNER = value; }
        }
        private char _DIABETES;

        public char DIABETES
        {
            get { return _DIABETES; }
            set { _DIABETES = value; }
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
