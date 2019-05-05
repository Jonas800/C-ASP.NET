<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="OpretAktivitet.aspx.cs" Inherits="Admin_OpretAktivitet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <label>Navn</label>
    <asp:TextBox ID="TextBox_Navn" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Navn" runat="server" ControlToValidate="TextBox_Navn" ErrorMessage="Skal udfyldes" Display="Dynamic" ForeColor="DarkRed"></asp:RequiredFieldValidator>

    <label>Beskrivelse</label>
    <asp:TextBox ID="TextBox_Beskrivelse" TextMode="MultiLine" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Beskrivelse" runat="server" ControlToValidate="TextBox_Beskrivelse" ErrorMessage="Skal udfyldes" Display="Dynamic" ForeColor="DarkRed"></asp:RequiredFieldValidator>

    <label>Billede</label>
    <asp:Image ID="Image_Billede" runat="server" />
    <asp:FileUpload ID="FileUpload_Billede" runat="server" />
    <asp:HiddenField ID="HiddenField_Billede" runat="server" />
    <asp:Label ID="Label_FileUpload_Besked" runat="server" ForeColor="DarkRed"></asp:Label>

    <asp:Button ID="Button_Gem" runat="server" Text="Gem" OnClick="Button_Gem_Click" />

    <a href="Aktiviteter.aspx">Gå tilbage til aktiviteter</a>
</asp:Content>

