<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <h2>Forside</h2>
    <asp:Repeater ID="Repeater_Forside" runat="server">
        <ItemTemplate>
            <h1><%# Eval("forside_titel") %></h1>
            <img src="<%# Eval("forside_billede", "/billeder/forside/{0}") %>" />
            <h3>Lidt om os...</h3>
            <p><%# Eval("forside_tekst").ToString().Replace(Environment.NewLine,"<br />") %></p>
        </ItemTemplate>
    </asp:Repeater>
    <asp:Repeater ID="Repeater_Kontaktoplysninger" runat="server">
        <HeaderTemplate>
            <ul class="forside_ul">
                <h3 class="forside_h3">Mettes planteskole</h3>
        </HeaderTemplate>
        <ItemTemplate>
            <li><%# Eval("kontaktoplysning_adresse") %></li>
            <li><%# Eval("kontaktoplysning_postnummer") %> <%# Eval("kontaktoplysning_by") %></li>
            <li><%# Eval("kontaktoplysning_telefon").ToString().Insert(2, " ").Insert(5, " ").Insert(8, " ") %></li>
            <li><%# Eval("kontaktoplysning_email") %></li>
        </ItemTemplate>
        <FooterTemplate>
            </ul>
        </FooterTemplate>
    </asp:Repeater>
    <asp:Repeater ID="Repeater_Åbningstider" runat="server">
        <HeaderTemplate>
            <ul class="forside_ul">
                <h3 class="forside_h3">Åbningstider</h3>
        </HeaderTemplate>
        <ItemTemplate>
            <li><span><%# Eval("åbningstid_dag") %></span><span><%# Eval("åbningstid_tid") %></span></li>
        </ItemTemplate>
        <FooterTemplate>
            </ul>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>

