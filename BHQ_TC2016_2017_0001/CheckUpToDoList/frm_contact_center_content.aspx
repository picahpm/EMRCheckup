<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm_contact_center_content.aspx.cs" Inherits="CheckUpToDoList.frm_contact_center_content" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<link rel="stylesheet" href="style/Style.css" />
<style type="text/css">
    .td-width-title
    {
    	width:250px;
    }

    .td-width-content-colspan
    {
    	width:600px;
    }
    
    .td-width-content
    {
    	width:175px;
    }
    
    .div-title
    {
        font-size:12px;
        padding:3px 3px;
        border:1px solid silver;
        background-color:Silver;
       
    }
    
    .div-content
    {
        font-size:12px;
        padding:3px 3px;
        border:1px solid silver;
        min-height:14px;
    }
    
    .td-border
    {
    	border:1px solid silver;
    }

    .bg-label
    {
        border:1px solid silver;
        padding:4px 4px;
        min-height:13px;
        width:585px;
    }
    .horizontalscroll
    {
    overflow-x: auto;
    overflow-y: auto;
    width:600px;
    height:150px;
    }
    
    .horizontalscroll-pg
    {
    overflow-x: auto;
    overflow-y: auto;
    width:900px;
    height:200px;
    }
    
    .bordor-bottom-data
    {
        border-bottom:1px dashed gray;
    }
</style>

<script type="text/javascript">
    function checkEnter(event) {
        if (event) {
            if (event.keyCode == 13) //ur code
            {
                $('#btnsearch').click();
            }
        }

    }
</script>

</head>
<body onkeypress="checkEnter(event)">
<form id="form1" runat="server">
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</asp:ToolkitScriptManager>
<div style="float:right;"><asp:Button ID="btnEdit" runat="server" Width="80px" 
        onclick="Button1_Click" Text="Edit" Visible="False" /></div>

