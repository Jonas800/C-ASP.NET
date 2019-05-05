<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Anmeldelser.aspx.cs" Inherits="Anmeldelser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1>Skriv en anmeldelse!</h1>

    <asp:TextBox ID="TextBox_Tekst" TextMode="MultiLine" runat="server" MaxLength="10"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Tekst" ControlToValidate="TextBox_Tekst" runat="server" ErrorMessage="Må ikke være tom!"></asp:RequiredFieldValidator>

    <asp:Button ID="Button_Gem" OnClick="Button_Gem_Click" runat="server" Text="Gem og udgiv" />

    <asp:Label ID="Label_Ikke_Bruger" runat="server" Visible="false" Text="Du skal være logget ind for at skrive en anmeldelse"></asp:Label>

    <asp:Panel ID="Panel_Slet" runat="server" Visible="false"><a class="Form_a2" href="?id=<%= Request.QueryString["id"] %>&slet=true" onclick="return confirm('Er du sikker? Du vil miste alt du har skrevet')">Slet din eksisterende anmeldelse</a></asp:Panel>
</asp:Content>

