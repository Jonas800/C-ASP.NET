<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="Server">
    <%--LOGIN--%>
    <div class="wrapper">
        <asp:Panel ID="Panel_Login" runat="server" DefaultButton="Button_Login">
            <h2>Login</h2>
            <asp:Label ID="Label_Login_Name" runat="server" Text="Brugernavn" CssClass="label_contact"></asp:Label>
            <asp:TextBox ID="TextBox_Login_Name" runat="server" CssClass="textbox_contact"></asp:TextBox>
            <asp:Label ID="Label_Login_Password" runat="server" Text="Kodeord" CssClass="label_contact"></asp:Label>
            <asp:TextBox ID="TextBox_Login_Password" runat="server" CssClass="textbox_contact" TextMode="Password"></asp:TextBox>
            <asp:Button ID="Button_Login" runat="server" Text="Login" CssClass="button" OnClick="Button_Login_Click" />
            <br />
            <asp:Label ID="Label_Message" runat="server" Text="" ForeColor="DarkRed"></asp:Label>
        </asp:Panel>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
</asp:Content>

