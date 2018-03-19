<%@ Page Title="" Language="C#" MasterPageFile="~/HeadToDoListNew.master" AutoEventWireup="true" CodeBehind="frmCompanyMst.aspx.cs" Inherits="CheckUpToDoList.frmCompanyMst" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_body" runat="server">
<table border="0" cellpadding="0" cellspacing="0" style="width:100%;" >
        <tr>
        <td id="t1" valign="top" class="left-content">
            <iframe id="Iframe1" name="frm1" src="frmCompanyMstSearch.aspx" style=" width:100%; min-height:500px;"></iframe>
        </td>
        <td id="t2" valign="top" class="right-content" style="height:500px;">    
            <iframe id="Iframe2" name="frm2" src="frmCompanyMstBody.aspx" style=" width:100%; min-height:500px;"></iframe>
        </td>
        </tr>
    </table>
    <%--<script type="text/javascript">
        // Script สำหรับให้ Column Search มีความยาวเท่ากับ Column Data (Right)
        var leftHeight = $('#frm1').height();
        var rightHeight = $('#t2').height();
        if (leftHeight >= rightHeight) {
            $('#t2').css({ 'height': leftHeight });
        } else {
            $('#frm1').css({ 'height': rightHeight });
        }
    </script>--%>
    
</asp:Content>
