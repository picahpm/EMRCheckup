<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminControlPage.aspx.cs" Inherits="CheckUpToDoList.BackOffice.AdminControlPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>BackOffice Control</title>
    <link rel="Stylesheet" href="../style/Style.css" />
    <script language="javascript">
        function msgconfirmDelete() {
            if (confirm('  Do you want delete data !!!  ')) {
                return true;
            } else {
                return false;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ScriptManager ID="ScriptManager1" runat="server"> </asp:ScriptManager>
    <center>
    <asp:UpdatePanel ID="udpAddUserlogin" runat="server">
    <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" style="width:1024px;">
                <tr>
                    <td colspan="2" style=" border-bottom: 1px solid #336699;">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="center" style="background-color:#fff; padding-left:5px;">
                                <img src="../images/logo.bmp"/>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr id="trMenuMain" runat="server">
                    <td align="center">
                        <div id="top-menu">
                            <span id="lnkMenuMain" runat="server"></span><%--<a href="webfromtest.aspx">form test</a>--%>
                        </div>
                                
                    </td>
                    <td style="text-align:right;">
                        <div id="top-menu" style=" text-align:right; float:right; display:inline; padding-right:5px; font-weight:normal;">
                                    
                                    &nbsp;<a href="LogOff.aspx" target="_parent" style="cursor:pointer;color:Red;">Logout</a>
                                    </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="vertical-align:middle; border: 1px solid #336699;" >
                       
        <table border="0" cellspacing="2" cellpadding="0" width="100%">
            <tr>
                <td>
                    Search :<asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                            &nbsp;<asp:Button ID="btnSearch" runat="server" Text="Search" 
                            onclick="btnSearch_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%" border="0" cellspacing="0">
                        <tr>
                            <td>
                                UserLogin :
                            </td>
                            <td>
                                <asp:TextBox ID="txtUserLogin" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ErrorMessage="*" ControlToValidate="txtUserLogin" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                
                                <asp:Button ID="btnRetrieve" runat="server"
                                    Text="retrieve" onclick="btnRetrieve_Click" />
                                
                                <asp:Label ID="lbAlert" runat="server" ForeColor="Red"></asp:Label>
                                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                First Name :
                            </td>
                            <td>
                                <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                    ControlToValidate="txtFirstName" ErrorMessage="*" ValidationGroup="Add"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Last Name :
                            </td>
                            <td>
                                <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                    ControlToValidate="txtLastName" ErrorMessage="*" ValidationGroup="Add"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Department :
                            </td>
                            <td style="text-align:left;">
                            
                             <table border="0" cellspacing="0" cellpadding="0" >
                                <tr>
                                    <td style="border: 1px solid #336699; padding-right:5px;">
                                        <asp:RadioButtonList ID="RDDepartment" runat="server" 
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1">Marketing</asp:ListItem>
                                            <asp:ListItem Value="2">HPC</asp:ListItem>
                                            <asp:ListItem Value="3">Collection</asp:ListItem>
                                            <asp:ListItem Value="4">Contact Center</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                            ControlToValidate="RDDepartment" ErrorMessage="*" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                             </table>
                            
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Policy Edit :
                            </td>
                            <td style="text-align:left;">
                                <table border="0" cellspacing="0" cellpadding="0" >
                                    <tr>
                                        <td align="left">
                                            <asp:RadioButtonList ID="RDPolicyEdit" runat="server" 
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem Value="R">Read Only</asp:ListItem>
                                                <asp:ListItem Value="W">Read &amp; Write</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                                ControlToValidate="RDPolicyEdit" ErrorMessage="*" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                        </td>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Active :
                            </td>
                            <td>
                                <asp:CheckBox ID="chActive" runat="server" Checked="True" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            
                            </td>
                            <td>
                                <asp:Button ID="btnAddnew" runat="server" Text="Add" 
                                    onclick="btnAddnew_Click" ValidationGroup="Add" />
                                &nbsp;<asp:Label ID="lbmsgAlertAdd" runat="server" CssClass="comment" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                
                            </td>
                            <td>
                                 
                            </td>
                        </tr>
                    </table> 
                </td>
            </tr>
            <tr>
                <td>
                <style>
                    .gridview1
                    {
                        width:99%;
                    }
                </style>
                        <asp:GridView ID="GridView1" runat="server" SkinID="gvSkSearch" CssClass="gridview1"
                            AutoGenerateColumns="false" onrowcommand="GridView1_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="User Login(TK)" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate> 
                                        <asp:Label ID="lbUserlogin" runat="server" Text='<%# Bind("Userlogin") %>'></asp:Label> 
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                                    <ItemStyle VerticalAlign="Top" Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="First Name" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate> 
                                        <asp:Label ID="lbFirstname" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label> 
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                                    <ItemStyle VerticalAlign="Top" Width="120px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Last Name" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate> 
                                        <asp:Label ID="lblastname" runat="server" Text='<%# Bind("Lastname") %>'></asp:Label> 
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                                    <ItemStyle VerticalAlign="Top" Width="120px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Marketing">
                                    <HeaderTemplate>
                                    <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                    <tr>
                                        <td colspan="2" >Marketing</td>
                                    </tr>
                                            <tr>
                                            <td align="center" style=" width:40;border-top:1px solid #000;padding-right:5px;">Read</td>
                                            <td align="center" style=" padding-right:4px; border-left:1px solid #000;border-top:1px solid #000;">Write</td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                      <center>
                                        <asp:HiddenField ID="mpf_idHiddenField_1" runat="server" Value='<%# Bind("Userlogin") %>' />
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                            <td align="center" style=" padding-right:4px;">
                                                <asp:RadioButton ID="chkMarket" runat="server" 
                                                  GroupName='<%# Bind("Userlogin") %>' AutoPostBack="true"
                                                  Checked='<%# (((Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Market")) == true) && Convert.ToString(DataBinder.Eval(Container.DataItem, "IsEdit")) == "R") ? true : false)%>' 
                                                  oncheckedchanged="chkMarket_CheckedChanged" />
                                            </td>
                                            <td align="center" style=" width:40; border-left:1px solid #000;padding-right:7px;">
                                            <center>
                                                <asp:RadioButton ID="chkMarketWrite" runat="server" 
                                                  GroupName='<%# Bind("Userlogin") %>' AutoPostBack="true"
                                                  Checked='<%# (((Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Market")) == true) && Convert.ToString(DataBinder.Eval(Container.DataItem, "IsEdit")) == "W") ? true : false)%>' 
                                                  oncheckedchanged="chkMarketWrite_CheckedChanged" /></center>
                                            </td>
                                            </tr>
                                        </table>
                                      </center>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                                    
                                    <ItemStyle CssClass="txtAlignCenter" VerticalAlign="Top" Width="80px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="HPC">
                                <HeaderTemplate>
                                    <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                    <tr>
                                        <td colspan="2" >HPC</td>
                                    </tr>
                                            <tr>
                                            <td align="center" style=" width:40;border-top:1px solid #000;padding-right:5px;">Read</td>
                                            <td align="center" style=" padding-right:4px; border-left:1px solid #000;border-top:1px solid #000;">Write</td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                      <center>
                                        <asp:HiddenField ID="mpf_idHiddenField_2" runat="server" Value='<%# Bind("Userlogin") %>' />
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                            <td align="center" style=" padding-right:4px;">
                                        <asp:RadioButton ID="chkHPC" runat="server" GroupName='<%# Bind("Userlogin") %>' 
                                              AutoPostBack="true" Checked='<%# (((Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "HPC")) == true) && Convert.ToString(DataBinder.Eval(Container.DataItem, "IsEdit")) == "R") ? true : false)%>' 
                                              oncheckedchanged="chkHPC_CheckedChanged" />
                                              </td>
                                            <td align="center" style=" width:40; border-left:1px solid #000;padding-right:7px;">
                                        <asp:RadioButton ID="chkHPCWrite" runat="server" GroupName='<%# Bind("Userlogin") %>' 
                                              AutoPostBack="true" Checked='<%# (((Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "HPC")) == true) && Convert.ToString(DataBinder.Eval(Container.DataItem, "IsEdit")) == "W") ? true : false)%>' 
                                              oncheckedchanged="chkHPCWrite_CheckedChanged" />
                                                </td>
                                            </tr>
                                        </table>
                                      </center>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                                    <ItemStyle CssClass="txtAlignCenter" VerticalAlign="Top" Width="80px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Collection">
                                <HeaderTemplate>
                                    <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                    <tr>
                                        <td colspan="2" >Collection</td>
                                    </tr>
                                            <tr>
                                            <td align="center" style=" width:40;border-top:1px solid #000;padding-right:5px;">Read</td>
                                            <td align="center" style=" padding-right:4px; border-left:1px solid #000;border-top:1px solid #000;">Write</td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                      <center>
                                        <asp:HiddenField ID="mpf_idHiddenField_3" runat="server" Value='<%# Bind("Userlogin") %>' />
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                            <td align="center" style=" padding-right:4px;">
                                        <asp:RadioButton ID="chkCollection" runat="server" 
                                              GroupName='<%# Bind("Userlogin") %>' AutoPostBack="true" 
                                              Checked='<%# (((Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Collection")) == true) && Convert.ToString(DataBinder.Eval(Container.DataItem, "IsEdit")) == "R") ? true : false)%>' 
                                              oncheckedchanged="chkCollection_CheckedChanged" />
                                               </td>
                                            <td align="center" style=" width:40; border-left:1px solid #000;padding-right:7px;">
                                        <asp:RadioButton ID="chkCollectionWrite" runat="server" 
                                              GroupName='<%# Bind("Userlogin") %>' AutoPostBack="true" 
                                              Checked='<%# (((Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Collection")) == true) && Convert.ToString(DataBinder.Eval(Container.DataItem, "IsEdit")) == "W") ? true : false)%>' 
                                              oncheckedchanged="chkCollectionWrite_CheckedChanged" />
                                                </td>
                                            </tr>
                                        </table>
                                      </center>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                                    <ItemStyle CssClass="txtAlignCenter" VerticalAlign="Top" Width="80px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ContactCenter">
                                <HeaderTemplate>
                                    <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                    <tr>
                                        <td colspan="2" >ContactCenter</td>
                                    </tr>
                                            <tr>
                                            <td align="center" style=" width:40;border-top:1px solid #000;padding-right:5px;">Read</td>
                                            <td align="center" style=" padding-right:4px; border-left:1px solid #000;border-top:1px solid #000;">Write</td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                      <center>
                                        <asp:HiddenField ID="mpf_idHiddenField_4" runat="server" Value='<%# Bind("Userlogin") %>' />
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                            <td align="center" style=" padding-right:4px;">
                                        <asp:RadioButton ID="chkContactCenter" runat="server" 
                                              GroupName='<%# Bind("Userlogin") %>' AutoPostBack="true" 
                                              Checked='<%# (((Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "ContactCenter")) == true) && Convert.ToString(DataBinder.Eval(Container.DataItem, "IsEdit")) == "R") ? true : false)%>' 
                                              oncheckedchanged="chkContactCenter_CheckedChanged" />
                                              </td>
                                            <td align="center" style=" width:40; border-left:1px solid #000;padding-right:7px;">
                                        <asp:RadioButton ID="chkContactCenterWrite" runat="server" 
                                              GroupName='<%# Bind("Userlogin") %>' AutoPostBack="true" 
                                              Checked='<%# (((Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "ContactCenter")) == true) && Convert.ToString(DataBinder.Eval(Container.DataItem, "IsEdit")) == "W") ? true : false)%>' 
                                              oncheckedchanged="chkContactCenterWrite_CheckedChanged" />
                                                </td>
                                            </tr>
                                        </table>
                                      </center>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                                    <ItemStyle CssClass="txtAlignCenter" VerticalAlign="Top" Width="80px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Active">
                                    <ItemTemplate>
                                      <center>
                                        <asp:HiddenField ID="mpf_idHiddenField_Active" runat="server" Value='<%# Bind("Userlogin") %>' />
                                        <asp:CheckBox ID="chkActive" runat="server" AutoPostBack="true" 
                                              Checked='<%# Bind("IsActive") %>' oncheckedchanged="chkActive_CheckedChanged"/>
                                      </center>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                                    <ItemStyle CssClass="txtAlignCenter" VerticalAlign="Top" Width="80px" />
                                </asp:TemplateField>
                                <asp:TemplateField ControlStyle-Width="40" HeaderStyle-Wrap="true" 
                                    HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEditPackage" runat="server" 
                                          CommandArgument='<%# Bind("Userlogin") %>' CommandName="Edituser" 
                                          CausesValidation="false" ImageUrl="~/images/edit.png" 
                                          Style="width:18px; height: 18px;" />

                                        <asp:ImageButton ID="btnDelUserlogin" runat="server" CausesValidation="false" 
                                        CommandArgument='<%# Bind("Userlogin") %>' CommandName="DeleteUser" 
                                        ImageUrl="~/images/delete.png" OnClientClick="return msgconfirmDelete();" 
                                        Style="width: 18px; height: 18px;" />
                                    </ItemTemplate>
                                    <controlstyle width="40px" />
                                    <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="40px" />
                                </asp:TemplateField>                                                                                
                            </Columns>
                            <EmptyDataTemplate>There are currently no items in this table.</EmptyDataTemplate>
                        </asp:GridView>
        
                 </td>
            </tr>
            <tr>
                <td>
                   <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" />
                &nbsp;<asp:Label ID="lbmsgAlert" runat="server" CssClass="comment" Text=""></asp:Label>
                </td>
            </tr>

        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="height:30px;" align="left">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td style="height:30px; font-size:10px; color:#5f5858;" align="center">
                                Bangkok Hospital Located at 2 Soi Soonvijai 7, New Petchburi Rd, Bangkok, 10310. Thailand. Phone: +66 2310-3000.<br />
                                Copyright © 2013  Bangkok Hospital. All Rights Reserved. | Website by Greenline Synergy
                                </td>
                            </tr>
                        </table>
                   </td>
                </tr>
            </table>
        </center>    
       </ContentTemplate>
    </asp:UpdatePanel>

    </div>
    </form>
</body>
</html>
