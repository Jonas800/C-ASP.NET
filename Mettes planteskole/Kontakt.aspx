<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Kontakt.aspx.cs" Inherits="Kontakt" %>

<%@ Register Src="~/UserControl/UserControl_Kontaktformular.ascx" TagPrefix="uc1" TagName="UserControl_Kontaktformular" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <h2>Kontakt</h2>

    <h3>Mettes planteskole</h3>
    <asp:Repeater ID="Repeater_Kontaktoplysninger" runat="server">
        <ItemTemplate>
            <ul class="kontakt">
                <li><%# Eval("kontaktoplysning_adresse") %></li>
                <li><%# Eval("kontaktoplysning_postnummer") %> <%# Eval("kontaktoplysning_by") %></li>
                <li><%# Eval("kontaktoplysning_telefon").ToString().Insert(2, " ").Insert(5, " ").Insert(8, " ") %></li>
                <li><%# Eval("kontaktoplysning_email") %></li>
            </ul>
        </ItemTemplate>
    </asp:Repeater>
    <h3>Åbningstider</h3>
    <asp:Repeater ID="Repeater_Åbningstider" runat="server">
        <HeaderTemplate>
            <ul class="kontakt">
        </HeaderTemplate>
        <ItemTemplate>
            <li><span><%# Eval("åbningstid_dag") %></span><span><%# Eval("åbningstid_tid") %></span></li>
        </ItemTemplate>
        <FooterTemplate>
            </ul>
        </FooterTemplate>
    </asp:Repeater>
    <h3>Kontakt formular</h3>
    <uc1:UserControl_Kontaktformular runat="server" ID="UserControl_Kontaktformular" />
</asp:Content>

