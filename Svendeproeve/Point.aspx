<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Point.aspx.cs" Inherits="Point" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="responsivTable">
        <asp:Repeater ID="Repeater_Hold" runat="server">
            <HeaderTemplate>
                <h3>Holdoversigt</h3>

                <table class="point">
                    <tr>
                        <th>Aktivitet</th>
                        <th>Tidspunkt</th>
                        <th>Instruktør</th>
                        <th>Point</th>
                        <th>Tilmeldte</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Eval("aktivitet_navn") %></td>
                    <td><%# Convert.ToDateTime(Eval("hold_tidspunkt")).ToString("dd/MM/yyyy - HH:mm") %></td>
                    <td><%# Eval("bruger_navn") %></td>
                    <td><%# Eval("hold_point") %></td>
                    <td><a href="Point.aspx?hold_id=<%# Eval("hold_id") %>">(<%# Eval("total") %>) tilmeldte</a></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    <div class="responsivTable">
        <asp:Repeater ID="Repeater_Tilmeldte" runat="server">
            <HeaderTemplate>
                <a href="Point.aspx">Tilbage til hold</a>

                <h3>Tilmeldte på hold</h3>

                <table class="point">
                    <tr>
                        <th>Medlem</th>
                        <th>Point for hold</th>
                        <th>Point modtaget</th>
                        <th>Tilstede</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Eval("bruger_navn") %></td>
                    <td><%# Eval("hold_point") %></td>
                    <td><%# Eval("hold_brugere_point") %></td>
                    <td>
                        <asp:HiddenField ID="HiddenField_HoldBrugereID" Value='<%# Eval("hold_brugere_id") %>' runat="server" />
                        <asp:HiddenField ID="HiddenField_Point" Value='<%# Eval("hold_point") %>' runat="server" />
                        <asp:CheckBox ID="CheckBox_Tilstede" Checked='<%# Eval("hold_brugere_godkendt") %>' runat="server" />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
        <asp:Button ID="Button_Point" runat="server" OnClick="Button_Point_Click" Text="Gem"  Visible="false"/>

        <asp:Panel ID="Panel_Ryd_Point" runat="server" Visible="false">
            <asp:Button ID="Button_Ryd_Point" CssClass="RydAlle" runat="server" Text="Ryd alle point" OnClick="Button_Ryd_Point_Click"  OnClientClick="return confirm('Er du sikker?')"/>
        </asp:Panel>

    </div>

</asp:Content>

