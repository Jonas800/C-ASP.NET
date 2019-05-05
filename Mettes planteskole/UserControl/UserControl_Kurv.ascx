<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserControl_Kurv.ascx.cs" Inherits="UserControls_UserControl_Kurv" %>

<asp:Repeater ID="Repeater_Kurv" runat="server" ItemType="Produkt">
    <HeaderTemplate>
        <table class="kurv">
            <tr>
                <th>Produkt</th>
                <th>Pris</th>
            </tr>
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
            <td><%# Item.Antal %> <%# Item.Navn %></td>
            <td><%# Kurv.SamletPrisForEnVare(Item.Antal, Item.Pris) %></td>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
        <tr>
            <td></td>
            <td><a href="Kasse.aspx">Til kassen</a></td>
        </tr>
        </table>
    </FooterTemplate>
</asp:Repeater>
