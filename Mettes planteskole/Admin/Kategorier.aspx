<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="Kategorier.aspx.cs" Inherits="Admin_Kategorier" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2>Kategorier</h2>
    <label>Kategorinavn</label>
    <asp:TextBox ID="TextBox_Navn" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Navn" runat="server" ControlToValidate="TextBox_Navn" ErrorMessage="Skal udfyldes" Display="Dynamic"></asp:RequiredFieldValidator>
    <label>Sæt kategori til aktiv</label>
    <asp:CheckBox ID="CheckBox_Er_Aktiv" runat="server" />
    <asp:Button ID="Button_Gem" runat="server" Text="Gem" OnClick="Button_Gem_Click" />

    <asp:Repeater ID="Repeater_Kategorier" runat="server">
        <HeaderTemplate>
            <table>
                <tr>
                    <th>Navn</th>
                    <th>Ret</th>
                    <th>Deaktiver</th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td><%# Eval("kategori_navn") %></td>
                <td><a href="Kategorier.aspx?action=ret&id=<%# Eval("kategori_id") %>">Ret</a></td>
                <td><a href="Kategorier.aspx?action=slet&id=<%# Eval("kategori_id") %>" onclick="return confirm('Er du sikker?')">Deaktiver</a></td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>

