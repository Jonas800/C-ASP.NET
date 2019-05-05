<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="Konti.aspx.cs" Inherits="Admin_Konti" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2>Egen konti</h2>

    <label>Email</label>
    <asp:TextBox ID="TextBox_Email" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Email" runat="server" ControlToValidate="TextBox_Email" ErrorMessage="Skal udfyldes" Display="Dynamic"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator_Email" runat="server" ErrorMessage="Ikke en gyldig email" ControlToValidate="TextBox_Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic"></asp:RegularExpressionValidator>

    <label>Kodeord</label>
    <asp:TextBox ID="TextBox_Kodeord" runat="server"></asp:TextBox>
    <label>Gentag kodeord</label>
    <asp:TextBox ID="TextBox_Kodeord_Gentag" runat="server"></asp:TextBox>
    <asp:CompareValidator ID="CompareValidator_Kodeord" Display="Dynamic" runat="server" ControlToCompare="TextBox_Kodeord_Gentag" ControlToValidate="TextBox_Kodeord" ErrorMessage="Kodeord er ikke ens"></asp:CompareValidator>

    <asp:Button ID="Button_Gem" runat="server" Text="Gem" OnClick="Button_Gem_Click" />
</asp:Content>

