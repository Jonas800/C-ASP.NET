<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="Produkter.aspx.cs" Inherits="Admin_Produkter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2>Produkter</h2>

    <a class="a_opretprodukt" href="OpretProdukt.aspx">Opret nyt produkt</a>

    <h3>Produktoversigt</h3>
    <asp:Repeater ID="Repeater_Produkter" runat="server">
        <HeaderTemplate>
            <table>
                <tr>
                    <th>Varenummer</th>
                    <th>Navn</th>
                    <th>Pris</th>
                    <th>Beskrivelse</th>
                    <th>Lager</th>
                    <th>Max lager</th>
                    <th>Min lager</th>
                    <th>Kategori</th>
                    <th>Jordtype</th>
                    <th>Dyrkningstid</th>
                    <th>Er aktivt</th>
                    <th>Billeder</th>
                    <th>Ret</th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr class="<%# UdsolgtClass(Convert.ToInt32(Eval("produkt_id"))) %>">
                <td><%# Eval("produkt_varenummer") %></td>
                <td><%# Eval("produkt_navn") + Udsolgt(Convert.ToInt32(Eval("produkt_id"))) %></td>
                <td><%# Eval("produkt_pris") %></td>
                <td><%# Eval("produkt_beskrivelse").ToString().PadRight(30).Substring(0,30).TrimEnd() %></td>
                <td><%# Eval("produkt_lager_stand") %></td>
                <td><%# Eval("produkt_lager_max") %></td>
                <td><%# Eval("produkt_lager_min") %></td>
                <td><%# Eval("kategori_navn") %></td>
                <td><%# Eval("jordtype_navn") %></td>
                <td><%# Eval("dyrkningstid_navn") %></td>
                <td><%# (Boolean.Parse(Eval("produkt_er_aktiv").ToString())) ? "Ja" : "Nej" %></td>
                <td>
                    <a href="Billeder.aspx?id=<%# Eval("produkt_id") %>"><%# Eval("total") %></a>
                </td>
                <td><a href="OpretProdukt.aspx?id=<%# Eval("produkt_id") %>">Ret</a></td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>

