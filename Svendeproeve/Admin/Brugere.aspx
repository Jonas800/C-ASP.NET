<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="Brugere.aspx.cs" Inherits="Admin_Brugere" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <label>Email</label>
    <asp:TextBox ID="TextBox_Email" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Email" runat="server" ControlToValidate="TextBox_Email" ErrorMessage="Skal udfyldes" Display="Dynamic" ForeColor="DarkRed"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator_Email" runat="server" ErrorMessage="Ikke en gyldig email" ControlToValidate="TextBox_Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic"></asp:RegularExpressionValidator>
    <asp:Label ID="Label_Error" runat="server" ForeColor="DarkRed"></asp:Label>

    <label>Navn</label>
    <asp:TextBox ID="TextBox_Navn" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Navn" runat="server" ControlToValidate="TextBox_Navn" ErrorMessage="Skal udfyldes" Display="Dynamic" ForeColor="DarkRed"></asp:RequiredFieldValidator>

    <label>Kodeord</label>
    <asp:TextBox ID="TextBox_Kodeord" runat="server" TextMode="Password"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Kodeord" runat="server" ControlToValidate="TextBox_Kodeord" ErrorMessage="Skal udfyldes" Display="Dynamic" ForeColor="DarkRed"></asp:RequiredFieldValidator>
    <label>Gentag kodeord</label>
    <asp:TextBox ID="TextBox_Kodeord_Gentag" TextMode="Password" runat="server"></asp:TextBox>
    <asp:CompareValidator ForeColor="DarkRed" ID="CompareValidator_Kodeord" Display="Dynamic" runat="server" ControlToCompare="TextBox_Kodeord_Gentag" ControlToValidate="TextBox_Kodeord" ErrorMessage="Kodeord er ikke ens"></asp:CompareValidator>

    <label>Rolle</label>
    <asp:DropDownList ID="DropDownList_Rolle" DataTextField="rolle_navn" DataValueField="rolle_id" runat="server"></asp:DropDownList>
    <asp:RequiredFieldValidator ForeColor="DarkRed" ID="RequiredFieldValidator_Rolle" runat="server" ControlToValidate="DropDownList_Rolle" InitialValue="Vælg rolle" ErrorMessage="Skal vælges" Display="Dynamic"></asp:RequiredFieldValidator>


    <asp:Button ID="Button_Gem" runat="server" Text="Gem" OnClick="Button_Gem_Click" />

    <h3>Brugeroversigt</h3>
    <asp:Repeater ID="Repeater_Brugere" runat="server">
        <HeaderTemplate>
            <table>
                <tr>
                    <th>Email</th>
                    <th>Navn</th>
                    <th>Rolle</th>
                    <th>Ret</th>
                    <th>Slet</th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td><%# Eval("bruger_email") %></td>
                <td><%# Eval("bruger_navn") %></td>
                <td><%# Eval("rolle_navn") %></td>
                <td><a href="Brugere.aspx?action=ret&id=<%# Eval("bruger_id") %>"><i class="fa fa-edit"></i></a></td>
                <td><a class="sletTd" href="Brugere.aspx?action=slet&id=<%# Eval("bruger_id") %>&rolle=<%# Eval("fk_rolle_id") %>" onclick="return confirm('Er du sikker?')"><i class="fa fa-trash"></i></a></td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>

