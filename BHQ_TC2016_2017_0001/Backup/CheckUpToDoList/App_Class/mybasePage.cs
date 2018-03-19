using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
namespace CheckUpToDoList
{
    public class mybasePage:System.Web.UI.Page
    {
        public mybasePage()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        static string cultureName;

        public static string CultureName
        {
            get { return cultureName; }
            set { cultureName = value; }
        }

        protected override void InitializeCulture()
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture(cultureName);
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(cultureName);
       
            base.InitializeCulture();
        }

        /*
         *  <asp:DropDownList ID="DropDownList1" runat="server">
            <asp:ListItem Text="English" Value="en-US"></asp:ListItem>
            <asp:ListItem Text="Marathi" Value="mr-IN"></asp:ListItem>
            <asp:ListItem Text="Gujarathi" Value="gu-IN"></asp:ListItem>
            </asp:DropDownList>
         * protected void Button1_Click(object sender, EventArgs e)
            {       
                mybasePage.CultureName = DropDownList1.SelectedItem.Value.ToString();
            }
        */
    }

    
}