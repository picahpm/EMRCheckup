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
    public class clsPersonalIllnessUnderlyingDeceases :clsPersonalIllnessHadAnOperation
    {
        private char _SLE;

        public char SLE
        {
            get { return _SLE; }
            set { _SLE = value; }
        }
        private char _CANCER;

        public char CANCER
        {
            get { return _CANCER; }
            set { _CANCER = value; }
        }
        private char _DIABETES;

        public char DIABETES1
        {
            get { return _DIABETES; }
            set { _DIABETES = value; }
        }
        private char _ASTHMA;

        public char ASTHMA
        {
            get { return _ASTHMA; }
            set { _ASTHMA = value; }
        }
        private char _PEPTIC_ULCER;

        public char PEPTIC_ULCER
        {
            get { return _PEPTIC_ULCER; }
            set { _PEPTIC_ULCER = value; }
        }
        private char _EPILEPSY;

        public char EPILEPSY
        {
            get { return _EPILEPSY; }
            set { _EPILEPSY = value; }
        }
        private char _HIGH_BLOOD_PRESSURE;

        public char HIGH_BLOOD_PRESSURE
        {
            get { return _HIGH_BLOOD_PRESSURE; }
            set { _HIGH_BLOOD_PRESSURE = value; }
        }
        private char _CHRONIC_OBSTRUCTIVE;

        public char CHRONIC_OBSTRUCTIVE
        {
            get { return _CHRONIC_OBSTRUCTIVE; }
            set { _CHRONIC_OBSTRUCTIVE = value; }
        }
        private char _ANEMIA;

        public char ANEMIA
        {
            get { return _ANEMIA; }
            set { _ANEMIA = value; }
        }
        private char _LUNG_EMPHYSEMA;

        public char LUNG_EMPHYSEMA
        {
            get { return _LUNG_EMPHYSEMA; }
            set { _LUNG_EMPHYSEMA = value; }
        }
        private char _CARDIOVASCULAR;

        public char CARDIOVASCULAR
        {
            get { return _CARDIOVASCULAR; }
            set { _CARDIOVASCULAR = value; }
        }
        private char _KIDNEY_DISEASE;

        public char KIDNEY_DISEASE
        {
            get { return _KIDNEY_DISEASE; }
            set { _KIDNEY_DISEASE = value; }
        }
        private char _HEPATITIS;

        public char HEPATITIS
        {
            get { return _HEPATITIS; }
            set { _HEPATITIS = value; }
        }
        private char _HIGH_BLOOD_LIPIDS;

        public char HIGH_BLOOD_LIPIDS
        {
            get { return _HIGH_BLOOD_LIPIDS; }
            set { _HIGH_BLOOD_LIPIDS = value; }
        }
    }
}
