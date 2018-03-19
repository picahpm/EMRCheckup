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
    public class clsPersonalIllnessillnessImpairment :clsPersonalIllnessMedicineOrFood
    {
        private string _DETAILS_ONE;

        public string DETAILS_ONE
        {
            get { return _DETAILS_ONE; }
            set { _DETAILS_ONE = value; }
        }
        private string _DETAILS_TWO;

        public string DETAILS_TWO
        {
            get { return _DETAILS_TWO; }
            set { _DETAILS_TWO = value; }
        }
        private string _YEAR_ONE;

        public string YEAR_ONE
        {
            get { return _YEAR_ONE; }
            set { _YEAR_ONE = value; }
        }
        private string _YEAR_TWO;

        public string YEAR_TWO
        {
            get { return _YEAR_TWO; }
            set { _YEAR_TWO = value; }
        }
    }
}
