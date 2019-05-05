<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="Tilmeldte.aspx.cs" Inherits="Admin_Tilmeldte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <h3>Hold</h3>
    <div class="responsivTable">
        <asp:Repeater ID="Repeater_Hold" runat="server">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>Aktivitet</th>
                        <th>Tidspunkt</th>
                        <th>Instruktør</th>
                        <th>Point</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Eval("aktivitet_navn") %></td>
                    <td><%# Convert.ToDateTime(Eval("hold_tidspunkt")).ToString("dd/MM/yyyy - HH:mm") %></td>
                    <td><%# Eval("bruger_navn") %></td>
                    <td><%# Eval("hold_point") %></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>

    <h3>Tilmeldte på hold</h3>
    <div class="responsivTable">
        <asp:Repeater ID="Repeater_Tilmeldte" OnItemCommand="Repeater_Tilmeldte_ItemCommand" runat="server">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>Medlem</th>
                        <th>Point</th>
                        <th>Tilstede</th>
                        <th>Gem</th>
                        <th>Fjern</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Eval("bruger_navn") %></td>
                    <td>
                        <asp:HiddenField ID="HiddenField_HoldBrugereID" Value='<%# Eval("hold_brugere_id") %>' runat="server" />
                        <asp:TextBox ID="TextBox_Point" runat="server" Text='<%# Eval("hold_brugere_point") %>' TextMode="Number"></asp:TextBox>
                    </td>
                    <td>
                        <asp:CheckBox ID="CheckBox_Tilstede" Checked='<%# Eval("hold_brugere_godkendt") %>' runat="server" />
                    </td>
                    <td>
                        <asp:Button ID="Button_Point" runat="server" Text="Gem" CommandName="changePoint" /></td>
                    <td><a class="sletTd" href="Tilmeldte.aspx?hold_id=<%# Request.QueryString["hold_id"] %>&id=<%# Eval("fk_bruger_id") %>&action=slet" onclick="return confirm('Er du sikker?')"><i class="fa fa-trash"></i></a></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
        <a class="a_opretprodukt" href="Hold.aspx">Tilbage til hold</a>
    </div>

</asp:Content>

