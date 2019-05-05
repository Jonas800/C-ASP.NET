<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="Åbningstider.aspx.cs" Inherits="Admin_Åbningstider" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2>Åbningstider</h2>

    <asp:Panel ID="Panel_Form" DefaultButton="Button_Gem" runat="server">
        <label>Dag</label>
        <asp:TextBox ID="TextBox_Dag" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator_Dag" runat="server" ControlToValidate="TextBox_Dag" ErrorMessage="Skal udfyldes" Display="Dynamic"></asp:RequiredFieldValidator>
        <label>Tid</label>
        <asp:TextBox ID="TextBox_Tid" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator_Tid" runat="server" ControlToValidate="TextBox_Tid" ErrorMessage="Skal udfyldes" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:Button ID="Button_Gem" runat="server" Text="Gem" OnClick="Button_Gem_Click" />
    </asp:Panel>

    <asp:Panel ID="Panel_Rækkefølge" runat="server" DefaultButton="Button_Ret">
        <asp:Repeater ID="Repeater_Åbningstider" runat="server">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>Dag</th>
                        <th>Tid</th>
                        <th>Rækkefølge</th>
                        <th>Ret</th>
                        <th>Slet</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Eval("åbningstid_dag") %></td>
                    <td><%# Eval("åbningstid_tid") %></td>
                    <td>
                        <asp:TextBox ID="TextBox_Rækkefølge" TextMode="Number" Text='<%# Eval("åbningstid_rækkefølge") %>' CssClass="order" runat="server"></asp:TextBox></td>
                    <td><a href="Åbningstider.aspx?action=ret&id=<%# Eval("åbningstid_id") %>">Ret</a></td>
                    <td><a onclick="return confirm('Er du sikker?')" href="Åbningstider.aspx?action=slet&id=<%# Eval("åbningstid_id") %>">Slet</a></td>
                    <asp:HiddenField ID="HiddenField_ID" runat="server" Value='<%# Eval("åbningstid_id") %>' />
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
        <asp:Button ID="Button_Ret" runat="server" Text="Gem rækkefølge" OnClick="Button_Ret_Click" />
    </asp:Panel>
</asp:Content>

