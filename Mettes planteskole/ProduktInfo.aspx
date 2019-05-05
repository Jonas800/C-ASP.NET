<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ProduktInfo.aspx.cs" Inherits="ProduktInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_UnderMenu" runat="Server">
    <ul class="undermenu">
        <asp:Repeater ID="Repeater_UnderMenu" runat="server">
            <ItemTemplate>
                <li><a href="Butikken.aspx?Kategori=<%# Eval("kategori_id") %>"><%# Eval("kategori_navn") %></a></li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2>Butikken</h2>
    <section>
        <asp:Repeater ID="Repeater_ProduktInfo" runat="server" OnItemDataBound="Repeater_ProduktInfo_ItemDataBound" OnItemCommand="Repeater_ProduktInfo_ItemCommand">
            <ItemTemplate>
                <h1>
                    <asp:Literal ID="Literal_Navn" runat="server" Text='<%# Eval("produkt_navn") %>'></asp:Literal></h1>
                <div>
                    <asp:Repeater ID="Repeater_Billeder" runat="server">
                        <ItemTemplate>
                            <img src="<%# Eval("billede_sti", "/billeder/produkter/{0}") %>" />
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <p><%# Eval("produkt_beskrivelse") %></p>

                <aside>
                    <ul>
                        <li>
                            <h3>Jordtype:</h3>
                        </li>
                        <li><%# Eval("jordtype_navn") %></li>
                        <li>
                            <h3>Dyrkningstid:</h3>
                        </li>
                        <li><%# Eval("dyrkningstid_navn") %></li>
                        <li>
                            <h3>Varenummer:</h3>
                        </li>
                        <li><%# Eval("produkt_varenummer") %></li>
                    </ul>
                </aside>
                <aside class="koeb_nu">
                    <h4>Køb nu</h4>
                    <p>Pris:
                        <asp:Literal ID="Literal_Pris" runat="server" Text='<%# Eval("produkt_pris") %>'></asp:Literal>
                        kr.</p>
                    <label>Antal</label>
                    <asp:TextBox ID="TextBox_Antal" TextMode="Number" Text="1" runat="server"></asp:TextBox>
                    <asp:Button ID="Button_Køb" CommandName="køb" runat="server" Text="Tilføj til kurv" />
                </aside>
                <a href="Butikken.aspx?Kategori=<%# Eval("fk_kategori_id") %>">Tilbage</a>
            </ItemTemplate>
        </asp:Repeater>
    </section>
</asp:Content>

