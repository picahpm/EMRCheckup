<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm_company_details.aspx.cs" Inherits="CheckUpToDoList.frm_company_details" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="Stylesheet" href="css/Style.css" />
    <style type="text/css">
            .td-width
            {
    	        padding:6px 6px;
            }
            .td-border
            {
    	        border:1px solid silver;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%;">
            <tr>
                <td class="td-width">
                    <div style="background-color:Silver;padding:4px 4px;">Company Name (Thai) :</div>
                </td>
                <td colspan="2">
                    &nbsp;
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    &nbsp;
                </td>
                <td rowspan="3" valign="top">
                    <fieldset style="width:250px;"><legend>Payment Method</legend>
                        <asp:RadioButton ID="RadioButton1" Text="วางบิล" runat="server" /><br />
                        <asp:RadioButton ID="RadioButton2" Text="ชำระเงินสด" runat="server" /><br />
                        <asp:RadioButton ID="RadioButton3" Text="วางบิลและชำระเงินสดเพิ่ม" runat="server" /><br />
                        <asp:RadioButton ID="RadioButton4" Text="วงเงิน" runat="server" />
                        <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td class="td-width">
                    <div style="background-color:Silver;padding:4px 4px;">Company Name (Eng) :</div>
                </td>
                <td colspan="2">
                    &nbsp;
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="td-width">
                    <div style="background-color:Silver;padding:4px 4px;">Order Set or Option :</div>
                </td>
                <td colspan="2">
                    &nbsp;
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="td-width">
                    <div style="background-color:Silver;padding:4px 4px;">Order Type :</div>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server">
                    </asp:DropDownList>
                    *</td>
                <td class="td-width">
                    <div style="background-color:Silver;padding:4px 4px;">Price :</div>
                </td>
                    <td>
                    &nbsp;
                    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                        บาท</td>
            </tr>
            <tr>
                <td class="td-width">
                    <div style="background-color:Silver;padding:4px 4px;">Date From :</div>
                </td>
                <td>
                <asp:TextBox ID="TextBox41" runat="server"></asp:TextBox></td>
                <td class="td-width">
                    <div style="background-color:Silver;padding:4px 4px;">Date To :</div>
                </td>
                    <td>
                    &nbsp;
                <asp:TextBox ID="TextBox42" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td-width">
                    <div style="background-color:Silver;padding:4px 4px;">Doctor Name :</div>
                </td>
                <td>
                <asp:TextBox ID="TextBox43" runat="server"></asp:TextBox></td>
                
            </tr>
            <tr>
                <td class="td-width">
                    <div style="background-color:Silver;padding:4px 4px;">Doctor Cat. :</div>
                </td>
                    <td>
                    &nbsp;
                        <asp:DropDownList ID="DropDownList2" runat="server">
                        </asp:DropDownList>
                        <asp:Button ID="Button4" runat="server" Text="+" Width="29px" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td colspan = "3">
                    <asp:GridView ID="GridView2" Width="100%" runat="server" AutoGenerateColumns="False">
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
                <td class="td-width">
                    <div style="background-color:Silver;padding:4px 4px;">การเตรียมตัวก่อนตรวจ :</div>
                </td>
                <td colspan="3">
                    &nbsp;
                    <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td-width">
                    &nbsp;</td>
                <td>
                    <asp:Button ID="Button1" runat="server" CssClass="buttonsubmit" Text="Add" />
                </td>
                <td class="td-width">
                    &nbsp;</td>
                    <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="td-width">
                    &nbsp;</td>
                <td colspan="3">
                                <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No." HeaderStyle-Width="5%" HeaderStyle-BackColor="#CCCCCC">
                                            <ItemTemplate>
                                    
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Description" HeaderStyle-Width="15%" HeaderStyle-BackColor="#CCCCCC">
                                            <ItemTemplate>
                                    
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Date From" HeaderStyle-Width="10%" HeaderStyle-BackColor="#CCCCCC">
                                            <ItemTemplate>
                                    
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Date To" HeaderStyle-Width="10%" HeaderStyle-BackColor="#CCCCCC">
                                            <ItemTemplate>
                                    
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Type" HeaderStyle-Width="10%" HeaderStyle-BackColor="#CCCCCC">
                                            <ItemTemplate>
                                    
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Price" HeaderStyle-Width="10%" HeaderStyle-BackColor="#CCCCCC">
                                            <ItemTemplate>
                                    
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Payment" HeaderStyle-Width="10%" HeaderStyle-BackColor="#CCCCCC">
                                            <ItemTemplate>
                                    
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <Columns>
                                        <asp:TemplateField HeaderText="การเตรียมตัวก่อนตรวจ" HeaderStyle-Width="20%" HeaderStyle-BackColor="#CCCCCC">
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
                <td class="td-width">
                    &nbsp;</td>
                <td>
                    <asp:Button ID="Button2" runat="server" CssClass="buttonsubmit" Text="Save" />
                </td>
                <td class="td-width">
                    &nbsp;</td>
                    <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="td-width" colspan="2">
                    <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                    <asp:Button ID="Button3" runat="server" CssClass="buttonsubmit" Text="Search" />
                </td>
                <td class="td-width">
                    &nbsp;</td>
                    <td>
                        &nbsp;</td>
            </tr>
            <tr>
                <td class="td-width">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td class="td-width">
                    &nbsp;</td>
                    <td>
                        &nbsp;</td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
