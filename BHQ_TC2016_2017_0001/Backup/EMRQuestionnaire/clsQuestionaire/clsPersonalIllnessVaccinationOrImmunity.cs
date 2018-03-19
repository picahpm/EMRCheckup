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
    public class clsPersonalIllnessVaccinationOrImmunity : clsPersonalIllnessUnderlyingDeceases
    {
        private char _JE;

        public char JE
        {
            get { return _JE; }
            set { _JE = value; }
        }
        private char _CHICKENPOX;

        public char CHICKENPOX
        {
            get { return _CHICKENPOX; }
            set { _CHICKENPOX = value; }
        }
        private char _INFLUENZA;

        public char INFLUENZA
        {
            get { return _INFLUENZA; }
            set { _INFLUENZA = value; }
        }
        private char _HEPATITIS_A;

        public char HEPATITIS_A
        {
            get { return _HEPATITIS_A; }
            set { _HEPATITIS_A = value; }
        }
        private char _YELLOW_FEVER;

        public char YELLOW_FEVER
        {
            get { return _YELLOW_FEVER; }
            set { _YELLOW_FEVER = value; }
        }
        private char _MENING;

        public char MENING
        {
            get { return _MENING; }
            set { _MENING = value; }
        }
        private char _HEPATITIS_B;

        public char HEPATITIS_B
        {
            get { return _HEPATITIS_B; }
            set { _HEPATITIS_B = value; }
        }
        private char _TETANUS;

        public char TETANUS
        {
            get { return _TETANUS; }
            set { _TETANUS = value; }
        }
        private char _TYPHOID;

        public char TYPHOID
        {
            get { return _TYPHOID; }
            set { _TYPHOID = value; }
        }
    }
}
