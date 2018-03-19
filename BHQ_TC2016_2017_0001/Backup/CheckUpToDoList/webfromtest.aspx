<%@ Page Title="" Language="C#" MasterPageFile="~/HeadToDoListNew.Master" AutoEventWireup="true" CodeBehind="webfromtest.aspx.cs" Inherits="CheckUpToDoList.webfromtest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_body" runat="server">
    <script type="text/javascript" src="js/jquery-1.9.1.js"></script>
<script type="text/javascript" src="js/jquery-ui-1.10.3.custom.js"></script>
<script type="text/javascript" src="js/jquery.min.js"></script>
<script type="text/javascript" src="js/jquery-ui.min.js"></script>
<link rel="stylesheet" href="style/styleAutoComplete.css" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    Test Brows File Form Local To Server :<asp:FileUpload ID="FileUpload1" runat="server" />
    <asp:Button ID="Button1" runat="server" Text="Add File" 
        onclick="Button1_Click" />
    <asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
        Text="Save To DB" />
    <br />
    <table width="600px">
        <tr>
            <td>No</td>
            <td>File Name</td>
            <td>...</td>
        </tr>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
            <tr>
                <td><%# DataBinder.Eval(Container.DataItem,"_fileid") %></td>
                <td><%# DataBinder.Eval(Container.DataItem,"_filename") %></td>
                <td>Del</td>
            </tr>
            </ItemTemplate>
        </asp:Repeater>   
    </table>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="Button1" />
    </Triggers>
    </asp:UpdatePanel>
<script type="text/javascript">
    $(document).ready(function () {
        $('.txtAutoComplete').autocomplete({
            source: function (request, response) {
                jQuery.ajax({
                    type: "post",
                    url: 'GetDataTrakCare.ashx',
                    data: 'type=req_doc&param1=' + document.getElementById('<%=txttest.ClientID %>').value,
                    success: function (data) {
                        //alert(data);
                        if (data.length > 0) {
                            response(data.split("<,>"))
                        }
                    },
                    fail: function (msg) { }
                });
            },
            minLength: 2
        });
    });
</script>
Test Autocomplete Docter Request :<asp:TextBox ID="txttest" CssClass="txtAutoComplete" runat="server"></asp:TextBox><br />

    
</asp:Content>
