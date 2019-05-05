<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>Login</h1>

    <h3>Email</h3>
    <asp:TextBox ID="TextBox_Email" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Email" runat="server" ControlToValidate="TextBox_Email" ErrorMessage="Skal udfyldes"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator_Email" runat="server" ErrorMessage="Ikke en gyldig email" ControlToValidate="TextBox_Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>

    <h3>Kodeord</h3>
    <asp:TextBox ID="TextBox_Kodeord" runat="server" TextMode="Password"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Kodeord" runat="server" ErrorMessage="Skal udfyldes" ControlToValidate="TextBox_Kodeord"></asp:RequiredFieldValidator>

    <asp:Button ID="Button_Gem" runat="server" Text="Login" OnClick="Button_Gem_Click" />
    <asp:Label ID="Label_Error" runat="server" Text=""></asp:Label>
    <a class="Form_a" href="Opret.aspx">Har du ikke en bruger? Opret dig her!</a>
    <a class="Form_a" href="Opret.aspx">Glemt kodeord? Klik her!</a>

</asp:Content>

