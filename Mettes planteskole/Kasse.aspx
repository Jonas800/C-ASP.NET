<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Kasse.aspx.cs" Inherits="Kasse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2>Kassen</h2>
    <asp:Panel ID="Panel_Bestil" runat="server">
        <asp:Repeater ID="Repeater_Kurv_Checkout" runat="server" ItemType="Produkt">
            <HeaderTemplate>
                <table class="butikken">
                    <tr>
                        <th>Stk.</th>
                        <th>Produkt</th>
                        <th>Pr. stk.</th>
                        <th>I alt</th>
                        <th></th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Item.Antal %></td>
                    <td><%# Item.Navn %></td>
                    <td><%# Item.Pris %></td>
                    <td><%# Item.SamletPris %></td>
                    <td><a href="?action=fjern&produkt=<%# Item.Id %>">Slet</a></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
        <div class="samletpris">
            <label>Samlet pris:</label>
            <label><%= Kurv.KurvensSamledePris() %> Kr.</label>
        </div>

        <asp:Button ID="Button_Bestil" CssClass="bestil" OnClick="Button_Bestil_Click" runat="server" Text="Bestil" />

        <asp:Label ID="Label_Tak" runat="server" Text="Tak for bestillingen!" Visible="false"></asp:Label>

        <a class="tilbage" href="Butikken.aspx">Tilbage</a>
    </asp:Panel>

    <asp:Panel ID="Panel_Opret_Or_Login" runat="server" Visible="false">
        <h3>Login til højre eller <a href="Opret.aspx">opret bruger</a> (bare rolig, din kurv er stadig gemt og du kan altid gå til kassen igen).</h3>
    </asp:Panel>
</asp:Content>

