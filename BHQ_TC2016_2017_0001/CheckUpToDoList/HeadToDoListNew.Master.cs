using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DBToDoList;

namespace CheckUpToDoList
{
    public partial class HeadToDoListNew : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Constant.CurrentUserLogin!="")
                {
                    trMenuMain.Visible = true;
                    using (InhToDoListDataContext dbc = new InhToDoListDataContext())
                    {
                        var objpolicy = (from t1 in dbc.mst_user_logins
                                         where t1.mul_user_login == Constant.CurrentUserLogin
                                         select t1.mst_user_type.mut_code).FirstOrDefault();
                        if(objpolicy!=null)
                        {
                            string strlnk="";
                            //สำหรับ Admin 
                            if (Constant.CurrentUserLogin == "Admin") { objpolicy = "All"; }
                            Constant.CurrentPageLogin = objpolicy;
                            //string strspli = "";
                            switch (objpolicy){
                                case "MKT": strlnk = "<a href='frmmktNew.aspx'>Marketing</a>&nbsp;|&nbsp;<a href='frmMasterCompany.aspx'>Company Master</a>"; //Response.Redirect("frmmktNew.aspx");
                                break;
                                case "HPC": strlnk = "<a href='frm_hpc.aspx'>HPC</a>"; //Response.Redirect("frm_hpc.aspx");
                                break;
                                case "CLT": strlnk = "<a href='frm_collection.aspx'>Collection</a>"; //Response.Redirect("frm_collection.aspx");
                                break;
                                case "CTC": strlnk = "<a href='frm_contact_center.aspx'>Contact Center</a>"; //Response.Redirect("frm_contact_center.aspx");
                                break;
                                case "All": strlnk = "<a href='frmmktNew.aspx'>Marketing</a>"
                                                        + "&nbsp;|&nbsp; <a href='frmMasterCompany.aspx'>Company Master</a>"
                                                        + "&nbsp;|&nbsp; <a href='frm_hpc.aspx'>HPC</a>"
                                                        + "&nbsp;|&nbsp; <a href='frm_collection.aspx'>Collection</a>"
                                                        + "&nbsp;|&nbsp; <a href='frm_contact_center.aspx'>Contact Center</a>"
                                                        + "&nbsp;|&nbsp; <a href='BackOffice/AdminControlPage.aspx' target='blank'>Back Office</a>"; //AllRoom
                                break;
                            }
                            //strspli="&nbsp;|";

                            

                            lnkMenuMain.InnerHtml = strlnk;
                        }
                    }
                }
                else
                {
                    trMenuMain.Visible = false;
                    string sPagePath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
                    System.IO.FileInfo oFileInfo = new System.IO.FileInfo(sPagePath);
                    string sPageName = oFileInfo.Name;
                    if (sPageName.ToLower() != Constant.DefaultPageLogin)
                    {
                        Response.Redirect(Constant.DefaultPageLogin);
                    }
                }
            }

            /*
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/frmmktNew.aspx">Marketing</asp:HyperLink>
                &nbsp;|
                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/frm_hpc.aspx">HPC</asp:HyperLink>
                &nbsp;|
                <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/frm_collection.aspx">Collection</asp:HyperLink>
                &nbsp;|
                <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/frm_contact_center.aspx">Contact Center</asp:HyperLink>
                &nbsp;|
                <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/frmMasterCompany.aspx">Company Master</asp:HyperLink>

             */

        }
    }
}