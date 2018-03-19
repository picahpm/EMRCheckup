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
    public class clsWorkingHistoryClassificationOfEmployment :clsMainQuestionaire
    {

        private char _OIL_NATURAL_GAS;

        public char OIL_NATURAL_GAS
        {
            get { return _OIL_NATURAL_GAS; }
            set { _OIL_NATURAL_GAS = value; }
        }

        private char _NON_METAL_PRODUCTS;

        public char NON_METAL_PRODUCTS
        {
            get { return _NON_METAL_PRODUCTS; }
            set { _NON_METAL_PRODUCTS = value; }
        }

        private char _MANUFACTURE_OF_FOOD;

        public char MANUFACTURE_OF_FOOD
        {
            get { return _MANUFACTURE_OF_FOOD; }
            set { _MANUFACTURE_OF_FOOD = value; }
        }


        private char _MANUFACTURE_OF_BASIC_METALS;

        public char MANUFACTURE_OF_BASIC_METALS
        {
            get { return _MANUFACTURE_OF_BASIC_METALS; }
            set { _MANUFACTURE_OF_BASIC_METALS = value; }
        }


        private char _MANUFACTURE_OF_TEXTILES;

        public char MANUFACTURE_OF_TEXTILES
        {
            get { return _MANUFACTURE_OF_TEXTILES; }
            set { _MANUFACTURE_OF_TEXTILES = value; }
        }


        private char _METALS_PRODUCTS;

        public char METALS_PRODUCTS
        {
            get { return _METALS_PRODUCTS; }
            set { _METALS_PRODUCTS = value; }
        }


        private char _FORESTRY_AND_LOGGING;

        public char FORESTRY_AND_LOGGING
        {
            get { return _FORESTRY_AND_LOGGING; }
            set { _FORESTRY_AND_LOGGING = value; }
        }


        private char _MANUFACTURE_OF_MOTOR;

        public char MANUFACTURE_OF_MOTOR
        {
            get { return _MANUFACTURE_OF_MOTOR; }
            set { _MANUFACTURE_OF_MOTOR = value; }
        }


        private char _MANUFACTURE_OF_PAPER;

        public char MANUFACTURE_OF_PAPER
        {
            get { return _MANUFACTURE_OF_PAPER; }
            set { _MANUFACTURE_OF_PAPER = value; }
        }


        private char _PUBLIC_UTILITY;

        public char PUBLIC_UTILITY
        {
            get { return _PUBLIC_UTILITY; }
            set { _PUBLIC_UTILITY = value; }
        }


        private char _MANUFACTURE_OF_CHEMICAL;

        public char MANUFACTURE_OF_CHEMICAL
        {
            get { return _MANUFACTURE_OF_CHEMICAL; }
            set { _MANUFACTURE_OF_CHEMICAL = value; }
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
