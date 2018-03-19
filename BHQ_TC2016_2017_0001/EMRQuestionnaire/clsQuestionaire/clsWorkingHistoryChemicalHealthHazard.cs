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
    public class clsWorkingHistoryChemicalHealthHazard :clsWorkingHistoryBiologicalHealthHazard
    {
        private char _NO_CHEMICAL_HEALTH_HAZARD;

        public char NO_CHEMICAL_HEALTH_HAZARD
        {
            get { return _NO_CHEMICAL_HEALTH_HAZARD; }
            set { _NO_CHEMICAL_HEALTH_HAZARD = value; }
        }
        private char _ORGANIC;

        public char ORGANIC
        {
            get { return _ORGANIC; }
            set { _ORGANIC = value; }
        }
        private char _GAS;

        public char GAS
        {
            get { return _GAS; }
            set { _GAS = value; }
        }
        private char _HEAVY_METAL;

        public char HEAVY_METAL
        {
            get { return _HEAVY_METAL; }
            set { _HEAVY_METAL = value; }
        }
        private char _ACID;

        public char ACID
        {
            get { return _ACID; }
            set { _ACID = value; }
        }
        private char _METAL_FUME;

        public char METAL_FUME
        {
            get { return _METAL_FUME; }
            set { _METAL_FUME = value; }
        }
        private char _HERBICIDE;

        public char HERBICIDE
        {
            get { return _HERBICIDE; }
            set { _HERBICIDE = value; }
        }
        private char _DUST;

        public char DUST
        {
            get { return _DUST; }
            set { _DUST = value; }
        }
        private char _METAL_POWDERS;

        public char METAL_POWDERS
        {
            get { return _METAL_POWDERS; }
            set { _METAL_POWDERS = value; }
        }
    }
}
