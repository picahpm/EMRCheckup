<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmCompanyMstSearch.aspx.cs" Inherits="CheckUpToDoList.frmCompanyMstSearch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        table .text-right
        {
	        text-align:right;
        }
        td{ padding-top:5px; padding-bottom:5px; text-align:left;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%" cellpadding="5" cellspacing="2" border="0">
            <tr>
                <td>
                    <asp:DropDownList ID="DropDownList5" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="txtSearch" runat="server" Width="100%" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp</td>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Search" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                Test Link To Refresh Frame 2
                <br /><a target="frm2" href="frmCompanyMstBody.aspx?data=yee1"> แสดง data 1</a>
                <br /><a target="frm2" href="frmCompanyMstBody.aspx?data=yee2"> แสดง data 2</a>


                <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:TemplateField HeaderText="No." HeaderStyle-Width="10%">
                            <ItemTemplate>
                                    
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <Columns>
                        <asp:TemplateField HeaderText="Company Name" HeaderStyle-Width="50%">
                            <ItemTemplate>
                                    
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <Columns>
                        <asp:TemplateField HeaderText="Start Date" HeaderStyle-Width="20%">
                            <ItemTemplate>
                                    
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <Columns>
                        <asp:TemplateField HeaderText="..." HeaderStyle-Width="20%">
                            <ItemTemplate>
                                    
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
