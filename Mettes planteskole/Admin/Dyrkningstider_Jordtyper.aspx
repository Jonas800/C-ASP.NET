<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="Dyrkningstider_Jordtyper.aspx.cs" Inherits="Admin_Dyrkningstider_Jordtyper" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--Dyrkningstider--%>
    <h2>Dyrkningstider</h2>
    <asp:Panel ID="Panel_Dyrkningstider" DefaultButton="Button_Dyrkningstid" runat="server">
        <label>Dyrkningstid</label>
        <asp:TextBox ID="TextBox_Dyrkningstid" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator_Dyrkningstid" runat="server" ControlToValidate="TextBox_Dyrkningstid" ErrorMessage="Skal udfyldes" Display="Dynamic" ValidationGroup="Dyrk"></asp:RequiredFieldValidator>
        <asp:Button ID="Button_Dyrkningstid" runat="server" Text="Gem" OnClick="Button_Dyrkningstid_Click" ValidationGroup="Dyrk" />
    </asp:Panel>

    <asp:Repeater ID="Repeater_Dyrkningstider" runat="server">
        <HeaderTemplate>
            <table>
                <tr>
                    <th>Dyrkningstid</th>
                    <th>Ret</th>
                    <th>Slet</th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td><%# Eval("dyrkningstid_navn") %></td>
                <td><a href="Dyrkningstider_Jordtyper.aspx?action=ret_dyrkningstid&dyrkningstid_id=<%# Eval("dyrkningstid_id") %>">Ret</a></td>
                <td><a onclick="return confirm('Er du sikker?')" href="Dyrkningstider_Jordtyper.aspx?action=slet_dyrkningstid&dyrkningstid_id=<%# Eval("dyrkningstid_id") %>">Slet</a></td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>

    <%--Jordtyper--%>
    <h2>Jordtyper</h2>
    <asp:Panel ID="Panel_Jordtyper" DefaultButton="Button_Jordtyper" runat="server">
        <label>Jordtyper</label>
        <asp:TextBox ID="TextBox_Jordtyper" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator_Jordtyper" runat="server" ControlToValidate="TextBox_Jordtyper" ErrorMessage="Skal udfyldes" ValidationGroup="Jord" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:Button ID="Button_Jordtyper" runat="server" Text="Gem" OnClick="Button_Jordtyper_Click" ValidationGroup="Jord" />
    </asp:Panel>

    <asp:Repeater ID="Repeater_Jordtyper" runat="server">
        <HeaderTemplate>
            <table>
                <tr>
                    <th>Jordtype</th>
                    <th>Ret</th>
                    <th>Slet</th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td><%# Eval("jordtype_navn") %></td>
                <td><a href="Dyrkningstider_Jordtyper.aspx?action=ret_jordtype&jordtype_id=<%# Eval("jordtype_id") %>">Ret</a></td>
                <td><a onclick="return confirm('Er du sikker?')" href="Dyrkningstider_Jordtyper.aspx?action=slet_jordtype&jordtype_id=<%# Eval("jordtype_id") %>">Slet</a></td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>

