<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmmktdata.aspx.cs" Inherits="CheckUpToDoList.frmmktdata" EnableEventValidation="false" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>
</title>
<link rel="Stylesheet" href="style/Style.css" />
<link rel="Stylesheet" href="style/smoothness/jquery-ui-1.10.3.custom.css" />
<link rel="stylesheet" href="style/styleAutoComplete.css" />

<style type="text/css">
    .tabshow
    {   background-color:White;display:inline-block;border:1px solid #000;padding:5px;}
    .tabClose
    {   background-color:transparent;display:inline-block;border:1px solid #000;padding:5px;}
    .horizontalscroll
    { 
        overflow-x: auto; 
        overflow-y: auto;
        width:945px;
        height:450px;
    } 
    .txtAlignCenter{ text-align:center;}
       
    .style1
    {
        width: 138px;
    }
       
    </style>
    <script type="text/javascript" src="js/jquery-1.9.1.js">
    </script>
    <script type="text/javascript" src="js/jquery-ui-1.10.3.custom.js">
    </script>
    <script type="text/javascript" src="js/jquery.min.js">
    </script>
    <script type="text/javascript" src="js/jquery-ui.min.js">
    </script>
    <script src="js/JScript1.js" type="text/javascript">
    </script>
    <script type="text/javascript">
        var jq = jQuery.noConflict();
        var arrCon = new Array();
        var arrConBill = new Array();
        var arrPayAgree = new Array();
        var arrMkt = new Array();
        var arrDocCate = new Array();
        var arrSite = new Array();
        var araPlan = new Array();
	</script>

    <script type="text/javascript">
        function checkEnter(event) {
            if (event) {
                if (event.keyCode == 13) //ur code
                {
                    //alert("press enter key");//return;
                    $('#btnsearch').click();
                    
                }
            }

        }
</script>
</head>
<body onkeypress="checkEnter(event)">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="SelTool" style="margin-top:10px;padding:5px 5px;text-align:right;">
            <asp:Button ID="btnAddNew" runat="server" Text="New" Width="100px" Visible="False" onclick="btnAddNew_Click" />
            <asp:Button ID="btnEditFrm" runat="server" Text="Edit" Width="100px" onclick="btnEditFrm_Click" Visible="False" />
            <asp:Button ID="btnExport" runat="server" Text="Export" Width="100px"  onclick="btnExport_Click" Visible="False" />
            <asp:Button ID="btnCopy" runat="server" Text="Copy" Width="100px" onclick="btnCopy_Click"  Visible="False" />
            <asp:HiddenField ID="HDIsCopy" runat="server" Value="0" />
        </div>
        <center>
            <asp:Label ID="lbmsgAlert" runat="server" CssClass="comment"> </asp:Label>
        </center>
        <asp:HiddenField ID="HDStatus" runat="server" Value="0" />
        <asp:HiddenField ID="HDActive" runat="server" Value="A" />
        <asp:HiddenField ID="HDedit" runat="server" />
        <asp:TabContainer ID="TabContainer1" CssClass="ajax__tab_Custom" Width="100%" runat="server" 
            ActiveTabIndex="0" onactivetabchanged="TabContainer1_ActiveTabChanged" 
            AutoPostBack="false">
        <asp:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1">
        
        
        <HeaderTemplate>
            ข้อมูลบริษัท
        
</HeaderTemplate>
<ContentTemplate>
<asp:Panel ID="PanelCompanyData" runat="server"><asp:UpdatePanel ID="UpdatePanel4" runat="server"><ContentTemplate><table style="width:850px;padding:5px;"><tr><td ><table width="100%" cellpadding="0" cellspacing="0"><tr><td class="tabHeader">ข้อมูลบริษัท</td></tr><tr><td><table width="100%" cellpadding="0" cellspacing="0"><tr><td class="td-width"><div class="divtitleNameStyle">Import Data File (.xls,.xlsx): </div></td><td colspan="3">&#160; <asp:FileUpload ID="ImportFileUploadCompanyDetail" runat="server" /><asp:Button ID="btnImport" runat="server" CssClass="buttonCss" onclick="btnImport_Click" Text="Import file" /><asp:Label ID="lblselectfile" runat="server" ForeColor="Red"> </asp:Label></td></tr><tr><td class="td-width" ><div class="divtitleNameStyle">Document Code :</div></td><td colspan="3"><asp:TextBox ID="txtdoccode" runat="server" MaxLength="200" Enabled="False"> </asp:TextBox></td></tr><tr><td class="td-width"><div class="divtitleNameStyle">Company Code :</div></td><td><asp:TextBox ID="txt_company_code" runat="server" MaxLength="20"></asp:TextBox><asp:RequiredFieldValidator ID="Requiredtxt_company_code" runat="server" 
                                        ControlToValidate="txt_company_code" ErrorMessage="* Required" 
                                        ValidationGroup="SaveData"> </asp:RequiredFieldValidator></td><td><div class="divtitleNameStyle">Year :</div></td><td><asp:DropDownList ID="DDCompanyYear" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr><td class="td-width"><div class="divtitleNameStyle">Company Name (Thai) :</div></td><td colspan="3"><asp:TextBox ID="txtComNameTH" runat="server" Width="600px" MaxLength="300" > </asp:TextBox><asp:Button ID="btnfindnameth" CssClass="buttoncss" runat="server" Text="Search" onclick="btnfindnameth_Click" /></td>
                            </tr>
                            <tr><td class="td-width" ><div class="divtitleNameStyle">Company Name (Eng) :</div></td><td colspan="3"><asp:TextBox ID="txtComNameEng" runat="server" Width="670px" MaxLength="200"> </asp:TextBox></td></tr><tr><td class="td-width" ><div class="divtitleNameStyle">Sub Company :</div></td><td colspan="3"><asp:TextBox ID="txtlegal" runat="server" Width="670px" MaxLength="200"> </asp:TextBox></td></tr><tr><td class="td-width"><div class="divtitleNameStyle">Dept. Owner :</div></td><td colspan="3"><asp:RadioButton 
        ID="rdona" runat="server" GroupName="mcotype" Text="N/A" Checked="True" /><asp:RadioButton ID="rdojms" runat="server" GroupName="mcotype" Text="JMS" /></td>
                            </tr>
                            <tr><td class="td-width" ><div class="divtitleNameStyle">Address :</div></td><td colspan="3"><asp:TextBox ID="txtComAddress" runat="server" CssClass="textbox-width" MaxLength="199"> </asp:TextBox></td></tr><tr><td>&nbsp;</td><td colspan = "3"><table width = "100%" cellpadding = "0" cellspacing = "4px"><tr><td class="td-width"><div class="divtitleNameStyle">Sub-district :</div></td><td><asp:TextBox ID="txtComTumbon" runat="server" MaxLength="100"> </asp:TextBox></td><td class="td-width"><div class="divtitleNameStyle">District :</div></td><td><asp:TextBox ID="txtComAumphur" runat="server" MaxLength="100"> </asp:TextBox></td></tr><tr><td class="td-width"><div class="divtitleNameStyle">Province :</div></td><td><asp:TextBox ID="txtComProvince" runat="server" MaxLength="100"> </asp:TextBox></td><td class="td-width"><div class="divtitleNameStyle">Post Code :</div></td><td><asp:TextBox ID="txtComPostCode" runat="server" MaxLength="10"> </asp:TextBox></td></tr></table></td></tr><tr><td class="td-width" ><div class="divtitleNameStyle">Telephone :</div></td><td ><div><asp:TextBox ID="txtComTel" runat="server" MaxLength="100"> </asp:TextBox></div></td><td class="td-width"><div class="divtitleNameStyle">Fax :</div></td><td><asp:TextBox ID="txtComFax" runat="server" MaxLength="100"> </asp:TextBox></td></tr><tr><td class="td-width" ><div class="divtitleNameStyle">E-mail Address :</div></td><td colspan="2"><asp:TextBox ID="txtComEmail" runat="server" CssClass="textbox-width" MaxLength="100"> </asp:TextBox></td><td style="padding-left:5px;"><asp:RegularExpressionValidator ID="RegularExpressionValidator2_txtComEmail" 
                                            runat="server" ErrorMessage=" Email incorrect format." 
                                            ControlToValidate="txtComEmail" Display="Dynamic" 
                                            ValidationExpression="^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$" 
                                            SetFocusOnError="True"  ValidationGroup="SaveData" > </asp:RegularExpressionValidator></td></tr><tr><td class="td-width" ><div class="divtitleNameStyle">Contact Person :</div></td><td colspan="3"><asp:TextBox ID="txtConPer" runat="server" Font-Italic="False" ForeColor="Black"> </asp:TextBox>&#160;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"  ControlToValidate="txtConPer" ValidationGroup="EmaiCon" ErrorMessage="* Required"> </asp:RequiredFieldValidator><span id="alertconper" style="display:none;color:Red;">*</span> </td></tr><tr><td class="td-width" ><div class="divtitleNameStyle">Contact Person Tel :</div></td><td><asp:TextBox ID="txtConTel" runat="server" validationgroup="EmaiCon" Font-Italic="False" ForeColor="Black"> </asp:TextBox>&nbsp; <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtConTel" ValidationGroup="EmaiCon" ErrorMessage="* Required"> </asp:RequiredFieldValidator></td><td class="td-width"><div class="divtitleNameStyle">Contact Person Fax :</div></td><td><asp:TextBox ID="txtConFax" runat="server" validationgroup="EmaiCon" Font-Italic="False" ForeColor="Black"></asp:TextBox>&#160; <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"  
                                            ControlToValidate="txtConFax" ValidationGroup="EmaiCon" ErrorMessage="* Required"> </asp:RequiredFieldValidator></td></tr><tr><td class="td-width" ><div class="divtitleNameStyle">Contact Person E-mail :</div></td><td colspan="3"><asp:HiddenField ID="txtHiddenContact" runat="server" /><asp:TextBox ID="txtConEmail" runat="server"> </asp:TextBox><asp:Button ID="btnaddContact" runat="server"  Text="+" 
                                                                            ValidationGroup="EmaiCon" onclick="btnaddContact_Click" /><asp:RegularExpressionValidator ID="RegularExpressionValidatortxtConEmail" 
                                                                        runat="server" ErrorMessage=" อีเมลล์ไม่ถูกต้อง" 
                                                                        ControlToValidate="txtConEmail" Display="Dynamic" 
                                                                        ValidationExpression="^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$" 
                                                                        SetFocusOnError="True"  ValidationGroup="EmaiCon"> </asp:RegularExpressionValidator>&nbsp; </td></tr><tr><td></td>
                                <td  colspan="3"><div style="border: 1px solid #C0C0C0;"><table id="tblConPer" border="0" cellpadding="0" cellspacing="0" style="width:100%"><tr class="headerStyle"><td style="width:20px;">No.</td><td>ชื่อ-สกุล</td><td>Contact Tel</td><td>Contact Fax</td><td>Contact E-mail</td><td>...</td></tr><asp:Repeater ID="RepeaterContacrPerson" runat="server" 
                                                                                        onitemcommand="RepeaterContacrPerson_ItemCommand" ><ItemTemplate><tr><td><%# Container.ItemIndex + 1 %>
                                            </td>
                                            <td><%# DataBinder.Eval(Container.DataItem, "Name")%>
                                            </td>
                                            <td><%# DataBinder.Eval(Container.DataItem,"Tel") %>
                                            </td>
                                            <td><%# DataBinder.Eval(Container.DataItem,"Fax") %>
                                            </td>
                                            <td><%# DataBinder.Eval(Container.DataItem,"Email") %>
                                            </td>
                                            <td><asp:ImageButton ID="ImageBtnDel" ImageUrl="~/images/delete.png" OnClientClick="return msgconfirmDelete();" CommandName="Del" runat="server" /></td>
                                            </tr>
                                            </ItemTemplate>
                                            </asp:Repeater>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr><td class="td-width" ><div class="divtitleNameStyle">Contract Date From :</div></td><td><asp:TextBox ID="txtconDatefrom" runat="server" MaxLength="10"> </asp:TextBox><asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                                        TargetControlID="txtconDatefrom" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender>
                                </td>
                                <td class="td-width"><div class="divtitleNameStyle">Date To :</div></td><td><asp:TextBox ID="txtConDateto" runat="server"> </asp:TextBox><asp:CalendarExtender ID="CalendarExtender2" runat="server" 
                                     TargetControlID="txtConDateto" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender>
                                </td>
                            </tr>
                            </table>
                        </td>
                        </tr>
                        <tr><td class="tabHeader">ข้อมูลการวางบิล</td></tr><tr><td><table width="100%" cellpadding="0" cellspacing="0"><tr><td class="td-width"><div class="divtitleNameStyle">Company Name (Bill) :</div></td><td colspan="3"><asp:TextBox ID="txtbillComNameth" runat="server" CssClass="textbox-width" MaxLength="300"> </asp:TextBox></td></tr><tr><td class="td-width" ><div class="divtitleNameStyle">Address (Bill):</div></td><td colspan="3"><asp:TextBox ID="txtbillAddress" runat="server" CssClass="textbox-width" 
                                                                                    MaxLength="100"> </asp:TextBox></td></tr><tr><td>&nbsp;</td><td colspan = "3"><table width = "100%" cellpadding = "4px" cellspacing = "4px"><tr><td class="td-width"><div class="divtitleNameStyle">Sub-district :</div></td><td><asp:TextBox ID="txtbillTumbon" runat="server" MaxLength="100"> </asp:TextBox></td><td class="td-width"><div class="divtitleNameStyle">District :</div></td><td><asp:TextBox ID="txtbillAumphur" runat="server" MaxLength="100"> </asp:TextBox></td></tr><tr><td class="td-width"><div class="divtitleNameStyle">Province :</div></td><td><asp:TextBox ID="txtbillProvince" runat="server" MaxLength="100"> </asp:TextBox></td><td class="td-width"><div class="divtitleNameStyle">Post Code :</div></td><td><asp:TextBox ID="txtbillPostCode" runat="server" MaxLength="10"> </asp:TextBox></td></tr></table></td></tr><tr><td class="td-width" ><div class="divtitleNameStyle">Telephone (Bill) :</div></td><td><asp:TextBox ID="txtbillTel" runat="server" MaxLength="100"> </asp:TextBox></td><td class="td-width" ><div class="divtitleNameStyle">Fax (Bill) :</div></td><td><asp:TextBox ID="txtbillFax" runat="server" MaxLength="100"> </asp:TextBox></td></tr><tr><td class="td-width" ><div class="divtitleNameStyle">Contact Person (Bill) :</div></td><td colspan="3"><asp:TextBox ID="txtConBill" runat="server" MaxLength="500"> </asp:TextBox>&#160;<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtConBill"  
                                                       ErrorMessage="* Required" ValidationGroup="conbill"> </asp:RequiredFieldValidator><span id="conbill" style="color:Red;display:none;">*</span> </td></tr><tr><td class="td-width"><div class="divtitleNameStyle">Contact Tel (Bill) :</div></td><td><asp:TextBox ID="txtConBillTel" runat="server" MaxLength="100"> </asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"  ControlToValidate="txtConBillTel" 
                                                                                    ErrorMessage="* Required" ValidationGroup="conbill"> </asp:RequiredFieldValidator></td><td class="td-width"><div class="divtitleNameStyle">Contact Fax (Bill) :</div></td><td><asp:TextBox ID="txtConBillFax" runat="server" MaxLength="100" > </asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtConBillFax"  
                                                                                    ErrorMessage="* Required" ValidationGroup="conbill"> </asp:RequiredFieldValidator></td></tr><tr><td class="td-width" ><div class="divtitleNameStyle">Contact E-mail (Bill) :</div></td><td colspan="3"><asp:HiddenField ID="txtHiddenContactBill" runat="server" /><asp:TextBox ID="txtConBillEmail" runat="server" MaxLength="100"> </asp:TextBox><asp:Button ID="btnaddbill" runat="server" Text="+" ValidationGroup="conbill" onclick="btnaddbill_Click" /><asp:RegularExpressionValidator ID="RegularExpressionValidatortxtConBillEmail" 
                                                                                runat="server" ErrorMessage=" อีเมลล์ไม่ถูกต้อง" 
                                                                                ControlToValidate="txtConBillEmail" Display="Dynamic" 
                                                                                ValidationExpression="^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$" 
                                                                                SetFocusOnError="True"  ValidationGroup="conbill"
                                                                                    > </asp:RegularExpressionValidator></td></tr><tr><td>&nbsp;</td><td colspan="3"><div style="border: 1px solid #C0C0C0;" ><table id="Table1" border="0" cellpadding="0" cellspacing="0" style="width:100%"><tr class="headerStyle"><td style="width:20px;">No.</td><td>ชื่อ-สกุล</td><td>Tel</td><td>Fax</td><td>E-mail</td><td>...</td></tr><asp:Repeater ID="RepeaterContactBill" runat="server" 
                                                                                            onitemcommand="RepeaterContactBill_ItemCommand"><ItemTemplate><tr><td><%# Container.ItemIndex + 1 %>
                                                </td>
                                                <td><%# DataBinder.Eval(Container.DataItem, "Name")%>
                                                </td>
                                                <td><%# DataBinder.Eval(Container.DataItem,"Tel") %>
                                                </td>
                                                <td><%# DataBinder.Eval(Container.DataItem,"Fax") %>
                                                </td>
                                                <td><%# DataBinder.Eval(Container.DataItem,"Email") %>
                                                </td>
                                                <td><asp:ImageButton ID="ImageBtnDel" ImageUrl="~/images/delete.png" OnClientClick="return msgconfirmDelete();" CommandName="Del" runat="server" /></td>
                                                </tr>
                                                </ItemTemplate>
                                                </asp:Repeater>
                                            </table>
                                            </div>
                                        </td>
                                    </tr>
                        <tr><td class="td-width" ><div class="divtitleNameStyle">Payor :</div></td><td colspan="3"><div style="display:inline;"><asp:HiddenField ID="txtHiddenPayAgree" runat="server" /><asp:TextBox ID="txt_payor" runat="server" Width="400px"> </asp:TextBox><span id="waitingpayor" style="width:14px;">&nbsp;&nbsp;&nbsp;</span> <asp:Button ID="btnRetrieve" runat="server" Text="Retrieve Plan" 
                                                                    onclick="btnRetrieve_Click" /><asp:Button ID="btnRefreshCornjob" runat="server" OnClientClick="OpenPopup()" Text="Refresh Payor" /><script type="text/javascript">
                            function OpenPopup() {
                                window.open("InterfaceTrakCare/RunJob.aspx", "Refresh Cornjob", "left = 400, top=250,status=no,toolbar=no,menubar=no,location=no,scrollbars=no,resizable=no,width=200,height=180");
                            }
                                                                </script>
                        </div>
                        </td>
                        </tr>
                        <tr><td class="td-width" valign="top"><div class="divtitleNameStyle">Plan :</div></td><td colspan="3"><div style="overflow-x: auto; overflow-y: auto; width: 700px;height:200px;border:1px solid silver;"><table id="tblPlan" style="width:680px;"><tbody ><tr class="headerStyle"><td style="width:20px;">No.</td><td style="width:640px;">Plan</td><td style="width:20px;">...</td></tr><asp:Repeater ID="RepeaterPlan" runat="server" 
                                                                            onitemcommand="RepeaterPlan_ItemCommand"><ItemTemplate><tr><td><%# Container.ItemIndex + 1 %>
                        </td>
                        <td><%# DataBinder.Eval(Container.DataItem, "tpl_name")%>
                        </td>
                        <td><asp:ImageButton ID="ImageBtnDel" ImageUrl="~/images/delete.png" OnClientClick="return msgconfirmDelete();" CommandName="Del" runat="server" /></td>
                        </tr>
                        </ItemTemplate>
                        </asp:Repeater>
                        </tbody>
                        </table>
                        </div>
                        </td>
                        </tr>
                        <tr><td class="td-width"><div class="divtitleNameStyle">Family&apos;s welfare :</div></td><td colspan="3"><asp:TextBox ID="txtFamilyWelfar" runat="server" 
                                                                CssClass="textbox-width" MaxLength="300"> </asp:TextBox></td></tr></table></td></tr></table></td></tr></table></ContentTemplate><Triggers><asp:PostBackTrigger ControlID="btnImport" ></asp:PostBackTrigger>
</Triggers>
</asp:UpdatePanel>


        <script type="text/javascript">

            function funcGetListPayAgree() {
                arrPayAgree = [];
                var payorName = document.getElementById("<%=txt_payor.ClientID%>").value;
                if (payorName.length > 2) {
                    var tbl = document.getElementById("tblPlan");
                    jQuery.ajax({
                        type: "post",
                        url: 'GetDataTrakCare.ashx',
                        data: 'type=payor&param1=' + payorName,
                        success: function (msg) {
                            if (msg != "-1") {
                                var datarow = msg.split("|*|");
                                var tmpStr = "<tr class='headerStyle'> <td>No.</td> <td style='display:none;'>Code</td> <td>Desc</td> <td>...</td> </tr>";
                                var stra1 = "";
                                for (var i = 0; i < datarow.length; i++) {
                                    var datacol = datarow[i].split(",");
                                    var desc = datacol[1].replace("|x|", ",");
                                    tmpStr += "<tr> <td>" + (i + 1) + "</td>";
                                    tmpStr += "<td style='display:none;'>" + datacol[0] + "</td>";
                                    tmpStr += "<td>" + desc + "</td>";
                                    tmpStr += "<td> <button type='button' id='btnTest' onclick='delRowPayAgree(" + i + ")'>X</button> </td> </tr>";
                                    arrPayAgree[arrPayAgree.length] = [datacol[0], datacol[1]];
                                    if (stra1 == "") {
                                        var strdata = arrPayAgree[i].toString();
                                        stra1 = strdata;
                                    } else {
                                        var strdata = arrPayAgree[i].toString();
                                        stra1 += "|*|" + strdata;
                                    }
                                }
                                var tbllabeldata = document.getElementById("<%=txtHiddenPayAgree.ClientID%>"); //เอาค่าใส่ใน Text Hidden
                                tbllabeldata.value = stra1;

                                tbl.innerHTML = tmpStr;
                            } else {
                                obj1.innerHTML = totrim(msg);
                            }
                        },
                        fail: function (msg) { }
                    });
                }
            }

            function insArrPayAgree() {
                arrPayAgree = [];
                var tbl = document.getElementById("tblPlan");
                var rowCount = tbl.rows.length;
                for (i = 1; i < rowCount; i++) {
                    var row = tbl.rows[i];
                    arrPayAgree[arrPayAgree.length] = [row.cells[1].innerHTML.replace(",", "|x|"), row.cells[2].innerHTML.replace(",", "|x|")];
                }
            }
            function BindingPayAgree() {
                var tbllabeldata = document.getElementById("<%=txtHiddenPayAgree.ClientID%>");
                if (tbllabeldata != null && tbllabeldata.value != "") {
                    var splitdata = tbllabeldata.value.split("|*|");
                    for (var i = 0; i < splitdata.length; i++) {
                        var dr = splitdata[i].split(",");
                        if (dr[0] != "") {
                            arrPayAgree[arrPayAgree.length] = [dr[0], dr[1]];
                        }
                    }
                    genTblPayAgree();
                }
            }
            function delRowPayAgree(row) {
                //insArrPayAgree();
                if (msgconfirmDelete() == true) {
                    arrPayAgree.splice(row, 1);
                    genTblPayAgree();
                }
            }

            function genTblPayAgree() {
                var tbl = document.getElementById("tblPlan");
                var rowTbl = tbl.rows.length;
                for (i = rowTbl - 1; i > 0; i--) {
                    tbl.deleteRow(i);
                }

                var arrCount = arrPayAgree.length;
                var stra1 = "";
                for (i = 0; i < arrCount; i++) {
                    var row = tbl.insertRow(tbl.rows.length);
                    var cellNo = row.insertCell(0);
                    cellNo.innerHTML = (i + 1);

                    arrDtl = arrPayAgree[i];
                    var arrDtlCount = arrDtl.length;
                    for (j = 0; j < arrDtlCount; j++) {
                        var cell = row.insertCell(j + 1);
                        cell.innerHTML = arrDtl[j].replace("|x|", ",");
                        if (j == 0) { cell.style.display = "none"; }
                    }

                    var cellBtn = row.insertCell(j + 1);
                    cellBtn.innerHTML = "<button type='button' onclick='delRowPayAgree(" + i + ")'>X</button>";
                    if (stra1 == "") {
                        var strdata = arrPayAgree[i].toString();
                        stra1 = strdata;
                    } else {
                        var strdata = arrPayAgree[i].toString();
                        stra1 += "|*|" + strdata;
                    }
                }
                var tbllabeldata = document.getElementById("<%=txtHiddenPayAgree.ClientID%>"); //เอาค่าใส่ใน Text Hidden
                tbllabeldata.value = stra1;
            }
        </script>
        <asp:Literal ID="littab1script" runat="server"></asp:Literal></asp:Panel>
</ContentTemplate>
</asp:TabPanel>
        <asp:TabPanel ID="TabPanel5" runat="server" HeaderText="TabPanel5">
        
        
        <HeaderTemplate>
            ประเภทการชำระเงิน


        
</HeaderTemplate>
<ContentTemplate>
        <asp:Panel ID="PanelPayment" runat="server">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
        <table style="width: 850px;padding:5px;">
        <tr>
        <td class="td-width" valign="top">
        <div class="divtitleNameStyle">Type :</div>
        </td>
        <td>
        <asp:DropDownList ID="DDtype" runat="server">
        </asp:DropDownList>
        </td>
        </tr>
        <tr>
        <td class="td-width" valign="top">
        <div class="divtitleNameStyle">Payment Type :</div>
        </td>
        <td >
        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" 
                ControlToValidate="RDPaymentType" Display="Dynamic" ErrorMessage="* Required" 
                ValidationGroup="AddpaymentData">
        </asp:RequiredFieldValidator>
        <div 
                style="border:solid 1px Silver; width:99%; padding-left:5px; padding-bottom:5px">
        <table border="0" cellpadding="0" cellspacing="0">
        <tr>
        <td valign="middle">
        <asp:RadioButtonList ID="RDPaymentType" runat="server" RepeatLayout="Flow" >
        <asp:ListItem Value="ชำระเงินสด">ชำระเงินสด</asp:ListItem>
        <asp:ListItem Value="วางบิล">วางบิล</asp:ListItem>
        <asp:ListItem Value="วางบิลและชำระเงินสดเพิ่ม">วางบิลและชำระเงินสดเพิ่ม</asp:ListItem>
        <asp:ListItem Value="วงเงิน">วงเงิน</asp:ListItem>
        </asp:RadioButtonList>
        <asp:TextBox ID="txtBillAmount" runat="server" MaxLength="10" Width="100px">
        </asp:TextBox>&#160;บาท

                                                </td>
        <td class="style1" style="padding:5px;" valign="bottom">
        </td>
        </tr>
        </table>
                                        หมายเหตุ <asp:TextBox ID="txtBillRemark" 
                runat="server" MaxLength="500" Width="600px">
        </asp:TextBox>
        </div>
        <script type="text/javascript">
            function checkRDpamenttype(strpaytype, strbillAmount) {
                var objrdtab3 = document.getElementById(strpaytype);
                var objtxtlimit = document.getElementById(strbillAmount);

                var inputs = objrdtab3.getElementsByTagName('input');

                if (inputs[inputs.length - 1].checked) {
                    objtxtlimit.disabled = false;
                } else {
                    objtxtlimit.value = "";
                    objtxtlimit.disabled = true;
                }
            }
                                    </script>
        </td>
        </tr>
        <tr>
        <td class="td-width" valign="top">
        <div class="divtitleNameStyle">Billing Method :</div>
        </td>
        <td>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
        <td colspan="2">
        <style type="text/css">
                                                .divBlock
                                                {
                                                    display: block;
                                                }
                                                .RdBlock
                                                {
                                                    display: inline;
                                                }
                                            </style>
        <div 
                style="border:solid 1px Silver; display:block; padding:2px 0px;margin-bottom:5px;margin-right:5px">
        <%--<asp:RequiredFieldValidator 
                                                        ID="RequiredFieldValidator1" runat="server" 
                                                        ControlToValidate="RDMbm_id" ErrorMessage="* Required" Display="Dynamic" 
                                                        ValidationGroup="AddpaymentData">
        </asp:RequiredFieldValidator>--%>
        <asp:RadioButtonList ID="RDMbm_id" runat="server" CssClass="RdBlock" 
                RepeatDirection="Horizontal">
        <asp:ListItem Value="1">วางบิลสำนักงานใหญ่ / Invoice send to the mother company</asp:ListItem>
        <asp:ListItem Value="2">วางบิลแต่ละบริษัท / Invoice send to each company</asp:ListItem>
        </asp:RadioButtonList>
        </div>
        </td>
        </tr>
        <tr>
        <td>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" 
                ControlToValidate="RDMpm_id" Display="Dynamic" ErrorMessage="* Required" 
                ValidationGroup="AddpaymentData">
        </asp:RequiredFieldValidator>
        <div style="width:350px;float:left;border:1px solid silver;padding-bottom:5px;">
        <div style="background-color:Silver;padding:3px 3px;">การชำระเงินชุดตรวจหลัก / term of payment : Main program</div>
        <asp:RadioButtonList ID="RDMpm_id" runat="server" RepeatLayout="Flow">
        <asp:ListItem Value="บริษัทจ่าย/Credit (Invoice to the conpany)">บริษัทจ่าย/Credit (Invoice to the conpany)</asp:ListItem>
        <asp:ListItem Value="ชำระเงินสด/Cash">ชำระเงินสด/Cash</asp:ListItem>
        </asp:RadioButtonList>
        </div>
        </td>
        <td>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" 
                ControlToValidate="RDmpr_id" Display="Dynamic" ErrorMessage="* Required" 
                ValidationGroup="AddpaymentData">
        </asp:RequiredFieldValidator>
        <div 
                style="width:320px;float:left;margin-left:5px;border:1px solid silver;padding-bottom:5px;">
        <div style="background-color:Silver;padding:3px 3px;">อัตราค่าบริการ / Check-up rate</div>
        <asp:RadioButtonList ID="RDmpr_id" runat="server" RepeatLayout="Flow">
        <asp:ListItem Value="เหมาจ่าย / Packages">เหมาจ่าย / Packages</asp:ListItem>
        <asp:ListItem Value="คิดตามรายการที่ตรวจจริง / Actual Price">คิดตามรายการที่ตรวจจริง / Actual Price</asp:ListItem>
        </asp:RadioButtonList>
        </div>
        </td>
        </tr>
        </table>
        </td>
        </tr>
        <tr>
        <td>
        </td>
        <td>
        <asp:RequiredFieldValidator 
                                        ID="RequiredFieldValidator20" runat="server" 
                                        ControlToValidate="RDmpq_id" ErrorMessage="* Required"  Display="Dynamic" 
                                        ValidationGroup="AddpaymentData">
        </asp:RequiredFieldValidator>
        <div style="border:1px solid silver;padding-bottom:5px;margin-right:5px;">
        <div style="background-color:Silver;padding:3px 3px;">ตรวจเพิ่มตามใบเสนอราคา / Term of payment : Options items 
                                            as montioned in quatation</div>
        <asp:RadioButtonList ID="RDmpq_id" runat="server" RepeatLayout="Flow">
        <asp:ListItem >ชำระเงินสด / Cash</asp:ListItem>
        <asp:ListItem >C/O บริษัท(รวมบิล) / C/O Company (Total Bills)</asp:ListItem>
        <asp:ListItem >C/O บริษัท(รวมบิล) [เฉพาะรายชื่อที่ระบุ]</asp:ListItem>
        <asp:ListItem >C/O บริษัท(แยกบิล) / C/O Company (Separate Bills)</asp:ListItem>
        <asp:ListItem >C/O บริษัท(แยกบิล) [เฉพาะรายชื่อที่ระบุ]</asp:ListItem>
        <asp:ListItem >วงเงิน C/O</asp:ListItem>
        </asp:RadioButtonList>
        <asp:TextBox ID="txtmqp_credit" runat="server" MaxLength="10">
        </asp:TextBox>&nbsp;บาท
                                    </div>
        </td>
        </tr>
        <tr>
        <td>
        </td>
        <td>
        <asp:RequiredFieldValidator 
                                        ID="RequiredFieldValidator21" runat="server" 
                                        ControlToValidate="RDmpn_id" ErrorMessage="* Required"  Display="Dynamic" 
                                        ValidationGroup="AddpaymentData">
        </asp:RequiredFieldValidator>
        <div style="border:1px solid silver;padding-bottom:5px;margin-right:5px;">
        <div style="background-color:Silver;padding:3px 3px;">ตรวจเพิ่มนอกใบเสนอราคา / Term of payment : Options items montioned not in quatation </div>
        <asp:RadioButtonList ID="RDmpn_id" runat="server" RepeatLayout="Flow">
        <asp:ListItem >ชำระเงินสด / Cash</asp:ListItem>
        <asp:ListItem >C/O บริษัท(รวมบิล) / C/O Company (Total Bills)</asp:ListItem>
        <asp:ListItem >C/O บริษัท(รวมบิล) [เฉพาะรายชื่อที่ระบุ]</asp:ListItem>
        <asp:ListItem >C/O บริษัท(แยกบิล) / C/O Company (Separate Bills)</asp:ListItem>
        <asp:ListItem >C/O บริษัท(แยกบิล) [เฉพาะรายชื่อที่ระบุ]</asp:ListItem>
        <asp:ListItem >วงเงิน C/O</asp:ListItem>
        </asp:RadioButtonList>
                                            &#160;<asp:TextBox ID="txtmpn_Credit" runat="server" MaxLength="10">
        </asp:TextBox>&nbsp;บาท
                                    </div>
        </td>
        </tr>
        <tr>
        <td>
        </td>
        <td>
        <asp:RequiredFieldValidator 
                                        ID="RequiredFieldValidator22" runat="server" 
                                        ControlToValidate="RDmrm_id" ErrorMessage="* Required"  Display="Dynamic" 
                                        ValidationGroup="AddpaymentData">
        </asp:RequiredFieldValidator>
        <div style="border:1px solid silver;padding-bottom:5px;margin-right:5px;">
        <div style="background-color:Silver;padding:3px 3px;">รับยาจากการตรวจสุขภาพ / Term of Receiving Medicine</div>
        <asp:RadioButtonList ID="RDmrm_id" runat="server" RepeatLayout="Flow">
        <asp:ListItem >ชำระเงินสด / Cash</asp:ListItem>
        <asp:ListItem >C/O บริษัท(รวมบิล) / C/O Company (Total Bills)</asp:ListItem>
        <asp:ListItem >C/O บริษัท(แยกบิล) / C/O Company (Separate Bills)</asp:ListItem>
        <asp:ListItem >วงเงิน C/O / Credit Limit C/O</asp:ListItem>
        </asp:RadioButtonList>
        </div>
        </td>
        </tr>
        <tr>
        <td>
        </td>
        <td>
        <asp:RequiredFieldValidator 
                                        ID="RequiredFieldValidator23" runat="server" 
                                        ControlToValidate="RDCoupon" ErrorMessage="* Required"  Display="Dynamic" 
                                        ValidationGroup="AddpaymentData">
        </asp:RequiredFieldValidator>
        <div style="width:320px;border:1px solid silver;padding-bottom:5px;">
        <div style="background-color:Silver;padding:3px 3px;">คูปองอาหาร / Meal Coupon</div>
        <asp:RadioButtonList ID="RDCoupon" runat="server" RepeatLayout="Flow">
        <asp:ListItem Value="I">ให้</asp:ListItem>
        <asp:ListItem Value="N">ไม่ให้</asp:ListItem>
        </asp:RadioButtonList>
        <br />


                                        &#160;<asp:TextBox ID="txtCouponRemark" runat="server" MaxLength="500" 
                                            Height="60px" TextMode="MultiLine" Width="98%">
        </asp:TextBox>
        </div>
        </td>
        </tr>
        <tr>
        <td>
        </td>
        <td>
        <asp:Button ID="btnAddPaymentType" runat="server" Text="Add" 
                                        ValidationGroup="AddpaymentData" onclick="btnAddPaymentType_Click" />
        <asp:HiddenField ID="HiddenPaymentRowID" runat="server" />
        <asp:HiddenField ID="HiddenPaymentRunning" runat="server" />
        </td>
        </tr>
        <tr>
        <td colspan="2">
        <asp:GridView ID="GridPaymentType" runat="server" AutoGenerateColumns="False" 
                HeaderStyle-Height="30px" onrowcommand="GridPaymentType_RowCommand" 
                SkinID="gvSkSearch">
        <Columns>
        <asp:TemplateField HeaderText="Type" ItemStyle-VerticalAlign="Top">
        <ItemTemplate>
        <center>
        <%# (DataBinder.Eval(Container.DataItem, "mst_name"))%>
        </center>
        </ItemTemplate>
        <ItemStyle VerticalAlign="Top" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Payment Type" ItemStyle-VerticalAlign="Top">
        <ItemTemplate>
        <center>
        <%# (DataBinder.Eval(Container.DataItem, "mpt_name"))%>
        <%# ((Convert.ToInt32(DataBinder.Eval(Container.DataItem, "tpa_mpt_credit")) == 0) ? "" : DataBinder.Eval(Container.DataItem, "tpa_mpt_credit", "[{0}]")) %>
        <%# (DataBinder.Eval(Container.DataItem, "tpa_mpt_remark")=="")?"":(DataBinder.Eval(Container.DataItem, "tpa_mpt_remark", "Remark:[{0}]"))%>
        </center>
        </ItemTemplate>
        <ItemStyle VerticalAlign="Top" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Billing Method" ItemStyle-VerticalAlign="Top">
        <ItemTemplate>
        <center>
        <%# (DataBinder.Eval(Container.DataItem, "mbm_name"))%>
        </center>
        </ItemTemplate>
        <ItemStyle VerticalAlign="Top" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Main program" ItemStyle-VerticalAlign="Top">
        <ItemTemplate>
        <center>
        <%# (DataBinder.Eval(Container.DataItem, "mpm_name"))%>
        </center>
        </ItemTemplate>
        <ItemStyle VerticalAlign="Top" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Check-up rate" ItemStyle-VerticalAlign="Top">
        <ItemTemplate>
        <center>
        <%# (DataBinder.Eval(Container.DataItem, "mpr_name"))%>
        </center>
        </ItemTemplate>
        <ItemStyle VerticalAlign="Top" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Options items as montioned in quatation" 
                ItemStyle-VerticalAlign="Top">
        <ItemTemplate>
        <center>
        <%# (DataBinder.Eval(Container.DataItem, "mpq_name"))%>
        <%# ((Convert.ToInt32(DataBinder.Eval(Container.DataItem, "tpa_mpq_credit")) == 0) ? "" : DataBinder.Eval(Container.DataItem, "tpa_mpq_credit", "[{0}]"))%>
        </center>
        </ItemTemplate>
        <ItemStyle VerticalAlign="Top" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Options items montioned not in quatation" 
                ItemStyle-VerticalAlign="Top">
        <ItemTemplate>
        <center>
        <%# (DataBinder.Eval(Container.DataItem, "mpn_name"))%>
        <%# ((Convert.ToInt32(DataBinder.Eval(Container.DataItem, "tpa_mpn_credit")) == 0) ? "" : DataBinder.Eval(Container.DataItem, "tpa_mpn_credit", "[{0}]"))%>
        </center>
        </ItemTemplate>
        <ItemStyle VerticalAlign="Top" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Term of Receiving Medicine" 
                ItemStyle-VerticalAlign="Top">
        <ItemTemplate>
        <center>
        <%# (DataBinder.Eval(Container.DataItem, "mrm_name"))%>
        </center>
        </ItemTemplate>
        <ItemStyle VerticalAlign="Top" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Meal Coupon" ItemStyle-VerticalAlign="Top">
        <ItemTemplate>
        <center>
        <%# (DataBinder.Eval(Container.DataItem, "coupon_name"))%>
        <%# (DataBinder.Eval(Container.DataItem, "tpa_coupon_remark") == "") ? "" : (DataBinder.Eval(Container.DataItem, "tpa_coupon_remark", "Remark:[{0}]"))%>
        </center>
        </ItemTemplate>
        <ItemStyle VerticalAlign="Top" />
        </asp:TemplateField>
        <asp:TemplateField ControlStyle-Width="40" HeaderStyle-Wrap="true" HeaderText="" 
                ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top">
        <ItemTemplate>
        <div style="display:inline-block; width:50px;">
        <asp:ImageButton ID="btnEditPaymentType" runat="server" CausesValidation="false" 
                CommandArgument='<%# Bind("RowID") %>' CommandName="EditFile" 
                ImageUrl="~/images/edit.png" Style="width: 20px; height: 20px;" />
        <asp:ImageButton ID="btnDelPaymentType" runat="server" CausesValidation="false" 
                CommandArgument='<%# Bind("RowID") %>' CommandName="DeleteFile" 
                ImageUrl="~/images/delete.png" OnClientClick="return msgconfirmDelete();" 
                Style="width: 20px; height: 20px;" />
        </div>
        </ItemTemplate>
        <controlstyle width="40px" />
        <HeaderStyle Wrap="True" />
        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="50px" />
        </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>


                                            There are currently no items in this table.

                                        </EmptyDataTemplate>
        <HeaderStyle Height="30px" />
        </asp:GridView>
        </td>
        </tr>
        </table>
        <script type="text/javascript">
            function showpaymentType2() {
                checkRDpamenttype("<%=RDPaymentType.ClientID%>", "<%=txtBillAmount.ClientID%>");
                checkRDpamenttype("<%=RDmpq_id.ClientID%>", "<%=txtmqp_credit.ClientID%>");
                checkRDpamenttype("<%=RDmpn_id.ClientID%>", "<%=txtmpn_Credit.ClientID%>");
            }

            showpaymentType2();
                        </script>
        <asp:Literal ID="littab5" runat="server">
        </asp:Literal>
        </ContentTemplate>
        </asp:UpdatePanel>
        </asp:Panel>
        
</ContentTemplate>
</asp:TabPanel>
        <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
        
        
        <HeaderTemplate>
            ข้อมูลตรวจสุขภาพ


        
</HeaderTemplate>
<ContentTemplate>
        <asp:Panel ID="PanelCheckupData" runat="server">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
        <table style="width: 850px;padding:5px;">
        <tr>
        <td>
        <div class="tab-content">
        <table width="850px" cellpadding="0" cellspacing="0">
        <tr>
        <td class="tabHeader">ข้อมูลการตรวจสุขภาพ</td>
        </tr>
        <tr>
        <td>
        <table cellpadding="0" cellspacing="0" width="850px">
        <tr>
        <td class="td-width">
        <div class="divtitleNameStyle">Request Doctor :</div>
        </td>
        <td>
        <div style="display:inline;">
        <asp:TextBox ID="txt_req_doc" runat="server" CssClass="textbox-width">
        </asp:TextBox>
        <span ID="waitinghtmlReqDoc1">
        </span>
        </div>
        </td>
        </tr>
        <tr>
        <td class="td-width">
        <div class="divtitleNameStyle">Request Doctor Cat. :</div>
        </td>
        <td align="left" valign="top">
        <asp:HiddenField ID="txtHiddenDocCate" runat="server" />
        <asp:DropDownList ID="dlDocCate" runat="server" ValidationGroup="group_doccate">
        </asp:DropDownList>
        <asp:Button ID="btnDoccate" runat="server" onclick="btnDoccate_Click" Text="+" 
                ValidationGroup="group_doccate" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" 
                ControlToValidate="dlDocCate" ErrorMessage="* Required" 
                ValidationGroup="group_doccate">
        </asp:RequiredFieldValidator>
        </td>
        </tr>
        <tr>
        <td>&#160;</td>
        <td>
        <table ID="tblDocCate" style="width:100%;">
        <tr class="headerStyle">
        <td style="width:20px;">No.</td>
        <td>Doctor Cat.</td>
        <td style="width:20px;">...</td>
        </tr>
        <asp:Repeater ID="RepeaterDocCate" runat="server" 
                onitemcommand="RepeaterDocCate_ItemCommand">
        <ItemTemplate>
        <tr>
        <td style="width:20px;">
        <%# Container.ItemIndex + 1 %>
        </td>
        <td>
        <asp:HiddenField ID="HiddenFieldDocCate" runat="server" 
                Value='<%# DataBinder.Eval(Container.DataItem, "mdc_id")%>' />
        <%# DataBinder.Eval(Container.DataItem, "mdc_tname")%>
        </td>
        <td style="width:20px;">
        <asp:ImageButton ID="ImgBtnDel" runat="server" CommandName="Del" 
                ImageUrl="~/images/delete.png" OnClientClick="return msgconfirmDelete();" />
        </td>
        </tr>
        </ItemTemplate>
        </asp:Repeater>
        </table>
        </td>
        </tr>
        <tr>
        <td class="td-width" valign="top">
        <div class="divtitleNameStyle">Check-up Location :</div>
        </td>
        <td>
        <asp:HiddenField ID="txtHiddenSite" runat="server" />
        <asp:DropDownList ID="dlSite" runat="server" ValidationGroup="group_chkup_loc">
        </asp:DropDownList>
        <asp:Button ID="btnsite" runat="server" onclick="btnsite_Click" Text="+" 
                ValidationGroup="group_chkup_loc" />
        <%--OnClientClick="funcAddSite()"--%>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" 
                ControlToValidate="dlSite" ErrorMessage="* Required" InitialValue="0" 
                ValidationGroup="group_chkup_loc">
        </asp:RequiredFieldValidator>
        </td>
        </tr>
        <tr>
        <td>
        </td>
        <td>
        <table ID="tblSite" style="width:100%;">
        <tr class="headerStyle">
        <td style="width:20px;">No.</td>
        <td>Check-up Location</td>
        <td style="width:20px;">...</td>
        </tr>
        <asp:Repeater ID="RepeaterSite" runat="server" 
                onitemcommand="RepeaterSite_ItemCommand">
        <ItemTemplate>
        <tr>
        <td style="width:20px;">
        <%# Container.ItemIndex + 1 %>
        </td>
        <td>
        <asp:HiddenField ID="HiddenFieldSite" runat="server" 
                Value='<%# DataBinder.Eval(Container.DataItem, "mcl_id")%>' />
        <%# DataBinder.Eval(Container.DataItem, "mcl_tname")%>
        </td>
        <td style="width:20px;">
        <asp:ImageButton ID="ImgBtnDel" runat="server" CommandName="Del" 
                ImageUrl="~/images/delete.png" OnClientClick="return msgconfirmDelete();" />
        </td>
        </tr>
        </ItemTemplate>
        </asp:Repeater>
        </table>
        </td>
        </tr>
        <tr>
        <td>
        </td>
        <td>
        <table border="0" cellspacing="2" style="width:100%;">
        <tr>
        <td class="td-width">
        <div class="divtitleNameStyle">Remark :</div>
        </td>
        <td>
        <asp:TextBox ID="txttcdLocation_remark" runat="server" CssClass="textbox-width" 
                MaxLength="500">
        </asp:TextBox>
        </td>
        </tr>
        </table>
        </td>
        </tr>
        <tr>
        <td class="td-width">
        <div class="divtitleNameStyle">
        <asp:Label ID="Label1" runat="server" Text="Address Send Result :">
        </asp:Label>
        </div>
        </td>
        <td>
        <asp:TextBox ID="txtResultAddress" runat="server" CssClass="textbox-width" 
                MaxLength="100">
        </asp:TextBox>
        </td>
        </tr>
        <tr>
        <td>&#160;</td>
        <td>
        <table width="100%">
        <tr>
        <td>
        <div class="divtitleNameStyle">
        <asp:Label ID="Label3" runat="server" Text="Sub-District :">
        </asp:Label>
        </div>
        </td>
        <td>
        <asp:TextBox ID="txtResultTumbon" runat="server" MaxLength="100">
        </asp:TextBox>
        </td>
        <td>
        <div class="divtitleNameStyle">
        <asp:Label ID="Label2" runat="server" Text="District :">
        </asp:Label>
        </div>
        </td>
        <td>
        <asp:TextBox ID="txtResultAumphur" runat="server" MaxLength="100">
        </asp:TextBox>
        </td>
        </tr>
        <tr>
        <td>
        <div class="divtitleNameStyle">
        <asp:Label ID="Label4" runat="server" Text="Province :">
        </asp:Label>
        </div>
        </td>
        <td>
        <asp:TextBox ID="txtResultProvince" runat="server" MaxLength="100">
        </asp:TextBox>
        </td>
        <td>
        <div class="divtitleNameStyle">
        <asp:Label ID="Label5" runat="server" Text="Post Code :">
        </asp:Label>
        </div>
        </td>
        <td>
        <asp:TextBox ID="txtResultPostCode" runat="server" MaxLength="100">
        </asp:TextBox>
        </td>
        </tr>
        </table>
        </td>
        </tr>
        <tr>
        <td class="td-width" valign="top">
        <div class="divtitleNameStyle">Medical Reports :</div>
        </td>
        <td>
        <div style="border:solid 1px silver;">
        <asp:CheckBoxList ID="CHMmmr_id" runat="server" RepeatLayout="Flow">
        <asp:ListItem>สมุดตรวจสุขภาพ</asp:ListItem>
        <asp:ListItem>Report fit to Off-Shore</asp:ListItem>
        <asp:ListItem>สรุปรวม / Total Report</asp:ListItem>
        <asp:ListItem>สำเนาตรวจสุขภาพให้บริษัทเก็บ 1 ชุด</asp:ListItem>
        <asp:ListItem>ใบรับรองแพทย์ 5 โรค</asp:ListItem>
        <asp:ListItem>ใบรับรองแพทย์ 6 โรค</asp:ListItem>
        <asp:ListItem>อื่นๆ</asp:ListItem>
        </asp:CheckBoxList>&#160;<asp:TextBox ID="txttcd_rep_remark" runat="server" 
                MaxLength="500" Width="545px">
        </asp:TextBox>
        </div>
        </td>
        </tr>
        <tr>
        <td>
        </td>
        <td>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
        <td colspan="2" style="width:370px;" valign="top">
        <div 
                style="float:left;border:1px solid silver;margin-bottom:5px;padding-bottom:5px;">
        <div style="background-color:Silver;padding:3px 3px;">Send Report Method</div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
        <td>
        <script type="text/javascript">
            function realYclick() {
                var radioButton = document.getElementById('<%=RDtcd_send_rep_realY.ClientID%>');
                if (radioButton.checked) {
                    var fh = document.getElementById('<%=RDtcd_send_rep_flagH.ClientID%>');
                    var fc = document.getElementById('<%=RDtcd_send_rep_flagC.ClientID%>');
                    fh.checked = true;
                    fh.checked = false;
                }
            }
                                                            </script>
        <fieldset style="width:150px;float:left;margin-left:10px;margin-right:10px;">
        <legend>ฉบับจริง</legend>
        <asp:RadioButton ID="RDtcd_send_rep_realY" runat="server" GroupName="sendrep" 
                Text="รับกลับ">
        </asp:RadioButton>
        <br />
        <asp:RadioButton ID="RDtcd_send_rep_realN" runat="server" GroupName="sendrep" 
                Text="ไม่รับกลับ">
        </asp:RadioButton>
        <br /> &#160;&#160;&#160;&#160; <asp:RadioButton ID="RDtcd_send_rep_flagH" runat="server" 
                GroupName="sendrep_sub" Text="ส่งบ้าน">
        </asp:RadioButton>
        <asp:RadioButton ID="RDtcd_send_rep_flagC" runat="server" GroupName="sendrep_sub" 
                Text="ส่งบริษัท">
        </asp:RadioButton>
        </fieldset>
        </td>
        <td>
        <fieldset style="width:150px;float:left;margin-left:10px;margin-right:10px;">
        <legend>สำเนา</legend>
        <asp:RadioButtonList ID="RDtcd_send_rep_copy" runat="server" RepeatLayout="Flow">
        <asp:ListItem Value="H">ส่งบ้าน</asp:ListItem>
        <asp:ListItem Value="C">ส่งบริษัท</asp:ListItem>
        <asp:ListItem Value="N">ไม่ต้องการ</asp:ListItem>
        </asp:RadioButtonList>
        </fieldset>
        </td>
        </tr>
        </table>
        </div>
        </td>
        </tr>
        <tr>
        <td>
        <div style="width:320px;float:left;margin-left:8px;border:1px solid silver;">
        <div style="background-color:Silver;padding:3px 3px;">เงื่อนไขการเข้ารับบริการ[สำหรับพนักงาน]</div>
        <asp:CheckBoxList ID="CHmcs_id_Employee" runat="server" RepeatLayout="Flow">
        <asp:ListItem>แสดงใบส่งตัว / จดหมายส่งตัว</asp:ListItem>
        <asp:ListItem>แสดงบัตรพนักงาน</asp:ListItem>
        <asp:ListItem>ตามรายชื่อจาก Email การตลาด</asp:ListItem>
        <asp:ListItem>ตามรายชื่อจากบริษัท</asp:ListItem>
        <asp:ListItem>ให้ผู้ป่วยเซ็นชื่อยืนยันการรับบริการ</asp:ListItem>
        <asp:ListItem>บัตรกำนัล</asp:ListItem>
        </asp:CheckBoxList>
        </div>
        </td>
        <td>
        <div style="width:320px;float:left;margin-left:8px;border:1px solid silver;">
        <div style="background-color:Silver;padding:3px 3px;">เงื่อนไขการเข้ารับบริการ[สำหรับผู้บริหาร]</div>
        <asp:CheckBoxList ID="CHmcs_id_Boss" runat="server" RepeatLayout="Flow">
        <asp:ListItem>แสดงใบส่งตัว / จดหมายส่งตัว</asp:ListItem>
        <asp:ListItem>แสดงบัตรพนักงาน</asp:ListItem>
        <asp:ListItem>ตามรายชื่อจาก Email การตลาด</asp:ListItem>
        <asp:ListItem>ตามรายชื่อจากบริษัท</asp:ListItem>
        <asp:ListItem>ให้ผู้ป่วยเซ็นชื่อยืนยันการรับบริการ</asp:ListItem>
        <asp:ListItem>บัตรกำนัล</asp:ListItem>
        </asp:CheckBoxList>
        </div>
        </td>
        </tr>
        </table>
        </td>
        </tr>
        </table>
        </td>
        </tr>
        <tr>
        <td class="tabHeader">ข้อมูลอื่นๆ</td>
        </tr>
        <tr>
        <td>
        <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
        <td >
        <div class="divtitleNameStyle">


                                                                Marketing Name :</div>
        </td>
        <td>
        <asp:TextBox ID="txtMktName" runat="server" ValidationGroup="mktdata">
        </asp:TextBox>
        <%--<asp:TextBox ID="txtMkt" runat="server" MaxLength="500" >
        </asp:TextBox>--%>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                ControlToValidate="txtMktName" ErrorMessage="* Required" 
                ValidationGroup="mktdata">
        </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
        <td>
        </td>
        </tr>
        <tr>
        <td class="td-width">
        <div class="divtitleNameStyle">Marketing Tel :</div>
        </td>
        <td>
        <asp:TextBox ID="txtMkt_Tel" runat="server" MaxLength="100">
        </asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                ControlToValidate="txtMkt_Tel" ErrorMessage="* Required" 
                ValidationGroup="mktdata">
        </asp:RequiredFieldValidator>
        </td>
        <td class="td-width">
        <div class="divtitleNameStyle">Marketing Fax :</div>
        </td>
        <td>
        <asp:TextBox ID="txtMkt_Fax" runat="server" MaxLength="100">
        </asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                ControlToValidate="txtMkt_Fax" ErrorMessage="* Required" 
                ValidationGroup="mktdata">
        </asp:RequiredFieldValidator>
        </td>
        </tr>
        <tr>
        <td 
            class="td-width">
        <div class="divtitleNameStyle">Marketing E-mail :</div>
        </td>
        <td colspan="3">
        <asp:HiddenField ID="txtHiddenContactMkt" runat="server">
        </asp:HiddenField>
        <asp:TextBox ID="txtMktEmail" runat="server" MaxLength="100">
        </asp:TextBox>
        <asp:Button ID="btnmarket" runat="server" onclick="btnmarket_Click" Text="+" 
                ValidationGroup="mktdata" />
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                ControlToValidate="txtMktEmail" Display="Dynamic" 
                ErrorMessage=" อีเมลล์ไม่ถูกต้อง" SetFocusOnError="True" 
                ValidationExpression="^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$" 
                ValidationGroup="mktdata">
        </asp:RegularExpressionValidator>
        </td>
        </tr>
        <tr>
        <td>
        </td>
        <td colspan="3">
        <table ID="Table2" border="0" cellpadding="0" cellspacing="0" 
                style="width:100%;border:1px solid Silver;">
        <tr class="headerStyle">
        <td style="width:20px;">No.</td>
        <td>ชื่อ-สกุล</td>
        <td>Contact Tel</td>
        <td>Contact Fax</td>
        <td>Contact E-mail</td>
        <td>...</td>
        </tr>
        <asp:Repeater ID="RepeaterContactMKT" runat="server" 
                onitemcommand="RepeaterContactMKT_ItemCommand">
        <ItemTemplate>
        <tr>
        <td>
        <%# Container.ItemIndex + 1 %>
        </td>
        <td>
        <%# DataBinder.Eval(Container.DataItem, "Name")%>
        </td>
        <td>
        <%# DataBinder.Eval(Container.DataItem,"Tel") %>
        </td>
        <td>
        <%# DataBinder.Eval(Container.DataItem,"Fax") %>
        </td>
        <td>
        <%# DataBinder.Eval(Container.DataItem,"Email") %>
        </td>
        <td>
        <asp:ImageButton ID="ImageBtnDel" runat="server" CommandName="Del" 
                ImageUrl="~/images/delete.png" OnClientClick="return msgconfirmDelete();" />
        </td>
        </tr>
        </ItemTemplate>
        </asp:Repeater>
        </table>
        </td>
        </tr>
        <tr>
        <td 
                                                    class="td-width" valign="top">
        <div class="divtitleNameStyle">Upload File :</div>
        <br />* ไฟล์สูงสุดไม่เกิน 3 mb <br />
        </td>
        <td  colspan="3">
        <asp:UpdatePanel ID="udpPalliativeDoc" runat="server">
        <ContentTemplate>
        <table cellpadding="0" cellspacing="0" class="infoLayout" style="width:97%;">
        <!-- Group Coordinator Data Record -->
        <tr>
        <td style="padding:5px;">
        <table width="100%">
        <tr>
        <td>
        <asp:UpdatePanel ID="updatePalliativeDocFile" runat="server" RenderMode="Block">
        <ContentTemplate>
        <table cellpadding="0" cellspacing="0" style="padding:0px;">
        <tr>
        <td class="textBold" style="background-color:White;">&#160;Attach file : <span 
                class="comment">
        <literal id="lbmsgbox" runat="server">
        </literal>
        </span>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" 
                ControlToValidate="AttachfileSite" 
                ErrorMessage="1. please select file.  | 2. select type.  | 3. click add file." 
                ValidationGroup="group_uploadfile_chekup">
        </asp:RequiredFieldValidator>
        </td>
        </tr>
        <tr>
        <td>
        <table border="0" cellpadding="0" cellspacing="3" width="100%">
        <tr>
        <td>
        <asp:FileUpload ID="AttachfileSite" runat="server" />
        <script type="text/javascript">
        </script>
        </td>
        <td>Type :</td>
        <td></td>
        <tr>
        <td>
        <asp:CheckBoxList ID="ch_typeAttachfile" runat="server" 
                RepeatDirection="Horizontal">
        </asp:CheckBoxList>
        </td>
        </tr>
            <tr>
                <td>
                    <asp:Button ID="btnAddFile" runat="server" CssClass="buttonstyle" 
                        OnClick="btnAddFile_Click" Text="Add" 
                        ValidationGroup="group_uploadfile_chekup" />
                    <%--OnClientClick="return GetFileSize('TabContainer1_TabPanel2_fileUpload');"--%>
                </td>
            </tr>
        </tr>
        </table>
        </td>
        </tr>
        <tr>
        <td>
        <div style="overflow-x: auto; overflow-y: hidden; width: 734px;">
        <table style="width:900px;">
        <tr>
        <td>
        <asp:GridView ID="gnvAttachFile" runat="server" AutoGenerateColumns="False" 
                HeaderStyle-Height="30px" onrowcommand="gnvAttachFile_RowCommand" 
                SkinID="gvSkSearch">
        <Columns>
        <asp:TemplateField HeaderText="File Name" ItemStyle-VerticalAlign="Top">
        <ItemTemplate>
        <asp:HyperLink ID="Hyperlink1" runat="server" 
                NavigateUrl='<%# Eval("attach_file", "{0}") %>' Target="_blank" 
                Text='<%# Bind("file_name")%>' />
        </ItemTemplate>
        <HeaderStyle HorizontalAlign="Center" Wrap="True" />
        <ItemStyle VerticalAlign="Top" Width="220px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Marketing">
        <ItemTemplate>
        <asp:HiddenField ID="mpf_idHiddenField_m" runat="server" 
                Value='<%# Bind("RowID") %>' />
        <asp:CheckBox ID="chkMarketing" runat="server" AutoPostBack="true" 
                Checked='<%# Bind("TypeMkt") %>' OnCheckedChanged="chkMKT_CheckedChanged" />
        </center>
        </ItemTemplate>
        <HeaderStyle HorizontalAlign="Center" Wrap="True" />
        <ItemStyle CssClass="txtAlignCenter" VerticalAlign="Top" Width="60px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="HPC">
        <ItemTemplate>
        <asp:HiddenField ID="mpf_idHiddenField" runat="server" 
                Value='<%# Bind("RowID") %>' />
        <asp:CheckBox ID="chkHPC" runat="server" AutoPostBack="true" 
                Checked='<%# Bind("TypeHPC") %>' OnCheckedChanged="chkHPC_CheckedChanged" />
        </center>
        </ItemTemplate>
        <HeaderStyle HorizontalAlign="Center" Wrap="True" />
        <ItemStyle CssClass="txtAlignCenter" VerticalAlign="Top" Width="60px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Collection">
        <ItemTemplate>
        <asp:HiddenField ID="mpf_idHiddenField_Co" runat="server" 
                Value='<%# Bind("RowID") %>' />
        <asp:CheckBox ID="chkCollection" runat="server" AutoPostBack="true" 
                Checked='<%# Bind("TypeCollection") %>' 
                OnCheckedChanged="chkCollection_CheckedChanged" />
        </center>
        </ItemTemplate>
        <HeaderStyle HorizontalAlign="Center" Wrap="True" />
        <ItemStyle CssClass="txtAlignCenter" VerticalAlign="Top" Width="60px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Contact Center">
        <ItemTemplate>
        <center>
        <asp:HiddenField ID="mpf_idHiddenField_Ct" runat="server" 
                Value='<%# Bind("RowID") %>' />
        <asp:CheckBox ID="chkContactCenter" runat="server" AutoPostBack="true" 
                Checked='<%# Bind("TypeContact") %>' 
                OnCheckedChanged="chkcontact_CheckedChanged" />
        </center>
        </ItemTemplate>
        <HeaderStyle HorizontalAlign="Center" Wrap="True" />
        <ItemStyle CssClass="txtAlignCenter" VerticalAlign="Top" Width="60px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Update Date" ItemStyle-VerticalAlign="Top">
        <ItemTemplate>
        <center>
        <%# String.Format("{0:dd/MM/yyyy}",(DataBinder.Eval(Container.DataItem, "UpdateDate")))%>
        </center>
        </ItemTemplate>
        <controlstyle width="40px" />
        <HeaderStyle HorizontalAlign="Center" Wrap="True" />
        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="85px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderStyle-Wrap="true" HeaderText="Update By" 
                ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top">
        <ItemTemplate>
        <center>
        <%# (DataBinder.Eval(Container.DataItem, "UpdateBy"))%>
        </center>
        </ItemTemplate>
        <controlstyle width="40px" />
        <HeaderStyle HorizontalAlign="Center" Wrap="True" />
        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="110px" />
        </asp:TemplateField>
        <asp:TemplateField ControlStyle-Width="40" HeaderStyle-Wrap="true" HeaderText="" 
                ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top">
        <ItemTemplate>
        <asp:ImageButton ID="btnDelAttacheFile" runat="server" CausesValidation="false" 
                CommandArgument='<%# Bind("RowID") %>' CommandName="DeleteFile" 
                ImageUrl="~/images/delete.png" OnClientClick="return msgconfirmDelete();" 
                Style="width: 20px; height: 20px;" />
        </ItemTemplate>
        <controlstyle width="40px" />
        <HeaderStyle HorizontalAlign="Center" Wrap="True" />
        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="20px" />
        </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>There are currently no items in this table.</EmptyDataTemplate>
        <HeaderStyle Height="30px" />
        </asp:GridView>
        </td>
        </tr>
        </table>
        </div>
        </td>
        </tr>
        </table>
        </ContentTemplate>
        <triggers>
        <asp:PostBackTrigger ControlID="btnAddFile" />
        </triggers>
        </asp:UpdatePanel>
        </td>
        </tr>
        </table>
        </td>
        </tr>
        </table>
        </ContentTemplate>
        </asp:UpdatePanel>
        </td>
        </tr>
        </table>
        </td>
        </tr>
        </table>
        </div>
        </td>
        </tr>
        </table>
        </ContentTemplate>
        </asp:UpdatePanel>
        <script type="text/javascript">

            function funcAddDocCate() {
                var docCate = document.getElementById("<%=dlDocCate.ClientID %>");
                if (docCate.selectedIndex == 0) { alert('ยังไม่ได้เลือก โว๊ยยย!!'); return false; }
                var chkSame = false;
                var rowCount = arrDocCate.length;
                for (i = 0; i < rowCount; i++) {
                    var arrDtl = arrDocCate[i];
                    if (arrDtl[0] == docCate.value) {
                        chkSame = true;
                        break;
                    }
                }
                if (chkSame == false) {
                    arrDocCate[arrDocCate.length] = [docCate.value.replace(",", "|x|"), docCate.options[docCate.selectedIndex].text.replace(",", "|x|")];
                    funcGenTblDocCate();
                    docCate.focus();
                }
            }
            function BindingDocCate() {
                var tbllabeldata = document.getElementById("<%=txtHiddenDocCate.ClientID %>");
                if (tbllabeldata != null && tbllabeldata.value != "") {
                    var splitdata = tbllabeldata.value.split("|*|");
                    for (var i = 0; i < splitdata.length; i++) {
                        var dr = splitdata[i].split(",");
                        if (dr[0] != "") {
                            arrDocCate[arrDocCate.length] = [dr[0], dr[1]];
                        }
                    }
                    funcGenTblDocCate();
                }
            }
            function funcGenTblDocCate() {
                var tbl = document.getElementById("tblDocCate");
                var rowCount = tbl.rows.length;

                for (i = rowCount - 1; i > 0; i--) {
                    tbl.deleteRow(i);
                }

                var arrCount = arrDocCate.length;
                var stra1 = "";
                for (i = 0; i < arrCount; i++) {
                    var row = tbl.insertRow(tbl.rows.length);
                    var cellNo = row.insertCell(0);
                    cellNo.innerHTML = (i + 1);

                    arrDtl = arrDocCate[i];
                    var arrDtlCount = arrDtl.length;
                    for (j = 0; j < arrDtlCount; j++) {
                        var cell = row.insertCell(j + 1);
                        cell.innerHTML = arrDtl[j].replace("|x|", ",");
                        if (j == 0) { cell.style.display = "none"; }
                    }

                    var cellBtn = row.insertCell(j + 1);
                    cellBtn.innerHTML = "<button type='button' onclick='funcDelDocCate(" + i + ")'>X</button>";
                    if (stra1 == "") {
                        var strdata = arrDocCate[i].toString();
                        stra1 = strdata;
                    } else {
                        var strdata = arrDocCate[i].toString();
                        stra1 += "|*|" + strdata;
                    }
                }
                var tbllabeldata = document.getElementById("<%=txtHiddenDocCate.ClientID %>"); //เอาค่าใส่ใน Text Hidden
                tbllabeldata.value = stra1;
            }

            function funcDelDocCate(index) {
                if (msgconfirmDelete() == true) {
                    arrDocCate.splice(index, 1);
                    funcGenTblDocCate();
                }
            }
        </script>
        <script type="text/javascript">

            function funcAddSite() {
                var site = document.getElementById("<%=dlSite.ClientID %>");
                if (site.selectedIndex == 0) { return false; }
                var chkSame = false;
                var rowCount = arrSite.length;
                for (i = 0; i < rowCount; i++) {
                    var arrDtl = arrSite[i];
                    if (arrDtl[0] == site.value) {
                        chkSame = true;
                        break;
                    }
                }
                if (chkSame == false) {

                    arrSite[arrSite.length] = [site.value.replace(",", "|x|"), site.options[site.selectedIndex].text.replace(",", "|x|")];
                    funcGenTblSite();
                    site.focus();
                }
            }
            function BindingSite() {
                var tbllabeldata = document.getElementById("<%=txtHiddenSite.ClientID %>");
                if (tbllabeldata != null && tbllabeldata.value != "") {
                    var splitdata = tbllabeldata.value.split("|*|");
                    for (var i = 0; i < splitdata.length; i++) {
                        var dr = splitdata[i].split(",");
                        if (dr[0] != "") {
                            arrSite[arrSite.length] = [dr[0], dr[1]];
                        }
                    }
                    funcGenTblSite();
                }
            }

            function funcGenTblSite() {
                var tbl = document.getElementById("tblSite");
                var rowCount = tbl.rows.length;

                for (i = rowCount - 1; i > 0; i--) {
                    tbl.deleteRow(i);
                }

                var arrCount = arrSite.length;
                var stra1 = "";
                for (i = 0; i < arrCount; i++) {
                    var row = tbl.insertRow(tbl.rows.length);
                    var cellNo = row.insertCell(0);
                    cellNo.innerHTML = (i + 1);

                    arrDtl = arrSite[i];
                    var arrDtlCount = arrDtl.length;
                    for (j = 0; j < arrDtlCount; j++) {
                        var cell = row.insertCell(j + 1);
                        cell.innerHTML = arrDtl[j].replace("|x|", ",");
                        if (j == 0) { cell.style.display = "none"; }
                    }

                    var cellBtn = row.insertCell(j + 1);
                    cellBtn.innerHTML = "<button type='button' onclick='funcDelSite(" + i + ")'>X</button>";
                    if (stra1 == "") {
                        var strdata = arrSite[i].toString();
                        stra1 = strdata;
                    } else {
                        var strdata = arrSite[i].toString();
                        stra1 += "|*|" + strdata;
                    }
                }
                var tbllabeldata = document.getElementById("<%=txtHiddenSite.ClientID %>"); //เอาค่าใส่ใน Text Hidden 
                tbllabeldata.value = stra1;
            }

            function funcDelSite(index) {
                if (msgconfirmDelete() == true) {
                    arrSite.splice(index, 1);
                    funcGenTblSite();
                }
            }

        </script>
        <asp:Literal ID="littab2script" runat="server"></asp:Literal>
        </asp:Panel>
        
</ContentTemplate>
</asp:TabPanel>
        <asp:TabPanel ID="TabPanel6" runat="server" HeaderText="TabPanel6">
        
        
        <HeaderTemplate>
Remark
</HeaderTemplate>
<ContentTemplate>
        <asp:Panel ID="PanelRemark" runat="server">
        <cc1:Editor ID="EditorReamrk" Height="500px" runat="server" />
        </asp:Panel>
        
</ContentTemplate>
</asp:TabPanel>
        <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="TabPanel3" Width="95%">
        
        
        <HeaderTemplate>
            แพ็คเกจ


        
</HeaderTemplate>
<ContentTemplate>
        <asp:Panel ID="PanelPackage" runat="server">
        <table style="padding:5px;">
        <tr>
        <td>
        <table cellpadding="0" cellspacing="0">
        <tr>
        <td >
            <div class="divtitleNameStyle">Company Name(Thai) : </div>
        </td>
        <td >
            <asp:Label ID="lbCompanyNameTH" runat="server" Text="-"></asp:Label>
        </td>
        <td rowspan="3" style="width:220px;">
            <fieldset style="width:170px; float:right;margin-left:2px;margin-right:2px;">
                <legend>Payment Type</legend>
                    <asp:RadioButtonList ID="tab3RDPaymentType" runat="server" RepeatLayout="Flow">
                    <asp:ListItem Value="1">วางบิล</asp:ListItem>
                    <asp:ListItem Value="2">ชำระเงินสด</asp:ListItem>
                    <asp:ListItem Value="3">วางบิลและชำระเงินสดเพิ่ม</asp:ListItem>
                    <asp:ListItem Value="4">วงเงิน</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:TextBox ID="txtLimitCredit" runat="server" MaxLength="10"></asp:TextBox>&#160;บาท <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="tab3RDPaymentType" ErrorMessage="* Required" 
                        ValidationGroup="AddPackage"></asp:RequiredFieldValidator>&#160;
             </fieldset>
        <script 
            type="text/javascript">
            function checkradiopamenttype() {

                var objrdtab3 = document.getElementById("<%=tab3RDPaymentType.ClientID%>");
                var objtxtlimit = document.getElementById("<%=txtLimitCredit.ClientID%>");

                var inputs = objrdtab3.getElementsByTagName('input');

                if (inputs[inputs.length - 1].checked) {
                    objtxtlimit.disabled = false;
                } else {
                    objtxtlimit.value = "";
                    objtxtlimit.disabled = true;
                }
            }
                                            </script>
        </td>
        </tr>
        <tr>
        <td>
        <div 
            class="divtitleNameStyle">Company Name(Eng) : </div>
        </td>
        <td >
        <asp:Label 
                ID="lbCompanyNameEng" runat="server" Text="-"></asp:Label>
        </td>
        </tr>
        <tr>
        <td>
        <div class="divtitleNameStyle">Order Set or Option : </div>
        </td>
        <td >
            <div style="display:inline;">
            <asp:TextBox ID="txtordersetOrOption" runat="server" Width="430px"></asp:TextBox>
            <span ID="waitinghtml1">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" 
                    ControlToValidate="txtordersetOrOption" ErrorMessage="*" 
                    ValidationGroup="AddPackage"></asp:RequiredFieldValidator>
            </span>
            </div>
        </td>
        </tr>
        <tr>
        <td>
            <div class="divtitleNameStyle">Order Type : </div>
        </td>
        <td >
            <table border="0" cellpadding="0" cellspacing="0" width="96%">
            <tr>
                <td class="style6">
                <asp:RadioButtonList ID="tab3RDOrderType" 
                        runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Selected="True" 
                            Value="Order Set">Order Set</asp:ListItem>
                <asp:ListItem Value="Option">Option</asp:ListItem>
                </asp:RadioButtonList>
                </td>
                <td 
                        class="td-width">
                <div class="divtitleNameStyle">Price :</div>
                </td>
                <td>
                <asp:TextBox 
                        ID="txtPricevalue" runat="server" Width="100px" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtPricevalue" 
                        ErrorMessage="*" ValidationGroup="AddPackage"></asp:RequiredFieldValidator>
                </td>
            </tr>
            </table>
        </td>
        </tr>
        <tr>
        <td>
        <div class="divtitleNameStyle">Date From : </div>
        </td>
        <td >
        <table 
            border="0" cellpadding="0" cellspacing="0" width="96%">
        <tr>
        <td class="style1">
        <asp:TextBox ID="txtDateFrom" Width="120px" runat="server" 
                MaxLength="10"></asp:TextBox>
        <asp:CalendarExtender 
                ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy" 
                TargetControlID="txtDateFrom">
        </asp:CalendarExtender>
        </td>
        <td 
                class="td-width">
        <div class="divtitleNameStyle">Date To :</div>
        </td>
        <td>
        <asp:TextBox 
                ID="txtDateTo" runat="server" Width="110px" MaxLength="10"></asp:TextBox>
        <asp:CalendarExtender 
                ID="CalendarExtender4" runat="server" Enabled="True" Format="dd/MM/yyyy" 
                TargetControlID="txtDateTo">
        </asp:CalendarExtender>
        </td>
        </tr>
        </table>
        </td>
        </tr>
        <tr>
        <td >
        <div class="divtitleNameStyle">Doctor Name : </div>
        </td>
        <td 
            >
        <table border="0" cellpadding="0" cellspacing="0" width="96%">
        <tr>
        <td style="width:210px;">
        <div style="display:inline;">
        <asp:TextBox ID="txtDoctorName" runat="server" Width="430px"></asp:TextBox>
        <span ID="waitinghtmlReqDoc2">&#160;&#160;&#160;&#160; </span>
        </div>
        </td>
        </tr>
        </table>
        </td>
        </tr>
        <tr>
        <td >
        <div class="divtitleNameStyle">Doctor Cate :</div>
        </td>
        <td >
        <asp:DropDownList 
                ID="dlDocCate2" runat="server">
        </asp:DropDownList>
        </td>
        </tr>
        <tr>
        <td class="style5">
        </td>
        <td>&#160;</td>
        </tr>
        <tr>
        <td valign="top" >
        <div class="divtitleNameStyle">การเตรียมตัวก่อนตรวจ : </div>
        </td>
        <td >
        <asp:DropDownList ID="DDmpy" runat="server" Width="300px">
        </asp:DropDownList>
        <br />
        <div style="display:inline-table;">
        <asp:TextBox ID="txtprepareCheck" runat="server" MaxLength="500" Width="300px"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" 
                ControlToValidate="txtprepareCheck" ErrorMessage="* Required" 
                ValidationGroup="Addremark"></asp:RequiredFieldValidator>&#160;<asp:Button 
                ID="btnAddRemarkmpy" runat="server" OnClick="btnAddRemarkmpy_Click" 
                Text="Add Remark" ValidationGroup="Addremark">
        </asp:Button>



                                                &#160;<asp:Label ID="lbmsgRemarktab3" 
                runat="server" CssClass="comment"></asp:Label>
        <br />
        <asp:Button ID="btntab3Add" runat="server" OnClick="btntab3Add_Click" Text="Add" 
                ValidationGroup="AddPackage" />
        <asp:Label ID="lbmsgAlertPackage" runat="server" CssClass="comment"></asp:Label>
        <asp:HiddenField ID="HDpackageid" runat="server" />
        </div>
        </td>
        </tr>
        </table>
        </td>
        </tr>
        </table>
        <asp:HiddenField ID="HiddenCountRowPackage" 
        runat="server" />
        <div class="horizontalscroll" style="width:100%;">
        <b>Package :</b><br />
        <asp:GridView ID="Gridtab3packagePlan" runat="server" 
                                            AutoGenerateColumns="False" OnRowCommand="Gridtab3packagePlan_RowCommand" 
                                            SkinID="gvSkSearch">
        <AlternatingRowStyle CssClass="alternateItemStyle">
        </AlternatingRowStyle>
        <Columns>
        <asp:TemplateField HeaderText="No.">
        <ItemTemplate>
        <center>
        <%# (DataBinder.Eval(Container.DataItem, "Rowid"))%>
        </center>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="30px">
        </ItemStyle>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Description">
        <ItemTemplate>
        <center>
        <%# (DataBinder.Eval(Container.DataItem, "tpd_order_desc"))%>
        </center>
        </ItemTemplate>
        <controlstyle width="200px">
        </controlstyle>
        <HeaderStyle Wrap="True">
        </HeaderStyle>
        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="250px">
        </ItemStyle>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Date From">
        <ItemTemplate>
        <center>
        <%# (DataBinder.Eval(Container.DataItem, "tpd_date_from", "{0:dd/MM/yyyy}"))%>
        </center>
        </ItemTemplate>
        <controlstyle width="50px">
        </controlstyle>
        <HeaderStyle Wrap="True">
        </HeaderStyle>
        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="50px">
        </ItemStyle>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Date To">
        <ItemTemplate>
        <center>
        <%# (DataBinder.Eval(Container.DataItem, "tpd_date_to", "{0:dd/MM/yyyy}"))%>
        </center>
        </ItemTemplate>
        <controlstyle width="50px">
        </controlstyle>
        <HeaderStyle Wrap="True">
        </HeaderStyle>
        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="50px">
        </ItemStyle>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Price">
        <ItemTemplate>
        <center>
        <%# (DataBinder.Eval(Container.DataItem, "tpd_price"))%>
        </center>
        </ItemTemplate>
        <controlstyle width="70px">
        </controlstyle>
        <HeaderStyle Wrap="True">
        </HeaderStyle>
        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="70px">
        </ItemStyle>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Payment">
        <ItemTemplate>
        <center>
        <%# (DataBinder.Eval(Container.DataItem, "mpt_name"))%>
        </center>
        </ItemTemplate>
        <controlstyle width="70px">
        </controlstyle>
        <HeaderStyle Wrap="True">
        </HeaderStyle>
        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="70px">
        </ItemStyle>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Amount">
        <ItemTemplate>
        <center>
        <%# ((Convert.ToInt32(DataBinder.Eval(Container.DataItem, "tpd_limit_credit")) == 0) ? "" : DataBinder.Eval(Container.DataItem, "tpd_limit_credit"))%>
        </center>
        </ItemTemplate>
        <controlstyle width="60px">
        </controlstyle>
        <HeaderStyle Wrap="True">
        </HeaderStyle>
        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="60px">
        </ItemStyle>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Prepare for Check">
        <ItemTemplate>
        <center>
        <%# (DataBinder.Eval(Container.DataItem, "mpy_name"))%>
        </center>
        </ItemTemplate>
        <controlstyle width="150px">
        </controlstyle>
        <HeaderStyle Wrap="True">
        </HeaderStyle>
        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="150px">
        </ItemStyle>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Remark">
        <ItemTemplate>
        <center>
        <%# (DataBinder.Eval(Container.DataItem, "tpd_mpy_remark"))%>
        </center>
        </ItemTemplate>
        <HeaderStyle Wrap="True">
        </HeaderStyle>
        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top">
        </ItemStyle>
        </asp:TemplateField>
        <asp:TemplateField>
        <ItemTemplate>
        <asp:ImageButton 
            ID="btnEditPackage" runat="server" CausesValidation="false" 
            CommandArgument='<%# Bind("id") %>' CommandName="EditFile" 
            ImageUrl="~/images/edit.png" Style="width: 20px; height: 20px;" />
        <asp:ImageButton 
            ID="btnDelPackage" runat="server" CausesValidation="false" 
            CommandArgument='<%# Bind("id") %>' CommandName="DeleteFile" 
            ImageUrl="~/images/delete.png" OnClientClick="return msgconfirmDelete();" 
            Style="width: 20px; height: 20px;" />
        </ItemTemplate>
        <controlstyle width="80px">
        </controlstyle>
        <HeaderStyle Wrap="True">
        </HeaderStyle>
        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="80px">
        </ItemStyle>
        </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>There are currently no items in this table.</EmptyDataTemplate>
        <HeaderStyle CssClass="headerStyle" 
                                                    Height="30px">
        </HeaderStyle>
        <RowStyle CssClass="itemStyle">
        </RowStyle>
        </asp:GridView>
        <br />
        <b>Option</b><br />
        <asp:GridView ID="GridView3PackagePlanItem" runat="server" 
                                            AutoGenerateColumns="False" OnRowCommand="Gridtab3packagePlan_RowCommand" 
                                            SkinID="gvSkSearch">
        <AlternatingRowStyle CssClass="alternateItemStyle">
        </AlternatingRowStyle>
        <Columns>
        <asp:TemplateField HeaderText="No.">
        <ItemTemplate>
        <center>
        <%# (DataBinder.Eval(Container.DataItem, "Rowid"))%>
        </center>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="30px">
        </ItemStyle>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Description">
        <ItemTemplate>
        <center>
        <%# (DataBinder.Eval(Container.DataItem, "tpd_order_desc"))%>
        </center>
        </ItemTemplate>
        <controlstyle width="200px">
        </controlstyle>
        <HeaderStyle Wrap="True">
        </HeaderStyle>
        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="250px">
        </ItemStyle>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Date From">
        <ItemTemplate>
        <center>
        <%# (DataBinder.Eval(Container.DataItem, "tpd_date_from", "{0:dd/MM/yyyy}"))%>
        </center>
        </ItemTemplate>
        <controlstyle width="50px">
        </controlstyle>
        <HeaderStyle Wrap="True">
        </HeaderStyle>
        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="50px">
        </ItemStyle>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Date To">
        <ItemTemplate>
        <center>
        <%# (DataBinder.Eval(Container.DataItem, "tpd_date_to", "{0:dd/MM/yyyy}"))%>
        </center>
        </ItemTemplate>
        <controlstyle width="50px">
        </controlstyle>
        <HeaderStyle Wrap="True">
        </HeaderStyle>
        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="50px">
        </ItemStyle>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Price">
        <ItemTemplate>
        <center>
        <%# (DataBinder.Eval(Container.DataItem, "tpd_price"))%>
        </center>
        </ItemTemplate>
        <controlstyle width="70px">
        </controlstyle>
        <HeaderStyle Wrap="True">
        </HeaderStyle>
        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="70px">
        </ItemStyle>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Payment">
        <ItemTemplate>
        <center>
        <%# (DataBinder.Eval(Container.DataItem, "mpt_name"))%>
        </center>
        </ItemTemplate>
        <controlstyle width="70px">
        </controlstyle>
        <HeaderStyle Wrap="True">
        </HeaderStyle>
        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="70px">
        </ItemStyle>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Amount">
        <ItemTemplate>
        <center>
        <%# ((Convert.ToInt32(DataBinder.Eval(Container.DataItem, "tpd_limit_credit")) == 0) ? "" : DataBinder.Eval(Container.DataItem, "tpd_limit_credit"))%>
        </center>
        </ItemTemplate>
        <controlstyle width="60px">
        </controlstyle>
        <HeaderStyle Wrap="True">
        </HeaderStyle>
        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="60px">
        </ItemStyle>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Prepare for Check-up">
        <ItemTemplate>
        <center>
        <%# (DataBinder.Eval(Container.DataItem, "mpy_name"))%>
        </center>
        </ItemTemplate>
        <controlstyle width="150px">
        </controlstyle>
        <HeaderStyle Wrap="True">
        </HeaderStyle>
        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="150px">
        </ItemStyle>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Remark">
        <ItemTemplate>
        <center>
        <%# (DataBinder.Eval(Container.DataItem, "tpd_mpy_remark"))%>
        </center>
        </ItemTemplate>
        <HeaderStyle Wrap="True">
        </HeaderStyle>
        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top">
        </ItemStyle>
        </asp:TemplateField>
        <asp:TemplateField>
        <ItemTemplate>
        <asp:ImageButton 
            ID="btnEditPackage" runat="server" CausesValidation="false" 
            CommandArgument='<%# Bind("id") %>' CommandName="EditFile" 
            ImageUrl="~/images/edit.png" Style="width: 20px; height: 20px;" />
        <asp:ImageButton 
            ID="btnDelPackage" runat="server" CausesValidation="false" 
            CommandArgument='<%# Bind("id") %>' CommandName="DeleteFile" 
            ImageUrl="~/images/delete.png" OnClientClick="return msgconfirmDelete();" 
            Style="width: 20px; height: 20px;" />
        </ItemTemplate>
        <controlstyle width="80px">
        </controlstyle>
        <HeaderStyle Wrap="True">
        </HeaderStyle>
        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="80px">
        </ItemStyle>
        </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>There are currently no items in this table.</EmptyDataTemplate>
        <HeaderStyle CssClass="headerStyle" 
                                                    Height="30px">
        </HeaderStyle>
        <RowStyle CssClass="itemStyle">
        </RowStyle>
        </asp:GridView>

        </div>
        <script type="text/javascript">
            function showpaymentType() {
                checkradiopamenttype();
            }
                  </script>
        <asp:Literal ID="littab3script" runat="server"></asp:Literal>
        </asp:Panel>
        
</ContentTemplate>
</asp:TabPanel>
        <asp:TabPanel ID="TabPanel4" runat="server" HeaderText="TabPanel4">
        
        
        <HeaderTemplate>
            รายชื่อผู้ตรวจ


    
</HeaderTemplate>
<ContentTemplate>
        <asp:Panel ID="PanelNameCheckup" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <table style="width: 850px;padding:5px;">
        <tr>
        <td class="tabHeader" colspan="2">รายชื่อผู้ตรวจ</td>
        </tr>
        <tr>
        <td class="td-width">
        <div class="divtitleNameStyle">Search</div>
        </td>
        <td>
        <asp:TextBox ID="txtsearch" Width="250px" runat="server" 
                Placeholder="Search by First Name/Last Name"></asp:TextBox>
        <asp:Button ID="btnsearch"  runat="server" Text="Search" 
                             onclick="btnsearch_Click" />
        </td>
        </tr>
        <tr>
        <td class="td-width">
        <div class="divtitleNameStyle">Company Name (Thai) :</div>
        </td>
        <td>
        <asp:Label ID="lblnamechk_compname_th" runat="server">-</asp:Label>
        </td>
        </tr>
        <tr>
        <td class="td-width">
        <div class="divtitleNameStyle">Company Name (Eng) :</div>
        </td>
        <td>
        <asp:Label ID="lblnamechk_compname_en" runat="server">-</asp:Label>
        </td>
        </tr>
        <tr>
        <td valign="top">
        <div class="divtitleNameStyle">Select File : </div>
        </td>


        <td>
        <asp:FileUpload ID="FileUploadExcelContactCheck" runat="server" />
        <asp:Button ID="btnImportExcelContactCheck" runat="server" 
                onclick="btnImportExcelContactCheck_Click" Text="Import File" />
        <asp:Label ID="lblcheckfile" runat="server" ForeColor="Red" 
                Text="please select excel file." Visible="False">
        </asp:Label>
        <br />
        <div style="width:600px;margin-top:10px;">
        <asp:Button ID="btnAddNewData" runat="server" onclick="btnAddNewData_Click" 
                Text="Add New" />
        <asp:HiddenField ID="HiddenFieldCountClick" runat="server" Value="show" />
        <asp:HiddenField ID="HiddenFieldStatus" runat="server" Value="add" />
        <asp:HiddenField ID="HiddenField_index" runat="server" />
        <asp:Panel ID="PanelAddNewNameCheck" runat="server" Visible="false">
        <table style="width:680px;">
        <tr>
        <td>Legal</td>
        <td colspan="3">
        <asp:TextBox ID="txtnamechk_legal" runat="server" Width="350px">
        </asp:TextBox>
        </td>
        </tr>
        <tr>
        <td>Company Name</td>
        <td colspan="3">
        <asp:TextBox ID="txtnamechk_compname" runat="server" Width="350px">
        </asp:TextBox>
        </td>
        </tr>
        <tr>
        <td>Employee ID</td>
        <td style="width:220px;">
        <asp:TextBox ID="txtempid" runat="server" MaxLength="20">
        </asp:TextBox>
        </td>
        <td>Personal ID</td>
        <td style="width:220px;">
        <asp:TextBox ID="txtpersonalid" runat="server" MaxLength="13">
        </asp:TextBox>
        </td>
        </tr>
        <tr>
        <td>HN</td>
        <td>
        <asp:TextBox ID="txthn" runat="server" MaxLength="20">
        </asp:TextBox>
        </td>
        <td>
        </td>
        <td>
        </td>
        </tr>
        <tr>
        <td>Title Name</td>
        <td>
        <asp:TextBox ID="txttitlename" runat="server" MaxLength="50">
        </asp:TextBox>
        </td>
        <td>
        </td>
        <td>
        </td>
        </tr>
        <tr>
        <td>First Name</td>
        <td>
        <asp:TextBox ID="txtfname" runat="server" MaxLength="50">
        </asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" 
                ControlToValidate="txtfname" ErrorMessage="* " ValidationGroup="addcuscheck">
        </asp:RequiredFieldValidator>
        </td>
        <td>Last Name</td>
        <td>
        <asp:TextBox ID="txtlname" runat="server" MaxLength="50">
        </asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" 
                ControlToValidate="txtlname" ErrorMessage="* " ValidationGroup="addcuscheck">
        </asp:RequiredFieldValidator>
        </td>
        </tr>
        <tr>
        <td>Sex</td>
        <td>
        <asp:RadioButton ID="rdoM" runat="server" Checked="True" GroupName="sex" Text="M" />
        <asp:RadioButton ID="rdoF" runat="server" GroupName="sex" Text="F" />
        </td>
        <td>Position</td>
        <td>
        <asp:TextBox ID="txtposition" runat="server" MaxLength="100">
        </asp:TextBox>
        </td>
        </tr>
        <tr>
        <td>Site</td>
        <td>
        <asp:TextBox ID="txtsite" runat="server" MaxLength="100">
        </asp:TextBox>
        </td>
        <td>Department</td>
        <td>
        <asp:TextBox ID="txtdept" runat="server" MaxLength="100">
        </asp:TextBox>
        </td>
        </tr>
        <tr>
        <td>Date of Birth</td>
        <td>
        <asp:TextBox ID="txtDOB" runat="server" MaxLength="10">
        </asp:TextBox>
        <asp:CalendarExtender ID="txtDOB_CalendarExtender" runat="server" 
                Format="dd/MM/yyyy" TargetControlID="txtDOB">
        </asp:CalendarExtender>
        </td>
        <td>Age</td>
        <td>
        <asp:TextBox ID="txtage" runat="server" MaxLength="10">
        </asp:TextBox>
        </td>
        </tr>
        <tr>
        <td>Program</td>
        <td>
        <asp:TextBox ID="txtprogram" runat="server" MaxLength="300">
        </asp:TextBox>
        </td>
        <td>Option</td>
        <td>
        <asp:TextBox ID="txtoption" runat="server" MaxLength="300">
        </asp:TextBox>
        </td>
        </tr>
        <tr>
        <td>Appoint Date/Time</td>
        <td colspan="3">
        <asp:TextBox ID="txtAppointDate" runat="server">
        </asp:TextBox>
        <asp:CalendarExtender ID="txtAppointDate_CalendarExtender" runat="server" 
                Format="dd/MM/yyyy" TargetControlID="txtAppointDate">
        </asp:CalendarExtender>
        <asp:TextBox ID="txtAppointTime" runat="server">
        </asp:TextBox>
        <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureName="en-US" 
                Mask="99:99" MaskType="Time" TargetControlID="txtAppointTime">
        </asp:MaskedEditExtender>
        </td>
        </tr>
        <tr>
        <td>Remark</td>
        <td colspan="3">
        <asp:TextBox ID="txtaddremark" runat="server" MaxLength="300" Width="100%">
        </asp:TextBox>
        </td>
        </tr>
        <tr>
        <td colspan="4">
        <asp:Button ID="btnadd" runat="server" onclick="btnadd_Click" Text=" Save" 
                ValidationGroup="addcuscheck" />
        </td>
        </tr>
        </table>
        </asp:Panel>
        </div>
        </td>
        </tr>
        <tr>
        <td colspan="2">
        <div class="horizontalscroll">
        <table style="width:2020px;" cellpadding="2" cellspacing="2">
        <tr class="headerStyle" >
        <td style="width:50px;">...</td>
        <td style="width:20px;">No</td>
        <td style="width:100px;">Employee ID</td>
        <td style="width:100px;">Title Name</td>
        <td style="width:100px;">First Name</td>
        <td style="width:100px;">Last Name</td>
        <td style="width:100px;">Program</td>
        <td style="width:100px;">Option</td>
        <td style="width:400px;">Remark</td>
        <td style="width:100px;">Legal</td>
        <td style="width:100px;">Company Name</td>
        <td style="width:100px;">Personal ID</td>
        <td style="width:100px;">HN</td>
        <td style="width:100px;">Sex(M/F)</td>
        <td style="width:100px;">Site</td>
        <td style="width:100px;">Department</td>
        <td style="width:100px;">Position</td>
        <td style="width:100px;">Date Of Birth</td>
        <td style="width:100px;">Age</td>
        <td style="width:150px;">Appoint Date/Time</td>
        </tr>
        <asp:Repeater ID="RepeaterPatient" runat="server" 
                                    onitemcommand="RepeaterPatient_ItemCommand">
        <ItemTemplate>
        <tr>
        <td style="width:50px;">
        <asp:ImageButton ID="ImgEdit" runat="server" 
                CommandArgument='<%# DataBinder.Eval(Container.DataItem,"tnc_id") %>' 
                CommandName="edit" Height="18px" ImageUrl="~/images/edit.png" Width="18px" />
        <asp:ImageButton ID="ImgDel" runat="server" 
                CommandArgument='<%# DataBinder.Eval(Container.DataItem,"tnc_id") %>' 
                CommandName="del" Height="18px" ImageUrl="~/images/delete.png" 
                OnClientClick="return msgconfirmDelete();" Width="18px" />
        </td>
        <td style="width:20px;">
        <asp:Label ID="lblno" runat="server" Text="<%# Container.ItemIndex + 1 %>">
        </asp:Label>
        </td>
        <td style="width:100px;">
        <asp:Label ID="lblF2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"tnc_emp_id") %>'>
        </asp:Label>
        </td>
        <td style="width:100px;">
        <asp:Label ID="lblF5" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"tnc_title_name") %>'>
        </asp:Label>
        </td>
        <td style="width:100px;">
        <asp:Label ID="lblF6" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"tnc_fname") %>'>
        </asp:Label>
        </td>
        <td style="width:100px;">
        <asp:Label ID="lblF7" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"tnc_lname") %>'>
        </asp:Label>
        </td>
        <td style="width:100px;">
        <asp:Label ID="lblF12" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"tnc_program") %>'>
        </asp:Label>
        </td>
        <td style="width:100px;">
        <asp:Label ID="lblF13" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"tnc_option") %>'>
        </asp:Label>
        </td>
        <td style="width:400px;">
        <asp:Label ID="lblF15" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"tnc_remark") %>'>
        </asp:Label>
        </td>
        <td style="width:100px;">
        <asp:HiddenField ID="HiddenField_tcn_id" Value='<%# DataBinder.Eval(Container.DataItem,"tnc_id") %>' runat="server" />
        <asp:Label ID="lblF0" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"tnc_legal") %>'>
        </asp:Label>
        </td>
        <td style="width:100px;">
        <asp:Label ID="lblF1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"tnc_company_name") %>'>
        </asp:Label>
        </td>
        <td style="width:100px;">
        <asp:Label ID="lblF3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"tnc_personal_id") %>'>
        </asp:Label>
        </td>
        <td style="width:100px;">
        <asp:Label ID="lblF4" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"tnc_hn") %>'>
        </asp:Label>
        </td>
        
        <td style="width:100px;">
        <asp:Label ID="lblF8" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"tnc_gender") %>'>
        </asp:Label>
        </td>
        <td style="width:100px;">
        <asp:Label ID="lblsite" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"tnc_site") %>'>
        </asp:Label>
        </td>
        <td style="width:100px;">
        <asp:Label ID="lbldept" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"tnc_department") %>'>
        </asp:Label>
        </td>
        <td style="width:100px;">
        <asp:Label ID="lblF9" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"tnc_position") %>'>
        </asp:Label>
        </td>
        <td style="width:100px;">
        <asp:Label ID="lblF10" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"tnc_dob") == null ? "" : DataBinder.Eval(Container.DataItem,"tnc_dob","{0:dd/MM/yyyy}") %>'>
        </asp:Label>
        </td>
        <td style="width:100px;">
        <asp:Label ID="lblF11" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"tnc_age") %>'>
        </asp:Label>
        </td>
        <td style="width:150px;">
        <asp:Label ID="lblF14" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"tnc_appoint_date") %>'>
        </asp:Label>
        </td>
        </tr>
        </ItemTemplate>
        </asp:Repeater>
        </table>
        </div>
        </td>
        </tr>
        </table>
        </ContentTemplate>
        <Triggers>
        <asp:PostBackTrigger ControlID="btnImportExcelContactCheck" >
        </asp:PostBackTrigger>
        </Triggers>
        </asp:UpdatePanel>
        </asp:Panel>
        
