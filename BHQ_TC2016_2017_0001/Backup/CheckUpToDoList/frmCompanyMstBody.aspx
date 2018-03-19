<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmCompanyMstBody.aspx.cs" Inherits="CheckUpToDoList.frmCompanyMstBody" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
    .td-width
    {
    	width:250px;
    	padding:6px 6px;
    }
    .td-border
    {
    	border:1px solid silver;
    }
    .textbox-width
    {
    	width:100%;
    }
    .tabshow
    {
        background-color:White;
        display:inline-block;
        border:1px solid #000;
        padding:5px;
    }
    .tabClose
    {
        background-color:transparent;
        display:inline-block;
        border:1px solid #000;
        padding:5px;

    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%" cellpadding="0" cellspacing="0">                                
            <tr>
                <td class="td-width"><div style="background-color:Silver;padding:4px 4px;">Company Code :</div></td>
                <td colspan="3">
                    <asp:TextBox ID="TextBox37" CssClass="textbox-width" runat="server" 
                        MaxLength="20"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td-width"><div style="background-color:Silver;padding:4px 4px;">Company Name (Thai) :</div></td>
                <td colspan="3">
                    <asp:TextBox ID="TextBox2" CssClass="textbox-width" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td-width" ><div style="background-color:Silver;padding:4px 4px;">Company Name (Eng) :</div></td>
                <td colspan="3">
                    <asp:TextBox ID="TextBox3" runat="server" CssClass="textbox-width"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td-width" ><div style="background-color:Silver;padding:4px 4px;">Address :</div></td>
                <td colspan="3">
                    <asp:TextBox ID="TextBox4" runat="server" CssClass="textbox-width"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp</td>
                <td colspan = "3">
                    <table width = "100%" cellpadding = "4px" cellspacing = "4px">
                        <tr>
                            <td><div style="background-color:Silver;padding:4px 4px;">Aumphur :</div></td>
                            <td><asp:TextBox ID="TextBox16" runat="server"></asp:TextBox></td>
                            <td><div style="background-color:Silver;padding:4px 4px;">Tumbon :</div></td>
                            <td><asp:TextBox ID="TextBox25" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><div style="background-color:Silver;padding:4px 4px;">Province :</div></td>
                            <td><asp:TextBox ID="TextBox38" runat="server"></asp:TextBox></td>
                            <td><div style="background-color:Silver;padding:4px 4px;">Post Code :</div></td>
                            <td><asp:TextBox ID="TextBox39" runat="server"></asp:TextBox></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="td-width" ><div style="background-color:Silver;padding:4px 4px;">Telephone :</div></td>
                <td  style="width: 62px;" >
                    <div><asp:TextBox ID="TextBox5" runat="server"></asp:TextBox></div>
                </td>
                <td class="td-width"><div style="background-color:Silver;padding:4px 4px;">Fax :</div></td>
                <td>
                    <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td-width" ><div style="background-color:Silver;padding:4px 4px;">E-mail Address :</div></td>
                <td colspan="3">
                    <asp:TextBox ID="TextBox7" runat="server" CssClass="textbox-width"></asp:TextBox>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
