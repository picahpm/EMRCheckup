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
    public class clsWorkingHistoryBiologicalHealthHazard : clsWorkingHistoryPhysicalHealthHazard
    {
        private char _NO_BIOLOGICAL_HEALTH_HAZARD;

        public char NO_BIOLOGICAL_HEALTH_HAZARD
        {
            get { return _NO_BIOLOGICAL_HEALTH_HAZARD; }
            set { _NO_BIOLOGICAL_HEALTH_HAZARD = value; }
        }
        private char _ANIMAL_CARRIERS;

        public char ANIMAL_CARRIERS
        {
            get { return _ANIMAL_CARRIERS; }
            set { _ANIMAL_CARRIERS = value; }
        }
        private char _BLOOD_OR_OTHER;

        public char BLOOD_OR_OTHER
        {
            get { return _BLOOD_OR_OTHER; }
            set { _BLOOD_OR_OTHER = value; }
        }
        private char _BACTERIA;

        public char BACTERIA
        {
            get { return _BACTERIA; }
            set { _BACTERIA = value; }
        }
        private char _FUNGUS;

        public char FUNGUS
        {
            get { return _FUNGUS; }
            set { _FUNGUS = value; }
        }
        private char _VIRUS;

        public char VIRUS
        {
            get { return _VIRUS; }
            set { _VIRUS = value; }
        }
    }
}