</ContentTemplate>
</asp:TabPanel>
        </asp:TabContainer>
        <asp:Button ID="btnSave" runat="server"  Text="Save" 
            ValidationGroup="SaveData" onclick="btnSave_Click" 
            style="width: 42px; height: 26px" />
        <script  type="text/javascript">
            function msgconfirmDelete() {
                if (confirm('  Do you want delete data !!!  ')) {
                    return true;
                } else {
                    return false;
                }
            }
            // function สำหรับ abjust HeightPage in Iframe
            function alertSize() {
                var myHeight = 0;
                if (typeof (parent.window.innerWidth) == 'number') {
                    //Non-IE
                    myHeight = parent.window.innerHeight;
                } else if (parent.document.documentElement && (parent.document.documentElement.clientWidth || parent.document.documentElement.clientHeight)) {
                    //IE 6+ in 'standards compliant mode'
                    myHeight = parent.document.documentElement.clientHeight;
                } else if (parent.document.body && (parent.document.body.clientWidth || parent.document.body.clientHeight)) {
                    //IE 4 compatible
                    myHeight = parent.document.body.clientHeight;
                }
                //window.alert( 'Height = ' + myHeight );
                return myHeight;
            }

            function AssignFrameHeight() {
                var frameHeight1 = getIframeHeight('frm2');
                var frameHeight2 = $(document.body).height();


                if ($(document.body)[0]) {
                    if ($(document.body)[0].bottomMargin)
                        frameHeight2 += Number($(document.body)[0].bottomMargin);
                    if ($(document.body)[0].topMargin)
                        frameHeight2 += Number($(document.body)[0].topMargin);
                }
                if (frameHeight1 > frameHeight2) {
                    parent.document.getElementById("t2").height = (frameHeight1 + 50) + "px";
                    parent.document.getElementById("ctl00_ContentPlaceHolder_body_frm2").height = (frameHeight1 + 50) + "px";
                } else {
                    parent.document.getElementById("t2").height = (frameHeight2 + 50) + "px";
                    parent.document.getElementById("ctl00_ContentPlaceHolder_body_frm2").height = (frameHeight2 + 50) + "px";
                }
            }

            function getIframeHeight(iframeName) {
                var iframeEl = parent.document.getElementById ?
                                parent.document.getElementById(iframeName)
                                : parent.document.all
                                  ? parent.document.all[iframeName]
                                  : null;
                if (iframeEl) {
                    iframeEl.style.height = "auto"; // helps resize (for some) if new doc shorter than previous
                    var h = alertSize();
                    return h;
                }
            }
            //            AssignFrameHeight();
        </script>
        <script type="text/javascript">
            function pageLoad() {



                jq("#<%=txt_payor.ClientID %>").autocomplete({
                    source: function (request, response) {
                        var obj1 = document.getElementById("waitingpayor");
                        obj1.innerHTML = "<img src='Images/wait/ajax-loader.gif' height='14px' />";
                        $.ajax({
                            url: "frmSearchCompany.aspx/AutoCompletePayor",
                            data: "{ 'txt': '" + document.getElementById("<%=txt_payor.ClientID%>").value + "' }",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            dataFilter: function (data) { return data; },
                            success: function (data) {
                                response(data.d);
                                obj1.innerHTML = "&nbsp;&nbsp;&nbsp;&nbsp;";
                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                alert(textStatus + " " + errorThrown);
                            }
                        });
                    },
                    minLength: 2
                });

                jq("#<%=txtComNameTH.ClientID %>").autocomplete({
                    source: function (request, response) {
                        //var obj1 = document.getElementById("waitingpayor");
                        //obj1.innerHTML = "<img src='Images/wait/ajax-loader.gif' height='14px' />";
                        $.ajax({
                            url: "frmSearchCompany.aspx/GetMasterCompanyListTh",
                            data: "{ 'cname': '" + document.getElementById("<%=txtComNameTH.ClientID%>").value + "' }",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            dataFilter: function (data) { return data; },
                            success: function (data) {
                                response(data.d);

                                //obj1.innerHTML = "&nbsp;&nbsp;&nbsp;&nbsp;";
                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                alert(textStatus + " " + errorThrown);
                            }
                        });
                    },
                    minLength: 2
                });


                jq("#<%=txtComNameEng.ClientID %>").autocomplete({
                    source: function (request, response) {
                        //var obj1 = document.getElementById("waitingpayor");
                        //obj1.innerHTML = "<img src='Images/wait/ajax-loader.gif' height='14px' />";
                        $.ajax({
                            url: "frmSearchCompany.aspx/GetMasterCompanyListEng",
                            data: "{ 'cname': '" + document.getElementById("<%=txtComNameEng.ClientID %>").value + "' }",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            dataFilter: function (data) { return data; },
                            success: function (data) {
                                response(data.d);
                                //obj1.innerHTML = "&nbsp;&nbsp;&nbsp;&nbsp;";
                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                alert(textStatus + " " + errorThrown);
                            }
                        });
                    },
                    minLength: 2
                });

                jq("#<%=txt_req_doc.ClientID %>").autocomplete({
                    source: function (request, response) {
                        var obj1 = document.getElementById("waitinghtmlReqDoc1");
                        obj1.innerHTML = "<img src='Images/wait/ajax-loader.gif' height='14px' />";
                        jQuery.ajax({
                            type: "post",
                            url: 'GetDataTrakCare.ashx',
                            data: 'type=req_doc&param1=' + document.getElementById("<%=txt_req_doc.ClientID %>").value,
                            success: function (data) {
                                if (data.length > 0) {
                                    response(data.split("<,>"))
                                }

                                obj1.innerHTML = "";
                            },
                            fail: function (msg) { }
                        });
                    },
                    minLength: 2
                });
                // ------- Package Doctor Name        
                jq("#<%=txtDoctorName.ClientID %>").autocomplete({
                    source: function (request, response) {
                        var obj1 = document.getElementById("waitinghtmlReqDoc2");
                        obj1.innerHTML = "<img src='Images/wait/ajax-loader.gif' height='14px' />";
                        jQuery.ajax({
                            type: "post",
                            url: 'GetDataTrakCare.ashx',
                            data: 'type=req_doc&param1=' + document.getElementById("<%=txtDoctorName.ClientID %>").value,
                            success: function (data) {
                                if (data.length > 0) {
                                    response(data.split("<,>"))
                                }
                                obj1.innerHTML = "";
                            },
                            fail: function (msg) { }
                        });
                    },
                    minLength: 2
                });
                // ----------- Package  Order set Or Option ---
                jq("#<%=txtordersetOrOption.ClientID %>").autocomplete({
                    source: function (request, response) {
                        var obj1 = document.getElementById("waitinghtml1");
                        obj1.innerHTML = "<img src='Images/wait/ajax-loader.gif' height='14px' />";
                        jQuery.ajax({
                            type: "post",
                            url: 'GetDataTrakCare.ashx',
                            data: 'type=ordersetall&input=' + document.getElementById("<%=txtordersetOrOption.ClientID %>").value,
                            success: function (data) {
                                if (data.length > 0) {
                                    response(data.split("<,>"))
                                }
                                obj1.innerHTML = "";
                            },
                            fail: function (msg) { }
                        });
                    },
                    minLength: 2
                });
                //-------------  -------------

            }
        </script>
        </form>
        </body>
</html>