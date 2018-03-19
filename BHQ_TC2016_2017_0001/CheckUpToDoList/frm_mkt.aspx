<%@ Page Title="" Language="C#" MasterPageFile="~/HeadToDoListNew.Master" AutoEventWireup="true" CodeBehind="frm_mkt.aspx.cs" Inherits="CheckUpToDoList.WebForm1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_body" runat="server">
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
    <link rel="Stylesheet" href="style/Style.css" />
    <link rel="Stylesheet" href="style/smoothness/jquery-ui-1.10.3.custom.css" />
    <script type="text/javascript" src="js/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="js/jquery-ui-1.10.3.custom.js"></script>
	<script type="text/javascript">
	    $(function () {
	        $("#tabs").tabs();
	    });
	    function tabshow(m) {
	        var tab1 = document.getElementById("tabsubs-1");
	        var tab2 = document.getElementById("tabsubs-2");
	        var ta1 = document.getElementById("ta1");
	        var ta2 = document.getElementById("ta2");
	        if (m == 1) {
	            tab1.style.display = "";
	            tab2.style.display = "none";
	            ta1.setAttribute("class", "tabshow");
	            ta2.setAttribute("class", "tabClose");
            }
	        else {
	            tab1.style.display = "none";
	            tab2.style.display = "";
	            ta1.setAttribute("class", "tabClose");
	            ta2.setAttribute("class", "tabshow");
	        }
	    }
	</script>    
    <asp:ToolkitScriptManager runat="server" ID="ToolkitScriptManager1" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ScriptMode="Debug" CombineScripts="false" >
    </asp:ToolkitScriptManager>

    <div id="tabs" style="width:100%;border:1px solid silver;">
	    <ul>
		    <li><a href="#tabs-1">ข้อมูลบริษัท</a></li>
            <li><a href="#tabs-2">รายชื่อผู้ตรวจ</a></li>
	    </ul>
	    <div id="tabs-1">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td  colspan="4" class="td-width" style="background-color:Silver;font-weight:bold;font-size:12px;vertical-align:middle; height:25px; ">
                    <div class="tabshow" id="ta1"><a href="javascript:tabshow(1);" style="text-decoration:none;">1.ข้อมูลบริษัท</a></div> 
                    <div class="tabClose" id="ta2"><a href="javascript:tabshow(2);" style="text-decoration:none;">2.ข้อมูลการตรวจสุขภาพ</a></div>
                </td>
            </tr>
            <tr>
                <td colspan="4" >
