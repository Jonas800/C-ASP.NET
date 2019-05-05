<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="Nyheder.aspx.cs" Inherits="Admin_Nyheder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="Panel_Besked" CssClass="Panel_Besked" runat="server">
        <asp:Label ID="Label_Besked" runat="server"></asp:Label>
    </asp:Panel>

    <label for="TextBox_Overskrift">Overskrift</label>
    <asp:TextBox ID="TextBox_Overskrift" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Overskrift" runat="server" ControlToValidate="TextBox_Overskrift" ErrorMessage="Skal udfyldes" Display="Dynamic"></asp:RequiredFieldValidator>

    <label for="TextBox_Nyhed">Brødtekst</label>
    <asp:TextBox ID="TextBox_Nyhed" runat="server" CssClass="ckeditor" TextMode="MultiLine" Rows="10"></asp:TextBox>
    <script src="ckeditor/ckeditor.js"></script>

    <asp:Button ID="Button_Gem" runat="server" Text="Gem" OnClick="Button_Gem_Click" />

    <div class="responsivTable">
        <asp:Repeater ID="Repeater_Nyheder" runat="server">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>Forfatter</th>
                        <th>Overskrift</th>
                        <th>Brødtekst</th>
                        <th>Dato</th>
                        <th>Ret</th>
                        <th>Slet</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# (String.IsNullOrEmpty(Eval("bruger_navn").ToString()) ? "Slettet" : Eval("bruger_navn")) %></td>
                    <td><%# Eval("nyhed_overskrift") %></td>
                    <td><%# Eval("nyhed_tekst").ToString().Length <= 180 ? Eval("nyhed_tekst") : Eval("nyhed_tekst").ToString().Substring(0,180) + "..." %> </td>
                    <td><%# Eval("nyhed_dato") %></td>
                    <td><a href="Nyheder.aspx?action=ret&id=<%# Eval("nyhed_id") %>"><i class="fa fa-edit"></i></a></td>
                    <td><a class="sletTd" onclick="return confirm('Er du sikker?')" href="Nyheder.aspx?action=slet&id=<%# Eval("nyhed_id") %>"><i class="fa fa-trash"></i></a></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>

