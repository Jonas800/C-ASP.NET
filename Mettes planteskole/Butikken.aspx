<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Butikken.aspx.cs" Inherits="Butikken" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content_UnderMenu" ContentPlaceHolderID="ContentPlaceHolder_UnderMenu" runat="Server">
    <ul class="undermenu">
        <asp:Repeater ID="Repeater_UnderMenu" runat="server">
            <ItemTemplate>
                <li><a class="<%# HighlightUnderMenu(Eval("kategori_id").ToString()) %>" href="Butikken.aspx?Kategori=<%# Eval("kategori_id") %>"><%# Eval("kategori_navn") %></a></li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <h2>Butikken</h2>

    <asp:Repeater ID="Repeater_Produkter" runat="server">
        <HeaderTemplate>
            <table class="butikken">
                <tr>
                    <th>Stk</th>
                    <th>Produkt navn</th>
                    <th>Pris</th>
                    <th>mere info</th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr class="<%# UdsolgtClass(Convert.ToInt32(Eval("produkt_id"))) %>">
                <td>
                    <asp:TextBox ID="TextBox_Antal" TextMode="Number" Text="0" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Literal ID="Literal_Navn" Text='<%# Eval("produkt_navn") + Udsolgt(Convert.ToInt32(Eval("produkt_id"))) %>' runat="server"></asp:Literal></td>
                <td>
                    <asp:Literal ID="Literal_Pris" Text='<%# Eval("produkt_pris") %>' runat="server"></asp:Literal></td>
                <td><a href="ProduktInfo.aspx?id=<%# Eval("produkt_id") %>">Info</a></td>
                <asp:HiddenField ID="HiddenField_ID" Value='<%# Eval("produkt_id") %>' runat="server" />
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    <asp:Button ID="Button_Gem" CssClass="opdater_kurv" OnClick="Button_Gem_Click" runat="server" Text="Opdater kurv" />
</asp:Content>

