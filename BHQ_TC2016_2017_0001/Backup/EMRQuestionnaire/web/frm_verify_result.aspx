<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm_verify_result.aspx.cs"
    Inherits="EMRQuestionnaire.web.frm_verify_result" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<link href="../css/FontsThai/fontsthaisans_neueregular.css" rel="stylesheet" type="text/css" />
<link href="../css/maincss.css" rel="stylesheet" type="text/css" />
<link rel="shortcut icon" href="../images/quiz-games-300x300.png" />
<%--<script src="../scripts/verify_result.js" type="text/javascript"></script>
--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>History Questionaire</title>
<%--    <script type="text/javascript" language="javascript">
        function AviationType_Click(clic) {
            var item = document.getElementById(clic).getElementsByTagName('a');
            alert(item);
        }
    </script>--%>
</head>
<body>
<table border ="0" cellpadding ="0" cellspacing="0" width="100%" > <tr> <td  align ="center">
    <form id="frmHistory" runat="server">
    <div id="dash" class="header">
    
        <div id="left" style="float:left;"> 
            <img src="../images/header_logo.jpg" />  
        </div>
       <div id="right" 
            style="float:right;width:50%; height:22px; vertical-align:bottom;" 
            align="right">
            &nbsp;
                      
        </div>
          <div id="Div1" 
            style="float:right;width:50%; height:30px; vertical-align:bottom;" 
            align="right">
           <span style="vertical-align:bottom;"> <asp:ImageButton ID="imgThai" 
                 runat="server" ImageUrl="~/images/Thailand.png" OnClick="imgThai_Click"  
                 Height="36px" />
            <asp:ImageButton ID="imgEnglish" runat="server" 
                 ImageUrl="~/images/United-Kingdom.png" OnClick="imgEnglish_Click" 
                 Height="36px"/></span>
          </div> 
    </div>
    <div id="Content" style="margin-top: 1px; width: 1080px;">
        <div id="mainHead" style="background-color: #4980C1; color: #FFFFFF; height: 20px;
            padding: 8px 14px 9px;">
             <asp:Label ID="LabelQuesMain" runat="server" Text=""></asp:Label></div> 
        <br />        
        <div id="divBtnHealthHistory"  align ="center">
        <asp:Button
                    ID="btnHealthHistory" runat="server" Width="250" class="buttonRec" Text=""
                    OnClick="btnHealthHistory_Click" />
        </div>
        <br />
        <div id="divBtnMainPage" align ="center" >
            <asp:Button ID="btnMainPage" runat="server" Width="250" class="buttonRec" Text=""
                OnClick="btnMainPage_Click" />
        </div>        
        <br />
        <div id="divBtnAviation"  align ="center">
            <asp:Button
                    ID="btnAviation" runat="server" Width="250" class="buttonRec" 
                Text="" onclick="btnAviation_Click" />
            <table id="Aviation" cellpadding="0" cellspacing="0">        
            <tr><td>
            <asp:Panel ID="plAviationType" runat="server" Visible="false">
                <asp:BulletedList  ID="AviationTypeLink" runat="server"
                    DisplayMode="LinkButton" onclick="AviationTypeLink_Click" BulletStyle="NotSet">
                    <asp:ListItem Value="~/web/Aviation_Initial.aspx" Text="Application for Medial Certificate"></asp:ListItem>
                    <asp:ListItem Value="~/web/Aviation_Renew.aspx" Text="Application for Renewal Medial Certificate"></asp:ListItem>
                </asp:BulletedList>                
            </asp:Panel>
            </td></tr></table>
        </div>                         
       <%--<div id="aviationType">
       <table border ="0" cellpadding ="0" cellspacing="0" width="250">
        <tr>
            <td align="left">
                <ul>
                    <li>Initial</li>
                    <li>Renewal</li>
                </ul>    
            </td>
        </tr>        
       </table> 
       </div>   --%>      
    </div>
    <br />
    <br />
    <div align="center">
        Bangkok Hospital, 2 Soi Soonvijai 7, New Petchaburi Rd., Bangkok, Thailand 10310
        Tel.(+66) 2310-3000, 1719 (local mobile calls only)
    </div>
    </form>
    </td></tr></table>
</body>
</html>
