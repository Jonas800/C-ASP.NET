<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Profil.aspx.cs" Inherits="Profil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>Din profil</h1>
    <h3>Navn</h3>
    <asp:TextBox ID="TextBox_Navn" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Navn" runat="server" ControlToValidate="TextBox_Navn" ErrorMessage="Skal udfyldes"></asp:RequiredFieldValidator>

    <h3>Email</h3>
    <asp:TextBox ID="TextBox_Email" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Email" runat="server" ControlToValidate="TextBox_Email" ErrorMessage="Skal udfyldes"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator_Email" runat="server" ErrorMessage="Ikke en gyldig email" ControlToValidate="TextBox_Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>

    <h3>Kodeord</h3>
    <asp:TextBox ID="TextBox_Kodeord" TextMode="Password" runat="server"></asp:TextBox>

    <h3>Gentag kodeord</h3>
    <asp:TextBox ID="TextBox_Kodeord_Gentag" TextMode="Password" runat="server"></asp:TextBox>
    <asp:CompareValidator ID="CompareValidator_Kodeord" runat="server" ErrorMessage="Kodeord er ikke ens" ControlToValidate="TextBox_Kodeord_Gentag" ControlToCompare="TextBox_Kodeord"></asp:CompareValidator>

    <h3 class="Nyhedsbrev">Vil du modtage vores nyhedsbrev?</h3>
    <asp:CheckBox ID="CheckBox_Nyhedsbrev" runat="server" />
    <h3>Vælg dine favorit genrer</h3>
    <asp:CheckBoxList ID="CheckBoxList_Genre" RepeatDirection="Horizontal" CssClass="CheckBoxList" DataTextField="genre_navn" runat="server" DataValueField="genre_id"></asp:CheckBoxList>
    <asp:Button ID="Button_Gem" runat="server" Text="Gem" OnClick="Button_Gem_Click" />

    <a class="Form_a" href="Profil.aspx?slet=true">Slet din profil</a>
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <li>
        <a href="?logout=true" onclick="return confirm('Dette vil logge dig ud')"><i class="fa fa-sign-out fa-2x" aria-hidden="true"></i>
            Log ud</a>
    </li>
</asp:Content>--%>
