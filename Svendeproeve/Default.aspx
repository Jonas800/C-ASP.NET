<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Repeater ID="Repeater_Forside" runat="server">
        <ItemTemplate>
            <article class="kontaktOplysninger_tekst forside">
                <%# Eval("oplysning_forside") %>
            </article>
        </ItemTemplate>
    </asp:Repeater>

    <h2>Nyheder</h2>

    <asp:Repeater ID="Repeater_Nyheder" runat="server">
        <ItemTemplate>
            <article class="aktiviteter">
                <h3><%# Eval("aktivitet_navn") %></h3>

                <div class="nyhed">
                    <img src="<%# Eval("aktivitet_billede", "/billeder/produkter/{0}") %>" />
                    <%# Eval("aktivitet_tekst").ToString().Replace(Environment.NewLine,"<br />") %>
                </div>
            </article>

        </ItemTemplate>
    </asp:Repeater>
</asp:Content>

