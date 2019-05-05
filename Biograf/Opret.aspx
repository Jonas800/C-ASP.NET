<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Opret.aspx.cs" Inherits="Opret" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>Opret ny bruger</h1>
    <h3>Navn</h3>
    <asp:TextBox ID="TextBox_Navn" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Navn" runat="server" ControlToValidate="TextBox_Navn" ErrorMessage="Skal udfyldes"></asp:RequiredFieldValidator>

    <h3>Email</h3>
    <asp:TextBox ID="TextBox_Email" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Email" runat="server" ControlToValidate="TextBox_Email" ErrorMessage="Skal udfyldes"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator_Email" runat="server" ErrorMessage="Ikke en gyldig email" ControlToValidate="TextBox_Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>

    <h3>Kodeord</h3>
    <asp:TextBox ID="TextBox_Kodeord" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Kodeord" runat="server" ErrorMessage="Skal udfyldes" ControlToValidate="TextBox_Kodeord"></asp:RequiredFieldValidator>

    <h3>Gentag kodeord</h3>
    <asp:TextBox ID="TextBox_Kodeord_Gentag" runat="server"></asp:TextBox>
    <asp:CompareValidator ID="CompareValidator_Kodeord" runat="server" ErrorMessage="Kodeord er ikke ens" ControlToValidate="TextBox_Kodeord_Gentag" ControlToCompare="TextBox_Kodeord"></asp:CompareValidator>


    <asp:CheckBox ID="CheckBox_Nyhedsbrev" runat="server" />
    <asp:Button ID="Button_Gem" runat="server" OnClick="Button_Gem_Click" Text="Gem" />
</asp:Content>

