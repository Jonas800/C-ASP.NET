<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>Velkommen til BIOGRAFEN</h1>
    <p>I denne portal kan du se vores film, få tilbud, tildele ratings og skrive anmeldelser!</p>
    <asp:Repeater ID="Repeater_Forside" runat="server" OnItemDataBound="Repeater_Forside_ItemDataBound">
        <ItemTemplate>
            <a class="a_no_decoration" href="Filmbeskrivelse.aspx?id=<%# Eval("film_id") %>">
                <asp:Repeater ID="Repeater_nested" runat="server">
                    <ItemTemplate>
                        <img src="<%# Eval("billede_sti", "billeder/filmbeskrivelse/{0}") %>" />

                    </ItemTemplate>
                </asp:Repeater>
                <div class="Beskrivelse">
                    <h2>
                        <%# Eval("film_navn") %>
                    </h2>
                    <p>
                        <%# Eval("film_beskrivelse") %>
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

