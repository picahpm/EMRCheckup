<%@ Page Title="" Language="C#" MasterPageFile="~/HeadToDoListNew.Master" AutoEventWireup="true" CodeBehind="frmMasterCompany.aspx.cs" Inherits="CheckUpToDoList.frmMasterCompany" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_body" runat="server">
<script src="calendar/calendar.js"></script>
<link rel="Stylesheet" href="calendar/calendar.css" />
<link rel="stylesheet" href="style/Style.css" />

<link rel="Stylesheet" href="style/smoothness/jquery-ui-1.10.3.custom.css" />
<link rel="stylesheet" href="style/styleAutoComplete.css" />

<script type="text/javascript" src="js/jquery-1.9.1.js"></script>
<script type="text/javascript" src="js/jquery-ui-1.10.3.custom.js"></script>
<script type="text/javascript" src="js/jquery.min.js"></script>
<script type="text/javascript" src="js/jquery-ui.min.js"></script>

<style type="text/css">
    .CompanyName
    {
    	width:600px;
    }
    
    .textbox-width
    {
        margin-bottom: 0px;
    }
    
</style>

<script type="text/javascript">

    $(document).ready(function () {
        $(".CompanyName").autocomplete({
            source: function (request, response) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "frmSearchCompany.aspx/GetMasterCompanyList",
                    data: "{'cname':'" + $("#<%= txtsearch.ClientID %>").val() + "'}",
                    dataType: "json",
                    success: function (data) {
                        response(data.d);
                    },
                    error: function (result) {
                        alert(result + " แหง่ว2");
                    }
                });
            }
        });
    });
</script>
 
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>   
    <table width="800px" style="margin:0 auto;" cellpadding="2" cellspacing="2">
