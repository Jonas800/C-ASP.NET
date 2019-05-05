<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Filmbeskrivelse.aspx.cs" Inherits="Filmbeskrivelse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>Filmbeskrivelse</h1>
    <asp:Repeater ID="Repeater_Film" runat="server" OnItemDataBound="Repeater_Film_ItemDataBound">
        <ItemTemplate>
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
            <div>
                <h3>Stemningsbilleder</h3>
                <asp:Repeater ID="Repeater_Stemning" runat="server">
                    <ItemTemplate>
                        <img src="<%# Eval("billede_sti", "billeder/filmliste/{0}") %>" />
                    </ItemTemplate>
                </asp:Repeater>
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
            <%--            <div id="stjerner" runat="server" class="rating">
                <a href="?id=<%# Eval("film_id") %>&rating=1"><i class="fa fa-star fa-2x" aria-hidden="true"></i></a>
                <a href="?id=<%# Eval("film_id") %>&rating=2"><i class="fa fa-star fa-2x" aria-hidden="true"></i></a>
                <a href="?id=<%# Eval("film_id") %>&rating=3"><i class="fa fa-star fa-2x" aria-hidden="true"></i></a>
                <a href="?id=<%# Eval("film_id") %>&rating=4"><i class="fa fa-star fa-2x" aria-hidden="true"></i></a>
                <a href="?id=<%# Eval("film_id") %>&rating=5"><i class="fa fa-star fa-2x" aria-hidden="true"></i></a>
            </div>--%>
            <asp:Repeater ID="Repeater_Rating" runat="server">
                <ItemTemplate>
                    <p>Din rating: <%# Eval("rating_tal") %></p>
                </ItemTemplate>
            </asp:Repeater>
            <p>Global rating: <%# Convert.ToDecimal(Eval("vurderinger")).ToString("F2") %> ud af 5 stjerner</p>

        </ItemTemplate>
    </asp:Repeater>

    <ajaxToolkit:Rating ID="Rating_Stjerner" runat="server" EmptyStarCssClass="emptystar" FilledStarCssClass="filledstar" StarCssClass="filledstar" WaitingStarCssClass="filledstar" CurrentRating="0" MaxRating="5" OnClick="Rating_Stjerner_Click" AutoPostBack="true"></ajaxToolkit:Rating>
    <%--    <div id="stjerner" runat="server" class="rating">
                <a href="?id=<%= Request.QueryString["id"] %>&rating=1"><i class="fa fa-star fa-2x" aria-hidden="true"></i></a>
        <a href="?id=<%= Request.QueryString["id"] %>&rating=2"><i class="fa fa-star fa-2x" aria-hidden="true"></i></a>
        <a href="?id=<%= Request.QueryString["id"] %>&rating=3"><i class="fa fa-star fa-2x" aria-hidden="true"></i></a>
        <a href="?id=<%= Request.QueryString["id"] %>&rating=4"><i class="fa fa-star fa-2x" aria-hidden="true"></i></a>
        <a href="?id=<%= Request.QueryString["id"] %>&rating=5"><i class="fa fa-star fa-2x" aria-hidden="true"></i></a>
    </div>--%>
    <asp:Label ID="Label_Rating_Error" runat="server" CssClass="Label_Rating_Error" Text=""></asp:Label>
    <a class="Form_a2" href="Anmeldelser.aspx?id=<%= Request.QueryString["id"] %>">Skriv en anmeldelse af denne film!</a>

    <br />

    <asp:Repeater ID="Repeater_Anmeldelser" runat="server">
        <HeaderTemplate>
            <h3>Brugernes anmeldelser</h3>
        </HeaderTemplate>
        <ItemTemplate>
            <div class="Anmeldelser">
                <h5><%# Eval("bruger_navn") %>
                </h5>
                <p><%# Eval("anmeldelse_tekst").ToString().Replace(Environment.NewLine,"<br />").ToString().Replace(Environment.NewLine,"<br />") %></p>
                <h5><%# Eval("rating_tal") %> ud af 5 stjerner</h5>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>

