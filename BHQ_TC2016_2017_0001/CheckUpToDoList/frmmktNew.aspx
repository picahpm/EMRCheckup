<%@ Page Title="" Language="C#" MasterPageFile="~/HeadToDoListNew.Master" AutoEventWireup="true" CodeBehind="frmmktNew.aspx.cs" Inherits="CheckUpToDoList.frmmktNew" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_body" runat="server">
<%--<table style="width:100%;">
    <tr>
        <td>
            <iframe id="Iframe1" name="frm1" style="border-style:none;width:290px;height:1500px;border-style:none;" src="frmSearch.aspx" runat="server"></iframe>
        </td>
        <td>
            <iframe id="frm2" name="frm2" src="frmmktdata.aspx" style="width:1000px;height:1500px;border-style:none;" runat="server" ></iframe>
        </td>
    </tr>
</table>--%>

<table style="width:1270px;">
<tr>
    <td style="width:280px;border-right:1px dashed gray; vertical-align:top;"><iframe id="Iframe1" name="frm1" style="border:none;height:740px; width:280px;" src="frmSearch.aspx" runat="server"></iframe></td>
    <td style="width:960px; vertical-align:top;"><iframe id="frm2" runat="server" name="frm2" src="frmmktdata.aspx" style="width:960px;height:740px;border:none;"></iframe></td>
</tr>
</table>
    <%--<script type="text/javascript">
        document.getElementById("ctl00_ContentPlaceHolder_body_frm2").onload = function () {
            calcHeight();
        };

        function calcHeight() {

            var frm_height = (document.getElementById('ctl00_ContentPlaceHolder_body_frm2').contentWindow.document.body.offsetHeight + 10) + 'px';
            document.getElementById('ctl00_ContentPlaceHolder_body_frm2').style.height = frm_height;
            document.getElementById('ctl00_ContentPlaceHolder_body_Iframe1').style.height = frm_height;
        }
    </script>--%>

    <%--<table border="0" cellpadding="0" cellspacing="0" style="width:100%;" >
        <tr>
        <td id="t1" valign="top" class="left-content">
            <iframe id="Iframe1" name="frm1" src="frmSearch.aspx" style=" border-style: none; width:100%; min-height:700px; z-index:1;"></iframe>
        </td>
        <td id="t2" valign="top" class="right-content" style=" min-height:700px;">    
            <iframe id="frm2" name="frm2" src="frmmktdata.aspx" style="border-style: none; width:100%; min-height:700px; z-index:2;" runat="server"></iframe>
        </td>
        </tr>
    </table>--%>
    <%--<script type="text/javascript" src="js/jquery-1.9.1.js"></script>
    <script type="text/javascript">
//        // Script สำหรับให้ Column Search มีความยาวเท่ากับ Column Data (Right)
//        var leftHeight = $('#frm1').height();
//        var rightHeight = $('#t2').height();
//        if (leftHeight >= rightHeight) {
//            $('#t2').css({ 'height': leftHeight });
//        } else {
//            $('#frm1').css({ 'height': rightHeight });
        //        }

//        document.getElementById("ctl00_ContentPlaceHolder_body_frm2").onload = function () {
//            calcHeight();
//        };

//        function calcHeight() {
//            var frm_height = (document.getElementById('ctl00_ContentPlaceHolder_body_frm2').contentWindow.document.body.offsetHeight + 30) + 'px';
//            document.getElementById('ctl00_ContentPlaceHolder_body_frm2').style.height = frm_height;
//            document.getElementById('Iframe1').style.height = frm_height;
//        }
    </script>--%>

</asp:Content>
