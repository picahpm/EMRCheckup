<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="CorporateSummaryReportDynamic._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            document.getElementById("<%=startdate.ClientID %>").removeAttribute("Type");
            document.getElementById("<%=enddate.ClientID %>").removeAttribute("Type");
            document.getElementById("<%=startdate.ClientID %>").setAttribute("Type", "Date");
            document.getElementById("<%=enddate.ClientID %>").setAttribute("Type", "Date");
            $('#<%=txtCompanyName.ClientID %>').autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "AutoComplete.asmx/GetListCorp",
                        data: JSON.stringify({'prefix': $('#<%=txtCompanyName.ClientID  %>').val()}),
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item,
                                    val: item
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus + " " + errorThrown);
                        }
                    });

                },
                select: function (e, i) {
                    $("#<%=txtCompanyName.ClientID %>").val(i.item);
                },
                minlength: 1,
                delay: 700
            });
        });
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h1 class="register-title">Corporate Summary Report(Dynamic)</h1>
    <div class="register">
        <table style="width: 100%;">
            <tr>
                <td style="width: 130px;">
                    Company Name.
                </td>
                <td colspan="5">
                    <asp:TextBox ID="txtCompanyName" runat="server" CssClass="register-input" />
                </td>
            </tr>
            <tr>
                <td style="width: 130px;">
                    Sub Company Name.
                </td>
                <td colspan="5">
                    <asp:TextBox ID="txtSubCompanyName" runat="server" CssClass="register-input" />
                </td>
            </tr>
            <tr>
                <td>
                    No. of patients
                </td>
                <td>
                    <asp:TextBox ID="txtTotalPatients" runat="server" Text="0" CssClass="register-input" />
                </td>
                <td>
                    No. of arrived patients
                </td>
                <td>
                    <asp:TextBox ID="txtArrivedPatients" runat="server" Text="0" CssClass="register-input" />
                </td>
            </tr>
            <tr>
                <td>
                    Start Date.
                </td>
                <td>
                    <asp:TextBox ID="startdate" runat="server" CssClass="register-date" required />
                </td>
                <td>
                    End Date.
                </td>
                <td>
                    <asp:TextBox ID="enddate" runat="server" CssClass="register-date" required />
                </td>
            </tr>
            <tr>
                <td>
                    Select Report.
                </td>
                <td colspan="4">
                    <asp:DropDownList runat="server" ID="ddlSelectReport" DataTextField="Text" DataValueField="No"
                        Style="width: 100%" CssClass="custom-dropdown">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="right">
                    <asp:Button runat="server" ID="btnShowPatients" Text="Show Patients List" CssClass="register-button"
                        OnClick="btnShowPatients_Click" Style="width: 70%;" />
                </td>
                <td colspan="2" align="left">
                    <asp:Button runat="server" ID="btnConfirmCriterias" Text="Confirm Criterias" CssClass="register-button"
                        OnClick="btnConfirmCriterias_Click" Style="width: 60%; color: red; display: inline" />
                    <asp:Button runat="server" ID="btnExcel" Visible="false" Text="Excel" CssClass="register-button"
                        OnClick="btnExcel_Click" Style="width: 18%; display: inline" />
                    <asp:Button runat="server" ID="btnPdf" Enabled="false" Visible="false" Text="PDF"
                        CssClass="register-button" OnClick="btnPdf_Click" Style="width: 18%; display: inline" />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="register">
        <asp:GridView runat="server" ID="gvShowPatients" AutoGenerateColumns="false" CssClass="fixed_headers">
            <Columns>
                <asp:TemplateField HeaderText="No.">
                    <ItemTemplate>
                        <asp:Label ID="lblNo" runat="server" Text='<%# Eval("No").ToString()%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="HN">
                    <ItemTemplate>
                        <asp:Label ID="lblHN" runat="server" Text='<%# Eval("HN").ToString()%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ID">
                    <ItemTemplate>
                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID").ToString()%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name").ToString()%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Arrived">
                    <ItemTemplate>
                        <asp:Label ID="lblArrived" runat="server" Text='<%# Eval("Arrived","{0:dd/MM/yyyy}")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:GridView runat="server" ID="gvDynamicReport" OnRowDataBound="gvDynamicReport_OnRowDatabound"
            Visible="false" AutoGenerateColumns="false" CssClass="gvDynamicCss">
        </asp:GridView>
    </div>
</asp:Content>