<div id="tabsubs-1" style="width:100%;">
                        <table width="100%" cellpadding="0" cellspacing="0">                                
            <tr>
                <td class="td-width"><div style="background-color:Silver;padding:4px 4px;">Company Code :</div></td>
                <td colspan="3">
                    <asp:TextBox ID="TextBox37" CssClass="textbox-width" runat="server"></asp:TextBox>
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
            </table>
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
            <tr>
                <td class="td-width" ><div style="background-color:Silver;padding:4px 4px;">Contact Person :</td>
                <td colspan="3">
                    <asp:TextBox ID="TextBox8" runat="server" CssClass="textbox-width" 
                       ></asp:TextBox>
                </td>
        </tr>
        <tr>
                <td class="td-width" ><div style="background-color:Silver;padding:4px 4px;">Contact Person Tel :</div></td>
                <td>
                    <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
                </td>
                <td class="td-width"><div style="background-color:Silver;padding:4px 4px;">Contact Person Fax :</div></td>
                <td>
                    <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox>
                </td>
        </tr>
        <tr>
                <td class="td-width" ><div style="background-color:Silver;padding:4px 4px;">Contact Person E-mail :</div></td>
                <td colspan="3">
                    <asp:TextBox ID="TextBox11" runat="server" Width="280px"></asp:TextBox>
                    <asp:Button ID="Button2" runat="server" Font-Bold="True" style="height: 22px" 
                        Text="+" />
                </td>
        </tr>
        <tr>
                <td></td>
                <td  colspan="3">
                                <asp:GridView ID="GridView1" ShowHeaderWhenEmpty="true" Width="100%" runat="server" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="ชื่อ-สกุล" HeaderStyle-Width="40%" HeaderStyle-BackColor="#CCCCCC">
                                            <ItemTemplate>
                                    
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Tel" HeaderStyle-Width="15%" HeaderStyle-BackColor="#CCCCCC">
                                            <ItemTemplate>
                                    
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Fax" HeaderStyle-Width="15%" HeaderStyle-BackColor="#CCCCCC">
                                            <ItemTemplate>
                                    
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Email Address" HeaderStyle-Width="25%" HeaderStyle-BackColor="#CCCCCC">
                                            <ItemTemplate>
                                    
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <Columns>
                                        <asp:TemplateField HeaderText="..." HeaderStyle-Width="5%" HeaderStyle-BackColor="#CCCCCC">
                                            <ItemTemplate>
                                    
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                </td>
        </tr>
        <tr>
                <td class="td-width" ><div style="background-color:Silver;padding:4px 4px;">Contract Date From :</div></td>
                <td>
                    
                    <asp:TextBox ID="TextBox34" runat="server"></asp:TextBox>
                    <asp:CalendarExtender ID="TextBox34_CalendarExtender" runat="server" 
                        TargetControlID="TextBox34">
                    </asp:CalendarExtender>
                    
                </td>
                <td class="td-width"><div style="background-color:Silver;padding:4px 4px;">Date To :</div></td>
                <td>
                    <asp:TextBox ID="TextBox22" runat="server"></asp:TextBox>
                    <asp:CalendarExtender ID="TextBox22_CalendarExtender" runat="server" 
                        TargetControlID="TextBox22">
                    </asp:CalendarExtender>
                </td>
        </tr>
        <tr>
            <td  colspan="4" class="td-width" style="background-color:Silver;font-weight:bold;font-size:12px;">
            ข้อมูลการวางบิล
            </td>
        </tr>
        <tr>
                <td class="td-width" ><div style="background-color:Silver;padding:4px 4px;">Company Name (Bill) :</div></td>
                <td colspan="3">
                    <asp:TextBox ID="TextBox12" runat="server" CssClass="textbox-width"></asp:TextBox>
                </td>
        </tr>
        <tr>
                <td class="td-width" ><div style="background-color:Silver;padding:4px 4px;">Address (Bill):</div></td>
                <td colspan="3">
                    <asp:TextBox ID="TextBox13" runat="server" CssClass="textbox-width"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td>&nbsp</td>
            <td colspan = "3">
                <table width = "100%" cellpadding = "4px" cellspacing = "4px">
                    <tr>
                        <td><div style="background-color:Silver;padding:4px 4px;">Aumphur :</div></td>
                        <td><asp:TextBox ID="TextBox42" runat="server"></asp:TextBox></td>
                        <td><div style="background-color:Silver;padding:4px 4px;">Tumbon :</div></td>
                        <td><asp:TextBox ID="TextBox43" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><div style="background-color:Silver;padding:4px 4px;">Province :</div></td>
                        <td><asp:TextBox ID="TextBox44" runat="server"></asp:TextBox></td>
                        <td><div style="background-color:Silver;padding:4px 4px;">Post Code :</div></td>
                        <td><asp:TextBox ID="TextBox46" runat="server"></asp:TextBox></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
                <td class="td-width" ><div style="background-color:Silver;padding:4px 4px;">Telephone (Bill) :</div></td>
                <td>
                    <asp:TextBox ID="TextBox19" runat="server"></asp:TextBox>
                </td>
                <td class="td-width" ><div style="background-color:Silver;padding:4px 4px;">Fax (Bill) :</div></td>
                <td>
                    <asp:TextBox ID="TextBox20" runat="server"></asp:TextBox>
                </td>
        </tr>
        <tr>
                <td class="td-width" ><div style="background-color:Silver;padding:4px 4px;">Contact Perspon (Bill) :</div></td>
                <td colspan="3">
                    <asp:TextBox ID="TextBox14" runat="server" CssClass="textbox-width"></asp:TextBox>
                </td>
        </tr>
        <tr>
                <td class="td-width" ><div style="background-color:Silver;padding:4px 4px;">Contact E-mail (Bill) :</div></td>
                <td colspan="3">
                    <asp:TextBox ID="TextBox15" runat="server" Width="280px"></asp:TextBox>
                    <asp:Button ID="Button3" runat="server" Font-Bold="True" style="height: 22px" 
                        Text="+" />
                </td>
        </tr>
        <tr>
                <td></td>
                <td  colspan="3">
                                <asp:GridView ID="GridView2" ShowHeaderWhenEmpty="true" Width="100%" runat="server" 
                        AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="ชื่อ-สกุล" HeaderStyle-Width="40%" HeaderStyle-BackColor="#CCCCCC">
                                            <ItemTemplate>
                                    
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Tel" HeaderStyle-Width="15%" HeaderStyle-BackColor="#CCCCCC">
                                            <ItemTemplate>
                                    
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Fax" HeaderStyle-Width="15%" HeaderStyle-BackColor="#CCCCCC">
                                            <ItemTemplate>
                                    
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Email Address" HeaderStyle-Width="25%" HeaderStyle-BackColor="#CCCCCC">
                                            <ItemTemplate>
                                    
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <Columns>
                                        <asp:TemplateField HeaderText="..." HeaderStyle-Width="5%" HeaderStyle-BackColor="#CCCCCC">
                                            <ItemTemplate>
                                    
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                </td>
        </tr>
        <tr>
                <td class="td-width" ><div style="background-color:Silver;padding:4px 4px;">Payor :</div></td>
                <td colspan="3">
                    <asp:DropDownList ID="DropDownList6" runat="server">
                    </asp:DropDownList>
                </td>
        </tr>
        <tr>
                <td class="td-width" ><div style="background-color:Silver;padding:4px 4px;">Plan :</div></td>
                <td colspan="3">
                    <asp:TextBox ID="TextBox17" runat="server" Width="280px"></asp:TextBox>
                    <asp:Button ID="Button4" runat="server" Font-Bold="True" style="height: 22px" 
                        Text="+" />
                </td>
        </tr>
        <tr>
                <td></td>
                <td  colspan="3">
                                <asp:GridView ID="GridView3" Width="100%" runat="server" 
                        AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No." HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                    
                                            </ItemTemplate>

                                        <HeaderStyle Width="5%" BackColor="#CCCCCC"></HeaderStyle>
                                                                                </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Plan"><ItemTemplate>
                                    
                                            
                                        </ItemTemplate>

                                        <HeaderStyle Width="90%" BackColor="#CCCCCC"></HeaderStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="..."><ItemTemplate>
                                    
                                            
                                        </ItemTemplate>

                                        <HeaderStyle Width="5%" BackColor="#CCCCCC"></HeaderStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                </td>
        </tr>
        <tr>
                <td class="td-width" ><div style="background-color:Silver;padding:4px 4px;">Type :</div></td>
                <td colspan="3">
                    <asp:DropDownList ID="DropDownList3" runat="server">
                    </asp:DropDownList>
                </td>
        </tr>
        <tr>
                <td class="td-width" ><div style="background-color:Silver;padding:4px 4px;">Payment Type :</div></td>
                <td colspan="3">
                    <div style="border:solid 1px Silver;padding:2px 2px;margin-right:5px">
                        <asp:RadioButton ID="RadioButton1" GroupName="PayType" runat="server" 
                            Text="ชำระเงินสด" />
                        <asp:RadioButton ID="RadioButton2" GroupName="PayType" runat="server" 
                            Text="วางบิล" />
                        <asp:RadioButton ID="RadioButton3" GroupName="PayType" runat="server" 
                            Text="วางบิลและชำระเงินสดเพิ่ม" />
                            &nbsp;วงเงิน&nbsp;<asp:TextBox ID="TextBox1" runat="server" Width="100px"></asp:TextBox>&nbsp;บาท&nbsp; 
                        <br />
                        หมายเหตุ <asp:TextBox ID="TextBox45" runat="server" Width="220px"></asp:TextBox>
                    </div>
                </td>
        </tr>
        <tr>
                <td class="td-width" valign="top"><div style="background-color:Silver;padding:4px 4px;">Billing Method :</div></td>
                <td colspan="3">
                     <div style="border:solid 1px Silver;padding:2px 2px;margin-bottom:5px;margin-right:5px">
                        <asp:RadioButton ID="RadioButton4" GroupName="BillMethod" runat="server" 
                            Text="วางบิลสำนักงานใหญ่ / Invoice send to the mother company" />
                        <asp:RadioButton ID="RadioButton5" GroupName="BillMethod" runat="server" 
                            Text="วางบิลแต่ละบริษัท / Invoice send to each company" />
                    </div>
                    <div style="width:350px;float:left;border:1px solid silver;padding-bottom:5px;">
                        <div style="background-color:Silver;padding:3px 3px;">การชำระเงินชุดตรวจหลัก / term of payment : Main program</div>
                        <asp:RadioButton ID="RadioButton30" Text="บริษัทจ่าย/Credit (Invoice to the conpany)" runat="server" /><br />
                        <asp:RadioButton ID="RadioButton31" Text="ชำระเงินสด/Cash" runat="server" />
                    </div>
                    <div style="width:320px;float:left;margin-left:5px;border:1px solid silver;padding-bottom:5px;">
                        <div style="background-color:Silver;padding:3px 3px;">อัตราค่าบริการ / Check-up rate</div>
                        <asp:RadioButton ID="RadioButton37" Text="เหมาจ่าย / Packages" runat="server" /><br />
                        <asp:RadioButton ID="RadioButton38" Text="คิดตามรายการที่ตรวจจริง / Actual Price" runat="server" />
                    </div> 
                </td>
        </tr>
        
        <tr>
            <td></td>
            <td colspan="3">
                <div style="border:1px solid silver;padding-bottom:5px;margin-right:5px;">
                   <div style="background-color:Silver;padding:3px 3px;">ตรวจเพิ่มเติมตามใบเสนอราคา / Term of payment : Options items montioned in quatation </div>
                    <asp:RadioButton ID="RadioButton6" GroupName="" runat="server" Text="ชำระเงินสด / Cash" /><br />
                    <asp:RadioButton ID="RadioButton7" GroupName="" runat="server" Text="C/O บริษัท(รวมบิล) / C/O Company (Total Bills)" /><br />
                    <asp:RadioButton ID="RadioButton8" GroupName="" runat="server" Text="C/O บริษัท(รวมบิล) [เฉพาะรายชื่อที่ระบุ]" /><br />
                    <asp:RadioButton ID="RadioButton9" GroupName="" runat="server" Text="C/O บริษัท(แยกบิล) / C/O Company (Separate Bills)" /><br />
                    <asp:RadioButton ID="RadioButton10" GroupName="" runat="server" Text="C/O บริษัท(แยกบิล) [เฉพาะรายชื่อที่ระบุ]" /><br />
                    <asp:RadioButton ID="RadioButton11" GroupName="" runat="server" Text="วงเงิน C/O" />&nbsp;<asp:TextBox ID="TextBox18" runat="server"> </asp:TextBox>&nbsp;บาท
                   </div>
            </td>
        </tr>
        <tr>
            <td></td>
            <td colspan="3">
                <div style="border:1px solid silver;padding-bottom:5px;margin-right:5px;">
                   <div style="background-color:Silver;padding:3px 3px;">ตรวจเพิ่มนอกใบเสนอราคา / Term of payment : Options items montioned 
                       not in quatation </div>
                    <asp:RadioButton ID="RadioButton16" GroupName="" runat="server" Text="ชำระเงินสด / Cash" /><br />
                    <asp:RadioButton ID="RadioButton17" GroupName="" runat="server" Text="C/O บริษัท(รวมบิล) / C/O Company (Total Bills)" /><br />
                    <asp:RadioButton ID="RadioButton18" GroupName="" runat="server" Text="C/O บริษัท(รวมบิล) [เฉพาะรายชื่อที่ระบุ]" /><br />
                    <asp:RadioButton ID="RadioButton19" GroupName="" runat="server" Text="C/O บริษัท(แยกบิล) / C/O Company (Separate Bills)" /><br />
                    <asp:RadioButton ID="RadioButton20" GroupName="" runat="server" Text="C/O บริษัท(แยกบิล) [เฉพาะรายชื่อที่ระบุ]" /><br />
                    <asp:RadioButton ID="RadioButton21" GroupName="" runat="server" Text="วงเงิน C/O" />&nbsp;<asp:TextBox ID="TextBox21" runat="server"> </asp:TextBox>&nbsp;บาท
                   </div>
            </td>
        </tr>
        <tr>
            <td></td>
            <td colspan="3">
                <div style="border:1px solid silver;padding-bottom:5px;margin-right:5px;">
                   <div style="background-color:Silver;padding:3px 3px;">รับยาจากการตรวจสุขภาพ / Term of 
                       Receiving Medicine</div>
                    <asp:RadioButton ID="RadioButton22" GroupName="" runat="server" Text="ชำระเงินสด / Cash" /><br />
                    <asp:RadioButton ID="RadioButton23" GroupName="" runat="server" Text="C/O บริษัท(รวมบิล) / C/O Company (Total Bills)" /><br />
                    <asp:RadioButton ID="RadioButton25" GroupName="" runat="server" Text="C/O บริษัท(แยกบิล) / C/O Company (Separate Bills)" /><br />
                    <asp:RadioButton ID="RadioButton27" GroupName="" runat="server" 
                        Text="วงเงิน C/O / Credit Limit C/O" />
                   </div>
            </td>
        </tr>
        <tr>
            <td></td>
            <td colspan="3">
                        <div style="width:320px;float:left;border:1px solid silver;padding-bottom:5px;">
                        <div style="background-color:Silver;padding:3px 3px;">คูปองอาหาร / Meal Coupon</div>
                            <asp:RadioButton ID="RadioButton28" 
                                Text="บริษัทจ่าย/Credit (Invoice to the conpany)" runat="server" /><br />
                            <asp:RadioButton ID="RadioButton29" Text="ชำระเงินสด/Cash" runat="server" />
                        </div>
                        <div style="width:350px;float:left;margin-left:8px;">
                        <table width="350px">
                            <tr>
                                <td class="td-width"><div style="background-color:Silver;padding:4px 4px;width:160px;">
                                Check-up Duration Date :</div></td>
                                <td>
                                    <asp:TextBox ID="TextBox35" runat="server"></asp:TextBox>
                                    <asp:CalendarExtender ID="TextBox35_CalendarExtender" runat="server" 
                                        Enabled="True" TargetControlID="TextBox35">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-width"><div style="background-color:Silver;padding:4px 4px;width:160px;">
                                Date To :</div></td>
                                <td>
                                    <asp:TextBox ID="TextBox36" runat="server"></asp:TextBox>
                                    <asp:CalendarExtender ID="TextBox36_CalendarExtender" runat="server" 
                                        Enabled="True" TargetControlID="TextBox36">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                        </table>
                        </div>
                    </td>

                    <%--<div style="padding:3px 3px;">
                            <div style="background-color:Silver;padding:4px 4px;width:160px;float:left;">
                                Check-up Duration Date :</div>
                            </div>
                        <div style="padding:3px 3px;">
                            <div style="background-color:Silver;padding:4px 4px;width:160px;float:left;">
                                Date To :</div>
                            </div>--%>
        </tr>
        <tr>
            <td class="td-width"><div style="background-color:Silver;padding:4px 4px;">
                    Family's welfar :</div></td>
            <td colspan="3"><asp:TextBox ID="TextBox23" runat="server" CssClass="textbox-width"></asp:TextBox></td>
        </tr>
                        </table>
                    </div>

