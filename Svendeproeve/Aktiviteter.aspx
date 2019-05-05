<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Aktiviteter.aspx.cs" Inherits="Aktiviteter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:Repeater ID="Repeater_Aktiviteter" runat="server">
        <ItemTemplate>
            <article class="aktiviteter kontaktOplysninger_tekst">
                <h3><%# Eval("aktivitet_navn") %></h3>

                <div class="nyhed">
                    <img src="<%# Eval("aktivitet_billede", "/billeder/produkter/{0}") %>" />
                    <%# Eval("aktivitet_tekst").ToString().Replace(Environment.NewLine,"<br />") %>
                </div>
            </article>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>