<asp:TabContainer ID="TabContainer1" CssClass="ajax__tab_Custom" Width="100%" 
    AutoPostBack="false" 
    ActiveTabIndex="2" runat="server">
    <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1">
        <HeaderTemplate>
            ข้อมูลบริษัท
        </HeaderTemplate>
        <ContentTemplate>
        <table cellpadding="2" width="850px" cellspacing="0">
        <tr>
                <td class="td-width-title"><div class="div-title">Document Code :</div></td>
                <td class="td-width-content-colspan" colspan="3"><div class="div-content"><asp:Label ID="lbldoccode" runat="server"></asp:Label></div></td>
            </tr>
            <tr>
                <td class="td-width-title"><div class="div-title">Company Code :</div></td>
                <td class="td-width-content-colspan" colspan="3"><div class="div-content"><asp:Label ID="lblcompcode" runat="server"></asp:Label></div></td>
            </tr>
    <tr>
        <td class="td-width-title"><div class="div-title">Company Name (Thai) :</div></td>
        <td class="td-td-width-content-colspan" colspan="3"><div class="div-content"><asp:Label ID="lblcompany_th" runat="server"></asp:Label></div></td>
    </tr>
    <tr>
        <td class="td-width-title"><div class="div-title">Company Name (Eng) :</div></td>
        <td class="td-width-td-width-content-colspan" colspan="3"><div class="div-content"><asp:Label ID="lblcompany_en" runat="server"></asp:Label></div></td>
    </tr>
     <tr>
                    <td class="td-width-title"><div class="div-title">Dept. Owner :</div></td>
                    <td class="td-width-content-colspan" colspan="3" ><div class="div-content"><asp:Label ID="lbldeptowner" runat="server"></asp:Label></div></td>
                </tr>
    <tr style="display:none;">
        <td class="td-width-title" style="vertical-align:top"><div class="div-title">Check-up Location :</div></td>
        <td class="td-width-content-colspan" colspan="3"><div style="height:auto; min-height:14px" class="div-content"><asp:Label ID="lblchk_location" runat="server"></asp:Label></div></td>
    </tr>
    <tr>
        <td class="td-width-title"><div class="div-title">วันเริ่มต้นสัญญา :</div></td>
        <td class="td-width-content"><div class="div-content"><asp:Label ID="lbldate_s_contract" runat="server"></asp:Label></div></td>
        <td class="td-width-title"><div class="div-title">วันสิ้นสุดสัญญา :</div></td>
        <td class="td-width-content"><div class="div-content"><asp:Label ID="lbldate_e_contract" Width="100px" runat="server"></asp:Label></div></td>
    </tr>
    <tr>
                <td class="td-width-title" style="vertical-align:top;"><div class="div-title">Payment Type :</div></td>
                <td class="td-width-content-colspan" colspan="3">
                    <div style="border:1px solid Gray; width:100%;">
                        <div class="horizontalscroll" style=" width:612px;">
                            <table cellpadding="2" cellspacing="2" style="width:1280px;text-align:center;">
                                    <tr class="headerStyle">
                                        <td style="width:20px;">No.</td>
                                        <td style="width:100px;">Type</td>
                                        <td style="width:100px;">Payment Type</td>
                                        <td style="width:100px;">Billing Method</td>
                                        <td style="width:100px;">Main program</td>
                                        <td style="width:100px;">Check-up rate</td>
                                        <td style="width:200px;">Options items as montioned in quatation</td>
                                        <td style="width:200px;">Options items montioned not in quatation</td>
                                        <td style="width:150px;">Term of Receiving Medicine</td>
                                        <td style="width:100px;">Meal Coupon</td>
                                    </tr>
                                <asp:Repeater ID="RepeaterPaymentType" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                        <td style="width:20px;border-bottom:1px dashed gray;"><%# Container.ItemIndex + 1 %></td>
                                        <td style="width:100px;border-bottom:1px dashed gray;"><%# (DataBinder.Eval(Container.DataItem, "mst_name"))%></td>

                                        <td style="width:100px;border-bottom:1px dashed gray;">
                                            <%# (DataBinder.Eval(Container.DataItem, "mpt_name"))%>
                                            <%# ((Convert.ToInt32(DataBinder.Eval(Container.DataItem, "tpa_mpt_credit")) == 0) ? "" : DataBinder.Eval(Container.DataItem, "tpa_mpt_credit", "[{0}]")) %> 
                                            <%# (DataBinder.Eval(Container.DataItem, "tpa_mpt_remark")=="")?"":(DataBinder.Eval(Container.DataItem, "tpa_mpt_remark", "Remark:[{0}]"))%>
                                        </td>
                                    
                                        <td style="width:100px;border-bottom:1px dashed gray;"><%# (DataBinder.Eval(Container.DataItem, "mbm_name"))%></td>
                                        <td style="width:100px;border-bottom:1px dashed gray;"><%# (DataBinder.Eval(Container.DataItem, "mpm_name"))%></td>
                                        <td style="width:100px;border-bottom:1px dashed gray;"><%# (DataBinder.Eval(Container.DataItem, "mpr_name"))%></td>
                                        <td style="width:200px;border-bottom:1px dashed gray;">
                                            <%# (DataBinder.Eval(Container.DataItem, "mpq_name"))%>
                                            <%# ((Convert.ToInt32(DataBinder.Eval(Container.DataItem, "tpa_mpq_credit")) == 0) ? "" : DataBinder.Eval(Container.DataItem, "tpa_mpq_credit", "[{0}]"))%> 
                                        </td>
                                        <td style="width:200px;border-bottom:1px dashed gray;">
                                            <%# (DataBinder.Eval(Container.DataItem, "mpn_name"))%>
                                            <%# ((Convert.ToInt32(DataBinder.Eval(Container.DataItem, "tpa_mpn_credit")) == 0) ? "" : DataBinder.Eval(Container.DataItem, "tpa_mpn_credit", "[{0}]"))%> 
                                        </td>
                                        <td style="width:150px;border-bottom:1px dashed gray;"><%# (DataBinder.Eval(Container.DataItem, "mrm_name"))%></td>
                                        <td style="width:100px;border-bottom:1px dashed gray;">
                                            <%# (DataBinder.Eval(Container.DataItem, "coupon_name"))%>
                                            <%# (DataBinder.Eval(Container.DataItem, "tpa_coupon_remark") == "") ? "" : (DataBinder.Eval(Container.DataItem, "tpa_coupon_remark", "Remark:[{0}]"))%>
                                        </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                </table>
                        </div>
                    </div>  
                </td>
            </tr>
    <tr>
        <td class="td-width-title"><div class="div-title">เงื่อนไขการเข้ารับบริการ (พนักงาน):</div></td>
        <td class="td-width-content-colspan" colspan="3"><div class="div-content">
            <asp:Label ID="lblcond_service" runat="server"></asp:Label></div></td>
    </tr>
    <tr>
        <td class="td-width-title"><div class="div-title">เงื่อนไขการเข้ารับบริการ (ผู้บริหาร):</div></td>
        <td class="td-width-content-colspan" colspan="3"><div class="div-content">
            <asp:Label ID="lblcond_service_executive" runat="server"></asp:Label></div></td>
    </tr>
    <tr>
        <td class="td-width-title"><div class="div-title">ประเภท :</div></td>
        <td class="td-width-content-colspan" colspan="3"><div class="div-content"><asp:Label ID="lblcomptype" runat="server"></asp:Label></div></td>
    </tr>
    <tr>
        <td class="td-width-title"><div class="div-title">สวัสดิการครอบครัว :</div></td>
        <td class="td-width-content-colspan" colspan="3"><div class="div-content"><asp:Label ID="lblwelfare" runat="server"></asp:Label></div></td>
    </tr>
    <tr>
        <td class="td-width-title"><div class="div-title">แพทย์ :</div></td>
        <td class="td-width-content-colspan" colspan="3"><div class="div-content"><asp:Label ID="lbldoctor" runat="server"></asp:Label></div></td>
    </tr>
    <tr>
        <td class="td-width-title"><div class="div-title">ประเภทของแพทย์ :</div></td>
        <td class="td-width-content-colspan" colspan="3"><div class="div-content"><asp:Label ID="lbldoctor_type" runat="server"></asp:Label></div></td>
    </tr>
    <tr>
        <td class="td-width-title" style="vertical-align:top;"><div class="div-title">สถานที่ตรวจ :</div></td>
        <td class="td-width-content-colspan td-border" colspan="3">
                   <table style="vertical-align:top;float:left;">
                        <tr>
                            <td><asp:CheckBox ID="chkjms" runat="server" Text="JMS" Enabled="False" />
                                <asp:CheckBox ID="chkims" runat="server" Text="IMS" Enabled="False" />
                                <asp:CheckBox ID="chkhpc1" runat="server" Text="HPC1" Enabled="False" />
                                <asp:CheckBox ID="chkhpc2" runat="server" Text="HPC2" Enabled="False" />
                                <asp:CheckBox ID="chkhpc3" runat="server" Text="HPC3" Enabled="False" />
                                <asp:CheckBox ID="chkbcancer" runat="server" Text="OBG" Enabled="False" />
                                <asp:CheckBox ID="chkoth" runat="server" Text="Other" Enabled="False" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="bg-label">
                                    <asp:Label ID="lblloc_remark" runat="server"></asp:Label>
                                </div>
                            </td>
                        </tr>
                    </table>
        </td>
    </tr>
    <tr>
        <td class="td-width-title" valign="top"><div class="div-title">Staff :</div></td>
        <td class="td-width-content-colspan" colspan="3">
        <div class="horizontalscroll" style="border:1px solid gray;width:100%;">
            <table style="width:100%;" cellpadding="2" cellspacing="2">
                        <tr class="headerStyle">
                            <td style="width:50px;">No.</td>
                            <td style="width:200px;">ชื่อ-นามสกุล</td>
                            <td style="width:100px;">Tel</td>
                            <td style="width:100px;">Fax</td>
                            <td style="width:150px;">Email"</td>
                        </tr>
                <asp:Repeater ID="RepeaterMTK" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td class="bordor-bottom-data"><%# Container.ItemIndex +1 %></td>
                            <td class="bordor-bottom-data"><%# DataBinder.Eval(Container.DataItem, "mtk_name")%></td>
                            <td class="bordor-bottom-data"><%# DataBinder.Eval(Container.DataItem, "mtk_tel")%></td>
                            <td class="bordor-bottom-data"><%# DataBinder.Eval(Container.DataItem, "mtk_fax")%></td>
                            <td class="bordor-bottom-data"><%# DataBinder.Eval(Container.DataItem, "mtk_email")%></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
            
        </td>
    </tr>
    <tr>
        <td class="td-width-title"><div class="div-title">วันที่อัพเดตล่าสุด :</div></td>
        <td class="td-width-content"><div class="div-content"><asp:Label ID="lbllastupdate" runat="server"></asp:Label></div></td>
        <td class="td-width-title"><div class="div-title">จนท.ที่ทำการแก้ไขข้อมูลล่าสุด :</div></td>
        <td class="td-width-content"><div class="div-content"><asp:Label ID="lbllastupdateby" runat="server"></asp:Label></div></td>
    </tr>
    <tr>
        <td class="td-width-title" valign="top"><div class="div-title">เอกสารแนบ :</div></td>
        <td class="td-width-content-colspan" colspan="3">
        <div class="horizontalscroll" style="border:1px solid gray;width:100%;">
            <table style="width:600px;" cellpadding="2" cellspacing="2">
                        <tr class="headerStyle">
                            <td style="width:20px;">No.</td>
                            <td style="width:550px;">File</td>
                            <td style="width:50px;">Dowload</td>
                        </tr>
                <asp:Repeater ID="RepeaterFile" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td class="bordor-bottom-data"><%# Container.ItemIndex + 1 %></td>
                            <td class="bordor-bottom-data"><%# DataBinder.Eval(Container.DataItem, "file_name")%></td>
                            <td class="bordor-bottom-data"><asp:HyperLink ID="Hyperlink1" runat="server" NavigateUrl='<%# Eval("file_path", "{0}") %>' Target="_blank" Text='Download' /></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        </td>
    </tr>
    </table>
        </ContentTemplate>
    </asp:TabPanel>
  <asp:TabPanel ID="TabPanel4" runat="server" HeaderText="TabPanel4">
        <HeaderTemplate>
            Package And Option
        </HeaderTemplate>
        <ContentTemplate>
        <div class="horizontalscroll-pg" style="border:1px solid Gray;height:550px;">
                        <table cellpadding="2" width="1280px" cellspacing="2" 
                                style="font-family: Tahoma; font-style: normal; font-size: 12px;">
                                            <tr>
                                                <td colspan="9" style="background-color:Silver;"><strong>Package</strong></td>
                                            </tr>
                                            <tr class="headerStyle">
                                                <td style="width:20px;">No.</td>
                                                <td style="width:600px;">รายการตรวจ</td>
                                                <td style="width:80px;">Start Date</td>
                                                <td style="width:70px;">End Date</td>
                                                <td style="width:100px;">Request Doc.</td>
                                                <td style="width:100px;">Request Cate.</td>
                                                <td style="width:50px;">Price</td>
                                                <td style="width:100px;">วิธีการชำระ</td>
                                                <td style="width:70px;">วงเงิน</td>
                                                
                                            </tr>
                                    <asp:Repeater ID="RepeaterPackage" runat="server">
                                        <ItemTemplate>
                                            <tr >
                                                <td style="border-bottom:1px dashed gray;"><%# Container.ItemIndex + 1 %></td>
                                                <td style="border-bottom:1px dashed gray;"><%# DataBinder.Eval(Container.DataItem, "package_order")%></td>
                                                <td style="border-bottom:1px dashed gray;"><%# DataBinder.Eval(Container.DataItem, "package_s_date")%></td>
                                                <td style="border-bottom:1px dashed gray;"><%# DataBinder.Eval(Container.DataItem, "package_e_date")%></td>
                                                <td style="border-bottom:1px dashed gray;"><%# DataBinder.Eval(Container.DataItem, "package_rd")%></td>
                                                <td style="border-bottom:1px dashed gray;"><%# DataBinder.Eval(Container.DataItem, "package_dc")%></td>
                                                <td style="border-bottom:1px dashed gray;"><%# String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "package_price"))%></td>
                                                <td style="border-bottom:1px dashed gray;"><%# DataBinder.Eval(Container.DataItem, "package_payment")%></td>
                                                <td style="border-bottom:1px dashed gray;"><%# String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "package_credit"))%></td>
                                                
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr><td></td></tr>
                                    <tr>
                                        <td colspan="9" style="background-color:Silver;"><strong>Option</strong></td>
                                    </tr>
                                    <tr class="headerStyle">
                                                <td style="width:20px;">No.</td>
                                                <td style="width:600px;">รายการตรวจ</td>
                                                <td style="width:80px;">Start Date</td>
                                                <td style="width:70px;">End Date</td>
                                                <td style="width:100px;">Request Doc.</td>
                                                <td style="width:100px;">Request Cate.</td>
                                                <td style="width:50px;">Price</td>
                                                <td style="width:100px;">วิธีการชำระ</td>
                                                <td style="width:70px;">วงเงิน</td>
                                            </tr>
                                    <asp:Repeater ID="RepeaterOption" runat="server">
                                        <ItemTemplate>
                                            <tr >
                                                <td style="border-bottom:1px dashed gray;"><%# Container.ItemIndex + 1 %></td>
                                                <td style="border-bottom:1px dashed gray;"><%# DataBinder.Eval(Container.DataItem, "package_order")%></td>
                                                <td style="border-bottom:1px dashed gray;"><%# DataBinder.Eval(Container.DataItem, "package_s_date")%></td>
                                                <td style="border-bottom:1px dashed gray;"><%# DataBinder.Eval(Container.DataItem, "package_e_date")%></td>
                                                <td style="border-bottom:1px dashed gray;"><%# DataBinder.Eval(Container.DataItem, "package_rd")%></td>
                                                <td style="border-bottom:1px dashed gray;"><%# DataBinder.Eval(Container.DataItem, "package_dc")%></td>
                                                <td style="border-bottom:1px dashed gray;"><%# String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "package_price"))%></td>
                                                <td style="border-bottom:1px dashed gray;"><%# DataBinder.Eval(Container.DataItem, "package_payment")%></td>
                                                <td style="border-bottom:1px dashed gray;"><%# String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "package_credit"))%></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    </table> 
                                    </div>
        </ContentTemplate>
    </asp:TabPanel>

    <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="TabPanel3">
        <HeaderTemplate>
            Remark
        </HeaderTemplate>
        <ContentTemplate>
            <table width="600px">
                <tr>
                     <td class="td-width"><div class="divtitleNameStyle">Company Name (Thai) :</div></td>
                        <td><asp:Label ID="lblremark_compname_th" runat="server">-</asp:Label></td>
                    </tr>
                    <tr>
                       <td class="td-width"><div class="divtitleNameStyle">Company Name (Eng) :</div></td>
                       <td><asp:Label ID="lblremark_compname_en" runat="server">-</asp:Label></td>
                    </tr>
            </table>
            <div class="div-content" style="min-height:640px;" id="divremark" runat="server"></div>
        </ContentTemplate>
        </asp:TabPanel>
    <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
        <HeaderTemplate>
            รายชื่อผู้ตรวจ
        </HeaderTemplate>
        <ContentTemplate>
