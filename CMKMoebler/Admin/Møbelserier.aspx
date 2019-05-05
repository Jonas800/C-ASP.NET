<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="Møbelserier.aspx.cs" Inherits="Admin_Møbelserier" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="Panel_Besked" CssClass="Panel_Besked" runat="server">
        <asp:Label ID="Label_Besked" runat="server"></asp:Label>
    </asp:Panel>
    <asp:Panel ID="Panel_Serier" DefaultButton="Button_Gem" runat="server">
        <label for="TextBox_Navn">Møbelserie</label>
        <asp:TextBox ID="TextBox_Navn" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator_Navn" runat="server" ControlToValidate="TextBox_Navn" ErrorMessage="Skal udfyldes" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:Button ID="Button_Gem" runat="server" Text="Gem" OnClick="Button_Gem_Click" />
    </asp:Panel>

    <asp:Repeater ID="Repeater_Serier" runat="server">
        <HeaderTemplate>
            <table>
                <tr>
                    <th>Møbelserie</th>
                    <th>Ret</th>
                    <th>Slet</th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td><%# Eval("serie_navn") %></td>
                <td><a href="Møbelserier.aspx?action=ret&id=<%# Eval("serie_id") %>"><i class="fa fa-edit"></i></a></td>
                <td><a class="sletTd" href="Møbelserier.aspx?action=slet&id=<%# Eval("serie_id") %>" onclick="return confirm('Er du sikker?')"><i class="fa fa-trash"></i></a></td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>

