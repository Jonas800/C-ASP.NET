<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" CodeFile="AdminTilbud.aspx.cs" Inherits="AdminTilbud" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>Administrer tilbud</h1>
    <h3>Upload et tilbud</h3>
    <asp:FileUpload ID="FileUpload_Tilbud" runat="server" />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Tilbud" runat="server" ErrorMessage="Skal have fil" ControlToValidate="FileUpload_Tilbud"></asp:RequiredFieldValidator>
    <h3>Hvem kan den vises til?</h3>
    <asp:RadioButtonList ID="RadioButtonList_Roller" runat="server"></asp:RadioButtonList>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Roller" runat="server" ErrorMessage="Skal vælge en" ControlToValidate="RadioButtonList_Roller" Display="Dynamic"></asp:RequiredFieldValidator>
    <asp:Button ID="Button_Gem" runat="server" Text="Gem" OnClick="Button_Gem_Click" />
    <a class="Form_a2" href="AdminTilbud.aspx">Annullér</a>

    <asp:HiddenField ID="HiddenField_Billede" runat="server" />

    <asp:Repeater ID="Repeater_Tilbud" runat="server">
        <HeaderTemplate>
            <table>
                <tr>
                    <th>Billede
                    </th>
                    <th>Rolle
                    </th>
                    <th>Ret
                    </th>
                    <th>Slet
                    </th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <img class="Admin_Tilbud" src="<%# Eval("tilbud_billede", "/billeder/tilbud/{0}") %>" />
                </td>
                <td>
                    <%# Eval("tilbud_rolle") %>
                </td>
                <td><a href="?handling=ret&id=<%# Eval("tilbud_id") %>"><i class="fa fa-pencil" aria-hidden="true"></i></a></td>
                <td><a href="?handling=slet&id=<%# Eval("tilbud_id") %>"><i class="fa fa-trash" aria-hidden="true"></i></a></td>
            </tr>
        </ItemTemplate>
        <FooterTemplate></table></FooterTemplate>
    </asp:Repeater>
</asp:Content>