<div id="tabsubs-2" style="display:;">
                        <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td  colspan="4" class="td-width" style="background-color:Silver;font-weight:bold;font-size:12px;">
                ข้อมูลการตรวจสุขภาพ</td>
        </tr>
         <tr>
                <td class="td-width"><div style="background-color:Silver;padding:4px 4px;">
                    Request Doctor :</div></td>
                <td colspan="3">
                    <asp:TextBox ID="TextBox24" runat="server" CssClass="textbox-width"></asp:TextBox>
                </td>
         </tr>
        <tr>
            <td class="td-width"><div style="background-color:Silver;padding:4px 4px;">
                Request Doctor Cat. :</div></td>
            <td colspan="3" align="left" valign="top">
            <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
            <asp:Button ID="Button9" runat="server" Text="+" Width="29px" />
            </td>
        </tr>
        <tr>
            <td>&nbsp</td>
            <td colspan = "3">
                <asp:GridView ID="GridView7" width="100%" runat="server" AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField HeaderText="No." HeaderStyle-Width="5%" HeaderStyle-BackColor="#CCCCCC">
                            <ItemTemplate>
                                    
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <Columns>
                        <asp:TemplateField HeaderText="Doctor Cat." HeaderStyle-Width="95%" HeaderStyle-BackColor="#CCCCCC">
                            <ItemTemplate>
                                    
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
            <tr>
                <td class="td-width" valign="top"><div style="background-color:Silver;padding:4px 4px;">
                    Check-up Location :</div></td>
                <td colspan="3">
                    <div style="border:solid 1px silver;margin-right:5px;">
                        <table border="0" width="100%">
                            <tr>
                                <td style="width: 131px;">
                                    <asp:CheckBox ID="CheckBox2" runat="server" Text="JMS" />
                                </td>
                                <td>
                                    
                    <asp:TextBox ID="TextBox51" runat="server" CssClass="textbox-width"></asp:TextBox>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="CheckBox3" runat="server" Text="IMS" />
                                </td>
                                <td>
                                    
                    <asp:TextBox ID="TextBox52" runat="server" CssClass="textbox-width"></asp:TextBox>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td >
                                    <asp:CheckBox ID="CheckBox4" runat="server" Text="HPC1" />
                                </td>
                                <td>
                                    
                    <asp:TextBox ID="TextBox53" runat="server" CssClass="textbox-width"></asp:TextBox>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td >
                                    <asp:CheckBox ID="CheckBox5" runat="server" Text="HPC2" />
                                </td>
                                <td>
                                    
                    <asp:TextBox ID="TextBox54" runat="server" CssClass="textbox-width"></asp:TextBox>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td >
                                    <asp:CheckBox ID="CheckBox6" runat="server" Text="HPC3" />
                                </td>
                                <td>
                                    
                    <asp:TextBox ID="TextBox55" runat="server" CssClass="textbox-width"></asp:TextBox>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td >
                                    <asp:CheckBox ID="CheckBox7" runat="server" Text="Other" />
                                </td>
                                <td>
                                    
                    <asp:TextBox ID="TextBox56" runat="server" CssClass="textbox-width"></asp:TextBox>
                                    
                                </td>
                            </tr>
                        </table>
           
                    </div>
                </td>
            </tr>
            <tr>
                <td class="td-width"><div style="background-color:Silver;padding:4px 4px;">
                     <asp:Label ID="Label1" runat="server" Text="ที่อยู่ให้ส่งผล :"></asp:Label></div></td>
                <td colspan="3">
                    <asp:TextBox ID="TextBox26" runat="server" CssClass="textbox-width"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td colspan = "3">
                    <table width="100%">
                        <tr>
                            <td><div style="background-color:Silver;padding:4px 4px;">
                                <asp:Label ID="Label3" runat="server" Text="ตำบล :"></asp:Label></div>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox57" runat="server"></asp:TextBox>
                            </td>
                            <td><div style="background-color:Silver;padding:4px 4px;">
                                <asp:Label ID="Label2" runat="server" Text="อำเภอ :"></asp:Label></div>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox59" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td><div style="background-color:Silver;padding:4px 4px;">
                                <asp:Label ID="Label4" runat="server" Text="จังหวัด :"></asp:Label></div>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox58" runat="server"></asp:TextBox>
                            </td>
                            <td><div style="background-color:Silver;padding:4px 4px;">
                                <asp:Label ID="Label5" runat="server" Text="รหัสไปรษณีย์ :"></asp:Label></div>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox60" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="td-width" valign="top"><div style="background-color:Silver;padding:4px 4px;">
                    Medical Reports :</div></td>
                <td colspan="3">
                    <div style="border:solid 1px silver;">
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:CheckBox ID="CheckBox10" Text="สมุดตรวจสุขภาพ" runat="server" /><br />
                                    <asp:CheckBox ID="CheckBox8" Text="Report fit to Off-Shore" runat="server" /><br />
                                    <asp:CheckBox ID="CheckBox14" runat="server" 
                                    Text="สรุปรวม / Total Report" />
                            </td>
                            <td valign="top">
                                <asp:CheckBox ID="CheckBox9" Text="สำเนาตรวจสุขภาพให้บริษัทเก็บ 1 ชุด" runat="server" /><br />
                                    <asp:CheckBox ID="CheckBox11" Text="ใบรับรองแพทย์ 5 โรค" runat="server" /><br />
                                    <asp:CheckBox ID="CheckBox12" Text="ใบรับรองแพทย์ 6 โรค" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:CheckBox ID="CheckBox13" runat="server" Text="อื่นๆ" />
                                &nbsp;<asp:TextBox ID="TextBox27" runat="server"></asp:TextBox>
                            </td>
                        </tr>

                    </table>
                       
                    </div>
                </td>
            </tr>
            <tr>
                <td></td>
                <td colspan="3">
                    
                        <div style="width:350px;float:left;border:1px solid silver;margin-bottom:5px;padding-bottom:5px;">
                        <div style="background-color:Silver;padding:3px 3px;">Send Report Method</div>
                            <fieldset style="width:150px;float:left;margin-left:10px;margin-right:10px;"><legend>ฉบับจริง</legend>
                                <asp:RadioButton ID="RadioButton24" runat="server" Text="รับกลับ" />                      
                                <br />
                                <asp:RadioButton ID="RadioButton33" runat="server" Text="ไม่รับกลับ" />                       
                                <br />
                                &nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="RadioButton32" runat="server" Text="ส่งบ้าน" />
                                <asp:RadioButton ID="RadioButton26" runat="server" Text="ส่งบริษัท" />
                            </fieldset>
                            <fieldset style="width:150px;float:left;margin-left:10px;margin-right:10px;"><legend>สำเนา</legend>
                            <asp:RadioButton ID="RadioButton34" runat="server" Text="ส่งบ้าน" />                      
                            <br />
                            <asp:RadioButton ID="RadioButton35" runat="server" Text="ส่งบริษัท" />                      
                                <br />
                            <asp:RadioButton ID="RadioButton36" runat="server" Text="ไม่ต้องการ" />                    
                            </fieldset>
                        </div>
                        <div style="width:320px;float:left;margin-left:8px;border:1px solid silver;">
                        <div style="background-color:Silver;padding:3px 3px;">เงื่อนไขการเข้ารับบริการ</div>
                            <asp:CheckBox ID="CheckBox15" runat="server" 
                                Text="แสดงใบส่งตัว / จดหมายส่งตัว" />
                             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="CheckBox20" runat="server" Text="บัตรกำนัล" />
                            <br />
                            <asp:CheckBox ID="CheckBox16" runat="server" Text="แสดงบัตรพนักงาน" />
                            <br />
                            <asp:CheckBox ID="CheckBox17" runat="server" 
                                Text="ตามรายชื่อจาก Email การตลาด" />
                            <br />
                            <asp:CheckBox ID="CheckBox18" runat="server" Text="ตามรายชื่อจากบริษัท" />
                            <br />
                            <asp:CheckBox ID="CheckBox19" runat="server" 
                                Text="ให้ผู้ป่วยเซ็นชื่อยืนยันการรับบริการ" />
                            <br />
                        </div>
                   
              
                </td>
            </tr>
            <tr>
                <td  colspan="4" class="td-width" style="background-color:Silver;font-weight:bold;font-size:12px;">
                ข้อมูลอื่นๆ</td>
            </tr>
            <tr>
                <td class="td-width"><div style="background-color:Silver;padding:4px 4px;">
                    Marketing Name :</div></td>
                <td colspan="3">
                    <asp:TextBox ID="TextBox28" runat="server" CssClass="textbox-width"></asp:TextBox>
                </td>
         </tr>
         <tr>
                <td class="td-width" ><div style="background-color:Silver;padding:4px 4px;">
                    Marketing Tel :</div></td>
                <td>
                    <asp:TextBox ID="TextBox29" runat="server"></asp:TextBox>
                </td>
                <td class="td-width" ><div style="background-color:Silver;padding:4px 4px;">
                    Marketing Fax :</div></td>
                <td>
                    <asp:TextBox ID="TextBox30" runat="server"></asp:TextBox>
                </td>
        </tr>
        <tr>
                <td class="td-width" ><div style="background-color:Silver;padding:4px 4px;">
                    Marketing E-mail :</div></td>
                <td colspan="3">
                    <asp:TextBox ID="TextBox31" runat="server" Width="280px"></asp:TextBox>
                    <asp:Button ID="Button1" runat="server" Font-Bold="True" style="height: 22px" 
                        Text="+" />
                </td>
        </tr>
        <tr>
                <td></td>
                <td  colspan="3">
                                <asp:GridView ID="GridView4" Width="100%" runat="server" 
                        AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="ชื่อ-สกุล" HeaderStyle-Width="40%" HeaderStyle-BackColor="#CCCCCC">
                                            <ItemTemplate>
                                    
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Tel" HeaderStyle-Width="15%" HeaderStyle-BackColor="#CCCCCC">
                                            <ItemTemplate>
                                    
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Fax" HeaderStyle-Width="15%" HeaderStyle-BackColor="#CCCCCC">
                                            <ItemTemplate>
                                    
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Email Address" HeaderStyle-Width="25%" HeaderStyle-BackColor="#CCCCCC">
                                            <ItemTemplate>
                                    
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <Columns>
                                        <asp:TemplateField HeaderText="..." HeaderStyle-Width="5%" HeaderStyle-BackColor="#CCCCCC">
                                            <ItemTemplate>
                                    
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                </td>
        </tr>
        <tr>
                <td class="td-width" ><div style="background-color:Silver;padding:4px 4px;">
                    Upload File :</div></td>
                <td colspan="3">
                    <asp:FileUpload ID="FileUpload1" runat="server" />
