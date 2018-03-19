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
    public class clsWorkingHistoryPPE : clsPersonalIllnessDoYouSmoke
    {

        private char _EARPLUG_EARMUFF;

        public char EARPLUG_EARMUFF
        {
            get { return _EARPLUG_EARMUFF; }
            set { _EARPLUG_EARMUFF = value; }
        }
        private char _SAFETY_GLASSES;

        public char SAFETY_GLASSES
        {
            get { return _SAFETY_GLASSES; }
            set { _SAFETY_GLASSES = value; }
        }
        private char _HELMET;

        public char HELMET
        {
            get { return _HELMET; }
            set { _HELMET = value; }
        }
        private char _SAFETY_SHOES;

        public char SAFETY_SHOES
        {
            get { return _SAFETY_SHOES; }
            set { _SAFETY_SHOES = value; }
        }
        private char _GLOVES;

        public char GLOVES
        {
            get { return _GLOVES; }
            set { _GLOVES = value; }
        }
        private char _COVERALLS;

        public char COVERALLS
        {
            get { return _COVERALLS; }
            set { _COVERALLS = value; }
        }
        
    }
}
