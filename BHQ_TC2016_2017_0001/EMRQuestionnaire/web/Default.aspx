<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EMRQuestionnaire.web.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<link href="../css/FontsThai/fontsthaisans_neueregular.css" rel="stylesheet" type="text/css" />
<link href="../css/maincss.css" rel="stylesheet" type="text/css" />

<link rel="shortcut icon" href="../images/quiz-games-300x300.png" />
<link href="../datePicker/jquery-ui.css" rel="stylesheet" type="text/css" />

<script src="../datePicker/external/jquery/jquery.js" type="text/javascript"></script>

<script src="../datePicker/jquery-ui.js" type="text/javascript"></script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="headDefaults" runat="server">
    <title>History Questionaire</title>
    <style type="text/css">
        .style1
        {
            width: 185px;
        }
    </style>
</head>

<script type="text/javascript">
    $(document).ready(function() {
        $("#txtBirthDate").datepicker({ dateFormat: 'dd-mm-yy', inline: true });
    });
  
</script>

<body>
   <table border ="0" cellpadding ="0" cellspacing="0" width="100%"  align="center" > <tr> <td  align ="center">
    <form id="frmHistory" runat="server">
    
    <div id="dash" class="header" style="vertical-align:middle;width: 1080px;" >
    
        <div id="left" style="float:left;"> 
            <img src="~/images/header_logo.jpg" />  
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
    
     
    <div id="Content" style="margin-top: 1px;width: 1080px;" >
        <div id="mainHead" style="background-color: #0072BB; color: #FFFFFF; height: 20px;
            padding: 8px 14px 9px;" class ="divBarMainTopic">
             <asp:Label ID="LabelMEDMain" runat="server" Text=""></asp:Label>
       </div>
        <div id="divBtn" style=" width: 1080px; height: 120px;">
            <br />
            <div style="">
                <table>
                    <tr>
                        <td   class="trPath" style="width:100px">
                           <asp:Label ID="LabelMED01" runat="server" Text=""></asp:Label>
                             
                                
                            
                        </td>
                        <td class="trPath"><asp:TextBox ID="txtHN" runat="server" class="inputbox" MaxLength="12"></asp:TextBox></td>
                    </tr>
                    <tr>    
                        <td class="trPath" style="width:100px"><asp:Label ID="LabelMED02" runat="server" Text=""></asp:Label></td>
                        <td class="trPath"><asp:TextBox ID="txtName" runat="server" class="inputbox"></asp:TextBox></td>
                    </tr>
                     <tr>    
                        <td class="trPath" style="width:100px">  <asp:Label ID="LabelMED03" runat="server" Text=""></asp:Label></td>
                        <td class="trPath"><asp:TextBox ID="txtBirthDate" runat="server" class="inputbox"></asp:TextBox></asp:TextBox></td>
                    </tr>
                    <tr>
                    <td colspan="2" class="trPath" ><asp:Label ID="lblWarningVerify" runat="server" Text=""></asp:Label>
                            <asp:Button ID="btnVerify" runat="server" Width="100" Height="25" class="buttonRec"  Text="" OnClick="btnVerify_Click" />
                            <div id="alertWarning" class="verify"></div>
                    </td>
                    </tr>
                </table>
            </div>
        </div>
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
