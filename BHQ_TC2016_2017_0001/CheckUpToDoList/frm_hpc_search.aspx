<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm_hpc_search.aspx.cs" Inherits="CheckUpToDoList.frm_hpc_search" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

<link rel="stylesheet" href="style/Style.css" />

<link rel="Stylesheet" href="style/smoothness/jquery-ui-1.10.3.custom.css" />
<link rel="stylesheet" href="style/styleAutoComplete.css" />

<script type="text/javascript" src="js/jquery-1.9.1.js"></script>
<script type="text/javascript" src="js/jquery-ui-1.10.3.custom.js"></script>
<script type="text/javascript" src="js/jquery.min.js"></script>
<script type="text/javascript" src="js/jquery-ui.min.js"></script>
<style type="text/css">
    .CompanyName,.txtDate,.ddl-width
    {
        width:254px;
    }
    
    .ddl-width
    {
        width:200px;
    }
    
    .txtDate-width
    {
        width:115px;
    }
    .dropdown-width
    {
        width:260px;
    }
    .txtOther-width
    {
        width:254px;
    }
    
    .horizontalscroll
    {
    overflow-x: auto;
    overflow-y: auto;
    width:280px;
    }
</style>

<script type="text/javascript">

    $(document).ready(function () {
        //$(".txtDate").datepicker({ dateFormat: "dd/mm/yy" });

        $('.CompanyName').autocomplete({
            source: function (request, response) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "frmSearchCompany.aspx/GetCompanyList",
                    data: "{'cname':'" + document.getElementById('txtsearch').value + "'}",
                    dataType: "json",
                    success: function (data) {
                        response(data.d);
                    },
                    error: function (result) {
                        alert(result);
                    }
                });
            }
        });
    });
</script>
</head>
<body>

    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <table cellpadding="0" cellspacing="2" border="0" style="width: 280px">
            <tr>
                <td colspan="2">
                    <strong>Company Name(TH)/Company Name(EN)</strong>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:TextBox ID="txtsearch" runat="server" CssClass="CompanyName" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <strong>Start Date :</strong>                                   
                </td>
                <td>
                    <strong>End Date :</strong>                                   
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtstart" runat="server" CssClass="txtDate-width" ></asp:TextBox>
                    <asp:CalendarExtender ID="txtstart_CalendarExtender" Format="dd/MM/yyyy" runat="server" 
                        TargetControlID="txtstart">
                    </asp:CalendarExtender>
                </td>
                <td>
                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="txtDate-width" ></asp:TextBox>
                    <asp:CalendarExtender ID="txtEndDate_CalendarExtender" Format="dd/MM/yyyy" runat="server" 
                    TargetControlID="txtEndDate">
                    </asp:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td colspan="2"><strong>Legal Company :</strong></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:TextBox ID="txtlegal" runat="server" CssClass="txtOther-width" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2"><strong>Dept. Owner :</strong></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:DropDownList ID="ddltypeofcomp" runat="server" CssClass="dropdown-width" >
                        <asp:ListItem Value=""></asp:ListItem>
                        <asp:ListItem Value="JMS">JMS</asp:ListItem>
                        <asp:ListItem Value="N/A">N/A</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2"><strong>Checkup Type :</strong></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:DropDownList ID="DDType" runat="server" CssClass="dropdown-width">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2"><strong>Location Checkup :</strong></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:DropDownList ID="DDLocation" runat="server" CssClass="dropdown-width">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:CheckBox ID="ChSearch" runat="server" Text="Active" Checked="True" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" 
                        onclick="btnSearch_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                <div class="horizontalscroll" style="height:300px; width:260px; ">
                    <table cellpadding="2" width="620px" cellspacing="2">
                        <tr class="headerStyle">
                            <td style="width:20px;">No</td>
                            <td style="width:80px;">Doc. No.</td>
                            <td style="width:300px;">Company Name</td>
                            <td style="width:100px;">Start Date</td>
                            <td style="width:100px;">End Date</td>
                            <td style="width:100px;">Update By</td>
                        </tr>
                        <asp:Repeater ID="RepeaterCompany" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td style="width:20px;"><%# Container.ItemIndex + 1 %></td>
                                    <td style="width:50px;"><%# DataBinder.Eval(Container.DataItem, "tcd_document_no")%></td>
                                    <td style="width:300px;"><a target="frm2" href='frm_hpc_content.aspx?id=<%# DataBinder.Eval(Container.DataItem, "tcd_id")%>&status=1&active=<%# DataBinder.Eval(Container.DataItem, "status")%>'><%# DataBinder.Eval(Container.DataItem, "tcd_tname")%></a></td>
                                    <td style="width:100px;"><%# String.Format("{0:dd/MM/yyyy}", DataBinder.Eval(Container.DataItem, "tcd_date_from"))%></td>
                                    <td style="width:100px;"><%# String.Format("{0:dd/MM/yyyy}", DataBinder.Eval(Container.DataItem, "tcd_date_to"))%></td>
                                    <td style="width:100px;"><%# DataBinder.Eval(Container.DataItem, "mul_user_login")%></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
                </td>
            </tr>
        </table>
    </form>
  
    </body>
</html>
