<%@ Page Title="" Language="C#" MasterPageFile="~/HeadToDoListNew.Master" AutoEventWireup="true" CodeBehind="testCalendarMaster.aspx.cs" Inherits="CheckUpToDoList.testCalendarMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_body" runat="server">

                <asp:TextBox ID="TxtInvDate" runat="server" Width="150" />
                
                <img id="ImgBntCalc" runat="server" alt="Select Invoice Date" src="~/calendar/calendar3.gif"
                    style="cursor: pointer" />
                <span class="comment"> * </span>
               
                <asp:RequiredFieldValidator ID="Req5" runat="server"
                ControlToValidate="TxtInvDate" ErrorMessage=" Required" SetFocusOnError="true" />

</asp:Content>
