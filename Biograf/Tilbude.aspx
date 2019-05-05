<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Tilbude.aspx.cs" Inherits="Tilbude" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>Tilbudsliste</h1>
    <asp:Label ID="Label_Tilbud" runat="server" Text="Tilmeld dig i dag, optjen point og få masser af gode tilbud!"></asp:Label>
    <asp:Repeater ID="Repeater_Tilbud" runat="server">
        <ItemTemplate>
            <div class="Tilbudsliste">
                <img src="<%# Eval("tilbud_billede", "/billeder/tilbud/{0}") %>" />
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>

