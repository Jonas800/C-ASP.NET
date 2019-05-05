<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserControl_Kurv.ascx.cs" Inherits="UserControls_UserControl_Kurv" %>

<asp:Repeater ID="Repeater_Kurv" runat="server" ItemType="Produkt">
    <HeaderTemplate>
        <table>
            <tr>
                <th>ID</th>
                <th>Navn</th>
                <th>Pris</th>
                <th>Antal</th>
                <th>Samlet Pris</th>
                <th>Fjern</th>
            </tr>
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
            <td><%# Item.Id %></td>
            <td><%# Item.Navn %></td>
            <td><%# Item.Pris %></td>
            <td><%# Item.Antal %></td>
            <td><%# Item.SamletPris %></td>
            <td><a href="?action=fjern&produkt=<%# Item.Id %>">Fjern</a></td>
        </tr>

    </ItemTemplate>
    <FooterTemplate>
        <tr>
            <td>Kurvens Totalpris: 
            </td>
            <td><%# Kurv.KurvensSamledePris() %></td>
        </tr>
        </table>
    </FooterTemplate>
</asp:Repeater>
