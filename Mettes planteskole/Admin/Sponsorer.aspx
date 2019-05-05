<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="Sponsorer.aspx.cs" Inherits="Admin_Sponsorer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2>Sponsorer</h2>

    <label>Link til sponsor</label>
    <asp:TextBox ID="TextBox_Url" runat="server">
    </asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Url" runat="server" ControlToValidate="TextBox_Url" ErrorMessage="Skal udfyldes" Display="Dynamic"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator_Url" runat="server" ErrorMessage="Skal være et gyldigt link" Display="Dynamic" ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?" ControlToValidate="TextBox_Url"></asp:RegularExpressionValidator>
    <br />
    <label>Reklame</label>
    <asp:Image ID="Image_GammeltBillede" runat="server" />
    <asp:FileUpload ID="FileUpload_Billede" runat="server" />
    <asp:Label ID="Label_Error" runat="server"></asp:Label>
    <asp:HiddenField ID="HiddenField_Billede" runat="server" />
    <asp:Button ID="Button_Gem" runat="server" Text="Gem" OnClick="Button_Gem_Click" />
    <a href="Sponsorer.aspx">Annuller</a>
    <asp:Repeater ID="Repeater_Sponsor" runat="server">
        <HeaderTemplate>
            <table>
                <tr>
                    <th>Billede</th>
                    <th>Link</th>
                    <th>Ret</th>
                    <th>Slet</th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <img src='<%# Eval("sponsor_billede", "../billeder/sponsor/{0}") %>' /></td>
                <td><a href='<%# Eval("sponsor_url") %>'><%# Eval("sponsor_url") %></a></td>
                <td><a href="Sponsorer.aspx?action=ret&id=<%# Eval("sponsor_id") %>">Ret</a></td>
                <td><a onclick="return confirm('Er du sikker?')" href="Sponsorer.aspx?action=slet&id=<%# Eval("sponsor_id") %>">Slet</a></td>
            </tr>
        </ItemTemplate>
        <FooterTemplate></table></FooterTemplate>
    </asp:Repeater>
</asp:Content>

