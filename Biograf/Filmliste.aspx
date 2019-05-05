<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Filmliste.aspx.cs" Inherits="Filmliste" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>Filmliste</h1>
    <asp:Repeater ID="Repeater_Filmliste" runat="server" OnItemDataBound="Repeater_Filmliste_ItemDataBound">
        <ItemTemplate>
            <a href="Filmbeskrivelse.aspx?id=<%# Eval("film_id") %>" class="Filmliste">
                <asp:Repeater ID="Repeater_nested" runat="server">
                    <ItemTemplate>
                        <img src="<%# Eval("billede_sti", "billeder/filmliste/{0}") %>" />
                    </ItemTemplate>
                </asp:Repeater>

                <div class="Beskrivelse">
                    <h2>
                        <%# Eval("film_navn") %>
                    </h2>
                    <p>
                        <%# Eval("film_beskrivelse").ToString().Replace(Environment.NewLine,"<br />") %>
                    </p>
                </div>
                <asp:Repeater ID="Repeater_Genrer" runat="server">
                    <HeaderTemplate>
                        <p>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%# Eval("genre_navn") %>
                    </ItemTemplate>
                    <FooterTemplate></p></FooterTemplate>
                </asp:Repeater>
                <p>Global rating: <%# Convert.ToDecimal(Eval("vurderinger")).ToString("F2") %> ud af 5 stjerner</p>
            </a>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>