&nbsp;<asp:Button ID="Button5" runat="server" Font-Bold="True" style="height: 22px" 
                        Text="+" />
                </td>
        </tr>
        <tr>
            <td class="td-width" ><div style="background-color:Silver;padding:4px 4px;"> Department Type :</div></td>
            <td colspan = "3">
                <asp:DropDownList ID="DropDownList5" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td></td>
            <td colspan="3">
                <asp:GridView ID="GridView5" Width="100%" runat="server" 
                        AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No." HeaderStyle-Width="5%" HeaderStyle-BackColor="#CCCCCC">
                                            <ItemTemplate>
                                    
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <Columns>
                                        <asp:TemplateField HeaderText="File" HeaderStyle-Width="90%" HeaderStyle-BackColor="#CCCCCC">
                                            <ItemTemplate>
                                    
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <Columns>
                                        <asp:TemplateField HeaderText="..." HeaderStyle-Width="5%" HeaderStyle-BackColor="#CCCCCC">
                                            <ItemTemplate>
                                    
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
            </td>
        </tr>
        <tr>
                <td class="td-width"><div style="background-color:Silver;padding:4px 4px;">
                    Remark :</div></td>
                <td colspan="3">
                    <asp:TextBox ID="TextBox32" runat="server" CssClass="textbox-width"></asp:TextBox>
                </td>
         </tr>
                        </table>
                    </div>
                </td>
            </tr>

         <tr>
            <td></td>
            <td colspan="3">
                <asp:Button ID="Button6" CssClass="buttonsubmit" runat="server" Text="Save as Draft" />
                &nbsp;
                <asp:Button ID="Button7" CssClass="buttonsubmit" runat="server" 
            Text="Save" onclick="Button7_Click" />
             </td>
         </tr>
        </table>
        </div>
        <%--///////////////////////////// tab 2 //////////////////////////////--%>
	    <div id="tabs-2">
            <table style="width: 100%;">
                <tr>
                    <td>
                        &nbsp;
                        <asp:TextBox ID="TextBox33" Width="350px" runat="server"></asp:TextBox>
                        &nbsp;
                        &nbsp;
                        <asp:Button ID="Button8" CssClass="buttonsubmit" runat="server" Text="Search" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="GridView6" Width="100%" runat="server" 
                        AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No." HeaderStyle-Width="5%" HeaderStyle-BackColor="#CCCCCC">
                                            <ItemTemplate>
                                    
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Emp Id" HeaderStyle-Width="10%" HeaderStyle-BackColor="#CCCCCC">
                                            <ItemTemplate>
                                    
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Personal Id" HeaderStyle-Width="10%" HeaderStyle-BackColor="#CCCCCC">
                                            <ItemTemplate>
                                    
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <Columns>
                                        <asp:TemplateField HeaderText="ชื่อ-สกุล" HeaderStyle-Width="10%" HeaderStyle-BackColor="#CCCCCC">
                                            <ItemTemplate>
                                    
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Age" HeaderStyle-Width="10%" HeaderStyle-BackColor="#CCCCCC">
                                            <ItemTemplate>
                                    
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Gender" HeaderStyle-Width="10%" HeaderStyle-BackColor="#CCCCCC">
                                            <ItemTemplate>
                                    
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <Columns>
                                        <asp:TemplateField HeaderText="DOB" HeaderStyle-Width="10%" HeaderStyle-BackColor="#CCCCCC">
                                            <ItemTemplate>
                                    
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Position" HeaderStyle-Width="10%" HeaderStyle-BackColor="#CCCCCC">
                                            <ItemTemplate>
                                    
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                    </td>
                </tr>
                </table>
        </div>
</asp:Content>
