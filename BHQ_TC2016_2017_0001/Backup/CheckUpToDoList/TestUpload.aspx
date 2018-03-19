<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestUpload.aspx.cs" Inherits="CheckUpToDoList.TestUpload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
                              <asp:UpdatePanel ID="udpPalliativeDoc"  runat="server" >
                                <ContentTemplate>
                                    <table class="infoLayout" cellpadding="0" cellspacing="0" style="width:97%;" > <!-- Group Coordinator Data Record -->
                                            <tr>
                                                <td style="padding:5px;" >
                                                        <table  width="100%" class="infoLayout" style="background-color:#A9D0F5;" >
                                                            <tr>
                                                                <td>
                                                                    <table class="infoLayout" cellpadding="0" cellspacing="0" style="width:500px;padding:0px;border-color:White;">
                                                                    <tr>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                <asp:UpdatePanel ID="updatePalliativeDocFile"  runat="server" RenderMode="Block" >
                                                                <ContentTemplate>
                                                                    <table class="infoLayout"  cellpadding="0" cellspacing="0"  style="width:500px;padding:0px;border-color:White;">
                                                                    <tr>
                                                                        <td colspan="2" class="textBold" style="background-color:White;">&nbsp;Attache file : 
                                                                        <literal id="lbmsgbox" runat="server"></literal>
                                                                        </td>
                                                                    </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:FileUpload ID="fileUpload" runat="server" style="width:400px;" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Button ID="btnAddFile" OnClick="btnAddFile_Click" runat="server" CssClass="buttonstyle" Text="Add" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2">
                                                                                    <asp:GridView ID="gnvAttachFile" runat="server" SkinID="gvSkSearch"
                                                                                AutoGenerateColumns="False" HeaderStyle-Height="30px"  >
                                                                                    <Columns>
                                                                                        <asp:TemplateField  HeaderText="File Name"  ItemStyle-VerticalAlign="Top">
                                                                                            <ItemTemplate >
                                                                                                <center>  <%# (DataBinder.Eval(Container.DataItem, "file_name"))%></center>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle VerticalAlign="Top"  Width="100px"  />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Update By" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top"
                                                                                                HeaderStyle-Wrap="true">
                                                                                            <ItemTemplate><center>
                                                                                                    <%# (DataBinder.Eval(Container.DataItem, "UpdateBy"))%></center>
                                                                                            </ItemTemplate>
                                                                                            <ControlStyle Width="40px" />
                                                                                            <HeaderStyle Wrap="True" />
                                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="200px" />
                                                                                        </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Update Date" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top"
                                                                                                HeaderStyle-Wrap="true">
                                                                                            <ItemTemplate><center>
                                                                                                    <%# (DataBinder.Eval(Container.DataItem, "UpdateDate"))%></center>
                                                                                            </ItemTemplate>
                                                                                            <ControlStyle Width="40px" />
                                                                                            <HeaderStyle Wrap="True" />
                                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="200px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ControlStyle-Width="40"
                                                                                            ItemStyle-VerticalAlign="Top" HeaderStyle-Wrap="true">
                                                                                            <ItemTemplate>
                                                                                                <asp:ImageButton ID="btnDelAttacheFile" OnClick="btnDelAttacheFile_Click" runat="server" ImageUrl="~/images/icon/delete.png"
                                                                                                    Style="width: 20px; height: 20px;" CausesValidation="false" />
                                                                                            </ItemTemplate>
                                                                                            <ControlStyle Width="40px" />
                                                                                            <HeaderStyle Wrap="True" />
                                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"  Width="20px"  />
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
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="btnAddFile"/>
                                                                    </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    
                                                                    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                                                                        Text="Save file" />
                                                                    
                                                                </td>
                                                            </tr>
                                                         </table>
                                                </td>
                                            </tr>
                                     </table>
                                  
                                </ContentTemplate>
                               
                              </asp:UpdatePanel>
                       
            </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