<tr>
                    <td colspan="2"><div style="width:350px;padding:5px 5px;float:left;color:#203E5F;">Company Name(TH)/Company Name(EN)</div>
                    </td>
                </tr>
                <tr>
                    <td ><div>
                       <asp:TextBox ID="txtsearch" CssClass="CompanyName" runat="server" 
                            MaxLength="100" ontextchanged="txtsearch_TextChanged"></asp:TextBox>
                        </div></td>
                    <td ><div style="text-align:left;">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                 <asp:Button ID="btnSearch" Width="80px"  runat="server" Text="Search" 
                            onclick="btnSearch_Click" />
                                 <asp:Button ID="btnClear" Width="80px" runat="server" Text="Clear" 
                                     onclick="btnClear_Click" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        </div></td>
                </tr>
                </table>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        
            <table width="800px" style="margin:0 auto;" cellpadding="2" cellspacing="2">
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnedit" runat="server" Text="Edit Company" Enabled="False" 
                            onclick="btnedit_Click" EnableTheming="True" />&nbsp;<asp:Button ID="btnaddnew" runat="server" 
                            Text="Add New Company" onclick="btnaddnew_Click" />
                        <asp:HiddenField ID="HiddenFieldStatus" runat="server" />
                        <center>
                            <asp:Label ID="lblStatus" runat="server" ForeColor="Red" Text="Save Complete. Please wait ...." 
                            Visible="False"></asp:Label>
                            <asp:Timer ID="TimerProgressWait" runat="server" Interval="5000" 
                                ontick="TimerProgressWait_Tick">
                            </asp:Timer>
                        </center>
                    </td>
                </tr>
                
            </table>

            <asp:Panel ID="PanelContent" runat="server" BackColor="White" Enabled="False">
            <table width="800px" style="margin:0 auto;" cellpadding="2" cellspacing="2">
                <tr>
                    <td colspan="2" class="headerStyle">Company</td>
                </tr>
                <tr>
                    <td style="width:200px;">
                        
                        <asp:HiddenField ID="HiddenField_mcoid" runat="server" Value="0" />
                        
                    </td>
                    <td style="width:600px;">
                        <asp:RadioButton ID="rdoactive" runat="server" GroupName="group_status" 
                            Text="Active" Checked="True" AutoPostBack="True" 
                            oncheckedchanged="rdoactive_CheckedChanged" />
                        <asp:RadioButton ID="rdoinactive" runat="server" GroupName="group_status" 
                            Text="Inactive" AutoPostBack="True" 
                            oncheckedchanged="rdoinactive_CheckedChanged" />
                    </td>
                </tr>
                <tr>
                    <td style="width:200px;"><div style="padding:4px 4px;background-color:Silver;color:#203E5F;">
                        Company Code :</div></td>
                    <td style="width:600px;">
                        <asp:TextBox ID="txtcompany_code" CssClass="textbox-width" runat="server" 
                            MaxLength="20"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width:200px;"><div style="padding:4px 4px;background-color:Silver;color:#203E5F;">
                        Company Name(TH) :</div></td>
                    <td style="width:600px;">
                        <asp:TextBox ID="txtname_th" CssClass="textbox-width" runat="server" 
                            MaxLength="200"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width:200px;"><div style="padding:4px 4px;background-color:Silver;color:#203E5F;">
                        Company Name(EN) :</div></td>
                    <td style="width:600px;">
                        <asp:TextBox ID="txtname_en" CssClass="textbox-width" runat="server" 
                            MaxLength="200"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width:200px;"><div style="padding:4px 4px;background-color:Silver;color:#203E5F;">
                        Legal :</div></td>
                    <td style="width:600px;">
                        <asp:TextBox ID="txtlegal" CssClass="textbox-width" runat="server" 
                            MaxLength="200"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                                <td style="width:200px;">
                                    <div style="padding:4px 4px;background-color:Silver;color:#203E5F;">Dept. Owner :</div>
                                </td>
                                <td colspan="3">
                                    <asp:RadioButton ID="rdona" runat="server" GroupName="mcotype" Text="N/A" 
                                        Checked="True" />
                                    <asp:RadioButton ID="rdojms" runat="server" GroupName="mcotype" Text="JMS" />
                                </td>
                            </tr>
                <tr>
                    <td style="width:200px;"><div style="padding:4px 4px;background-color:Silver;color:#203E5F;">
                        Address :</div></td>
                    <td style="width:600px;">
                        <asp:TextBox ID="txtaddress" CssClass="textbox-width" runat="server" 
                            MaxLength="200"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width:200px;"><div style="padding:4px 4px;background-color:Silver;color:#203E5F;">
                        Sub-District :</div></td>
                    <td style="width:600px;">
                        <asp:TextBox ID="txtsubdistrict" CssClass="textbox-width" runat="server" 
                            MaxLength="100"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width:200px;"><div style="padding:4px 4px;background-color:Silver;color:#203E5F;">
                        District :</div></td>
                    <td style="width:600px;">
                        <asp:TextBox ID="txtdistrict" CssClass="textbox-width" runat="server" 
                            MaxLength="100"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width:200px;"><div style="padding:4px 4px;background-color:Silver;color:#203E5F;">
                        Province :</div></td>
                    <td style="width:600px;">
                        <asp:TextBox ID="txtprovince" CssClass="textbox-width" runat="server" 
                            MaxLength="100"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width:200px;"><div style="padding:4px 4px;background-color:Silver;color:#203E5F;">
                        Postcode :</div></td>
                    <td style="width:600px;">
                        <asp:TextBox ID="txtpostcode" CssClass="textbox-width" runat="server" 
                            MaxLength="10"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width:200px;"><div style="padding:4px 4px;background-color:Silver;color:#203E5F;">
                        Telephone :</div></td>
                    <td style="width:600px;">
                        <asp:TextBox ID="txttelephone" CssClass="textbox-width" runat="server" 
                            MaxLength="100"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width:200px;"><div style="padding:4px 4px;background-color:Silver;color:#203E5F;">
                        Fax :</div></td>
                    <td style="width:600px;">
                        <asp:TextBox ID="txtfax" CssClass="textbox-width" runat="server" 
                            MaxLength="100"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width:200px;"><div style="padding:4px 4px;background-color:Silver;color:#203E5F;">
                        Email :</div></td>
                    <td style="width:600px;">
                        <asp:TextBox ID="txtemail" CssClass="textbox-width" runat="server" 
                            MaxLength="100"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="headerStyle">Contact</td>
                </tr>
                <tr>
                    <td style="width:200px;"><div style="padding:4px 4px;background-color:Silver;color:#203E5F;">
                        Type f Responsibility : </div></td>
                    <td style="width:600px;">
                        <asp:DropDownList ID="ddlcontact_type" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width:200px;"><div style="padding:4px 4px;background-color:Silver;color:#203E5F;">
                        Type Of Contact : </div></td>
                    <td style="width:600px;">
                        <asp:DropDownList ID="ddltype" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width:200px;"><div style="padding:4px 4px;background-color:Silver;color:#203E5F;">
                        Contact Person :</div></td>
                    <td style="width:600px;">
                        <asp:TextBox ID="txtcontact_person" CssClass="textbox-width" runat="server" 
                            MaxLength="500"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width:200px;"><div style="padding:4px 4px;background-color:Silver;color:#203E5F;">
                        Contact Person Tel :</div></td>
                    <td style="width:600px;">
                        <asp:TextBox ID="txtcontact_person_tel" CssClass="textbox-width" runat="server" 
                            MaxLength="100"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width:200px;"><div style="padding:4px 4px;background-color:Silver;color:#203E5F;">
                        Contact Person Fax :</div></td>
                    <td style="width:600px;">
                        <asp:TextBox ID="txtcontact_fax" CssClass="textbox-width" 
                            runat="server" MaxLength="100"></asp:TextBox>
                    
                </td>
                </tr>
                <tr>
                    <td style="width:200px;"><div style="padding:4px 4px;background-color:Silver;color:#203E5F;">
                        Contact Person Email :</div></td>
                    <td style="width:600px;">
                        <asp:TextBox ID="txtcontact_person_email" CssClass="textbox-width" 
                            runat="server" MaxLength="100"></asp:TextBox>
                    
                    </td>
                </tr>
                
                <tr>
                    <td style="width:200px;">&nbsp;</td>
                    <td style="width:600px;">
                        <asp:Button ID="btncontact_add" CssClass="buttoncss" runat="server" 
                            Text="เพิ่ม" onclick="btncontact_add_Click" />
                    
                        <asp:HiddenField ID="HiddenFieldRowContact" runat="server" />
                    
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table cellpadding="2" cellspacing="2">
                            <tr class="headerStyle">
                                <td style="width:20px;">No</td>
                                <td style="width:100px;">Type Of Contact</td>
                                <td style="width:100px;">Type Of Responsibility</td>
                                <td style="width:150px;">ชื่อ-นามสกุล</td>
                                <td style="width:100px;">Tel</td>
                                <td style="width:100px;">Fax</td>
                                <td style="width:150px;">Email</td>
                                <td style="width:50px;">...</td>
                            </tr>
                            <asp:Repeater ID="RepeaterContact" runat="server" onitemcommand="RepeaterContact_ItemCommand" 
                                >
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Container.ItemIndex + 1 %></td>
                                        <td><asp:Label ID="lblcontact_type" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "contact_type")%>'></asp:Label></td>
                                        <td><%# DataBinder.Eval(Container.DataItem, "type_name")%></td>
                                        <td><asp:HiddenField ID="HiddenFieldType" Value='<%# DataBinder.Eval(Container.DataItem, "type_id")%>' runat="server" />
                                            <asp:HiddenField ID="HiddenFieldTypeId" Value='<%# DataBinder.Eval(Container.DataItem, "contact_type_id")%>' runat="server" />
                                            <asp:HiddenField ID="HiddenFieldContact_id" Value='<%# DataBinder.Eval(Container.DataItem, "contact_id")%>' runat="server" />
                                            <asp:Label ID="lblcontact_name" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "contact_name")%>'></asp:Label></td>
                                        <td><asp:Label ID="lblcontact_tel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "contact_tel")%>'></asp:Label></td>
                                        <td><asp:Label ID="lblcontact_fax" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "contact_fax")%>'></asp:Label></td>
                                        <td><asp:Label ID="lblcontact_email" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "contact_email")%>'></asp:Label></td> 
                                        <td><asp:ImageButton ID="ImageEdit" CommandArgument='<%# Container.ItemIndex + 1 %>' CommandName="Edit" ImageUrl="~/images/edit.png" runat="server" /><asp:ImageButton ID="ImgDel" CommandArgument='<%# Container.ItemIndex + 1 %>' CommandName="Del" ImageUrl="~/images/delete.png" runat="server" /></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>  
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                    <asp:Button ID="btnsavedetail" runat="server" Text="Save and Input Detail" 
                            onclick="btnsavedetail_Click" />
                        &nbsp;<asp:Button ID="btnsave" runat="server" Text="Save" 
                    onclick="btnsave_Click" />
                    </td>
                </tr>
            </table>
            
           </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
