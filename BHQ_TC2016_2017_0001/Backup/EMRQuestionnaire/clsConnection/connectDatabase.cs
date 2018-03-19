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
using DBCheckup;

namespace QuestionnaireWebSite.clsConnection
{
    public static class connectDatabase
    {
        public static string QuestionaireConnectionString
        {
            get
            {
                using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                {
                    return dbc.mst_project_configs.Where(x => x.mpc_code == "ConnStrQuesOccMed").Select(x => x.mpc_value).FirstOrDefault();
                }
            }
        }
        public static string EMRQConnectionString
        {
            get
            {
                using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                {
                    return dbc.mst_project_configs.Where(x => x.mpc_code == "ConnStrQuesHealthHis").Select(x => x.mpc_value).FirstOrDefault();
                }
            }
        }
    }
}
