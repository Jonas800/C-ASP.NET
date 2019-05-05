<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="Designere.aspx.cs" Inherits="Admin_Designere" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="Panel_Besked" CssClass="Panel_Besked" runat="server">
        <asp:Label ID="Label_Besked" runat="server"></asp:Label>
    </asp:Panel>

    <asp:Panel ID="Panel_Designere" DefaultButton="Button_Gem" runat="server">
        <label for="TextBox_Designere">Designer</label>
        <asp:TextBox ID="TextBox_Designere" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator_Designere" runat="server" ControlToValidate="TextBox_Designere" ErrorMessage="Skal udfyldes" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:Button ID="Button_Gem" runat="server" Text="Gem" OnClick="Button_Gem_Click" />
    </asp:Panel>

    <asp:Repeater ID="Repeater_Designere" runat="server">
        <HeaderTemplate>
            <table>
                <tr>
                    <th>Designer</th>
                    <th>Ret</th>
                    <th>Slet</th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td><%# Eval("designer_navn") %></td>
                <td><a href="Designere.aspx?action=ret&id=<%# Eval("designer_id") %>"><i class="fa fa-edit"></i></a></td>
                <td><a class="sletTd" href="Designere.aspx?action=slet&id=<%# Eval("designer_id") %>" onclick="return confirm('Er du sikker?')"><i class="fa fa-trash"></i></a></td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>

