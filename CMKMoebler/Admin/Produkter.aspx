<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="Produkter.aspx.cs" Inherits="Admin_Produkter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="Panel_Besked" CssClass="Panel_Besked" runat="server">
        <asp:Label ID="Label_Besked" runat="server"></asp:Label>
    </asp:Panel>

    <a class="a_opretprodukt" href="OpretProdukt.aspx">Opret nyt produkt</a>

    <h3>Produktoversigt</h3>
    <div class="responsivTable">
        <asp:Repeater ID="Repeater_Produkter" runat="server">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>Varenummer</th>
                        <th>Navn</th>
                        <th>Pris</th>
                        <th>År</th>
                        <th>Beskrivelse</th>
                        <th>Designer</th>
                        <th>Møbelserie</th>
                        <th>Billeder</th>
                        <th>Ret</th>
                        <th>Slet</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Eval("produkt_varenummer") %></td>
                    <td><%# Eval("produkt_navn") %></td>
                    <td><%# Eval("produkt_pris") %></td>
                    <td><%# Eval("produkt_aar") %></td>
                    <td><%# Eval("produkt_beskrivelse").ToString().PadRight(30).Substring(0,30).TrimEnd() %></td>
                    <td><%# Eval("designer_navn") %></td>
                    <td><%# Eval("serie_navn") %></td>
                    <td>
                        <a href="Billeder.aspx?id=<%# Eval("produkt_id") %>"><%# Eval("total") %> billeder</a>
                    </td>
                    <td><a href="OpretProdukt.aspx?id=<%# Eval("produkt_id") %>"><i class="fa fa-edit"></i></a></td>
                    <td><a class="sletTd" href="Produkter.aspx?id=<%# Eval("produkt_id") %>" onclick="return confirm('Er du sikker?')"><i class="fa fa-trash"></i></a></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>

