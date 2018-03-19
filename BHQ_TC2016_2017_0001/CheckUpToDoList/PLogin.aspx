<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PLogin.aspx.cs" Inherits="CheckUpToDoList.PLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="js/JScript1.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <style type="text/css">
        .btlogin :hover
        {
            background-image: url(images/login/btlogin2.gif);
            width: 200px;
        }
        .comment{  font-family:Tahoma; color:Red; font-size:13px;}
    </style>
<div style="margin:0 auto;padding:50px 50px; height:450px;">
    <center>
    <div id="divLogin" style="height:450px;">
        <table align="center" bgcolor="LightBlue" cellpadding="3" cellspacing="0" 
            style="border: 2px solid #808080;" width="400px">
            <tr>
                <td>
                    <table bgcolor="White" style="width: 100%;">
                        <tr>
                           
                            <td>
                                <div style="text-align: center">
                                    <span style="font-size: 20px; font-family: tahoma; font-weight: bold; color: #333333;
                                                text-align: center;">Check-up To Do List&nbsp;&nbsp;&nbsp;&nbsp; </span>
                                </div>
                                <div style="text-align: right">
                                    <span style="font-size: 10px; font-family: tahoma; font-weight: bold; color: #333333;
                                                text-align: right;"><b>Version : 0.01 </b></span>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <table cellpadding="3" cellspacing="0" style="width: 100%;">
                        <tr>
                            <td align="center">
                            </td>
                        </tr>
                    </table>
                    <table align="center" cellpadding="3" cellspacing="0" style="width: 90%;">
                        <tr>
                            <td align="center" style="width: 40%">
                                <img alt="" height="100px" src="images/login/lock.png" width="100px" />
                            </td>
                            <td align="right">
                                <table cellpadding="0" cellspacing="0" style="font-size: 12px; font-family: tahoma;color: #333333; width: 100%">
                                    <tr>
                                        <td style="width: 85px">
                                            Username :
                                        </td>
                                        <td align="left" style="padding-left:5px;">
                                            <asp:TextBox ID="txtUser" Width="100px" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="left" style="height:3px" >
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="comment"  ControlToValidate="txtUser" 
                                            ErrorMessage="* Required"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td> Password : </td>
                                        <td align="left" style="padding-left:5px">
                                            <asp:TextBox ID="txtpass" Width="100px"  TextMode="Password" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td> <center><a href="download/BMC_EMRCheckUpToDoList_UM_002.docx" target="_blank">User Manual</a></center></td>
                                        <td align="center" style="padding: 5px">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="btnLogin" runat="server" class="btlogin" 
                                                            OnClick="btnsubmit_Click" src="images/login/btlogin1.gif" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <%--<a href="Utility/BMC_EMR%20DM%20Pathway_UM_100.pdf" style="font-size: 9px" 
                                    target="_blank">User Manual</a>--%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <span id="lblWarning" style="color: #CC0000;"></span>
                                <asp:Label ID="lbalertmsg" runat="server" CssClass="comment" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div style="font-size: 9px; font-family: tahoma; color: #808080; text-align: justify;">
                                    <b>Disclaimer:</b> The information contained in this application may be 
                                    confidential and protected information intended only for the use of the entity 
                                    name above. As the user of this application you may be prohibited by law from 
                                    disclosing this information to any other parties without specific written 
                                    authorization from the individual to whom it pertains and you are hereby 
                                    notified that any disclosure, dissemination, distribution, copying, or action 
                                    taken in reliance on the contents of this communication is strictly prohibited<br />
                                    <br />
                                    <center>
                                        Copyright (c) 2010 Greenline Synergy Co.,Ltd. All rights reserved.</center>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <table bgcolor="White" style="width: 100%;">
                        <tr>
                            <td align="center">
                                <img alt="" height="40px" src="images/login/bdms_logo.png" />
                                <img alt="" height="40px" src="images/login/gls_logo.png" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </center>
</div>

    </div>
    <asp:Literal ID="litscript" runat="server"></asp:Literal>
    </form>
</body>
</html>
