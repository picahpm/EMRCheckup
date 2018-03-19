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
    public class clsWorkingHistoryPhysicalHealthHazard :clsWorkingHistorySpecialAssignment
    {
        private char _NO_PHYSICAL_HEALTH_HAZARD;

        public char NO_PHYSICAL_HEALTH_HAZARD
        {
            get { return _NO_PHYSICAL_HEALTH_HAZARD; }
            set { _NO_PHYSICAL_HEALTH_HAZARD = value; }
        }
        private char _LIGHT;

        public char LIGHT
        {
            get { return _LIGHT; }
            set { _LIGHT = value; }
        }
        private char _COLD;

        public char COLD
        {
            get { return _COLD; }
            set { _COLD = value; }
        }
        private char _NOISE;

        public char NOISE
        {
            get { return _NOISE; }
            set { _NOISE = value; }
        }
        private char _RADIATION;

        public char RADIATION
        {
            get { return _RADIATION; }
            set { _RADIATION = value; }
        }
        private char _HEAT;

        public char HEAT
        {
            get { return _HEAT; }
            set { _HEAT = value; }
        }
       
    }
}
