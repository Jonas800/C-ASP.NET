<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="Label_Name" runat="server" Text="Username"></asp:Label>
    <asp:TextBox ID="TextBox_Name" runat="server"></asp:TextBox>
    <asp:Label ID="Label_Password" runat="server" Text="Password"></asp:Label>
    <asp:TextBox ID="TextBox_Password" runat="server"></asp:TextBox>
    <asp:Button ID="Button_Login" runat="server" Text="Login" OnClick="Button_Login_Click" />
    <asp:Label ID="Label_Message" runat="server"></asp:Label>

</asp:Content>

