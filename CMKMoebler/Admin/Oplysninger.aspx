<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="Oplysninger.aspx.cs" Inherits="Admin_Oplysninger" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="Panel_Besked" CssClass="Panel_Besked" runat="server">
        <asp:Label ID="Label_Besked" runat="server"></asp:Label>
    </asp:Panel>

    <label for="TextBox_Kontaktoplysninger">Kontaktoplysninger</label>
    <asp:TextBox ID="TextBox_Kontaktoplysninger" runat="server" CssClass="ckeditor" TextMode="MultiLine" Rows="10"></asp:TextBox>
    <script src="ckeditor/ckeditor.js"></script>

    <label for="TextBox_Åbningstider">Åbningstider</label>
    <asp:TextBox ID="TextBox_Åbningstider" runat="server" CssClass="ckeditor" TextMode="MultiLine" Rows="10"></asp:TextBox>
    <script src="ckeditor/ckeditor.js"></script>

    <label for="TextBox_Email">Email til modtagelse af emails</label>
        <asp:TextBox ID="TextBox_Email" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Email" runat="server" ControlToValidate="TextBox_Email" ErrorMessage="Skal udfyldes" Display="Dynamic"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator_Email" runat="server" ErrorMessage="Ikke en gyldig email" ControlToValidate="TextBox_Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic"></asp:RegularExpressionValidator>

    <asp:Button ID="Button_Gem" runat="server" Text="Gem" OnClick="Button_Gem_Click" />

</asp:Content>

