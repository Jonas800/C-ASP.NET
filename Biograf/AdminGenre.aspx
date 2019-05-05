<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" CodeFile="AdminGenre.aspx.cs" Inherits="AdminGenre" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>Administrer genrer</h1>
    <h3>Genrenavn</h3>
    <asp:TextBox ID="TextBox_Genre" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Genre" runat="server" ErrorMessage="Skal udfyldes" ControlToValidate="TextBox_Genre" Display="Dynamic"></asp:RequiredFieldValidator>
    <asp:Button ID="Button_Gem" runat="server" Text="Gem" OnClick="Button_Gem_Click" />
    <a class="Form_a2" href="AdminGenre.aspx">Annullér</a>


    <asp:Repeater ID="Repeater_Genre" runat="server">
        <HeaderTemplate>
            <table>
                <tr>
                    <th>Navn</th>
                    <th>Ret</th>
                    <th>Slet</th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <%# Eval("genre_navn") %>
                </td>
                <td><a href="?handling=ret&id=<%# Eval("genre_id") %>"><i class="fa fa-pencil" aria-hidden="true"></i></a></td>
                <td><a href="?handling=slet&id=<%# Eval("genre_id") %>"><i class="fa fa-trash" aria-hidden="true"></i></a></td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>

</asp:Content>

