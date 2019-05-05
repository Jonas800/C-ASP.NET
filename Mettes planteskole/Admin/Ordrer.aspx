<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="Ordrer.aspx.cs" Inherits="Admin_Ordrer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2>Ordrer</h2>
    <asp:Repeater ID="Repeater_Ordrer" runat="server" OnItemDataBound="Repeater_Ordrer_ItemDataBound">
        <HeaderTemplate>
        </HeaderTemplate>
        <ItemTemplate>
            <table class="ordrer_table">
                <tr>
                    <th>Kunde</th>
                    <th>Tid</th>
                    <th>Status</th>
                    <th>Adresse</th>
                    <th>Postnummer</th>
                    <th>By</th>
                    <th>Telefon</th>
                </tr>
                <tr>
                    <td><%# Eval("kunde_navn") %></td>
                    <td><%# Eval("ordre_datetime") %></td>
                    <td><%# Eval("status_navn") %></td>
                    <td><%# Eval("kunde_adresse") %></td>
                    <td><%# Eval("kunde_postnummer") %></td>
                    <td><%# Eval("kunde_by") %></td>
                    <td><%# Eval("kunde_telefon") %></td>
                </tr>
                <asp:Repeater ID="Repeater_Ordrer_Linjer" runat="server">
                    <HeaderTemplate>
                        <tr>
                            <th>Produkt navn</th>
                            <th>Antal</th>
                            <th>Pris</th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("produkt_navn") %></td>
                            <td><%# Eval("ordre_linje_antal") %></td>
                            <td><%# Eval("ordre_linje_pris") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <a class="retOrdrer" href="RetOrdrer.aspx?id=<%# Eval("ordre_id") %>">Ret denne ordre.</a>
        </ItemTemplate>
        <FooterTemplate>
        </FooterTemplate>
    </asp:Repeater>

</asp:Content>

