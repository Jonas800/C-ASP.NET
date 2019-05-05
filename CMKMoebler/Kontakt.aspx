<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Kontakt.aspx.cs" Inherits="Kontakt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3>Åbningstider</h3>
    <asp:Repeater ID="Repeater_Åbningstider" runat="server">
        <ItemTemplate>
            <div><%# Eval("oplysning_åbningstid") %></div>
        </ItemTemplate>
    </asp:Repeater>

    <asp:Panel ID="Panel_Besked" Visible="false" CssClass="Panel_Besked" runat="server">
        <asp:Label ID="Label_Besked" runat="server"></asp:Label>
    </asp:Panel>

    <asp:Panel ID="Panel_Kontakt" runat="server" CssClass="kontaktFormular">
        <label for="TextBox_Navn">Navn</label>
        <asp:TextBox ID="TextBox_Navn" runat="server" placeholder="Udfyld venligst" ValidationGroup="Panel_Kontakt"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator_Navn" runat="server" ControlToValidate="TextBox_Navn" ValidationGroup="Panel_Kontakt" ErrorMessage="Skal udfyldes" Display="Dynamic"></asp:RequiredFieldValidator>

        <label for="TextBox_Adresse">Adresse</label>
        <asp:TextBox ID="TextBox_Adresse" ValidationGroup="Panel_Kontakt" runat="server" placeholder="Udfyld venligst"></asp:TextBox>

        <label for="TextBox_Telefon">Telefon</label>
        <asp:TextBox ID="TextBox_Telefon" ValidationGroup="Panel_Kontakt" runat="server" placeholder="Udfyld venligst"></asp:TextBox>

        <label for="TextBox_Email">Email</label>
        <asp:TextBox ID="TextBox_Email" ValidationGroup="Panel_Kontakt" runat="server" placeholder="Udfyld venligst"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator_Email" runat="server" ControlToValidate="TextBox_Email" ErrorMessage="Skal udfyldes" ValidationGroup="Panel_Kontakt" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator_Email" runat="server" ErrorMessage="Ikke en gyldig email" ValidationGroup="Panel_Kontakt" ControlToValidate="TextBox_Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic"></asp:RegularExpressionValidator>

        <label for="TextBox_Kommentar">Kommentar</label>
        <asp:TextBox ID="TextBox_Kommentar" ValidationGroup="Panel_Kontakt" runat="server" placeholder="Udfyld venligst" TextMode="MultiLine"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator_Kommentar" runat="server" ValidationGroup="Panel_Kontakt" ControlToValidate="TextBox_Kommentar" ErrorMessage="Skal udfyldes" Display="Dynamic"></asp:RequiredFieldValidator>

        <asp:Button ID="Button_Send" ValidationGroup="Panel_Kontakt" runat="server" OnClick="Button_Send_Click" Text="Send" />
    </asp:Panel>

</asp:Content>