<table style="width: 100%;border:1px solid silver;padding:5px;">
        <tr>
                      <td class="td-width"><div class="divtitleNameStyle">Search</div></td>
                     <td>
                         <asp:TextBox ID="txtsearch" Width="250px" runat="server" Placeholder="Search by First Name/Last Name"></asp:TextBox>
                         <asp:Button ID="btnsearch"  runat="server" Text="Search" 
                             onclick="btnsearch_Click" />
                        </td>
                    </tr>
    </table>
    <div class="horizontalscroll" style="width:100%;height:550px;">
                            <table style="width:1870px;" cellpadding="2" cellspacing="2">
                                <tr class="headerStyle" >
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
                               
                                <asp:Repeater ID="RepeaterPatient" runat="server">
                                <ItemTemplate>
                                <tr>
                                    <td style="width:20px;"><%# Container.ItemIndex + 1 %></td>
                                    
                                    <td style="width:100px;"><asp:Label ID="lblF2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"tnc_emp_id") %>'></asp:Label></td>
                                    <td style="width:100px;"><asp:Label ID="lblF5" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"tnc_title_name") %>'></asp:Label></td>
                                     <td style="width:100px;"><asp:Label ID="lblF6" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"tnc_fname") %>'></asp:Label></td>
                                    <td style="width:100px;"><asp:Label ID="lblF7" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"tnc_lname") %>'></asp:Label></td>
                                    <td style="width:100px;"><asp:Label ID="lblF12" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"tnc_program") %>'></asp:Label></td>
                                    <td style="width:100px;"><asp:Label ID="lblF13" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"tnc_option") %>'></asp:Label></td>
                                    <td style="width:400px;"><asp:Label ID="lblF15" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"tnc_remark") %>'></asp:Label></td>
                                    <td style="width:100px;"><asp:Label ID="lblF0" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"tnc_legal") %>'></asp:Label></td>
                                    <td style="width:100px;"><asp:Label ID="lblF1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"tnc_company_name") %>'></asp:Label></td>
                                    <td style="width:100px;"><asp:Label ID="lblF3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"tnc_personal_id") %>'></asp:Label></td>
                                    <td style="width:100px;"><asp:Label ID="lblF4" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"tnc_hn") %>'></asp:Label></td>
                                    
                                    <td style="width:100px;"><asp:Label ID="lblF8" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"tnc_gender") %>'></asp:Label></td>
                                    <td style="width:100px;"><asp:Label ID="lblsite" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"tnc_site") %>'></asp:Label></td>
                                    <td style="width:100px;"><asp:Label ID="lbldept" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"tnc_department") %>'></asp:Label></td>
                                    <td style="width:100px;"><asp:Label ID="lblF9" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"tnc_position") %>'></asp:Label></td>
                                    <td style="width:100px;"><asp:Label ID="lblF10" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"tnc_dob","{0:dd/MM/yyyy}") %>'></asp:Label></td>
                                    <td style="width:100px;"><asp:Label ID="lblF11" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"tnc_age") %>'></asp:Label></td>
                                    <td style="width:150px;"><asp:Label ID="lblF14" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"tnc_appoint_date") %>'></asp:Label></td>
                                    
                                </tr>
                                </ItemTemplate>
                                </asp:Repeater>
                                
                            </table>
                            </div>
        </ContentTemplate>
    </asp:TabPanel>
</asp:TabContainer>
</form>
</body>
</html>
