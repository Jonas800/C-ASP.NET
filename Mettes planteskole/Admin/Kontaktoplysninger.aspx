<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="Kontaktoplysninger.aspx.cs" Inherits="Admin_Kontaktoplysninger" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2>Kontaktoplysninger</h2>

    <label>Adresse</label>
    <asp:TextBox ID="TextBox_Adresse" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Adresse" ControlToValidate="TextBox_Adresse" runat="server" ErrorMessage="Skal udfyldes" Display="Dynamic"></asp:RequiredFieldValidator>

    <label>Postnummer</label>
    <asp:TextBox ID="TextBox_Postnummer" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Postnummer" ControlToValidate="TextBox_Postnummer" runat="server" ErrorMessage="Skal udfyldes" Display="Dynamic"></asp:RequiredFieldValidator>
    <asp:CompareValidator ID="CompareValidator_Postnummer" runat="server" Operator="DataTypeCheck" Type="Integer"
        ControlToValidate="TextBox_Postnummer" Display="Dynamic" ErrorMessage="Skal være hele tal" />

    <label>By</label>
    <asp:TextBox ID="TextBox_By" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_By" runat="server" ControlToValidate="TextBox_By" ErrorMessage="Skal udfyldes" Display="Dynamic"></asp:RequiredFieldValidator>

    <label>Telefon</label>
    <asp:TextBox ID="TextBox_Telefon" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Telefon" ControlToValidate="TextBox_Telefon" runat="server" ErrorMessage="Skal udfyldes" Display="Dynamic"></asp:RequiredFieldValidator>
    <asp:CompareValidator ID="CompareValidator_Telefon" runat="server" Operator="DataTypeCheck" Type="Integer" Display="Dynamic"
        ControlToValidate="TextBox_Telefon" ErrorMessage="Skal være hele tal" />

    <label>Email</label>
    <asp:TextBox ID="TextBox_Email" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Email" runat="server" ControlToValidate="TextBox_Email" ErrorMessage="Skal udfyldes" Display="Dynamic"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator_Email" runat="server" ErrorMessage="Ikke en gyldig email" ControlToValidate="TextBox_Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic"></asp:RegularExpressionValidator>

    <asp:Button ID="Button_Gem" runat="server" Text="Gem" OnClick="Button_Gem_Click" />
</asp:Content>

